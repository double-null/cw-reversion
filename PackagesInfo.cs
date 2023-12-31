using System;
using System.Collections.Generic;

// Token: 0x020002A4 RID: 676
internal class PackagesInfo
{
	// Token: 0x060012FF RID: 4863 RVA: 0x000CDD9C File Offset: 0x000CBF9C
	public PackagesInfo()
	{
	}

	// Token: 0x06001300 RID: 4864 RVA: 0x000CDDB0 File Offset: 0x000CBFB0
	public PackagesInfo(Dictionary<string, object>[] dict, int setIndex)
	{
		this.package.Clear();
		for (int i = 0; i < dict.Length; i++)
		{
			if (setIndex == (int)dict[i]["set"])
			{
				this.package.Add(new Packages(dict[i], i));
			}
		}
	}

	// Token: 0x06001301 RID: 4865 RVA: 0x000CDE1C File Offset: 0x000CC01C
	public static int GetSize(Dictionary<string, object>[] dict)
	{
		List<int> list = new List<int>();
		for (int i = 0; i < dict.Length; i++)
		{
			if (!list.Contains((int)dict[i]["set"]))
			{
				list.Add((int)dict[i]["set"]);
			}
		}
		return list.Count;
	}

	// Token: 0x06001302 RID: 4866 RVA: 0x000CDE80 File Offset: 0x000CC080
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
	}

	// Token: 0x040015FC RID: 5628
	public List<Packages> package = new List<Packages>();
}
