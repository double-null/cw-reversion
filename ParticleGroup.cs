using System;
using UnityEngine;

// Token: 0x020001CE RID: 462
internal class ParticleGroup : MonoBehaviour
{
	// Token: 0x06000F8D RID: 3981 RVA: 0x000B1F34 File Offset: 0x000B0134
	private void Awake()
	{
		this.Check();
	}

	// Token: 0x06000F8E RID: 3982 RVA: 0x000B1F3C File Offset: 0x000B013C
	private void OnDrawGizmos()
	{
		this.Check();
	}

	// Token: 0x06000F8F RID: 3983 RVA: 0x000B1F44 File Offset: 0x000B0144
	private void Check()
	{
		if (this._meshRenderer == null)
		{
			this._meshRenderer = (base.GetComponent<MeshRenderer>() ?? base.gameObject.AddComponent<MeshRenderer>());
		}
		if (this._meshFilter == null)
		{
			this._meshFilter = (base.GetComponent<MeshFilter>() ?? base.gameObject.AddComponent<MeshFilter>());
		}
		if (this._positions == null || this._positions.Length != base.transform.childCount)
		{
			this._positions = new Vector3[base.transform.childCount];
			this._scales = new Vector3[base.transform.childCount];
			this._angles = new float[base.transform.childCount];
		}
		int num = 0;
		bool flag = false;
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			if (this._positions[num] != transform.localPosition || this._scales[num] != transform.localScale || this._angles[num] != transform.eulerAngles.x)
			{
				this._positions[num] = transform.localPosition;
				this._scales[num] = transform.localScale;
				this._angles[num] = transform.eulerAngles.x;
				flag = true;
			}
			num++;
		}
		if (flag)
		{
			this._meshFilter.mesh = this.GetMesh();
		}
	}

	// Token: 0x06000F90 RID: 3984 RVA: 0x000B2138 File Offset: 0x000B0338
	private Mesh GetMesh()
	{
		int num = this._positions.Length;
		int num2 = num << 2;
		Vector3[] array = new Vector3[num2];
		Vector2[] array2 = new Vector2[num2];
		Vector2[] array3 = new Vector2[num2];
		int[] array4 = new int[num * 6];
		int i = 0;
		int num3 = 0;
		while (i < num)
		{
			int num4 = i << 2;
			array4[num3++] = num4;
			array4[num3++] = num4 + 1;
			array4[num3++] = num4 + 2;
			array4[num3++] = num4 + 2;
			array4[num3++] = num4 + 3;
			array4[num3++] = num4;
			i++;
		}
		int j = 0;
		int num5 = 0;
		while (j < num)
		{
			int num6 = num5++;
			int num7 = num5++;
			int num8 = num5++;
			int num9 = num5++;
			array2[num6] = new Vector2(0f, 1f);
			array2[num7] = new Vector2(0f, 0f);
			array2[num8] = new Vector2(1f, 0f);
			array2[num9] = new Vector2(1f, 1f);
			Vector2 vector = this._scales[j];
			float f = this._angles[j];
			float num10 = Mathf.Cos(f);
			float num11 = Mathf.Sin(f);
			float num12 = vector.x * num10;
			float num13 = vector.y * -num11;
			float num14 = vector.x * num11;
			float num15 = vector.y * num10;
			array3[num6] = new Vector2(-num12 - num13, -num14 - num15);
			array3[num7] = new Vector2(-num12 + num13, -num14 + num15);
			array3[num8] = new Vector2(num12 + num13, num14 + num15);
			array3[num9] = new Vector2(num12 - num13, num14 - num15);
			array[num6] = (array[num7] = (array[num8] = (array[num9] = this._positions[j])));
			j++;
		}
		return new Mesh
		{
			vertices = array,
			uv = array2,
			uv1 = array3,
			triangles = array4
		};
	}

	// Token: 0x0400100B RID: 4107
	private Vector3[] _positions;

	// Token: 0x0400100C RID: 4108
	private Vector3[] _scales;

	// Token: 0x0400100D RID: 4109
	private float[] _angles;

	// Token: 0x0400100E RID: 4110
	private MeshRenderer _meshRenderer;

	// Token: 0x0400100F RID: 4111
	private MeshFilter _meshFilter;
}
