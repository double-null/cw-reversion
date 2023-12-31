using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using UnityEngine;

namespace SocketHttp
{
	// Token: 0x0200036B RID: 875
	internal class Request
	{
		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06001C7C RID: 7292 RVA: 0x000FCAE8 File Offset: 0x000FACE8
		public string DebugResponce
		{
			get
			{
				return Encoding.UTF8.GetString(this._debugStream.ToArray());
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06001C7D RID: 7293 RVA: 0x000FCB00 File Offset: 0x000FAD00
		public Body Responce
		{
			get
			{
				return this.Body;
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06001C7E RID: 7294 RVA: 0x000FCB08 File Offset: 0x000FAD08
		public bool Done
		{
			get
			{
				return this._doneRequest;
			}
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x000FCB10 File Offset: 0x000FAD10
		public void Post(string url, string text)
		{
			this.Post(url, Encoding.UTF8.GetBytes(text));
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x000FCB24 File Offset: 0x000FAD24
		public void Post(string url, byte[] data)
		{
			this.Raw("POST", url, data, false);
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x000FCB34 File Offset: 0x000FAD34
		public void Get(string url)
		{
			this.Raw("GET", url, null, false);
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x000FCB44 File Offset: 0x000FAD44
		public void PostFile(string url, byte[] data)
		{
			this.Raw("POST", url, data, true);
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x000FCB54 File Offset: 0x000FAD54
		private void OnConnected(IAsyncResult ar)
		{
			try
			{
				TcpClient tcpClient = (TcpClient)ar.AsyncState;
				tcpClient.EndConnect(ar);
			}
			catch (Exception ex)
			{
				this.Body = new ErrorBody(ex.ToString());
				this._doneRequest = true;
				Debug.LogError(ex);
			}
			this.SendRequest(this);
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x000FCBC0 File Offset: 0x000FADC0
		private void Raw(string method, string url, byte[] data, bool file = false)
		{
			this._data = data;
			this._method = method;
			this._isFile = file;
			this.uri = new Uri(url);
			this.client = new TcpClient();
			this.client.SendTimeout = 30000;
			this.client.LingerState.LingerTime = 30000;
			this.client.ReceiveTimeout = 30000;
			try
			{
				this.client.BeginConnect(this.uri.Host, this.uri.Port, new AsyncCallback(this.OnConnected), this.client);
			}
			catch (Exception ex)
			{
				Debug.LogError(ex);
				this.Body = new ErrorBody(ex.ToString());
				this._doneRequest = true;
				this.client.Close();
			}
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x000FCCB4 File Offset: 0x000FAEB4
		private void SendRequest(object state)
		{
			string text = string.Empty;
			MemoryStream memoryStream = new MemoryStream();
			Stream stream;
			if (this.uri.Scheme.ToLower() == "https")
			{
				SslStream sslStream = new SslStream(this.client.GetStream(), false, new RemoteCertificateValidationCallback(this.ValidateServerCertificate));
				sslStream.AuthenticateAsClient(this.uri.Host);
				stream = sslStream;
			}
			else
			{
				stream = this.client.GetStream();
			}
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				this._method,
				" ",
				this.uri.PathAndQuery,
				" HTTP/1.1\r\n"
			});
			text = text + "Host: " + this.uri.Host + "\r\n";
			if (!string.IsNullOrEmpty(Request._cookieString))
			{
				text = text + "Cookie: " + Request._cookieString + "\r\n";
			}
			byte[] bytes = Encoding.UTF8.GetBytes(text);
			memoryStream.Write(bytes, 0, bytes.Length);
			if (this._data != null)
			{
				if (this._isFile)
				{
					this.SendFile(memoryStream, this._data);
				}
				else
				{
					this.SendData(memoryStream, this._data);
				}
			}
			else
			{
				memoryStream.Write(Request.EOL, 0, Request.EOL.Length);
			}
			memoryStream.WriteTo(stream);
			stream.BeginRead(this.bytesBuffer, 0, this.bytesBuffer.Length, new AsyncCallback(this.ReadCallback), stream);
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06001C86 RID: 7302 RVA: 0x000FCE3C File Offset: 0x000FB03C
		public string RawHeaders
		{
			get
			{
				return Encoding.UTF8.GetString(this._rawHeaders.ToArray());
			}
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x000FCE54 File Offset: 0x000FB054
		private void ReadCallback(IAsyncResult ar)
		{
			Stream stream = (Stream)ar.AsyncState;
			stream.EndRead(ar);
			this._debugStream.Write(this.bytesBuffer, 0, this.bytesBuffer.Length);
			for (int i = 0; i < this.bytesBuffer.Length; i++)
			{
				if (this.EndOfHeader.Parse(this.bytesBuffer[i]))
				{
					if (this.ContentLenght.Complete)
					{
						if (this.Body == null)
						{
							int bytes = 0;
							int.TryParse(this.ContentLenght.HeaderValue, out bytes);
							this.Body = new Body(bytes);
							goto IL_22D;
						}
						if (this.Body.Parse(this.bytesBuffer[i]))
						{
							if (Request.DebugLog)
							{
								Debug.Log(string.Concat(new object[]
								{
									"<--Done: ",
									this.Body.Lenght,
									" \n",
									this.DebugResponce
								}));
							}
							this.SaveCookie(this.SetCookie.HeaderValue);
							this.client.Close();
							this._doneRequest = true;
							return;
						}
					}
					if (this.HeaderTransferEncoding.Complete && this.HeaderTransferEncoding.EqualValue)
					{
						if (this.Body == null)
						{
							int intValue = this.HexString.IntValue;
							this.Body = new ChunkedBody();
						}
						else if (this.Body.Parse(this.bytesBuffer[i]))
						{
							if (Request.DebugLog)
							{
								Debug.Log(string.Concat(new object[]
								{
									"<--Done: ",
									this.Body.Lenght,
									" \n",
									this.DebugResponce
								}));
							}
							this.SaveCookie(this.SetCookie.HeaderValue);
							this.client.Close();
							this._doneRequest = true;
							return;
						}
					}
				}
				else
				{
					this._rawHeaders.WriteByte(this.bytesBuffer[i]);
					this.SetCookie.Parse(this.bytesBuffer[i]);
					this.ContentLenght.Parse(this.bytesBuffer[i]);
					this.HeaderTransferEncoding.Parse(this.bytesBuffer[i]);
				}
				IL_22D:;
			}
			this.bytesBuffer = new byte[1];
			stream.BeginRead(this.bytesBuffer, 0, this.bytesBuffer.Length, new AsyncCallback(this.ReadCallback), stream);
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x000FD0D0 File Offset: 0x000FB2D0
		private bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x000FD0D4 File Offset: 0x000FB2D4
		private void SaveCookie(string cookie)
		{
			if (!string.IsNullOrEmpty(cookie))
			{
				string[] array = cookie.Split(new char[]
				{
					';'
				});
				if (array != null && array.Length > 0 && Request._cookieString != array[0])
				{
					Request._cookieString = array[0].Trim();
				}
			}
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x000FD12C File Offset: 0x000FB32C
		private void SendFile(Stream sendStream, byte[] bytes)
		{
			BinaryWriter binaryWriter = new BinaryWriter(sendStream);
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter2 = new BinaryWriter(memoryStream);
			binaryWriter2.Write(Encoding.UTF8.GetBytes("--ffeUUzIN7c5D11L9OnBOzpTStFF5YQasiMeyBBgp"));
			binaryWriter2.Write(Request.EOL);
			binaryWriter2.Write(Encoding.UTF8.GetBytes("Content-disposition: form-data; name=\"file1\"; filename=\"screenshot.png\""));
			binaryWriter2.Write(Request.EOL);
			binaryWriter2.Write(Encoding.UTF8.GetBytes("Content-Type: image/png"));
			binaryWriter2.Write(Request.EOL);
			binaryWriter2.Write(Encoding.UTF8.GetBytes("Content-Length: " + bytes.Length));
			binaryWriter2.Write(Request.EOL);
			binaryWriter2.Write(Request.EOL);
			binaryWriter2.Write(bytes);
			binaryWriter2.Write(Request.EOL);
			binaryWriter2.Write(Encoding.UTF8.GetBytes("--ffeUUzIN7c5D11L9OnBOzpTStFF5YQasiMeyBBgp--"));
			binaryWriter.Write(Encoding.UTF8.GetBytes("Content-Type: multipart/form-data; boundary=\"ffeUUzIN7c5D11L9OnBOzpTStFF5YQasiMeyBBgp\""));
			binaryWriter.Write(Request.EOL);
			binaryWriter.Write(Encoding.UTF8.GetBytes("Content-Length: " + memoryStream.Length));
			binaryWriter.Write(Request.EOL);
			binaryWriter.Write(Request.EOL);
			memoryStream.WriteTo(sendStream);
			memoryStream.Flush();
			binaryWriter2.Close();
			memoryStream.Close();
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x000FD280 File Offset: 0x000FB480
		private void SendData(Stream sendStream, byte[] bytes)
		{
			BinaryWriter binaryWriter = new BinaryWriter(sendStream);
			if (bytes.Length > 0)
			{
				binaryWriter.Write(Encoding.UTF8.GetBytes("content-type: application/x-www-form-urlencoded"));
				binaryWriter.Write(Request.EOL);
				binaryWriter.Write(Encoding.UTF8.GetBytes("content-length: " + bytes.Length));
				binaryWriter.Write(Request.EOL);
				binaryWriter.Write(Request.EOL);
				binaryWriter.Write(bytes);
			}
			else
			{
				binaryWriter.Write(Request.EOL);
			}
		}

		// Token: 0x04002154 RID: 8532
		public static bool DebugLog = false;

		// Token: 0x04002155 RID: 8533
		private static string _cookieString = string.Empty;

		// Token: 0x04002156 RID: 8534
		private static byte[] EOL = new byte[]
		{
			13,
			10
		};

		// Token: 0x04002157 RID: 8535
		private MemoryStream _rawHeaders = new MemoryStream();

		// Token: 0x04002158 RID: 8536
		private byte[] bytesBuffer = new byte[1024];

		// Token: 0x04002159 RID: 8537
		private Header ContentLenght = new Header("Content-Length");

		// Token: 0x0400215A RID: 8538
		private Header HeaderTransferEncoding = new Header("Transfer-Encoding", "chunked");

		// Token: 0x0400215B RID: 8539
		private Header SetCookie = new Header("Set-Cookie");

		// Token: 0x0400215C RID: 8540
		private TcpClient client;

		// Token: 0x0400215D RID: 8541
		private Body Body;

		// Token: 0x0400215E RID: 8542
		private HexString HexString = new HexString();

		// Token: 0x0400215F RID: 8543
		private bool _doneRequest;

		// Token: 0x04002160 RID: 8544
		private ParseItem EndOfHeader = new ParseItem("\r\n\r\n");

		// Token: 0x04002161 RID: 8545
		private Uri uri;

		// Token: 0x04002162 RID: 8546
		private string _method = string.Empty;

		// Token: 0x04002163 RID: 8547
		private bool _isFile;

		// Token: 0x04002164 RID: 8548
		private byte[] _data = new byte[0];

		// Token: 0x04002165 RID: 8549
		private MemoryStream _debugStream = new MemoryStream();

		// Token: 0x04002166 RID: 8550
		private int _connectTry = 10;
	}
}
