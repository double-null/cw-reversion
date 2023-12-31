using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

// Token: 0x02000096 RID: 150
internal class Request
{
	// Token: 0x06000353 RID: 851 RVA: 0x00017F54 File Offset: 0x00016154
	public Request()
	{
	}

	// Token: 0x06000354 RID: 852 RVA: 0x00017FAC File Offset: 0x000161AC
	public Request(string method, string uri)
	{
		this.method = method;
		this.uri = new Uri(uri);
	}

	// Token: 0x06000355 RID: 853 RVA: 0x00018018 File Offset: 0x00016218
	public Request(string method, string uri, bool useCache, bool isFile = false)
	{
		this.method = method;
		this.uri = new Uri(uri);
		this.useCache = useCache;
		this.isFile = isFile;
	}

	// Token: 0x06000356 RID: 854 RVA: 0x00018094 File Offset: 0x00016294
	public Request(string method, string uri, byte[] bytes, bool isFile = false)
	{
		this.method = method;
		this.uri = new Uri(uri);
		this.bytes = bytes;
		this.isFile = isFile;
	}

	// Token: 0x06000358 RID: 856 RVA: 0x00018134 File Offset: 0x00016334
	public void AddHeader(string name, string value)
	{
		name = name.ToLower().Trim();
		value = value.Trim();
		if (!this.headers.ContainsKey(name))
		{
			this.headers[name] = new List<string>();
		}
		this.headers[name].Add(value);
	}

	// Token: 0x06000359 RID: 857 RVA: 0x0001818C File Offset: 0x0001638C
	public void SetHeader(string name, string value)
	{
		name = name.ToLower().Trim();
		value = value.Trim();
		if (!this.headers.ContainsKey(name))
		{
			this.headers[name] = new List<string>();
		}
		this.headers[name].Clear();
		this.headers[name].Add(value);
	}

	// Token: 0x0600035A RID: 858 RVA: 0x000181F4 File Offset: 0x000163F4
	public string GetHeader(string name)
	{
		name = name.ToLower().Trim();
		if (!this.headers.ContainsKey(name))
		{
			return string.Empty;
		}
		return this.headers[name][0];
	}

	// Token: 0x0600035B RID: 859 RVA: 0x00018238 File Offset: 0x00016438
	public List<string> GetHeaders(string name)
	{
		name = name.ToLower().Trim();
		if (!this.headers.ContainsKey(name))
		{
			this.headers[name] = new List<string>();
		}
		return this.headers[name];
	}

	// Token: 0x0600035C RID: 860 RVA: 0x00018280 File Offset: 0x00016480
	public static X509Certificate SelectLocalCertificate(object sender, string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers)
	{
		global::Console.WriteLine("Client is selecting ky local certificate.", null);
		if (acceptableIssuers != null && acceptableIssuers.Length > 0 && localCertificates != null && localCertificates.Count > 0)
		{
			foreach (X509Certificate x509Certificate in localCertificates)
			{
				string issuer = x509Certificate.Issuer;
				if (Array.IndexOf<string>(acceptableIssuers, issuer) != -1)
				{
					return x509Certificate;
				}
			}
		}
		if (localCertificates != null && localCertificates.Count > 0)
		{
			return localCertificates[0];
		}
		return null;
	}

	// Token: 0x0600035D RID: 861 RVA: 0x00018354 File Offset: 0x00016554
	public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
	{
		return true;
	}

	// Token: 0x0600035E RID: 862 RVA: 0x00018358 File Offset: 0x00016558
	public void Send()
	{
		this.isDone = false;
		if (CVars.n_acceptGzip)
		{
			this.SetHeader("Accept-Encoding", "gzip");
		}
		ThreadPool.QueueUserWorkItem(new WaitCallback(this.SendCallback));
	}

	// Token: 0x0600035F RID: 863 RVA: 0x00018390 File Offset: 0x00016590
	private void SendBody()
	{
		int num = 0;
		while (++num < this.maximumRetryCount)
		{
			if (this.useCache)
			{
				List<string> cookies = HtmlLayer.Cookies;
				lock (cookies)
				{
					foreach (string value in HtmlLayer.Cookies)
					{
						this.AddHeader("Cookie", value);
					}
				}
				string empty = string.Empty;
				if (Request.etags.TryGetValue(this.uri.AbsoluteUri, out empty))
				{
					this.SetHeader("If-None-Match", empty);
				}
			}
			this.SetHeader("Host", this.uri.Host);
			this.client = new TcpClient();
			this.client.SendTimeout = 30000;
			this.client.LingerState.LingerTime = 30000;
			this.client.ReceiveTimeout = 30000;
			this.client.Connect(this.uri.Host, this.uri.Port);
			Stream stream = this.client.GetStream();
			if (this.uri.Scheme.ToLower() == "https")
			{
				stream = new SslStream(this.client.GetStream(), false, new RemoteCertificateValidationCallback(Request.ValidateServerCertificate));
				try
				{
					(stream as SslStream).AuthenticateAsClient(this.uri.Host);
				}
				catch (Exception ex)
				{
					global::Console.exception(ex);
					throw ex;
				}
			}
			this.WriteToStream(stream);
			this.response = new Response(stream, this.str);
			this.client.Close();
			int status = this.response.status;
			if (status != 301 && status != 302 && status != 307)
			{
				num = this.maximumRetryCount;
			}
			else
			{
				this.uri = new Uri(this.response.GetHeader("Location"));
			}
		}
		if (this.useCache)
		{
			List<string> cookies2 = HtmlLayer.Cookies;
			lock (cookies2)
			{
				if (HtmlLayer.Cookies.Count == 0)
				{
					HtmlLayer.Cookies = this.response.GetHeaders("SET-COOKIE");
				}
			}
			string header = this.response.GetHeader("etag");
			if (header.Length > 0)
			{
				Request.etags[this.uri.AbsoluteUri] = header;
			}
		}
	}

