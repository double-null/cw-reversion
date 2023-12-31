using System;

namespace SocketHttp
{
	// Token: 0x02000366 RID: 870
	internal class ChunkedBody : Body
	{
		// Token: 0x06001C6E RID: 7278 RVA: 0x000FC620 File Offset: 0x000FA820
		public override bool Parse(byte b)
		{
			if (this.chunkedLenght.Parse(b))
			{
				if (this._maxLenght == 0)
				{
					this._increaseLen = true;
				}
				if (this._increaseLen)
				{
					this._maxLenght += this.chunkedLenght.IntValue;
					this.Complete = false;
					this._increaseLen = false;
				}
				if (base.Parse(b))
				{
					this.chunkedLenght = new HexString();
					this._increaseLen = true;
				}
			}
			return this.chunkedLenght.IntValue == 0;
		}

		// Token: 0x04002144 RID: 8516
		private HexString chunkedLenght = new HexString();

		// Token: 0x04002145 RID: 8517
		private bool _increaseLen = true;
	}
}
