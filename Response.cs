using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

// Token: 0x02000097 RID: 151
[Obfuscation(Exclude = true, ApplyToMembers = true)]
internal class Response
{
	// Token: 0x06000362 RID: 866 RVA: 0x00018AB4 File Offset: 0x00016CB4
	public Response()
	{
	}

	// Token: 0x06000363 RID: 867 RVA: 0x00018AF4 File Offset: 0x00016CF4
	public Response(Stream stream)
	{
		this.ReadFromStream(stream);
	}

	// Token: 0x06000364 RID: 868 RVA: 0x00018B48 File Offset: 0x00016D48
	public Response(Stream stream, string req)
	{
		this.req = req;
		this.ReadFromStream(stream);
	}

	// Token: 0x17000064 RID: 100
	// (get) Token: 0x06000365 RID: 869 RVA: 0x00018BA0 File Offset: 0x00016DA0
	public string Text
	{
		get
		{
			if (this.bytes == null)
			{
				return string.Empty;
			}
			return Encoding.UTF8.GetString(this.bytes);
		}
	}

	// Token: 0x17000065 RID: 101
	// (get) Token: 0x06000366 RID: 870 RVA: 0x00018BC4 File Offset: 0x00016DC4
	public string Asset
	{
		get
		{
			throw new NotSupportedException("This can't be done, yet.");
		}
	}

	// Token: 0x06000367 RID: 871 RVA: 0x00018BD0 File Offset: 0x00016DD0
	private void AddHeader(string name, string value)
	{
		name = name.ToLower().Trim();
		value = value.Trim();
		if (!this.headers.ContainsKey(name))
		{
			this.headers[name] = new List<string>();
		}
		this.headers[name].Add(value);
	}

	// Token: 0x06000368 RID: 872 RVA: 0x00018C28 File Offset: 0x00016E28
	public List<string> GetHeaders(string name)
	{
		name = name.ToLower().Trim();
		if (!this.headers.ContainsKey(name))
		{
			this.headers[name] = new List<string>();
		}
		return this.headers[name];
	}

	// Token: 0x06000369 RID: 873 RVA: 0x00018C70 File Offset: 0x00016E70
	public string GetHeader(string name)
	{
		name = name.ToLower().Trim();
		if (!this.headers.ContainsKey(name))
		{
			return string.Empty;
		}
		return this.headers[name][this.headers[name].Count - 1];
	}

	// Token: 0x0600036A RID: 874 RVA: 0x00018CC8 File Offset: 0x00016EC8
	private string ReadLine(Stream stream)
	{
		List<byte> list = new List<byte>();
		this._timer.Start();
		while (this._timer.ElapsedMilliseconds < 30000L)
		{
			int num = stream.ReadByte();
			if (num != -1)
			{
				this._timer.Reset();
				this._timer.Start();
				byte b = (byte)num;
				if (b == Request.EOL[1])
				{
					break;
				}
				list.Add(b);
			}
		}
		return Encoding.UTF8.GetString(list.ToArray()).Trim();
	}

	// Token: 0x0600036B RID: 875 RVA: 0x00018D5C File Offset: 0x00016F5C
	private string[] ReadKeyValue(Stream stream)
	{
		string text = this.ReadLine(stream);
		if (text == string.Empty)
		{
			return null;
		}
		int num = text.IndexOf(':');
		if (num == -1)
		{
			return null;
		}
		return new string[]
		{
			text.Substring(0, num).Trim(),
			text.Substring(num + 1).Trim()
		};
	}

	// Token: 0x0600036C RID: 876 RVA: 0x00018DC0 File Offset: 0x00016FC0
	private void ReadFromStream(Stream inputStream)
	{
		string text = this.ReadLine(inputStream);
		string[] array = text.Split(new char[]
		{
			' '
		});
		if (array == null || array.Length < 1 || text == " ")
		{
			this.bytes = new byte[0];
			return;
		}
		MemoryStream memoryStream = new MemoryStream();
		try
		{
			if (!int.TryParse(array[1], out this.status))
			{
				throw new HTTPException("Bad Status Code: [" + text + "]");
			}
			this.message = string.Join(" ", array, 2, array.Length - 2);
			this.headers.Clear();
		}
		catch (Exception innerException)
		{
			string text2 = string.Empty;
			for (int i = 0; i < array.Length; i++)
			{
				text2 = text2 + array[0] + "|";
			}
			throw new Exception(string.Concat(new object[]
			{
				"Request:",
				this.req,
				"\nResponce:",
				text,
				"\nLen = ",
				array.Length,
				" ",
				text2
			}), innerException);
		}
		for (;;)
		{
			string[] array2 = this.ReadKeyValue(inputStream);
			if (array2 == null || array2.Length == 0)
			{
				break;
			}
			this.AddHeader(array2[0], array2[1]);
		}
		if (this.GetHeader("transfer-encoding") == "chunked")
		{
			for (;;)
			{
				string text3 = this.ReadLine(inputStream);
				if (text3 == "0")
				{
					break;
				}
				int num = int.Parse(text3, NumberStyles.AllowHexSpecifier);
				for (int j = 0; j < num; j++)
				{
					memoryStream.WriteByte((byte)inputStream.ReadByte());
				}
				inputStream.ReadByte();
				inputStream.ReadByte();
			}
			for (;;)
			{
				string[] array3 = this.ReadKeyValue(inputStream);
				if (array3 == null)
				{
					break;
				}
				this.AddHeader(array3[0], array3[1]);
			}
		}
		else
		{
			int num2 = 0;
			if (!int.TryParse(this.GetHeader("content-length"), out num2))
			{
				throw new HTTPException("fail parsing content-length");
			}
			this._timer.Reset();
			this._timer.Start();
			for (int k = 0; k < num2; k++)
			{
				int num3 = inputStream.ReadByte();
				while (this._timer.ElapsedMilliseconds < 30000L && num3 == -1)
				{
					num3 = inputStream.ReadByte();
				}
				memoryStream.WriteByte((byte)num3);
			}
		}
		if (this.GetHeader("content-encoding").Contains("gzip"))
		{
			MemoryStream memoryStream2 = new MemoryStream();
			memoryStream.Seek(0L, SeekOrigin.Begin);
			this.bytes = memoryStream2.ToArray();
		}
		else
		{
			this.bytes = memoryStream.ToArray();
		}
	}

	// Token: 0x0400038D RID: 909
	public int status = 200;

	// Token: 0x0400038E RID: 910
	public string message = "OK";

	// Token: 0x0400038F RID: 911
	public byte[] bytes;

	// Token: 0x04000390 RID: 912
	public string req = string.Empty;

	// Token: 0x04000391 RID: 913
	private Dictionary<string, List<string>> headers = new Dictionary<string, List<string>>();

	// Token: 0x04000392 RID: 914
	private Stopwatch _timer = new Stopwatch();
}
