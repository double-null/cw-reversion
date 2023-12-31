using System;
using UnityEngine;

// Token: 0x020000BD RID: 189
[AddComponentMenu("Scripts/Engine/SoundFactory")]
internal class SoundFactory : SingletoneForm<SoundFactory>
{
	// Token: 0x060004FA RID: 1274 RVA: 0x00020884 File Offset: 0x0001EA84
	public override void OnDisconnect()
	{
		SingletoneForm<SoundFactory>.instance = null;
	}

	// Token: 0x0400046D RID: 1133
	public AudioClip[] knifeSounds;

	// Token: 0x0400046E RID: 1134
	public AudioClip[] changeClips;

	// Token: 0x0400046F RID: 1135
	public AudioClip[] fallPain;

	// Token: 0x04000470 RID: 1136
	public AudioClip[] dieSounds;

	// Token: 0x04000471 RID: 1137
	public AudioClip[] hitSounds;

	// Token: 0x04000472 RID: 1138
	public AudioClip[] armorHitSounds;

	// Token: 0x04000473 RID: 1139
	public AudioClip noAmmo;

	// Token: 0x04000474 RID: 1140
	public AudioClip[] targetDestignation;

	// Token: 0x04000475 RID: 1141
	public AudioClip[] mortarClips;

	// Token: 0x04000476 RID: 1142
	public AudioClip[] explosionClips;

	// Token: 0x04000477 RID: 1143
	public AudioClip[] whizClips;

	// Token: 0x04000478 RID: 1144
	public AudioClipContainer[] groundShellHit;

	// Token: 0x04000479 RID: 1145
	public PlacementClipContainer[] placementSounds;

	// Token: 0x0400047A RID: 1146
	public AudioClip[] TeamElliminationSounds;

	// Token: 0x0400047B RID: 1147
	public AudioClip[] chatRadio;

	// Token: 0x0400047C RID: 1148
	public AudioClip[] TacticalConquestSounds;

	// Token: 0x0400047D RID: 1149
	public AudioClip brokenLimb;

	// Token: 0x0400047E RID: 1150
	public AudioClip SwitchOpticSound;

	// Token: 0x0400047F RID: 1151
	public AudioClip AdditionalAmmunitionSound;

	// Token: 0x04000480 RID: 1152
	public AudioClip ChangeWeaponSound;

	// Token: 0x04000481 RID: 1153
	public AudioClip[] ReloadSound;

	// Token: 0x04000482 RID: 1154
	public AudioClip[] CrouchSound;

	// Token: 0x04000483 RID: 1155
	public AudioClip[] AimSound;
}
