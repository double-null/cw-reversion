using System;
using UnityEngine;

// Token: 0x02000018 RID: 24
[ExecuteInEditMode]
public class MultiFlareLight : MonoBehaviour
{
	// Token: 0x06000064 RID: 100 RVA: 0x00006C54 File Offset: 0x00004E54
	private void OnEnable()
	{
	}

	// Token: 0x06000065 RID: 101 RVA: 0x00006C58 File Offset: 0x00004E58
	private void OnDrawGizmos()
	{
		Gizmos.DrawIcon(base.transform.position, "flareGismo.png", true);
	}

	// Token: 0x06000066 RID: 102 RVA: 0x00006C70 File Offset: 0x00004E70
	public void FillMesh(ref int pI, MultiFlare.FlareType type, ProFlareAtlas atlas, Vector3[] vertices, Vector4[] tangents, Vector3[] normals, Vector2[] uv0, Vector2[] uv1, Color32[] colors)
	{
		int num = pI << 2;
		foreach (MultiFlareLight.Flare flare in this.Flares)
		{
			if (flare.Type == type)
			{
				int num2 = num++;
				int num3 = num++;
				int num4 = num++;
				int num5 = num++;
				vertices[num2] = (vertices[num3] = (vertices[num4] = (vertices[num5] = base.transform.position)));
				flare.FillMesh(pI, atlas, this, tangents, normals, uv0, uv1, colors);
				pI++;
			}
		}
	}

	// Token: 0x040000C5 RID: 197
	public MultiFlare Parent;

	// Token: 0x040000C6 RID: 198
	public Light LightObject;

	// Token: 0x040000C7 RID: 199
	public float Scale = 1f;

	// Token: 0x040000C8 RID: 200
	public float Alpha = 1f;

	// Token: 0x040000C9 RID: 201
	public Color Color = Color.white;

	// Token: 0x040000CA RID: 202
	public MultiFlareLight.Flare[] Flares;

	// Token: 0x02000019 RID: 25
	[Serializable]
	public class Flare
	{
		// Token: 0x06000068 RID: 104 RVA: 0x00006D50 File Offset: 0x00004F50
		public void FillMesh(int pI, ProFlareAtlas atlas, MultiFlareLight light, Vector4[] tangents, Vector3[] normals, Vector2[] uv0, Vector2[] uv1, Color32[] colors)
		{
			Vector2 vector = this.Scale * this.MinScale * light.Scale;
			float b = this.Alpha * this.MinAlpha * light.Alpha;
			float y = 1f / (this.MaxDist - this.MinDist);
			int num = pI << 2;
			int num2 = num++;
			int num3 = num++;
			int num4 = num++;
			Rect uv2 = atlas.elementsList[this.TextureId].UV;
			if (this.RotType == MultiFlare.RotationType.Normal)
			{
				uv2.xMin -= 2f;
				uv2.xMax -= 2f;
			}
			else if (this.RotType == MultiFlare.RotationType.Inverse)
			{
				uv2.yMin -= 2f;
				uv2.yMax -= 2f;
			}
			uv0[num2] = new Vector2(uv2.xMin, uv2.yMax);
			uv0[num3] = new Vector2(uv2.xMin, uv2.yMin);
			uv0[num4] = new Vector2(uv2.xMax, uv2.yMin);
			uv0[num] = new Vector2(uv2.xMax, uv2.yMax);
			uv1[num2] = new Vector2(-vector.x, vector.y);
			uv1[num3] = new Vector2(-vector.x, -vector.y);
			uv1[num4] = new Vector2(vector.x, -vector.y);
			uv1[num] = new Vector2(vector.x, vector.y);
			Color32 color = this.Color * light.Color * b;
			color.a = 1;
			colors[num2] = (colors[num3] = (colors[num4] = (colors[num] = color)));
			normals[num2] = (normals[num3] = (normals[num4] = (normals[num] = new Vector3(this.MinDist, y, this.CenterShift))));
			tangents[num2] = (tangents[num3] = (tangents[num4] = (tangents[num] = new Vector4((this.MaxScale - this.MinScale) / this.MinScale, (this.MaxAlpha - this.MinAlpha) / this.MinAlpha, this.SvShift, this.SvWidth))));
		}

		// Token: 0x040000CB RID: 203
		public int TextureId;

		// Token: 0x040000CC RID: 204
		public MultiFlare.FlareType Type;

		// Token: 0x040000CD RID: 205
		public Vector2 Scale;

		// Token: 0x040000CE RID: 206
		public float Alpha;

		// Token: 0x040000CF RID: 207
		public float CenterShift = 1f;

		// Token: 0x040000D0 RID: 208
		public float MinDist;

		// Token: 0x040000D1 RID: 209
		public float MaxDist;

		// Token: 0x040000D2 RID: 210
		public float MinScale;

		// Token: 0x040000D3 RID: 211
		public float MaxScale;

		// Token: 0x040000D4 RID: 212
		public float MinAlpha;

		// Token: 0x040000D5 RID: 213
		public float MaxAlpha;

		// Token: 0x040000D6 RID: 214
		public float SvWidth;

		// Token: 0x040000D7 RID: 215
		public float SvShift;

		// Token: 0x040000D8 RID: 216
		public MultiFlare.RotationType RotType;

		// Token: 0x040000D9 RID: 217
		public Color Color;
	}
}
