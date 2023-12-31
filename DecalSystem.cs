using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200001A RID: 26
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class DecalSystem : MonoBehaviour
{
	// Token: 0x0600006A RID: 106 RVA: 0x000070E0 File Offset: 0x000052E0
	private void Awake()
	{
		this._sizeRandom = (this.SizeRandomRange > float.Epsilon);
		this._changedDecals = new LinkedList<DecalSystem.Decal>();
		this._decals = new DecalSystem.Decal[this.Count];
		for (int i = 0; i < this.Count; i++)
		{
			this._decals[i] = new DecalSystem.Decal();
			this._decals[i].Pos4 = i << 2;
		}
		int num = this.Count << 2;
		this._vertices = new Vector3[num];
		this._normals = new Vector3[num];
		this._tangents = new Vector4[num];
		this._uv = new Vector2[num];
		this._uv2Vals = new Vector2[num];
		this._triangles = new int[this.Count * 6];
		int j = 0;
		int num2 = 0;
		while (j < this.Count)
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
		while (k < this.Count)
		{
			this._uv[num4++] = new Vector2(0f, 0f);
			this._uv[num4++] = new Vector2(0f, 1f);
			this._uv[num4++] = new Vector2(1f, 1f);
			this._uv[num4++] = new Vector2(1f, 0f);
			k++;
		}
		this._tilesCount = this.TileSheetRows * this.TileSheetColumns;
		this._updateUv = (this._tilesCount > 1);
		if (this._updateUv)
		{
			this._tiles = new Vector2[this._tilesCount][];
			float num5 = 1f / (float)this.TileSheetRows;
			float num6 = 1f / (float)this.TileSheetColumns;
			int l = 0;
			int num7 = 0;
			int num8 = 0;
			while (l < this._tilesCount)
			{
				float x = num5 * (float)num7;
				float x2 = num5 * (float)(num7 + 1);
				float y = num6 * (float)num8;
				float y2 = num6 * (float)(num8 + 1);
				this._tiles[l] = new Vector2[4];
				this._tiles[l][0] = new Vector2(x, y);
				this._tiles[l][1] = new Vector2(x, y2);
				this._tiles[l][2] = new Vector2(x2, y2);
				this._tiles[l][3] = new Vector2(x2, y);
				num7++;
				if (num7 >= this.TileSheetRows)
				{
					num8++;
					num7 = 0;
				}
				l++;
			}
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
	}

	// Token: 0x0600006B RID: 107 RVA: 0x000074AC File Offset: 0x000056AC
	private void LateUpdate()
	{
		if (this._changedDecals.Count == 0)
		{
			return;
		}
		if (this._updateUv)
		{
			foreach (DecalSystem.Decal decal in this._changedDecals)
			{
				decal.Update(this._vertices, this._normals, this._tangents, this._uv);
			}
		}
		else
		{
			foreach (DecalSystem.Decal decal2 in this._changedDecals)
			{
				decal2.Update(this._vertices, this._normals, this._tangents);
			}
		}
		this._changedDecals.Clear();
		this._mesh.vertices = this._vertices;
		this._mesh.normals = this._normals;
		if (this._updateUv)
		{
			this._mesh.uv = this._uv;
		}
		this._mesh.tangents = this._tangents;
	}

	// Token: 0x0600006C RID: 108 RVA: 0x0000760C File Offset: 0x0000580C
	public void Clear()
	{
		this._vertices = new Vector3[this.Count << 2];
		this._mesh.vertices = this._vertices;
		this._mesh.bounds = new Bounds(Vector3.zero, Vector3.zero);
		this._bMin = Vector3.zero;
		this._bMax = Vector3.zero;
		this._changedDecals.Clear();
	}

	// Token: 0x0600006D RID: 109 RVA: 0x00007678 File Offset: 0x00005878
	public void Add(Vector3 position, Vector3 normal)
	{
		float radius = (!this._sizeRandom) ? this.SizeMin : (this.SizeMin + FastRndom.Float() * this.SizeRandomRange);
		DecalSystem.Decal decal = this._decals[this._currentDecalId];
		if (++this._currentDecalId >= this.Count)
		{
			this._currentDecalId = 0;
		}
		this._changedDecals.AddLast(decal);
		decal.Calc(position, normal, radius);
		if (this._updateUv)
		{
			decal.Uv = this._tiles[FastRndom.Int(this._tilesCount)];
		}
		this.CalcBounds(position);
	}

	// Token: 0x0600006E RID: 110 RVA: 0x00007720 File Offset: 0x00005920
	public void Add(Vector3 position, Vector3 normal, int tile)
	{
		float radius = (!this._sizeRandom) ? this.SizeMin : (this.SizeMin + FastRndom.Float() * this.SizeRandomRange);
		DecalSystem.Decal decal = this._decals[this._currentDecalId];
		if (++this._currentDecalId >= this.Count)
		{
			this._currentDecalId = 0;
		}
		this._changedDecals.AddLast(decal);
		decal.Calc(position, normal, radius);
		decal.Uv = this._tiles[tile];
		this.CalcBounds(position);
	}

	// Token: 0x0600006F RID: 111 RVA: 0x000077B4 File Offset: 0x000059B4
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

	// Token: 0x040000DA RID: 218
	public int Count = 2048;

	// Token: 0x040000DB RID: 219
	public float SizeMin = 1f;

	// Token: 0x040000DC RID: 220
	public float SizeRandomRange;

	// Token: 0x040000DD RID: 221
	private bool _sizeRandom;

	// Token: 0x040000DE RID: 222
	public int TileSheetRows = 1;

	// Token: 0x040000DF RID: 223
	public int TileSheetColumns = 1;

	// Token: 0x040000E0 RID: 224
	private Vector2[][] _tiles;

	// Token: 0x040000E1 RID: 225
	private int _tilesCount = 1;

	// Token: 0x040000E2 RID: 226
	private bool _updateUv;

	// Token: 0x040000E3 RID: 227
	private LinkedList<DecalSystem.Decal> _changedDecals;

	// Token: 0x040000E4 RID: 228
	private int _currentDecalId;

	// Token: 0x040000E5 RID: 229
	private DecalSystem.Decal[] _decals;

	// Token: 0x040000E6 RID: 230
	private Vector3[] _vertices;

	// Token: 0x040000E7 RID: 231
	private Vector3[] _normals;

	// Token: 0x040000E8 RID: 232
	private Vector4[] _tangents;

	// Token: 0x040000E9 RID: 233
	private Vector2[] _uv;

	// Token: 0x040000EA RID: 234
	private Vector2[] _uv2Vals;

	// Token: 0x040000EB RID: 235
	private int[] _triangles;

	// Token: 0x040000EC RID: 236
	private Mesh _mesh;

	// Token: 0x040000ED RID: 237
	private Vector3 _bMin = Vector3.zero;

	// Token: 0x040000EE RID: 238
	private Vector3 _bMax = Vector3.zero;

	// Token: 0x0200001B RID: 27
	private class Decal
	{
		// Token: 0x06000071 RID: 113 RVA: 0x000079A8 File Offset: 0x00005BA8
		public void FastCalcWithoutRotation(Vector3 position, Vector3 normal, float radius)
		{
			Vector3 vector;
			float num;
			Vector3 vector2;
			if (normal.x < 0.95f && normal.x > -0.95f)
			{
				vector = new Vector3(0f, normal.z, -normal.y);
				Debug.DrawRay(position, normal, Color.green, 5f);
				Debug.DrawRay(position, vector, Color.red, 5f);
				num = radius / Mathf.Sqrt(vector.y * vector.y + vector.z * vector.z);
				vector2 = new Vector3(normal.y * -normal.y - normal.z * normal.z, -normal.x * -normal.y, normal.x * normal.z);
			}
			else
			{
				vector = new Vector3(-normal.z, 0f, normal.x);
				num = radius / Mathf.Sqrt(vector.x * vector.x + vector.z * vector.z);
				vector2 = new Vector3(normal.y * vector.z, normal.z * vector.x - normal.x * vector.z, -normal.y * vector.x);
			}
			vector *= num;
			vector2 *= num;
			this.Vertices[0] = position - vector;
			this.Vertices[1] = position - vector2;
			this.Vertices[2] = position + vector;
			this.Vertices[3] = position + vector2;
			num = 0.7071f / radius;
			this.Tangent = new Vector4((this.Vertices[3].x - this.Vertices[0].x) * num, (this.Vertices[3].y - this.Vertices[0].y) * num, (this.Vertices[3].z - this.Vertices[0].z) * num, -1f);
			this.Normal = normal;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00007C0C File Offset: 0x00005E0C
		public void Calc(Vector3 position, Vector3 normal, float radius)
		{
			Vector3 vector = Vector3.Cross(normal, FastRndom.VectorNormalized());
			vector *= radius / vector.magnitude;
			Vector3 b = Vector3.Cross(normal, vector);
			this.Vertices[0] = position - vector;
			this.Vertices[1] = position - b;
			this.Vertices[2] = position + vector;
			this.Vertices[3] = position + b;
			float num = 0.7071f / radius;
			this.Tangent = new Vector4((this.Vertices[3].x - this.Vertices[0].x) * num, (this.Vertices[3].y - this.Vertices[0].y) * num, (this.Vertices[3].z - this.Vertices[0].z) * num, -1f);
			this.Normal = normal;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00007D2C File Offset: 0x00005F2C
		public void Update(Vector3[] vertices, Vector3[] normals, Vector4[] tangents)
		{
			for (int i = 0; i < 4; i++)
			{
				int num = this.Pos4 + i;
				vertices[num] = this.Vertices[i];
				normals[num] = this.Normal;
				tangents[num] = this.Tangent;
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00007D98 File Offset: 0x00005F98
		public void Update(Vector3[] vertices, Vector3[] normals, Vector4[] tangents, Vector2[] uv)
		{
			for (int i = 0; i < 4; i++)
			{
				int num = this.Pos4 + i;
				vertices[num] = this.Vertices[i];
				normals[num] = this.Normal;
				tangents[num] = this.Tangent;
				uv[num] = this.Uv[i];
			}
		}

		// Token: 0x040000EF RID: 239
		public int Pos4;

		// Token: 0x040000F0 RID: 240
		public Vector3[] Vertices = new Vector3[4];

		// Token: 0x040000F1 RID: 241
		public Vector3 Normal;

		// Token: 0x040000F2 RID: 242
		public Vector4 Tangent;

		// Token: 0x040000F3 RID: 243
		public Vector2 Uv2Val;

		// Token: 0x040000F4 RID: 244
		public Vector2[] Uv;
	}
}
