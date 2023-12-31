using System;
using UnityEngine;

// Token: 0x020001B7 RID: 439
internal class BinocularHighLighting
{
	// Token: 0x06000F31 RID: 3889 RVA: 0x000AF7A0 File Offset: 0x000AD9A0
	public BinocularHighLighting()
	{
		this._centerArea = new BinocularHighLighting.CenterArea(StartData.binocular.Reticle, 0.23703703f, 0.3f);
	}

	// Token: 0x17000245 RID: 581
	// (set) Token: 0x06000F32 RID: 3890 RVA: 0x000AF7E0 File Offset: 0x000AD9E0
	public string DistanceText
	{
		set
		{
			this._centerArea.DistanceText = value;
		}
	}

	// Token: 0x17000246 RID: 582
	// (set) Token: 0x06000F33 RID: 3891 RVA: 0x000AF7F0 File Offset: 0x000AD9F0
	public string MultiplicityText
	{
		set
		{
			this._centerArea.MultiplicityText = value;
		}
	}

	// Token: 0x17000247 RID: 583
	// (get) Token: 0x06000F34 RID: 3892 RVA: 0x000AF800 File Offset: 0x000ADA00
	// (set) Token: 0x06000F35 RID: 3893 RVA: 0x000AF808 File Offset: 0x000ADA08
	public bool Enable { get; set; }

	// Token: 0x06000F36 RID: 3894 RVA: 0x000AF814 File Offset: 0x000ADA14
	public void Start(float delay)
	{
		if (!this.Enable)
		{
			return;
		}
		if (this._process)
		{
			return;
		}
		this._process = true;
		this._nextTime = Time.time + delay;
		this._delay = delay;
		this._progressVal = 0f;
	}

	// Token: 0x06000F37 RID: 3895 RVA: 0x000AF860 File Offset: 0x000ADA60
	public void Stop()
	{
		if (!this.Enable)
		{
			return;
		}
		if (!this._process)
		{
			return;
		}
		this._process = false;
	}

	// Token: 0x06000F38 RID: 3896 RVA: 0x000AF884 File Offset: 0x000ADA84
	public void Update()
	{
		if (BinocularHighLighting._camera == null)
		{
			BinocularHighLighting._camera = (Camera.mainCamera ?? Camera.main);
			if (BinocularHighLighting._camera == null)
			{
				return;
			}
		}
		if (!this.Enable)
		{
			return;
		}
		if (this._process)
		{
			if (this._nextTime < Time.time)
			{
				this._process = false;
				this.MarkObjects();
				this._nextTime = Time.time + this._duration;
			}
			else
			{
				this._progressVal = 1f - (this._nextTime - Time.time) / this._delay;
			}
		}
	}

	// Token: 0x06000F39 RID: 3897 RVA: 0x000AF934 File Offset: 0x000ADB34
	private void MarkObjects()
	{
		EntityMoveController[] array = (EntityMoveController[])UnityEngine.Object.FindObjectsOfType(typeof(EntityMoveController));
		foreach (EntityMoveController entityMoveController in array)
		{
			SkinnedMeshRenderer[] componentsInChildren = entityMoveController.GetComponentsInChildren<SkinnedMeshRenderer>(false);
			foreach (SkinnedMeshRenderer skinnedMeshRenderer in componentsInChildren)
			{
				if (skinnedMeshRenderer.enabled)
				{
					Rect rect;
					if (this.CalculateAndGetRect(skinnedMeshRenderer, out rect) && this._centerArea.Contains(rect))
					{
						int id = entityMoveController.transform.parent.GetComponent<EntityNetPlayer>().ID;
						this.MarkPlayer(id);
					}
				}
			}
		}
	}

