using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200000D RID: 13
[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class LocalDustParticlesParent : MonoBehaviour
{
	// Token: 0x06000036 RID: 54 RVA: 0x00004884 File Offset: 0x00002A84
	private void Awake()
	{
		this.GenerateMesh();
	}

	// Token: 0x06000037 RID: 55 RVA: 0x0000488C File Offset: 0x00002A8C
	public void GenerateMesh()
	{
		this._material = base.renderer.sharedMaterial;
		LocalDustParticlesParent._size = new Vector2(this.Size, this.Size);
		Vector2[][] tileUv = LocalDustParticlesParent.GetTileUv(this.QuadTileSetSide, this.QuadTileSetSide, this.UvTilePadding);
		this.Childs = LocalDustParticlesParent.Remove<LocalDustParticles>(this.Childs, (LocalDustParticles ldp) => !ldp);
		if (this.Childs.Length == 0)
		{
			return;
		}
		int num = 0;
		foreach (LocalDustParticles localDustParticles in this.Childs)
		{
			num += localDustParticles.Count;
		}
		Vector3[] array = new Vector3[num << 2];
		Vector4[] tangents = new Vector4[num << 2];
		Vector2[] array2 = new Vector2[num << 2];
		Vector2[] uv = new Vector2[num << 2];
		Color32[] array3 = new Color32[num << 2];
		int[] triangles = new int[num * 6];
		int num2 = 0;
		Bounds bounds = new Bounds(this.Childs[0].transform.position, Vector3.zero);
		foreach (LocalDustParticles localDustParticles2 in this.Childs)
		{
			Transform transform = localDustParticles2.transform;
			Matrix4x4 localToWorldMatrix = transform.localToWorldMatrix;
			float sideSpeed = localDustParticles2.SideSpeed;
			Bounds bounds2 = new Bounds(transform.position, transform.localScale * (1.5f + sideSpeed));
			bounds.Encapsulate(bounds2);
			int count = localDustParticles2.Count;
			Vector2[][] rotatedShifts = LocalDustParticlesParent.GetRotatedShifts(LocalDustParticlesParent._size, 6, localDustParticles2.SizeRandomness);
			Color32[] randomColors = this.GetRandomColors(localDustParticles2.ParticlesColor, 16, localDustParticles2.AlphaRandomness);
			for (int k = 0; k < count; k++)
			{
				this.GetPlane(num2++, triangles, array, array2, uv, tangents, array3, localToWorldMatrix, sideSpeed, tileUv, rotatedShifts, randomColors);
			}
		}
		base.transform.position = Vector3.zero;
		Mesh mesh = new Mesh
		{
			vertices = array,
			triangles = triangles,
			uv = array2,
			uv1 = uv,
			tangents = tangents,
			colors32 = array3,
			bounds = bounds
		};
		base.GetComponent<MeshFilter>().mesh = mesh;
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00004AE4 File Offset: 0x00002CE4
	private void GetPlane(int i, int[] triangles, Vector3[] positions, Vector2[] uv0, Vector2[] uv1, Vector4[] tangents, Color32[] colors, Matrix4x4 matrix, float sideSpeed, Vector2[][] uvs, Vector2[][] shifts, Color32[] rndColors)
	{
		int num = i << 2;
		int num2 = num++;
		int num3 = num++;
		int num4 = num++;
		int num5 = num;
		int num6 = i * 6;
		triangles[num6++] = num2;
		triangles[num6++] = num3;
		triangles[num6++] = num5;
		triangles[num6++] = num5;
		triangles[num6++] = num4;
		triangles[num6] = num2;
		Vector3 vector = matrix.MultiplyPoint(new Vector3(UnityEngine.Random.value - 0.5f, UnityEngine.Random.value - 0.5f, UnityEngine.Random.value - 0.5f));
		positions[num2] = (positions[num3] = (positions[num4] = (positions[num5] = vector)));
		tangents[num2] = (tangents[num3] = (tangents[num4] = (tangents[num5] = new Vector4(UnityEngine.Random.value * 3f, UnityEngine.Random.value * 3f, UnityEngine.Random.value * 3f, (0.1f + UnityEngine.Random.value * 0.2f) * sideSpeed))));
		colors[num2] = (colors[num3] = (colors[num4] = (colors[num5] = rndColors[UnityEngine.Random.Range(0, rndColors.Length - 1)])));
		Vector2[] array = uvs[UnityEngine.Random.Range(0, uvs.Length - 1)];
		uv0[num2] = array[0];
		uv0[num3] = array[1];
		uv0[num4] = array[2];
		uv0[num5] = array[3];
		Vector2[] array2 = shifts[UnityEngine.Random.Range(0, shifts.Length - 1)];
		uv1[num2] = array2[0];
		uv1[num3] = array2[1];
		uv1[num4] = array2[2];
		uv1[num5] = array2[3];
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00004D88 File Offset: 0x00002F88
	private Color32[] GetRandomColors(Color color, int count, float randomnessAlpha)
	{
		float min = color.a - randomnessAlpha;
		float max = color.a + randomnessAlpha;
		Color32[] array = new Color32[count];
		for (int i = 0; i < count; i++)
		{
			color.a = UnityEngine.Random.Range(min, max);
			array[i] = color;
		}
		return array;
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00004DE4 File Offset: 0x00002FE4
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.white;
		Bounds bounds = base.GetComponent<MeshFilter>().mesh.bounds;
		Gizmos.DrawWireCube(bounds.center, bounds.size);
	}

	// Token: 0x0600003B RID: 59 RVA: 0x00004E20 File Offset: 0x00003020
	private void OnDrawGizmos()
	{
		float num = (Time.realtimeSinceStartup - this._editorLastTime) * this.TimeSpeed;
		this._editorLastTime = Time.realtimeSinceStartup;
		float d = 0.5f * num;
		this._time += new Vector3(num, num, num);
		this._time += new Vector3(1f + Mathf.PerlinNoise(this._time.y, 0.5f), 1f + Mathf.PerlinNoise(this._time.z, 743f), 1f + Mathf.PerlinNoise(this._time.x, -424f)) * d;
		this._material.SetVector("_SelfTime", new Vector4(this._time.x, this._time.y, this._time.z, 0f));
	}

	// Token: 0x0600003C RID: 60 RVA: 0x00004F18 File Offset: 0x00003118
	private void Update()
	{
		float num = Time.deltaTime * this.TimeSpeed;
		float d = 0.5f * num;
		this._time += new Vector3(num, num, num);
		this._time += new Vector3(1f + Mathf.PerlinNoise(this._time.y, 0.5f), 1f + Mathf.PerlinNoise(this._time.z, 743f), 1f + Mathf.PerlinNoise(this._time.x, -424f)) * d;
		this._material.SetVector("_SelfTime", new Vector4(this._time.x, this._time.y, this._time.z, 0f));
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00004FFC File Offset: 0x000031FC
	private static Vector2[][] GetRotatedShifts(Vector2 size, int steps, float randomnessSize)
	{
		Vector2[][] array = new Vector2[steps][];
		float num = 6.2831855f / (float)steps;
		float num2 = 0f;
		for (int i = 0; i < steps; i++)
		{
			float num3 = UnityEngine.Random.Range(1f - randomnessSize, 1f + randomnessSize);
			Vector2 vector = new Vector2(Mathf.Sin(num2), Mathf.Cos(num2));
			vector.x *= size.x * num3;
			vector.y *= size.y * num3;
			Vector2 vector2 = new Vector2(vector.y, -vector.x);
			num2 += num;
			array[i] = new Vector2[]
			{
				vector,
				-vector2,
				vector2,
				-vector
			};
		}
		return array;
	}

	// Token: 0x0600003E RID: 62 RVA: 0x000050FC File Offset: 0x000032FC
	private static Vector2[][] GetTileUv(int width, int heigth, float padding = 0f)
	{
		int num = width * heigth;
		Vector2[][] array = new Vector2[num][];
		float num2 = 1f / (float)width;
		float num3 = 1f / (float)heigth;
		float num4 = padding * num2;
		float num5 = (1f - padding) * num2;
		float num6 = padding * num3;
		float num7 = (1f - padding) * num3;
		int num8 = 0;
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < heigth; j++)
			{
				Vector2[] array2 = new Vector2[4];
				float x = (float)i * num2 + num4;
				float x2 = (float)i * num2 + num5;
				float y = (float)j * num3 + num6;
				float y2 = (float)j * num3 + num7;
				array2[0] = new Vector2(x, y);
				array2[1] = new Vector2(x, y2);
				array2[2] = new Vector2(x2, y);
				array2[3] = new Vector2(x2, y2);
				array[num8++] = array2;
			}
		}
		return array;
	}

	// Token: 0x0600003F RID: 63 RVA: 0x0000520C File Offset: 0x0000340C
	private static T[] Remove<T>(T[] array, Func<T, bool> needToRemove)
	{
		LinkedList<T> linkedList = new LinkedList<T>(array);
		for (LinkedListNode<T> linkedListNode = linkedList.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
		{
			if (needToRemove(linkedListNode.Value))
			{
				linkedList.Remove(linkedListNode);
			}
		}
		array = new T[linkedList.Count];
		linkedList.CopyTo(array, 0);
		return array;
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00005268 File Offset: 0x00003468
	private static Texture2D[] GetTextures(Texture3D tex)
	{
		Color[] pixels = tex.GetPixels();
		int width = tex.width;
		int height = tex.height;
		int depth = tex.depth;
		Texture2D[] array = new Texture2D[depth];
		for (int i = 0; i < array.Length; i++)
		{
			Color[] array2 = new Color[width * height];
			int num = array2.Length * i;
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = pixels[j + num];
			}
			array[i] = new Texture2D(width, height, TextureFormat.Alpha8, false);
			array[i].SetPixels(array2);
			array[i].Apply();
		}
		return array;
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00005324 File Offset: 0x00003524
	private static Texture3D Built(Texture2D[] textures)
	{
		int width = textures[0].width;
		int num = width * width;
		int num2 = textures.Length;
		Color[] array = new Color[num * num2];
		for (int i = 0; i < num2; i++)
		{
			Color[] pixels = textures[i].GetPixels();
			int num3 = i * num;
			for (int j = 0; j < num; j++)
			{
				array[num3 + j] = pixels[j];
			}
		}
		Texture3D texture3D = new Texture3D(width, width, num2, TextureFormat.Alpha8, false);
		texture3D.SetPixels(array);
		texture3D.Apply();
		return texture3D;
	}

	// Token: 0x06000042 RID: 66 RVA: 0x000053C4 File Offset: 0x000035C4
	private static Texture2D ResizeTex(Texture2D tex, int width, int height, float xScale, float yScale, float xOffset, float yOffset)
	{
		Texture2D texture2D = new Texture2D(width, height, tex.format, false);
		float num = yScale / (float)height;
		float num2 = xScale / (float)width;
		float num3 = (1f - yScale) * 0.5f + yOffset;
		float num4 = (1f - xScale) * 0.5f + xOffset;
		float num5 = num3;
		for (int i = 0; i < height; i++)
		{
			float num6 = num4;
			for (int j = 0; j < width; j++)
			{
				Color pixelBilinear = tex.GetPixelBilinear(num6, num5);
				texture2D.SetPixel(j, i, pixelBilinear);
				num6 += num2;
			}
			num5 += num;
		}
		texture2D.Apply();
		return texture2D;
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00005470 File Offset: 0x00003670
	private static void Qweqwe(Texture2D[] textures, int width, int height)
	{
		float num = 1f / (float)textures.Length;
		float num2 = 0f;
		for (int i = 0; i < textures.Length; i++)
		{
			float num3 = Mathf.Lerp(0.16f, 1f, num2);
			textures[i] = LocalDustParticlesParent.ResizeTex(textures[i], width, height, num3, num3, 0.065f, -0.015f);
			num2 += num;
		}
	}

	// Token: 0x04000072 RID: 114
	public LocalDustParticles[] Childs;

	// Token: 0x04000073 RID: 115
	public float TimeSpeed;

	// Token: 0x04000074 RID: 116
	public float Size;

	// Token: 0x04000075 RID: 117
	public int QuadTileSetSide = 2;

	// Token: 0x04000076 RID: 118
	public float UvTilePadding;

	// Token: 0x04000077 RID: 119
	private Material _material;

	// Token: 0x04000078 RID: 120
	private static Vector2 _size;

	// Token: 0x04000079 RID: 121
	private Vector3 _time = new Vector3(41.234f, 50.43f, 60.753f);

	// Token: 0x0400007A RID: 122
	private float _editorLastTime;
}
