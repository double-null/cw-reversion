using System;
using System.Reflection;
using UnityEngine;

// Token: 0x020001D1 RID: 465
[AddComponentMenu("Scripts/Game/Components/ShellManager")]
internal class ShellManager : PoolableBehaviour
{
	// Token: 0x06000F99 RID: 3993 RVA: 0x000B2474 File Offset: 0x000B0674
	public override void OnPoolSpawn()
	{
		base.OnPoolSpawn();
		base.Invoke("Change", 0.5f);
		base.Invoke("Collide", 1f);
	}

	// Token: 0x06000F9A RID: 3994 RVA: 0x000B24A8 File Offset: 0x000B06A8
	public override void OnPoolDespawn()
	{
		base.rigidbody.velocity = Vector3.zero;
		base.gameObject.layer = LayerMask.NameToLayer("hands_render");
		base.OnPoolDespawn();
	}

	// Token: 0x06000F9B RID: 3995 RVA: 0x000B24E0 File Offset: 0x000B06E0
	[Obfuscation(Exclude = true)]
	public void Change()
	{
		base.gameObject.layer = 0;
	}

	// Token: 0x06000F9C RID: 3996 RVA: 0x000B24F0 File Offset: 0x000B06F0
	[Obfuscation(Exclude = true)]
	private void Collide()
	{
		if (ShellManager.lastSoundTime < Time.realtimeSinceStartup)
		{
			if ((CameraListener.Camera.transform.position - base.transform.position).magnitude < 20f)
			{
				int num = Mathf.RoundToInt(UnityEngine.Random.Range(-0.49f, (float)SingletoneForm<SoundFactory>.Instance.groundShellHit[(int)this.soundIndex].clips.Length - 0.51f));
				Audio.Play(base.transform.position, SingletoneForm<SoundFactory>.Instance.groundShellHit[(int)this.soundIndex].clips[num], -1f, -1f);
			}
			ShellManager.lastSoundTime = Time.realtimeSinceStartup + 0.2f;
		}
		SingletoneForm<PoolManager>.Instance[base.name].Despawn(base.GetComponent<PoolItem>());
	}

	// Token: 0x06000F9D RID: 3997 RVA: 0x000B25CC File Offset: 0x000B07CC
	private void OnDisable()
	{
	}

	// Token: 0x06000F9E RID: 3998 RVA: 0x000B25D0 File Offset: 0x000B07D0
	private void OnEnable()
	{
	}

	// Token: 0x04001010 RID: 4112
	public ShellSoundType soundIndex;

	// Token: 0x04001011 RID: 4113
	private static float lastSoundTime;
}
