using System;
using UnityEngine;

namespace LeagueGUI
{
	// Token: 0x0200031F RID: 799
	internal class RulesFrame : AbstractFrame
	{
		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06001B25 RID: 6949 RVA: 0x000F57F0 File Offset: 0x000F39F0
		private Rect RulesFrameRect
		{
			get
			{
				return new Rect((float)(Screen.width - 800) * 0.5f + 4f, (float)(Screen.height - 600) * 0.5f + 120f, 394f, 300f);
			}
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x000F583C File Offset: 0x000F3A3C
		public override void OnStart()
		{
			this.GenerateList();
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x000F5844 File Offset: 0x000F3A44
		public override void OnGUI()
		{
			GUI.BeginGroup(this.RulesFrameRect);
			GUI.DrawTexture(new Rect(0f, 0f, this.RulesFrameRect.width, 20f), LeagueWindow.I.Textures.Black);
			GUI.DrawTexture(new Rect(0f, 20f, this.RulesFrameRect.width, this.RulesFrameRect.height - 20f), LeagueWindow.I.Textures.Gray);
			LeagueWindow.I.Styles.BrownLabel.alignment = TextAnchor.MiddleLeft;
			GUI.Label(new Rect(5f, 4f, 50f, 14f), Language.LeagueRules, LeagueWindow.I.Styles.BrownLabel);
			LeagueWindow.I.Styles.BrownLabel.alignment = TextAnchor.MiddleCenter;
			LeagueWindow.I.Lists.RulesList.OnGUI();
			GUI.EndGroup();
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x000F594C File Offset: 0x000F3B4C
		private void GenerateList()
		{
			LeagueWindow.I.Lists.RulesList.Clear();
			LeagueWindow.I.Lists.RulesList.Add(new RulesBar());
		}
	}
}
