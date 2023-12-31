using System;
using UnityEngine;

// Token: 0x0200013E RID: 318
[Serializable]
internal class HotspotRadarState : Alpha
{
	// Token: 0x06000801 RID: 2049 RVA: 0x00048E14 File Offset: 0x00047014
	public HotspotRadarState(int id, float distance)
	{
		this._timer.Start();
		base.Once(0.1f, 1f + UnityEngine.Random.value, 0.1f);
		foreach (EntityNetPlayer entityNetPlayer in Peer.ClientGame.AllPlayers)
		{
			if (entityNetPlayer.ID == id)
			{
				this.Enemy = entityNetPlayer;
			}
		}
		if (!this.Enemy)
		{
			return;
		}
		this._position = this.Enemy.Position;
		if (this.Enemy.IsAlive && this.Enemy.Ammo.weaponEquiped)
		{
			ClientWeapon clientWeapon = this.Enemy.Ammo.CurrentWeapon as ClientWeapon;
			if (clientWeapon && clientWeapon.BulletHolder)
			{
				this._position = clientWeapon.BulletHolder.position;
			}
		}
		this.Distance = distance;
	}

	// Token: 0x17000102 RID: 258
	// (get) Token: 0x06000802 RID: 2050 RVA: 0x00048F64 File Offset: 0x00047164
	// (set) Token: 0x06000803 RID: 2051 RVA: 0x00048F6C File Offset: 0x0004716C
	public float Distance { get; private set; }

	// Token: 0x17000103 RID: 259
	// (get) Token: 0x06000804 RID: 2052 RVA: 0x00048F78 File Offset: 0x00047178
	// (set) Token: 0x06000805 RID: 2053 RVA: 0x00048F80 File Offset: 0x00047180
	public EntityNetPlayer Enemy { get; private set; }

	// Token: 0x17000104 RID: 260
	// (get) Token: 0x06000806 RID: 2054 RVA: 0x00048F8C File Offset: 0x0004718C
	public Vector3 Position
	{
		get
		{
			return this._position;
		}
	}

	// Token: 0x17000105 RID: 261
	// (get) Token: 0x06000807 RID: 2055 RVA: 0x00048F94 File Offset: 0x00047194
	public float Elapsed
	{
		get
		{
			return this._timer.Elapsed;
		}
	}

	// Token: 0x040008FF RID: 2303
	private readonly Vector3 _position = Vector3.zero;

	// Token: 0x04000900 RID: 2304
	private readonly eTimer _timer = new eTimer();
}
