using System;
using UnityEngine;

// Token: 0x02000028 RID: 40
public class ParticleSystemAdapter : MonoBehaviour
{
	// Token: 0x0600009D RID: 157 RVA: 0x00008D04 File Offset: 0x00006F04
	private void Awake()
	{
		if (this.ParticleSystemObject == null)
		{
			this.ParticleSystemObject = base.particleSystem;
		}
		this.Lifetime.Init();
		this.Speed.Init();
		this.Size.Init();
	}

	// Token: 0x0600009E RID: 158 RVA: 0x00008D50 File Offset: 0x00006F50
	public void Emit(Vector3 pos, Vector3 vel)
	{
		float value = this.Lifetime.Value;
		float value2 = this.Speed.Value;
		float value3 = this.Size.Value;
		float rotation = (!this.RotationRandom) ? this.Rotation : FastRndom.FloatRotation();
		this.ParticleSystemObject.Emit(new ParticleSystem.Particle
		{
			position = pos,
			lifetime = value,
			startLifetime = value,
			velocity = vel * value2,
			randomSeed = FastRndom.Uint(),
			color = this.Color,
			size = value3,
			rotation = rotation
		});
	}

	// Token: 0x0600009F RID: 159 RVA: 0x00008E04 File Offset: 0x00007004
	public void Emit(Vector3 pos, Vector3 vel, float sizeMult)
	{
		float value = this.Lifetime.Value;
		float value2 = this.Speed.Value;
		float num = this.Size.Value;
		float rotation = (!this.RotationRandom) ? this.Rotation : FastRndom.FloatRotation();
		num *= sizeMult;
		this.ParticleSystemObject.Emit(new ParticleSystem.Particle
		{
			position = pos,
			lifetime = value,
			startLifetime = value,
			velocity = vel * value2,
			randomSeed = FastRndom.Uint(),
			color = this.Color,
			size = num,
			rotation = rotation
		});
	}

	// Token: 0x0400013C RID: 316
	public ParticleSystem ParticleSystemObject;

	// Token: 0x0400013D RID: 317
	public RundomRange Lifetime;

	// Token: 0x0400013E RID: 318
	public RundomRange Speed;

	// Token: 0x0400013F RID: 319
	public RundomRange Size;

	// Token: 0x04000140 RID: 320
	public bool RotationRandom;

	// Token: 0x04000141 RID: 321
	public float Rotation;

	// Token: 0x04000142 RID: 322
	public Color32 Color;
}
