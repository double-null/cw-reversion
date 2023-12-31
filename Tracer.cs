using System;
using UnityEngine;

// Token: 0x0200002A RID: 42
public static class Tracer
{
	// Token: 0x060000A3 RID: 163 RVA: 0x00008F08 File Offset: 0x00007108
	public static void Initialize()
	{
		Tracer._psSmoke = DecalsAndTracersData.particles.Smoke;
		Tracer._psFireSide = DecalsAndTracersData.particles.FireSide;
		Tracer._psFireFront = DecalsAndTracersData.particles.FireFront;
		StartData.UpdateForStatic += Tracer.Update;
		Tracer._psRendererFireSide = Tracer._psFireSide.GetComponent<ParticleSystemRenderer>();
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x00008F64 File Offset: 0x00007164
	private static void Update()
	{
		if (Tracer._psRendererFireSide == null)
		{
			return;
		}
		Tracer._dt = Time.deltaTime;
		Tracer._psRendererFireSide.lengthScale = 5500f * Tracer._dt;
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x00008FA4 File Offset: 0x000071A4
	public static void Create(Vector3 start, Vector3 end, byte alpha, bool doubleSpeed)
	{
		Vector3 a = end - start;
		float magnitude = a.magnitude;
		if (magnitude < 5f)
		{
			return;
		}
		Vector3 a2 = a / magnitude;
		Vector3 velocity = a2 * ((!doubleSpeed) ? 150f : 300f);
		float lifetime = magnitude * ((!doubleSpeed) ? 0.006666667f : 0.0033333334f);
		start += a2 * 3f;
		Color32 color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, alpha);
		Tracer._psFireSide.Emit(start, velocity, 0.027999999f, lifetime, color);
		Tracer._psFireFront.Emit(start, velocity, 0.04f, lifetime, color);
		Tracer._psSmoke.Add(start, end, color);
	}

	// Token: 0x04000146 RID: 326
	private const float BulletSpeed = 150f;

	// Token: 0x04000147 RID: 327
	private static ParticleSystem _psFireSide;

	// Token: 0x04000148 RID: 328
	private static ParticleSystem _psFireFront;

	// Token: 0x04000149 RID: 329
	private static TracerSystem _psSmoke;

	// Token: 0x0400014A RID: 330
	private static ParticleSystemRenderer _psRendererFireSide;

	// Token: 0x0400014B RID: 331
	private static float _dt;
}
