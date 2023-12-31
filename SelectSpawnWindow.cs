using System;
using System.Collections.Generic;
using Samples;
using UnityEngine;

// Token: 0x0200017C RID: 380
[Serializable]
internal class SelectSpawnWindow : Execute
{
	// Token: 0x1700014B RID: 331
	// (get) Token: 0x06000ACC RID: 2764 RVA: 0x00083714 File Offset: 0x00081914
	// (set) Token: 0x06000ACD RID: 2765 RVA: 0x0008372C File Offset: 0x0008192C
	public Texture2D spawnMap
	{
		get
		{
			return this.level ?? this.mapTexture;
		}
		set
		{
			this.level = value;
		}
	}

	// Token: 0x06000ACE RID: 2766 RVA: 0x00083738 File Offset: 0x00081938
	public void Clear()
	{
		this.buttonsToUpdatePos.Clear();
		this.engine.Clear();
	}

	// Token: 0x06000ACF RID: 2767 RVA: 0x00083750 File Offset: 0x00081950
	public void SetTacticalPoints(TacticalPoint[] points)
	{
		this.Clear();
		if (points == null)
		{
			return;
		}
		this.tacticalPoints = points;
		for (int i = 0; i < points.Length; i++)
		{
			Vector2 vector = this.Convert(points[i].gameObject.transform.position, this.UpperLeft, this.LowerRight, this.mapTexture);
			SpawnButton spawnButton = new SpawnButton(vector.x, vector.y, this.GetImg(points[i]), points[i].NumberOfPoint, !points[i].IsBasePoint);
			this.buttonsToUpdatePos.Add(spawnButton);
			this.engine.Add(spawnButton);
			if (points[i].IsHomebase(Peer.ClientGame.LocalPlayer))
			{
				spawnButton.OnClick();
			}
		}
	}

	// Token: 0x06000AD0 RID: 2768 RVA: 0x00083818 File Offset: 0x00081A18
	private GUIContent GetImg(TacticalPoint p)
	{
		if (p.pointState == TacticalPointState.neutral)
		{
			return this.neutral;
		}
		if ((p.pointState == TacticalPointState.usec_homeBase && Peer.ClientGame.LocalPlayer.IsBear) || (p.pointState == TacticalPointState.bear_homeBase && !Peer.ClientGame.LocalPlayer.IsBear))
		{
			return this.enemyHome;
		}
		if ((p.pointState == TacticalPointState.usec_homeBase && !Peer.ClientGame.LocalPlayer.IsBear) || (p.pointState == TacticalPointState.bear_homeBase && Peer.ClientGame.LocalPlayer.IsBear))
		{
			return this.homePoint;
		}
		if ((p.pointState == TacticalPointState.usec_captured && Peer.ClientGame.LocalPlayer.IsBear) || (p.pointState == TacticalPointState.bear_captured && !Peer.ClientGame.LocalPlayer.IsBear))
		{
			return this.enemyPoint;
		}
		if ((p.pointState == TacticalPointState.usec_captured && !Peer.ClientGame.LocalPlayer.IsBear) || (p.pointState == TacticalPointState.bear_captured && Peer.ClientGame.LocalPlayer.IsBear))
		{
			return this.spawnPoint;
		}
		return null;
	}

	// Token: 0x06000AD1 RID: 2769 RVA: 0x0008395C File Offset: 0x00081B5C
	public void execute()
	{
		if (!Main.IsDeadOrSpectactor)
		{
			return;
		}
		Peer.ClientGame.LocalPlayer.ChooseAmmunition();
		Peer.ClientGame.LocalPlayer.Spawn(this.engine.SelectedID);
	}

	// Token: 0x06000AD2 RID: 2770 RVA: 0x000839A0 File Offset: 0x00081BA0
	public void AddHomePoint(Vector3 pos, int ID)
	{
		if (this.spawnMap == null)
		{
			return;
		}
		Vector2 vector = this.Convert(pos, this.UpperLeft, this.LowerRight, this.spawnMap);
		this.engine.Add(new SpawnButton(this.pos.x + vector.x, this.pos.y + vector.y + 50f, this.spawnPoint, ID, false));
	}

