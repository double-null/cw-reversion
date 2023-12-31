using System;
using System.Collections.Generic;

// Token: 0x02000250 RID: 592
internal class ClanSystemData
{
	// Token: 0x0600121A RID: 4634 RVA: 0x000C795C File Offset: 0x000C5B5C
	public void ParseClanInfo(Dictionary<string, object>[] dict)
	{
		this.clanShortInfoList = new ShortClanInfo[dict.Length];
		for (int i = 0; i < dict.Length; i++)
		{
			this.clanShortInfoList[i] = new ShortClanInfo(dict[i]);
		}
	}

	// Token: 0x0600121B RID: 4635 RVA: 0x000C799C File Offset: 0x000C5B9C
	public void ParseDetailClanInfo(Dictionary<string, object> dict)
	{
		this.SelectedClanInfo = new DetailClanInfo(dict);
	}

	// Token: 0x0600121C RID: 4636 RVA: 0x000C79AC File Offset: 0x000C5BAC
	public void ParseClanMemberList(Dictionary<string, object>[] dict)
	{
		this.clanMemberList = new ClanMemberInfo[dict.Length];
		for (int i = 0; i < dict.Length; i++)
		{
			this.clanMemberList[i] = new ClanMemberInfo(dict[i]);
		}
	}

	// Token: 0x0600121D RID: 4637 RVA: 0x000C79EC File Offset: 0x000C5BEC
	public void ParseRequestList(Dictionary<string, object>[] dict)
	{
		if (dict.Length == 0)
		{
			return;
		}
		this.clanRequestList = new ClanRequestInfo[dict.Length];
		for (int i = 0; i < dict.Length; i++)
		{
			this.clanRequestList[i] = new ClanRequestInfo(dict[i]);
		}
	}

	// Token: 0x0600121E RID: 4638 RVA: 0x000C7A34 File Offset: 0x000C5C34
	public void ParseClanTransaction(Dictionary<string, object>[] dict)
	{
		if (dict.Length == 0)
		{
			return;
		}
		this.clanTransactionList = new ClanTransactionData[dict.Length];
		for (int i = 0; i < dict.Length; i++)
		{
			this.clanTransactionList[i] = new ClanTransactionData(dict[i]);
		}
	}

	// Token: 0x040011BF RID: 4543
	public int ongoing_race;

	// Token: 0x040011C0 RID: 4544
	public string race_end = string.Empty;

	// Token: 0x040011C1 RID: 4545
	public ShortClanInfo[] clanShortInfoList = new ShortClanInfo[0];

	// Token: 0x040011C2 RID: 4546
	public DetailClanInfo SelectedClanInfo = new DetailClanInfo();

	// Token: 0x040011C3 RID: 4547
	public ClanMemberInfo[] clanMemberList = new ClanMemberInfo[0];

	// Token: 0x040011C4 RID: 4548
	public ClanRequestInfo[] clanRequestList = new ClanRequestInfo[0];

	// Token: 0x040011C5 RID: 4549
	public ClanTransactionData[] clanTransactionList = new ClanTransactionData[0];
}
