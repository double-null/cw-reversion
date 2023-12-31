using System;
using System.Text;

namespace SocketHttp
{
	// Token: 0x0200036A RID: 874
	internal class ParseItem
	{
		// Token: 0x06001C77 RID: 7287 RVA: 0x000FC944 File Offset: 0x000FAB44
		public ParseItem(byte[] pattern)
		{
			this._patternBytes = pattern;
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x000FC954 File Offset: 0x000FAB54
		public ParseItem(string pattern) : this(Encoding.UTF8.GetBytes(pattern))
		{
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x000FC968 File Offset: 0x000FAB68
		public bool Parse(byte b)
		{
			if (this.Complete)
			{
				return this.Complete;
			}
			if (this._patternBytes[this._index] == b)
			{
				this._index++;
				if (this._index >= this._patternBytes.Length)
				{
					this.Complete = true;
					return this.Complete;
				}
			}
			else
			{
				this._index = 0;
				if (this._patternBytes[this._index] == b)
				{
					this._index++;
					if (this._index >= this._patternBytes.Length)
					{
						this.Complete = true;
						return this.Complete;
					}
				}
			}
			return false;
		}

		// Token: 0x04002151 RID: 8529
		private byte[] _patternBytes;

		// Token: 0x04002152 RID: 8530
		private int _index;

		// Token: 0x04002153 RID: 8531
		public bool Complete;
	}
}
