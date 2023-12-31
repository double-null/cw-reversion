using System;
using UnityEngine;

namespace ClanSystemGUI
{
	// Token: 0x0200010C RID: 268
	internal class ClanRace : AbstractClanPage
	{
		// Token: 0x06000714 RID: 1812 RVA: 0x0003F288 File Offset: 0x0003D488
		public override void OnStart()
		{
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0003F28C File Offset: 0x0003D48C
		private void GenerateList()
		{
			ClanSystemWindow.I.Lists.RaceList.Clear();
			for (int i = 0; i < Main.UserInfo.clanData.clanShortInfoList.Length; i++)
			{
				ClanSystemWindow.I.Lists.RaceList.Add(new ClanRaceBar(Main.UserInfo.clanData.clanShortInfoList[i]));
			}
			ClanSystemWindow.I.Lists.RaceList.Sort();
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0003F310 File Offset: 0x0003D510
		public void OnGUI(int topType)
		{
			string text = base.Search();
			if (this._topType != topType)
			{
				this._dataRequest = false;
				this._topType = topType;
			}
			if (!this._dataRequest || !string.IsNullOrEmpty(text))
			{
				this._dataRequest = true;
				Main.AddDatabaseRequestCallBack<ListRace>(new DatabaseEvent.SomeAction(this.GenerateList), delegate
				{
				}, new object[]
				{
					text,
					topType
				});
			}
			int num = 87;
			if (Globals.I.ongoingRace != 0)
			{
				GUI.Label(new Rect(27f, (float)num, 100f, 25f), Language.ClansRaceAttention, ClanSystemWindow.I.Styles.styleRedLabel);
				GUI.Label(new Rect(90f, (float)num, 200f, 25f), Language.ClansRaceHint, ClanSystemWindow.I.Styles.styleWhiteLabel14);
				GUI.Label(new Rect(520f, (float)num, 100f, 25f), Language.ClansRaceEnding, ClanSystemWindow.I.Styles.styleRedLabel);
				GUI.Label(new Rect(580f, (float)num, 200f, 25f), " " + Globals.I.raceEnds, ClanSystemWindow.I.Styles.styleWhiteLabel14);
			}
			else
			{
				GUI.Label(new Rect(27f, (float)num, 200f, 25f), Language.ClansRaceHint1, ClanSystemWindow.I.Styles.styleWhiteLabel14);
			}
			GUI.DrawTexture(new Rect(25f, 110f, 660f, 25f), ClanSystemWindow.I.Textures.narrowStripe);
			GUI.Label(new Rect(65f, 110f, 100f, 25f), Language.CarrPlace, ClanSystemWindow.I.Styles.styleGrayLabel);
			GUI.Label(new Rect(130f, 110f, 100f, 25f), Language.CarrTAG, ClanSystemWindow.I.Styles.styleGrayLabel);
			GUI.Label(new Rect(165f, 110f, 100f, 25f), Language.CarrName, ClanSystemWindow.I.Styles.styleGrayLabel);
			GUI.Label(new Rect(348f, 110f, 100f, 25f), Language.ClansLead, ClanSystemWindow.I.Styles.styleGrayLabel);
			GUI.Label(new Rect(445f, 110f, 100f, 25f), Language.CarrPlayers, ClanSystemWindow.I.Styles.styleGrayLabel);
			int race_points = CVars.race_points;
			string text2;
			if (race_points != 0)
			{
				if (race_points != 1)
				{
					text2 = Language.ClansRaceExp;
				}
				else
				{
					text2 = Language.ClansRaceKills;
				}
			}
			else
			{
				text2 = Language.ClansRaceExp;
			}
			GUI.Label(new Rect(510f, 110f, 150f, 25f), text2, ClanSystemWindow.I.Styles.styleGrayLabel);
			if (this.refreshTimer < Time.realtimeSinceStartup && GUI.Button(new Rect(653f, 112f, 28f, 23f), string.Empty, ClanSystemWindow.I.Styles.styleJoinRefreshBtn))
			{
				this.refreshTimer = Time.realtimeSinceStartup + 5f;
				this._dataRequest = false;
			}
			GUI.DrawTexture(new Rect(658f, 114f, (float)ClanSystemWindow.I.Textures.refreshIcon.width, (float)ClanSystemWindow.I.Textures.refreshIcon.height), ClanSystemWindow.I.Textures.refreshIcon);
			ClanSystemWindow.I.Lists.RaceList.OnGUI();
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0003F6FC File Offset: 0x0003D8FC
		public override void OnUpdate()
		{
		}

		// Token: 0x040007EC RID: 2028
		private bool _dataRequest;

		// Token: 0x040007ED RID: 2029
		private float refreshTimer;

		// Token: 0x040007EE RID: 2030
		private int _topType;
	}
}
