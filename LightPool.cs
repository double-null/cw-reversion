using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000024 RID: 36
public class LightPool
{
	// Token: 0x0600008F RID: 143 RVA: 0x00008864 File Offset: 0x00006A64
	public static void Initialize()
	{
		StartData.UpdateForStatic += LightPool.Update;
	}

	// Token: 0x06000090 RID: 144 RVA: 0x00008878 File Offset: 0x00006A78
	private static void Update()
	{
		if (LightPool._used.Count == 0)
		{
			return;
		}
		float deltaTime = Time.deltaTime;
		LinkedListNode<LightPool.LightStruct> linkedListNode = LightPool._used.First;
		while (linkedListNode != null)
		{
			LightPool.LightStruct value = linkedListNode.Value;
			if (value.DeathTime < Time.time)
			{
				value.LightScript.enabled = false;
				LinkedListNode<LightPool.LightStruct> node = linkedListNode;
				linkedListNode = linkedListNode.Next;
				LightPool._used.Remove(node);
				LightPool._free.AddLast(node);
			}
			else
			{
				linkedListNode = linkedListNode.Next;
				value.Update(deltaTime);
			}
		}
	}

	// Token: 0x06000091 RID: 145 RVA: 0x00008908 File Offset: 0x00006B08
	public static void Add(Vector3 position, float range = 1.5f, float intensity = 5f, float time = 0.1f)
	{
		LightPool.LightStruct lightStruct;
		if (LightPool._free.Count == 0)
		{
			lightStruct = new LightPool.LightStruct();
			lightStruct.LightObject = new GameObject("LightOfPool", new Type[]
			{
				typeof(Light)
			});
			lightStruct.LightObject.hideFlags = HideFlags.HideInHierarchy;
			lightStruct.LightTransform = lightStruct.LightObject.transform;
			lightStruct.LightScript = lightStruct.LightObject.light;
			lightStruct.LightScript.range = 1.5f;
			lightStruct.LightScript.color = new Color32(byte.MaxValue, 212, 116, byte.MaxValue);
			LightPool._used.AddLast(lightStruct);
		}
		else
		{
			LinkedListNode<LightPool.LightStruct> first = LightPool._free.First;
			LightPool._free.Remove(first);
			LightPool._used.AddLast(first);
			lightStruct = first.Value;
		}
		lightStruct.Start(time, range, intensity, position);
	}

	// Token: 0x04000125 RID: 293
	private const float LifeTime = 0.1f;

	// Token: 0x04000126 RID: 294
	private const float StartIntensity = 5f;

	// Token: 0x04000127 RID: 295
	private const float Fallof = 50f;

	// Token: 0x04000128 RID: 296
	private static LinkedList<LightPool.LightStruct> _free = new LinkedList<LightPool.LightStruct>();

	// Token: 0x04000129 RID: 297
	private static LinkedList<LightPool.LightStruct> _used = new LinkedList<LightPool.LightStruct>();

	// Token: 0x0400012A RID: 298
	private static GameObject _instance;

	// Token: 0x02000025 RID: 37
	private class LightStruct
	{
		// Token: 0x06000093 RID: 147 RVA: 0x000089FC File Offset: 0x00006BFC
		public void Start(float time, float range, float intensity, Vector3 position)
		{
			this.DeathTime = Time.time + time;
			this.RangeFallof = range / time;
			this.IntensityFallof = intensity / time;
			this.LightScript.enabled = true;
			this.LightScript.range = range;
			this.LightScript.intensity = 5f;
			this.LightTransform.position = position;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00008A60 File Offset: 0x00006C60
		public void Update(float deltaTime)
		{
			this.LightScript.intensity -= this.IntensityFallof * deltaTime;
			this.LightScript.range -= this.RangeFallof * deltaTime;
		}

		// Token: 0x0400012B RID: 299
		public Light LightScript;

		// Token: 0x0400012C RID: 300
		public GameObject LightObject;

		// Token: 0x0400012D RID: 301
		public Transform LightTransform;

		// Token: 0x0400012E RID: 302
		public float DeathTime;

		// Token: 0x0400012F RID: 303
		public float RangeFallof;

		// Token: 0x04000130 RID: 304
		public float IntensityFallof;
	}
}
