using System;
using System.Collections.Generic;
using ClanSystemGUI;
using LeagueSystem;

namespace LeagueGUI
{
	// Token: 0x0200031B RID: 795
	internal class ResultTable : IScrollListItem, IComparable
	{
		// Token: 0x06001AF3 RID: 6899 RVA: 0x000F3C3C File Offset: 0x000F1E3C
		public ResultTable()
		{
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x000F3C68 File Offset: 0x000F1E68
		public ResultTable(MatchJsonData matchData)
		{
			this.matchData = matchData;
			foreach (PlayerJsonData playerJsonData in matchData.players)
			{
				if (playerJsonData.team == 2)
				{
					this.usecList.Add(new ResultBar(new Player(playerJsonData)));
				}
				if (playerJsonData.team == 1)
				{
					this.bearList.Add(new ResultBar(new Player(playerJsonData)));
				}
			}
			this.usecHeader = new ResultTableHeader(false, ref matchData.usec_points, matchData.usec_points > matchData.bear_points);
			this.bearHeader = new ResultTableHeader(true, ref matchData.bear_points, matchData.bear_points > matchData.usec_points);
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06001AF5 RID: 6901 RVA: 0x000F3D7C File Offset: 0x000F1F7C
		public float Width
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06001AF6 RID: 6902 RVA: 0x000F3D84 File Offset: 0x000F1F84
		public float Height
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x000F3D8C File Offset: 0x000F1F8C
		public int CompareTo(object obj)
		{
			return 0;
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x000F3D90 File Offset: 0x000F1F90
		public void OnGUI(float x, float y, int index)
		{
			this.usecHeader.OnGUI(x, y, index);
			for (int i = 0; i < this.usecList.Count; i++)
			{
				this.usecList[i].OnGUI(x, y + this.usecHeader.Height + this.usecList[0].Height * (float)i, index);
			}
			this.bearHeader.OnGUI(x, y + this.usecHeader.Height + (float)(this.usecList.Count * 27), index);
			for (int j = 0; j < this.bearList.Count; j++)
			{
				this.bearList[j].OnGUI(x, y + this.bearList[0].Height * (float)j + this.usecList[0].Height * (float)this.usecList.Count + this.usecHeader.Height * 2f, index);
			}
		}

		// Token: 0x04001FF5 RID: 8181
		private List<ResultBar> usecList = new List<ResultBar>();

		// Token: 0x04001FF6 RID: 8182
		private List<ResultBar> bearList = new List<ResultBar>();

		// Token: 0x04001FF7 RID: 8183
		private ResultTableHeader usecHeader;

		// Token: 0x04001FF8 RID: 8184
		private ResultTableHeader bearHeader;

		// Token: 0x04001FF9 RID: 8185
		private MatchJsonData matchData = new MatchJsonData();
	}
}
