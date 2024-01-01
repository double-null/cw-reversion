using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x020000DC RID: 220
[AddComponentMenu("Scripts/GUI/BattleScreen")]
internal class BattleScreenGUI : Form
{
	// Token: 0x060005C7 RID: 1479 RVA: 0x0002BC44 File Offset: 0x00029E44
	public static void DirtToFace()
	{
		if (BattleScreenGUI.I != null)
		{
			BattleScreenGUI.I.AddDirtToFace();
		}
	}

	// Token: 0x060005C8 RID: 1480 RVA: 0x0002BC60 File Offset: 0x00029E60
	private void AddDirtToFace()
	{
		if (this.dirtArray.Count < 5)
		{
			BattleScreenGUI.DirtContainer dirtContainer = new BattleScreenGUI.DirtContainer(new GraphicValue(), new Rect((float)UnityEngine.Random.Range(-this.screenDust.width / 2, Screen.width - this.screenDust.width / 2), (float)UnityEngine.Random.Range(-this.screenDust.height / 2, Screen.height - this.screenDust.height / 2), (float)this.screenDust.width * UnityEngine.Random.Range(1f, 1.5f), (float)this.screenDust.height * UnityEngine.Random.Range(1f, 1.5f)));
			Vector2[] p = new Vector2[]
			{
				new Vector2(0f, 0f),
				new Vector2(0.1f, 6f),
				new Vector2(6f, -1f)
			};
			dirtContainer.Init(p);
			this.dirtArray.Add(dirtContainer);
		}
	}

	// Token: 0x060005C9 RID: 1481 RVA: 0x0002BD80 File Offset: 0x00029F80
	[Obfuscation(Exclude = true)]
	public void Fire(object obj = null)
	{
		Vector2[] v = new Vector2[]
		{
			(!this.fireGNAME.Exist()) ? new Vector2(0f, 0f) : new Vector2(0f, this.fireGNAME.Get()),
			new Vector2(0.3f, 1f),
			new Vector2(4f, 1f),
			new Vector2(4.45f, 0.001f),
			new Vector2(4.5f, 0f)
		};
		this.fireGNAME.Init(v);
	}

	// Token: 0x060005CA RID: 1482 RVA: 0x0002BE54 File Offset: 0x0002A054
	[Obfuscation(Exclude = true)]
	private void Grenade(object obj = null)
	{
		Vector2[] v = new Vector2[]
		{
			new Vector2(0f, 1f),
			new Vector2(2.5f, 0.001f),
			new Vector2(2.51f, 0f),
			new Vector2(3f, 0f)
		};
		this.grenadeGNAME.Init(v);
	}

	// Token: 0x060005CB RID: 1483 RVA: 0x0002BEE0 File Offset: 0x0002A0E0
	[Obfuscation(Exclude = true)]
	private void Hit(object obj = null)
	{
		this.isHit = true;
		this._hpHit.Start(false);
		this.GenerateBlood();
	}

	// Token: 0x060005CC RID: 1484 RVA: 0x0002BEFC File Offset: 0x0002A0FC
	private void GenerateBlood()
	{
		for (int i = 0; i < 2; i++)
		{
			this._bloodShardIndex++;
			if (this._bloodShardIndex >= this._bloodShards.Count)
			{
				this._bloodShardIndex = 0;
			}
			this._bloodShards[this._bloodShardIndex].Start();
		}
	}

	// Token: 0x060005CD RID: 1485 RVA: 0x0002BF5C File Offset: 0x0002A15C
	[Obfuscation(Exclude = true)]
	private void Hp(object obj = null)
	{
		this.isHit = false;
		this._hpHit.Start(false);
	}

	// Token: 0x060005CE RID: 1486 RVA: 0x0002BF74 File Offset: 0x0002A174
	[Obfuscation(Exclude = true)]
	private void Change(object obj = null)
	{
		Vector2[] v = new Vector2[]
		{
			(!this.changeGNAME.Exist()) ? new Vector2(0f, 0f) : new Vector2(0f, this.changeGNAME.Get()),
			new Vector2(0.05f, 1f),
			new Vector2(0.2f, 1f),
			new Vector2(0.3f, 0.001f),
			new Vector2(0.301f, 0f)
		};
		this.changeGNAME.Init(v);
	}

