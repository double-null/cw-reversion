using System;
using UnityEngine;

// Token: 0x02000252 RID: 594
public class Colors
{
	// Token: 0x1700026C RID: 620
	// (get) Token: 0x06001225 RID: 4645 RVA: 0x000C7C68 File Offset: 0x000C5E68
	public static Color consoleServerCmds
	{
		get
		{
			return new Color(0.7f, 1f, 0.7f);
		}
	}

	// Token: 0x1700026D RID: 621
	// (get) Token: 0x06001226 RID: 4646 RVA: 0x000C7C80 File Offset: 0x000C5E80
	public static Color consoleUnityLog
	{
		get
		{
			return new Color(0.5f, 0.8f, 1f);
		}
	}

	// Token: 0x1700026E RID: 622
	// (get) Token: 0x06001227 RID: 4647 RVA: 0x000C7C98 File Offset: 0x000C5E98
	public static Color RadarWhite
	{
		get
		{
			return new Color(1f, 1f, 1f);
		}
	}

	// Token: 0x1700026F RID: 623
	// (get) Token: 0x06001228 RID: 4648 RVA: 0x000C7CB0 File Offset: 0x000C5EB0
	public static Color RadarYellow
	{
		get
		{
			return new Color(1f, 0.682f, 0f);
		}
	}

	// Token: 0x17000270 RID: 624
	// (get) Token: 0x06001229 RID: 4649 RVA: 0x000C7CC8 File Offset: 0x000C5EC8
	public static Color RadarGreen
	{
		get
		{
			return new Color(0.427f, 0.686f, 0.027f);
		}
	}

	// Token: 0x17000271 RID: 625
	// (get) Token: 0x0600122A RID: 4650 RVA: 0x000C7CE0 File Offset: 0x000C5EE0
	public static Color RadarRed
	{
		get
		{
			return new Color(0.678f, 0.019f, 0.019f);
		}
	}

	// Token: 0x17000272 RID: 626
	// (get) Token: 0x0600122B RID: 4651 RVA: 0x000C7CF8 File Offset: 0x000C5EF8
	public static Color RadarBlue
	{
		get
		{
			return new Color(0.146f, 0.197f, 0.255f);
		}
	}

	// Token: 0x17000273 RID: 627
	// (get) Token: 0x0600122C RID: 4652 RVA: 0x000C7D10 File Offset: 0x000C5F10
	public static Color RadarGold
	{
		get
		{
			return new Color(0.255f, 0.201f, 0.041f);
		}
	}

	// Token: 0x17000274 RID: 628
	// (get) Token: 0x0600122D RID: 4653 RVA: 0x000C7D28 File Offset: 0x000C5F28
	public static Color Chat
	{
		get
		{
			return new Color(0.8f, 0.87f, 0.9f);
		}
	}

	// Token: 0x17000275 RID: 629
	// (get) Token: 0x0600122E RID: 4654 RVA: 0x000C7D40 File Offset: 0x000C5F40
	public static Color ChatYouTalk
	{
		get
		{
			return new Color(0.9f, 0.5f, 0.5f);
		}
	}

	// Token: 0x0600122F RID: 4655 RVA: 0x000C7D58 File Offset: 0x000C5F58
	public static Color alpha(Color color, float alpha)
	{
		return new Color(color.r, color.g, color.b, alpha);
	}

	// Token: 0x040011CE RID: 4558
	public static string RadarWhiteWeb = "#FFFFFF";

	// Token: 0x040011CF RID: 4559
	public static string RadarGrayWeb = "#AAAAAA";

	// Token: 0x040011D0 RID: 4560
	public static string RadarYellowWeb = "#FFAE00";

	// Token: 0x040011D1 RID: 4561
	public static string RadarGreenWeb = "#6daf07";

	// Token: 0x040011D2 RID: 4562
	public static string RadarRedWeb = "#ad0505";

	// Token: 0x040011D3 RID: 4563
	public static string RadarBlueWeb = "#62aeea";

	// Token: 0x040011D4 RID: 4564
	public static string standartGrey = "#a1a1a1";

	// Token: 0x040011D5 RID: 4565
	public static string standartBlueGary = "#b9c3d0";

	// Token: 0x040011D6 RID: 4566
	public static string greyDDDDDD = "#DDDDDD";
}