	// Token: 0x06000AD3 RID: 2771 RVA: 0x00083A20 File Offset: 0x00081C20
	public void AddPoint(Vector3 pos, int ID)
	{
		if (this.spawnMap == null)
		{
			return;
		}
		Vector2 vector = this.Convert(pos, this.UpperLeft, this.LowerRight, this.spawnMap);
		this.engine.Add(new SpawnButton(this.pos.x + vector.x, this.pos.y + vector.y + 50f, this.spawnPoint, ID, false));
	}

	// Token: 0x06000AD4 RID: 2772 RVA: 0x00083AA0 File Offset: 0x00081CA0
	public void SetTimerButton(float t)
	{
		int num = 0;
		int num2 = 0;
		if (this.timeButton != null && this.tacticalPoints.Length > this.engine.SelectedID)
		{
			for (int i = 0; i < this.tacticalPoints.Length; i++)
			{
				if (this.tacticalPoints[i].isEnemy(Peer.ClientGame.LocalPlayer) == 0 && !this.tacticalPoints[i].IsBasePoint)
				{
					num2++;
				}
				if (this.tacticalPoints[i].isEnemy(Peer.ClientGame.LocalPlayer) == -1 && !this.tacticalPoints[i].IsBasePoint)
				{
					num++;
				}
			}
			if (num2 == 0 && num == 0)
			{
				t *= 3f;
			}
			if (this.tacticalPoints[this.engine.SelectedID].IsBasePoint)
			{
				this.timeButton.Set(t);
			}
			else
			{
				this.timeButton.Set(t * 1.67f);
			}
		}
	}

	// Token: 0x06000AD5 RID: 2773 RVA: 0x00083BA8 File Offset: 0x00081DA8
	private Vector2 Convert(Vector3 pos, Vector3 UpperLeft, Vector3 LowerRight, Texture2D level)
	{
		Vector3 vector = UpperLeft - LowerRight;
		vector.x = Mathf.Abs(vector.x);
		vector.y = Mathf.Abs(vector.y);
		vector.z = Mathf.Abs(vector.z);
		Vector2 result = new Vector2(Mathf.Abs(UpperLeft.x - pos.x) / vector.x * (float)level.width, Mathf.Abs(UpperLeft.z - pos.z) / vector.z * (float)level.height);
		return result;
	}

	// Token: 0x06000AD6 RID: 2774 RVA: 0x00083C48 File Offset: 0x00081E48
	public void SelectDefaultPoint(bool isBear)
	{
		for (int i = 0; i < this.tacticalPoints.Length; i++)
		{
			if (this.tacticalPoints[i].IsHomebase(Peer.ClientGame.LocalPlayer))
			{
				this.engine.ClickByID(i);
				return;
			}
		}
	}

	// Token: 0x06000AD7 RID: 2775 RVA: 0x00083C98 File Offset: 0x00081E98
	public void DeletePoint(int ID)
	{
		this.engine.DeleteByID(ID);
	}

	// Token: 0x06000AD8 RID: 2776 RVA: 0x00083CA8 File Offset: 0x00081EA8
	public void OnStart()
	{
		this.mapHint.text = Language.ChooseDislocate;
		this.spawnButton.text = Language.Dislocate;
		this.RefreshPos();
		if (this.timeButton == null)
		{
			this.timeButton = new TimerButton(this.pos.x - 5f, this.pos.y + 275f, this.spawnButton);
		}
		this.RefreshPos();
		this.BackgrOffsetX = CWGUI.CalcCentredOffset((float)this.RadarTexture.width, (float)this.mapTexture.width);
		this.timeButton.ex = this;
	}

