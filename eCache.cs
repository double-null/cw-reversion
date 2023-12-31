using System;
using UnityEngine;

// Token: 0x020000C2 RID: 194
[AddComponentMenu("Scripts/Engine/CacheFactory")]
internal class eCache : SingletoneComponent<eCache>
{
	// Token: 0x170000B6 RID: 182
	// (get) Token: 0x06000507 RID: 1287 RVA: 0x00020A84 File Offset: 0x0001EC84
	// (set) Token: 0x06000508 RID: 1288 RVA: 0x00020A90 File Offset: 0x0001EC90
	public static CWInput cwInput
	{
		get
		{
			return SingletoneComponent<eCache>.Instance.input;
		}
		set
		{
			SingletoneComponent<eCache>.Instance.input = value;
		}
	}

	// Token: 0x170000B7 RID: 183
	// (get) Token: 0x06000509 RID: 1289 RVA: 0x00020AA0 File Offset: 0x0001ECA0
	// (set) Token: 0x0600050A RID: 1290 RVA: 0x00020AAC File Offset: 0x0001ECAC
	public static HitInfo HitInfo
	{
		get
		{
			return SingletoneComponent<eCache>.Instance.hitInfo;
		}
		set
		{
			SingletoneComponent<eCache>.Instance.hitInfo = value;
		}
	}

	// Token: 0x170000B8 RID: 184
	// (get) Token: 0x0600050B RID: 1291 RVA: 0x00020ABC File Offset: 0x0001ECBC
	// (set) Token: 0x0600050C RID: 1292 RVA: 0x00020AC8 File Offset: 0x0001ECC8
	public static MoveState MoveState
	{
		get
		{
			return SingletoneComponent<eCache>.Instance.moveState;
		}
		set
		{
			SingletoneComponent<eCache>.Instance.moveState = value;
		}
	}

	// Token: 0x170000B9 RID: 185
	// (get) Token: 0x0600050D RID: 1293 RVA: 0x00020AD8 File Offset: 0x0001ECD8
	// (set) Token: 0x0600050E RID: 1294 RVA: 0x00020AE4 File Offset: 0x0001ECE4
	public static PlayerCmd PlayerCmd
	{
		get
		{
			return SingletoneComponent<eCache>.Instance.cmd;
		}
		set
		{
			SingletoneComponent<eCache>.Instance.cmd = value;
		}
	}

	// Token: 0x170000BA RID: 186
	// (get) Token: 0x0600050F RID: 1295 RVA: 0x00020AF4 File Offset: 0x0001ECF4
	// (set) Token: 0x06000510 RID: 1296 RVA: 0x00020B00 File Offset: 0x0001ED00
	public static PredictionState PredictionState
	{
		get
		{
			return SingletoneComponent<eCache>.Instance.predicted;
		}
		set
		{
			SingletoneComponent<eCache>.Instance.predicted = value;
		}
	}

	// Token: 0x170000BB RID: 187
	// (get) Token: 0x06000511 RID: 1297 RVA: 0x00020B10 File Offset: 0x0001ED10
	// (set) Token: 0x06000512 RID: 1298 RVA: 0x00020B1C File Offset: 0x0001ED1C
	public static InvokerStateKey InvokerStateKey
	{
		get
		{
			return SingletoneComponent<eCache>.Instance.invokerStateKey;
		}
		set
		{
			SingletoneComponent<eCache>.Instance.invokerStateKey = value;
		}
	}

	// Token: 0x0400048D RID: 1165
	private CWInput input = new CWInput();

	// Token: 0x0400048E RID: 1166
	private HitInfo hitInfo = new HitInfo();

	// Token: 0x0400048F RID: 1167
	private MoveState moveState = new MoveState();

	// Token: 0x04000490 RID: 1168
	private PlayerCmd cmd = new PlayerCmd();

	// Token: 0x04000491 RID: 1169
	private PredictionState predicted = new PredictionState();

	// Token: 0x04000492 RID: 1170
	private InvokerStateKey invokerStateKey = new InvokerStateKey();
}
