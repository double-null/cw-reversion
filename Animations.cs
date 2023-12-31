using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000243 RID: 579
[AddComponentMenu("Scripts/Game/Foundation/Animations")]
internal class Animations : PoolableBehaviour
{
	// Token: 0x17000263 RID: 611
	// (get) Token: 0x060011C9 RID: 4553 RVA: 0x000C5C60 File Offset: 0x000C3E60
	public Animation handsAnimation
	{
		get
		{
			return this.general2d;
		}
	}

	// Token: 0x060011CA RID: 4554 RVA: 0x000C5C68 File Offset: 0x000C3E68
	public override void OnPoolDespawn()
	{
		this.Arms = null;
		this.general2d = null;
		this.prename = string.Empty;
		this.fireRand = new CircleRandom();
		this.fireAimRand = new CircleRandom();
		base.OnPoolDespawn();
	}

	// Token: 0x060011CB RID: 4555 RVA: 0x000C5CA0 File Offset: 0x000C3EA0
	public void Init()
	{
		this.general2d = base.transform.FindChild("proxy/hands").GetComponent<Animation>();
		this.general2d.Stop();
		this.Arms = base.transform.FindChild("proxy/hands/Arms").gameObject;
	}

	// Token: 0x060011CC RID: 4556 RVA: 0x000C5CF0 File Offset: 0x000C3EF0
	public void SetWeapon(int maxFire, int maxAimFire)
	{
		this.fireRand.InitNew(maxFire - 1);
		this.fireAimRand.InitNew(maxAimFire - 1);
	}

	// Token: 0x060011CD RID: 4557 RVA: 0x000C5D10 File Offset: 0x000C3F10
	public void Knife(float time)
	{
		this.general2d["knife"].speed = this.general2d["knife"].length / time;
		this.general2d.Play("knife");
	}

	// Token: 0x060011CE RID: 4558 RVA: 0x000C5D5C File Offset: 0x000C3F5C
	public void Grenade()
	{
		this.general2d.Play("grenade");
	}

	// Token: 0x060011CF RID: 4559 RVA: 0x000C5D70 File Offset: 0x000C3F70
	public void CrossFade(string s, float fade = 0.05f)
	{
		this.general2d.CrossFade(this.prename + s, fade);
	}

	// Token: 0x060011D0 RID: 4560 RVA: 0x000C5D8C File Offset: 0x000C3F8C
	public AnimationState Get(string s)
	{
		return this.general2d[this.prename + s];
	}

	// Token: 0x060011D1 RID: 4561 RVA: 0x000C5DA8 File Offset: 0x000C3FA8
	public void SetPreName(string n)
	{
		this.prename = n + "_";
	}

	// Token: 0x060011D2 RID: 4562 RVA: 0x000C5DBC File Offset: 0x000C3FBC
	public void PlayFire(Action<float> boltDistort = null)
	{
		this.general2d.Stop();
		string text = this.prename + "fire" + this.fireRand.GetI();
		this.general2d.Play(text);
		if (boltDistort != null)
		{
			base.StartCoroutine(this.DelayedBoltDistort(this.general2d[text].length, boltDistort));
		}
	}

	// Token: 0x060011D3 RID: 4563 RVA: 0x000C5E28 File Offset: 0x000C4028
	private IEnumerator DelayedBoltDistort(float time, Action<float> boltDistort)
	{
		yield return new WaitForSeconds(time);
		if (this.general2d.IsPlaying("knife"))
		{
			yield break;
		}
		this.general2d.Stop();
		this.general2d.Play(this.prename + "bolt");
		boltDistort(0f);
		yield break;
	}

	// Token: 0x060011D4 RID: 4564 RVA: 0x000C5E60 File Offset: 0x000C4060
	public void PlayBoltDistort()
	{
		this.general2d.Stop();
		this.general2d.Play(this.prename + "bolt");
	}

	// Token: 0x060011D5 RID: 4565 RVA: 0x000C5E8C File Offset: 0x000C408C
	public void PlayFireSince(float normalizeTime)
	{
		this.general2d.Stop();
		int i = this.fireRand.GetI();
		this.general2d[this.prename + "fire" + i].normalizedTime = normalizeTime;
		this.general2d.Play(this.prename + "fire" + i);
	}

