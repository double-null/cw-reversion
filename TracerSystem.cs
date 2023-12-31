using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200002B RID: 43
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class TracerSystem : MonoBehaviour
{
	// Token: 0x060000A7 RID: 167 RVA: 0x000090B8 File Offset: 0x000072B8
	private void Awake()
	{
		if (SystemInfo.graphicsShaderLevel < 20)
		{
			base.enabled = false;
			return;
		}
		this._count2 = this.Count << 1;
		this._changedParticles = new LinkedList<TracerSystem.Particle>();
		this._particles = new TracerSystem.Particle[this.Count];
		for (int i = 0; i < this.Count; i++)
		{
			this._particles[i] = new TracerSystem.Particle();
			this._particles[i].Pos4 = i << 3;
		}
		int num = this._count2 << 2;
		this._vertices = new Vector3[num];
		this._normals = new Vector3[num];
		this._tangents = new Vector4[num];
		this._uv = new Vector2[num];
		this._uv2Vals = new Vector2[num];
		this._colors = new Color32[num];
		this._triangles = new int[this._count2 * 6];
		int j = 0;
		int num2 = 0;
		while (j < this._count2)
		{
			int num3 = j << 2;
			this._triangles[num2++] = num3;
			this._triangles[num2++] = num3 + 1;
			this._triangles[num2++] = num3 + 2;
			this._triangles[num2++] = num3 + 2;
			this._triangles[num2++] = num3 + 3;
			this._triangles[num2++] = num3;
			j++;
		}
		int k = 0;
		int num4 = 0;
		while (k < this._count2)
		{
			this._uv[num4++] = new Vector2(0f, 0f);
			this._uv[num4++] = new Vector2(0f, 1f);
			this._uv[num4++] = new Vector2(1f, 1f);
			this._uv[num4++] = new Vector2(1f, 0f);
			k++;
		}
		this._mesh = new Mesh
		{
			vertices = this._vertices,
			normals = this._normals,
			tangents = this._tangents,
			uv = this._uv,
			uv2 = this._uv2Vals,
			triangles = this._triangles
		};
		this._mesh.MarkDynamic();
		this._mesh.bounds = new Bounds(Vector3.zero, Vector3.zero);
		base.GetComponent<MeshFilter>().mesh = this._mesh;
		this._material = base.GetComponent<Renderer>().sharedMaterial;
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x00009370 File Offset: 0x00007570
	private void LateUpdate()
	{
		if (this._changedParticles.Count == 0)
		{
			return;
		}
		foreach (TracerSystem.Particle particle in this._changedParticles)
		{
			particle.Update(this._vertices, this._uv2Vals, this._colors);
		}
		this._changedParticles.Clear();
		this._mesh.vertices = this._vertices;
		this._mesh.uv1 = this._uv2Vals;
		this._mesh.colors32 = this._colors;
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x00009438 File Offset: 0x00007638
	private void Update()
	{
		float time = Time.time;
		this._material.SetFloat("_TimeTime", time);
	}

	// Token: 0x060000AA RID: 170 RVA: 0x0000945C File Offset: 0x0000765C
	public void Add(Vector3 start, Vector3 end, Color32 color)
	{
		if (this._particles == null)
		{
			return;
		}
		TracerSystem.Particle particle = this._particles[this._currentParticleId];
		if (++this._currentParticleId >= this.Count)
		{
			this._currentParticleId = 0;
		}
		this._changedParticles.AddLast(particle);
		particle.Calc(start, end, this.Size, 1f, color);
		this.CalcBounds(start);
		this.CalcBounds(end);
	}

	// Token: 0x060000AB RID: 171 RVA: 0x000094D8 File Offset: 0x000076D8
	public void Add(Vector3 start, Vector3 end, Color32 color, float size, float time = 1f)
	{
		if (this._particles == null)
		{
			return;
		}
		TracerSystem.Particle particle = this._particles[this._currentParticleId];
		if (++this._currentParticleId >= this.Count)
		{
			this._currentParticleId = 0;
		}
		this._changedParticles.AddLast(particle);
		particle.Calc(start, end, size, time, color);
		this.CalcBounds(start);
		this.CalcBounds(end);
	}

	// Token: 0x060000AC RID: 172 RVA: 0x0000954C File Offset: 0x0000774C
	private void CalcBounds(Vector3 position)
	{
		if (position.x > this._bMin.x && position.y > this._bMin.y && position.z > this._bMin.z && position.x < this._bMax.x && position.y < this._bMax.y && position.z < this._bMax.z)
		{
			return;
		}
		if (position.x < this._bMin.x)
		{
			this._bMin.x = position.x - 50f;
		}
		if (position.y < this._bMin.y)
		{
			this._bMin.y = position.y - 50f;
		}
		if (position.z < this._bMin.z)
		{
			this._bMin.z = position.z - 50f;
		}
		if (position.x > this._bMax.x)
		{
			this._bMax.x = position.x + 50f;
		}
		if (position.y > this._bMax.y)
		{
			this._bMax.y = position.y + 50f;
		}
		if (position.z > this._bMax.z)
		{
			this._bMax.z = position.z + 50f;
		}
		this._mesh.bounds = new Bounds
		{
			min = this._bMin,
			max = this._bMax
		};
	}

	// Token: 0x0400014C RID: 332
	public int Count = 128;

	// Token: 0x0400014D RID: 333
	public float Size = 0.3f;

	// Token: 0x0400014E RID: 334
	private int _count2;

	// Token: 0x0400014F RID: 335
	private LinkedList<TracerSystem.Particle> _changedParticles;

	// Token: 0x04000150 RID: 336
	private TracerSystem.Particle[] _particles;

	// Token: 0x04000151 RID: 337
	private int _currentParticleId;

	// Token: 0x04000152 RID: 338
	private Vector3[] _vertices;

	// Token: 0x04000153 RID: 339
	private Vector3[] _normals;

	// Token: 0x04000154 RID: 340
	private Vector4[] _tangents;

	// Token: 0x04000155 RID: 341
	private Vector2[] _uv;

	// Token: 0x04000156 RID: 342
	private Vector2[] _uv2Vals;

	// Token: 0x04000157 RID: 343
	private Color32[] _colors;

	// Token: 0x04000158 RID: 344
	private int[] _triangles;

	// Token: 0x04000159 RID: 345
	private Mesh _mesh;

	// Token: 0x0400015A RID: 346
	private Vector3 _bMin = Vector3.zero;

	// Token: 0x0400015B RID: 347
	private Vector3 _bMax = Vector3.zero;

	// Token: 0x0400015C RID: 348
	private Material _material;

	// Token: 0x0200002C RID: 44
	private class Particle
	{
		// Token: 0x060000AE RID: 174 RVA: 0x00009740 File Offset: 0x00007940
		public void Calc(Vector3 start, Vector3 end, float size, float time, Color32 color)
		{
			Vector3 vector = (start - end).normalized * size;
			Vector3 b = new Vector3(-vector.z, 0f, vector.x);
			Vector3 b2 = new Vector3(vector.y * b.z, vector.z * b.x - vector.x * b.z, -vector.y * b.x) * 3f;
			this.Vertices[0] = start + b;
			this.Vertices[1] = start - b;
			this.Vertices[2] = end - b;
			this.Vertices[3] = end + b;
			this.Vertices[4] = start + b2;
			this.Vertices[5] = start - b2;
			this.Vertices[6] = end - b2;
			this.Vertices[7] = end + b2;
			this.Uv2Val.x = Time.time;
			this.Uv2Val.y = time;
			this.Color = color;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000098B4 File Offset: 0x00007AB4
		public void Update(Vector3[] vertices, Vector2[] uv2, Color32[] colors)
		{
			for (int i = 0; i < 8; i++)
			{
				int num = this.Pos4 + i;
				vertices[num] = this.Vertices[i];
				uv2[num] = this.Uv2Val;
				colors[num] = this.Color;
			}
		}

		// Token: 0x0400015D RID: 349
		public int Pos4;

		// Token: 0x0400015E RID: 350
		public Vector3[] Vertices = new Vector3[8];

		// Token: 0x0400015F RID: 351
		public Vector2 Uv2Val;

		// Token: 0x04000160 RID: 352
		public Color32 Color;
	}
}
