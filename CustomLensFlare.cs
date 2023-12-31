using System;
using UnityEngine;

// Token: 0x020001C0 RID: 448
[Serializable]
internal class CustomLensFlare
{
	// Token: 0x06000F63 RID: 3939 RVA: 0x000B0BE8 File Offset: 0x000AEDE8
	public void Initialize()
	{
		if (this.FlareMaterial == null || this.Elements.Length == 0)
		{
			return;
		}
		Vector2 texScale = new Vector2((float)this.GridSize / (float)this.FlareMaterial.mainTexture.width, (float)this.GridSize / (float)this.FlareMaterial.mainTexture.height);
		float screenAspect = (float)Screen.width / (float)Screen.height;
		this._center = new Vector2(0.5f, 0.5f);
		foreach (CustomLensFlare.Element element in this.Elements)
		{
			element.Calc(texScale, screenAspect);
		}
	}

	// Token: 0x06000F64 RID: 3940 RVA: 0x000B0C9C File Offset: 0x000AEE9C
	public void Draw(Vector2 positionOnScreen)
	{
		if (this.FlareMaterial == null || this.Elements.Length == 0)
		{
			return;
		}
		Vector2 a = this._center - positionOnScreen;
		float num = a.magnitude;
		float alpha = 1f - num * 0.65f;
		num = Mathf.Sqrt(num * 0.333333f);
		GL.PushMatrix();
		this.FlareMaterial.SetPass(0);
		GL.LoadOrtho();
		GL.Begin(7);
		foreach (CustomLensFlare.Element element in this.Elements)
		{
			Vector2 pos = this._center + a * element.Position;
			element.Draw(pos, num, alpha);
		}
		GL.End();
		GL.PopMatrix();
	}

	// Token: 0x04000FB1 RID: 4017
	public Material FlareMaterial;

	// Token: 0x04000FB2 RID: 4018
	public int GridSize = 64;

	// Token: 0x04000FB3 RID: 4019
	public CustomLensFlare.Element[] Elements;

	// Token: 0x04000FB4 RID: 4020
	private Vector2 _center;

	// Token: 0x020001C1 RID: 449
	[Serializable]
	internal class Element
	{
		// Token: 0x06000F66 RID: 3942 RVA: 0x000B0D74 File Offset: 0x000AEF74
		public void Calc(Vector2 texScale, float screenAspect)
		{
			float x = (float)this.X * texScale.x;
			float y = (float)this.Y * texScale.y;
			float x2 = (float)(this.X + this.Width) * texScale.x;
			float y2 = (float)(this.Y + this.Heigth) * texScale.y;
			this._texCoords = new Vector3[]
			{
				new Vector3(x, y, 0f),
				new Vector3(x, y2, 0f),
				new Vector3(x2, y2, 0f),
				new Vector3(x2, y, 0f)
			};
			this._screenAspect = screenAspect;
			this._halfSize = new Vector2(1f, screenAspect) * this.Size * 0.5f;
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x000B0E68 File Offset: 0x000AF068
		public void Draw(Vector2 pos, float distance, float alpha)
		{
			GL.Color(this.Color * alpha);
			if (this.Rotation == 0f)
			{
				GL.TexCoord(this._texCoords[0]);
				GL.Vertex3(pos.x - this._halfSize.x, pos.y - this._halfSize.y, 0f);
				GL.TexCoord(this._texCoords[1]);
				GL.Vertex3(pos.x - this._halfSize.x, pos.y + this._halfSize.y, 0f);
				GL.TexCoord(this._texCoords[2]);
				GL.Vertex3(pos.x + this._halfSize.x, pos.y + this._halfSize.y, 0f);
				GL.TexCoord(this._texCoords[3]);
				GL.Vertex3(pos.x + this._halfSize.x, pos.y - this._halfSize.y, 0f);
			}
			else
			{
				float f = distance * this.Rotation;
				float num = Mathf.Cos(f);
				float num2 = Mathf.Sin(f);
				float num3 = (num - num2) * this._halfSize.x;
				float num4 = (num + num2) * this._halfSize.x;
				float num5 = num3 * this._screenAspect;
				float num6 = num4 * this._screenAspect;
				GL.TexCoord(this._texCoords[0]);
				GL.Vertex3(pos.x - num3, pos.y - num6, 0f);
				GL.TexCoord(this._texCoords[1]);
				GL.Vertex3(pos.x - num4, pos.y + num5, 0f);
				GL.TexCoord(this._texCoords[2]);
				GL.Vertex3(pos.x + num3, pos.y + num6, 0f);
				GL.TexCoord(this._texCoords[3]);
				GL.Vertex3(pos.x + num4, pos.y - num5, 0f);
			}
		}

		// Token: 0x04000FB5 RID: 4021
		public float Position;

		// Token: 0x04000FB6 RID: 4022
		public float Size;

		// Token: 0x04000FB7 RID: 4023
		public float Rotation;

		// Token: 0x04000FB8 RID: 4024
		public Color Color;

		// Token: 0x04000FB9 RID: 4025
		public int X;

		// Token: 0x04000FBA RID: 4026
		public int Y;

		// Token: 0x04000FBB RID: 4027
		public int Width;

		// Token: 0x04000FBC RID: 4028
		public int Heigth;

		// Token: 0x04000FBD RID: 4029
		private Vector3[] _texCoords;

		// Token: 0x04000FBE RID: 4030
		private Vector2 _halfSize;

		// Token: 0x04000FBF RID: 4031
		private float _screenAspect;
	}
}
