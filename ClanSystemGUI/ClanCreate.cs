using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace ClanSystemGUI
{
	// Token: 0x0200010A RID: 266
	internal class ClanCreate : AbstractClanPage
	{
		// Token: 0x060006F8 RID: 1784 RVA: 0x0003DCCC File Offset: 0x0003BECC
		public override void OnStart()
		{
			string text = this.clanTag;
			string text2 = this.clanTagColor;
			string text3 = this.clanName;
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0003DCF0 File Offset: 0x0003BEF0
		public string FixString(string str)
		{
			string pattern;
			if (str == this.clanTag)
			{
				pattern = "[^a-zA-Z0-9]";
			}
			else if (str == this.clanTagColor)
			{
				pattern = "[^a-fA-F0-9]";
			}
			else if (str == this.clanName && str.Length > 0)
			{
				if (str[0] == ' ')
				{
					pattern = "[^a-zA-Z0-9]";
				}
				else
				{
					pattern = "[^a-zA-Z0-9\\s]";
				}
			}
			else
			{
				pattern = string.Empty;
			}
			string empty = string.Empty;
			Regex regex = new Regex(pattern);
			return regex.Replace(str, empty);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0003DD98 File Offset: 0x0003BF98
		private string CheckName(float x, float y)
		{
			if (this.previousName != this.clanName)
			{
				this.nameStyle.textColor = this.tmpStyle.textColor;
				this.nameTimer.Start();
				this.nameState = Language.ClansCheck;
			}
			if (this.nameTimer.Elapsed > 1f && !this.nameRequestSended)
			{
				this.nameRequestSended = true;
				Main.AddDatabaseRequestCallBack<CheckClanName>(delegate
				{
					this.nameTimer.Stop();
					this.nameState = Language.ClansAvailable;
					this.nameRequestSended = false;
					this.nameStyle.textColor = Colors.RadarGreen;
				}, delegate
				{
					this.nameTimer.Stop();
					this.nameState = Language.ClansUnavailable;
					this.nameRequestSended = false;
					this.nameStyle.textColor = Colors.RadarRed;
				}, new object[]
				{
					this.clanName
				});
			}
			if (this.nameTimer.Enabled)
			{
				this.Krutilka(x, y);
			}
			this.previousName = this.clanName;
			ClanSystemWindow.I.Styles.styleGrayLabel.normal = this.nameStyle;
			return this.nameState;
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0003DE80 File Offset: 0x0003C080
		private string CheckColor(float x, float y)
		{
			if (this.previousColor != this.clanTagColor)
			{
				this.colorStyle.textColor = this.tmpStyle.textColor;
				this.colorTimer.Start();
				this.colorState = Language.ClansCheck;
			}
			if (this.colorTimer.Elapsed > 1f && !this.colorRequestSended && Utility.ValidColor(this.clanTagColor))
			{
				this.colorRequestSended = true;
				Main.AddDatabaseRequestCallBack<CheckClanTagColor>(delegate
				{
					this.colorTimer.Stop();
					this.colorState = Language.ClansAvailable;
					this.colorRequestSended = false;
					this.colorStyle.textColor = Colors.RadarGreen;
				}, delegate
				{
					this.colorTimer.Stop();
					this.colorState = Language.ClansUnavailable;
					this.colorRequestSended = false;
					this.colorStyle.textColor = Colors.RadarRed;
				}, new object[]
				{
					this.clanTagColor
				});
			}
			if (this.colorTimer.Enabled)
			{
				this.Krutilka(x, y);
			}
			if (this.colorTimer.Elapsed > 1f && !Utility.ValidColor(this.clanTagColor))
			{
				this.colorTimer.Stop();
				this.colorState = Language.ClansUnavailable;
				this.colorStyle.textColor = Colors.RadarRed;
			}
			this.previousColor = this.clanTagColor;
			ClanSystemWindow.I.Styles.styleGrayLabel.normal = this.colorStyle;
			return this.colorState;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0003DFC4 File Offset: 0x0003C1C4
		private string CheckTag(float x, float y)
		{
			if (this.previousTag != this.clanTag)
			{
				this.tagStyle.textColor = this.tmpStyle.textColor;
				this.tagTimer.Start();
				this.tagState = Language.ClansCheck;
			}
			if (this.tagTimer.Elapsed > 1f && !this.tagRequestSended)
			{
				this.tagRequestSended = true;
				Main.AddDatabaseRequestCallBack<CheckClanTag>(delegate
				{
					this.tagTimer.Stop();
					this.tagState = Language.ClansAvailable;
					this.tagRequestSended = false;
					this.tagStyle.textColor = Colors.RadarGreen;
				}, delegate
				{
					this.tagTimer.Stop();
					this.tagState = Language.ClansUnavailable;
					this.tagRequestSended = false;
					this.tagStyle.textColor = Colors.RadarRed;
				}, new object[]
				{
					this.clanTag
				});
			}
			if (this.tagTimer.Enabled)
			{
				this.Krutilka(x, y);
			}
			this.previousTag = this.clanTag;
			ClanSystemWindow.I.Styles.styleGrayLabel.normal = this.tagStyle;
			return this.tagState;
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0003E0AC File Offset: 0x0003C2AC
		private void AssembleClanInfo()
		{
			this.clanTotalInfo[0] = this.clanTag;
			this.clanTotalInfo[1] = this.clanTagColor;
			this.clanTotalInfo[2] = this.clanName;
			this.clanTotalInfo[3] = this.clanHomePageURL;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0003E0F4 File Offset: 0x0003C2F4
		private void Krutilka(float x, float y)
		{
			float angle = 180f * Time.realtimeSinceStartup * 1.5f;
			Vector2 vector = new Vector2(x, y);
			MainGUI.Instance.RotateGUI(angle, new Vector2(vector.x + (float)(ClanSystemWindow.I.Textures.progressRotate.width / 2), vector.y + (float)(ClanSystemWindow.I.Textures.progressRotate.height / 2) + 30f));
			MainGUI.Instance.Picture(new Vector2(vector.x, vector.y + 30f), ClanSystemWindow.I.Textures.progressRotate);
			MainGUI.Instance.RotateGUI(0f, Vector2.zero);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0003E1B8 File Offset: 0x0003C3B8
		public override void OnGUI()
		{
			GUI.DrawTexture(new Rect(473f, 70f, 66f, 46f), ClanSystemWindow.I.Textures.whiteGlow);
			GUI.Label(this.CreateLabel, Language.ClansCreateLabel, ClanSystemWindow.I.Styles.styleWhiteLabel14);
			GUI.DrawTexture(new Rect(75f, 150f, 556f, 70f), ClanSystemWindow.I.Textures.createBack);
			GUI.DrawTexture(new Rect(110f, 185f, 80f, 28f), ClanSystemWindow.I.Textures.clanTagBack);
			GUI.DrawTexture(new Rect(200f, 185f, 80f, 28f), ClanSystemWindow.I.Textures.clanTagBack);
			GUI.DrawTexture(new Rect(290f, 185f, 303f, 28f), MainGUI.Instance.settings_window[0]);
			MainGUI.Instance.textMaxSize = 5;
			this.clanTag = MainGUI.Instance.TextField(new Rect(110f, 185f, 80f, 28f), this.FixString(this.clanTag), 20, (!Utility.ValidColor(this.clanTagColor)) ? "#FFFFFF" : ("#" + this.clanTagColor), TextAnchor.MiddleCenter, true, true);
			MainGUI.Instance.textMaxSize = 6;
			this.clanTagColor = MainGUI.Instance.TextField(new Rect(200f, 185f, 80f, 28f), this.FixString(this.clanTagColor), 20, "#FFFFFF", TextAnchor.MiddleCenter, true, true);
			MainGUI.Instance.textMaxSize = 25;
			this.clanName = MainGUI.Instance.TextField(new Rect(290f, 185f, 303f, 28f), this.FixString(this.clanName), 20, "#FFFFFF", TextAnchor.MiddleCenter, true, true);
			GUI.Label(new Rect(130f, 155f, 100f, 25f), Language.ClansClantag, ClanSystemWindow.I.Styles.styleGrayLabel);
			GUI.Label(new Rect(200f, 155f, 100f, 25f), Language.ClansClantagColor, ClanSystemWindow.I.Styles.styleGrayLabel);
			GUI.Label(new Rect(400f, 155f, 100f, 25f), Language.ClansClanName, ClanSystemWindow.I.Styles.styleGrayLabel);
			GUI.Label(new Rect(95f, 210f, 100f, 50f), Language.ClansCreateHint1, ClanSystemWindow.I.Styles.styleGrayLabel14);
			GUI.Label(new Rect(195f, 210f, 80f, 50f), Language.ClansCreateHint2, ClanSystemWindow.I.Styles.styleGrayLabel14);
			GUI.Label(new Rect(400f, 210f, 100f, 50f), Language.ClansCreateHint3, ClanSystemWindow.I.Styles.styleGrayLabel14);
			ClanSystemWindow.I.Styles.styleGrayLabel.fontSize = 14;
			this.tmpStyle.textColor = ClanSystemWindow.I.Styles.styleGrayLabel.normal.textColor;
			if (this.clanTag != string.Empty)
			{
				GUI.Label(new Rect(130f, 105f, 100f, 20f), this.CheckTag(140f, 95f), ClanSystemWindow.I.Styles.styleGrayLabel);
			}
			if (this.clanTagColor != string.Empty)
			{
				GUI.Label(new Rect(200f, 105f, 100f, 20f), this.CheckColor(210f, 95f), ClanSystemWindow.I.Styles.styleGrayLabel);
			}
			if (this.clanName != string.Empty)
			{
				GUI.Label(new Rect(400f, 105f, 100f, 20f), this.CheckName(410f, 95f), ClanSystemWindow.I.Styles.styleGrayLabel);
			}
			ClanSystemWindow.I.Styles.styleGrayLabel.normal.textColor = this.tmpStyle.textColor;
			ClanSystemWindow.I.Styles.styleGrayLabel.fontSize = 18;
			GUI.DrawTexture(new Rect(75f, 260f, 556f, 70f), ClanSystemWindow.I.Textures.createBack);
			GUI.DrawTexture(new Rect(200f, 295f, 303f, 28f), MainGUI.Instance.settings_window[0]);
			GUI.Label(new Rect(270f, 265f, 200f, 25f), Language.ClansHomePage, ClanSystemWindow.I.Styles.styleGrayLabel);
			MainGUI.Instance.textMaxSize = 100;
			this.clanHomePageURL = MainGUI.Instance.TextField(new Rect(200f, 295f, 303f, 28f), this.clanHomePageURL, 20, "#FFFFFF", TextAnchor.MiddleCenter, true, true);
			GUI.Label(new Rect(260f, 320f, 200f, 50f), Language.ClansHomePageHint, ClanSystemWindow.I.Styles.styleGrayLabel14);
			GUI.DrawTexture(new Rect(75f, 370f, 556f, 70f), ClanSystemWindow.I.Textures.createBack);
			if (this.nameState != Language.ClansAvailable || this.colorState != Language.ClansAvailable || this.tagState != Language.ClansAvailable || this.clanTag == string.Empty || this.clanTagColor == string.Empty || this.clanName == string.Empty)
			{
				GUI.enabled = false;
			}
			if (Globals.I.clanCRPrice > 0)
			{
				if (CWGUI.Button(ClanSystemWindow.I.Tabs.createBtn1, Helpers.SeparateNumericString(Globals.I.clanCRPrice.ToString()), ClanSystemWindow.I.Styles.styleCreateBtnWhite))
				{
					this.AssembleClanInfo();
					EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.ClansPopupCreate, "base", delegate()
					{
					}, PopupState.createClan, false, true, this.clanTotalInfo));
				}
				GUI.DrawTexture(new Rect(225f, 407f, 21f, 22f), MainGUI.Instance.crIcon);
			}
			else
			{
				if (CWGUI.Button(ClanSystemWindow.I.Tabs.createBtn1, Globals.I.clanBasePrice.ToString(), ClanSystemWindow.I.Styles.styleCreateBtn))
				{
					this.AssembleClanInfo();
					EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.ClansPopupCreate, "base", delegate()
					{
					}, PopupState.createClan, false, true, this.clanTotalInfo));
				}
				GUI.DrawTexture(new Rect(215f, 407f, 21f, 22f), MainGUI.Instance.gldIcon);
			}
			if (CWGUI.Button(ClanSystemWindow.I.Tabs.createBtn2, Globals.I.clanExtendedPrice.ToString(), ClanSystemWindow.I.Styles.styleCreateBtn))
			{
				this.AssembleClanInfo();
				EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.ClansPopupCreate, "extended", delegate()
				{
				}, PopupState.createClan, false, true, this.clanTotalInfo));
			}
			if (CWGUI.Button(ClanSystemWindow.I.Tabs.createBtn3, Globals.I.clanPremiumPrice.ToString(), ClanSystemWindow.I.Styles.styleCreateBtn))
			{
				this.AssembleClanInfo();
				EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.ClansPopupCreate, "premium", delegate()
				{
				}, PopupState.createClan, false, true, this.clanTotalInfo));
			}
			GUI.enabled = true;
			GUI.DrawTexture(new Rect(365f, 407f, 21f, 22f), MainGUI.Instance.gldIcon);
			GUI.DrawTexture(new Rect(505f, 407f, 21f, 22f), MainGUI.Instance.gldIcon);
			GUI.Label(new Rect(165f, 375f, 100f, 25f), Language.ClansBase, ClanSystemWindow.I.Styles.styleGrayLabel);
			GUI.Label(new Rect(310f, 375f, 100f, 25f), Language.ClansExtended, ClanSystemWindow.I.Styles.styleGrayLabel);
			GUI.Label(new Rect(450f, 375f, 100f, 25f), Language.ClansPremium, ClanSystemWindow.I.Styles.styleGrayLabel);
			GUI.Label(new Rect(155f, 435f, 100f, 25f), Language.ClansCreateHint4, ClanSystemWindow.I.Styles.styleGrayLabel14);
			GUI.Label(new Rect(300f, 435f, 100f, 25f), Language.ClansCreateHint5, ClanSystemWindow.I.Styles.styleGrayLabel14);
			GUI.Label(new Rect(440f, 435f, 100f, 25f), Language.ClansCreateHint6, ClanSystemWindow.I.Styles.styleGrayLabel14);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0003EBF8 File Offset: 0x0003CDF8
		public override void OnUpdate()
		{
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0003EBFC File Offset: 0x0003CDFC
		public override void Clear()
		{
			this.clanTag = string.Empty;
			this.clanTagColor = string.Empty;
			this.clanName = string.Empty;
			this.clanHomePageURL = string.Empty;
			this.previousTag = string.Empty;
			this.previousColor = string.Empty;
			this.previousName = string.Empty;
			this.tagState = string.Empty;
			this.colorState = string.Empty;
			this.nameState = string.Empty;
			this.tagTimer.Stop();
			this.colorTimer.Stop();
			this.nameTimer.Stop();
		}

		// Token: 0x040007C7 RID: 1991
		private GUIStyleState tagStyle = new GUIStyleState();

		// Token: 0x040007C8 RID: 1992
		private GUIStyleState colorStyle = new GUIStyleState();

		// Token: 0x040007C9 RID: 1993
		private GUIStyleState nameStyle = new GUIStyleState();

		// Token: 0x040007CA RID: 1994
		private GUIStyleState tmpStyle = new GUIStyleState();

		// Token: 0x040007CB RID: 1995
		private string clanTag = string.Empty;

		// Token: 0x040007CC RID: 1996
		private string clanTagColor = string.Empty;

		// Token: 0x040007CD RID: 1997
		private string clanName = string.Empty;

		// Token: 0x040007CE RID: 1998
		private string clanHomePageURL = string.Empty;

		// Token: 0x040007CF RID: 1999
		private string previousTag = string.Empty;

		// Token: 0x040007D0 RID: 2000
		private string previousColor = string.Empty;

		// Token: 0x040007D1 RID: 2001
		private string previousName = string.Empty;

		// Token: 0x040007D2 RID: 2002
		private string[] clanTotalInfo = new string[4];

		// Token: 0x040007D3 RID: 2003
		private string tagState = string.Empty;

		// Token: 0x040007D4 RID: 2004
		private string colorState = string.Empty;

		// Token: 0x040007D5 RID: 2005
		private string nameState = string.Empty;

		// Token: 0x040007D6 RID: 2006
		private string urlState = string.Empty;

		// Token: 0x040007D7 RID: 2007
		private eTimer tagTimer = new eTimer();

		// Token: 0x040007D8 RID: 2008
		private eTimer colorTimer = new eTimer();

		// Token: 0x040007D9 RID: 2009
		private eTimer nameTimer = new eTimer();

		// Token: 0x040007DA RID: 2010
		private eTimer urlTimer = new eTimer();

		// Token: 0x040007DB RID: 2011
		private bool nameRequestSended;

		// Token: 0x040007DC RID: 2012
		private bool colorRequestSended;

		// Token: 0x040007DD RID: 2013
		private bool tagRequestSended;

		// Token: 0x040007DE RID: 2014
		private Rect CreateLabel = new Rect(35f, 55f, 100f, 25f);
	}
}