	// Token: 0x06000F3A RID: 3898 RVA: 0x000AF9F4 File Offset: 0x000ADBF4
	public bool CalculateAndGetRect(Renderer renderer, out Rect rect)
	{
		rect = default(Rect);
		Bounds bounds = renderer.bounds;
		Vector3 center = bounds.center;
		Vector3 position = BinocularHighLighting._camera.transform.position;
		float magnitude = (center - position).magnitude;
		if (magnitude < 2f)
		{
			return false;
		}
		if (Physics.Linecast(center, position, 4458496))
		{
			return false;
		}
		center.y += bounds.extents.y;
		Vector3 b = BinocularHighLighting._camera.WorldToScreenPoint(center);
		center.y -= bounds.size.y;
		Vector3 vector = BinocularHighLighting._camera.WorldToScreenPoint(center) - b;
		vector.x = -vector.y / bounds.size.y;
		rect = new Rect(b.x - vector.x * 0.5f, (float)Screen.height - b.y, vector.x, -vector.y);
		return true;
	}

	// Token: 0x06000F3B RID: 3899 RVA: 0x000AFB10 File Offset: 0x000ADD10
	private void MarkPlayer(int id)
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
		if (Peer.ClientGame.IsTeamGame)
		{
			Peer.ClientGame.LocalPlayer.ToServer("PlayerHilight", new object[]
			{
				id
			});
		}
		else
		{
			EventFactory.Call("AddEnemy", id);
		}
	}

	// Token: 0x06000F3C RID: 3900 RVA: 0x000AFB70 File Offset: 0x000ADD70
	public void Draw()
	{
		this._centerArea.Draw();
		if (!this.Enable)
		{
			return;
		}
		if (this._process)
		{
			Rect position = new Rect((float)(Screen.width / 2 - 100), (float)(Screen.height - 30), 200f, (float)BannerGUI.I.placement_progressbar[0].height);
			Texture image = BannerGUI.I.placement_progressbar[1];
			GUI.color = new Color(1f, 1f, 1f, 0.15f);
			GUI.DrawTexture(position, image);
			position.width *= this._progressVal;
			GUI.color = Color.white;
			GUI.DrawTexture(position, image);
		}
	}

	// Token: 0x04000F7A RID: 3962
	private static GUIStyle _style;

	// Token: 0x04000F7B RID: 3963
	private static Camera _camera;

	// Token: 0x04000F7C RID: 3964
	private float _progressVal;

	// Token: 0x04000F7D RID: 3965
	private BinocularHighLighting.CenterArea _centerArea;

	// Token: 0x04000F7E RID: 3966
	private bool _process;

	// Token: 0x04000F7F RID: 3967
	private float _nextTime;

	// Token: 0x04000F80 RID: 3968
	private float _delay;

	// Token: 0x04000F81 RID: 3969
	private float _duration = 3f;

	// Token: 0x020001B8 RID: 440
	private class CenterArea
	{
		// Token: 0x06000F3D RID: 3901 RVA: 0x000AFC28 File Offset: 0x000ADE28
		public CenterArea(Texture texture, float texSizeRelativeToScreen, float sizeRelativeToTexture)
		{
			this._texture = texture;
			this._texSizeRelativeToScreen = texSizeRelativeToScreen;
			this._sizeRelativeToTexture = sizeRelativeToTexture;
			this._distanceStyle = new GUIStyle
			{
				alignment = TextAnchor.UpperLeft,
				normal = 
				{
					textColor = new Color(0f, 0f, 0f, 0.58f)
				},
				font = MainGUI.Instance.fontMicra
			};
			this._multiplicityStyle = new GUIStyle
			{
				alignment = TextAnchor.LowerRight,
				normal = 
				{
					textColor = new Color(0f, 0f, 0f, 0.58f)
				},
				font = MainGUI.Instance.fontMicra
			};
			UtilsScreen.OnScreenChange += this.UpdateSize;
			this.UpdateSize();
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x000AFCF8 File Offset: 0x000ADEF8
		private void UpdateSize()
		{
			this._center = UtilsScreen.Size * 0.5f;
			float num = UtilsScreen.Size.y * this._texSizeRelativeToScreen;
			float num2 = num * ((float)this._texture.width / (float)this._texture.height);
			float num3 = num * 0.5f;
			this._sqrRadius = num3 * this._sizeRelativeToTexture;
			this._viewFrame = new Rect(this._center.x - this._sqrRadius, this._center.y - this._sqrRadius, this._sqrRadius * 2f, this._sqrRadius * 2f);
			this._sqrRadius *= this._sqrRadius;
			this._reticleRect = new Rect(this._center.x - num2 * 0.5f, this._center.y - num3, num2, num);
			this._distanceRect = new Rect(this._reticleRect.xMin + this._reticleRect.width * 0.007f, this._reticleRect.y + this._reticleRect.height * 0.5f, this._reticleRect.width * 0.5f, 40f);
			this._multiplicityRect = new Rect(this._reticleRect.xMin + this._reticleRect.width * 0.5f, this._reticleRect.y + this._reticleRect.height * 0.475f - 40f, this._reticleRect.width * 0.495f, 40f);
			GUIStyle multiplicityStyle = this._multiplicityStyle;
			int fontSize = (int)(num * 0.05f);
			this._distanceStyle.fontSize = fontSize;
			multiplicityStyle.fontSize = fontSize;
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x000AFEC8 File Offset: 0x000AE0C8
		public void Draw()
		{
			GUI.DrawTexture(this._reticleRect, this._texture);
			GUI.Label(this._distanceRect, this.DistanceText, this._distanceStyle);
			GUI.Label(this._multiplicityRect, this.MultiplicityText, this._multiplicityStyle);
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x000AFF14 File Offset: 0x000AE114
		private bool RadiusContains(Vector2 point)
		{
			return this._sqrRadius > (this._center - point).sqrMagnitude;
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x000AFF40 File Offset: 0x000AE140
		public bool Contains(Rect rect)
		{
			if (!this.ContainsRect(rect))
			{
				return false;
			}
			if (rect.Contains(this._center))
			{
				return true;
			}
			if (this._center.x < rect.xMin)
			{
				if (this._center.y < rect.yMin)
				{
					return this.RadiusContains(new Vector2(rect.xMin, rect.yMin));
				}
				if (this._center.y > rect.yMax)
				{
					return this.RadiusContains(new Vector2(rect.xMin, rect.yMax));
				}
			}
			else if (this._center.x > rect.xMax)
			{
				if (this._center.y < rect.yMin)
				{
					return this.RadiusContains(new Vector2(rect.xMax, rect.yMin));
				}
				if (this._center.y > rect.yMax)
				{
					return this.RadiusContains(new Vector2(rect.xMax, rect.yMax));
				}
			}
			return true;
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x000B0068 File Offset: 0x000AE268
		private bool ContainsRect(Rect rect)
		{
			return this._viewFrame.xMin <= rect.xMax && this._viewFrame.xMax >= rect.xMin && this._viewFrame.yMin <= rect.yMax && this._viewFrame.yMax >= rect.yMin;
		}

		// Token: 0x04000F83 RID: 3971
		private Texture _texture;

		// Token: 0x04000F84 RID: 3972
		private Rect _reticleRect;

		// Token: 0x04000F85 RID: 3973
		private Rect _viewFrame;

		// Token: 0x04000F86 RID: 3974
		private float _texSizeRelativeToScreen;

		// Token: 0x04000F87 RID: 3975
		private float _sizeRelativeToTexture;

		// Token: 0x04000F88 RID: 3976
		private Vector2 _center;

		// Token: 0x04000F89 RID: 3977
		private float _sqrRadius;

		// Token: 0x04000F8A RID: 3978
		private GUIStyle _distanceStyle;

		// Token: 0x04000F8B RID: 3979
		private GUIStyle _multiplicityStyle;

		// Token: 0x04000F8C RID: 3980
		private Rect _distanceRect;

		// Token: 0x04000F8D RID: 3981
		private Rect _multiplicityRect;

		// Token: 0x04000F8E RID: 3982
		public string DistanceText;

		// Token: 0x04000F8F RID: 3983
		public string MultiplicityText;
	}
}
