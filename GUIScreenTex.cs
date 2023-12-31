using System;
using UnityEngine;

// Token: 0x02000384 RID: 900
internal class GUIScreenTex
{
	// Token: 0x06001D0B RID: 7435 RVA: 0x0010013C File Offset: 0x000FE33C
	public GUIScreenTex(Texture texture)
	{
		this.Texture = texture;
		this._screenPos = UtilsScreen.Rect;
		UtilsScreen.OnScreenChange += this.UpdateSize;
	}

	// Token: 0x06001D0C RID: 7436 RVA: 0x00100168 File Offset: 0x000FE368
	private void UpdateSize()
	{
		this._screenPos = UtilsScreen.Rect;
	}

	// Token: 0x06001D0D RID: 7437 RVA: 0x00100178 File Offset: 0x000FE378
	public void Draw()
	{
		GUI.DrawTexture(this._screenPos, this.Texture);
	}

	// Token: 0x06001D0E RID: 7438 RVA: 0x0010018C File Offset: 0x000FE38C
	public void Draw(float alpha)
	{
		Color color = GUI.color;
		GUI.color = new Color(0f, 0f, 0f, alpha);
		GUI.DrawTexture(this._screenPos, this.Texture);
		GUI.color = color;
	}

	// Token: 0x06001D0F RID: 7439 RVA: 0x001001D0 File Offset: 0x000FE3D0
	public void DrawGL(Color color)
	{
	}

	// Token: 0x040021BC RID: 8636
	public Texture Texture;

	// Token: 0x040021BD RID: 8637
	private Rect _screenPos;
}
