using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace ClanSystemGUI
{
	// Token: 0x02000119 RID: 281
	internal class ClanRaceBar : IScrollListItem, IComparable
	{
		// Token: 0x06000791 RID: 1937 RVA: 0x0004690C File Offset: 0x00044B0C
		public ClanRaceBar()
		{
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00046920 File Offset: 0x00044B20
		public ClanRaceBar(ShortClanInfo info)
		{
			this.shortClanInfo = info;
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x00046940 File Offset: 0x00044B40
		public float barHeight
		{
			get
			{
				return (float)ClanSystemWindow.I.Textures.statsBack.height;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x00046958 File Offset: 0x00044B58
		public float barWidth
		{
			get
			{
				return (float)ClanSystemWindow.I.Textures.statsBack.width;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x00046970 File Offset: 0x00044B70
		public float Width
		{
			get
			{
				return this.barWidth;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x00046978 File Offset: 0x00044B78
		public float Height
		{
			get
			{
				return this.barHeight;
			}
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00046980 File Offset: 0x00044B80
		public int CompareTo(object obj)
		{
			if (this.shortClanInfo.raceExp.Value > ((ClanRaceBar)obj).shortClanInfo.raceExp.Value)
			{
				return -1;
			}
			if (this.shortClanInfo.raceExp.Value < ((ClanRaceBar)obj).shortClanInfo.raceExp.Value)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x000469E8 File Offset: 0x00044BE8
		public void DrawBar(float x = 0f, float y = 0f, int index = 0, bool yourStats = false)
		{
			if (this.shortClanInfo.SocialNet > 0)
			{
				int num = (this.shortClanInfo.SocialNet != 2) ? 16 : 8;
				int num2 = this.shortClanInfo.SocialNet - 1;
				if (num2 > MainGUI.Instance.SocialNetIcons.Length - 1)
				{
					num2 = 2;
				}
				Texture2D texture2D = MainGUI.Instance.SocialNetIcons[num2];
				GUI.DrawTexture(new Rect(x + 32f, y + (float)num, (float)(texture2D.width / 2), (float)(texture2D.height / 2)), texture2D);
			}
			x += 30f;
			GUI.DrawTexture(new Rect(x + 25f, y + 5f, 643f, 38f), ClanSystemWindow.I.Textures.statsBack);
			if (CWGUI.Button(new Rect(x + 608f, y + 14f, 23f, 23f), string.Empty, ClanSystemWindow.I.Styles.styleInfoBtn))
			{
				ClanSystemWindow.I.SelectedClan = this.shortClanInfo;
				this.shortClanInfo.ReloadAdditionInfo(delegate
				{
					ClanSystemWindow.I.Lists.MemberList.Clear();
					ClanSystemWindow.I.controller.SetState(ClanSystemWindow.I.controller.TabInfo);
					ClanTabInfo.showInfo = true;
				}, delegate
				{
				}, false);
			}
			string text = (index + 1).ToString();
			int num3 = (text.Length <= 1) ? 25 : 32;
			GUI.Label(new Rect(x + (float)num3, y + 10f, 50f, 25f), this.shortClanInfo.clanLevel.ToString(), ClanSystemWindow.I.Styles.styleLevelLabel);
			GUI.Label(new Rect(x + 27f, y + 22f, 50f, 10f), Language.Level.ToLower(), ClanSystemWindow.I.Styles.styleLevel);
			x -= 30f;
			GUI.DrawTexture(new Rect(x + 315f, y + 5f, 29f, 38f), MainGUI.Instance.rank_icon[this.shortClanInfo.clanLeaderLevel]);
			GUI.Label(new Rect(x + 62f, y + 10f, 50f, 20f), text, ClanSystemWindow.I.Styles.styleWhiteLabel16);
			MainGUI.Instance.TextField(new Rect(x + 110f, y + 15f, 50f, 20f), this.shortClanInfo.tag, 16, "#" + this.shortClanInfo.tagColor, TextAnchor.MiddleCenter, false, false);
			GUI.Label(new Rect(x + 165f, y + 15f, 200f, 20f), this.shortClanInfo.name, ClanSystemWindow.I.Styles.styleWhiteLabel16);
			GUIStyle guistyle = new GUIStyle(ClanSystemWindow.I.Styles.styleWhiteLabel16);
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			if (Regex.IsMatch(this.shortClanInfo.leaderNickColor.Substring(1, 6), "^[0-9,A-F,a-f]+$"))
			{
				num4 = Convert.ToInt32(this.shortClanInfo.leaderNickColor.Substring(1, 2), 16);
				num5 = Convert.ToInt32(this.shortClanInfo.leaderNickColor.Substring(3, 2), 16);
				num6 = Convert.ToInt32(this.shortClanInfo.leaderNickColor.Substring(5, 2), 16);
			}
			guistyle.normal.textColor = new Color((float)num4 / 256f, (float)num5 / 256f, (float)num6 / 256f);
			GUI.Label(new Rect(x + 348f, y + 6f, 200f, 20f), this.shortClanInfo.leaderNick, guistyle);
			GUI.Label(new Rect(x + 348f, y + 23f, 200f, 20f), MainGUI.Instance.FormatedUserName(this.shortClanInfo.leaderFname, this.shortClanInfo.leaderLname), ClanSystemWindow.I.Styles.styleGrayLabel14Left);
			ClanSystemWindow.I.Styles.styleWhiteLabel16.alignment = TextAnchor.MiddleCenter;
			GUI.Label(new Rect(x + 440f, y + 12f, 50f, 27f), this.shortClanInfo.membersCount.ToString(), ClanSystemWindow.I.Styles.styleWhiteLabel16);
			GUI.Label(new Rect(x + 520f, y + 12f, 86f, 27f), Helpers.SeparateNumericString(this.shortClanInfo.raceExp.Value.ToString("F0")), ClanSystemWindow.I.Styles.styleWhiteLabel16);
			ClanSystemWindow.I.Styles.styleWhiteLabel16.alignment = TextAnchor.MiddleLeft;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00046EDC File Offset: 0x000450DC
		public void OnGUI(float x, float y, int index)
		{
			this.DrawBar(x, y, index, false);
		}

		// Token: 0x0400082F RID: 2095
		public static int position;

		// Token: 0x04000830 RID: 2096
		public ShortClanInfo shortClanInfo = new ShortClanInfo();
	}
}
