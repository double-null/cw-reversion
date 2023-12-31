using System;
using System.Text;
using cygwin_x32.ObscuredTypes;
using UnityEngine;

// Token: 0x020000A1 RID: 161
internal class eNetworkStream
{
	// Token: 0x0600038F RID: 911 RVA: 0x000199E8 File Offset: 0x00017BE8
	public eNetworkStream(BitStream bitStream, double timestamp)
	{
		this.bitStream = bitStream;
		if (this.isReading)
		{
			this.halfPing = (float)(Network.time - timestamp);
		}
		this.Serialize<UpdateType>(ref this.update);
	}

	// Token: 0x06000390 RID: 912 RVA: 0x00019A3C File Offset: 0x00017C3C
	public eNetworkStream(UpdateType update, BitStream bitStream, double timestamp)
	{
		this.update = update;
		this.bitStream = bitStream;
		if (this.isReading)
		{
			this.halfPing = (float)(Network.time - timestamp);
		}
		this.Serialize<UpdateType>(ref this.update);
	}

	// Token: 0x06000391 RID: 913 RVA: 0x00019A94 File Offset: 0x00017C94
	public eNetworkStream(ListStream listStream)
	{
		this.listStream = listStream;
		if (this.isReading)
		{
			this.halfPing = (float)(Network.time - listStream.timestamp);
		}
		this.Serialize<UpdateType>(ref this.update);
	}

	// Token: 0x06000392 RID: 914 RVA: 0x00019AEC File Offset: 0x00017CEC
	public eNetworkStream(UpdateType update, ListStream listStream)
	{
		this.update = update;
		this.listStream = listStream;
		if (this.isReading)
		{
			this.halfPing = (float)(Network.time - listStream.timestamp);
		}
		this.Serialize<UpdateType>(ref this.update);
	}

	// Token: 0x1700006A RID: 106
	// (get) Token: 0x06000393 RID: 915 RVA: 0x00019B4C File Offset: 0x00017D4C
	public bool isReading
	{
		get
		{
			if (this.bitStream != null)
			{
				return this.bitStream.isReading;
			}
			return this.listStream.isReading;
		}
	}

	// Token: 0x1700006B RID: 107
	// (get) Token: 0x06000394 RID: 916 RVA: 0x00019B7C File Offset: 0x00017D7C
	public bool isWriting
	{
		get
		{
			if (this.bitStream != null)
			{
				return this.bitStream.isWriting;
			}
			return this.listStream.isWriting;
		}
	}

	// Token: 0x1700006C RID: 108
	// (get) Token: 0x06000395 RID: 917 RVA: 0x00019BAC File Offset: 0x00017DAC
	public bool isFullUpdate
	{
		get
		{
			return BIT.AND((int)this.update, 2);
		}
	}

	// Token: 0x1700006D RID: 109
	// (get) Token: 0x06000396 RID: 918 RVA: 0x00019BBC File Offset: 0x00017DBC
	public bool isNoUpdate
	{
		get
		{
			return BIT.AND((int)this.update, 1);
		}
	}

	// Token: 0x06000397 RID: 919 RVA: 0x00019BCC File Offset: 0x00017DCC
	public void Serialize<T>(ref T v) where T : struct, IConvertible
	{
		if (this.bitStream != null)
		{
			if (this.isWriting)
			{
				int num = (int)((object)v);
				this.bitStream.Serialize(ref num);
			}
			else
			{
				int num2 = 0;
				this.bitStream.Serialize(ref num2);
				v = (T)((object)num2);
			}
		}
		else
		{
			this.listStream.Serialize<T>(ref v);
		}
	}

	// Token: 0x06000398 RID: 920 RVA: 0x00019C44 File Offset: 0x00017E44
	public void Serialize<T>(ref T[] v) where T : class, cwNetworkSerializable, new()
	{
		if (this.isWriting)
		{
			short num = (short)v.Length;
			this.Serialize(this.update, ref num);
			for (int i = 0; i < (int)num; i++)
			{
				v[i].Serialize(this);
			}
		}
		else
		{
			short num2 = 0;
			this.Serialize(this.update, ref num2);
			if (v == null || v.Length != (int)num2)
			{
				v = new T[(int)num2];
			}
			for (int j = 0; j < (int)num2; j++)
			{
				if (v[j] == null)
				{
					v[j] = Activator.CreateInstance<T>();
				}
				v[j].Deserialize(this);
			}
		}
	}

