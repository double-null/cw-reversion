using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000026 RID: 38
[Serializable]
public class FlashManager
{
	// Token: 0x06000097 RID: 151 RVA: 0x00008AB8 File Offset: 0x00006CB8
	public void Init()
	{
		FlashManager.Texutre = this.Tex;
		FlashManager.Material = this.Mat;
	}

	// Token: 0x06000098 RID: 152 RVA: 0x00008AD0 File Offset: 0x00006CD0
	public static void Add(Vector3 pos, float size, float time)
	{
		FlashManager._flashs.AddLast(new FlashManager.Flash(pos, size, time));
	}

	// Token: 0x06000099 RID: 153 RVA: 0x00008AE8 File Offset: 0x00006CE8
	public static void Update()
	{
		if (FlashManager._flashs.Count == 0)
		{
			return;
		}
		if (!Event.current.type.Equals(EventType.Repaint))
		{
			return;
		}
		FlashManager._camera = (Camera.main ?? Camera.mainCamera);
		if (FlashManager._camera == null)
		{
			return;
		}
		FlashManager._cameraPos = FlashManager._camera.transform.position;
		float deltaTime = Time.deltaTime;
		LinkedListNode<FlashManager.Flash> linkedListNode = FlashManager._flashs.First;
		while (linkedListNode != null)
		{
			FlashManager.Flash value = linkedListNode.Value;
			if (value.Draw(deltaTime))
			{
				LinkedListNode<FlashManager.Flash> node = linkedListNode;
				linkedListNode = linkedListNode.Next;
				FlashManager._flashs.Remove(node);
			}
			else
			{
				linkedListNode = linkedListNode.Next;
			}
		}
	}

	// Token: 0x04000131 RID: 305
	public Texture Tex;

	// Token: 0x04000132 RID: 306
	public Material Mat;

	// Token: 0x04000133 RID: 307
	public static Texture Texutre;

	// Token: 0x04000134 RID: 308
	public static Material Material;

	// Token: 0x04000135 RID: 309
	private static Camera _camera;

	// Token: 0x04000136 RID: 310
	private static Vector3 _cameraPos;

	// Token: 0x04000137 RID: 311
	private static LinkedList<FlashManager.Flash> _flashs = new LinkedList<FlashManager.Flash>();

	// Token: 0x02000027 RID: 39
	private class Flash
	{
		// Token: 0x0600009A RID: 154 RVA: 0x00008BB0 File Offset: 0x00006DB0
		public Flash(Vector3 pos, float size, float time)
		{
			this._pos = pos;
			this._size = size;
			this._energy = 1f;
			this._fallof = 1f / time;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00008BEC File Offset: 0x00006DEC
		public bool Draw(float delta)
		{
			Vector3 vector = FlashManager._camera.WorldToScreenPoint(this._pos);
			this._energy -= this._fallof * delta;
			if (this._energy < 0f)
			{
				return true;
			}
			if (vector.z < 0f)
			{
				return false;
			}
			if (Physics.Linecast(this._pos, FlashManager._cameraPos, 4458496))
			{
				return false;
			}
			Vector3 vector2 = this._pos - FlashManager._cameraPos;
			int num = (int)(this._size / vector2.magnitude);
			int num2 = num >> 1;
			Rect screenRect = new Rect(vector.x - (float)num2, (float)Screen.height - vector.y - (float)num2, (float)num, (float)num);
			Graphics.DrawTexture(screenRect, FlashManager.Texutre, new Rect(0f, 0f, 1f, 1f), 0, 0, 0, 0, new Color(this._energy, this._energy, this._energy, 1f), FlashManager.Material);
			return false;
		}

		// Token: 0x04000138 RID: 312
		private Vector3 _pos;

		// Token: 0x04000139 RID: 313
		private float _size;

		// Token: 0x0400013A RID: 314
		private float _fallof;

		// Token: 0x0400013B RID: 315
		private float _energy;
	}
}
