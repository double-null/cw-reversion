using System;
using UnityEngine;

// Token: 0x02000383 RID: 899
internal class GUIMaskTex
{
	// Token: 0x06001D04 RID: 7428 RVA: 0x000FFE40 File Offset: 0x000FE040
	public GUIMaskTex(Texture texture, bool horAlign = false)
	{
		this.HorAlign = horAlign;
		this._texture = texture;
		this._screenPos = UtilsScreen.Rect;
		UtilsScreen.OnScreenChange += this.UpdateSize;
		this.Culc();
	}

	// Token: 0x1700083B RID: 2107
	// (get) Token: 0x06001D05 RID: 7429 RVA: 0x000FFE84 File Offset: 0x000FE084
	// (set) Token: 0x06001D06 RID: 7430 RVA: 0x000FFE8C File Offset: 0x000FE08C
	public Texture Texture
	{
		get
		{
			return this._texture;
		}
		set
		{
			if (value == this._texture)
			{
				return;
			}
			this._texture = value;
			this.Culc();
		}
	}

	// Token: 0x06001D07 RID: 7431 RVA: 0x000FFEB0 File Offset: 0x000FE0B0
	private void UpdateSize()
	{
		this._screenPos = UtilsScreen.Rect;
		this.Culc();
	}

	// Token: 0x06001D08 RID: 7432 RVA: 0x000FFEC4 File Offset: 0x000FE0C4
	private void Culc()
	{
		float num = this._screenPos.width / (float)this._texture.width;
		float num2 = this._screenPos.height / (float)this._texture.height;
		if (this.HorAlign)
		{
			float num3 = num / num2;
			this._maskUvPos = new Rect((1f - num3) * 0.5f, 0f, num3, 1f);
		}
		else
		{
			float num4 = num2 / num;
			this._maskUvPos = new Rect(0f, (1f - num4) * 0.5f, 1f, num4);
		}
		float x = this._maskUvPos.x;
		float y = this._maskUvPos.y;
		float x2 = x + this._maskUvPos.width;
		float y2 = y + this._maskUvPos.height;
		this._texCoords = new Vector3[]
		{
			new Vector3(x, y, 0f),
			new Vector3(x, y2, 0f),
			new Vector3(x2, y2, 0f),
			new Vector3(x2, y, 0f)
		};
	}

	// Token: 0x06001D09 RID: 7433 RVA: 0x00100010 File Offset: 0x000FE210
	public void Draw()
	{
		GUI.DrawTextureWithTexCoords(this._screenPos, this._texture, this._maskUvPos);
	}

	// Token: 0x06001D0A RID: 7434 RVA: 0x0010002C File Offset: 0x000FE22C
	public void DrawGL()
	{
		if (this._material == null)
		{
			string contents = "Shader \"Unlit/Transparent\" { Properties { _MainTex (\"Base (RGB) Trans (A)\", 2D) = \"white\" {}}SubShader {Tags {\"Queue\"=\"Transparent\" \"IgnoreProjector\"=\"True\" \"RenderType\"=\"Transparent\"}ZWrite Off Blend SrcAlpha OneMinusSrcAlpha Pass { Lighting Off SetTexture [_MainTex] { combine texture } } }}";
			this._material = new Material(contents);
			this._material.mainTexture = this._texture;
		}
		GL.PushMatrix();
		this._material.SetPass(0);
		GL.LoadOrtho();
		GL.Begin(7);
		GL.TexCoord(this._texCoords[0]);
		GL.Vertex3(0f, 0f, 0f);
		GL.TexCoord(this._texCoords[1]);
		GL.Vertex3(0f, 1f, 0f);
		GL.TexCoord(this._texCoords[2]);
		GL.Vertex3(1f, 1f, 0f);
		GL.TexCoord(this._texCoords[3]);
		GL.Vertex3(1f, 0f, 0f);
		GL.End();
		GL.PopMatrix();
	}

	// Token: 0x040021B6 RID: 8630
	private Material _material;

	// Token: 0x040021B7 RID: 8631
	private Texture _texture;

	// Token: 0x040021B8 RID: 8632
	private Rect _screenPos;

	// Token: 0x040021B9 RID: 8633
	private Rect _maskUvPos;

	// Token: 0x040021BA RID: 8634
	private Vector3[] _texCoords;

	// Token: 0x040021BB RID: 8635
	public bool HorAlign;
}