	// Token: 0x06000399 RID: 921 RVA: 0x00019D0C File Offset: 0x00017F0C
	public void Serialize<T>(ref ClassArray<T> v) where T : class, ReusableClass<T>, cwNetworkSerializable, new()
	{
		if (this.isWriting)
		{
			for (int i = 0; i < v.Length; i++)
			{
				T t = v[i];
				t.Serialize(this);
			}
		}
		else
		{
			for (int j = 0; j < v.Length; j++)
			{
				T t2 = v[j];
				t2.Deserialize(this);
			}
		}
	}

	// Token: 0x0600039A RID: 922 RVA: 0x00019D88 File Offset: 0x00017F88
	public void Serialize(ref bool v)
	{
		if (this.bitStream != null)
		{
			this.bitStream.Serialize(ref v);
		}
		else
		{
			this.listStream.Serialize(ref v);
		}
	}

	// Token: 0x0600039B RID: 923 RVA: 0x00019DC0 File Offset: 0x00017FC0
	public void Serialize(ref char v)
	{
		if (this.bitStream != null)
		{
			this.bitStream.Serialize(ref v);
		}
		else
		{
			this.listStream.Serialize(ref v);
		}
	}

	// Token: 0x0600039C RID: 924 RVA: 0x00019DF8 File Offset: 0x00017FF8
	public void Serialize(ref float v)
	{
		if (this.bitStream != null)
		{
			this.bitStream.Serialize(ref v);
		}
		else
		{
			this.listStream.Serialize(ref v);
		}
	}

	// Token: 0x0600039D RID: 925 RVA: 0x00019E30 File Offset: 0x00018030
	public void Serialize(ref int v)
	{
		if (this.bitStream != null)
		{
			this.bitStream.Serialize(ref v);
		}
		else
		{
			this.listStream.Serialize(ref v);
		}
	}

	// Token: 0x0600039E RID: 926 RVA: 0x00019E68 File Offset: 0x00018068
	public void Serialize(ref ObscuredInt v)
	{
		if (this.bitStream != null)
		{
			if (this.isWriting)
			{
				int num = v;
				this.bitStream.Serialize(ref num);
			}
			else
			{
				int value = 0;
				this.bitStream.Serialize(ref value);
				v = value;
			}
		}
		else
		{
			this.listStream.Serialize(ref v);
		}
	}

	// Token: 0x0600039F RID: 927 RVA: 0x00019ED8 File Offset: 0x000180D8
	public void Serialize(ref Quaternion v)
	{
		if (this.bitStream != null)
		{
			this.bitStream.Serialize(ref v);
		}
		else
		{
			this.listStream.Serialize(ref v);
		}
	}

	// Token: 0x060003A0 RID: 928 RVA: 0x00019F10 File Offset: 0x00018110
	public void Serialize(ref short v)
	{
		if (this.bitStream != null)
		{
			this.bitStream.Serialize(ref v);
		}
		else
		{
			this.listStream.Serialize(ref v);
		}
	}

	// Token: 0x060003A1 RID: 929 RVA: 0x00019F48 File Offset: 0x00018148
	public void Serialize(ref Vector3 v)
	{
		if (this.bitStream != null)
		{
			this.bitStream.Serialize(ref v);
		}
		else
		{
			this.listStream.Serialize(ref v);
		}
	}

