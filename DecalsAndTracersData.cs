using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200001C RID: 28
public class DecalsAndTracersData : MonoBehaviour
{
	// Token: 0x17000005 RID: 5
	// (get) Token: 0x06000077 RID: 119 RVA: 0x00007E48 File Offset: 0x00006048
	public static DecalsAndTracersData Instance
	{
		get
		{
			if (DecalsAndTracersData._instance == null)
			{
				DecalsAndTracersData._instance = (DecalsAndTracersData)UnityEngine.Object.FindObjectOfType(typeof(DecalsAndTracersData));
			}
			return DecalsAndTracersData._instance;
		}
	}

	// Token: 0x06000078 RID: 120 RVA: 0x00007E84 File Offset: 0x00006084
	private void Awake()
	{
		DecalsAndTracersData._instance = this;
		DecalsAndTracersData.DecalSettings = new Dictionary<string, DecalsAndTracersData.Decals.Decal>();
		foreach (DecalsAndTracersData.Decals.Decal decal in DecalsAndTracersData.decals.decals)
		{
			DecalsAndTracersData.DecalSettings.Add(decal.name, decal);
			DecalsAndTracersData.DecalSettings.Add(decal.name + " (Instance)", decal);
		}
		DecalsAndTracersData._blood = DecalsAndTracersData.DecalSettings["blood"];
		Tracer.Initialize();
		LightPool.Initialize();
		this.Flash.Init();
	}

	// Token: 0x06000079 RID: 121 RVA: 0x00007F1C File Offset: 0x0000611C
	private void OnGUI()
	{
		FlashManager.Update();
	}

