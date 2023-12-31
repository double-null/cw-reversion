using System;

namespace DataTypes
{
	// Token: 0x02000377 RID: 887
	internal struct BoolArray : cwNetworkSerializable
	{
		// Token: 0x06001CCE RID: 7374 RVA: 0x000FF114 File Offset: 0x000FD314
		public BoolArray(int size)
		{
			this._size = size;
			this._intCapacity = 32;
			int num = size / this._intCapacity;
			if (size % this._intCapacity != 0)
			{
				num++;
			}
			this.mass = new int[num];
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06001CCF RID: 7375 RVA: 0x000FF158 File Offset: 0x000FD358
		public int Lenght
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x17000835 RID: 2101
		public bool this[int i]
		{
			get
			{
				return (this.mass[i / this._intCapacity] & 1 << i) != 0;
			}
			set
			{
				if (value)
				{
					this.mass[i / this._intCapacity] |= 1 << i;
				}
				else
				{
					this.mass[i / this._intCapacity] ^= 1 << i;
				}
			}
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x000FF1D8 File Offset: 0x000FD3D8
		public void Serialize(eNetworkStream stream)
		{
			stream.Serialize(ref this._intCapacity);
			for (int i = 0; i < this._intCapacity; i++)
			{
				stream.Serialize(ref this.mass[i]);
			}
		}

		// Token: 0x06001CD3 RID: 7379 RVA: 0x000FF21C File Offset: 0x000FD41C
		public void Deserialize(eNetworkStream stream)
		{
			stream.Serialize(ref this._intCapacity);
			for (int i = 0; i < this._intCapacity; i++)
			{
				stream.Serialize(ref this.mass[i]);
			}
		}

		// Token: 0x04002193 RID: 8595
		private int _size;

		// Token: 0x04002194 RID: 8596
		private int[] mass;

		// Token: 0x04002195 RID: 8597
		private int _intCapacity;
	}
}
