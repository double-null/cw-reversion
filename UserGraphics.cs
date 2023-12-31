using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002D8 RID: 728
[Serializable]
internal class UserGraphics : Convertible
{
	// Token: 0x060013D4 RID: 5076 RVA: 0x000D5454 File Offset: 0x000D3654
	// Note: this type is marked as 'beforefieldinit'.
	static UserGraphics()
	{
		UserGraphics.ProfileChanged = delegate()
		{
		};
	}

	// Token: 0x14000006 RID: 6
	// (add) Token: 0x060013D5 RID: 5077 RVA: 0x000D5484 File Offset: 0x000D3684
	// (remove) Token: 0x060013D6 RID: 5078 RVA: 0x000D549C File Offset: 0x000D369C
	internal static event Action ProfileChanged;

	// Token: 0x060013D7 RID: 5079 RVA: 0x000D54B4 File Offset: 0x000D36B4
	public void test()
	{
		this.textureQuality = 9;
	}

	// Token: 0x170002D6 RID: 726
	// (get) Token: 0x060013D8 RID: 5080 RVA: 0x000D54C4 File Offset: 0x000D36C4
	// (set) Token: 0x060013D9 RID: 5081 RVA: 0x000D54CC File Offset: 0x000D36CC
	public QualityLevelUser Level
	{
		get
		{
			return this.level;
		}
		set
		{
			if (this.level != value || this.startSet)
			{
				this.level = value;
				if (this.level == QualityLevelUser.Max)
				{
					this.SetMax();
				}
				if (this.level == QualityLevelUser.High)
				{
					this.SetHigh();
				}
				if (this.level == QualityLevelUser.Middle)
				{
					this.SetMiddle();
				}
				if (this.level == QualityLevelUser.LowMiddle)
				{
					this.SetLowMiddle();
				}
				if (this.level == QualityLevelUser.Low)
				{
					this.SetLow();
				}
				if (this.level == QualityLevelUser.VeryLow)
				{
					this.SetVeryLow();
				}
				this.startSet = false;
			}
		}
	}

	// Token: 0x170002D7 RID: 727
	// (get) Token: 0x060013DA RID: 5082 RVA: 0x000D556C File Offset: 0x000D376C
	// (set) Token: 0x060013DB RID: 5083 RVA: 0x000D5578 File Offset: 0x000D3778
	public bool Anisotropic
	{
		get
		{
			return this.anisotropicFiltering == AnisotropicFiltering.Enable;
		}
		set
		{
			if (value && this.anisotropicFiltering == AnisotropicFiltering.Enable)
			{
				return;
			}
			if (!value && this.anisotropicFiltering == AnisotropicFiltering.Disable)
			{
				return;
			}
			if (value)
			{
				this.anisotropicFiltering = AnisotropicFiltering.Enable;
			}
			else
			{
				this.anisotropicFiltering = AnisotropicFiltering.Disable;
			}
			this.level = QualityLevelUser.Custom;
		}
	}

	// Token: 0x170002D8 RID: 728
	// (get) Token: 0x060013DC RID: 5084 RVA: 0x000D55CC File Offset: 0x000D37CC
	// (set) Token: 0x060013DD RID: 5085 RVA: 0x000D55D4 File Offset: 0x000D37D4
	public float ShadowDistance
	{
		get
		{
			return this.shadowDistance;
		}
		set
		{
			if (this.shadowDistance != value)
			{
				this.shadowDistance = value;
				this.level = QualityLevelUser.Custom;
			}
		}
	}

	// Token: 0x170002D9 RID: 729
	// (get) Token: 0x060013DE RID: 5086 RVA: 0x000D55F0 File Offset: 0x000D37F0
	// (set) Token: 0x060013DF RID: 5087 RVA: 0x000D5620 File Offset: 0x000D3820
	public float SmallDistance
	{
		get
		{
			return (this.smallDistance >= 35f) ? this.smallDistance : 35f;
		}
		set
		{
			if (this.smallDistance != value)
			{
				this.smallDistance = value;
				this.level = QualityLevelUser.Custom;
			}
		}
	}

	// Token: 0x170002DA RID: 730
	// (get) Token: 0x060013E0 RID: 5088 RVA: 0x000D563C File Offset: 0x000D383C
	// (set) Token: 0x060013E1 RID: 5089 RVA: 0x000D5644 File Offset: 0x000D3844
	public bool IsTurnOnMaxQueuedFrames
	{
		get
		{
			return this.bMaxQueuedFrames;
		}
		set
		{
			if (this.bMaxQueuedFrames != value)
			{
				this.bMaxQueuedFrames = value;
				if (CVars.maxQueuedFrames == 0)
				{
					if (this.bMaxQueuedFrames)
					{
						this.iMaxQueuedFrames = -1;
					}
					else
					{
						this.iMaxQueuedFrames = 0;
					}
				}
				else
				{
					this.iMaxQueuedFrames = CVars.maxQueuedFrames;
				}
			}
		}
	}

