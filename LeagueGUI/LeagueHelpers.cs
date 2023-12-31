using System;
using UnityEngine;

namespace LeagueGUI
{
	// Token: 0x0200032C RID: 812
	public static class LeagueHelpers
	{
		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06001B70 RID: 7024 RVA: 0x000F7604 File Offset: 0x000F5804
		// (set) Token: 0x06001B71 RID: 7025 RVA: 0x000F763C File Offset: 0x000F583C
		public static int[] Ranks
		{
			private get
			{
				if (LeagueHelpers._ranks == null || LeagueHelpers._ranks.Length != LeagueHelpers.DefaultRanks.Length)
				{
					return LeagueHelpers.DefaultRanks;
				}
				return LeagueHelpers._ranks;
			}
			set
			{
				LeagueHelpers._ranks = value;
			}
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x000F7644 File Offset: 0x000F5844
		public static void DrawRank(float x, float y, int lp_points, float colorAlpha = 1f, bool skipLeagueAlpha = false)
		{
			if (lp_points >= LeagueHelpers.Ranks[LeagueHelpers.Ranks.Length - 1])
			{
				GUI.DrawTexture(new Rect(x, y, (float)LeagueWindow.I.Textures.Champion.width, (float)LeagueWindow.I.Textures.Champion.height), LeagueWindow.I.Textures.Champion);
			}
			else
			{
				Color color = GUI.color;
				GUI.color = LeagueWindow.I.RankColor[LeagueHelpers.GetRankColorIndex(lp_points)];
				if (!skipLeagueAlpha)
				{
					GUI.color = Colors.alpha(GUI.color, LeagueWindow.I.LeagueAlpha.visibility * colorAlpha);
				}
				GUI.DrawTexture(new Rect(x, y, (float)LeagueWindow.I.Textures.Rank.width, (float)LeagueWindow.I.Textures.Rank.height), LeagueWindow.I.Textures.Rank);
				GUI.color = color;
				GUI.Label(new Rect(x, y + 1f, (float)LeagueWindow.I.Textures.Rank.width, (float)LeagueWindow.I.Textures.Rank.height), LeagueHelpers.GetRankLabel(lp_points), LeagueWindow.I.Styles.WhiteLabel14);
			}
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x000F7798 File Offset: 0x000F5998
		private static string GetRankLabel(int lp_points)
		{
			byte b = 0;
			while ((int)b < LeagueHelpers.Ranks.Length)
			{
				if (lp_points < LeagueHelpers.Ranks[(int)b])
				{
					return LeagueHelpers.RankLabels[(int)b];
				}
				b += 1;
			}
			return LeagueHelpers.RankLabels[0];
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x000F77DC File Offset: 0x000F59DC
		private static int GetRankColorIndex(int lp_points)
		{
			int result = 0;
			if (lp_points > LeagueHelpers.Ranks[0])
			{
				result = 1;
			}
			if (lp_points > LeagueHelpers.Ranks[3])
			{
				result = 2;
			}
			if (lp_points > LeagueHelpers.Ranks[6])
			{
				result = 3;
			}
			if (lp_points > LeagueHelpers.Ranks[9])
			{
				result = 4;
			}
			if (lp_points > LeagueHelpers.Ranks[11])
			{
				result = 5;
			}
			return result;
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x000F783C File Offset: 0x000F5A3C
		public static void Twister()
		{
			float x = LeagueWindow.I.Rects.TwisterTextureRect.xMin + (float)LeagueWindow.I.Textures.Twister.width * 0.5f;
			float y = LeagueWindow.I.Rects.TwisterTextureRect.yMin + (float)LeagueWindow.I.Textures.Twister.height * 0.5f;
			GUIUtility.RotateAroundPivot(180f * LeagueWindow.Timer.Time, new Vector2(x, y));
			GUI.DrawTexture(LeagueWindow.I.Rects.TwisterTextureRect, LeagueWindow.I.Textures.Twister);
			GUIUtility.RotateAroundPivot(-180f * LeagueWindow.Timer.Time, new Vector2(x, y));
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x000F7908 File Offset: 0x000F5B08
		public static int GetNextRank(int currentRank)
		{
			for (int i = 0; i < LeagueHelpers.Ranks.Length; i++)
			{
				if (currentRank < LeagueHelpers.Ranks[i])
				{
					return LeagueHelpers.Ranks[i];
				}
			}
			return 0;
		}

		// Token: 0x04002043 RID: 8259
		private static readonly string[] RankLabels = new string[]
		{
			"L",
			"D-",
			"D",
			"D+",
			"C-",
			"C",
			"C+",
			"B-",
			"B",
			"B+",
			"A-",
			"A",
			"A+"
		};

		// Token: 0x04002044 RID: 8260
		private static readonly int[] DefaultRanks = new int[]
		{
			399,
			899,
			1999,
			2999,
			3999,
			4999,
			5999,
			6999,
			7999,
			8999,
			10499,
			11999,
			14999,
			18499
		};

		// Token: 0x04002045 RID: 8261
		private static int[] _ranks;
	}
}
