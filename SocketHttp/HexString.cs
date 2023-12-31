using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace SocketHttp
{
	// Token: 0x02000369 RID: 873
	internal class HexString
	{
		// Token: 0x06001C75 RID: 7285 RVA: 0x000FC870 File Offset: 0x000FAA70
		public bool Parse(byte b)
		{
			if (this.Complete)
			{
				return this.Complete;
			}
			if (this.skipFirst)
			{
				if (b == 10 || b == 13)
				{
					return false;
				}
				this.skipFirst = false;
			}
			if (!this._eol.Parse(b))
			{
				this._rawData.WriteByte(b);
			}
			else
			{
				this.Complete = true;
				if (this.Text != string.Empty)
				{
					this.IntValue = int.Parse(this.Text, NumberStyles.AllowHexSpecifier);
				}
			}
			return false;
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06001C76 RID: 7286 RVA: 0x000FC908 File Offset: 0x000FAB08
		public string Text
		{
			get
			{
				return Encoding.UTF8.GetString(this._rawData.ToArray()).Trim(new char[]
				{
					'\r',
					'\n',
					' '
				});
			}
		}

		// Token: 0x0400214C RID: 8524
		public bool Complete;

		// Token: 0x0400214D RID: 8525
		public int IntValue = -1;

		// Token: 0x0400214E RID: 8526
		private MemoryStream _rawData = new MemoryStream();

		// Token: 0x0400214F RID: 8527
		private ParseItem _eol = new ParseItem("\r\n");

		// Token: 0x04002150 RID: 8528
		private bool skipFirst = true;
	}
}