	// Token: 0x060005CF RID: 1487 RVA: 0x0002C048 File Offset: 0x0002A248
	[Obfuscation(Exclude = true)]
	private void Pick(object obj = null)
	{
		Vector2[] v = new Vector2[]
		{
			(!this.pickGNAME.Exist()) ? new Vector2(0f, 0f) : new Vector2(0f, this.pickGNAME.Get()),
			new Vector2(0.05f, 1f),
			new Vector2(0.2f, 1f),
			new Vector2(0.3f, 0.001f),
			new Vector2(0.301f, 0f)
		};
		this.pickGNAME.Init(v);
	}

	// Token: 0x060005D0 RID: 1488 RVA: 0x0002C11C File Offset: 0x0002A31C
	[Obfuscation(Exclude = true)]
	private void Die(object obj = null)
	{
		this.GenerateBlood();
	}

	// Token: 0x060005D1 RID: 1489 RVA: 0x0002C124 File Offset: 0x0002A324
	[Obfuscation(Exclude = true)]
	private void ClearBlood(object obj = null)
	{
	}

	// Token: 0x060005D2 RID: 1490 RVA: 0x0002C128 File Offset: 0x0002A328
	private void ShowAmmoOnReload()
	{
		if (BattleScreenGUI.showAmmo && this.showAmmoTime.Elapsed == 0f)
		{
			this.showAmmoTime.Start();
		}
		if (this.showAmmoTime.Elapsed > 2f)
		{
			BattleScreenGUI.showAmmo = false;
			this.showAmmoTime.Stop();
		}
	}

	// Token: 0x060005D3 RID: 1491 RVA: 0x0002C188 File Offset: 0x0002A388
	public override void MainInitialize()
	{
		BattleScreenGUI.I = this;
		this.isRendering = true;
		this.isUpdating = true;
		base.MainInitialize();
		this._boolHit = new CurveEval(this.BloodCurve);
		this._hpHit = new CurveEval(this.HpCurve);
		for (int i = 0; i < 10; i++)
		{
			this._bloodShards.Add(new BattleScreenGUI.BloodShard(this.BloodCurve, this.Blood));
		}
	}

	// Token: 0x060005D4 RID: 1492 RVA: 0x0002C200 File Offset: 0x0002A400
	public override void OnConnected()
	{
		base.OnConnected();
		this.Clear();
	}

	// Token: 0x060005D5 RID: 1493 RVA: 0x0002C210 File Offset: 0x0002A410
	public override void OnDisconnect()
	{
		base.OnDisconnect();
		this.Clear();
	}

	// Token: 0x060005D6 RID: 1494 RVA: 0x0002C220 File Offset: 0x0002A420
	public override void Clear()
	{
		this.fireGNAME = new GraphicValue();
		this.grenadeGNAME = new GraphicValue();
		this.changeGNAME = new GraphicValue();
		this.pickGNAME = new GraphicValue();
		this.dirtArray = new List<BattleScreenGUI.DirtContainer>();
		this.deltaLen = 0.5f;
		this.accuracy = 0f;
		this.delta = Vector2.zero;
		this.isHit = false;
	}

	// Token: 0x060005D7 RID: 1495 RVA: 0x0002C28C File Offset: 0x0002A48C
	public override void Register()
	{
		EventFactory.Register("Fire", this);
		EventFactory.Register("Grenade", this);
		EventFactory.Register("Hit", this);
		EventFactory.Register("Hp", this);
		EventFactory.Register("Change", this);
		EventFactory.Register("Pick", this);
		EventFactory.Register("Die", this);
		EventFactory.Register("ClearBlood", this);
	}

	// Token: 0x060005D8 RID: 1496 RVA: 0x0002C2F4 File Offset: 0x0002A4F4
	public override void OnSpawn()
	{
		this.fireGNAME = new GraphicValue();
		this.grenadeGNAME = new GraphicValue();
		this.changeGNAME = new GraphicValue();
		this.pickGNAME = new GraphicValue();
		this.Fire(null);
		this.Grenade(null);
	}

	// Token: 0x060005D9 RID: 1497 RVA: 0x0002C33C File Offset: 0x0002A53C
	public override void OnDie()
	{
		this.Clear();
		this.Die(null);
	}

