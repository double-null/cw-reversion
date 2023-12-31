using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000298 RID: 664
[AddComponentMenu("Scripts/Game/Foundation/HitBody")]
public class HitBody : PoolableBehaviour
{
	// Token: 0x0600128D RID: 4749 RVA: 0x000CADB4 File Offset: 0x000C8FB4
	public override void OnPoolDespawn()
	{
		this.infos.Clear();
		base.OnPoolDespawn();
	}

	// Token: 0x0600128E RID: 4750 RVA: 0x000CADC8 File Offset: 0x000C8FC8
	public void Add(float time)
	{
		eCache.HitInfo.Clear();
		eCache.HitInfo.Time = time;
		this.GetHitInfoUnit(ref eCache.HitInfo.proxy, this.proxy);
		this.GetHitInfoUnit(ref eCache.HitInfo.NPC_Pelvis, this.NPC_Pelvis.transform);
		this.GetHitInfoUnit(ref eCache.HitInfo.NPC_L_Thigh, this.NPC_L_Thigh.transform);
		this.GetHitInfoUnit(ref eCache.HitInfo.NPC_L_Calf, this.NPC_L_Calf.transform);
		this.GetHitInfoUnit(ref eCache.HitInfo.NPC_R_Thigh, this.NPC_R_Thigh.transform);
		this.GetHitInfoUnit(ref eCache.HitInfo.NPC_R_Calf, this.NPC_R_Calf.transform);
		this.GetHitInfoUnit(ref eCache.HitInfo.NPC_Spine1, this.NPC_Spine1.transform);
		this.GetHitInfoUnit(ref eCache.HitInfo.NPC_L_UpperArm, this.NPC_L_UpperArm.transform);
		this.GetHitInfoUnit(ref eCache.HitInfo.NPC_L_Forearm, this.NPC_L_Forearm.transform);
		this.GetHitInfoUnit(ref eCache.HitInfo.NPC_R_UpperArm, this.NPC_R_UpperArm.transform);
		this.GetHitInfoUnit(ref eCache.HitInfo.NPC_R_Forearm, this.NPC_R_Forearm.transform);
		this.GetHitInfoUnit(ref eCache.HitInfo.NPC_Head, this.NPC_Head.transform);
		this.infos.Add(eCache.HitInfo);
	}

	// Token: 0x0600128F RID: 4751 RVA: 0x000CAF3C File Offset: 0x000C913C
	public void RaycastAll(Ray ray, float time, List<eRaycastHit> hits)
	{
		if (this.infos.Length == 0)
		{
			return;
		}
		HitInfo hitInfo = null;
		HitInfo hitInfo2 = null;
		float num = 0f;
		ArrayUtility.Interpolate<HitInfo>(this.infos, out hitInfo2, out hitInfo, time, out num);
		Vector3 point = Vector3.Lerp(hitInfo2.proxy.pos, hitInfo.proxy.pos, num);
		if (Utility.DistancePointLine(point, ray.origin, ray.origin + ray.direction * 1000f) > 1f)
		{
			return;
		}
		this.SetHitInfoUnit(hitInfo.NPC_Pelvis, hitInfo2.NPC_Pelvis, num, this.NPC_Pelvis, ray, ref hits);
		this.SetHitInfoUnit(hitInfo.NPC_L_Thigh, hitInfo2.NPC_L_Thigh, num, this.NPC_L_Thigh, ray, ref hits);
		this.SetHitInfoUnit(hitInfo.NPC_L_Calf, hitInfo2.NPC_L_Calf, num, this.NPC_L_Calf, ray, ref hits);
		this.SetHitInfoUnit(hitInfo.NPC_R_Thigh, hitInfo2.NPC_R_Thigh, num, this.NPC_R_Thigh, ray, ref hits);
		this.SetHitInfoUnit(hitInfo.NPC_R_Calf, hitInfo2.NPC_R_Calf, num, this.NPC_R_Calf, ray, ref hits);
		this.SetHitInfoUnit(hitInfo.NPC_Spine1, hitInfo2.NPC_Spine1, num, this.NPC_Spine1, ray, ref hits);
		this.SetHitInfoUnit(hitInfo.NPC_L_UpperArm, hitInfo2.NPC_L_UpperArm, num, this.NPC_L_UpperArm, ray, ref hits);
		this.SetHitInfoUnit(hitInfo.NPC_L_Forearm, hitInfo2.NPC_L_Forearm, num, this.NPC_L_Forearm, ray, ref hits);
		this.SetHitInfoUnit(hitInfo.NPC_R_UpperArm, hitInfo2.NPC_R_UpperArm, num, this.NPC_R_UpperArm, ray, ref hits);
		this.SetHitInfoUnit(hitInfo.NPC_R_Forearm, hitInfo2.NPC_R_Forearm, num, this.NPC_R_Forearm, ray, ref hits);
		this.SetHitInfoUnit(hitInfo.NPC_Head, hitInfo2.NPC_Head, num, this.NPC_Head, ray, ref hits);
	}

