using System;
using UnityEngine;

// Token: 0x02000130 RID: 304
[Serializable]
internal class EnemyRadarPosition : Alpha
{
	// Token: 0x060007FB RID: 2043 RVA: 0x00048C7C File Offset: 0x00046E7C
	public EnemyRadarPosition(int ID)
	{
		base.Once(0.2f, 0.2f, 0.5f);
		for (int i = 0; i < Peer.ClientGame.AllPlayers.Count; i++)
		{
			if (Peer.ClientGame.AllPlayers[i].ID == ID)
			{
				this.enemy = Peer.ClientGame.AllPlayers[i];
			}
		}
		if (this.enemy)
		{
			this.position = this.enemy.Position;
			if (this.enemy.IsAlive && this.enemy.Ammo != null && this.enemy.Ammo.CurrentWeapon != null && this.enemy.Ammo.weaponEquiped && Main.UserInfo.skillUnlocked(Skills.att2))
			{
				this.weapon = (int)this.enemy.Ammo.CurrentWeapon.type;
				this.mod = this.enemy.Ammo.CurrentWeapon.state.isMod;
			}
		}
	}

	// Token: 0x170000FE RID: 254
	// (get) Token: 0x060007FC RID: 2044 RVA: 0x00048DD0 File Offset: 0x00046FD0
	public EntityNetPlayer Enemy
	{
		get
		{
			return this.enemy;
		}
	}

	// Token: 0x170000FF RID: 255
	// (get) Token: 0x060007FD RID: 2045 RVA: 0x00048DD8 File Offset: 0x00046FD8
	public Vector3 Position
	{
		get
		{
			return this.position;
		}
	}

	// Token: 0x17000100 RID: 256
	// (get) Token: 0x060007FE RID: 2046 RVA: 0x00048DE0 File Offset: 0x00046FE0
	public int Weapon
	{
		get
		{
			return this.weapon;
		}
	}

	// Token: 0x17000101 RID: 257
	// (get) Token: 0x060007FF RID: 2047 RVA: 0x00048DE8 File Offset: 0x00046FE8
	public bool Mod
	{
		get
		{
			return this.mod;
		}
	}

	// Token: 0x06000800 RID: 2048 RVA: 0x00048DF0 File Offset: 0x00046FF0
	public void UpdatePosition()
	{
		if (this.enemy)
		{
			this.position = this.enemy.Position;
		}
	}

	// Token: 0x0400087A RID: 2170
	private EntityNetPlayer enemy;

	// Token: 0x0400087B RID: 2171
	private Vector3 position = Vector3.zero;

	// Token: 0x0400087C RID: 2172
	private int weapon = 127;

	// Token: 0x0400087D RID: 2173
	private bool mod;
}