	// Token: 0x06000AD9 RID: 2777 RVA: 0x00083D50 File Offset: 0x00081F50
	private void RefreshPos()
	{
		if (this.CenteredX)
		{
			this.pos.x = (float)((Screen.width - this.RadarTexture.width) / 2 + 20);
		}
		if (this.CenteredY)
		{
			this.pos.y = (float)((Screen.height - this.RadarTexture.height) / 2 - 65);
		}
		this.pos += this.publicPos;
		if (this.timeButton != null)
		{
			this.timeButton.SetPos(this.pos.x - 5f, this.pos.y + 275f);
		}
		if (this.engine.SelectedID >= 0 && this.engine.SelectedID < this.tacticalPoints.Length)
		{
			this.radarDescription.text = this.tacticalPoints[this.engine.SelectedID].Name;
		}
		else
		{
			this.radarDescription.text = string.Empty;
		}
	}

	// Token: 0x06000ADA RID: 2778 RVA: 0x00083E68 File Offset: 0x00082068
	public void OnUpdate()
	{
		if (this.tacticalPoints == null || this.buttonsToUpdatePos.Count != this.tacticalPoints.Length)
		{
			return;
		}
		if (this.RadarTexture != null)
		{
			this.RefreshPos();
		}
		if (this.tacticalPoints == null)
		{
			return;
		}
		for (int i = 0; i < this.tacticalPoints.Length; i++)
		{
			TacticalPointState pointState = this.tacticalPoints[i].pointState;
			if ((pointState != TacticalPointState.usec_homeBase && Peer.ClientGame.LocalPlayer.IsBear) || (pointState != TacticalPointState.bear_homeBase && !Peer.ClientGame.LocalPlayer.IsBear))
			{
				this.buttonsToUpdatePos[i].isClickable = (this.tacticalPoints[i].isEnemy(Peer.ClientGame.LocalPlayer) == 1);
				this.buttonsToUpdatePos[i].SetContent(this.GetImg(this.tacticalPoints[i]));
				Vector2 vector = this.Convert(this.tacticalPoints[i].Pos, this.UpperLeft, this.LowerRight, this.mapTexture);
				this.buttonsToUpdatePos[i].SetPos(this.pos.x + vector.x, this.pos.y + vector.y + 50f);
				this.buttonsToUpdatePos[i].Enabled = true;
			}
			else
			{
				this.buttonsToUpdatePos[i].Enabled = false;
			}
		}
		int selectedID = this.engine.SelectedID;
		if (selectedID != -1 && this.tacticalPoints.Length > selectedID && this.tacticalPoints[selectedID].isEnemy(Peer.ClientGame.LocalPlayer) != 1)
		{
			this.SelectDefaultPoint(Peer.ClientGame.LocalPlayer.IsBear);
		}
	}

	// Token: 0x06000ADB RID: 2779 RVA: 0x00084044 File Offset: 0x00082244
	public void Hide()
	{
		this.alpha.Hide(0f, 0f);
	}

	// Token: 0x1700014C RID: 332
	// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0008405C File Offset: 0x0008225C
	public bool isHidden
	{
		get
		{
			return this.alpha.Hiding;
		}
	}

