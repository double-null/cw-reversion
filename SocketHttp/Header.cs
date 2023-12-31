using System;
using System.IO;
using System.Text;

namespace SocketHttp
{
	// Token: 0x02000368 RID: 872
	internal class Header
	{
		// Token: 0x06001C70 RID: 7280 RVA: 0x000FC6E0 File Offset: 0x000FA8E0
		public Header(string headerName)
		{
			this._headerName = new ParseItem(headerName + ": ");
			this._eol = new ParseItem("\r\n");
			this._headerValue = new MemoryStream();
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x000FC730 File Offset: 0x000FA930
		public Header(string headerName, string headerValue)
		{
			this._headerName = new ParseItem(headerName + ": ");
			this._eol = new ParseItem("\r\n");
			this._headerValue = new MemoryStream();
			this._comparableValue = headerValue;
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x000FC788 File Offset: 0x000FA988
		public bool Parse(byte b)
		{
			if (this.Complete)
			{
				return this.Complete;
			}
			if (this._headerName.Parse(b))
			{
				if (!this._eol.Parse(b))
				{
					this._headerValue.WriteByte(b);
				}
				else
				{
					if (!string.IsNullOrEmpty(this._comparableValue) && this._comparableValue.ToLower() == this.HeaderValue.ToLower())
					{
						this.EqualValue = true;
					}
					this.Complete = true;
				}
			}
			return this.Complete;
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06001C73 RID: 7283 RVA: 0x000FC820 File Offset: 0x000FAA20
		public string HeaderValue
		{
			get
			{
				return Encoding.UTF8.GetString(this._headerValue.ToArray()).Trim();
			}
		}

		// Token: 0x04002146 RID: 8518
		private ParseItem _headerName;

		// Token: 0x04002147 RID: 8519
		private ParseItem _eol;

		// Token: 0x04002148 RID: 8520
		private MemoryStream _headerValue;

		// Token: 0x04002149 RID: 8521
		public bool EqualValue;

		// Token: 0x0400214A RID: 8522
		private string _comparableValue = string.Empty;

		// Token: 0x0400214B RID: 8523
		public bool Complete;
	}
}
