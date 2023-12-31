using System;
using UnityEngine;

// Token: 0x0200002F RID: 47
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class WireGenerator : MonoBehaviour
{
	// Token: 0x060000B9 RID: 185 RVA: 0x00009AF4 File Offset: 0x00007CF4
	private static WireGenerator.Shape CreateShape(int details, float size)
	{
		WireGenerator.Shape shape = new WireGenerator.Shape();
		if (details > 2)
		{
			shape.Vertices = new Vector2[details];
			float num = 6.2831855f / (float)details;
			float num2 = 0f;
			for (int i = 0; i < details; i++)
			{
				shape.Vertices[i] = new Vector2(Mathf.Sin(num2), Mathf.Cos(num2)) * size;
				num2 += num;
			}
			shape.TrisA = new int[details];
			shape.TrisB = new int[details];
			for (int j = 0; j < details; j++)
			{
				shape.TrisA[j] = j;
				shape.TrisB[j] = ((j <= 0) ? (details - 1) : (j - 1));
			}
		}
		else if (details == 2)
		{
			shape.Vertices = new Vector2[]
			{
				new Vector2(0f, size),
				new Vector2(0f, -size),
				new Vector2(size, 0f),
				new Vector2(-size, 0f)
			};
			shape.Uv = new float[]
			{
				0f,
				1f,
				0f,
				1f
			};
			shape.TrisA = new int[]
			{
				0,
				2
			};
			shape.TrisB = new int[]
			{
				1,
				3
			};
		}
		else if (details == 1)
		{
			shape.Vertices = new Vector2[]
			{
				new Vector2(0f, size),
				new Vector2(0f, -size)
			};
			shape.Uv = new float[]
			{
				0f,
				1f
			};
			shape.TrisA = new int[1];
			shape.TrisB = new int[]
			{
				1
			};
		}
		else if (details < 1)
		{
			shape.Vertices = new Vector2[]
			{
				new Vector2(size, 0f),
				new Vector2(-size, 0f)
			};
			shape.Uv = new float[]
			{
				0f,
				1f
			};
			shape.TrisA = new int[1];
			shape.TrisB = new int[]
			{
				1
			};
		}
		return shape;
	}

	// Token: 0x060000BA RID: 186 RVA: 0x00009D64 File Offset: 0x00007F64
	public void Generate()
	{
		if (this.Wires == null || this.Wires.Length == 0)
		{
			return;
		}
		WireGenerator.Shape shape = WireGenerator.CreateShape(this.ShapeDetails, this.ShapeSize);
		int num = 0;
		int num2 = 0;
		foreach (WireGenerator.Wire wire in this.Wires)
		{
			num += wire.GetTrianglesCount(shape);
			num2 += wire.GetVerticesCount(shape);
		}
		int[] triangles = new int[num];
		Vector3[] array = new Vector3[num2];
		Vector2[] array2 = new Vector2[num2];
		int num3 = 0;
		int num4 = 0;
		foreach (WireGenerator.Wire wire2 in this.Wires)
		{
			wire2.Generate(shape, num4, num3, triangles, array, array2);
			num3 += wire2.GetTrianglesCount(shape);
			num4 += wire2.GetVerticesCount(shape);
		}
		if (this._meshFilter == null)
		{
			this._meshFilter = (base.GetComponent<MeshFilter>() ?? base.gameObject.AddComponent<MeshFilter>());
		}
		if (this.Mesh == null)
		{
			this._meshFilter.mesh = (this.Mesh = new Mesh
			{
				vertices = array,
				triangles = triangles,
				uv = array2
			});
		}
		else
		{
			this.Mesh.Clear();
			this.Mesh.vertices = array;
			this.Mesh.triangles = triangles;
			this.Mesh.uv = array2;
		}
		this.Mesh.RecalculateBounds();
		this.Mesh.RecalculateNormals();
	}

	// Token: 0x04000165 RID: 357
	public WireGenerator.Wire[] Wires;

	// Token: 0x04000166 RID: 358
	public int ShapeDetails = 5;

	// Token: 0x04000167 RID: 359
	public float ShapeSize = 0.1f;

	// Token: 0x04000168 RID: 360
	private MeshFilter _meshFilter;

	// Token: 0x04000169 RID: 361
	public Mesh Mesh;

	// Token: 0x02000030 RID: 48
	[Serializable]
	public class Wire
	{
		// Token: 0x060000BC RID: 188 RVA: 0x00009F1C File Offset: 0x0000811C
		public int GetTrianglesCount(WireGenerator.Shape shape)
		{
			return shape.TrisA.Length * 6 * this.Parts;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00009F30 File Offset: 0x00008130
		public int GetVerticesCount(WireGenerator.Shape shape)
		{
			return shape.Vertices.Length * (this.Parts + 1);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00009F44 File Offset: 0x00008144
		public void Generate(WireGenerator.Shape shape, int vIndex, int tIndex, int[] triangles, Vector3[] positions, Vector2[] uv0)
		{
			WireGenerator.Wire.Generate(shape, this.GenerateVecs(), vIndex, tIndex, triangles, positions, uv0);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00009F68 File Offset: 0x00008168
		public Vector3[] GetPoints()
		{
			int parts = this.Parts;
			int num = parts + 1;
			Vector3[] array = new Vector3[num];
			Vector3 a = this.A;
			Vector3 b = this.B;
			Vector3 a2 = b - a;
			float num2 = 1f / (float)parts;
			Vector3 b2 = a2 * num2;
			Vector3 vector = a;
			for (int i = 0; i < num; i++)
			{
				array[i] = vector;
				vector += b2;
			}
			float num3 = 0f;
			for (int j = 0; j < num; j++)
			{
				float num4 = Mathf.Abs(0.5f - num3) * 2f;
				Vector3[] array2 = array;
				int num5 = j;
				array2[num5].y = array2[num5].y - (1f - num4 * num4) * this.Gravity;
				num3 += num2;
			}
			return array;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000A048 File Offset: 0x00008248
		public WireGenerator.Point[] GenerateVecs()
		{
			int parts = this.Parts;
			int num = parts + 1;
			WireGenerator.Point[] array = new WireGenerator.Point[num];
			Vector3[] array2 = new Vector3[num];
			Vector3[] array3 = new Vector3[num];
			Vector3[] array4 = new Vector3[num];
			Vector3 a = this.A;
			Vector3 b = this.B;
			Vector3 a2 = b - a;
			float num2 = 1f / (float)parts;
			Vector3 b2 = a2 * num2;
			Vector3 vector = new Vector3(a2.z, 0f, -a2.x);
			Vector3 normalized = vector.normalized;
			Vector3 vector2 = a;
			for (int i = 0; i < num; i++)
			{
				array2[i] = vector2;
				vector2 += b2;
			}
			float num3 = 0f;
			for (int j = 0; j < num; j++)
			{
				float num4 = Mathf.Abs(0.5f - num3) * 2f;
				Vector3[] array5 = array2;
				int num5 = j;
				array5[num5].y = array5[num5].y - (1f - num4 * num4) * this.Gravity;
				num3 += num2;
			}
			array3[0] = (array2[1] - array2[0]).normalized;
			array3[parts] = (array2[parts] - array2[parts - 1]).normalized;
			for (int k = 1; k < parts; k++)
			{
				array3[k] = (array2[k + 1] - array2[k - 1]).normalized;
			}
			for (int l = 0; l < num; l++)
			{
				array4[l] = Vector3.Cross(array3[l], normalized).normalized;
			}
			for (int m = 0; m < num; m++)
			{
				array[m] = new WireGenerator.Point
				{
					Position = array2[m],
					Right = normalized,
					Upward = array4[m]
				};
			}
			return array;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000A2C0 File Offset: 0x000084C0
		public static void Generate(WireGenerator.Shape shape, WireGenerator.Point[] points, int vIndex, int tIndex, int[] triangles, Vector3[] positions, Vector2[] uv0)
		{
			int num = vIndex;
			int num2 = tIndex;
			for (int i = 0; i < points.Length; i++)
			{
				WireGenerator.Point point = points[i];
				for (int j = 0; j < shape.Vertices.Length; j++)
				{
					positions[num + j] = point.Position + shape.Vertices[j].x * point.Right + shape.Vertices[j].y * point.Upward;
				}
				if (shape.Uv != null)
				{
					for (int k = 0; k < shape.Vertices.Length; k++)
					{
						uv0[num + k] = new Vector2(shape.Uv[k], 0f);
					}
				}
				if (i != 0)
				{
					for (int l = 0; l < shape.TrisA.Length; l++)
					{
						int num3 = num + shape.TrisA[l];
						int num4 = num3 - shape.Vertices.Length;
						int num5 = num + shape.TrisB[l];
						int num6 = num5 - shape.Vertices.Length;
						int num7 = num2 + l * 6;
						triangles[num7] = num3;
						triangles[num7 + 1] = num4;
						triangles[num7 + 2] = num6;
						triangles[num7 + 3] = num6;
						triangles[num7 + 4] = num5;
						triangles[num7 + 5] = num3;
					}
					num2 += shape.TrisA.Length * 6;
				}
				num += shape.Vertices.Length;
			}
		}

		// Token: 0x0400016A RID: 362
		public Vector3 A;

		// Token: 0x0400016B RID: 363
		public Vector3 B;

		// Token: 0x0400016C RID: 364
		public float Gravity;

		// Token: 0x0400016D RID: 365
		public int Parts;
	}

	// Token: 0x02000031 RID: 49
	public class Shape
	{
		// Token: 0x0400016E RID: 366
		public Vector2[] Vertices;

		// Token: 0x0400016F RID: 367
		public float[] Uv;

		// Token: 0x04000170 RID: 368
		public int[] TrisA;

		// Token: 0x04000171 RID: 369
		public int[] TrisB;
	}

	// Token: 0x02000032 RID: 50
	public struct Point
	{
		// Token: 0x04000172 RID: 370
		public Vector3 Position;

		// Token: 0x04000173 RID: 371
		public Vector3 Upward;

		// Token: 0x04000174 RID: 372
		public Vector3 Right;
	}
}
