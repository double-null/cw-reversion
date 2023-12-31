using System;
using UnityEngine;

namespace ClanSystemGUI
{
	// Token: 0x0200010B RID: 267
	internal class ClanJoin : AbstractClanPage
	{
		// Token: 0x0600070D RID: 1805 RVA: 0x0003EE1C File Offset: 0x0003D01C
		public override void OnStart()
		{
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0003EE20 File Offset: 0x0003D020
		private void InitTextures()
		{
			MainHelpGUI component = MainGUI.Instance.GetComponent<MainHelpGUI>();
			this._idleResetRequestsButton = component.tab_buttons[0];
			this._overResetRequestsButton = component.tab_buttons[1];
			this._selectedResetRequestsButton = component.tab_buttons[2];
			this._texturesInited = true;
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0003EE6C File Offset: 0x0003D06C
		public override void OnGUI()
		{
			if (!this._texturesInited)
			{
				this.InitTextures();
			}
			GUI.DrawTexture(new Rect(543f, 70f, 66f, 46f), ClanSystemWindow.I.Textures.whiteGlow);
			GUI.Label(this.JoinLabelWhite, Language.ClansRequestLeft, ClanSystemWindow.I.Styles.styleWhiteLabel14);
			GUI.Label(this.JoinLabelBlue, Main.UserInfo.ClanRequestsLeft.ToString() + "/" + Main.UserInfo.TotalClanRequests.ToString(), ClanSystemWindow.I.Styles.styleJoinBlue);
			GUI.DrawTexture(new Rect(25f, 81f, 660f, 25f), ClanSystemWindow.I.Textures.narrowStripe);
			if (this.refreshTimer < Time.realtimeSinceStartup && GUI.Button(new Rect(653f, 83f, 28f, 23f), string.Empty, ClanSystemWindow.I.Styles.styleJoinRefreshBtn))
			{
				this.refreshTimer = Time.realtimeSinceStartup + 5f;
				ClanTabJoin._dataRequest = false;
			}
			GUI.DrawTexture(new Rect(658f, 85f, (float)ClanSystemWindow.I.Textures.refreshIcon.width, (float)ClanSystemWindow.I.Textures.refreshIcon.height), ClanSystemWindow.I.Textures.refreshIcon);
			MainGUI instance = MainGUI.Instance;
			Vector2? scale = new Vector2?(new Vector2(1f, 1.22f));
			if (instance.Button(new Vector2(564f, 77f), this._idleResetRequestsButton, this._overResetRequestsButton, this._selectedResetRequestsButton, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, scale, null, null).Clicked)
			{
				Main.AddDatabaseRequestCallBack<WithdrawAllClanRequests>(delegate
				{
					(ClanSystemWindow.I.Lists.ClanList as ClanList).WithdrawAllRequests();
				}, delegate
				{
				}, new object[0]);
			}
			GUI.Label(new Rect(563f, 76f, (float)this._idleResetRequestsButton.width, (float)this._idleResetRequestsButton.height), Language.ClansWithdraw, ClanSystemWindow.I.Styles.styleBlackLabel);
			GUI.Label(new Rect(563f, 86f, (float)this._idleResetRequestsButton.width, (float)this._idleResetRequestsButton.height), Language.ClansManagmentDiscard2, ClanSystemWindow.I.Styles.styleBlackLabel);
			GUI.Label(new Rect(35f, 81f, 100f, 25f), Language.CarrPlace, ClanSystemWindow.I.Styles.styleGrayLabel);
			GUI.Label(new Rect(100f, 81f, 100f, 25f), Language.CarrTAG, ClanSystemWindow.I.Styles.styleGrayLabel);
			GUI.Label(new Rect(135f, 81f, 100f, 25f), Language.CarrName, ClanSystemWindow.I.Styles.styleGrayLabel);
			GUI.Label(new Rect(348f, 81f, 100f, 25f), Language.ClansLead, ClanSystemWindow.I.Styles.styleGrayLabel);
			GUI.Label(new Rect(445f, 81f, 100f, 25f), Language.CarrPlayers, ClanSystemWindow.I.Styles.styleGrayLabel);
			GUI.Label(new Rect(510f, 81f, 150f, 25f), Language.CarrEXP, ClanSystemWindow.I.Styles.styleGrayLabel);
			ClanSystemWindow.I.Lists.ClanList.OnGUI();
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0003F25C File Offset: 0x0003D45C
		public override void OnUpdate()
		{
		}

		// Token: 0x040007E3 RID: 2019
		private Rect JoinLabelWhite = new Rect(35f, 55f, 200f, 25f);

		// Token: 0x040007E4 RID: 2020
		private Rect JoinLabelBlue = new Rect(155f, 55f, 100f, 25f);

		// Token: 0x040007E5 RID: 2021
		private float refreshTimer;

		// Token: 0x040007E6 RID: 2022
		private Texture2D _idleResetRequestsButton;

		// Token: 0x040007E7 RID: 2023
		private Texture2D _overResetRequestsButton;

		// Token: 0x040007E8 RID: 2024
		private Texture2D _selectedResetRequestsButton;

		// Token: 0x040007E9 RID: 2025
		private bool _texturesInited;
	}
}
