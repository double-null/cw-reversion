using System;
using UnityEngine;

namespace CarrierGUINamespace
{
	// Token: 0x020000F7 RID: 247
	internal class CarrierStatistics
	{
		// Token: 0x060006AD RID: 1709 RVA: 0x0003BC10 File Offset: 0x00039E10
		public CarrierStatistics(OverviewInfo info)
		{
			this.info = info;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0003BC20 File Offset: 0x00039E20
		public void OnGUI()
		{
			GUI.DrawTexture(new Rect(663f, 55f, (float)CarrierGUI.I.statsScrollbarBg.width, (float)CarrierGUI.I.statsScrollbarBg.height), CarrierGUI.I.statsScrollbarBg);
			this.DrawStatisticBar(Language.CarrOnlineTime, this.info.userStats.timeOnlineStr, CarrierGUI.I.stats[1], 0f, 0f, false);
			this.DrawStatisticBar(Language.CarrHardcoreTime, this.info.userStats.timeHardcoreStr, CarrierGUI.I.stats[2], 0f, 30f, false);
			this.DrawStatisticBar(Language.CarrWins, this.info.winCount, CarrierGUI.I.stats[3], 0f, 60f, true);
			this.DrawStatisticBar(Language.CarrLoose, this.info.lossCount, CarrierGUI.I.stats[4], 0f, 90f, true);
			this.DrawStatisticBar(Language.CarrDamage, this.info.userStats.totalDamage, CarrierGUI.I.stats[5], 0f, 120f, true);
			this.DrawStatisticBar(Language.CarrUsedBullets, this.info.userStats.totalAmmo, CarrierGUI.I.stats[6], 0f, 150f, true);
			this.DrawStatisticBar(Language.CarrHeadShots, this.info.userStats.headShots, CarrierGUI.I.stats[7], 0f, 180f, true);
			this.DrawStatisticBar(Language.CarrDoubleHeadShots, this.info.userStats.doubleHeadShots, CarrierGUI.I.stats[8], 0f, 210f, true);
			this.DrawStatisticBar(Language.CarrLongHeadShots, this.info.userStats.longShots, CarrierGUI.I.stats[9], 0f, 240f, true);
			this.DrawStatisticBar(Language.CarrDoubleKills, this.info.userStats.doubleKills, CarrierGUI.I.stats[10], 0f, 270f, true);
			this.DrawStatisticBar(Language.CarrTripleKills, this.info.userStats.tripleKills, CarrierGUI.I.stats[11], 0f, 300f, true);
			this.DrawStatisticBar("RAGEKILLS", this.info.userStats.rageKills, CarrierGUI.I.stats[12], 0f, 330f, true);
			this.DrawStatisticBar("STORMKILLS", this.info.userStats.stormKills, CarrierGUI.I.stats[13], 0f, 360f, true);
			this.DrawStatisticBar("PROKILLS", this.info.userStats.proKills, CarrierGUI.I.stats[14], 0f, 390f, true);
			this.DrawStatisticBar(Language.CarrAssists, this.info.userStats.assists, CarrierGUI.I.stats[15], 0f, 420f, true);
			this.DrawStatisticBar(Language.CarrCreditSpend, this.info.userStats.creditsSpent, CarrierGUI.I.stats[16], 320f, 0f, true);
			this.DrawStatisticBar(Language.CarrWTaskOpened, this.info.WtaskOpenedCount, CarrierGUI.I.stats[17], 320f, 30f, true);
			this.DrawStatisticBar(Language.CarrAchievingGetted, AchievementsEngine.OpenedCount(this.info), CarrierGUI.I.stats[18], 320f, 60f, true);
			this.DrawStatisticBar(Language.CarrArmstreakGetted, this.info.userStats.armstreaksEarned, CarrierGUI.I.stats[19], 320f, 90f, true);
			this.DrawStatisticBar(Language.CarrSupportCaused, this.info.userStats.armstreaksUsed, CarrierGUI.I.stats[20], 320f, 120f, true);
			this.DrawStatisticBar(Language.CarrKnifeKill, this.info.userStats.knifeKills, CarrierGUI.I.stats[21], 320f, 150f, true);
			this.DrawStatisticBar(Language.CarrGrenadeKill, this.info.userStats.grenadeKills, CarrierGUI.I.stats[22], 320f, 180f, true);
			this.DrawStatisticBar(Language.CarrTeammateKill, this.info.userStats.teamKills, CarrierGUI.I.stats[23], 320f, 210f, true);
			this.DrawStatisticBar(Language.CarrSuicides, this.info.userStats.suicides, CarrierGUI.I.stats[24], 320f, 240f, true);
			this.DrawStatisticBar(Language.CarrBEARKills, this.info.userStats.bearKills, CarrierGUI.I.stats[25], 320f, 270f, true);
			this.DrawStatisticBar(Language.CarrUsecKills, this.info.userStats.usecKills, CarrierGUI.I.stats[26], 320f, 300f, true);
			this.DrawStatisticBar(Language.CarrFavoriteWeapon, (this.info.userStats.favGun != -1) ? this.info.weaponsStates[this.info.userStats.favGun].CurrentWeapon.ShortName : Language.No, CarrierGUI.I.stats[27], 320f, 330f, false);
			this.DrawStatisticBar(Language.CarrTotalAccuracy, (float)this.info.userStats.totalHits / ((float)this.info.userStats.totalAmmo + 0.1f) * 100f + "%", CarrierGUI.I.stats[29], 320f, 360f, false);
			this.DrawStatisticBar(Language.CarrMatchesCompleted, this.info.userStats.matchesEnded, CarrierGUI.I.stats[3], 320f, 390f, true);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0003C2EC File Offset: 0x0003A4EC
		private void DrawStatisticBar(string description, object amount, Texture2D t, float x, float y, bool separateString = true)
		{
			GUI.DrawTexture(new Rect(25f + x, 60f + y, (float)CarrierGUI.I.stats[0].width, (float)CarrierGUI.I.stats[0].height), CarrierGUI.I.stats[0]);
			GUI.DrawTexture(new Rect(25f + x, 60f + y, (float)t.width, (float)t.height), t);
			MainGUI.Instance.TextLabel(new Rect(60f + x, 60f + y, 200f, 28f), description, 16, "#cccccc", TextAnchor.MiddleLeft, true);
			MainGUI.Instance.TextLabel(new Rect(238f + x, 60f + y, 100f, 28f), (!separateString) ? amount.ToString() : Helpers.SeparateNumericString(amount.ToString()), 16, "#cccccc", TextAnchor.MiddleCenter, true);
		}

		// Token: 0x0400073B RID: 1851
		private OverviewInfo info;
	}
}
