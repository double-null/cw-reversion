using System;
using UnityEngine;

namespace LeagueGUI
{
	// Token: 0x0200032D RID: 813
	internal class Twister
	{
		// Token: 0x06001B77 RID: 7031 RVA: 0x000F7944 File Offset: 0x000F5B44
		public Twister()
		{
			this._twister = LeagueWindow.I.Textures.Twister;
			this._width = (float)this._twister.width;
			this._height = (float)this._twister.height;
			this._rect.Set(0f, 0f, this._width, this._height);
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x000F79B4 File Offset: 0x000F5BB4
		public void OnGUI(float xOffset = 0f, float yOffset = 0f, float speed = 180f)
		{
			this._rect.Set(xOffset, yOffset, this._width, this._height);
			float x = this._rect.xMin + this._width * 0.5f;
			float y = this._rect.yMin + this._height * 0.5f;
			GUIUtility.RotateAroundPivot(speed * Time.time, new Vector2(x, y));
			GUI.DrawTexture(this._rect, this._twister);
			GUIUtility.RotateAroundPivot(-speed * Time.time, new Vector2(x, y));
		}

		// Token: 0x04002046 RID: 8262
		private Texture2D _twister;

		// Token: 0x04002047 RID: 8263
		private Rect _rect;

		// Token: 0x04002048 RID: 8264
		private float _width;

		// Token: 0x04002049 RID: 8265
		private float _height;
	}
}