	// Token: 0x060011D6 RID: 4566 RVA: 0x000C5EFC File Offset: 0x000C40FC
	public void PlayExtraFire()
	{
		this.general2d.Stop();
		this.general2d.Play(string.Concat(new object[]
		{
			this.prename,
			"fire",
			this.fireRand.GetI(),
			"_extra"
		}));
	}

	// Token: 0x060011D7 RID: 4567 RVA: 0x000C5F58 File Offset: 0x000C4158
	public void PlayAimFire()
	{
		this.general2d.Stop();
		this.general2d.Play(this.prename + "aim_fire" + this.fireAimRand.GetNonStatic());
	}

	// Token: 0x060011D8 RID: 4568 RVA: 0x000C5F9C File Offset: 0x000C419C
	public void PlayAimOpticFire()
	{
		this.general2d.Stop();
		this.general2d.Play(string.Concat(new object[]
		{
			this.prename,
			"aim_fire",
			this.fireAimRand.GetNonStatic(),
			"_optic"
		}));
	}

	// Token: 0x060011D9 RID: 4569 RVA: 0x000C5FF8 File Offset: 0x000C41F8
	public void PlayAimExtraFire()
	{
		this.general2d.Stop();
		this.general2d.Play(string.Concat(new object[]
		{
			this.prename,
			"aim_fire",
			this.fireAimRand.GetNonStatic(),
			"_extra"
		}));
	}

	// Token: 0x060011DA RID: 4570 RVA: 0x000C6054 File Offset: 0x000C4254
	public void PlayAimOpticExtraFire()
	{
		this.general2d.Stop();
		this.general2d.Play(string.Concat(new object[]
		{
			this.prename,
			"aim_fire",
			this.fireAimRand.GetNonStatic(),
			"_optic_extra"
		}));
	}

	// Token: 0x060011DB RID: 4571 RVA: 0x000C60B0 File Offset: 0x000C42B0
	public void PlayAimOpticExtraReload()
	{
		this.general2d.Stop();
		this.general2d.Play(string.Concat(new object[]
		{
			this.prename,
			"aim_fire",
			this.fireAimRand.GetNonStatic(),
			"_optic_extra"
		}));
	}

	// Token: 0x060011DC RID: 4572 RVA: 0x000C610C File Offset: 0x000C430C
	public void PlayAimBolt()
	{
		this.general2d.Stop();
		this.general2d.Play(this.prename + "aim_bolt");
	}

	// Token: 0x060011DD RID: 4573 RVA: 0x000C6138 File Offset: 0x000C4338
	public void PlayAimOpticBolt()
	{
		this.general2d.Stop();
		this.general2d.Play(this.prename + "aim_bolt_optic");
	}

	// Token: 0x060011DE RID: 4574 RVA: 0x000C6164 File Offset: 0x000C4364
	public void PlayAimOpticExtraBolt()
	{
		this.general2d.Stop();
		this.general2d.Play(this.prename + "aim_bolt_optic_extra");
	}

	// Token: 0x060011DF RID: 4575 RVA: 0x000C6190 File Offset: 0x000C4390
	public void PlayReload(float time)
	{
		if (Peer.ClientGame.LocalPlayer.Ammo.CurrentWeapon.DoubleMagazine)
		{
			string text = (!Peer.ClientGame.LocalPlayer.Ammo.CurrentWeapon.FirstReload) ? "reload" : "reload2";
			this.general2d[this.prename + text].speed = this.general2d[this.prename + text].length / time;
			this.CrossFade(text, 0.05f);
		}
		else
		{
			this.general2d[this.prename + "reload"].speed = this.general2d[this.prename + "reload"].length / time;
			this.CrossFade("reload", 0.05f);
		}
	}

