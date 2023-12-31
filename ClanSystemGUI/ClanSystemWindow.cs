using System;
using UnityEngine;

namespace ClanSystemGUI
{
	// Token: 0x020000F9 RID: 249
	[Serializable]
	internal class ClanSystemWindow
	{
		// Token: 0x060006B8 RID: 1720 RVA: 0x0003CB18 File Offset: 0x0003AD18
		public ClanSystemWindow()
		{
			ClanSystemWindow.I = this;
			this.controller = new ClanTabController();
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0003CB78 File Offset: 0x0003AD78
		public void Start()
		{
			this.controller.OnStart();
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0003CB88 File Offset: 0x0003AD88
		public void Update()
		{
			this.controller.OnUpdate();
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0003CB98 File Offset: 0x0003AD98
		public void OnGUI()
		{
			if (CarrierGUI.I.IsFocused || Event.current.type == EventType.Repaint)
			{
				this.controller.OnGUI();
			}
		}

		// Token: 0x04000744 RID: 1860
		public ClanSystemWindow.GroupLists Lists = new ClanSystemWindow.GroupLists();

		// Token: 0x04000745 RID: 1861
		public ClanSystemWindow.GroupTab Tabs = new ClanSystemWindow.GroupTab();

		// Token: 0x04000746 RID: 1862
		public ClanSystemWindow.GroupStyle Styles = new ClanSystemWindow.GroupStyle();

		// Token: 0x04000747 RID: 1863
		public ClanSystemWindow.GroupTexture Textures = new ClanSystemWindow.GroupTexture();

		// Token: 0x04000748 RID: 1864
		public static ClanSystemWindow I;

		// Token: 0x04000749 RID: 1865
		public ClanTabController controller;

		// Token: 0x0400074A RID: 1866
		public ShortClanInfo SelectedClan = new ShortClanInfo();

		// Token: 0x0400074B RID: 1867
		public int gpToClan;

		// Token: 0x0400074C RID: 1868
		public int crToClan;

		// Token: 0x020000FA RID: 250
		[Serializable]
		internal class GroupLists
		{
			// Token: 0x0400074D RID: 1869
			public ScrollList InfoList = new InfoList();

			// Token: 0x0400074E RID: 1870
			public ScrollList RaceList = new RaceList();

			// Token: 0x0400074F RID: 1871
			public ScrollList ClanList = new ClanList();

			// Token: 0x04000750 RID: 1872
			public ScrollList MemberList = new MemberList();

			// Token: 0x04000751 RID: 1873
			public ScrollList LeadMemberList = new LeadMemberList();

			// Token: 0x04000752 RID: 1874
			public ScrollList ClanTransactionList = new ClanTransactionList();
		}

		// Token: 0x020000FB RID: 251
		[Serializable]
		internal class GroupTab
		{
			// Token: 0x04000753 RID: 1875
			public Rect clansTabInfo = default(Rect);

			// Token: 0x04000754 RID: 1876
			public Rect clansTabJoin = default(Rect);

			// Token: 0x04000755 RID: 1877
			public Rect clansTabCreate = default(Rect);

			// Token: 0x04000756 RID: 1878
			public Rect clansTabWars = default(Rect);

			// Token: 0x04000757 RID: 1879
			public Rect clansTabManagment = default(Rect);

			// Token: 0x04000758 RID: 1880
			public Rect clansTabLeveling = default(Rect);

			// Token: 0x04000759 RID: 1881
			public Rect plusBtn = default(Rect);

			// Token: 0x0400075A RID: 1882
			public Rect infoBtn = default(Rect);

			// Token: 0x0400075B RID: 1883
			public Rect createBtn1 = default(Rect);

			// Token: 0x0400075C RID: 1884
			public Rect createBtn2 = default(Rect);

			// Token: 0x0400075D RID: 1885
			public Rect createBtn3 = default(Rect);

			// Token: 0x0400075E RID: 1886
			public Rect joinRefresh = default(Rect);
		}

		// Token: 0x020000FC RID: 252
		[Serializable]
		internal class GroupStyle
		{
			// Token: 0x0400075F RID: 1887
			public GUIStyle styleClansWhiteTab = new GUIStyle();

			// Token: 0x04000760 RID: 1888
			public GUIStyle styleClansRedTab = new GUIStyle();

			// Token: 0x04000761 RID: 1889
			public GUIStyle styleWhiteLabel14 = new GUIStyle();

			// Token: 0x04000762 RID: 1890
			public GUIStyle styleWhiteLabel14MC = new GUIStyle();

			// Token: 0x04000763 RID: 1891
			public GUIStyle styleWhiteLabel16 = new GUIStyle();

			// Token: 0x04000764 RID: 1892
			public GUIStyle styleGrayLabel = new GUIStyle();

			// Token: 0x04000765 RID: 1893
			public GUIStyle styleGrayLabel14 = new GUIStyle();

			// Token: 0x04000766 RID: 1894
			public GUIStyle styleGrayLabel14Left = new GUIStyle();

			// Token: 0x04000767 RID: 1895
			public GUIStyle styleRedLabel = new GUIStyle();

			// Token: 0x04000768 RID: 1896
			public GUIStyle styleBlackLabel = new GUIStyle();

			// Token: 0x04000769 RID: 1897
			public GUIStyle styleJoinBlue = new GUIStyle();

			// Token: 0x0400076A RID: 1898
			public GUIStyle styleLevelLabel = new GUIStyle();

			// Token: 0x0400076B RID: 1899
			public GUIStyle stylePositionLabel = new GUIStyle();

			// Token: 0x0400076C RID: 1900
			public GUIStyle styleLevel = new GUIStyle();

			// Token: 0x0400076D RID: 1901
			public GUIStyle stylePosition = new GUIStyle();

			// Token: 0x0400076E RID: 1902
			public GUIStyle styleRequestBtn = new GUIStyle();

			// Token: 0x0400076F RID: 1903
			public GUIStyle stylePlusBtn = new GUIStyle();

			// Token: 0x04000770 RID: 1904
			public GUIStyle styleInfoBtn = new GUIStyle();

			// Token: 0x04000771 RID: 1905
			public GUIStyle styleCreateBtn = new GUIStyle();

			// Token: 0x04000772 RID: 1906
			public GUIStyle styleCreateBtnWhite = new GUIStyle();

			// Token: 0x04000773 RID: 1907
			public GUIStyle stylePopupYesNoBtn = new GUIStyle();

			// Token: 0x04000774 RID: 1908
			public GUIStyle styleJoinRefreshBtn = new GUIStyle();

			// Token: 0x04000775 RID: 1909
			public GUIStyle styleAcceptBtn = new GUIStyle();

			// Token: 0x04000776 RID: 1910
			public GUIStyle styleCancelBtn = new GUIStyle();

			// Token: 0x04000777 RID: 1911
			public GUIStyle styleBtnLabel = new GUIStyle();

			// Token: 0x04000778 RID: 1912
			public GUIStyle styleUnlockSkillBtn = new GUIStyle();

			// Token: 0x04000779 RID: 1913
			public GUIStyle styleAddBalanceBtn = new GUIStyle();

			// Token: 0x0400077A RID: 1914
			public GUIStyle styleWhiteKoratakiLable = new GUIStyle();

			// Token: 0x0400077B RID: 1915
			public GUIStyle styleWhiteTahomaLable = new GUIStyle();

			// Token: 0x0400077C RID: 1916
			public GUIStyle styleGoldLabel = new GUIStyle();

			// Token: 0x0400077D RID: 1917
			public GUIStyle DistinctionsLabelStyle = new GUIStyle();

			// Token: 0x0400077E RID: 1918
			public GUIStyle ListHeaderLabel = new GUIStyle();

			// Token: 0x0400077F RID: 1919
			public GUIStyle ClanEditBtn = new GUIStyle();

			// Token: 0x04000780 RID: 1920
			public GUIStyle LeaderMessageLabel = new GUIStyle();
		}

		// Token: 0x020000FD RID: 253
		[Serializable]
		internal class GroupTexture
		{
			// Token: 0x04000781 RID: 1921
			public Texture2D narrowStripe;

			// Token: 0x04000782 RID: 1922
			public Texture2D clanLeadBG;

			// Token: 0x04000783 RID: 1923
			public Texture2D exp;

			// Token: 0x04000784 RID: 1924
			public Texture2D createBack;

			// Token: 0x04000785 RID: 1925
			public Texture2D clanTagBack;

			// Token: 0x04000786 RID: 1926
			public Texture2D whiteGlow;

			// Token: 0x04000787 RID: 1927
			public Texture2D redGlow;

			// Token: 0x04000788 RID: 1928
			public Texture2D reputationBack;

			// Token: 0x04000789 RID: 1929
			public Texture2D kd;

			// Token: 0x0400078A RID: 1930
			public Texture2D yourStatsBack;

			// Token: 0x0400078B RID: 1931
			public Texture2D statsBack;

			// Token: 0x0400078C RID: 1932
			public Texture2D statsWtask;

			// Token: 0x0400078D RID: 1933
			public Texture2D statsAchievement;

			// Token: 0x0400078E RID: 1934
			public Texture2D statsProKill;

			// Token: 0x0400078F RID: 1935
			public Texture2D disbanded;

			// Token: 0x04000790 RID: 1936
			public Texture2D yellowBack;

			// Token: 0x04000791 RID: 1937
			public Texture2D blueBack;

			// Token: 0x04000792 RID: 1938
			public Texture2D progressRotate;

			// Token: 0x04000793 RID: 1939
			public Texture2D refreshIcon;

			// Token: 0x04000794 RID: 1940
			public Texture2D[] statsClass;

			// Token: 0x04000795 RID: 1941
			public Texture2D[] confirm;

			// Token: 0x04000796 RID: 1942
			public Texture2D clanSkillsGrid;

			// Token: 0x04000797 RID: 1943
			public Texture2D[] clanSkills;

			// Token: 0x04000798 RID: 1944
			public Texture2D clanBalanceBack;

			// Token: 0x04000799 RID: 1945
			public Texture2D clanSkillsDescriptionBack;

			// Token: 0x0400079A RID: 1946
			public Texture2D[] historyGlow;

			// Token: 0x0400079B RID: 1947
			public Texture2D avatar;

			// Token: 0x0400079C RID: 1948
			public Texture2D MessageArrow;

			// Token: 0x0400079D RID: 1949
			public Texture2D MessageBack;
		}
	}
}