	// Token: 0x170002DB RID: 731
	// (get) Token: 0x060013E2 RID: 5090 RVA: 0x000D569C File Offset: 0x000D389C
	// (set) Token: 0x060013E3 RID: 5091 RVA: 0x000D56A4 File Offset: 0x000D38A4
	public bool ShowingContractProgress
	{
		get
		{
			return this.bShowingContractProgress;
		}
		set
		{
			if (this.bShowingContractProgress != value)
			{
				this.bShowingContractProgress = value;
			}
		}
	}

	// Token: 0x170002DC RID: 732
	// (get) Token: 0x060013E4 RID: 5092 RVA: 0x000D56BC File Offset: 0x000D38BC
	// (set) Token: 0x060013E5 RID: 5093 RVA: 0x000D56C4 File Offset: 0x000D38C4
	public bool ShowingSimpleContractProgress
	{
		get
		{
			return this.bShowSimpleContractProgress;
		}
		set
		{
			if (this.bShowSimpleContractProgress != value)
			{
				this.bShowSimpleContractProgress = value;
			}
		}
	}

	// Token: 0x170002DD RID: 733
	// (get) Token: 0x060013E6 RID: 5094 RVA: 0x000D56DC File Offset: 0x000D38DC
	// (set) Token: 0x060013E7 RID: 5095 RVA: 0x000D56E4 File Offset: 0x000D38E4
	public bool AllwaysShowHP
	{
		get
		{
			return this.bAllwaysShowHP;
		}
		set
		{
			if (this.bAllwaysShowHP != value)
			{
				this.bAllwaysShowHP = value;
			}
		}
	}

	// Token: 0x170002DE RID: 734
	// (get) Token: 0x060013E8 RID: 5096 RVA: 0x000D56FC File Offset: 0x000D38FC
	// (set) Token: 0x060013E9 RID: 5097 RVA: 0x000D5704 File Offset: 0x000D3904
	public bool AllwaysShowDef
	{
		get
		{
			return this.bAllwaysShowDef;
		}
		set
		{
			if (this.bAllwaysShowDef != value)
			{
				this.bAllwaysShowDef = value;
			}
		}
	}

	// Token: 0x170002DF RID: 735
	// (get) Token: 0x060013EA RID: 5098 RVA: 0x000D571C File Offset: 0x000D391C
	// (set) Token: 0x060013EB RID: 5099 RVA: 0x000D5734 File Offset: 0x000D3934
	public bool AllwaysShowHPDef
	{
		get
		{
			return this.bAllwaysShowHP && this.bAllwaysShowDef;
		}
		set
		{
			if (this.bAllwaysShowHP != value || this.bAllwaysShowDef != value)
			{
				this.bAllwaysShowDef = value;
			}
			this.bAllwaysShowHP = value;
		}
	}

	// Token: 0x170002E0 RID: 736
	// (get) Token: 0x060013EC RID: 5100 RVA: 0x000D5768 File Offset: 0x000D3968
	// (set) Token: 0x060013ED RID: 5101 RVA: 0x000D576C File Offset: 0x000D396C
	public bool Autorespawn
	{
		get
		{
			return false;
		}
		set
		{
			this.autorespawn = value;
		}
	}

	// Token: 0x170002E1 RID: 737
	// (get) Token: 0x060013EE RID: 5102 RVA: 0x000D5778 File Offset: 0x000D3978
	// (set) Token: 0x060013EF RID: 5103 RVA: 0x000D5780 File Offset: 0x000D3980
	public bool ShouldSwitchOffChat
	{
		get
		{
			return this._shouldSwitchOffChat;
		}
		set
		{
			this._shouldSwitchOffChat = value;
		}
	}

	// Token: 0x170002E2 RID: 738
	// (get) Token: 0x060013F0 RID: 5104 RVA: 0x000D578C File Offset: 0x000D398C
	// (set) Token: 0x060013F1 RID: 5105 RVA: 0x000D5794 File Offset: 0x000D3994
	public bool HideInterface
	{
		get
		{
			return this.hideInterface;
		}
		set
		{
			this.hideInterface = value;
		}
	}

	// Token: 0x170002E3 RID: 739
	// (get) Token: 0x060013F2 RID: 5106 RVA: 0x000D57A0 File Offset: 0x000D39A0
	// (set) Token: 0x060013F3 RID: 5107 RVA: 0x000D57A8 File Offset: 0x000D39A8
	public bool EnableFullScreenInBattle
	{
		get
		{
			return this.enableFullScreenInBattle;
		}
		set
		{
			this.enableFullScreenInBattle = value;
		}
	}

	// Token: 0x170002E4 RID: 740
	// (get) Token: 0x060013F4 RID: 5108 RVA: 0x000D57B4 File Offset: 0x000D39B4
	// (set) Token: 0x060013F5 RID: 5109 RVA: 0x000D57BC File Offset: 0x000D39BC
	public bool SecondaryEquiped
	{
		get
		{
			return this.secondaryEquiped;
		}
		set
		{
			this.secondaryEquiped = value;
		}
	}