	// Token: 0x060011E0 RID: 4576 RVA: 0x000C6288 File Offset: 0x000C4488
	public float ReloadLenght()
	{
		if (Peer.ClientGame.LocalPlayer.Ammo.CurrentWeapon.DoubleMagazine)
		{
			string str = (!Peer.ClientGame.LocalPlayer.Ammo.CurrentWeapon.FirstReload) ? "reload" : "reload2";
			return (!(this.general2d[this.prename + str] == null)) ? this.general2d[this.prename + str].length : 1f;
		}
		return (!(this.general2d[this.prename + "reload"] == null)) ? this.general2d[this.prename + "reload"].length : 1f;
	}

	// Token: 0x060011E1 RID: 4577 RVA: 0x000C637C File Offset: 0x000C457C
	public void PlayReloadStart(float time)
	{
		this.general2d.Stop();
		this.general2d[this.prename + "reload_start"].speed = this.general2d[this.prename + "reload_start"].length / time;
		this.CrossFade("reload_start", 0.05f);
	}

	// Token: 0x060011E2 RID: 4578 RVA: 0x000C63E8 File Offset: 0x000C45E8
	public void PlayReloadLoop(float time)
	{
		this.general2d.Stop();
		this.general2d[this.prename + "reload_loop"].speed = this.general2d[this.prename + "reload_loop"].length / time;
		this.CrossFade("reload_loop", 0.05f);
	}

	// Token: 0x060011E3 RID: 4579 RVA: 0x000C6454 File Offset: 0x000C4654
	public void PlayReloadEnd(float time)
	{
		this.general2d.Stop();
		this.general2d[this.prename + "reload_end"].speed = this.general2d[this.prename + "reload_end"].length / time;
		this.CrossFade("reload_end", 0.05f);
	}

	// Token: 0x060011E4 RID: 4580 RVA: 0x000C64C0 File Offset: 0x000C46C0
	public float ReloadStartLenght()
	{
		if (this.general2d[this.prename + "reload_start"] == null)
		{
			return 1f;
		}
		return this.general2d[this.prename + "reload_start"].length;
	}

	// Token: 0x060011E5 RID: 4581 RVA: 0x000C651C File Offset: 0x000C471C
	public float ReloadLoopLenght()
	{
		if (this.general2d[this.prename + "reload_loop"] == null)
		{
			return 1f;
		}
		return this.general2d[this.prename + "reload_loop"].length;
	}

	// Token: 0x060011E6 RID: 4582 RVA: 0x000C6578 File Offset: 0x000C4778
	public float ReloadEndLenght()
	{
		if (this.general2d[this.prename + "reload_end"] == null)
		{
			return 1f;
		}
		return this.general2d[this.prename + "reload_end"].length;
	}

	// Token: 0x060011E7 RID: 4583 RVA: 0x000C65D4 File Offset: 0x000C47D4
	public void PlayIdleFrame()
	{
		this.general2d.Stop();
		try
		{
			this.general2d.Play(this.prename + "idle");
		}
		catch (Exception e)
		{
			global::Console.exception(e);
		}
	}

	// Token: 0x060011E8 RID: 4584 RVA: 0x000C6638 File Offset: 0x000C4838
	public void PlayIdleOut(float time)
	{
		this.general2d[this.prename + "idle_to_out"].speed = this.general2d[this.prename + "idle_to_out"].length / time;
		this.CrossFade("idle_to_out", 0.05f);
	}

	// Token: 0x060011E9 RID: 4585 RVA: 0x000C6698 File Offset: 0x000C4898
	public void PlayOutIdle(float time)
	{
		this.general2d[this.prename + "out_to_idle"].speed = this.general2d[this.prename + "out_to_idle"].length / time;
		this.CrossFade("out_to_idle", 0.05f);
	}

	// Token: 0x060011EA RID: 4586 RVA: 0x000C66F8 File Offset: 0x000C48F8
	public void PlayIdleAim(float time)
	{
		this.general2d[this.prename + "idle_to_aim"].speed = this.general2d[this.prename + "idle_to_aim"].length / time;
		this.general2d.Play(this.prename + "idle_to_aim");
	}

	// Token: 0x060011EB RID: 4587 RVA: 0x000C6764 File Offset: 0x000C4964
	public void PlayAimIdle(float time)
	{
		this.general2d[this.prename + "aim_to_idle"].speed = this.general2d[this.prename + "aim_to_idle"].length / time;
		this.general2d.Play(this.prename + "aim_to_idle");
	}

