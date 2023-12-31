using System;
using UnityEngine;

namespace LeagueGUI
{
	// Token: 0x02000318 RID: 792
	internal class ResultBodyFrame : AbstractFrame
	{
		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06001AE7 RID: 6887 RVA: 0x000F3528 File Offset: 0x000F1728
		private Rect BodyRect
		{
			get
			{
				return new Rect((float)(Screen.width - 800) * 0.5f + 4f, (float)(Screen.height - 600) * 0.5f + 180f, 792f, 415f);
			}
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x000F3574 File Offset: 0x000F1774
		public override void OnStart()
		{
			int num = 10;
			int num2 = 10;
			LeagueWindow.I.Lists.ResultList.Clear();
			LeagueWindow.I.Lists.ResultList.Add(new ResultTable(ClientLeagueSystem.MatchEndData));
			LeagueWindow.I.Lists.ResultList.FillDensity = (float)((num + num2) * 27 + 120 + 30);
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x000F35DC File Offset: 0x000F17DC
		public override void OnGUI()
		{
			GUI.BeginGroup(this.BodyRect);
			GUI.DrawTexture(new Rect(0f, 0f, this.BodyRect.width, this.BodyRect.height), LeagueWindow.I.Textures.Gray);
			LeagueWindow.I.Lists.ResultList.OnGUI();
			GUI.EndGroup();
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x000F364C File Offset: 0x000F184C
		public override void OnUpdate()
		{
		}
	}
}
