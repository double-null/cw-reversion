using System;

// Token: 0x0200007E RID: 126
[Serializable]
public class MonoEvented : PoolableBehaviour
{
	// Token: 0x1700004A RID: 74
	// (get) Token: 0x060002AB RID: 683 RVA: 0x00014EB4 File Offset: 0x000130B4
	// (set) Token: 0x060002AC RID: 684 RVA: 0x00014EBC File Offset: 0x000130BC
	public virtual bool isRendering { get; set; }

	// Token: 0x1700004B RID: 75
	// (get) Token: 0x060002AD RID: 685 RVA: 0x00014EC8 File Offset: 0x000130C8
	// (set) Token: 0x060002AE RID: 686 RVA: 0x00014ED0 File Offset: 0x000130D0
	public virtual bool isUpdating { get; set; }

	// Token: 0x1700004C RID: 76
	// (get) Token: 0x060002AF RID: 687 RVA: 0x00014EDC File Offset: 0x000130DC
	// (set) Token: 0x060002B0 RID: 688 RVA: 0x00014EE4 File Offset: 0x000130E4
	public virtual bool isGameHandler { get; set; }

	// Token: 0x1700004D RID: 77
	// (get) Token: 0x060002B1 RID: 689 RVA: 0x00014EF0 File Offset: 0x000130F0
	// (set) Token: 0x060002B2 RID: 690 RVA: 0x00014EF8 File Offset: 0x000130F8
	public virtual int WindowID { get; set; }

	// Token: 0x060002B3 RID: 691 RVA: 0x00014F04 File Offset: 0x00013104
	public virtual void MainInitialize()
	{
		this.Register();
	}

	// Token: 0x060002B4 RID: 692 RVA: 0x00014F0C File Offset: 0x0001310C
	public virtual void Clear()
	{
	}

	// Token: 0x060002B5 RID: 693 RVA: 0x00014F10 File Offset: 0x00013110
	public virtual void Register()
	{
	}

	// Token: 0x060002B6 RID: 694 RVA: 0x00014F14 File Offset: 0x00013114
	public virtual void OnInitialized()
	{
	}

	// Token: 0x060002B7 RID: 695 RVA: 0x00014F18 File Offset: 0x00013118
	public virtual void OnQuit()
	{
	}

	// Token: 0x060002B8 RID: 696 RVA: 0x00014F1C File Offset: 0x0001311C
	public virtual void OnDestroy()
	{
	}

	// Token: 0x060002B9 RID: 697 RVA: 0x00014F20 File Offset: 0x00013120
	public virtual void OnEnable()
	{
	}

	// Token: 0x060002BA RID: 698 RVA: 0x00014F24 File Offset: 0x00013124
	public virtual void OnLevelLoaded()
	{
	}

	// Token: 0x060002BB RID: 699 RVA: 0x00014F28 File Offset: 0x00013128
	public virtual void OnConnected()
	{
	}

	// Token: 0x060002BC RID: 700 RVA: 0x00014F2C File Offset: 0x0001312C
	public virtual void OnLevelUnloaded()
	{
	}

	// Token: 0x060002BD RID: 701 RVA: 0x00014F30 File Offset: 0x00013130
	public virtual void OnDisconnect()
	{
	}

	// Token: 0x060002BE RID: 702 RVA: 0x00014F34 File Offset: 0x00013134
	public virtual void MasterGUI()
	{
	}

	// Token: 0x060002BF RID: 703 RVA: 0x00014F38 File Offset: 0x00013138
	public virtual void GameGUI()
	{
	}

	// Token: 0x060002C0 RID: 704 RVA: 0x00014F3C File Offset: 0x0001313C
	public virtual void InterfaceGUI()
	{
	}

	// Token: 0x060002C1 RID: 705 RVA: 0x00014F40 File Offset: 0x00013140
	public virtual void PreGUI()
	{
	}

	// Token: 0x060002C2 RID: 706 RVA: 0x00014F44 File Offset: 0x00013144
	public virtual void LateGUI()
	{
	}

	// Token: 0x060002C3 RID: 707 RVA: 0x00014F48 File Offset: 0x00013148
	public virtual void OnUpdate()
	{
	}

	// Token: 0x060002C4 RID: 708 RVA: 0x00014F4C File Offset: 0x0001314C
	public virtual void OnFixedUpdate()
	{
	}

	// Token: 0x060002C5 RID: 709 RVA: 0x00014F50 File Offset: 0x00013150
	public virtual void OnLateUpdate()
	{
	}

	// Token: 0x060002C6 RID: 710 RVA: 0x00014F54 File Offset: 0x00013154
	public virtual void OnSpawn()
	{
	}

	// Token: 0x060002C7 RID: 711 RVA: 0x00014F58 File Offset: 0x00013158
	public virtual void OnDie()
	{
	}

	// Token: 0x060002C8 RID: 712 RVA: 0x00014F5C File Offset: 0x0001315C
	public virtual void OnRoundStart()
	{
	}

	// Token: 0x060002C9 RID: 713 RVA: 0x00014F60 File Offset: 0x00013160
	public virtual void OnRoundEnd()
	{
	}

	// Token: 0x060002CA RID: 714 RVA: 0x00014F64 File Offset: 0x00013164
	public virtual void OnMatchStart()
	{
	}

	// Token: 0x060002CB RID: 715 RVA: 0x00014F68 File Offset: 0x00013168
	public virtual void OnMatchEnd()
	{
	}
}