	// Token: 0x170002E5 RID: 741
	// (get) Token: 0x060013F6 RID: 5110 RVA: 0x000D57C8 File Offset: 0x000D39C8
	// (set) Token: 0x060013F7 RID: 5111 RVA: 0x000D57D0 File Offset: 0x000D39D0
	public bool ProKillTakeScreen
	{
		get
		{
			return this.pro_kill_take_screen;
		}
		set
		{
			this.pro_kill_take_screen = value;
		}
	}

	// Token: 0x170002E6 RID: 742
	// (get) Token: 0x060013F8 RID: 5112 RVA: 0x000D57DC File Offset: 0x000D39DC
	// (set) Token: 0x060013F9 RID: 5113 RVA: 0x000D57E4 File Offset: 0x000D39E4
	public bool QuadKillTakeScreen
	{
		get
		{
			return this.quad_kill_take_screen;
		}
		set
		{
			this.quad_kill_take_screen = value;
		}
	}

	// Token: 0x170002E7 RID: 743
	// (get) Token: 0x060013FA RID: 5114 RVA: 0x000D57F0 File Offset: 0x000D39F0
	// (set) Token: 0x060013FB RID: 5115 RVA: 0x000D57F8 File Offset: 0x000D39F8
	public bool LevelUpTakeScreen
	{
		get
		{
			return this.level_up_take_screen;
		}
		set
		{
			this.level_up_take_screen = value;
		}
	}

	// Token: 0x170002E8 RID: 744
	// (get) Token: 0x060013FC RID: 5116 RVA: 0x000D5804 File Offset: 0x000D3A04
	// (set) Token: 0x060013FD RID: 5117 RVA: 0x000D580C File Offset: 0x000D3A0C
	public bool AchievementTakeScreen
	{
		get
		{
			return this.achievement_take_screen;
		}
		set
		{
			this.achievement_take_screen = value;
		}
	}

	// Token: 0x170002E9 RID: 745
	// (get) Token: 0x060013FE RID: 5118 RVA: 0x000D5818 File Offset: 0x000D3A18
	// (set) Token: 0x060013FF RID: 5119 RVA: 0x000D5820 File Offset: 0x000D3A20
	public bool Favorites
	{
		get
		{
			return this._favorites;
		}
		set
		{
			this._favorites = value;
		}
	}

	// Token: 0x170002EA RID: 746
	// (get) Token: 0x06001400 RID: 5120 RVA: 0x000D582C File Offset: 0x000D3A2C
	// (set) Token: 0x06001401 RID: 5121 RVA: 0x000D5834 File Offset: 0x000D3A34
	public bool SS_LevelUp
	{
		get
		{
			return this.ss_levelup;
		}
		set
		{
			this.ss_levelup = value;
		}
	}

	// Token: 0x170002EB RID: 747
	// (get) Token: 0x06001402 RID: 5122 RVA: 0x000D5840 File Offset: 0x000D3A40
	// (set) Token: 0x06001403 RID: 5123 RVA: 0x000D5848 File Offset: 0x000D3A48
	public bool SS_WTask
	{
		get
		{
			return this.ss_wtask;
		}
		set
		{
			this.ss_wtask = value;
		}
	}

	// Token: 0x170002EC RID: 748
	// (get) Token: 0x06001404 RID: 5124 RVA: 0x000D5854 File Offset: 0x000D3A54
	// (set) Token: 0x06001405 RID: 5125 RVA: 0x000D585C File Offset: 0x000D3A5C
	public bool SS_Achievement
	{
		get
		{
			return this.ss_achievement;
		}
		set
		{
			this.ss_achievement = value;
		}
	}

	// Token: 0x170002ED RID: 749
	// (get) Token: 0x06001406 RID: 5126 RVA: 0x000D5868 File Offset: 0x000D3A68
	// (set) Token: 0x06001407 RID: 5127 RVA: 0x000D5870 File Offset: 0x000D3A70
	public bool SS_Streak
	{
		get
		{
			return this.ss_streak;
		}
		set
		{
			this.ss_streak = value;
		}
	}

	// Token: 0x170002EE RID: 750
	// (get) Token: 0x06001408 RID: 5128 RVA: 0x000D587C File Offset: 0x000D3A7C
	// (set) Token: 0x06001409 RID: 5129 RVA: 0x000D5884 File Offset: 0x000D3A84
	public bool PostEffects
	{
		get
		{
			return this.postEffects;
		}
		set
		{
			if (this.postEffects == value)
			{
				return;
			}
			this.postEffects = value;
			this.level = QualityLevelUser.Custom;
		}
	}