	// Token: 0x060003A2 RID: 930 RVA: 0x00019F80 File Offset: 0x00018180
	public void Serialize(ref string v)
	{
		if (this.isWriting)
		{
			byte[] bytes = Encoding.Unicode.GetBytes(v);
			int num = bytes.Length;
			this.Serialize(ref num);
			for (int i = 0; i < num; i++)
			{
				char c = (char)bytes[i];
				this.Serialize(ref c);
			}
		}
		else
		{
			int num2 = 0;
			this.Serialize(ref num2);
			byte[] array = new byte[num2];
			for (int j = 0; j < num2; j++)
			{
				char c2 = ' ';
				this.Serialize(ref c2);
				array[j] = (byte)c2;
			}
			v = WWW.UnEscapeURL(Encoding.Unicode.GetString(array));
		}
	}

	// Token: 0x060003A3 RID: 931 RVA: 0x0001A028 File Offset: 0x00018228
	public void Serialize<T>(UpdateType update, ref T v) where T : struct, IConvertible
	{
		if (!BIT.AND((int)this.update, (int)update))
		{
			return;
		}
		if (this.bitStream != null)
		{
			if (this.isWriting)
			{
				int num = (int)((object)v);
				this.bitStream.Serialize(ref num);
			}
			else
			{
				int num2 = 0;
				this.bitStream.Serialize(ref num2);
				v = (T)((object)num2);
			}
		}
		else
		{
			this.listStream.Serialize<T>(ref v);
		}
	}

	// Token: 0x060003A4 RID: 932 RVA: 0x0001A0B4 File Offset: 0x000182B4
	public void Serialize(UpdateType update, ref ObscuredInt v)
	{
		if (!BIT.AND((int)this.update, (int)update))
		{
			return;
		}
		if (this.bitStream != null)
		{
			if (this.isWriting)
			{
				int num = v;
				this.bitStream.Serialize(ref num);
			}
			else
			{
				int value = 0;
				this.bitStream.Serialize(ref value);
				v = value;
			}
		}
		else
		{
			this.listStream.Serialize(ref v);
		}
	}

	// Token: 0x060003A5 RID: 933 RVA: 0x0001A134 File Offset: 0x00018334
	public void Serialize<T>(UpdateType update, ref T[] v) where T : class, cwNetworkSerializable, new()
	{
		if (!BIT.AND((int)this.update, (int)update))
		{
			return;
		}
		if (this.isWriting)
		{
			int num = v.Length;
			this.Serialize(update, ref num);
			for (int i = 0; i < num; i++)
			{
				v[i].Serialize(this);
			}
		}
		else
		{
			int num2 = 0;
			this.Serialize(update, ref num2);
			if (v == null || v.Length != num2)
			{
				v = new T[num2];
			}
			for (int j = 0; j < num2; j++)
			{
				if (v[j] == null)
				{
					v[j] = Activator.CreateInstance<T>();
				}
				v[j].Deserialize(this);
			}
		}
	}

	// Token: 0x060003A6 RID: 934 RVA: 0x0001A200 File Offset: 0x00018400
	public void Serialize<T>(UpdateType update, ref ClassArray<T> v) where T : class, ReusableClass<T>, cwNetworkSerializable, new()
	{
		if (!BIT.AND((int)this.update, (int)update))
		{
			return;
		}
		if (this.isWriting)
		{
			int length = v.Length;
			this.Serialize(update, ref length);
			for (int i = 0; i < length; i++)
			{
				T t = v[i];
				t.Serialize(this);
			}
		}
		else
		{
			int num = 0;
			this.Serialize(update, ref num);
			if (v == null || v.Length != num)
			{
				v = new ClassArray<T>(num);
			}
			for (int j = 0; j < num; j++)
			{
				if (v[j] == null)
				{
					v[j] = Activator.CreateInstance<T>();
				}
				T t2 = v[j];
				t2.Deserialize(this);
			}
		}
	}

	// Token: 0x060003A7 RID: 935 RVA: 0x0001A2DC File Offset: 0x000184DC
	public void Serialize(UpdateType update, ref bool[] v)
	{
		if (!BIT.AND((int)this.update, (int)update))
		{
			return;
		}
		if (this.isWriting)
		{
			int num = v.Length;
			this.Serialize(update, ref num);
			for (int i = 0; i < num; i++)
			{
				this.Serialize(ref v[i]);
			}
		}
		else
		{
			int num2 = 0;
			this.Serialize(update, ref num2);
			v = new bool[num2];
			for (int j = 0; j < num2; j++)
			{
				this.Serialize(ref v[j]);
			}
		}
	}