	// Token: 0x060011EC RID: 4588 RVA: 0x000C67D0 File Offset: 0x000C49D0
	public void PlayAimFrame()
	{
		this.general2d.Stop();
		try
		{
			this.general2d.Play(this.prename + "aim_idle");
		}
		catch (Exception e)
		{
			global::Console.exception(e);
		}
	}

	// Token: 0x060011ED RID: 4589 RVA: 0x000C6834 File Offset: 0x000C4A34
	public void PlayIdleAimOptic(float time)
	{
		if (this.general2d[this.prename + "idle_to_aim_optic"] == null)
		{
			this.PlayIdleAim(time);
			return;
		}
		this.general2d[this.prename + "idle_to_aim_optic"].speed = this.general2d[this.prename + "idle_to_aim_optic"].length / time;
		this.general2d.Play(this.prename + "idle_to_aim_optic");
	}

	// Token: 0x060011EE RID: 4590 RVA: 0x000C68D0 File Offset: 0x000C4AD0
	public void PlayAimIdleOptic(float time)
	{
		if (this.general2d[this.prename + "aim_to_idle_optic"] == null)
		{
			this.PlayAimIdle(time);
			return;
		}
		this.general2d[this.prename + "aim_to_idle_optic"].speed = this.general2d[this.prename + "aim_to_idle_optic"].length / time;
		this.general2d.Play(this.prename + "aim_to_idle_optic");
	}

	// Token: 0x060011EF RID: 4591 RVA: 0x000C696C File Offset: 0x000C4B6C
	public void PlayAimOpticFrame()
	{
		this.general2d.Stop();
		try
		{
			this.general2d.Play(this.prename + "aim_idle_optic");
		}
		catch (Exception e)
		{
			global::Console.exception(e);
		}
	}

	// Token: 0x060011F0 RID: 4592 RVA: 0x000C69D0 File Offset: 0x000C4BD0
	public void SupportOutIdle(float time)
	{
		this.general2d["marker_out_to_idle"].speed = this.general2d["marker_out_to_idle"].length / time;
		this.general2d.Play("marker_out_to_idle");
	}

	// Token: 0x060011F1 RID: 4593 RVA: 0x000C6A1C File Offset: 0x000C4C1C
	public void SupportIdleOut(float time)
	{
		this.general2d["marker_idle_to_out"].speed = this.general2d["marker_idle_to_out"].length / time;
		this.general2d.Play("marker_idle_to_out");
	}

	// Token: 0x060011F2 RID: 4594 RVA: 0x000C6A68 File Offset: 0x000C4C68
	public void ChargeSupport()
	{
		this.general2d.Play("marker_prior_fire1");
	}

	// Token: 0x060011F3 RID: 4595 RVA: 0x000C6A7C File Offset: 0x000C4C7C
	public void ThrowSupport()
	{
		this.general2d.Play("marker_fire1");
	}

	// Token: 0x060011F4 RID: 4596 RVA: 0x000C6A90 File Offset: 0x000C4C90
	private void Update()
	{
	}

	// Token: 0x060011F5 RID: 4597 RVA: 0x000C6AA0 File Offset: 0x000C4CA0
	private string GetTextDesc(AnimationState st)
	{
		return string.Format("{0}: {1} {2:0.00} / {3:0.00} {4}", new object[]
		{
			st.name,
			st.wrapMode,
			st.time,
			st.length,
			st.speed
		});
	}

	// Token: 0x04001160 RID: 4448
	public GameObject Arms;

	// Token: 0x04001161 RID: 4449
	private Animation general2d;

	// Token: 0x04001162 RID: 4450
	private string prename = string.Empty;

	// Token: 0x04001163 RID: 4451
	private CircleRandom fireRand = new CircleRandom();

	// Token: 0x04001164 RID: 4452
	private CircleRandom fireAimRand = new CircleRandom();

	// Token: 0x04001165 RID: 4453
	private AnimationState _stateRef;
}
