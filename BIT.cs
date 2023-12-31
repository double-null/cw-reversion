using System;
using System.Collections.Generic;

// Token: 0x02000376 RID: 886
internal class BIT
{
	// Token: 0x06001CC9 RID: 7369 RVA: 0x000FF0AC File Offset: 0x000FD2AC
	public static List<int> INDEXES(int f)
	{
		List<int> list = new List<int>();
		int num = 0;
		do
		{
			if (f % 2 == 1)
			{
				list.Add(num);
			}
			f /= 2;
			num++;
		}
		while (f != 0);
		return list;
	}

	// Token: 0x06001CCA RID: 7370 RVA: 0x000FF0E4 File Offset: 0x000FD2E4
	public static bool AND(short f, short s)
	{
		return (f & s) != 0;
	}

	// Token: 0x06001CCB RID: 7371 RVA: 0x000FF0F0 File Offset: 0x000FD2F0
	public static bool AND(byte f, byte s)
	{
		return (f & s) != 0;
	}

	// Token: 0x06001CCC RID: 7372 RVA: 0x000FF0FC File Offset: 0x000FD2FC
	public static bool AND(int f, int s)
	{
		return (f & s) != 0;
	}

	// Token: 0x06001CCD RID: 7373 RVA: 0x000FF108 File Offset: 0x000FD308
	public static bool AND(ulong f, ulong s)
	{
		return (f & s) != 0UL;
	}
}
