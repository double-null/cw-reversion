using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000033 RID: 51
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class WireSplineGenerator : MonoBehaviour
{
	// Token: 0x060000C4 RID: 196 RVA: 0x0000A494 File Offset: 0x00008694
	private static WireSplineGenerator.Shape CreateShape(int details, float size)
	{
		WireSplineGenerator.Shape shape = new WireSplineGenerator.Shape();
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

	// Token: 0x060000C5 RID: 197 RVA: 0x0000A704 File Offset: 0x00008904
	public void Generate()
	{
		if (this.Wires == null || this.Wires.Length == 0)
		{
			return;
		}
		WireSplineGenerator.Shape shape = WireSplineGenerator.CreateShape(this.ShapeDetails, this.ShapeSize);
		int num = 0;
		int num2 = 0;
		foreach (WireSplineGenerator.Wire wire in this.Wires)
		{
			num += wire.GetTrianglesCount(shape);
			num2 += wire.GetVerticesCount(shape);
		}
		int[] triangles = new int[num];
		Vector3[] array = new Vector3[num2];
		Vector2[] array2 = new Vector2[num2];
		int num3 = 0;
		int num4 = 0;
		foreach (WireSplineGenerator.Wire wire2 in this.Wires)
		{
			wire2.Generate(shape, num4, num3, triangles, array, array2, this.Error);
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

	// Token: 0x060000C6 RID: 198 RVA: 0x0000A8BC File Offset: 0x00008ABC
	private static Vector3[] GetPoints(WireSplineGenerator.Wire wire)
	{
		int num = wire.Parts + 1;
		int num2 = num * (wire.Positins.Length - 1);
		num2 -= wire.Positins.Length - 2;
		Vector3[] array = new Vector3[num2];
		float num3 = 1f / (float)wire.Parts;
		Vector3[] positins = wire.Positins;
		Vector3[] normals = wire.Normals;
		int i = 1;
		int num4 = 0;
		while (i < positins.Length)
		{
			Vector3 p = positins[i - 1];
			Vector3 p2 = positins[i];
			Vector3 n = normals[i - 1];
			Vector3 n2 = normals[i];
			float num5 = 0f;
			int j = 0;
			while (j < num)
			{
				array[num4] = WireSplineGenerator.GetCurve(num5, p, n, n2, p2);
				num5 += num3;
				j++;
				num4++;
			}
			num4--;
			i++;
		}
		return array;
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x0000A9C0 File Offset: 0x00008BC0
	public static Vector3[] GetPoints(Vector3 p0, Vector3 p1, Vector3 n0, Vector3 n1, int parts)
	{
		int num = parts + 1;
		float num2 = 1f / (float)parts;
		Vector3[] array = new Vector3[num];
		float num3 = 0f;
		for (int i = 0; i < num; i++)
		{
			array[i] = WireSplineGenerator.GetCurve(num3, p0, n0, n1, p1);
			num3 += num2;
		}
		return array;
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x0000AA1C File Offset: 0x00008C1C
	private static Vector3[] Optimize(Vector3[] points, float error)
	{
		LinkedList<Vector3> linkedList = new LinkedList<Vector3>(points);
		int num = int.MaxValue;
		while (linkedList.Count < num)
		{
			num = linkedList.Count;
			WireSplineGenerator.Optimize(linkedList, error);
			error *= 0.5f;
		}
		points = new Vector3[linkedList.Count];
		linkedList.CopyTo(points, 0);
		return points;
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x0000AA74 File Offset: 0x00008C74
	private static void Optimize(LinkedList<Vector3> points, float error)
	{
		LinkedListNode<Vector3> linkedListNode = points.First;
		while (linkedListNode.Next != null && linkedListNode.Next.Next != null)
		{
			Vector3 value = linkedListNode.Value;
			Vector3 value2 = linkedListNode.Next.Next.Value;
			Vector3 value3 = linkedListNode.Next.Value;
			Vector3 onNormal = value2 - value;
			Vector3 vector = value3 - value;
			Vector3 b = Vector3.Project(vector, onNormal);
			float num = (vector - b).magnitude / onNormal.magnitude;
			if (num < error)
			{
				points.Remove(linkedListNode.Next);
			}
			linkedListNode = linkedListNode.Next;
		}
	}

	// Token: 0x060000CA RID: 202 RVA: 0x0000AB20 File Offset: 0x00008D20
	public static Vector3 GetCurve(float t, Vector3 p0, Vector3 n0, Vector3 n1, Vector3 p1)
	{
		return WireSplineGenerator.Hermite(t, p0, n0, n1, p1);
	}

	// Token: 0x060000CB RID: 203 RVA: 0x0000AB30 File Offset: 0x00008D30
	private static Vector3 Hermite(float t, Vector3 p0, Vector3 m0, Vector3 m1, Vector3 p1)
	{
		float num = t * t;
		float num2 = num * t;
		float num3 = 3f * num;
		float num4 = 2f * num2;
		return (num4 - num3 + 1f) * p0 + (num2 - 2f * num + t) * m0 + (num3 - num4) * p1 + (num2 - num) * m1;
	}

	// Token: 0x060000CC RID: 204 RVA: 0x0000AB98 File Offset: 0x00008D98
	private static Vector3 Bezier(float t, Vector3 p0, Vector3 n0, Vector3 n1, Vector3 p1)
	{
		float num = 1f - t;
		float num2 = num * num;
		float d = num2 * num;
		float num3 = t * t;
		float d2 = num3 * t;
		return d * p0 + num2 * t * n0 + num * num3 * n1 + d2 * p1;
	}

	// Token: 0x04000175 RID: 373
	public WireSplineGenerator.Wire[] Wires;

	// Token: 0x04000176 RID: 374
	public int ShapeDetails = 5;

	// Token: 0x04000177 RID: 375
	public float ShapeSize = 0.1f;

	// Token: 0x04000178 RID: 376
	public float Error = 0.05f;

	// Token: 0x04000179 RID: 377
	private MeshFilter _meshFilter;

	// Token: 0x0400017A RID: 378
	public Mesh Mesh;

	// Token: 0x02000034 RID: 52
	[Serializable]
	public class Wire
	{
		// Token: 0x060000CE RID: 206 RVA: 0x0000ABF8 File Offset: 0x00008DF8
		private int GetPointCount()
		{
			int num = this.Parts + 1;
			int num2 = num * (this.Positins.Length - 1);
			return num2 - (this.Positins.Length - 2);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000AC2C File Offset: 0x00008E2C
		public int GetTrianglesCount(WireSplineGenerator.Shape shape)
		{
			int pointCount = this.GetPointCount();
			return shape.TrisA.Length * 6 * pointCount;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000AC4C File Offset: 0x00008E4C
		public int GetVerticesCount(WireSplineGenerator.Shape shape)
		{
			int pointCount = this.GetPointCount();
			return shape.Vertices.Length * pointCount;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000AC6C File Offset: 0x00008E6C
		public void Generate(WireSplineGenerator.Shape shape, int vIndex, int tIndex, int[] triangles, Vector3[] positions, Vector2[] uv0, float error)
		{
			WireSplineGenerator.Wire.Generate(shape, this.GenerateVecs(WireSplineGenerator.Optimize(WireSplineGenerator.GetPoints(this), error)), vIndex, tIndex, triangles, positions, uv0);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000AC9C File Offset: 0x00008E9C
		public WireSplineGenerator.Point[] GenerateVecs(Vector3[] positions)
		{
			int num = positions.Length;
			int num2 = num - 1;
			WireSplineGenerator.Point[] array = new WireSplineGenerator.Point[num];
			Vector3[] array2 = new Vector3[num];
			Vector3[] array3 = new Vector3[num];
			Vector3[] array4 = new Vector3[num];
			array2[0] = (positions[1] - positions[0]).normalized;
			array2[num2] = (positions[num2] - positions[num2 - 1]).normalized;
			for (int i = 1; i < num2; i++)
			{
				array2[i] = (positions[i + 1] - positions[i - 1]).normalized;
			}
			if (array2[0].y > 0.9f || array2[0].y < -0.9f)
			{
				array3[0] = new Vector3(1f, 0f, 0f);
			}
			else
			{
				array3[0] = new Vector3(0f, 1f, 0f);
			}
			array4[0] = Vector3.Cross(array2[0], array3[0]).normalized;
			array3[0] = Vector3.Cross(array2[0], array4[0]).normalized;
			for (int j = 1; j < num; j++)
			{
				array4[j] = Vector3.Cross(array2[j], -array3[j - 1]).normalized;
				array3[j] = Vector3.Cross(array2[j], array4[j]).normalized;
			}
			for (int k = 0; k < num; k++)
			{
				array[k] = new WireSplineGenerator.Point
				{
					Position = positions[k],
					Right = array4[k],
					Upward = array3[k]
				};
			}
			return array;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000AF58 File Offset: 0x00009158
		public static void Generate(WireSplineGenerator.Shape shape, WireSplineGenerator.Point[] points, int vIndex, int tIndex, int[] triangles, Vector3[] positions, Vector2[] uv0)
		{
			int num = vIndex;
			int num2 = tIndex;
			for (int i = 0; i < points.Length; i++)
			{
				WireSplineGenerator.Point point = points[i];
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

		// Token: 0x0400017B RID: 379
		public Vector3[] Positins;

		// Token: 0x0400017C RID: 380
		public Vector3[] Normals;

		// Token: 0x0400017D RID: 381
		public int Parts;
	}

	// Token: 0x02000035 RID: 53
	public class Shape
	{
		// Token: 0x0400017E RID: 382
		public Vector2[] Vertices;

		// Token: 0x0400017F RID: 383
		public float[] Uv;

		// Token: 0x04000180 RID: 384
		public int[] TrisA;

		// Token: 0x04000181 RID: 385
		public int[] TrisB;
	}

	// Token: 0x02000036 RID: 54
	public struct Point
	{
		// Token: 0x04000182 RID: 386
		public Vector3 Position;

		// Token: 0x04000183 RID: 387
		public Vector3 Upward;

		// Token: 0x04000184 RID: 388
		public Vector3 Right;
	}
}
