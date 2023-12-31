using System;
using UnityEngine;

namespace GUIComponent.Radar
{
	// Token: 0x02000169 RID: 361
	internal class Radar : IRadar
	{
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x00069404 File Offset: 0x00067604
		// (set) Token: 0x060009C6 RID: 2502 RVA: 0x0006940C File Offset: 0x0006760C
		public Vector3 MapSize
		{
			get
			{
				return this._mapSize;
			}
			set
			{
				this._mapSize.x = Mathf.Abs(value.x);
				this._mapSize.y = Mathf.Abs(value.y);
				this._mapSize.z = Mathf.Abs(value.z);
			}
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x00069460 File Offset: 0x00067660
		protected Vector2 WorldPosToRadar(Vector3 pos)
		{
			return new Vector2(Mathf.Abs(this.UpperLeft.x - pos.x) / this._mapSize.x * this.RadarSize.x + this.RadarPos.x, Mathf.Abs(this.UpperLeft.z - pos.z) / this._mapSize.z * this.RadarSize.y + this.RadarPos.y);
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x000694EC File Offset: 0x000676EC
		protected void DrawPoint(Vector2 pos, Texture2D icon)
		{
			if (icon != null)
			{
				GUI.DrawTexture(new Rect(pos.x - (float)icon.width / 2f, pos.y - (float)icon.height / 2f, (float)icon.width, (float)icon.height), icon, ScaleMode.ScaleToFit, true);
			}
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0006954C File Offset: 0x0006774C
		public void DrawPoint(Vector3 pos, Texture2D icon)
		{
			this.DrawPoint(this.WorldPosToRadar(pos), icon);
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0006955C File Offset: 0x0006775C
		protected void DrawRotatePoint(Vector2 pos, float rotation, Texture2D icon)
		{
			if (icon != null)
			{
				pos.x -= this.RadarPos.x;
				pos.y -= this.RadarPos.y;
				this.lastMatrix = GUI.matrix;
				GUI.matrix = Matrix4x4.TRS(pos, Quaternion.Euler(0f, 0f, rotation), Vector3.one) * Matrix4x4.TRS(-pos, Quaternion.identity, Vector3.one);
				GUI.DrawTexture(new Rect(pos.x - (float)icon.width / 2f, pos.y - (float)icon.height / 2f, (float)icon.width, (float)icon.height), icon, ScaleMode.ScaleToFit, true);
				GUI.matrix = this.lastMatrix;
			}
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00069648 File Offset: 0x00067848
		public void DrawRotatePoint(Vector3 pos, float rotation, Texture2D icon)
		{
			this.DrawRotatePoint(this.WorldPosToRadar(pos), rotation, icon);
		}

		// Token: 0x04000B33 RID: 2867
		private Vector3 _mapSize = default(Vector3);

		// Token: 0x04000B34 RID: 2868
		public Vector3 UpperLeft = default(Vector3);

		// Token: 0x04000B35 RID: 2869
		public Vector3 LowerRight = default(Vector3);

		// Token: 0x04000B36 RID: 2870
		public Vector2 RadarSize = default(Vector2);

		// Token: 0x04000B37 RID: 2871
		public Vector2 RadarPos = new Vector2(500f, 500f);

		// Token: 0x04000B38 RID: 2872
		private Matrix4x4 lastMatrix = default(Matrix4x4);
	}
}
