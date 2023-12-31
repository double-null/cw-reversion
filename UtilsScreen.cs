using System;
using UnityEngine;

// Token: 0x02000382 RID: 898
public static class UtilsScreen
{
	// Token: 0x06001CF7 RID: 7415 RVA: 0x000FFD1C File Offset: 0x000FDF1C
	// Note: this type is marked as 'beforefieldinit'.
	static UtilsScreen()
	{
		UtilsScreen.OnScreenChange = delegate()
		{
		};
	}

	// Token: 0x14000008 RID: 8
	// (add) Token: 0x06001CF8 RID: 7416 RVA: 0x000FFD4C File Offset: 0x000FDF4C
	// (remove) Token: 0x06001CF9 RID: 7417 RVA: 0x000FFD64 File Offset: 0x000FDF64
	public static event Action OnScreenChange;

	// Token: 0x17000837 RID: 2103
	// (get) Token: 0x06001CFA RID: 7418 RVA: 0x000FFD7C File Offset: 0x000FDF7C
	// (set) Token: 0x06001CFB RID: 7419 RVA: 0x000FFD84 File Offset: 0x000FDF84
	public static Vector2 Size { get; private set; }

	// Token: 0x17000838 RID: 2104
	// (get) Token: 0x06001CFC RID: 7420 RVA: 0x000FFD8C File Offset: 0x000FDF8C
	// (set) Token: 0x06001CFD RID: 7421 RVA: 0x000FFD94 File Offset: 0x000FDF94
	public static Rect Rect { get; private set; }

	// Token: 0x17000839 RID: 2105
	// (get) Token: 0x06001CFE RID: 7422 RVA: 0x000FFD9C File Offset: 0x000FDF9C
	// (set) Token: 0x06001CFF RID: 7423 RVA: 0x000FFDA4 File Offset: 0x000FDFA4
	public static int Width { get; private set; }

	// Token: 0x1700083A RID: 2106
	// (get) Token: 0x06001D00 RID: 7424 RVA: 0x000FFDAC File Offset: 0x000FDFAC
	// (set) Token: 0x06001D01 RID: 7425 RVA: 0x000FFDB4 File Offset: 0x000FDFB4
	public static int Height { get; private set; }

	// Token: 0x06001D02 RID: 7426 RVA: 0x000FFDBC File Offset: 0x000FDFBC
	public static void Check()
	{
		if (UtilsScreen.Width != Screen.width || UtilsScreen.Height != Screen.height)
		{
			UtilsScreen.Width = Screen.width;
			UtilsScreen.Height = Screen.height;
			UtilsScreen.Size = new Vector2((float)UtilsScreen.Width, (float)UtilsScreen.Height);
			UtilsScreen.Rect = new Rect(0f, 0f, (float)UtilsScreen.Width, (float)UtilsScreen.Height);
			UtilsScreen.OnScreenChange();
		}
	}
}
