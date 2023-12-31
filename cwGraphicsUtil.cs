using System;
using UnityEngine;

// Token: 0x020002E5 RID: 741
public class cwGraphicsUtil
{
	// Token: 0x17000319 RID: 793
	// (get) Token: 0x06001484 RID: 5252 RVA: 0x000D8DE4 File Offset: 0x000D6FE4
	public static Texture2D Empty
	{
		get
		{
			if (cwGraphicsUtil.empty == null)
			{
				cwGraphicsUtil.empty = (Resources.LoadAssetAtPath("Assets\\Textures\\GUI\\Editor\\empty.png", typeof(Texture2D)) as Texture2D);
			}
			return cwGraphicsUtil.empty;
		}
	}

	// Token: 0x1700031A RID: 794
	// (get) Token: 0x06001485 RID: 5253 RVA: 0x000D8E1C File Offset: 0x000D701C
	public static Texture2D Resize
	{
		get
		{
			if (cwGraphicsUtil.resize == null)
			{
				cwGraphicsUtil.resize = (Resources.LoadAssetAtPath("Assets\\Textures\\GUI\\Editor\\resize.png", typeof(Texture2D)) as Texture2D);
			}
			return cwGraphicsUtil.resize;
		}
	}

	// Token: 0x1700031B RID: 795
	// (get) Token: 0x06001486 RID: 5254 RVA: 0x000D8E54 File Offset: 0x000D7054
	public static Texture2D BW
	{
		get
		{
			if (cwGraphicsUtil.bw == null)
			{
				cwGraphicsUtil.bw = (Resources.LoadAssetAtPath("Assets\\Textures\\GUI\\Editor\\bw.tga", typeof(Texture2D)) as Texture2D);
			}
			return cwGraphicsUtil.bw;
		}
	}

	// Token: 0x1700031C RID: 796
	// (get) Token: 0x06001487 RID: 5255 RVA: 0x000D8E8C File Offset: 0x000D708C
	public static Texture2D Frame
	{
		get
		{
			if (cwGraphicsUtil.frame == null)
			{
				cwGraphicsUtil.frame = (Resources.LoadAssetAtPath("Assets\\Textures\\GUI\\Editor\\frame.png", typeof(Texture2D)) as Texture2D);
			}
			return cwGraphicsUtil.frame;
		}
	}

	// Token: 0x1700031D RID: 797
	// (get) Token: 0x06001488 RID: 5256 RVA: 0x000D8EC4 File Offset: 0x000D70C4
	public static Texture2D White
	{
		get
		{
			if (cwGraphicsUtil.white == null)
			{
				cwGraphicsUtil.white = (Resources.LoadAssetAtPath("Assets\\Textures\\GUI\\Editor\\white.png", typeof(Texture2D)) as Texture2D);
			}
			return cwGraphicsUtil.white;
		}
	}

	// Token: 0x1700031E RID: 798
	// (get) Token: 0x06001489 RID: 5257 RVA: 0x000D8EFC File Offset: 0x000D70FC
	public static Texture2D Prefab
	{
		get
		{
			if (cwGraphicsUtil.prefab == null)
			{
				cwGraphicsUtil.prefab = (Resources.LoadAssetAtPath("Assets\\Textures\\GUI\\Editor\\pool_item.png", typeof(Texture2D)) as Texture2D);
			}
			return cwGraphicsUtil.prefab;
		}
	}

	// Token: 0x1700031F RID: 799
	// (get) Token: 0x0600148A RID: 5258 RVA: 0x000D8F34 File Offset: 0x000D7134
	public static Texture2D Pool
	{
		get
		{
			if (cwGraphicsUtil.pool == null)
			{
				cwGraphicsUtil.pool = (Resources.LoadAssetAtPath("Assets\\Textures\\GUI\\Editor\\pool.png", typeof(Texture2D)) as Texture2D);
			}
			return cwGraphicsUtil.pool;
		}
	}

	// Token: 0x17000320 RID: 800
	// (get) Token: 0x0600148B RID: 5259 RVA: 0x000D8F6C File Offset: 0x000D716C
	public static GUIStyle editorBack
	{
		get
		{
			if (cwGraphicsUtil.style == null)
			{
				cwGraphicsUtil.style = new GUIStyle();
				cwGraphicsUtil.style.normal.background = cwGraphicsUtil.White;
			}
			return cwGraphicsUtil.style;
		}
	}

	// Token: 0x04001944 RID: 6468
	private static Texture2D empty;

	// Token: 0x04001945 RID: 6469
	private static Texture2D resize;

	// Token: 0x04001946 RID: 6470
	private static Texture2D bw;

	// Token: 0x04001947 RID: 6471
	private static Texture2D frame;

	// Token: 0x04001948 RID: 6472
	private static Texture2D white;

	// Token: 0x04001949 RID: 6473
	private static Texture2D prefab;

	// Token: 0x0400194A RID: 6474
	private static Texture2D pool;

	// Token: 0x0400194B RID: 6475
	private static GUIStyle style;
}
