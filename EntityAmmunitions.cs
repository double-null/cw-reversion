using System;
using System.Reflection;
using UnityEngine;

// Token: 0x020001E4 RID: 484
[AddComponentMenu("Scripts/Game/EntityAmmunitions")]
internal class EntityAmmunitions : ClientAmmunitions
{
	// Token: 0x06000FFF RID: 4095 RVA: 0x000B4E6C File Offset: 0x000B306C
	public override void OnPoolDespawn()
	{
		this.locked = false;
		Transform transform = Utility.FindHierarchy(base.transform, "knife");
		if (transform)
		{
			transform.renderer.enabled = false;
		}
		base.OnPoolDespawn();
	}

	// Token: 0x06001000 RID: 4096 RVA: 0x000B4EB0 File Offset: 0x000B30B0
	[Obfuscation(Exclude = true)]
	public override void Toggle()
	{
		if (base.BreakableGroup || this.useBinocular)
		{
			return;
		}
		if (this.state.supportReady)
		{
			return;
		}
		if (this.G.IsInvoking("Toggle"))
		{
			return;
		}
		if (this.toggleTimer > Time.time)
		{
			return;
		}
		if (!base.weaponEquiped)
		{
			return;
		}
		if (base.weaponEquiped && base.CurrentWeapon != null)
		{
			Audio.Play(base.GetComponent<PoolItem>(), SingletoneForm<SoundFactory>.Instance.ChangeWeaponSound, true, CVars.pl_minStepDistance, CVars.pl_maxStepDistance);
		}
	}

	// Token: 0x06001001 RID: 4097 RVA: 0x000B4F54 File Offset: 0x000B3154
	[Obfuscation(Exclude = true)]
	protected override void AfterToggleIdleOut()
	{
		this.OutIdle();
	}

	// Token: 0x06001002 RID: 4098 RVA: 0x000B4F5C File Offset: 0x000B315C
	public override void Cancel()
	{
		this.useBinocular = false;
		this.G.CancelInvoke();
		if (this.cSecondary)
		{
			this.cSecondary.Cancel();
		}
		if (this.cPrimary)
		{
			this.cPrimary.Cancel();
		}
	}

	// Token: 0x06001003 RID: 4099 RVA: 0x000B4FB4 File Offset: 0x000B31B4
	public override void Knife()
	{
		if (base.BreakableGroup || this.useBinocular)
		{
			return;
		}
		base.Knife();
		this.player.AnimationsThird.PlayKnife(0.9f);
	}

	// Token: 0x06001004 RID: 4100 RVA: 0x000B4FF4 File Offset: 0x000B31F4
	public override void Show()
	{
		if (this.locked)
		{
			return;
		}
		if (this.cPrimary)
		{
			this.cPrimary.audio.mute = false;
			(this.cPrimary as ClientWeapon).StopLoopSound();
		}
		if (this.cSecondary)
		{
			this.cSecondary.audio.mute = false;
			(this.cSecondary as ClientWeapon).StopLoopSound();
		}
		base.Show();
	}

	// Token: 0x06001005 RID: 4101 RVA: 0x000B5078 File Offset: 0x000B3278
	protected override void OnPlayReload()
	{
		this.player.AnimationsThird.PlayReload(base.CurrentWeapon.ReloadTime);
		Audio.Play(base.GetComponent<PoolItem>(), SingletoneForm<SoundFactory>.Instance.ReloadSound[0], true, CVars.pl_minStepDistance, CVars.pl_maxStepDistance);
		this.G.Invoke("OnPlayReloadEnd", 1.5f, false);
	}

	// Token: 0x06001006 RID: 4102 RVA: 0x000B50D8 File Offset: 0x000B32D8
	protected override void OnPlayReloadEnd()
	{
		Audio.Play(base.GetComponent<PoolItem>(), SingletoneForm<SoundFactory>.Instance.ReloadSound[1], true, CVars.pl_minStepDistance, CVars.pl_maxStepDistance);
	}

	// Token: 0x06001007 RID: 4103 RVA: 0x000B5108 File Offset: 0x000B3308
	[Obfuscation(Exclude = true)]
	public override void Binoculars()
	{
		if (base.BreakableGroup || !this.CanUseBinocular)
		{
			return;
		}
		if (this.state.supportReady)
		{
			return;
		}
		if (this.state.equiped == WeaponEquipedState.Marker)
		{
			return;
		}
		if (this.player == Peer.ClientGame.LocalPlayer)
		{
			return;
		}
		this.useBinocular = !this.useBinocular;
	}

	// Token: 0x06001008 RID: 4104 RVA: 0x000B517C File Offset: 0x000B337C
	public override void CallLateUpdate()
	{
		base.CallLateUpdate();
	}

	// Token: 0x06001009 RID: 4105 RVA: 0x000B5184 File Offset: 0x000B3384
	protected override void OnPlayPompovikReload()
	{
		this.player.AnimationsThird.PlayReload(base.CurrentWeapon.ReloadTime);
		Audio.Play(base.GetComponent<PoolItem>(), SingletoneForm<SoundFactory>.Instance.ReloadSound[0], true, CVars.pl_minStepDistance, CVars.pl_maxStepDistance);
		this.G.Invoke("OnPlayReloadEnd", 1.5f, false);
	}

	// Token: 0x0600100A RID: 4106 RVA: 0x000B51E4 File Offset: 0x000B33E4
	[Obfuscation(Exclude = true)]
	public override void Grenade()
	{
		if (base.BreakableGroup || this.state.supportReady || this.useBinocular || this.G.IsInvoking("AfterAimIdle") || this.state.grenadeCount == 0)
		{
			return;
		}
		this.state.grenadeCount--;
		this.OnGrenade();
	}

	// Token: 0x0600100B RID: 4107 RVA: 0x000B5258 File Offset: 0x000B3458
	[Obfuscation(Exclude = true)]
	protected override void AfterSupport()
	{
		base.AfterSupport();
	}

	// Token: 0x0600100C RID: 4108 RVA: 0x000B5260 File Offset: 0x000B3460
	protected override void OnGrenade()
	{
		this.player.AnimationsThird.PlayGrenade(0.3f);
	}

	// Token: 0x0600100D RID: 4109 RVA: 0x000B5278 File Offset: 0x000B3478
	protected override void OnMortarStrike()
	{
		this.player.AnimationsThird.PlayGrenade(0.4f);
		base.OnMortarStrike();
	}

	// Token: 0x0600100E RID: 4110 RVA: 0x000B5298 File Offset: 0x000B3498
	protected override void OnSupportFire()
	{
		this.player.AnimationsThird.PlayGrenade(0.4f);
		base.OnSupportFire();
	}

	// Token: 0x04001077 RID: 4215
	public bool locked;

	// Token: 0x04001078 RID: 4216
	private float toggleTimer;
}
