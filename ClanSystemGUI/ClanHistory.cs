using System;
using UnityEngine;

namespace ClanSystemGUI
{
	// Token: 0x02000113 RID: 275
	internal class ClanHistory : AbstractClanPage
	{
		// Token: 0x06000750 RID: 1872 RVA: 0x00042CF0 File Offset: 0x00040EF0
		private void GenerateList()
		{
			ClanSystemWindow.I.Lists.ClanTransactionList.Clear();
			for (int i = Main.UserInfo.clanData.clanTransactionList.Length - 1; i >= 0; i--)
			{
				ClanSystemWindow.I.Lists.ClanTransactionList.Add(new ClanTransactionBar(Main.UserInfo.clanData.clanTransactionList[i]));
			}
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00042D60 File Offset: 0x00040F60
		public override void OnStart()
		{
			this.requestSended = false;
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00042D6C File Offset: 0x00040F6C
		public override void OnGUI()
		{
			if (this.refreshTimer < Time.realtimeSinceStartup && GUI.Button(new Rect(653f, 83f, 28f, 23f), string.Empty, ClanSystemWindow.I.Styles.styleJoinRefreshBtn))
			{
				this.refreshTimer = Time.realtimeSinceStartup + 3f;
				this.requestSended = false;
			}
			GUI.DrawTexture(new Rect(658f, 85f, (float)ClanSystemWindow.I.Textures.refreshIcon.width, (float)ClanSystemWindow.I.Textures.refreshIcon.height), ClanSystemWindow.I.Textures.refreshIcon);
			if (!this.requestSended)
			{
				Main.AddDatabaseRequestCallBack<LoadClanTransaction>(new DatabaseEvent.SomeAction(this.GenerateList), delegate
				{
				}, new object[]
				{
					Main.UserInfo.clanID.ToString()
				});
				this.requestSended = true;
			}
			GUI.Label(new Rect(180f, 90f, 100f, 20f), Language.BankQuantity, ClanSystemWindow.I.Styles.styleGrayLabel);
			GUI.Label(new Rect(255f, 90f, 100f, 20f), Language.BankValuta, ClanSystemWindow.I.Styles.styleGrayLabel);
			GUI.Label(new Rect(375f, 90f, 100f, 20f), Language.ClansHistoryWho, ClanSystemWindow.I.Styles.styleGrayLabel);
			GUI.Label(new Rect(540f, 90f, 100f, 20f), Language.BankDate, ClanSystemWindow.I.Styles.styleGrayLabel);
			ClanSystemWindow.I.Lists.ClanTransactionList.OnGUI();
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00042F5C File Offset: 0x0004115C
		public override void OnUpdate()
		{
		}

		// Token: 0x04000811 RID: 2065
		private bool requestSended;

		// Token: 0x04000812 RID: 2066
		private float refreshTimer;
	}
}