	// Token: 0x170002EF RID: 751
	// (get) Token: 0x0600140A RID: 5130 RVA: 0x000D58A4 File Offset: 0x000D3AA4
	// (set) Token: 0x0600140B RID: 5131 RVA: 0x000D58AC File Offset: 0x000D3AAC
	public bool ShouldLimitFrameRate
	{
		get
		{
			return this._shouldLimitFrameRate;
		}
		set
		{
			this._shouldLimitFrameRate = value;
			Application.targetFrameRate = ((!value || Application.targetFrameRate <= 60) ? Application.targetFrameRate : 60);
		}
	}

	// Token: 0x170002F0 RID: 752
	// (get) Token: 0x0600140C RID: 5132 RVA: 0x000D58E4 File Offset: 0x000D3AE4
	// (set) Token: 0x0600140D RID: 5133 RVA: 0x000D58EC File Offset: 0x000D3AEC
	public bool ModelsLQ
	{
		get
		{
			return this.modelsLQ;
		}
		set
		{
			if (this.modelsLQ != value)
			{
				this.modelsLQ = value;
				this.level = QualityLevelUser.Custom;
			}
		}
	}

	// Token: 0x170002F1 RID: 753
	// (get) Token: 0x0600140E RID: 5134 RVA: 0x000D5908 File Offset: 0x000D3B08
	// (set) Token: 0x0600140F RID: 5135 RVA: 0x000D5910 File Offset: 0x000D3B10
	public bool CharacterLQ
	{
		get
		{
			return this.characterLQ;
		}
		set
		{
			if (this.characterLQ != value)
			{
				this.characterLQ = value;
				this.level = QualityLevelUser.Custom;
			}
		}
	}

	// Token: 0x170002F2 RID: 754
	// (get) Token: 0x06001410 RID: 5136 RVA: 0x000D592C File Offset: 0x000D3B2C
	// (set) Token: 0x06001411 RID: 5137 RVA: 0x000D593C File Offset: 0x000D3B3C
	public Quality TextureQ
	{
		get
		{
			return (Quality)this.textureQuality.Value;
		}
		set
		{
			if (this.textureQuality.Value != (int)value)
			{
				this.textureQuality.Value = (int)value;
				this.level = QualityLevelUser.Custom;
			}
		}
	}

	// Token: 0x170002F3 RID: 755
	// (get) Token: 0x06001412 RID: 5138 RVA: 0x000D5970 File Offset: 0x000D3B70
	// (set) Token: 0x06001413 RID: 5139 RVA: 0x000D5978 File Offset: 0x000D3B78
	public SimpleQuality ShadowQ
	{
		get
		{
			return this.shadowQuality;
		}
		set
		{
			if (this.shadowQuality != value)
			{
				this.shadowQuality = value;
				this.level = QualityLevelUser.Custom;
			}
		}
	}

	// Token: 0x170002F4 RID: 756
	// (get) Token: 0x06001414 RID: 5140 RVA: 0x000D5994 File Offset: 0x000D3B94
	// (set) Token: 0x06001415 RID: 5141 RVA: 0x000D59CC File Offset: 0x000D3BCC
	public Quality LightingQ
	{
		get
		{
			return (this.lighting >= Quality.max) ? ((this.lighting <= Quality.low) ? this.lighting : Quality.low) : Quality.max;
		}
		set
		{
			if (this.lighting != value)
			{
				if (value == Quality.high)
				{
					this.ModelsLQ = true;
				}
				if (value == Quality.average)
				{
					this.ModelsLQ = true;
				}
				this.lighting = value;
				this.level = QualityLevelUser.Custom;
			}
		}
	}

	// Token: 0x170002F5 RID: 757
	// (get) Token: 0x06001416 RID: 5142 RVA: 0x000D5A10 File Offset: 0x000D3C10
	// (set) Token: 0x06001417 RID: 5143 RVA: 0x000D5A48 File Offset: 0x000D3C48
	public SimpleQuality PhysicsQ
	{
		get
		{
			return (this.physics >= SimpleQuality.low) ? ((this.physics <= SimpleQuality.high) ? this.physics : SimpleQuality.high) : SimpleQuality.low;
		}
		set
		{
			if (this.physics != value)
			{
				this.physics = value;
				this.level = QualityLevelUser.Custom;
			}
		}
	}

	// Token: 0x170002F6 RID: 758
	// (get) Token: 0x06001418 RID: 5144 RVA: 0x000D5A64 File Offset: 0x000D3C64
	public float RagDollTime
	{
		get
		{
			if (this.PhysicsQ == SimpleQuality.high)
			{
				return 4f;
			}
			if (this.PhysicsQ == SimpleQuality.average)
			{
				return 2.5f;
			}
			return 1.5f;
		}
	}