	// Token: 0x06000360 RID: 864 RVA: 0x000186A4 File Offset: 0x000168A4
	private void SendCallback(object state)
	{
		while (!this.isDone)
		{
			try
			{
				this.SendBody();
				this.isDone = true;
			}
			catch (Exception ex)
			{
				this.client.Close();
				this.maxTryCount--;
				if (this.maxTryCount == 0)
				{
					this.exception = ex;
					this.isDone = true;
					break;
				}
			}
		}
	}

	// Token: 0x06000361 RID: 865 RVA: 0x0001872C File Offset: 0x0001692C
	private void WriteToStream(Stream outputStream)
	{
		MemoryStream memoryStream = new MemoryStream();
		BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
		binaryWriter.Write(Encoding.UTF8.GetBytes(string.Concat(new string[]
		{
			this.method,
			" ",
			this.uri.PathAndQuery,
			" ",
			this.protocol
		})));
		binaryWriter.Write(Request.EOL);
		foreach (string text in this.headers.Keys)
		{
			foreach (string s in this.headers[text])
			{
				binaryWriter.Write(Encoding.UTF8.GetBytes(text));
				binaryWriter.Write(':');
				binaryWriter.Write(' ');
				binaryWriter.Write(Encoding.UTF8.GetBytes(s));
				binaryWriter.Write(Request.EOL);
			}
		}
		if (this.isFile)
		{
			MemoryStream memoryStream2 = new MemoryStream();
			BinaryWriter binaryWriter2 = new BinaryWriter(memoryStream2);
			binaryWriter2.Write(Encoding.UTF8.GetBytes("--ffeUUzIN7c5D11L9OnBOzpTStFF5YQasiMeyBBgp"));
			binaryWriter2.Write(Request.EOL);
			binaryWriter2.Write(Encoding.UTF8.GetBytes("Content-disposition: form-data; name=\"file1\"; filename=\"screenshot.png\""));
			binaryWriter2.Write(Request.EOL);
			binaryWriter2.Write(Encoding.UTF8.GetBytes("Content-Type: image/png"));
			binaryWriter2.Write(Request.EOL);
			binaryWriter2.Write(Encoding.UTF8.GetBytes("Content-Length: " + this.bytes.Length));
			binaryWriter2.Write(Request.EOL);
			binaryWriter2.Write(Request.EOL);
			binaryWriter2.Write(this.bytes);
			binaryWriter2.Write(Request.EOL);
			binaryWriter2.Write(Encoding.UTF8.GetBytes("--ffeUUzIN7c5D11L9OnBOzpTStFF5YQasiMeyBBgp--"));
			binaryWriter.Write(Encoding.UTF8.GetBytes("Content-Type: multipart/form-data; boundary=\"ffeUUzIN7c5D11L9OnBOzpTStFF5YQasiMeyBBgp\""));
			binaryWriter.Write(Request.EOL);
			binaryWriter.Write(Encoding.UTF8.GetBytes("Content-Length: " + memoryStream2.Length));
			binaryWriter.Write(Request.EOL);
			binaryWriter.Write(Request.EOL);
			memoryStream2.WriteTo(memoryStream);
			memoryStream2.Flush();
		}
		else if (this.Text.Length > 0 || this.bytes.Length > 0)
		{
			binaryWriter.Write(Encoding.UTF8.GetBytes("content-type: application/x-www-form-urlencoded"));
			binaryWriter.Write(Request.EOL);
			if (this.GetHeader("Content-Length") == string.Empty)
			{
				binaryWriter.Write(Encoding.UTF8.GetBytes("content-length: " + this.bytes.Length));
				binaryWriter.Write(Request.EOL);
				binaryWriter.Write(Request.EOL);
			}
			binaryWriter.Write(this.bytes);
		}
		else
		{
			binaryWriter.Write(Request.EOL);
		}
		memoryStream.WriteTo(outputStream);
		memoryStream.Flush();
		binaryWriter.Close();
		memoryStream.Close();
	}

	// Token: 0x0400037B RID: 891
	public string method = "GET";

	// Token: 0x0400037C RID: 892
	public string protocol = "HTTP/1.1";

	// Token: 0x0400037D RID: 893
	public byte[] bytes;

	// Token: 0x0400037E RID: 894
	public Uri uri;

	// Token: 0x0400037F RID: 895
	public static byte[] EOL = new byte[]
	{
		13,
		10
	};

	// Token: 0x04000380 RID: 896
	public Response response;

	// Token: 0x04000381 RID: 897
	public bool isDone;

	// Token: 0x04000382 RID: 898
	public int maximumRetryCount = 2;

	// Token: 0x04000383 RID: 899
	public bool acceptGzip;

	// Token: 0x04000384 RID: 900
	public bool useCache;

	// Token: 0x04000385 RID: 901
	public Exception exception;

	// Token: 0x04000386 RID: 902
	public string Text = string.Empty;

	// Token: 0x04000387 RID: 903
	public bool isFile;

	// Token: 0x04000388 RID: 904
	private Dictionary<string, List<string>> headers = new Dictionary<string, List<string>>();

	// Token: 0x04000389 RID: 905
	private static Dictionary<string, string> etags = new Dictionary<string, string>();

	// Token: 0x0400038A RID: 906
	private string str = string.Empty;

	// Token: 0x0400038B RID: 907
	private TcpClient client;

	// Token: 0x0400038C RID: 908
	private int maxTryCount = 2;
}