	// Token: 0x06000ADD RID: 2781 RVA: 0x0008406C File Offset: 0x0008226C
	public void OnGUI()
	{
		if (!this.Enabled)
		{
			return;
		}
		if (!this.alpha.Showing)
		{
			this.alpha.Show(0.5f, 0f);
		}
		if (this.alpha.visibility > 0f)
		{
			GUI.color = Colors.alpha(GUI.color, this.alpha.visibility * GUI.color.a);
		}
		GUI.color = Colors.alpha(GUI.color, GUI.color.a * 0.5f);
		GUI.DrawTexture(new Rect(this.pos.x, this.pos.y + 25f, (float)this.spawnMap.width, 25f), CWGUI.p.black, ScaleMode.StretchToFill, true);
		GUI.color = Colors.alpha(GUI.color, GUI.color.a * 2f);
		GUI.Label(new Rect(this.pos.x, this.pos.y + 25f, (float)this.spawnMap.width, 25f), this.mapHint, CWGUI.p.blackBackText);
		GUI.DrawTexture(new Rect(this.pos.x - this.BackgrOffsetX, this.pos.y + 37f, (float)this.RadarTexture.width, (float)this.RadarTexture.height), this.RadarTexture);
		GUI.DrawTexture(new Rect(this.pos.x, this.pos.y + 50f, (float)this.spawnMap.height, (float)this.spawnMap.height), this.spawnMap);
		this.engine.OnGUI();
		GUI.Label(new Rect(this.pos.x, this.pos.y + 50f + (float)this.spawnMap.height, (float)this.spawnMap.width, 50f), this.radarDescription, CWGUI.p.standartWhiteKorataki);
		this.timeButton.OnGUI();
		if (this.alpha.visibility > 0f)
		{
			GUI.color = Colors.alpha(GUI.color, this.alpha.visibility * GUI.color.a);
		}
	}

	// Token: 0x06000ADE RID: 2782 RVA: 0x000842EC File Offset: 0x000824EC
	public void OnGUI(float alpha)
	{
		this.lastColor = GUI.color;
		GUI.color = Colors.alpha(GUI.color, alpha);
		this.OnGUI();
		GUI.color = Colors.alpha(GUI.color, this.lastColor.a);
	}

	// Token: 0x04000CCF RID: 3279
	public Vector2 publicPos = default(Vector2);

	// Token: 0x04000CD0 RID: 3280
	private Vector2 pos = default(Vector2);

	// Token: 0x04000CD1 RID: 3281
	public bool CenteredX;

	// Token: 0x04000CD2 RID: 3282
	public bool CenteredY;

	// Token: 0x04000CD3 RID: 3283
	public GUIContent homePoint = new GUIContent();

	// Token: 0x04000CD4 RID: 3284
	public GUIContent enemyPoint = new GUIContent();

	// Token: 0x04000CD5 RID: 3285
	public GUIContent enemyHome = new GUIContent();

	// Token: 0x04000CD6 RID: 3286
	public GUIContent neutral = new GUIContent();

	// Token: 0x04000CD7 RID: 3287
	public GUIContent spawnPoint = new GUIContent();

	// Token: 0x04000CD8 RID: 3288
	public GUIContent spawnButton = new GUIContent();

	// Token: 0x04000CD9 RID: 3289
	public GUIContent mapHint = new GUIContent();

	// Token: 0x04000CDA RID: 3290
	public Texture2D mapTexture;

	// Token: 0x04000CDB RID: 3291
	protected Texture2D level;

	// Token: 0x04000CDC RID: 3292
	public Texture2D RadarTexture;

	// Token: 0x04000CDD RID: 3293
	[HideInInspector]
	public Vector3 UpperLeft = default(Vector3);

	// Token: 0x04000CDE RID: 3294
	[HideInInspector]
	public Vector3 LowerRight = default(Vector3);

	// Token: 0x04000CDF RID: 3295
	public bool Enabled = true;

	// Token: 0x04000CE0 RID: 3296
	public float visibility;

	// Token: 0x04000CE1 RID: 3297
	[HideInInspector]
	public SpawnEngine engine = new SpawnEngine();

	// Token: 0x04000CE2 RID: 3298
	private TimerButton timeButton;

	// Token: 0x04000CE3 RID: 3299
	private GUIContent radarDescription = new GUIContent();

	// Token: 0x04000CE4 RID: 3300
	private List<SpawnButton> buttonsToUpdatePos = new List<SpawnButton>();

	// Token: 0x04000CE5 RID: 3301
	private TacticalPoint[] tacticalPoints;

	// Token: 0x04000CE6 RID: 3302
	private float BackgrOffsetX;

	// Token: 0x04000CE7 RID: 3303
	private Color lastColor;

	// Token: 0x04000CE8 RID: 3304
	private Alpha alpha = new Alpha();
}