	// Token: 0x0600007A RID: 122 RVA: 0x00007F24 File Offset: 0x00006124
	private void Start()
	{
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00007F28 File Offset: 0x00006128
	private void Update()
	{
		if (this._mainCamera == null)
		{
			BaseClientGame clientGame = Peer.ClientGame;
			if (clientGame == null)
			{
				return;
			}
			ClientNetPlayer localPlayer = clientGame.LocalPlayer;
			if (localPlayer == null)
			{
				return;
			}
			Camera componentInChildren = localPlayer.GetComponentInChildren<Camera>();
			if (componentInChildren == null)
			{
				return;
			}
			this._mainCamera = componentInChildren.transform;
		}
		DecalsAndTracersData._cameraWarldPosition = this._mainCamera.position;
	}

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x0600007C RID: 124 RVA: 0x00007FA0 File Offset: 0x000061A0
	public static DecalsAndTracersData.Tracers particles
	{
		get
		{
			return DecalsAndTracersData.Instance._tracers;
		}
	}

	// Token: 0x17000007 RID: 7
	// (get) Token: 0x0600007D RID: 125 RVA: 0x00007FAC File Offset: 0x000061AC
	public static DecalsAndTracersData.Decals decals
	{
		get
		{
			return DecalsAndTracersData.Instance._decals;
		}
	}

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x0600007E RID: 126 RVA: 0x00007FB8 File Offset: 0x000061B8
	public static DecalsAndTracersData.Effects effects
	{
		get
		{
			return DecalsAndTracersData.Instance._effects;
		}
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00007FC4 File Offset: 0x000061C4
	public static void Create(RaycastHit hit, bool isKnife = false)
	{
		DecalsAndTracersData.Decals.Decal decal;
		if (!DecalsAndTracersData.DecalSettings.TryGetValue(hit.collider.material.name, out decal))
		{
			return;
		}
		DecalsAndTracersData.Create(hit, decal, isKnife);
	}

	// Token: 0x06000080 RID: 128 RVA: 0x00007FFC File Offset: 0x000061FC
	public static void Create(RaycastHit hit, DecalsAndTracersData.Decals.Decal decal, bool isKnife)
	{
		Vector3 point = hit.point;
		Vector3 normal = hit.normal;
		Vector3 vector = point + normal * UnityEngine.Random.Range(0.015f, 0.04f);
		float magnitude = (DecalsAndTracersData._cameraWarldPosition - point).magnitude;
		if (!isKnife && decal.decalObj != null)
		{
			decal.decalObj.Add(vector, normal);
		}
		if (magnitude > 20f)
		{
			if (decal.LOD != null)
			{
				decal.LOD.Emit(vector, normal);
			}
		}
		else
		{
			if (decal.Particles.Length > 0)
			{
				ParticleSystemAdapter[] particles = decal.Particles;
				int num = particles.Length;
				for (int i = 0; i < num; i++)
				{
					particles[i].Emit(vector, normal);
				}
			}
			if (decal.Clips.Length > 0 && DecalsAndTracersData._lastSoundTime < Time.realtimeSinceStartup)
			{
				Audio.Play(vector, decal.Clips[UnityEngine.Random.Range(0, decal.Clips.Length - 1)], 5f, 20f);
				DecalsAndTracersData._lastSoundTime = Time.realtimeSinceStartup + 0.2f;
			}
			if (decal.Drops != null)
			{
				ParticleSystemAdapter drops = decal.Drops;
				int num2 = UnityEngine.Random.Range(decal.DropsMinCount, decal.DropsMaxCount + 1);
				for (int j = 0; j < num2; j++)
				{
					drops.Emit(vector, normal + UnityEngine.Random.insideUnitSphere);
				}
			}
			if (decal.Light && magnitude < 10f)
			{
				LightPool.Add(vector, 1.5f, 5f, 0.1f);
			}
		}
	}

	// Token: 0x06000081 RID: 129 RVA: 0x000081B4 File Offset: 0x000063B4
	public static void CreateBody(RaycastHit hitBody, RaycastHit hitNext)
	{
		Vector3 point = hitBody.point;
		Vector3 normal = hitBody.normal;
		Vector3 pos = point + normal * UnityEngine.Random.Range(0.01f, 0.02f);
		float magnitude = (DecalsAndTracersData._cameraWarldPosition - point).magnitude;
		if (magnitude > 20f)
		{
			if (DecalsAndTracersData._blood.LOD != null)
			{
				DecalsAndTracersData._blood.LOD.Emit(pos, normal);
			}
		}
		else
		{
			if (DecalsAndTracersData._blood.Particles.Length > 0)
			{
				ParticleSystemAdapter[] particles = DecalsAndTracersData._blood.Particles;
				int num = particles.Length;
				for (int i = 0; i < num; i++)
				{
					particles[i].Emit(pos, normal);
				}
			}
			if (DecalsAndTracersData._blood.Clips.Length > 0 && DecalsAndTracersData._lastSoundTime < Time.realtimeSinceStartup)
			{
				Audio.Play(pos, DecalsAndTracersData._blood.Clips[UnityEngine.Random.Range(0, DecalsAndTracersData._blood.Clips.Length - 1)], 5f, 20f);
				DecalsAndTracersData._lastSoundTime = Time.realtimeSinceStartup + 0.2f;
			}
			if (DecalsAndTracersData._blood.Drops != null)
			{
				ParticleSystemAdapter drops = DecalsAndTracersData._blood.Drops;
				int num2 = UnityEngine.Random.Range(DecalsAndTracersData._blood.DropsMinCount, DecalsAndTracersData._blood.DropsMaxCount + 1);
				for (int j = 0; j < num2; j++)
				{
					drops.Emit(pos, normal + UnityEngine.Random.insideUnitSphere);
				}
			}
		}
		if (hitNext.distance - hitBody.distance < 8f)
		{
			DecalsAndTracersData._blood.decalObj.Add(hitNext.point + hitNext.normal * UnityEngine.Random.Range(0.01f, 0.02f), hitNext.normal);
		}
	}

	// Token: 0x06000082 RID: 130 RVA: 0x000083A0 File Offset: 0x000065A0
	public static void CreateBody(RaycastHit hitBody)
	{
		Vector3 point = hitBody.point;
		Vector3 normal = hitBody.normal;
		Vector3 pos = point + normal * UnityEngine.Random.Range(0.01f, 0.02f);
		float magnitude = (DecalsAndTracersData._cameraWarldPosition - point).magnitude;
		if (magnitude > 20f)
		{
			if (DecalsAndTracersData._blood.LOD != null)
			{
				DecalsAndTracersData._blood.LOD.Emit(pos, normal);
			}
		}
		else
		{
			if (DecalsAndTracersData._blood.Particles.Length > 0)
			{
				ParticleSystemAdapter[] particles = DecalsAndTracersData._blood.Particles;
				int num = particles.Length;
				for (int i = 0; i < num; i++)
				{
					particles[i].Emit(pos, normal);
				}
			}
			if (DecalsAndTracersData._blood.Clips.Length > 0 && DecalsAndTracersData._lastSoundTime < Time.realtimeSinceStartup)
			{
				Audio.Play(pos, DecalsAndTracersData._blood.Clips[UnityEngine.Random.Range(0, DecalsAndTracersData._blood.Clips.Length - 1)], 5f, 20f);
				DecalsAndTracersData._lastSoundTime = Time.realtimeSinceStartup + 0.2f;
			}
			if (DecalsAndTracersData._blood.Drops != null)
			{
				ParticleSystemAdapter drops = DecalsAndTracersData._blood.Drops;
				int num2 = UnityEngine.Random.Range(DecalsAndTracersData._blood.DropsMinCount, DecalsAndTracersData._blood.DropsMaxCount + 1);
				for (int j = 0; j < num2; j++)
				{
					drops.Emit(pos, normal + UnityEngine.Random.insideUnitSphere);
				}
			}
		}
	}

	// Token: 0x06000083 RID: 131 RVA: 0x00008534 File Offset: 0x00006734
	public static void CreateGrenade(Vector3 position)
	{
		position.y += 0.05f;
		DecalsAndTracersData.effects.effects[0].Emit(position, Vector3.up);
	}

	// Token: 0x06000084 RID: 132 RVA: 0x0000856C File Offset: 0x0000676C
	public static void CreateTracerFromTheSky(Vector3 position)
	{
		DecalsAndTracersData._instance._tracers.Smoke.Add(position + new Vector3(2f, 50f, 2f), position, DecalsAndTracersData.White, 6f, 5f);
	}

	// Token: 0x040000F5 RID: 245
	private static DecalsAndTracersData _instance;

	// Token: 0x040000F6 RID: 246
	public DecalsAndTracersData.Tracers _tracers;

	// Token: 0x040000F7 RID: 247
	public DecalsAndTracersData.Decals _decals;

	// Token: 0x040000F8 RID: 248
	public DecalsAndTracersData.Effects _effects;

	// Token: 0x040000F9 RID: 249
	public FlashManager Flash;

	// Token: 0x040000FA RID: 250
	private Transform _mainCamera;

	// Token: 0x040000FB RID: 251
	private static Vector3 _cameraWarldPosition;

	// Token: 0x040000FC RID: 252
	public static Dictionary<string, DecalsAndTracersData.Decals.Decal> DecalSettings;

	// Token: 0x040000FD RID: 253
	private static float _lastSoundTime;

	// Token: 0x040000FE RID: 254
	private static readonly Color32 White = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

	// Token: 0x040000FF RID: 255
	private static DecalsAndTracersData.Decals.Decal _blood;

	// Token: 0x0200001D RID: 29
	[Serializable]
	public class Tracers
	{
		// Token: 0x04000100 RID: 256
		public ParticleSystem FireSide;

		// Token: 0x04000101 RID: 257
		public ParticleSystem FireFront;

		// Token: 0x04000102 RID: 258
		public TracerSystem Smoke;
	}

	// Token: 0x0200001E RID: 30
	[Serializable]
	public class Decals
	{
		// Token: 0x04000103 RID: 259
		[ArrayCompact]
		public DecalsAndTracersData.Decals.Decal[] decals;

		// Token: 0x0200001F RID: 31
		[Serializable]
		public class Decal
		{
			// Token: 0x04000104 RID: 260
			public string name;

			// Token: 0x04000105 RID: 261
			public DecalSystem decalObj;

			// Token: 0x04000106 RID: 262
			public ParticleSystemAdapter LOD;

			// Token: 0x04000107 RID: 263
			public ParticleSystemAdapter[] Particles;

			// Token: 0x04000108 RID: 264
			public ParticleSystemAdapter Drops;

			// Token: 0x04000109 RID: 265
			public int DropsMinCount;

			// Token: 0x0400010A RID: 266
			public int DropsMaxCount;

			// Token: 0x0400010B RID: 267
			public AudioClip[] Clips;

			// Token: 0x0400010C RID: 268
			public bool Light;
		}
	}

	// Token: 0x02000020 RID: 32
	[Serializable]
	public class Effects
	{
		// Token: 0x0400010D RID: 269
		[ArrayCompact]
		public DecalsAndTracersData.Effects.Effect[] effects;

		// Token: 0x02000021 RID: 33
		[Serializable]
		public class Effect
		{
			// Token: 0x0600008A RID: 138 RVA: 0x000085E0 File Offset: 0x000067E0
			public void Emit(Vector3 position, Vector3 normal)
			{
				float magnitude = (DecalsAndTracersData._cameraWarldPosition - position).magnitude;
				for (int i = 0; i < this.Particles.Length; i++)
				{
					this.Particles[i].Emit(position, normal, magnitude);
				}
				if (this.Clips.Length > 0)
				{
					Audio.Play(position, this.Clips[FastRndom.Int(this.Clips.Length)], 5f, 20f);
				}
				if (this.Light && magnitude < this.LightLodDist)
				{
					LightPool.Add(position, this.LightRange, this.LightIntensity, this.LightTime);
				}
				if (this.Flash)
				{
					FlashManager.Add(position, this.FlashSize, this.FlashTime);
				}
			}

			// Token: 0x0400010E RID: 270
			public string name;

			// Token: 0x0400010F RID: 271
			public DecalsAndTracersData.Effects.Effect.ParticleSys[] Particles;

			// Token: 0x04000110 RID: 272
			public AudioClip[] Clips;

			// Token: 0x04000111 RID: 273
			public bool Flash;

			// Token: 0x04000112 RID: 274
			public float FlashSize;

			// Token: 0x04000113 RID: 275
			public float FlashTime;

			// Token: 0x04000114 RID: 276
			public bool Light;

			// Token: 0x04000115 RID: 277
			public float LightLodDist;

			// Token: 0x04000116 RID: 278
			public float LightRange;

			// Token: 0x04000117 RID: 279
			public float LightIntensity;

			// Token: 0x04000118 RID: 280
			public float LightTime;

			// Token: 0x02000022 RID: 34
			[Serializable]
			public class ParticleSys
			{
				// Token: 0x0600008C RID: 140 RVA: 0x000086B4 File Offset: 0x000068B4
				public void Emit(Vector3 position, Vector3 normal, float distance)
				{
					if (distance > this.LodDistance)
					{
						return;
					}
					switch (this.RandomType)
					{
					case DecalsAndTracersData.Effects.Effect.ParticleSys.Type.Once:
						this.Particle.Emit(position, normal);
						break;
					case DecalsAndTracersData.Effects.Effect.ParticleSys.Type.Normal:
					{
						int num = FastRndom.Int(this.MinCount, this.RandomCountRange);
						for (int i = 0; i < num; i++)
						{
							Vector3 vector = normal + FastRndom.VectorNormalized();
							vector = ((!this.UseRandomScale) ? vector : Vector3.Scale(vector, this.RandomScale));
							this.Particle.Emit(position, vector);
						}
						break;
					}
					case DecalsAndTracersData.Effects.Effect.ParticleSys.Type.Hemisphere:
					{
						int num2 = FastRndom.Int(this.MinCount, this.RandomCountRange);
						for (int j = 0; j < num2; j++)
						{
							Vector3 vector2 = FastRndom.VectorHemisphere();
							vector2 = ((!this.UseRandomScale) ? vector2 : Vector3.Scale(vector2, this.RandomScale));
							this.Particle.Emit(position, vector2);
						}
						break;
					}
					case DecalsAndTracersData.Effects.Effect.ParticleSys.Type.Circle:
					{
						int num3 = FastRndom.Int(this.MinCount, this.RandomCountRange);
						for (int k = 0; k < num3; k++)
						{
							Vector3 vector3 = FastRndom.VectorCircle();
							vector3 = ((!this.UseRandomScale) ? vector3 : Vector3.Scale(vector3, this.RandomScale));
							Vector3 pos = position;
							pos.y += 0.3f;
							this.Particle.Emit(pos, vector3);
						}
						break;
					}
					}
				}

				// Token: 0x04000119 RID: 281
				public ParticleSystemAdapter Particle;

				// Token: 0x0400011A RID: 282
				public float LodDistance;

				// Token: 0x0400011B RID: 283
				public DecalsAndTracersData.Effects.Effect.ParticleSys.Type RandomType;

				// Token: 0x0400011C RID: 284
				public int MinCount;

				// Token: 0x0400011D RID: 285
				public int RandomCountRange;

				// Token: 0x0400011E RID: 286
				public bool UseRandomScale;

				// Token: 0x0400011F RID: 287
				public Vector3 RandomScale;

				// Token: 0x02000023 RID: 35
				public enum Type
				{
					// Token: 0x04000121 RID: 289
					Once,
					// Token: 0x04000122 RID: 290
					Normal,
					// Token: 0x04000123 RID: 291
					Hemisphere,
					// Token: 0x04000124 RID: 292
					Circle
				}
			}
		}
	}
}