	// Token: 0x06001290 RID: 4752 RVA: 0x000CB0FC File Offset: 0x000C92FC
	private void GetHitInfoUnit(ref HitInfoUnit unit, Transform capsule)
	{
		unit.pos = capsule.position;
		unit.euler = capsule.rotation.eulerAngles;
	}

	// Token: 0x06001291 RID: 4753 RVA: 0x000CB12C File Offset: 0x000C932C
	private void SetHitInfoUnit(HitInfoUnit father, HitInfoUnit child, float k, CapsuleCollider capsule, Ray ray, ref List<eRaycastHit> hits)
	{
		Vector3 position = Vector3.Lerp(child.pos, father.pos, k);
		Quaternion rotation = Quaternion.Lerp(Quaternion.Euler(child.euler), Quaternion.Euler(father.euler), k);
		if (CVars.g_hitsShowTime != 0f)
		{
			Utility.DrawCapsule(capsule, position, rotation, Color.red, CVars.g_hitsShowTime);
		}
		eRaycastHit[] array = PhysicsUtility.CapsuleCastAll(ray, capsule, position, rotation);
		if (array.Length == 2)
		{
			array[0].transform = capsule.transform;
			array[1].transform = capsule.transform;
			array[0].MaterialName = "blood";
			array[1].MaterialName = "blood";
			if ((array[0].point - ray.origin).magnitude > (array[1].point - ray.origin).magnitude)
			{
				hits.Add(array[1]);
			}
			else
			{
				hits.Add(array[0]);
			}
		}
		else if (array.Length == 1)
		{
			array[0].transform = capsule.transform;
			array[0].MaterialName = "blood";
			hits.Add(array[0]);
		}
	}

	// Token: 0x0400154E RID: 5454
	private ClassArray<HitInfo> infos = new ClassArray<HitInfo>(CVars.g_tickrate);

	// Token: 0x0400154F RID: 5455
	public Transform proxy;

	// Token: 0x04001550 RID: 5456
	public CapsuleCollider NPC_Pelvis;

	// Token: 0x04001551 RID: 5457
	public CapsuleCollider NPC_L_Thigh;

	// Token: 0x04001552 RID: 5458
	public CapsuleCollider NPC_L_Calf;

	// Token: 0x04001553 RID: 5459
	public CapsuleCollider NPC_R_Thigh;

	// Token: 0x04001554 RID: 5460
	public CapsuleCollider NPC_R_Calf;

	// Token: 0x04001555 RID: 5461
	public CapsuleCollider NPC_Spine1;

	// Token: 0x04001556 RID: 5462
	public CapsuleCollider NPC_Head;

	// Token: 0x04001557 RID: 5463
	public CapsuleCollider NPC_L_UpperArm;

	// Token: 0x04001558 RID: 5464
	public CapsuleCollider NPC_L_Forearm;

	// Token: 0x04001559 RID: 5465
	public CapsuleCollider NPC_R_UpperArm;

	// Token: 0x0400155A RID: 5466
	public CapsuleCollider NPC_R_Forearm;
}
