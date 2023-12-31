using System;
using System.Collections.Generic;
using UnityEngine;

namespace LeagueGUI
{
	// Token: 0x02000315 RID: 789
	internal class PrizesFrame : AbstractFrame
	{
		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06001ADA RID: 6874 RVA: 0x000F2600 File Offset: 0x000F0800
		private Rect PrizesFrameRect
		{
			get
			{
				return new Rect((float)(Screen.width - 800) * 0.5f + 4f, (float)(Screen.height - 600) * 0.5f + 420f, 394f, 170f);
			}
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x000F264C File Offset: 0x000F084C
		public override void OnStart()
		{
			this._back = LeagueWindow.I.Textures.PrizePlaceBack;
			if (!LeagueWindow.I.LeagueInfo.Offseason)
			{
				for (int i = 0; i < this._medals.Length; i++)
				{
					this._medals[i] = LeagueWindow.I.LeagueInfo.SeasonPrizes[i].AwardIcon;
				}
				foreach (LeagueInfo.SeasonPrize seasonPrize in LeagueWindow.I.LeagueInfo.SeasonPrizes)
				{
					this.prizesAmount.Add(seasonPrize.Amount);
					this.prizesCurrency.Add(seasonPrize.IsGp);
				}
				this.prizesPlace.Add("1 " + Language.LeaguePlace);
				this.prizesPlace.Add("2 " + Language.LeaguePlace);
				this.prizesPlace.Add("3 " + Language.LeaguePlace);
				this.prizesPlace.Add("4-10 " + Language.LeaguePlace);
				this.prizesPlace.Add("11-100 " + Language.LeaguePlace);
				this.prizesPlace.Add("101-300 " + Language.LeaguePlace);
			}
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x000F27D8 File Offset: 0x000F09D8
		public override void OnGUI()
		{
			GUI.BeginGroup(this.PrizesFrameRect);
			GUI.DrawTexture(new Rect(0f, 0f, this.PrizesFrameRect.width, 20f), LeagueWindow.I.Textures.Black);
			GUI.DrawTexture(new Rect(0f, 20f, this.PrizesFrameRect.width, this.PrizesFrameRect.height - 20f), LeagueWindow.I.Textures.Gray);
			LeagueWindow.I.Styles.BrownLabel.alignment = TextAnchor.MiddleLeft;
			this._prizes = ((!LeagueWindow.I.LeagueInfo.Offseason) ? Language.LeagueCurrentPrizes : Language.LeagueFuturePrizes);
			GUI.Label(new Rect(5f, 4f, 50f, 14f), this._prizes, LeagueWindow.I.Styles.BrownLabel);
			LeagueWindow.I.Styles.BrownLabel.alignment = TextAnchor.MiddleCenter;
			GUI.DrawTexture(new Rect(5f, 110f, this.PrizesFrameRect.width - 10f, 1f), MainGUI.Instance.black);
			if (LeagueWindow.I.LeagueInfo.Offseason)
			{
				LeagueWindow.I.Styles.WhiteLabel16.alignment = TextAnchor.MiddleCenter;
				GUI.Label(new Rect(0f, 60f, this.PrizesFrameRect.width, 20f), Helpers.ColoredText(Language.LeagueUnknown, "#bfbfbf"), LeagueWindow.I.Styles.WhiteLabel16);
				GUI.Label(new Rect(0f, 80f, this.PrizesFrameRect.width, 20f), Helpers.ColoredText(Language.LeagueOffSeason, "#bfbfbf"), LeagueWindow.I.Styles.WhiteLabel16);
				LeagueWindow.I.Styles.WhiteLabel16.alignment = TextAnchor.MiddleLeft;
			}
			else
			{
				byte b = 0;
				while ((int)b < this._medals.Length)
				{
					GUI.DrawTexture(new Rect((float)(30 + b * 120), 25f, (float)this._back.width, (float)this._back.height), this._back);
					GUI.DrawTexture(new Rect((float)(30 + b * 120), 115f, (float)this._back.width, (float)this._back.height), this._back);
					GUI.Label(new Rect((float)(30 + b * 120), 26f, (float)this._back.width, (float)this._back.height), this.prizesPlace[(int)b], LeagueWindow.I.Styles.BlackLabel);
					GUI.DrawTexture(new Rect((float)(50 + b * 120), 40f, (float)this._medals[(int)b].width, (float)this._medals[(int)b].height), this._medals[(int)b]);
					GUI.Label(new Rect((float)(30 + b * 120), 85f, 50f, 24f), this.prizesAmount[(int)b].ToString(), LeagueWindow.I.Styles.GPLabel);
					GUI.DrawTexture(new Rect((float)(80 + b * 120), 86f, (float)MainGUI.Instance.gldIcon.width, (float)MainGUI.Instance.gldIcon.height), MainGUI.Instance.gldIcon);
					b += 1;
				}
				Texture2D texture2D = MainGUI.Instance.crIcon;
				GUIStyle style = LeagueWindow.I.Styles.CRLabel;
				int num = 0;
				byte b2 = 3;
				while ((int)b2 < this.prizesAmount.Count - 1)
				{
					string text = Helpers.SeparateNumericString(this.prizesAmount[(int)b2].ToString());
					if (this.prizesCurrency[(int)b2])
					{
						texture2D = MainGUI.Instance.gldIcon;
						style = LeagueWindow.I.Styles.GPLabel;
					}
					GUI.Label(new Rect((float)(30 + num * 120), 116f, (float)this._back.width, (float)this._back.height), this.prizesPlace[(int)b2], LeagueWindow.I.Styles.BlackLabel);
					GUI.Label(new Rect((float)(30 + num * 120), 135f, 50f, 24f), text, style);
					GUI.DrawTexture(new Rect((float)(85 + num * 120), 136f, (float)texture2D.width, (float)texture2D.height), texture2D);
					num++;
					b2 += 1;
				}
			}
			GUI.EndGroup();
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x000F2CB8 File Offset: 0x000F0EB8
		public override void OnUpdate()
		{
			if (LeagueWindow.I.SeasonState == SeasonState.past)
			{
				this._prizes = Language.LeaguePastPrizes;
			}
			else if (LeagueWindow.I.SeasonState == SeasonState.future)
			{
				this._prizes = Language.LeagueFuturePrizes;
			}
			else
			{
				this._prizes = Language.LeagueCurrentPrizes;
			}
		}

		// Token: 0x04001FE0 RID: 8160
		private Texture2D _back;

		// Token: 0x04001FE1 RID: 8161
		private readonly Texture2D[] _medals = new Texture2D[3];

		// Token: 0x04001FE2 RID: 8162
		private string _prizes;

		// Token: 0x04001FE3 RID: 8163
		private List<int> prizesAmount = new List<int>();

		// Token: 0x04001FE4 RID: 8164
		private List<bool> prizesCurrency = new List<bool>();

		// Token: 0x04001FE5 RID: 8165
		private List<string> prizesPlace = new List<string>();
	}
}