	// Token: 0x060003A8 RID: 936 RVA: 0x0001A36C File Offset: 0x0001856C
	public void Serialize(UpdateType update, ref bool v)
	{
		if (!BIT.AND((int)this.update, (int)update))
		{
			return;
		}
		if (this.bitStream != null)
		{
			this.bitStream.Serialize(ref v);
		}
		else
		{
			this.listStream.Serialize(ref v);
		}
	}

	// Token: 0x060003A9 RID: 937 RVA: 0x0001A3B4 File Offset: 0x000185B4
	public void Serialize(UpdateType update, ref char v)
	{
		if (!BIT.AND((int)this.update, (int)update))
		{
			return;
		}
		if (this.bitStream != null)
		{
			this.bitStream.Serialize(ref v);
		}
		else
		{
			this.listStream.Serialize(ref v);
		}
	}

	// Token: 0x060003AA RID: 938 RVA: 0x0001A3FC File Offset: 0x000185FC
	public void Serialize(UpdateType update, ref float v)
	{
		if (!BIT.AND((int)this.update, (int)update))
		{
			return;
		}
		if (this.bitStream != null)
		{
			this.bitStream.Serialize(ref v);
		}
		else
		{
			this.listStream.Serialize(ref v);
		}
	}

	// Token: 0x060003AB RID: 939 RVA: 0x0001A444 File Offset: 0x00018644
	public void Serialize(UpdateType update, ref int v)
	{
		if (!BIT.AND((int)this.update, (int)update))
		{
			return;
		}
		if (this.bitStream != null)
		{
			this.bitStream.Serialize(ref v);
		}
		else
		{
			this.listStream.Serialize(ref v);
		}
	}

	// Token: 0x060003AC RID: 940 RVA: 0x0001A48C File Offset: 0x0001868C
	public void Serialize(UpdateType update, ref Quaternion v)
	{
		if (!BIT.AND((int)this.update, (int)update))
		{
			return;
		}
		if (this.bitStream != null)
		{
			this.bitStream.Serialize(ref v);
		}
		else
		{
			this.listStream.Serialize(ref v);
		}
	}

	// Token: 0x060003AD RID: 941 RVA: 0x0001A4D4 File Offset: 0x000186D4
	public void Serialize(UpdateType update, ref short v)
	{
		if (!BIT.AND((int)this.update, (int)update))
		{
			return;
		}
		if (this.bitStream != null)
		{
			this.bitStream.Serialize(ref v);
		}
		else
		{
			this.listStream.Serialize(ref v);
		}
	}

	// Token: 0x060003AE RID: 942 RVA: 0x0001A51C File Offset: 0x0001871C
	public void Serialize(UpdateType update, ref Vector3 v)
	{
		if (!BIT.AND((int)this.update, (int)update))
		{
			return;
		}
		if (this.bitStream != null)
		{
			this.bitStream.Serialize(ref v);
		}
		else
		{
			this.listStream.Serialize(ref v);
		}
	}

	// Token: 0x060003AF RID: 943 RVA: 0x0001A564 File Offset: 0x00018764
	public void Serialize(UpdateType update, ref string v)
	{
		if (!BIT.AND((int)this.update, (int)update))
		{
			return;
		}
		if (this.bitStream != null)
		{
			this.Serialize(ref v);
		}
		else
		{
			this.Serialize(ref v);
		}
	}

	// Token: 0x040003A0 RID: 928
	private BitStream bitStream;

	// Token: 0x040003A1 RID: 929
	private ListStream listStream;

	// Token: 0x040003A2 RID: 930
	public float halfPing;

	// Token: 0x040003A3 RID: 931
	public int ID = IDUtil.NoID;

	// Token: 0x040003A4 RID: 932
	private UpdateType update = UpdateType.NoUpdate;
}
