using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200006F RID: 111
[AddComponentMenu("Scripts/Engine/DecalFactory")]
public class DecalFactory : SingletoneForm<DecalFactory>
{
	// Token: 0x060001EE RID: 494 RVA: 0x00010EA8 File Offset: 0x0000F0A8
	public static void Create(RaycastHit hit, bool forceCreation = false, bool destroyDecal = false)
	{
		if (!CameraListener.Camera)
		{
			return;
		}
		PhysicMaterial material = hit.collider.material;
		string name = material.name;
		for (int i = 0; i < SingletoneForm<DecalFactory>.Instance.surfaces.Count; i++)
		{
			SurfaceDescription surfaceDescription = SingletoneForm<DecalFactory>.Instance.surfaces[i];
			if (name.Contains(surfaceDescription.type.ToString()))
			{
				GameObject gameObject = SingletoneForm<PoolManager>.Instance["hole"].Spawn();
				GameObject gameObject2 = SingletoneForm<PoolManager>.Instance[surfaceDescription.prefabName].Spawn();
				gameObject.transform.parent = Main.Trash.transform;
				gameObject.transform.position = hit.point + hit.normal * (UnityEngine.Random.value * 0.03f);
				gameObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
				DecalFactory.HolePool = gameObject.GetComponent<PoolItem>();
				DecalFactory.HolePool.AutoDespawn(gameObject2.GetComponent<ParticleLife>().LifeTime);
				Utility.ChangeParent(gameObject2.transform, gameObject.transform);
				if (((CameraListener.Camera.transform.position - hit.point).magnitude > 20f && !hit.collider.material.name.Contains("blood")) || Main.UserInfo.settings.graphics.Level == QualityLevelUser.VeryLow)
				{
					if (surfaceDescription.effectReplacerName != string.Empty)
					{
						GameObject gameObject3 = SingletoneForm<PoolManager>.Instance[surfaceDescription.effectReplacerName].Spawn();
						Utility.ChangeParent(gameObject3.transform, gameObject.transform);
						DecalFactory.HolePool.Childs.Add(gameObject3.GetComponent<PoolItem>());
					}
				}
				else
				{
					if (DecalFactory.lastSoundTime < Time.realtimeSinceStartup)
					{
						Audio.Play(gameObject2.transform.position, surfaceDescription.sounds[Mathf.RoundToInt(UnityEngine.Random.Range(-0.49f, (float)surfaceDescription.sounds.Count - 0.501f))], 5f, 20f);
						DecalFactory.lastSoundTime = Time.realtimeSinceStartup + 0.2f;
					}
					for (int j = 0; j < surfaceDescription.particlesNames.Length; j++)
					{
						GameObject gameObject4 = SingletoneForm<PoolManager>.Instance[surfaceDescription.particlesNames[j]].Spawn();
						Utility.ChangeParent(gameObject4.transform, gameObject.transform);
						DecalFactory.HolePool.Childs.Add(gameObject4.GetComponent<PoolItem>());
					}
					int num = (int)surfaceDescription.minCount + (int)((float)((int)surfaceDescription.maxCount - (int)surfaceDescription.minCount) * UnityEngine.Random.value);
					for (int k = 0; k < num; k++)
					{
						ScrapDescription scrapDescription = surfaceDescription.scraps[(int)((float)surfaceDescription.scraps.Count * UnityEngine.Random.value)];
						GameObject gameObject5 = SingletoneForm<PoolManager>.Instance[scrapDescription.prefabName].Spawn();
						Utility.ChangeParent(gameObject5.transform, gameObject.transform);
						gameObject5.transform.position = gameObject.transform.position;
						float num2 = scrapDescription.minPower + (scrapDescription.maxPower - scrapDescription.minPower) * UnityEngine.Random.value;
						Vector3 b = Quaternion.Slerp(Quaternion.FromToRotation(Vector3.up, hit.normal), UnityEngine.Random.rotation, 0.3f) * Vector3.up;
						gameObject5.rigidbody.velocity = (hit.normal / 2f + b).normalized * num2;
						gameObject5.rigidbody.angularVelocity.Set(num2 * UnityEngine.Random.value * 20f, 0f, 0f);
						DecalFactory.HolePool.Childs.Add(gameObject5.GetComponent<PoolItem>());
					}
				}
				if (destroyDecal)
				{
					SingletoneForm<PoolManager>.Instance[gameObject2.name].Despawn(gameObject2.GetComponent<PoolItem>());
				}
				else
				{
					DecalFactory.HolePool.Childs.Add(gameObject2.GetComponent<PoolItem>());
				}
				break;
			}
		}
	}

	// Token: 0x060001EF RID: 495 RVA: 0x00011314 File Offset: 0x0000F514
	public static void Create(EffectType type, Vector3 point)
	{
		for (int i = 0; i < SingletoneForm<DecalFactory>.Instance.effects.Count; i++)
		{
			if (SingletoneForm<DecalFactory>.Instance.effects[i].type == type)
			{
				GameObject gameObject = SingletoneForm<PoolManager>.Instance[SingletoneForm<DecalFactory>.Instance.effects[i].prefabName].Spawn();
				gameObject.GetComponent<PoolItem>().AutoDespawn(3f);
				gameObject.transform.parent = Main.Trash;
				gameObject.transform.position = point;
				return;
			}
		}
	}

	// Token: 0x060001F0 RID: 496 RVA: 0x000113B0 File Offset: 0x0000F5B0
	public override void OnDisconnect()
	{
		DecalFactory.lastSoundTime = 0f;
		SingletoneForm<DecalFactory>.instance = null;
	}

	// Token: 0x04000265 RID: 613
	public List<SurfaceDescription> surfaces = new List<SurfaceDescription>();

	// Token: 0x04000266 RID: 614
	public List<EffectDescription> effects = new List<EffectDescription>();

	// Token: 0x04000267 RID: 615
	public static float lastSoundTime;

	// Token: 0x04000268 RID: 616
	private static PoolItem HolePool;
}