	// Token: 0x170002F7 RID: 759
	// (get) Token: 0x06001419 RID: 5145 RVA: 0x000D5A90 File Offset: 0x000D3C90
	// (set) Token: 0x0600141A RID: 5146 RVA: 0x000D5AB0 File Offset: 0x000D3CB0
	public float SelfRagDollTime
	{
		get
		{
			if (this.selfRagDollTime == 0f)
			{
				return this.RagDollTime;
			}
			return this.selfRagDollTime;
		}
		set
		{
			this.selfRagDollTime = ((value <= 0f) ? 0f : value);
		}
	}

	// Token: 0x0600141B RID: 5147 RVA: 0x000D5AD0 File Offset: 0x000D3CD0
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		JSON.ReadWriteEnum<QualityLevelUser>(dict, "level", ref this.level, isWrite);
		JSON.ReadWrite(dict, "shadowCascades", ref this.shadowCascades, isWrite);
		JSON.ReadWrite(dict, "pixelLightCount", ref this.pixelLightCount, isWrite);
		JSON.ReadWriteEnum<AnisotropicFiltering>(dict, "anisotropicFiltering", ref this.anisotropicFiltering, isWrite);
		JSON.ReadWrite(dict, "shadowDistance", ref this.shadowDistance, isWrite);
		JSON.ReadWrite(dict, "smallDistance", ref this.smallDistance, isWrite);
		JSON.ReadWrite(dict, "modelsLQ", ref this.modelsLQ, isWrite);
		JSON.ReadWrite(dict, "characterLQ", ref this.characterLQ, isWrite);
		JSON.ReadWrite(dict, "softParticles", ref this.softParticles, isWrite);
		JSON.ReadWrite(dict, "postEffects", ref this.postEffects, isWrite);
		if (isWrite && (this.textureQuality.Value > 3 || this.textureQuality.Value < 0))
		{
			this.textureQuality = 0;
		}
		Quality value = (Quality)this.textureQuality.Value;
		JSON.ReadWriteEnum<Quality>(dict, "textureQuality", ref value, isWrite);
		this.textureQuality = (int)value;
		if (!isWrite && (this.textureQuality.Value > 3 || this.textureQuality.Value < 0))
		{
			this.textureQuality = 0;
		}
		JSON.ReadWriteEnum<SimpleQuality>(dict, "shadowQuality", ref this.shadowQuality, isWrite);
		JSON.ReadWriteEnum<SimpleQuality>(dict, "physics", ref this.physics, isWrite);
		JSON.ReadWriteEnum<Quality>(dict, "lighting", ref this.lighting, isWrite);
		JSON.ReadWrite(dict, "bShowingContractProgress", ref this.bShowingContractProgress, isWrite);
		JSON.ReadWrite(dict, "bAllwaysShowHP", ref this.bAllwaysShowHP, isWrite);
		JSON.ReadWrite(dict, "bAllwaysShowDef", ref this.bAllwaysShowDef, isWrite);
		JSON.ReadWrite(dict, "autorespawn", ref this.autorespawn, isWrite);
		JSON.ReadWrite(dict, "hideInterface", ref this.hideInterface, isWrite);
		JSON.ReadWrite(dict, "enableFullScreenInBattle", ref this.enableFullScreenInBattle, isWrite);
		JSON.ReadWrite(dict, "secondaryEquiped", ref this.secondaryEquiped, isWrite);
		JSON.ReadWrite(dict, "weatherEffects", ref this.weatherEffects, isWrite);
		JSON.ReadWrite(dict, "favorites", ref this._favorites, isWrite);
		JSON.ReadWrite(dict, "bShowSimpleContractProgress", ref this.bShowSimpleContractProgress, isWrite);
		JSON.ReadWrite(dict, "bIsTurnOnMaxQueuedFrames", ref this.bMaxQueuedFrames, isWrite);
		JSON.ReadWrite(dict, "radioLoudness", ref this.radioLoudness, isWrite);
		JSON.ReadWrite(dict, "pro_kill_take_screen", ref this.pro_kill_take_screen, isWrite);
		JSON.ReadWrite(dict, "quad_kill_take_screen", ref this.quad_kill_take_screen, isWrite);
		JSON.ReadWrite(dict, "level_up_take_screen", ref this.level_up_take_screen, isWrite);
		JSON.ReadWrite(dict, "achievement_take_screen", ref this.achievement_take_screen, isWrite);
		if (this.bMaxQueuedFrames)
		{
			this.iMaxQueuedFrames = -1;
		}
		else
		{
			this.iMaxQueuedFrames = 0;
		}
		if (CVars.maxQueuedFrames != 0)
		{
			this.iMaxQueuedFrames = CVars.maxQueuedFrames;
		}
	}

	// Token: 0x0600141C RID: 5148 RVA: 0x000D5DA8 File Offset: 0x000D3FA8
	public void OnProfileChanged(bool checkQuality = false)
	{
		if (Peer.Dedicated)
		{
			return;
		}
		Audio.RefreshLoudness();
		if (QualitySettings.GetQualityLevel() != (int)this.level)
		{
			if (this.level != QualityLevelUser.Custom)
			{
				QualitySettings.SetQualityLevel((int)this.level);
			}
			else if (this.shadowQuality == SimpleQuality.high)
			{
				QualitySettings.SetQualityLevel(5);
			}
			else if (this.shadowQuality == SimpleQuality.average)
			{
				QualitySettings.SetQualityLevel(3);
			}
			else if (this.shadowQuality == SimpleQuality.low)
			{
				QualitySettings.SetQualityLevel(1);
			}
		}
		if (checkQuality)
		{
			if (this.textureQuality.Value > 3 || this.textureQuality.Value < 0)
			{
				QualitySettings.masterTextureLimit = 0;
				this.textureQuality.Value = 0;
			}
			else
			{
				QualitySettings.masterTextureLimit = this.textureQuality.Value;
			}
			if (QualitySettings.masterTextureLimit > 3 || QualitySettings.masterTextureLimit < 0)
			{
				Main.AddDatabaseRequest<HoneyPot>(new object[]
				{
					"CE/Artmoney",
					ViolationType.CeArtmoney
				});
				Application.targetFrameRate = 1;
			}
		}
		if (QualitySettings.maxQueuedFrames != this.iMaxQueuedFrames)
		{
			QualitySettings.maxQueuedFrames = this.iMaxQueuedFrames;
		}
		if (QualitySettings.pixelLightCount != this.pixelLightCount)
		{
			QualitySettings.pixelLightCount = this.pixelLightCount;
		}
		if (QualitySettings.shadowCascades != this.shadowCascades)
		{
			QualitySettings.shadowCascades = this.shadowCascades;
		}
		if (QualitySettings.shadowDistance != this.shadowDistance)
		{
			QualitySettings.shadowDistance = this.shadowDistance;
		}
		if (PrefabFactory.Sun)
		{
			LevelSettings.OnProfileChanged();
			this.SetLightingQuality();
			CameraListener.OnProfileChanged();
		}
		if (PrefabFactory.CurrentLevel)
		{
			AutoCustomLod[] array = (AutoCustomLod[])UnityEngine.Object.FindObjectsOfType(typeof(AutoCustomLod));
			if (this.modelsLQ)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i].ShowLQ();
				}
			}
			else
			{
				for (int j = 0; j < array.Length; j++)
				{
					array[j].Show();
				}
			}
		}
		MipmapCheck.Check();
		UserGraphics.ProfileChanged();
	}

	// Token: 0x0600141D RID: 5149 RVA: 0x000D5FC0 File Offset: 0x000D41C0
	public void SetLightingQuality()
	{
		if (this.lighting == Quality.low)
		{
			PrefabFactory.Sun.enabled = false;
			PrefabFactory.Sun.transform.FindChild("sunSecond").light.enabled = false;
			RenderSettings.ambientLight = SingletoneForm<LevelSettings>.Instance.ambientLightLQ2;
			if (PrefabFactory.Terrain)
			{
				PrefabFactory.Terrain.lightmapIndex = 255;
			}
			for (int i = 0; i < PrefabFactory.Renderers.Length; i++)
			{
				if (PrefabFactory.RenderersIndex[i] != -1)
				{
					PrefabFactory.Renderers[i].lightmapIndex = 255;
				}
			}
		}
		if (this.lighting == Quality.average)
		{
			this.ModelsLQ = true;
			PrefabFactory.Sun.enabled = false;
			PrefabFactory.Sun.transform.FindChild("sunSecond").light.enabled = false;
			RenderSettings.ambientLight = SingletoneForm<LevelSettings>.Instance.ambientLightLQ2;
			LightmapSettings.lightmapsMode = LightmapsMode.Single;
			if (PrefabFactory.Terrain)
			{
				PrefabFactory.Terrain.lightmapIndex = PrefabFactory.TerrainLightmapIndex;
			}
			for (int j = 0; j < PrefabFactory.Renderers.Length; j++)
			{
				if (PrefabFactory.RenderersIndex[j] != -1)
				{
					PrefabFactory.Renderers[j].lightmapIndex = PrefabFactory.RenderersIndex[j];
				}
			}
		}
		if (this.lighting == Quality.high)
		{
			LightmapSettings.lightmapsMode = SingletoneForm<LevelSettings>.Instance.lightmapsMode;
			if (LightmapSettings.lightmapsMode == LightmapsMode.Dual)
			{
				this.LightingQualityMax();
			}
			else
			{
				this.LightningQualityHight();
			}
		}
		if (this.lighting == Quality.max)
		{
			LightmapSettings.lightmapsMode = SingletoneForm<LevelSettings>.Instance.lightmapsMode;
			if (LightmapSettings.lightmapsMode == LightmapsMode.Dual)
			{
				this.LightningQualityHight();
			}
			else
			{
				this.LightingQualityMax();
			}
		}
	}

	// Token: 0x0600141E RID: 5150 RVA: 0x000D6188 File Offset: 0x000D4388
	private void LightningQualityHight()
	{
		this.ModelsLQ = true;
		PrefabFactory.Sun.enabled = true;
		PrefabFactory.Sun.transform.FindChild("sunSecond").light.enabled = false;
		RenderSettings.ambientLight = SingletoneForm<LevelSettings>.Instance.ambientLightLQ;
		if (PrefabFactory.Terrain)
		{
			PrefabFactory.Terrain.lightmapIndex = PrefabFactory.TerrainLightmapIndex;
		}
		LightmapSettings.lightmapsMode = SingletoneForm<LevelSettings>.Instance.lightmapsMode;
		for (int i = 0; i < PrefabFactory.Renderers.Length; i++)
		{
			if (PrefabFactory.RenderersIndex[i] != -1)
			{
				PrefabFactory.Renderers[i].lightmapIndex = PrefabFactory.RenderersIndex[i];
			}
		}
	}

	// Token: 0x0600141F RID: 5151 RVA: 0x000D6240 File Offset: 0x000D4440
	private void LightingQualityMax()
	{
		PrefabFactory.Sun.enabled = true;
		PrefabFactory.Sun.transform.FindChild("sunSecond").light.enabled = true;
		if (PrefabFactory.Terrain)
		{
			PrefabFactory.Terrain.lightmapIndex = 255;
		}
		for (int i = 0; i < PrefabFactory.Renderers.Length; i++)
		{
			if (PrefabFactory.RenderersIndex[i] != -1)
			{
				PrefabFactory.Renderers[i].lightmapIndex = 255;
			}
		}
	}

	// Token: 0x06001420 RID: 5152 RVA: 0x000D62D0 File Offset: 0x000D44D0
	public void OnLevelLoaded()
	{
		this.OnProfileChanged(true);
	}

	// Token: 0x06001421 RID: 5153 RVA: 0x000D62DC File Offset: 0x000D44DC
	private void SetMax()
	{
		this.shadowCascades = 2;
		this.pixelLightCount = 8;
		this.shadowDistance = 50f;
		this.smallDistance = 80f;
		this.softParticles = true;
		this.postEffects = true;
		this.textureQuality.Value = 0;
		this.shadowQuality = SimpleQuality.average;
		this.lighting = Quality.max;
		this.physics = SimpleQuality.high;
		this.characterLQ = false;
		this.modelsLQ = false;
	}

	// Token: 0x06001422 RID: 5154 RVA: 0x000D634C File Offset: 0x000D454C
	private void SetHigh()
	{
		this.shadowCascades = 2;
		this.pixelLightCount = 4;
		this.shadowDistance = 40f;
		this.smallDistance = 60f;
		this.postEffects = true;
		this.textureQuality.Value = 0;
		this.shadowQuality = SimpleQuality.average;
		this.lighting = Quality.max;
		this.physics = SimpleQuality.high;
		this.characterLQ = false;
		this.modelsLQ = false;
	}

	// Token: 0x06001423 RID: 5155 RVA: 0x000D63B4 File Offset: 0x000D45B4
	private void SetMiddle()
	{
		this.shadowCascades = 2;
		this.pixelLightCount = 3;
		this.shadowDistance = 30f;
		this.smallDistance = 45f;
		this.postEffects = true;
		this.textureQuality.Value = 0;
		this.shadowQuality = SimpleQuality.average;
		this.lighting = Quality.high;
		this.physics = SimpleQuality.average;
		this.characterLQ = false;
		this.modelsLQ = false;
	}

	// Token: 0x06001424 RID: 5156 RVA: 0x000D641C File Offset: 0x000D461C
	private void SetLowMiddle()
	{
		this.shadowCascades = 2;
		this.pixelLightCount = 3;
		this.anisotropicFiltering = AnisotropicFiltering.Disable;
		this.shadowDistance = 20f;
		this.smallDistance = 35f;
		this.postEffects = false;
		this.textureQuality.Value = 1;
		this.shadowQuality = SimpleQuality.low;
		this.lighting = Quality.average;
		this.physics = SimpleQuality.average;
		this.characterLQ = true;
		this.modelsLQ = true;
	}

	// Token: 0x06001425 RID: 5157 RVA: 0x000D648C File Offset: 0x000D468C
	private void SetLow()
	{
		this.shadowCascades = 2;
		this.pixelLightCount = 3;
		this.anisotropicFiltering = AnisotropicFiltering.Disable;
		this.shadowDistance = 10f;
		this.smallDistance = 35f;
		this.postEffects = false;
		this.textureQuality.Value = 1;
		this.shadowQuality = SimpleQuality.low;
		this.lighting = Quality.average;
		this.physics = SimpleQuality.low;
		this.characterLQ = true;
		this.modelsLQ = true;
	}

	// Token: 0x06001426 RID: 5158 RVA: 0x000D64FC File Offset: 0x000D46FC
	private void SetVeryLow()
	{
		this.shadowCascades = 1;
		this.pixelLightCount = 2;
		this.anisotropicFiltering = AnisotropicFiltering.Disable;
		this.shadowDistance = 5f;
		this.smallDistance = 35f;
		this.postEffects = false;
		this.textureQuality.Value = 1;
		this.shadowQuality = SimpleQuality.low;
		this.lighting = Quality.low;
		this.physics = SimpleQuality.low;
		this.characterLQ = true;
		this.modelsLQ = true;
	}

	// Token: 0x06001427 RID: 5159 RVA: 0x000D656C File Offset: 0x000D476C
	public static string SimpleQualityString(SimpleQuality quality)
	{
		if (quality == SimpleQuality.low)
		{
			return Language.SettingsLow;
		}
		if (quality == SimpleQuality.average)
		{
			return Language.SettingsMiddle;
		}
		if (quality == SimpleQuality.high)
		{
			return Language.SettingsHigh;
		}
		return "xplode your mind";
	}

	// Token: 0x06001428 RID: 5160 RVA: 0x000D659C File Offset: 0x000D479C
	public static string QualityString(Quality quality)
	{
		if (quality == Quality.low)
		{
			return Language.SettingsLow;
		}
		if (quality == Quality.average)
		{
			return Language.SettingsMiddle;
		}
		if (quality == Quality.high)
		{
			return Language.SettingsHigh;
		}
		if (quality == Quality.max)
		{
			return Language.SettingsMax;
		}
		return "shadows in your mind";
	}

	// Token: 0x040018A9 RID: 6313
	private QualityLevelUser level = QualityLevelUser.High;

	// Token: 0x040018AA RID: 6314
	private int shadowCascades = 4;

	// Token: 0x040018AB RID: 6315
	private int pixelLightCount = 4;

	// Token: 0x040018AC RID: 6316
	private AnisotropicFiltering anisotropicFiltering = AnisotropicFiltering.Enable;

	// Token: 0x040018AD RID: 6317
	private float shadowDistance = 30f;

	// Token: 0x040018AE RID: 6318
	private float smallDistance = 1f;

	// Token: 0x040018AF RID: 6319
	private float selfRagDollTime;

	// Token: 0x040018B0 RID: 6320
	private bool softParticles;

	// Token: 0x040018B1 RID: 6321
	public bool bShowingContractProgress = true;

	// Token: 0x040018B2 RID: 6322
	private bool bShowSimpleContractProgress = true;

	// Token: 0x040018B3 RID: 6323
	private bool bAllwaysShowHP;

	// Token: 0x040018B4 RID: 6324
	private bool bAllwaysShowDef;

	// Token: 0x040018B5 RID: 6325
	private bool autorespawn;

	// Token: 0x040018B6 RID: 6326
	private bool _shouldSwitchOffChat;

	// Token: 0x040018B7 RID: 6327
	private bool hideInterface;

	// Token: 0x040018B8 RID: 6328
	private bool enableFullScreenInBattle;

	// Token: 0x040018B9 RID: 6329
	private bool secondaryEquiped;

	// Token: 0x040018BA RID: 6330
	private bool weatherEffects = true;

	// Token: 0x040018BB RID: 6331
	private bool _favorites;

	// Token: 0x040018BC RID: 6332
	private bool postEffects;

	// Token: 0x040018BD RID: 6333
	private int iOldMaxQueuedFrames;

	// Token: 0x040018BE RID: 6334
	private int iMaxQueuedFrames = -1;

	// Token: 0x040018BF RID: 6335
	private bool bMaxQueuedFrames;

	// Token: 0x040018C0 RID: 6336
	private bool ss_levelup = true;

	// Token: 0x040018C1 RID: 6337
	private bool ss_wtask = true;

	// Token: 0x040018C2 RID: 6338
	private bool ss_achievement = true;

	// Token: 0x040018C3 RID: 6339
	private bool ss_streak = true;

	// Token: 0x040018C4 RID: 6340
	private bool pro_kill_take_screen;

	// Token: 0x040018C5 RID: 6341
	private bool quad_kill_take_screen;

	// Token: 0x040018C6 RID: 6342
	private bool level_up_take_screen;

	// Token: 0x040018C7 RID: 6343
	private bool achievement_take_screen;

	// Token: 0x040018C8 RID: 6344
	public Int textureQuality = new Int(0);

	// Token: 0x040018C9 RID: 6345
	private SimpleQuality shadowQuality;

	// Token: 0x040018CA RID: 6346
	private Quality lighting;

	// Token: 0x040018CB RID: 6347
	private SimpleQuality physics;

	// Token: 0x040018CC RID: 6348
	private bool startSet = true;

	// Token: 0x040018CD RID: 6349
	private bool modelsLQ;

	// Token: 0x040018CE RID: 6350
	private bool characterLQ;

	// Token: 0x040018CF RID: 6351
	public float radioLoudness = 1f;

	// Token: 0x040018D0 RID: 6352
	private bool _shouldLimitFrameRate;
}
