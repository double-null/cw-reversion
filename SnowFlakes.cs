using System;
using UnityEngine;

// Token: 0x02000005 RID: 5
public class SnowFlakes : MonoBehaviour
{
	// Token: 0x06000008 RID: 8 RVA: 0x00002358 File Offset: 0x00000558
	private string AddLog(string n, object t)
	{
		return string.Format("\n {0,0}:\t\t\t\t\t{1,-60}", n, t);
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00002368 File Offset: 0x00000568
	private void Start()
	{
		this._lowMemory = (SystemInfo.systemMemorySize < 2000);
		if (!this._lowMemory)
		{
			this._flakesRenderers = new MeshRenderer[4];
			this._flakesRenderers[0] = this.AddObject(8191, this.Close);
			this._flakesRenderers[1] = this.AddObject(16383, this.Close);
			this._flakesRenderers[2] = this.AddObject(16383, this.Far);
			this._flakesRenderers[3] = this.AddObject(16383, this.Far);
		}
		else
		{
			this._flakesRenderers = new MeshRenderer[2];
			this._flakesRenderers[0] = this.AddObject(8191, this.Close);
			this._flakesRenderers[1] = this.AddObject(16383, this.Close);
		}
		PerformanceMeter.OnFPSChange += this.OnFPSChange;
		UtilsScreen.OnScreenChange += this.OnScreenChange;
		this.OnScreenChange();
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002470 File Offset: 0x00000670
	private void OnFPSChange()
	{
		if (this._flakesRenderers == null || this._flakesRenderers[0] == null || !this._flakesRenderers[0].gameObject)
		{
			return;
		}
		PerformanceMeter.PerfRating performanceRating = PerformanceMeter.PerformanceRating;
		if (performanceRating >= PerformanceMeter.PerfRating.FPS60)
		{
			this._flakesRenderers[0].gameObject.SetActive(true);
			this._flakesRenderers[1].gameObject.SetActive(true);
			if (!this._lowMemory)
			{
				this._flakesRenderers[2].gameObject.SetActive(true);
				this._flakesRenderers[3].gameObject.SetActive(true);
			}
		}
		else if (performanceRating >= PerformanceMeter.PerfRating.FPS36)
		{
			this._flakesRenderers[0].gameObject.SetActive(false);
			this._flakesRenderers[1].gameObject.SetActive(true);
			if (!this._lowMemory)
			{
				this._flakesRenderers[2].gameObject.SetActive(true);
				this._flakesRenderers[3].gameObject.SetActive(false);
			}
		}
		else if (performanceRating >= PerformanceMeter.PerfRating.FPS24)
		{
			this._flakesRenderers[0].gameObject.SetActive(true);
			this._flakesRenderers[1].gameObject.SetActive(false);
			if (!this._lowMemory)
			{
				this._flakesRenderers[2].gameObject.SetActive(false);
				this._flakesRenderers[3].gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x0600000B RID: 11 RVA: 0x000025E0 File Offset: 0x000007E0
	private void OnScreenChange()
	{
		this.Close.SetVector("_Size", new Vector2((float)Screen.height / (float)Screen.width, 1f) * this.CloseSize);
		this.Far.SetVector("_Size", new Vector2((float)Screen.height / (float)Screen.width, 1f) * this.FarSize);
	}

	// Token: 0x0600000C RID: 12 RVA: 0x0000265C File Offset: 0x0000085C
	private void Update()
	{
		if (!Thermal.Exist || this._lastThermal == Thermal.On)
		{
			return;
		}
		this._lastThermal = Thermal.On;
		foreach (MeshRenderer meshRenderer in this._flakesRenderers)
		{
			meshRenderer.enabled = !this._lastThermal;
		}
	}

	// Token: 0x0600000D RID: 13 RVA: 0x000026C0 File Offset: 0x000008C0
	private MeshRenderer AddObject(int particlesCount, Material material)
	{
		GameObject gameObject = new GameObject("SnowFlakes(" + particlesCount + ")");
		gameObject.transform.parent = base.transform;
		gameObject.AddComponent<MeshFilter>().mesh = SnowFlakes.GetMesh(particlesCount);
		MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
		meshRenderer.castShadows = false;
		meshRenderer.material = material;
		return meshRenderer;
	}

	// Token: 0x0600000E RID: 14 RVA: 0x00002720 File Offset: 0x00000920
	private static Mesh GetMesh(int count)
	{
		int num = count << 2;
		Vector3[] array = new Vector3[num];
		Vector4[] array2 = new Vector4[num];
		Vector3[] array3 = new Vector3[num];
		Vector2[] array4 = new Vector2[num];
		Vector2[] array5 = new Vector2[num];
		int[] array6 = new int[count * 6];
		int i = 0;
		int num2 = 0;
		while (i < count)
		{
			int num3 = i << 2;
			array6[num2++] = num3;
			array6[num2++] = num3 + 1;
			array6[num2++] = num3 + 2;
			array6[num2++] = num3 + 2;
			array6[num2++] = num3 + 3;
			array6[num2++] = num3;
			i++;
		}
		int j = 0;
		int num4 = 0;
		while (j < count)
		{
			int num5 = num4++;
			int num6 = num4++;
			int num7 = num4++;
			int num8 = num4++;
			array4[num5] = new Vector2(0f, 1f);
			array4[num6] = new Vector2(0f, 0f);
			array4[num7] = new Vector2(1f, 0f);
			array4[num8] = new Vector2(1f, 1f);
			Vector2 vector = new Vector2(UnityEngine.Random.value - 0.5f, 0f);
			float num9 = (UnityEngine.Random.value - 0.5f) * 0.3f;
			vector.y = ((vector.x <= 0f) ? (-1f - vector.x) : (1f - vector.x));
			Vector2 vector2 = new Vector2(-vector.y, vector.x + num9);
			array5[num5] = vector;
			array5[num6] = vector2;
			vector.y -= num9;
			array5[num7] = -vector;
			vector2.x += num9;
			array5[num8] = -vector2;
			array[num5] = (array[num6] = (array[num7] = (array[num8] = new Vector3(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value))));
			array2[num5] = (array2[num6] = (array2[num7] = (array2[num8] = new Vector4((UnityEngine.Random.value - 0.5f) * 100f, (UnityEngine.Random.value - 0.5f) * 100f, (UnityEngine.Random.value - 0.5f) * 100f, (UnityEngine.Random.value - 0.5f) * 100f))));
			array3[num5] = (array3[num6] = (array3[num7] = (array3[num8] = Vector3.zero)));
			j++;
		}
		return new Mesh
		{
			vertices = array,
			normals = array3,
			tangents = array2,
			uv = array4,
			uv1 = array5,
			triangles = array6,
			bounds = new Bounds(Vector3.zero, Vector3.one * float.MaxValue)
		};
	}

	// Token: 0x0400000C RID: 12
	private const int MaxCount = 16383;

	// Token: 0x0400000D RID: 13
	private const int MidCount = 8191;

	// Token: 0x0400000E RID: 14
	private const int MinCount = 4095;

	// Token: 0x0400000F RID: 15
	public float CloseSize;

	// Token: 0x04000010 RID: 16
	public float FarSize;

	// Token: 0x04000011 RID: 17
	public Material Close;

	// Token: 0x04000012 RID: 18
	public Material Far;

	// Token: 0x04000013 RID: 19
	private MeshRenderer[] _flakesRenderers;

	// Token: 0x04000014 RID: 20
	private bool _lastThermal;

	// Token: 0x04000015 RID: 21
	private bool _lowMemory;
}
