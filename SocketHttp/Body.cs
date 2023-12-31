using System;
using System.IO;
using System.Text;

namespace SocketHttp
{
	// Token: 0x02000365 RID: 869
	internal class Body
	{
		// Token: 0x06001C66 RID: 7270 RVA: 0x000FC4D4 File Offset: 0x000FA6D4
		public Body(int bytes)
		{
			this._maxLenght = bytes;
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x000FC4F0 File Offset: 0x000FA6F0
		public Body()
		{
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x000FC504 File Offset: 0x000FA704
		public virtual bool Parse(byte b)
		{
			if (this.Complete)
			{
				return this.Complete;
			}
			this._bytes.WriteByte(b);
			this._index++;
			if (this._index == this._maxLenght)
			{
				this.Complete = true;
				return true;
			}
			return false;
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x000FC558 File Offset: 0x000FA758
		public bool Parse(byte[] bytes)
		{
			if (this.Complete)
			{
				return this.Complete;
			}
			if ((long)this._index + this._bytes.Length < (long)this._maxLenght)
			{
				this._bytes.Write(bytes, 0, bytes.Length);
			}
			else
			{
				this._bytes.Write(bytes, 0, this._maxLenght - this._index);
				this.Complete = true;
			}
			return this.Complete;
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06001C6A RID: 7274 RVA: 0x000FC5D4 File Offset: 0x000FA7D4
		public virtual string Text
		{
			get
			{
				return Encoding.UTF8.GetString(this._bytes.ToArray());
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06001C6B RID: 7275 RVA: 0x000FC5EC File Offset: 0x000FA7EC
		public byte[] Data
		{
			get
			{
				return this._bytes.ToArray();
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06001C6C RID: 7276 RVA: 0x000FC5FC File Offset: 0x000FA7FC
		public int Lenght
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x04002140 RID: 8512
		protected MemoryStream _bytes = new MemoryStream();

		// Token: 0x04002141 RID: 8513
		public int _index;

		// Token: 0x04002142 RID: 8514
		public bool Complete;

		// Token: 0x04002143 RID: 8515
		public int _maxLenght;
	}
}