	// Token: 0x060005DA RID: 1498 RVA: 0x0002C34C File Offset: 0x0002A54C
	public override void GameGUI()
	{
		if (Event.current.type != EventType.Repaint)
		{
			return;
		}
		float num = base.visibility;
		if (num > 1f)
		{
			num = 1f;
		}
		GUI.color = Colors.alpha(Color.white, num);
		for (int i = 0; i < this._bloodShards.Count; i++)
		{
			this._bloodShards[i].OnGUI();
		}
		for (int j = 0; j < this.dirtArray.Count; j++)
		{
			if (this.dirtArray[j].Visibility > 0f)
			{
				GUI.color = Colors.alpha(GUI.color, this.dirtArray[j].Visibility);
				this.gui.RotateGUI(this.dirtArray[j].angle, this.dirtArray[j].Center);
				GUI.DrawTexture(this.dirtArray[j].Rect, this.screenDust, ScaleMode.StretchToFill);
				this.gui.RotateGUI(0f, Vector2.zero);
			}
			else
			{
				this.dirtArray.RemoveAt(j);
			}
		}
		if (!Main.IsAlive)
		{
			return;
		}
		BaseWeapon currentWeapon = Peer.ClientGame.LocalPlayer.Ammo.CurrentWeapon;
		if (Peer.ClientGame.LocalPlayer.Ammo.IsAim && !currentWeapon.Optic && currentWeapon.markTexture && currentWeapon.showMarks)
		{
			this.gui.color = new Color(1f, 1f, 1f, Mathf.Clamp01((1f - this.accuracy - 0.5f) * 2f));
			this.gui.PictureCentereNoScale(new Vector2((float)(Screen.width / 2 - 1), (float)(Screen.height / 2 - 1)), currentWeapon.markTexture, Vector2.one, true);
		}
		if (Main.UserInfo.settings.graphics.HideInterface)
		{
			return;
		}
		int grenadeCount = Peer.ClientGame.LocalPlayer.Ammo.state.grenadeCount;
		float health = Peer.ClientGame.LocalPlayer.Health;
		float armor = Peer.ClientGame.LocalPlayer.Armor;
		if (Peer.ClientGame.LocalPlayer.IsRegen)
		{
			this.Hp(null);
		}
		float num2 = this._hpHit.Value;
		float a = (float)((((!Main.UserInfo.settings.graphics.AllwaysShowHP && num2 <= 0f && health >= 25f) || Peer.HardcoreMode) && (health >= 15f || !Peer.HardcoreMode)) ? 0 : 1);
		if (!this.isHit)
		{
			num2 = -1f;
		}
		if (health < 25f)
		{
			num2 = 1f;
		}
		GUI.color = new Color((num2 >= 0f) ? (0.72f + (1f - num2)) : 1f, (num2 <= 0f) ? 1f : (1f - num2), (num2 <= 0f) ? 1f : (1f - num2), a);
		GUI.DrawTexture(new Rect(10f, (float)(Screen.height - 67 + 12), (float)this.hp.width, (float)this.hp.height), this.hp);
		GUI.Label(new Rect((float)(23 + this.hp.width - 5), (float)(Screen.height - 64 + 11), 150f, 300f), health.ToString("F0"), CWGUI.p.healthStyle);
		if (!Peer.HardcoreMode)
		{
			GUI.color = new Color(1f, 1f, 1f, a);
			GUI.DrawTexture(new Rect(174f, (float)(Screen.height - 67 + 12), (float)this.armor_icon.width, (float)this.armor_icon.height), this.armor_icon);
			GUI.Label(new Rect((float)(23 + this.hp.width + 154), (float)(Screen.height - 64 + 11), 150f, 300f), armor.ToString("F0"), CWGUI.p.healthStyle);
		}
		if (!Peer.HardcoreMode)
		{
			this.tmpColor = this.gui.color;
			this.gui.color = new Color(1f, 1f, 1f, 0.5f * Main.UserInfo.settings.radarAlpha);
			GUI.DrawTexture(new Rect(13f, 240f, 190f, 20f), this.gui.black);
			this.gui.TextLabel(new Rect(15f, 240f, 190f, 20f), Language.KillStreak + ": ", 16, "#FFFFFF", TextAnchor.MiddleLeft, true);
			this.gui.TextLabel(new Rect(13f, 240f, 185f, 22f), BannerGUI.killCounter, 18, "#62AEEA", TextAnchor.MiddleRight, true);
			this.gui.color = this.tmpColor;
		}
		Camera camera = CameraListener.Camera;
		if (!currentWeapon)
		{
			this.gui.color = Colors.alpha(this.gui.color, 1f);
			if (Peer.ClientGame.LocalPlayer.Ammo.lastSupport == ArmstreakEnum.mortar)
			{
				this.gui.TextLabel(new Rect((float)(Screen.width - 210), (float)(Screen.height - 50), 200f, 30f), Language.SettingsCallMortarStrike, 14, "#FFFFFF_Micra", TextAnchor.MiddleRight, true);
			}
			if (Peer.ClientGame.LocalPlayer.Ammo.lastSupport == ArmstreakEnum.sonar)
			{
				this.gui.TextLabel(new Rect((float)(Screen.width - 210), (float)(Screen.height - 50), 200f, 30f), Language.SettingsCallSonar, 14, "#FFFFFF_Micra", TextAnchor.MiddleRight, true);
			}
		}
		if (!currentWeapon || !camera)
		{
			return;
		}
		if (Peer.HardcoreMode)
		{
			this.ShowAmmoOnReload();
		}
		if ((!Peer.HardcoreMode && this.fireGNAME.Exist()) || BattleScreenGUI.showAmmo)
		{
			if (currentWeapon.state.clips < 6)
			{
				this.Fire(null);
				if (this.fireGNAME.OnChange())
				{
					this.gui.color = new Color(0.72f, 0f, 0f, this.fireGNAME.Get());
				}
			}
			else
			{
				this.gui.color = new Color(1f, 1f, 1f, this.fireGNAME.Get());
			}
			this.gui.TextLabel(new Rect((float)(Screen.width - 194), (float)(Screen.height - 110), 110f, 100f), currentWeapon.state.clips, 30, "#FFFFFF_Micra", TextAnchor.LowerRight, true);
			this.gui.TextLabel(new Rect((float)(Screen.width - 167), (float)(Screen.height - 112), 110f, 100f), "/", 30, "#FFFFFF_Micra", TextAnchor.LowerRight, true);
			this.gui.color = new Color(1f, 1f, 1f, this.fireGNAME.Get());
			this.gui.TextLabel(new Rect((float)(Screen.width - 108 + 49), (float)(Screen.height - 36), 100f, 100f), currentWeapon.state.bagSize, 14, "#FFFFFF_Micra", TextAnchor.UpperLeft, true);
			this.gui.TextLabel(new Rect((float)(Screen.width - 550 + 22), (float)(Screen.height - 37), 350f, 100f), currentWeapon.guiInterfaceName, 14, "#FFFFFF_Micra", TextAnchor.UpperRight, true);
			this.gui.color = new Color(1f, 1f, 1f, this.fireGNAME.Get());
			if (currentWeapon.BagSize == 0 && currentWeapon.state.clips == 0)
			{
				this.gui.Picture(new Vector2((float)(Screen.width - 158), (float)(Screen.height - 63)), this.NO_AMMO);
			}
			else if (currentWeapon.Damaged)
			{
				this.gui.Picture(new Vector2((float)(Screen.width - 158), (float)(Screen.height - 63)), this.DAMAGED);
			}
			else if (currentWeapon.duplet || (currentWeapon.state.singleShot && currentWeapon.type == Weapons.an94))
			{
				this.gui.Picture(new Vector2((float)(Screen.width - 158), (float)(Screen.height - 63)), this.DOUBLE);
			}
			else if (currentWeapon.weaponNature != WeaponNature.shotgun && currentWeapon.state.singleShot && !currentWeapon.duplet)
			{
				this.gui.Picture(new Vector2((float)(Screen.width - 158), (float)(Screen.height - 63)), this.SINGLE);
			}
			else if (currentWeapon.weaponNature == WeaponNature.shotgun && currentWeapon.state.singleShot && !currentWeapon.duplet)
			{
				this.gui.Picture(new Vector2((float)(Screen.width - 158), (float)(Screen.height - 63)), this.SINGLE_SHOTGUN);
			}
			else if (currentWeapon.auto)
			{
				this.gui.Picture(new Vector2((float)(Screen.width - 158), (float)(Screen.height - 63)), this.AUTO);
			}
			else if (currentWeapon.burst)
			{
				this.gui.Picture(new Vector2((float)(Screen.width - 158), (float)(Screen.height - 63)), this.BURST);
			}
			if (grenadeCount > 0)
			{
				this.gui.Picture(new Vector2((float)(Screen.width - 84 + 6), (float)(Screen.height - 108 - 31)), this.GRENADE);
				if (grenadeCount > 1)
				{
					this.gui.TextLabel(new Rect((float)(Screen.width - 500 + 22 + 163), (float)(Screen.height - 44 - 55), 300f, 100f), "x" + grenadeCount, 12, "#FFFFFF_Micra", TextAnchor.UpperRight, true);
				}
			}
		}
		if (Peer.HardcoreMode)
		{
			this.gui.color = new Color(1f, 1f, 1f, this.fireGNAME.Get());
			if (currentWeapon.BagSize == 0 && currentWeapon.state.clips == 0)
			{
				this.gui.Picture(new Vector2((float)(Screen.width - 125), (float)(Screen.height - 50)), this.NO_AMMO);
			}
		}
		
		if (this.stab_alpha.Visible)
		{
			this.gui.color = Colors.alpha(Color.red, this.stab_alpha.visibility);
			this.gui.RotateGUI(45f, new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)));
			this.gui.PictureCentereNoScale(new Vector2((float)(Screen.width / 2 - (int)(4f / this.deltaLen) - (int)((float)Screen.width * this.delta.x) - 1), (float)(Screen.height / 2 - 1)), this.crossLeft, Vector2.one, false);
			this.gui.PictureCentereNoScale(new Vector2((float)(Screen.width / 2 + (int)(4f / this.deltaLen) + (int)((float)Screen.width * this.delta.x) - 1), (float)(Screen.height / 2 - 1)), this.crossRight, Vector2.one, false);
			this.gui.PictureCentereNoScale(new Vector2((float)(Screen.width / 2 - 1), (float)(Screen.height / 2 - (int)(4f / this.deltaLen) - (int)((float)Screen.height * this.delta.x * camera.aspect) - 1)), this.crossUp, Vector2.one, false);
			this.gui.PictureCentereNoScale(new Vector2((float)(Screen.width / 2 - 1), (float)(Screen.height / 2 + (int)(4f / this.deltaLen) + (int)((float)Screen.height * this.delta.x * camera.aspect) - 1)), this.crossDown, Vector2.one, false);
			this.gui.RotateGUI(0f, Vector2.zero);
		}
		this.gui.color = new Color(1f, 1f, 1f, this.accuracy);
		this.gui.PictureCentereNoScale(new Vector2((float)(Screen.width / 2 - (int)(8f / this.deltaLen) - (int)((float)Screen.width * this.delta.x) - 1), (float)(Screen.height / 2 - 1)), this.crossLeft, Vector2.one, false);
		this.gui.PictureCentereNoScale(new Vector2((float)(Screen.width / 2 + (int)(8f / this.deltaLen) + (int)((float)Screen.width * this.delta.x) - 1), (float)(Screen.height / 2 - 1)), this.crossRight, Vector2.one, false);
		this.gui.PictureCentereNoScale(new Vector2((float)(Screen.width / 2 - 1), (float)(Screen.height / 2 - (int)(8f / this.deltaLen) - (int)((float)Screen.height * this.delta.x * camera.aspect) - 1)), this.crossUp, Vector2.one, false);
		this.gui.PictureCentereNoScale(new Vector2((float)(Screen.width / 2 - 1), (float)(Screen.height / 2 + (int)(8f / this.deltaLen) + (int)((float)Screen.height * this.delta.x * camera.aspect) - 1)), this.crossDown, Vector2.one, false);
	}

	// Token: 0x060005DB RID: 1499 RVA: 0x0002D254 File Offset: 0x0002B454
	public override void OnUpdate()
	{
		if (!Main.IsGameLoaded || Peer.ClientGame != null || Peer.ClientGame.LocalPlayer != null)
		{
			return;
		}
		if (this.oldHealth < 15f && this.oldHealth != Peer.ClientGame.LocalPlayer.Health)
		{
			this.Hit(null);
		}
		this.oldHealth = Peer.ClientGame.LocalPlayer.Health;
		base.OnUpdate();
	}

	// Token: 0x04000593 RID: 1427
	private CurveEval _boolHit;

	// Token: 0x04000594 RID: 1428
	private CurveEval _hpHit;

	// Token: 0x04000595 RID: 1429
	private List<BattleScreenGUI.BloodShard> _bloodShards = new List<BattleScreenGUI.BloodShard>();

	// Token: 0x04000596 RID: 1430
	private int _bloodShardIndex;

	// Token: 0x04000597 RID: 1431
	public Texture2D hp;

	// Token: 0x04000598 RID: 1432
	public Texture2D armor_icon;

	// Token: 0x04000599 RID: 1433
	public Texture2D change;

	// Token: 0x0400059A RID: 1434
	public Texture2D pick;

	// Token: 0x0400059B RID: 1435
	public Texture2D crossLeft;

	// Token: 0x0400059C RID: 1436
	public Texture2D crossRight;

	// Token: 0x0400059D RID: 1437
	public Texture2D crossUp;

	// Token: 0x0400059E RID: 1438
	public Texture2D crossDown;

	// Token: 0x0400059F RID: 1439
	public Texture2D Blood;

	// Token: 0x040005A0 RID: 1440
	[SerializeField]
	public AnimationCurve BloodCurve = new AnimationCurve();

	// Token: 0x040005A1 RID: 1441
	[SerializeField]
	public AnimationCurve HpCurve = new AnimationCurve();

	// Token: 0x040005A2 RID: 1442
	public Texture2D DAMAGED;

	// Token: 0x040005A3 RID: 1443
	public Texture2D NO_AMMO;

	// Token: 0x040005A4 RID: 1444
	public Texture2D DOUBLE;

	// Token: 0x040005A5 RID: 1445
	public Texture2D BURST;

	// Token: 0x040005A6 RID: 1446
	public Texture2D SINGLE;

	// Token: 0x040005A7 RID: 1447
	public Texture2D SINGLE_SHOTGUN;

	// Token: 0x040005A8 RID: 1448
	public Texture2D AUTO;

	// Token: 0x040005A9 RID: 1449
	public Texture2D GRENADE;

	// Token: 0x040005AA RID: 1450
	public Texture2D screenDust;

	// Token: 0x040005AB RID: 1451
	public static bool showAmmo;

	// Token: 0x040005AC RID: 1452
	private GraphicValue fireGNAME = new GraphicValue();

	// Token: 0x040005AD RID: 1453
	private GraphicValue grenadeGNAME = new GraphicValue();

	// Token: 0x040005AE RID: 1454
	private GraphicValue changeGNAME = new GraphicValue();

	// Token: 0x040005AF RID: 1455
	private GraphicValue pickGNAME = new GraphicValue();

	// Token: 0x040005B0 RID: 1456
	private GraphicValue bloodGNAME = new GraphicValue();

	// Token: 0x040005B1 RID: 1457
	private float oldHealth = -1f;

	// Token: 0x040005B2 RID: 1458
	private List<BattleScreenGUI.DirtContainer> dirtArray = new List<BattleScreenGUI.DirtContainer>();

	// Token: 0x040005B3 RID: 1459
	private float deltaLen = 0.5f;

	// Token: 0x040005B4 RID: 1460
	[HideInInspector]
	public float accuracy;

	// Token: 0x040005B5 RID: 1461
	[HideInInspector]
	public Vector2 delta = Vector2.zero;

	// Token: 0x040005B6 RID: 1462
	public Alpha stab_alpha = new Alpha();

	// Token: 0x040005B7 RID: 1463
	private float h1 = 0.1f;

	// Token: 0x040005B8 RID: 1464
	private float h2 = 1f;

	// Token: 0x040005B9 RID: 1465
	private float h3 = 4f;

	// Token: 0x040005BA RID: 1466
	private float h4 = 4.5f;

	// Token: 0x040005BB RID: 1467
	private bool isHit;

	// Token: 0x040005BC RID: 1468
	private Vector3[] bloodShift = new Vector3[5];

	// Token: 0x040005BD RID: 1469
	private eTimer showAmmoTime = new eTimer();

	// Token: 0x040005BE RID: 1470
	private Color tmpColor;

	// Token: 0x040005BF RID: 1471
	public static BattleScreenGUI I;

	// Token: 0x020000DD RID: 221
	private class DirtContainer
	{
		// Token: 0x060005DC RID: 1500 RVA: 0x0002D2E0 File Offset: 0x0002B4E0
		public DirtContainer(GraphicValue value, Rect rect)
		{
			this.angle = (float)UnityEngine.Random.Range(0, 180);
			this.value = value;
			this.rect = rect;
			this.Center = new Vector2(rect.x + rect.width / 2f, rect.y + rect.height / 2f);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0002D348 File Offset: 0x0002B548
		public void Init(Vector2[] p)
		{
			if (this.value != null)
			{
				this.value.Init(p);
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x0002D364 File Offset: 0x0002B564
		public Rect Rect
		{
			get
			{
				return this.rect;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x0002D36C File Offset: 0x0002B56C
		public float Visibility
		{
			get
			{
				if (this.value != null)
				{
					return this.value.Get();
				}
				return -1f;
			}
		}

		// Token: 0x040005C0 RID: 1472
		public float angle;

		// Token: 0x040005C1 RID: 1473
		private GraphicValue value;

		// Token: 0x040005C2 RID: 1474
		private Rect rect;

		// Token: 0x040005C3 RID: 1475
		public Vector2 Center;
	}

	// Token: 0x020000DE RID: 222
	private class BloodShard
	{
		// Token: 0x060005E0 RID: 1504 RVA: 0x0002D38C File Offset: 0x0002B58C
		public BloodShard(AnimationCurve c, Texture2D blood)
		{
			this._curveEval = new CurveEval(c);
			this._blood = blood;
			this._halfBloodWidth = (float)(blood.width / 2);
			this._halfBloodHeight = (float)(blood.height / 2);
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0002D3DC File Offset: 0x0002B5DC
		public void OnGUI()
		{
			if (this._curveEval.Value > 0.01f)
			{
				GUI.color = new Color(1f, 1f, 1f, this._curveEval.Value);
				MainGUI.Instance.RotateGUI(this._angle, new Vector2(this._posx + this._halfBloodWidth * this._scale, this._posy + this._halfBloodHeight * this._scale));
				GUI.DrawTexture(new Rect(this._posx, this._posy, (float)this._blood.width * this._scale, (float)this._blood.height * this._scale), this._blood, ScaleMode.StretchToFill);
				MainGUI.Instance.RotateGUI(0f, Vector2.zero);
			}
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0002D4B8 File Offset: 0x0002B6B8
		public void Start()
		{
			this._angle = UnityEngine.Random.Range(0f, 360f);
			this._scale = UnityEngine.Random.Range(0.7f, 1.2f);
			this._side = UnityEngine.Random.Range(0, 4);
			if (this._side == 0)
			{
				this._posx = UnityEngine.Random.Range(-this._halfBloodWidth * this._scale, (float)Screen.width + this._halfBloodWidth * this._scale);
				this._posy = UnityEngine.Random.Range((float)(-(float)this._blood.height * 2 / 3) * this._scale, -this._halfBloodHeight * this._scale);
			}
			if (this._side == 1)
			{
				this._posy = UnityEngine.Random.Range(-this._halfBloodHeight * this._scale, (float)Screen.height + this._halfBloodHeight * this._scale);
				this._posx = UnityEngine.Random.Range((float)(-(float)this._blood.width * 2 / 3) * this._scale, -this._halfBloodWidth * this._scale);
			}
			if (this._side == 2)
			{
				this._posy = UnityEngine.Random.Range(-this._halfBloodHeight * this._scale, (float)Screen.height + this._halfBloodHeight * this._scale);
				this._posx = UnityEngine.Random.Range((float)Screen.width - this._halfBloodWidth * this._scale, (float)Screen.width - (float)(this._blood.width / 3) * this._scale);
			}
			if (this._side == 3)
			{
				this._posx = UnityEngine.Random.Range(-this._halfBloodWidth * this._scale, (float)Screen.width + this._halfBloodWidth * this._scale);
				this._posy = UnityEngine.Random.Range((float)Screen.height - this._halfBloodHeight * this._scale, (float)Screen.height - (float)(this._blood.height / 3) * this._scale);
			}
			this._curveEval.Start(false);
		}

		// Token: 0x040005C4 RID: 1476
		private readonly CurveEval _curveEval;

		// Token: 0x040005C5 RID: 1477
		private Vector3[] _bloodShift;

		// Token: 0x040005C6 RID: 1478
		private Texture2D _blood;

		// Token: 0x040005C7 RID: 1479
		private float _angle;

		// Token: 0x040005C8 RID: 1480
		private float _scale = 1f;

		// Token: 0x040005C9 RID: 1481
		private float _posx;

		// Token: 0x040005CA RID: 1482
		private float _posy;

		// Token: 0x040005CB RID: 1483
		private float _halfBloodWidth;

		// Token: 0x040005CC RID: 1484
		private float _halfBloodHeight;

		// Token: 0x040005CD RID: 1485
		private int _side;
	}
}
