using System;
using System.Collections.Generic;
using System.Reflection;
using cygwin_x32.ObscuredTypes;
using GUIComponent.Radar;
using UnityEngine;

// Token: 0x02000170 RID: 368
[AddComponentMenu("Scripts/GUI/Radar")]
internal class RadarGUI : Form
{
	// Token: 0x06000A54 RID: 2644 RVA: 0x00078FF8 File Offset: 0x000771F8
	[Obfuscation(Exclude = true)]
	public void AddEnemy(object obj)
	{
	}

	// Token: 0x06000A55 RID: 2645 RVA: 0x00078FFC File Offset: 0x000771FC
	[Obfuscation(Exclude = true)]
	public void AddHit(object obj)
	{
		if (!Main.IsAlive)
		{
			return;
		}
		int num = (int)obj;
		if (num == Peer.ClientGame.LocalPlayer.ID)
		{
			return;
		}
		this.hits.Add(new EnemyRadarPosition(num));
	}

	// Token: 0x06000A56 RID: 2646 RVA: 0x00079048 File Offset: 0x00077248
	[Obfuscation(Exclude = true)]
	public void Beep(object obj)
	{
		Audio.Play(this.beep);
	}

	// Token: 0x06000A57 RID: 2647 RVA: 0x00079058 File Offset: 0x00077258
	[Obfuscation(Exclude = true)]
	public void AddHotspot(object obj)
	{
		if (!Main.IsAlive)
		{
			return;
		}
		object[] args = (object[])obj;
		ObscuredInt value = (ObscuredInt)Crypt.ResolveVariable(args, 0, 0);
		float distance = (float)Crypt.ResolveVariable(args, 0f, 1);
		for (int i = 0; i < this.hotspots.Count; i++)
		{
			if (this.hotspots[i].Enemy.ID == value)
			{
				return;
			}
		}
		this.hotspots.Add(new HotspotRadarState(value, distance));
	}

	// Token: 0x06000A58 RID: 2648 RVA: 0x000790FC File Offset: 0x000772FC
	private void RadarPoint(RadarEntityType type, Vector3 pos, float rot, PlayerClass playerClass = PlayerClass.none, bool hostile = false)
	{
		Vector3 vector = this.UpperLeft - this.LowerRight;
		vector.x = Mathf.Abs(vector.x);
		vector.y = Mathf.Abs(vector.y);
		vector.z = Mathf.Abs(vector.z);
		this.gui.BeginGroup(new Rect(18f, 12f, (float)this.level.width, (float)this.level.height));
		Vector2 vector2 = new Vector2(Mathf.Abs(this.UpperLeft.x - pos.x) / vector.x * (float)this.level.width, Mathf.Abs(this.UpperLeft.z - pos.z) / vector.z * (float)this.level.height);
		this.gui.color = new Color(1f, 1f, 1f, Mathf.Min(Main.UserInfo.settings.radarAlpha, 1f));
		if (type == RadarEntityType.Player)
		{
			this.gui.RotateGUI(rot, vector2);
			this.gui.PictureScreenSpace(vector2, this.player, Vector2.one * this.scale);
			this.gui.RotateGUI(0f, Vector2.zero);
		}
		else if (type == RadarEntityType.Team)
		{
			this.gui.RotateGUI(rot, vector2);
			this.gui.PictureScreenSpace(vector2, this.team, Vector2.one * this.scale);
			this.gui.RotateGUI(0f, Vector2.zero);
		}
		else if (type == RadarEntityType.vip)
		{
			this.gui.PictureScreenSpace(vector2, this.vipIcons[(!hostile) ? 0 : 1], Vector2.one * this.scale);
		}
		else if (type == RadarEntityType.radio)
		{
			this.gui.color = new Color(1f, 1f, 1f, Mathf.Min(this.tickAlpha.Evaluate(Utility.Period(this.tickScaleKeys[this.tickScaleKeys.Length - 1].value, 2f)) * Main.UserInfo.settings.radarAlpha, 1f));
			this.gui.PictureScreenSpace(vector2, this.radioIcon[0], Vector2.one * this.scale);
		}
		this.gui.EndGroup();
	}

	// Token: 0x06000A59 RID: 2649 RVA: 0x0007939C File Offset: 0x0007759C
	private void MortarStrike(Vector3 pos)
	{
		if (pos.x > this.LowerRight.x || pos.z < this.LowerRight.z || pos.x < this.UpperLeft.x || pos.z > this.UpperLeft.z)
		{
			return;
		}
		Vector3 vector = this.UpperLeft - this.LowerRight;
		vector.x = Mathf.Abs(vector.x);
		vector.y = Mathf.Abs(vector.y);
		vector.z = Mathf.Abs(vector.z);
		Vector2 vector2 = new Vector2(Mathf.Abs(this.UpperLeft.x - pos.x) / vector.x * (float)this.level.width + 20f, Mathf.Abs(this.UpperLeft.z - pos.z) / vector.z * (float)this.level.height + 19f);
		GUIUtility.ScaleAroundPivot(Vector2.one * this.scale, vector2);
		this.gui.color = new Color(1f, 0.2f, 0.2f, Mathf.Min(Main.UserInfo.settings.radarAlpha, 1f));
		this.gui.PictureScreenSpace(vector2, this.mortarStrike, Vector2.one);
		this.gui.RotateGUI(0f, Vector2.zero);
		this.gui.BeginGroup(new Rect(18f, 12f, (float)this.level.width, (float)this.level.height));
		this.gui.color = new Color(1f, 0.2f, 0.2f, Mathf.Min(this.tickAlpha.Evaluate(Utility.Period(this.tickAlphaKeys[this.tickAlphaKeys.Length - 1].value, 1f)) * Main.UserInfo.settings.radarAlpha, 1f));
		this.gui.PictureCentered(vector2 - new Vector2(18f, 12f), this.radarTick, Vector2.one * this.scale * this.tickScale.Evaluate(Utility.Period(this.tickScaleKeys[this.tickScaleKeys.Length - 1].value, 1f)));
		this.gui.EndGroup();
	}

	// Token: 0x06000A5A RID: 2650 RVA: 0x00079648 File Offset: 0x00077848
	private void Sonar(Vector3 pos, float time)
	{
		if (pos.x > this.LowerRight.x || pos.z < this.LowerRight.z || pos.x < this.UpperLeft.x || pos.z > this.UpperLeft.z)
		{
			return;
		}
		Vector3 vector = this.UpperLeft - this.LowerRight;
		vector.x = Mathf.Abs(vector.x);
		vector.y = Mathf.Abs(vector.y);
		vector.z = Mathf.Abs(vector.z);
		Vector2 vector2 = new Vector2(Mathf.Abs(this.UpperLeft.x - pos.x) / vector.x * (float)this.level.width + 20f, Mathf.Abs(this.UpperLeft.z - pos.z) / vector.z * (float)this.level.height + 19f);
		GUIUtility.ScaleAroundPivot(Vector2.one * this.scale, vector2);
		this.gui.color = new Color(0.2f, 1f, 0.2f, Mathf.Min(Main.UserInfo.settings.radarAlpha, 1f));
		this.gui.PictureScreenSpace(vector2, this.sonar, Vector2.one);
		this.gui.color = new Color(1f, 1f, 1f, Mathf.Min(Main.UserInfo.settings.radarAlpha, 1f));
		this.gui.TextLabel(new Rect(vector2.x - 100f - 2f, vector2.y - 25f + 13f, 200f, 50f), time.ToString("F0"), 16, "#FFFFFF", TextAnchor.MiddleCenter, true);
		this.gui.RotateGUI(0f, Vector2.zero);
		this.gui.BeginGroup(new Rect(18f, 12f, (float)this.level.width, (float)this.level.height));
		this.gui.color = new Color(0.2f, 1f, 0.2f, Mathf.Min(this.tickAlpha.Evaluate(Utility.Period(this.tickScaleKeys[this.tickScaleKeys.Length - 1].value, 1f)) * Main.UserInfo.settings.radarAlpha, 1f));
		this.gui.PictureCentered(vector2 - new Vector2(18f, 12f), this.radarTick, Vector2.one * this.scale * this.tickScale.Evaluate(Utility.Period(this.tickScaleKeys[this.tickScaleKeys.Length - 1].value, 1f)) * 3f);
		this.gui.EndGroup();
	}

	// Token: 0x06000A5B RID: 2651 RVA: 0x0007998C File Offset: 0x00077B8C
	private void Placement(Vector3 pos)
	{
		if (pos.x > this.LowerRight.x || pos.z < this.LowerRight.z || pos.x < this.UpperLeft.x || pos.z > this.UpperLeft.z)
		{
			return;
		}
		Vector3 vector = this.UpperLeft - this.LowerRight;
		vector.x = Mathf.Abs(vector.x);
		vector.y = Mathf.Abs(vector.y);
		vector.z = Mathf.Abs(vector.z);
		Vector2 vector2 = new Vector2(Mathf.Abs(this.UpperLeft.x - pos.x) / vector.x * (float)this.level.width, Mathf.Abs(this.UpperLeft.z - pos.z) / vector.z * (float)this.level.height);
		this.gui.color = new Color(1f, 1f, 1f, Mathf.Min(Main.UserInfo.settings.radarAlpha, 1f));
		this.gui.PictureScreenSpace(vector2 + new Vector2(18f, 12f), this.placement, Vector2.one * this.scale);
		vector2 += new Vector2(18f, 12f);
		GUIUtility.ScaleAroundPivot(Vector2.one * this.scale, vector2);
		if (Peer.ClientGame.Placement != null)
		{
			string content = EnumNames.PlacementTypeName(Peer.ClientGame.Placement.PointType);
			this.gui.TextLabel(new Rect(vector2.x - 102f, vector2.y - 38f, 200f, 50f), content, 15, "#FFFFFF_Micra", TextAnchor.MiddleCenter, true);
			if (Peer.ClientGame.Placement.IsWaitingOrDeplacing && Peer.ClientGame.Placement.ElapsedTime != 0)
			{
				this.gui.TextLabel(new Rect(vector2.x - 102f, vector2.y - 12f, 200f, 50f), Peer.ClientGame.Placement.ElapsedTime, 16, "#FFFFFF", TextAnchor.MiddleCenter, true);
			}
		}
		this.gui.RotateGUI(0f, Vector2.zero);
		this.gui.BeginGroup(new Rect(18f, 12f, (float)this.level.width, (float)this.level.height));
		this.gui.color = new Color(0.8f, 0.8f, 0.2f, Mathf.Min(this.tickAlpha.Evaluate(Utility.Period(this.tickAlphaKeys[this.tickAlphaKeys.Length - 1].value, 1f)) * Main.UserInfo.settings.radarAlpha, 0.5f));
		this.gui.PictureCentered(vector2 - new Vector2(18f, 12f), this.radarTick, Vector2.one * this.scale * this.tickScale.Evaluate(Utility.Period(this.tickScaleKeys[this.tickScaleKeys.Length - 1].value, 0.5f)));
		this.gui.EndGroup();
	}

	// Token: 0x06000A5C RID: 2652 RVA: 0x00079D44 File Offset: 0x00077F44
	private void showPlacmentInScreenSpace(Vector3 pos)
	{
		Vector2 vector = this.gui.WorldToScreen(pos);
		this.gui.PictureScreenSpace(vector + new Vector2(18f, 12f), this.placement, Vector2.one * this.scale);
		vector += new Vector2(18f, 12f);
		GUIUtility.ScaleAroundPivot(Vector2.one * this.scale, vector);
		this.gui.TextLabel(new Rect(vector.x - 100f - 2f, vector.y - 25f - 13f, 200f, 50f), EnumNames.PlacementTypeName(Peer.ClientGame.Placement.PointType), 15, "#FFFFFF_Micra", TextAnchor.MiddleCenter, true);
		if (Peer.ClientGame.Placement.IsWaitingOrDeplacing && Peer.ClientGame.Placement.ElapsedTime != 0)
		{
			this.gui.TextFieldint(new Rect(vector.x - 100f - 2f, vector.y - 25f + 13f, 200f, 50f), Peer.ClientGame.Placement.ElapsedTime, 16, "#FFFFFF", TextAnchor.MiddleCenter, false, false);
		}
		this.gui.RotateGUI(0f, Vector2.zero);
		this.gui.BeginGroup(new Rect(18f, 12f, (float)this.level.width, (float)this.level.height));
		this.gui.color = new Color(0.8f, 0.8f, 0.2f, Mathf.Min(this.tickAlpha.Evaluate(Utility.Period(this.tickAlphaKeys[this.tickAlphaKeys.Length - 1].value, 1f)) * Main.UserInfo.settings.radarAlpha, 0.5f));
		this.gui.PictureCentered(vector - new Vector2(18f, 12f), this.radarTick, Vector2.one * this.scale * this.tickScale.Evaluate(Utility.Period(this.tickScaleKeys[this.tickScaleKeys.Length - 1].value, 0.5f)));
		this.gui.EndGroup();
	}

	// Token: 0x06000A5D RID: 2653 RVA: 0x00079FC4 File Offset: 0x000781C4
	private void GrenadeMark(Vector3 pos)
	{
		if ((pos - Peer.ClientGame.LocalPlayer.Position).magnitude > 5f)
		{
			this.gui.color = Colors.alpha(Color.white, 1f - Mathf.Abs(Mathf.Min((pos - Peer.ClientGame.LocalPlayer.Position).magnitude - 5f, 10f) / 10f));
		}
		else
		{
			this.gui.color = Color.white;
		}
		this.gui.PictureScreenSpace(this.gui.WorldToScreen(pos), this.grenade, Vector2.one);
	}

	// Token: 0x06000A5E RID: 2654 RVA: 0x0007A084 File Offset: 0x00078284
	private void TacticalPoint(Vector3 pos, Texture2D t, string str)
	{
		if (pos.x > this.LowerRight.x || pos.z < this.LowerRight.z || pos.x < this.UpperLeft.x || pos.z > this.UpperLeft.z)
		{
			return;
		}
		Vector3 vector = this.UpperLeft - this.LowerRight;
		vector.x = Mathf.Abs(vector.x);
		vector.y = Mathf.Abs(vector.y);
		vector.z = Mathf.Abs(vector.z);
		Vector2 a = new Vector2(Mathf.Abs(this.UpperLeft.x - pos.x) / vector.x * (float)this.level.width, Mathf.Abs(this.UpperLeft.z - pos.z) / vector.z * (float)this.level.height);
		this.gui.PictureScreenSpace(a + new Vector2(18f, 12f), t, Vector2.one * this.scale);
		this.gui.TextLabel(new Rect(a.x + 6f, a.y - 8f, 20f, 20f), str, 12, "#ffffff", TextAnchor.MiddleCenter, true);
	}

	// Token: 0x06000A5F RID: 2655 RVA: 0x0007A20C File Offset: 0x0007840C
	public void DrawEntities()
	{
		try
		{
			this.canShowSupport = false;
			for (int i = 0; i < Peer.ClientGame.AllEntities.Count; i++)
			{
				if (!EntityNetPlayer.IsClientPlayer(Peer.ClientGame.AllEntities[i].ID))
				{
					if (Peer.ClientGame.AllEntities[i].state.type == EntityType.grenade && Peer.ClientGame.LocalPlayer.SkillUnlocked(Skills.frag_id))
					{
						this.GrenadeMark(Peer.ClientGame.AllEntities[i].transform.position);
					}
					if (Main.IsTeamGame)
					{
						if (Peer.ClientGame.Placement && (Peer.ClientGame.Placement.state == PlacementState.waiting_bomber || Peer.ClientGame.Placement.state == PlacementState.deplacing))
						{
							this.gui.color = new Color(1f, 1f, 1f, Mathf.Min(Main.UserInfo.settings.radarAlpha, 1f));
							if (Peer.HardcoreMode)
							{
								this.gui.TextLabel(new Rect(3f, 1f, 300f, 50f), Language.RadarBeaconSet, 14, "#000000", TextAnchor.UpperLeft, true);
								this.gui.TextLabel(new Rect(2f, 0f, 300f, 50f), Language.RadarBeaconSet, 14, "#FFFFFF", TextAnchor.UpperLeft, true);
							}
							else
							{
								this.gui.TextLabel(new Rect(3f, 165f, 300f, 50f), Language.RadarBeaconSet, 14, "#000000", TextAnchor.MiddleCenter, true);
								this.gui.TextLabel(new Rect(2f, 164f, 300f, 50f), Language.RadarBeaconSet, 14, "#FFFFFF", TextAnchor.MiddleCenter, true);
							}
							if (!Peer.HardcoreMode && Peer.ClientGame.LocalPlayer.SkillUnlocked(Skills.des3))
							{
								this.showPlacmentInScreenSpace(Peer.ClientGame.AllEntities[i].transform.position);
							}
						}
						bool flag = (Peer.ClientGame.AllEntities[i].state.type == EntityType.mortar || Peer.ClientGame.AllEntities[i].state.type == EntityType.sonar) && Peer.ClientGame.LocalPlayer.SkillUnlocked(Skills.scan);
						if ((Peer.ClientGame.AllEntities[i].state.isBear ^ Peer.ClientGame.LocalPlayer.IsBear) && !flag)
						{
							goto IL_4C4;
						}
						if (!Peer.HardcoreMode && Peer.ClientGame.AllEntities[i].state.marker && Peer.ClientGame.AllEntities[i].state.type == EntityType.placement && (Peer.ClientGame.AllEntities[i].state.isBear ^ !Main.GameModeInfo.isBearPlacement))
						{
							this.Placement(Peer.ClientGame.AllEntities[i].transform.position);
						}
					}
					else if (Peer.ClientGame.AllEntities[i].state.playerID != Peer.ClientGame.LocalPlayer.ID && !Peer.ClientGame.LocalPlayer.SkillUnlocked(Skills.scan))
					{
						goto IL_4C4;
					}
					if (Peer.HardcoreMode && (Peer.ClientGame.AllEntities[i].state.type == EntityType.sonar || Peer.ClientGame.AllEntities[i].state.type == EntityType.mortar))
					{
						this.canShowSupport = true;
					}
					if (Peer.ClientGame.AllEntities[i].state.marker && Peer.ClientGame.AllEntities[i].state.type == EntityType.mortar)
					{
						this.MortarStrike(Peer.ClientGame.AllEntities[i].transform.position);
					}
					if (Peer.ClientGame.AllEntities[i].state.marker && Peer.ClientGame.AllEntities[i].state.type == EntityType.sonar)
					{
						this.Sonar(Peer.ClientGame.AllEntities[i].transform.position, (float)Peer.ClientGame.AllEntities[i].state.sonarTime);
					}
				}
				IL_4C4:;
			}
		}
		catch (Exception)
		{
			Debug.Log("DrawEntities jitted");
		}
	}

	// Token: 0x06000A60 RID: 2656 RVA: 0x0007A728 File Offset: 0x00078928
	private void DrawRadioScreenSpaceInfo(Vector3 screen, bool friendly)
	{
		if (this.SkipIfOptic())
		{
			return;
		}
		this.gui.color = new Color(1f, 1f, 1f, Mathf.Min(this.tickAlpha.Evaluate(Utility.Period(this.tickScaleKeys[this.tickScaleKeys.Length - 1].value, 2f)) * Main.UserInfo.settings.radarAlpha, 1f));
		if (friendly && !Peer.HardcoreMode)
		{
			this.gui.Picture(new Vector2(screen.x - 50f, (float)Screen.height - screen.y - 20f), this.radioIcon[1]);
		}
		if (friendly && Peer.HardcoreMode)
		{
			this.gui.Picture(new Vector2(screen.x, (float)Screen.height - screen.y - 20f), this.radioIcon[1]);
		}
	}

	// Token: 0x06000A61 RID: 2657 RVA: 0x0007A838 File Offset: 0x00078A38
	public void DrawVIPScreenspaceInfo(Vector3 screen, bool friendly)
	{
		try
		{
			if (!(screen == Vector3.zero))
			{
				if (!this.SkipIfOptic())
				{
					Vector2 a = new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2));
					Vector2 b = new Vector2(screen.x, screen.y);
					float num = (a - b).magnitude / 300f;
					if ((double)num > 0.75)
					{
						num = 0.75f;
					}
					this.gui.color = Colors.alpha(this.gui.color, this.gui.color.a * num);
					if (friendly)
					{
						this.gui.Picture(new Vector2(screen.x - 12f, (float)Screen.height - screen.y + 5f), this.vipIcons[0]);
					}
					else if (Peer.Info.TestVip)
					{
						Color color = this.gui.color;
						this.gui.color = new Color(1f, 1f, 1f, this.VipIconCurve.Evaluate(Time.time) * Main.UserInfo.settings.radarAlpha);
						this.gui.Picture(new Vector2(screen.x - 15f, (float)Screen.height - screen.y - 5f), this.vipIcons[1]);
						this.gui.color = color;
					}
					else
					{
						this.gui.Picture(new Vector2(screen.x - 15f, (float)Screen.height - screen.y - 5f), this.vipIcons[1]);
					}
					this.gui.color = Colors.alpha(this.gui.color, Main.UserInfo.settings.radarAlpha);
				}
			}
		}
		catch (Exception)
		{
			Debug.Log("DrawVIPScreenspaceInfo jitted");
		}
	}

	// Token: 0x06000A62 RID: 2658 RVA: 0x0007AA74 File Offset: 0x00078C74
	public void DrawFriendEntityScreenspaceInfo(EntityNetPlayer player, Vector3 screen, bool friendly, float alpha = 1f)
	{
		if (player == null)
		{
			return;
		}
		if (!friendly)
		{
			Debug.LogError("he is not a friend!");
			return;
		}
		if (this.SkipIfOptic() || Peer.HardcoreMode)
		{
			return;
		}
		alpha *= Main.UserInfo.settings.radarAlpha;
		if (!Main.IsTeamGame)
		{
			return;
		}
		if (alpha > 0.45f && alpha <= 1f)
		{
			GUI.color = new Color(1f, 1f, 1f, alpha * 0.5f);
			this.EntityInfoLabel.alignment = TextAnchor.MiddleRight;
			if (player.ClanTag.Length > 1 && player.WidthTag < 0.1f)
			{
				this.EntityInfoLabel.CalcMinMaxWidth(new GUIContent(Helpers.GetTag(player.ClanTag)), out player.WidthTag, out this.tmpval);
			}
			if (player.WidthTag > 0.1f)
			{
				GUI.Label(new Rect(screen.x - player.WidthTag, (float)Screen.height - screen.y, player.WidthTag, 14f), Helpers.ColoredTag(player.ClanTag), this.EntityInfoLabel);
			}
			this.EntityInfoLabel.alignment = TextAnchor.MiddleLeft;
			if (player.Nick.Length > 1 && player.WidthNick < 0.1f)
			{
				this.EntityInfoLabel.CalcMinMaxWidth(new GUIContent(player.Nick), out player.WidthNick, out this.tmpval);
			}
			GUI.Label(new Rect(screen.x, (float)Screen.height - screen.y, player.WidthNick, 14f), string.Concat(new object[]
			{
				player.Nick,
				" [",
				player.Level,
				"]"
			}), this.EntityInfoLabel);
			GUI.DrawTexture(new Rect(screen.x - player.WidthTag - (float)this.friendlyClassIcons[(int)player.PlayerClass].width, (float)Screen.height - screen.y - 10f, 31f, 32f), this.friendlyClassIcons[(int)player.PlayerClass]);
			if (Main.UserInfo.skillUnlocked(Skills.analyze1))
			{
				GUI.Label(new Rect(screen.x + 15f, (float)Screen.height - screen.y - 15f, 200f, 14f), player.Health.ToString("F0"), this.EntityInfoLabel);
				GUI.DrawTexture(new Rect(screen.x, (float)Screen.height - screen.y - 16f, 14f, 13f), this.smallHPIcon);
			}
		}
		if (alpha < 0.5f)
		{
			GUI.color = new Color(1f, 1f, 1f, (1f - alpha) * 0.5f);
			GUI.DrawTexture(new Rect(screen.x - (float)this.team.width * 0.5f, (float)Screen.height - screen.y - 21f, (float)this.team.width, (float)this.team.height), this.team);
		}
		GUI.color = new Color(1f, 1f, 1f, 1f * Main.UserInfo.settings.radarAlpha);
	}

	// Token: 0x06000A63 RID: 2659 RVA: 0x0007AE08 File Offset: 0x00079008
	public void DrawEnemyEntityScreenspaceInfo(EntityNetPlayer player, Vector3 screen, bool friendly, float alpha = 1f)
	{
		try
		{
			if (!friendly)
			{
				if (!this.SkipIfOptic() && !Peer.HardcoreMode)
				{
					alpha *= Main.UserInfo.settings.radarAlpha;
					if (!Peer.ClientGame.LocalPlayer.IsTeam(player))
					{
						if (player.showHealth.Visible)
						{
							GUI.color = new Color(1f, 0f, 0f, player.showHealth.visibility);
							GUI.Label(new Rect(screen.x + 10f, (float)Screen.height - screen.y - 20f, 50f, 14f), player.Health.ToString("F0"), this.EntityInfoLabel);
							GUI.DrawTexture(new Rect(screen.x - (float)this.smallHPIcon.height * 0.5f, (float)Screen.height - screen.y - 20f, (float)this.smallHPIcon.width, (float)this.smallHPIcon.height), this.smallHPIcon);
						}
						if (player.showArmor.Visible)
						{
							GUI.color = new Color(1f, 0f, 0f, alpha * 0.5f * player.showArmor.visibility);
							GUI.DrawTexture(new Rect(screen.x - 10f, (float)Screen.height - screen.y - 23f, (float)this.armorIcon.width, (float)this.armorIcon.height), this.armorIcon);
						}
						GUI.color = new Color(1f, 1f, 1f, alpha * 0.5f);
						if (this.IsPlayerMarked(player))
						{
							this.DrawHostileIcons(player, screen);
						}
						GUI.color = new Color(1f, 1f, 1f, 1f * Main.UserInfo.settings.radarAlpha);
					}
				}
			}
		}
		catch (Exception)
		{
			Debug.Log("DrawEnemyEntityScreenspaceInfo jitted");
		}
	}

	// Token: 0x06000A64 RID: 2660 RVA: 0x0007B05C File Offset: 0x0007925C
	public void DrawHostileIcons(EntityNetPlayer playerEntity, Vector3 screen)
	{
		try
		{
			GUI.color = new Color(1f, 1f, 1f, 1f * Main.UserInfo.settings.radarAlpha);
			if (Peer.ClientGame.LocalPlayer.SkillUnlocked(Skills.analyze3) && playerEntity.PlayerClass != PlayerClass.none)
			{
				GUI.DrawTexture(new Rect(screen.x - (float)this.hostileClassIcons[(int)playerEntity.PlayerClass].width * 0.5f, (float)Screen.height - screen.y - 15f, (float)this.hostileClassIcons[0].width, (float)this.hostileClassIcons[0].height), this.hostileClassIcons[(int)playerEntity.PlayerClass]);
			}
			else
			{
				GUI.DrawTexture(new Rect(screen.x - (float)this.hostileIcon.width * 0.5f, (float)Screen.height - screen.y - 10f, (float)this.hostileIcon.width, (float)this.hostileIcon.height), this.hostileIcon);
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000A65 RID: 2661 RVA: 0x0007B1A4 File Offset: 0x000793A4
	public bool IsPlayerMarked(EntityNetPlayer player)
	{
		bool result;
		try
		{
			float num = Vector3.Distance(player.PlayerTransform.position, Main.LocalPlayer.Entity.PlayerTransform.position);
			bool flag = Peer.HardcoreMode && Main.LocalPlayer.IsVip;
			bool flag2 = Peer.ClientGame.LocalPlayer.SkillUnlocked(Skills.att5) && (Peer.ClientGame.LocalPlayer.SkillUnlocked(Skills.hear3) || !player.Ammo.CurrentWeapon.SS);
			bool flag3 = flag || flag2;
			bool visible = player.showHealth.Visible;
			bool visible2 = player.showArmor.Visible;
			bool flag4 = BIT.AND((int)player.playerInfo.buffs, 4096);
			bool flag5 = ((player.lastFireTime > Time.time && player.Ammo.CurrentWeapon.HearRadius > num) || player.lastSonarTime > Time.time) && flag3;
			result = (visible || visible2 || flag4 || flag5);
		}
		catch (Exception)
		{
			result = false;
		}
		return result;
	}

	// Token: 0x06000A66 RID: 2662 RVA: 0x0007B308 File Offset: 0x00079508
	private void DrawHitCircle(Vector3 pos, int weapon, bool mod)
	{
		Vector2 a = new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)) + new Vector2(pos.x - Peer.ClientGame.LocalPlayer.Position.x, pos.z - Peer.ClientGame.LocalPlayer.Position.z) * 100f;
		Vector2 normalized = (a - new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2))).normalized;
		Vector3 vector = Quaternion.Euler(0f, Peer.ClientGame.LocalPlayer.Euler.y, 0f) * new Vector3(normalized.x, 0f, -normalized.y);
		normalized.x = vector.x;
		normalized.y = vector.z;
		float angle = CVars.test + 45f - Vector2.Angle(normalized, new Vector2(1f, 0f));
		if (normalized.y > 0f)
		{
			angle = CVars.test + 45f + Vector2.Angle(normalized, new Vector2(1f, 0f));
		}
		this.gui.RotateGUI(angle, new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)));
		this.gui.Picture(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2 - this.hit_circle.height)), this.hit_circle, Vector2.one);
		this.gui.RotateGUI(0f, Vector2.zero);
		if (weapon != 127)
		{
			if (mod)
			{
				this.gui.PictureScreenSpace(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)) + normalized * 120f, this.killsGUI.killMethods[weapon], Vector2.one);
			}
			else
			{
				this.gui.PictureScreenSpace(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2)) + normalized * 120f, this.killsGUI.killMethods[weapon], Vector2.one);
			}
		}
	}

	// Token: 0x06000A67 RID: 2663 RVA: 0x0007B560 File Offset: 0x00079760
	private bool CanShowSupportInHardcore()
	{
		return true;
	}

	// Token: 0x06000A68 RID: 2664 RVA: 0x0007B580 File Offset: 0x00079780
	private bool SkipIfOptic()
	{
		return !(Peer.ClientGame.LocalPlayer.Ammo.CurrentWeapon == null) && Peer.ClientGame.LocalPlayer.Ammo.IsAim && Peer.ClientGame.LocalPlayer.Ammo.CurrentWeapon.Optic;
	}

	// Token: 0x06000A69 RID: 2665 RVA: 0x0007B5E4 File Offset: 0x000797E4
	private void Blink(TacticalPointState s, ref TacticalPointState lastState, ref float time)
	{
		if (s != lastState && (s == TacticalPointState.bear_captured || s == TacticalPointState.usec_captured))
		{
			time = Time.time + 4f;
		}
		if (time > Time.time)
		{
			float num = Mathf.Abs(Mathf.Sin(Time.time * 4f));
			this.gui.color = Colors.alpha(this.gui.color, this.gui.color.a * num);
		}
		lastState = s;
	}

	// Token: 0x06000A6A RID: 2666 RVA: 0x0007B66C File Offset: 0x0007986C
	private void PointToScreenSpace(Vector3 pos, Texture2D t, TacticalPointState s, bool rotate = false)
	{
		if (pos.z <= 0f)
		{
			return;
		}
		float angle = 180f * Time.realtimeSinceStartup * 1.5f;
		Vector2 a = new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2));
		Vector2 b = new Vector2(pos.x, pos.y);
		float num = (a - b).magnitude / 300f;
		if (pos.y > (float)Screen.height)
		{
			pos.y = (float)Screen.height;
		}
		if (pos.y < 0f)
		{
			pos.y = 0f;
		}
		if (pos.x > (float)Screen.width)
		{
			pos.x = (float)Screen.width;
		}
		if (pos.x < 0f)
		{
			pos.x = 0f;
		}
		if (num > 1f)
		{
			num = 1f;
		}
		this.gui.color = Colors.alpha(this.gui.color, this.gui.color.a * num);
		if (rotate)
		{
			this.gui.RotateGUI(angle, new Vector3(pos.x, (float)Screen.height - pos.y, pos.z));
		}
		this.gui.Picture(new Vector2(pos.x - (float)(t.width / 2), (float)Screen.height - pos.y - (float)(t.height / 2)), t);
		if (rotate)
		{
			this.gui.RotateGUI(0f, Vector2.zero);
		}
	}

	// Token: 0x06000A6B RID: 2667 RVA: 0x0007B82C File Offset: 0x00079A2C
	private void TextToScreenSpace(Vector3 pos, string str, float a = 1f)
	{
		if (pos.z <= 0f)
		{
			return;
		}
		int num = 50;
		if (pos.y > (float)Screen.height)
		{
			pos.y = (float)Screen.height;
		}
		if (pos.y < 0f)
		{
			pos.y = 0f;
		}
		if (pos.x > (float)Screen.width)
		{
			pos.x = (float)Screen.width;
		}
		if (pos.x < 0f)
		{
			pos.x = 0f;
		}
		if (a < 1f)
		{
			this.gui.color = Colors.alpha(this.gui.color, a);
		}
		this.gui.TextLabel(new Rect(pos.x - (float)(num / 2) - 2f, (float)Screen.height - pos.y - (float)(num / 2), (float)num, (float)num), str, 16, "#ffffff", TextAnchor.MiddleCenter, true);
		if (a < 1f)
		{
			this.gui.color = Colors.alpha(this.gui.color, base.visibility);
		}
	}

	// Token: 0x06000A6C RID: 2668 RVA: 0x0007B95C File Offset: 0x00079B5C
	public override void MainInitialize()
	{
		base.MainInitialize();
		this.tabGUI = this.gui.GetComponent<TabGUI>();
		this.killsGUI = this.gui.GetComponent<KillsGUI>();
		RadarGUI.I = this;
	}

	// Token: 0x06000A6D RID: 2669 RVA: 0x0007B998 File Offset: 0x00079B98
	public override void OnConnected()
	{
		this.Clear();
		if (Main.IsTacticalConquest)
		{
			this.tacticalPoints = (TacticalPoint[])UnityEngine.Object.FindSceneObjectsOfType(typeof(TacticalPoint));
		}
		this.level = SingletoneForm<LevelSettings>.Instance.radar;
		this.scale = SingletoneForm<LevelSettings>.Instance.scale;
		this.UpperLeft = ((UpperLeftPoint)UnityEngine.Object.FindObjectOfType(typeof(UpperLeftPoint))).transform.position;
		this.LowerRight = ((LowerRightPoint)UnityEngine.Object.FindObjectOfType(typeof(LowerRightPoint))).transform.position;
		this.isGameHandler = true;
		this.isUpdating = true;
		this.isRendering = true;
		this.showPlayerClassIcon = Main.UserInfo.skillsInfos[3].Unlocked;
		this.allplayers = Peer.ClientGame.AllPlayers;
		this.entites = Peer.ClientGame.AllEntities;
		this._mapSize = this.UpperLeft - this.LowerRight;
	}

	// Token: 0x06000A6E RID: 2670 RVA: 0x0007BA9C File Offset: 0x00079C9C
	public override void OnDisconnect()
	{
		this.Clear();
		this.showPlayerClassIcon = false;
		this.isGameHandler = false;
		this.isUpdating = false;
		this.isRendering = false;
	}

	// Token: 0x06000A6F RID: 2671 RVA: 0x0007BACC File Offset: 0x00079CCC
	public override void OnEnable()
	{
		this.tickScaleKeys = this.tickScale.keys;
		this.tickAlphaKeys = this.tickAlpha.keys;
		base.OnEnable();
	}

	// Token: 0x06000A70 RID: 2672 RVA: 0x0007BB04 File Offset: 0x00079D04
	public override void Clear()
	{
		base.Clear();
		this.enemies.Clear();
		this.hits.Clear();
		this.hotspots.Clear();
		this.level = null;
		this.scale = 1f;
		this.UpperLeft = Vector3.zero;
		this.LowerRight = Vector3.zero;
	}

	// Token: 0x06000A71 RID: 2673 RVA: 0x0007BB60 File Offset: 0x00079D60
	public override void Register()
	{
		EventFactory.Register("AddEnemy", this);
		EventFactory.Register("AddHit", this);
		EventFactory.Register("AddHotspot", this);
		EventFactory.Register("Beep", this);
	}

	// Token: 0x06000A72 RID: 2674 RVA: 0x0007BB9C File Offset: 0x00079D9C
	public override void GameGUI()
	{
		if (Event.current.type != EventType.Repaint)
		{
			return;
		}
		if (!Main.IsAlive)
		{
			return;
		}
		if (Main.UserInfo.settings.graphics.HideInterface)
		{
			return;
		}
		this.gui.color = new Color(1f, 1f, 1f, Mathf.Min(Main.UserInfo.settings.radarAlpha, 1f));
		if (this.CanShowSupportInHardcore())
		{
			GUI.DrawTexture(new Rect(0f, 0f, (float)this.background.width, (float)this.background.height), this.background);
			GUI.DrawTexture(new Rect(this.mapPos.x, this.mapPos.y, (float)this.level.width, (float)this.level.height), this.level);
			if (!Peer.HardcoreMode)
			{
				if (!Main.IsTeamGame)
				{
					this.gui.TextLabel(new Rect(35f, 200f, 100f, 100f), "#", 13, "#b10909_Micra", TextAnchor.UpperLeft, true);
					this.gui.TextLabel(new Rect(-14f, 213f, 100f, 100f), this.tabGUI.cachedMatchPlace, 12, "#FFFFFF_Micra", TextAnchor.UpperCenter, true);
					this.gui.TextLabel(new Rect(71f, 200f, 100f, 100f), "EXP", 10, "#62aeea_Micra", TextAnchor.UpperLeft, true);
					this.gui.TextLabel(new Rect(37f, 213f, 100f, 100f), this.tabGUI.myExp, 12, "#FFFFFF_Micra", TextAnchor.UpperCenter, true);
					this.gui.TextLabel(new Rect(90f, 200f, 100f, 100f), this.gui.SecondsToStringMS((int)Peer.ClientGame.ElapsedNextEventTime), 10, "#c9c9c9_Micra", TextAnchor.UpperRight, true);
					string content = (!ClientLeagueSystem.IsLeagueGame) ? Main.GameModeInfo.matchNeededPoints.ToString() : Main.GameModeInfo.LeagueMatchNeededPoints.ToString();
					this.gui.TextLabel(new Rect(90f, 215f, 100f, 100f), content, 9, "#FFFFFF_Micra", TextAnchor.UpperRight, true);
				}
				else
				{
					if (Peer.ClientGame.LocalPlayer.IsBear)
					{
						this.gui.TextLabel(new Rect(21f, 203f, 100f, 100f), "BEAR", 10, Colors.RadarBlueWeb + "_Micra", TextAnchor.UpperLeft, true);
						this.gui.TextLabel(new Rect(-5f, 214f, 100f, 100f), Peer.ClientGame.BearWinCount, 12, "#FFFFFF_Micra", TextAnchor.UpperCenter, true);
						this.gui.TextLabel(new Rect(83f, 203f, 100f, 100f), "USEC", 10, "#ad0505_Micra", TextAnchor.UpperLeft, true);
						this.gui.TextLabel(new Rect(57f, 214f, 100f, 100f), Peer.ClientGame.UsecWinCount, 12, "#FFFFFF_Micra", TextAnchor.UpperCenter, true);
					}
					else
					{
						this.gui.TextLabel(new Rect(21f, 203f, 100f, 100f), "USEC", 10, Colors.RadarBlueWeb + "_Micra", TextAnchor.UpperLeft, true);
						this.gui.TextLabel(new Rect(-5f, 214f, 100f, 100f), Peer.ClientGame.UsecWinCount, 12, "#FFFFFF_Micra", TextAnchor.UpperCenter, true);
						this.gui.TextLabel(new Rect(83f, 203f, 100f, 100f), "BEAR", 10, "#ad0505_Micra", TextAnchor.UpperLeft, true);
						this.gui.TextLabel(new Rect(57f, 214f, 100f, 100f), Peer.ClientGame.BearWinCount, 12, "#FFFFFF_Micra", TextAnchor.UpperCenter, true);
					}
					this.gui.TextLabel(new Rect(98f, 203f, 100f, 100f), this.gui.SecondsToStringMS((int)Peer.ClientGame.ElapsedNextEventTime), 10, "#c9c9c9_Micra", TextAnchor.UpperRight, true);
					if (Main.IsTargetDesignation)
					{
						this.gui.TextLabel(new Rect(85f, 214f, 100f, 100f), Main.GameModeInfo.matchNeededKills, 12, "#FFFFFF_Micra", TextAnchor.UpperRight, true);
					}
					if (Main.IsTeamElimination)
					{
						string content2 = (!ClientLeagueSystem.IsLeagueGame) ? Main.GameModeInfo.matchNeededTeamKills.ToString() : Main.GameModeInfo.LeagueMatchNeededTeamKills.ToString();
						this.gui.TextLabel(new Rect(85f, 214f, 100f, 100f), content2, 12, "#FFFFFF_Micra", TextAnchor.UpperRight, true);
					}
					if (Main.IsTacticalConquest)
					{
						string content3 = (!ClientLeagueSystem.IsLeagueGame) ? Main.GameModeInfo.matchNeededPoints.ToString() : Main.GameModeInfo.LeagueMatchNeededPoints.ToString();
						this.gui.TextLabel(new Rect(93f, 214f, 100f, 100f), content3, 12, "#FFFFFF_Micra", TextAnchor.UpperRight, true);
					}
				}
			}
		}
		if (Main.IsTacticalConquest && this.tacticalPoints != null)
		{
			int i = 0;
			while (i < this.tacticalPoints.Length)
			{
				int num = 2;
				Vector3 vector = Peer.ClientGame.LocalPlayer.MainCamera.WorldToScreenPoint(this.tacticalPoints[i].Pos + new Vector3(0f, 1.35f, 0f));
				TacticalPointState pointState = this.tacticalPoints[i].pointState;
				if (pointState == TacticalPointState.neutral)
				{
					num = 0;
				}
				if (pointState == TacticalPointState.bear_captured && Peer.ClientGame.LocalPlayer.IsBear)
				{
					num = 1;
				}
				if (pointState == TacticalPointState.usec_captured && !Peer.ClientGame.LocalPlayer.IsBear)
				{
					num = 1;
				}
				if (pointState != TacticalPointState.usec_homeBase)
				{
					goto IL_696;
				}
				if (!Peer.ClientGame.LocalPlayer.IsBear)
				{
					num = 3;
					goto IL_696;
				}
				IL_8EC:
				i++;
				continue;
				IL_696:
				if (pointState == TacticalPointState.bear_homeBase)
				{
					if (!Peer.ClientGame.LocalPlayer.IsBear)
					{
						goto IL_8EC;
					}
					num = 3;
				}
				if (!this.tacticalPoints[i].IsHomebase(Peer.ClientGame.LocalPlayer))
				{
					this.Blink(pointState, ref this.tacticalPoints[i].lastState, ref this.tacticalPoints[i].blinkTimer);
					this.PointToScreenSpace(vector, this.tcPointIconsScreen[num], pointState, this.tacticalPoints[i].InAction);
					this.TextToScreenSpace(vector, this.tacticalPoints[i].NumberOfPoint.ToString(), 1f);
					this.gui.color = Colors.alpha(this.gui.color, base.visibility);
				}
				this.gui.color = new Color(1f, 1f, 1f, Mathf.Min(Main.UserInfo.settings.radarAlpha, 1f));
				if (!Peer.HardcoreMode && !this.tacticalPoints[i].IsBasePoint)
				{
					this.TacticalPoint(this.tacticalPoints[i].Pos, this.tcPointIcons[num], this.tacticalPoints[i].NumberOfPoint.ToString());
				}
				if (this.tacticalPoints[i].isEnemy(Peer.ClientGame.LocalPlayer) != 1)
				{
					for (int j = 0; j < this.tacticalPoints[i].playersNeeded; j++)
					{
						int num2 = 0;
						if (Peer.ClientGame.LocalPlayer.IsBear)
						{
							num2 = this.tacticalPoints[i].bearIn;
						}
						if (!Peer.ClientGame.LocalPlayer.IsBear)
						{
							num2 = this.tacticalPoints[i].usecIn;
						}
						this.PointToScreenSpace(vector + new Vector3((float)(-(float)(this.tacticalPoints[i].playersNeeded - 1) * this.tcSlotIcons[0].width / 2 + this.tcSlotIcons[0].width * j), (float)(this.tcPointIconsScreen[num].height / 3), 0f), (j >= num2) ? this.tcSlotIcons[0] : this.tcSlotIcons[1], pointState, false);
					}
					goto IL_8EC;
				}
				goto IL_8EC;
			}
		}
		PlayerType playerType = Peer.ClientGame.LocalPlayer.playerInfo.playerType;
		EntityNetPlayer entity = Peer.ClientGame.LocalPlayer.Entity;
		float time = Time.time;
		ClientEntity clientEntity = null;
		if (playerType != PlayerType.spectactor)
		{
			if (this.entites.Count > this.entitiesIndex)
			{
				clientEntity = this.entites[this.entitiesIndex];
			}
			for (int k = 0; k < this.allplayers.Count; k++)
			{
				EntityNetPlayer entityNetPlayer = this.allplayers[k];
				if (!EntityNetPlayer.IsClientPlayer(entityNetPlayer.ID))
				{
					if (entityNetPlayer.playerInfo.playerType != PlayerType.spectactor && entityNetPlayer.IsAlive && entityNetPlayer != entity && entityNetPlayer.PlayerTransform != null)
					{
						if (this.CanShowSupportInHardcore() || entityNetPlayer.IsVip)
						{
							if (Main.IsTeamGame)
							{
								Vector3 screen = Peer.ClientGame.LocalPlayer.MainCamera.WorldToScreenPoint(entityNetPlayer.NeckPosition + new Vector3(0f, 0.35f, 0f));
								this.lastPlayerDisctance = Mathf.Abs(Vector3.Distance(entity.Position, entityNetPlayer.PlayerTransform.position));
								if (screen.z > 0f)
								{
									if (entityNetPlayer.IsVip)
									{
										this.DrawVIPScreenspaceInfo(screen, entityNetPlayer.playerInfo.playerType == playerType);
									}
									if (this.lastPlayerDisctance > 20f)
									{
										this.changedAlphaPlayerDistance = true;
										this.lastColorPlayerDistance = this.gui.color;
										if (this.lastPlayerDisctance < 50f)
										{
											this.alphaPlayerDistance = 1f / (this.lastPlayerDisctance - 20f) * 3f;
										}
										else
										{
											this.alphaPlayerDistance = 0f;
										}
										if (this.alphaPlayerDistance < 0f)
										{
											this.alphaPlayerDistance = 0f;
										}
										if (this.alphaPlayerDistance > 1f)
										{
											this.alphaPlayerDistance = 1f;
										}
									}
									else
									{
										this.alphaPlayerDistance = 1f;
									}
									if (entityNetPlayer.playerInfo.playerType == playerType)
									{
										this.DrawFriendEntityScreenspaceInfo(entityNetPlayer, screen, entityNetPlayer.playerInfo.playerType == playerType, this.alphaPlayerDistance.Value);
									}
									else
									{
										this.DrawEnemyEntityScreenspaceInfo(entityNetPlayer, screen, entityNetPlayer.playerInfo.playerType == playerType, this.alphaPlayerDistance.Value);
									}
								}
								if (entityNetPlayer.playerInfo.playerType == playerType)
								{
									GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, Main.UserInfo.settings.radarAlpha);
									if (this.CanShowSupportInHardcore())
									{
										if (entityNetPlayer.IsVip)
										{
											this.DrawPoint(entityNetPlayer.PlayerTransform.position, this.vipIcons[0]);
										}
										else
										{
											this.DrawRotatePoint(entityNetPlayer.PlayerTransform.position, entityNetPlayer.Euler.y, this.team);
										}
									}
								}
								else
								{
									GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, Main.UserInfo.settings.radarAlpha);
									if (entityNetPlayer.Ammo != null && ((entityNetPlayer.lastFireTime > time && Vector3.Distance(entityNetPlayer.Position, entity.Position) <= entityNetPlayer.Ammo.CurrentWeapon.HearRadius) || entityNetPlayer.lastSonarTime > time || BIT.AND((int)entityNetPlayer.playerInfo.buffs, 4096)))
									{
										if (Peer.ClientGame.LocalPlayer.SkillUnlocked(Skills.hear3) || !entityNetPlayer.Ammo.CurrentWeapon.SS)
										{
											if (this.showPlayerClassIcon && entityNetPlayer.playerInfo.playerClass != PlayerClass.none)
											{
												this.DrawPoint(entityNetPlayer.PlayerTransform.position, this.hostileClassIcons[(int)entityNetPlayer.playerInfo.playerClass]);
											}
											else
											{
												this.DrawPoint(entityNetPlayer.PlayerTransform.position, this.enemy);
											}
										}
									}
									else
									{
										GUI.color = new Color(GUI.color.r, GUI.color.b, GUI.color.g, Main.UserInfo.settings.radarAlpha);
										if (entityNetPlayer.IsVip && this.CanShowSupportInHardcore())
										{
											if (Peer.Info.TestVip)
											{
												Color color = this.gui.color;
												this.gui.color = new Color(1f, 1f, 1f, this.VipIconCurve.Evaluate(Time.time) * Main.UserInfo.settings.radarAlpha);
												this.DrawPoint(entityNetPlayer.PlayerTransform.position, this.vipIcons[1]);
												this.gui.color = color;
											}
											else
											{
												this.DrawPoint(entityNetPlayer.PlayerTransform.position, this.vipIcons[1]);
											}
										}
									}
									if (clientEntity != null && clientEntity.EntityType == EntityType.sonar && Vector3.Distance(clientEntity.state.pos, entityNetPlayer.Position) < 20f && entityNetPlayer.lastSonarTime + 1f < Time.time && ((clientEntity.state.isBear && playerType == PlayerType.bear && entityNetPlayer.playerInfo.playerType == PlayerType.usec) || (!clientEntity.state.isBear && playerType == PlayerType.usec && entityNetPlayer.playerInfo.playerType == PlayerType.bear)))
									{
										entityNetPlayer.lastSonarTime = Time.time + 1f;
									}
									if (entityNetPlayer.IsVip)
									{
										Vector3 screen2 = Peer.ClientGame.LocalPlayer.MainCamera.WorldToScreenPoint(entityNetPlayer.NeckPosition + new Vector3(0f, 0.35f, 0f));
										if (screen2.z > 0f)
										{
											this.DrawVIPScreenspaceInfo(screen2, false);
										}
									}
								}
								if (entityNetPlayer.IsTalking && this.CanShowSupportInHardcore() && entityNetPlayer.NeckPosition != Vector3.zero)
								{
									Vector3 screen3 = Peer.ClientGame.LocalPlayer.MainCamera.WorldToScreenPoint(entityNetPlayer.NeckPosition + new Vector3(0f, 0.35f, 0f));
									this.RadarPoint(RadarEntityType.radio, entityNetPlayer.Position, entityNetPlayer.Euler.y, PlayerClass.none, false);
									if (screen3.z > 0f)
									{
										this.DrawRadioScreenSpaceInfo(screen3, Peer.ClientGame.LocalPlayer.IsTeam(entityNetPlayer));
									}
								}
							}
							else
							{
								if (!this.CanShowSupportInHardcore())
								{
									break;
								}
								Vector3 screen4 = Peer.ClientGame.LocalPlayer.MainCamera.WorldToScreenPoint(entityNetPlayer.NeckPosition + new Vector3(0f, 0.35f, 0f));
								this.lastPlayerDisctance = Mathf.Abs(Vector3.Distance(entity.Position, entityNetPlayer.PlayerTransform.position));
								if (screen4.z > 0f)
								{
									if (this.lastPlayerDisctance > 20f)
									{
										this.changedAlphaPlayerDistance = true;
										this.lastColorPlayerDistance = this.gui.color;
										if (this.lastPlayerDisctance < 50f)
										{
											this.alphaPlayerDistance = 1f / (this.lastPlayerDisctance - 20f) * 3f;
										}
										else
										{
											this.alphaPlayerDistance = 0f;
										}
										if (this.alphaPlayerDistance < 0f)
										{
											this.alphaPlayerDistance = 0f;
										}
										if (this.alphaPlayerDistance > 1f)
										{
											this.alphaPlayerDistance = 1f;
										}
									}
									else
									{
										this.alphaPlayerDistance = 1f;
									}
									this.DrawEnemyEntityScreenspaceInfo(entityNetPlayer, screen4, false, this.alphaPlayerDistance.Value);
								}
								if (((entityNetPlayer.lastFireTime > time && Vector3.Distance(entityNetPlayer.Position, entity.Position) < entityNetPlayer.Ammo.CurrentWeapon.HearRadius) || entityNetPlayer.lastSonarTime > time) && (Peer.ClientGame.LocalPlayer.SkillUnlocked(Skills.hear3) || !entityNetPlayer.Ammo.CurrentWeapon.SS))
								{
									if (this.showPlayerClassIcon && entityNetPlayer.playerInfo.playerClass != PlayerClass.none)
									{
										this.DrawPoint(entityNetPlayer.PlayerTransform.position, this.hostileClassIcons[(int)entityNetPlayer.playerInfo.playerClass]);
									}
									else
									{
										this.DrawPoint(entityNetPlayer.PlayerTransform.position, this.enemy);
									}
								}
								if (clientEntity != null && clientEntity.EntityType == EntityType.sonar && clientEntity.state.playerID == entity.playerInfo.playerID && Vector3.Distance(clientEntity.state.pos, entityNetPlayer.Position) < 20f && entityNetPlayer.lastSonarTime + 1f < Time.time)
								{
									entityNetPlayer.lastSonarTime = Time.time + 1f;
								}
							}
						}
					}
				}
			}
			GUI.matrix = Matrix4x4.TRS(Vector2.zero, Quaternion.identity, Vector3.one);
			this.entitiesIndex++;
			if (this.entitiesIndex > this.entites.Count)
			{
				this.entitiesIndex = 0;
			}
		}
		if (this.CanShowSupportInHardcore())
		{
			this.DrawRotatePoint(Peer.ClientGame.LocalPlayer.Position, Peer.ClientGame.LocalPlayer.Euler.y, this.player);
		}
		this.lastMatrix = GUI.matrix;
		this.DrawEntities();
		GUI.matrix = this.lastMatrix;
		for (int l = 0; l < this.hits.Count; l++)
		{
			this.gui.color = new Color(1f, 1f, 1f, Mathf.Min(Main.UserInfo.settings.radarAlpha * this.hits[l].visibility, 1f));
			this.DrawHitCircle(this.hits[l].Position, this.hits[l].Weapon, this.hits[l].Mod);
		}
		for (int m = 0; m < this.hotspots.Count; m++)
		{
			this.gui.color = new Color(1f, 1f, 1f, Mathf.Min(this.hotspotAlpha.Evaluate(this.hotspots[m].Elapsed), 1f));
			float d = 1f;
			if (this.hotspots[m].Distance > 50f && this.hotspots[m].Distance < 100f)
			{
				d = 1f - (this.hotspots[m].Distance / 50f - 1f) * 0.9f;
			}
			if (this.hotspots[m].Distance >= 100f)
			{
				d = 0.1f;
			}
			Vector3 vector2 = CameraListener.Camera.WorldToScreenPoint(this.hotspots[m].Position);
			if (vector2.z > 0f)
			{
				this.gui.PictureScreenSpace(new Vector2(vector2.x, (float)Screen.height - vector2.y), this.hotspot, Vector2.one * d * this.hotspotScale.Evaluate(this.hotspots[m].Elapsed));
				this.gui.color = new Color(1f, 1f, 1f, Main.UserInfo.settings.radarAlpha);
			}
		}
		if (Peer.HardcoreMode && Main.IsAlive)
		{
			this.gui.color = new Color(1f, 1f, 1f, 1f);
			this.gui.BeginGroup(new Rect((float)(Screen.width / 2 - 150), 0f, (float)(Screen.width / 2 + 150), 20f));
			this.gui.Picture(new Vector2(0f, 0f), this.hc_topBar);
			if (!Main.IsTeamGame)
			{
				this.gui.TextLabel(new Rect(20f, 2f, 25f, 15f), "#", 12, "#b10909_Micra", TextAnchor.UpperLeft, true);
				this.gui.TextLabel(new Rect(28f, 2f, 37f, 15f), this.tabGUI.cachedMatchPlace.ToString(), 9, "#FFFFFF_Micra", TextAnchor.UpperLeft, true);
				this.gui.TextLabel(new Rect(67f, 2f, 84f, 15f), "EXP", 9, "#62aeea_Micra", TextAnchor.UpperLeft, true);
				this.gui.TextLabel(new Rect(103f, 2f, 129f, 15f), this.tabGUI.myExp.ToString(), 9, "#FFFFFF_Micra", TextAnchor.UpperLeft, true);
				this.gui.TextLabel(new Rect(159f, 2f, 200f, 15f), this.gui.SecondsToStringMS((int)Peer.ClientGame.ElapsedNextEventTime), 9, "#c9c9c9_Micra", TextAnchor.UpperLeft, true);
				string content4 = (!ClientLeagueSystem.IsLeagueGame) ? Main.GameModeInfo.matchNeededPoints.ToString() : Main.GameModeInfo.LeagueMatchNeededPoints.ToString();
				this.gui.TextLabel(new Rect(230f, 2f, 270f, 15f), content4, 9, "#FFFFFF_Micra", TextAnchor.UpperLeft, true);
			}
			else
			{
				if (Peer.ClientGame.LocalPlayer.IsBear)
				{
					this.gui.TextLabel(new Rect(15f, 2f, 50f, 15f), "BEAR", 9, Colors.RadarBlueWeb + "_Micra", TextAnchor.UpperLeft, true);
					this.gui.TextLabel(new Rect(65f, 2f, 70f, 15f), Peer.ClientGame.BearWinCount.ToString(), 9, "#FFFFFF_Micra", TextAnchor.UpperLeft, true);
					this.gui.TextLabel(new Rect(105f, 2f, 125f, 15f), "USEC", 9, "#ad0505_Micra", TextAnchor.UpperLeft, true);
					this.gui.TextLabel(new Rect(153f, 2f, 170f, 15f), Peer.ClientGame.UsecWinCount.ToString(), 9, "#FFFFFF_Micra", TextAnchor.UpperLeft, true);
				}
				else
				{
					this.gui.TextLabel(new Rect(15f, 2f, 50f, 15f), "USEC", 9, Colors.RadarBlueWeb + "_Micra", TextAnchor.UpperLeft, true);
					this.gui.TextLabel(new Rect(64f, 2f, 70f, 15f), Peer.ClientGame.UsecWinCount.ToString(), 9, "#FFFFFF_Micra", TextAnchor.UpperLeft, true);
					this.gui.TextLabel(new Rect(110f, 2f, 125f, 15f), "BEAR", 9, "#ad0505_Micra", TextAnchor.UpperLeft, true);
					this.gui.TextLabel(new Rect(160f, 2f, 170f, 15f), Peer.ClientGame.BearWinCount.ToString(), 9, "#FFFFFF_Micra", TextAnchor.UpperLeft, true);
				}
				this.gui.TextLabel(new Rect(195f, 2f, 200f, 15f), this.gui.SecondsToStringMS((int)Peer.ClientGame.ElapsedNextEventTime), 9, "#c9c9c9_Micra", TextAnchor.UpperLeft, true);
				if (Main.IsTargetDesignation)
				{
					this.gui.TextLabel(new Rect(251f, 2f, 270f, 15f), Main.GameModeInfo.matchNeededKills.ToString(), 9, "#FFFFFF_Micra", TextAnchor.UpperLeft, true);
				}
				if (Main.IsTeamElimination)
				{
					string content5 = (!ClientLeagueSystem.IsLeagueGame) ? Main.GameModeInfo.matchNeededTeamKills.ToString() : Main.GameModeInfo.LeagueMatchNeededTeamKills.ToString();
					this.gui.TextLabel(new Rect(251f, 2f, 270f, 15f), content5, 9, "#FFFFFF_Micra", TextAnchor.UpperLeft, true);
				}
				if (Main.IsTacticalConquest)
				{
					string content6 = (!ClientLeagueSystem.IsLeagueGame) ? Main.GameModeInfo.matchNeededPoints.ToString() : Main.GameModeInfo.LeagueMatchNeededPoints.ToString();
					this.gui.TextLabel(new Rect(251f, 2f, 270f, 15f), content6, 9, "#FFFFFF_Micra", TextAnchor.UpperLeft, true);
				}
			}
			this.gui.EndGroup();
		}
	}

	// Token: 0x06000A73 RID: 2675 RVA: 0x0007D78C File Offset: 0x0007B98C
	public override void OnUpdate()
	{
		for (int i = 0; i < this.hits.Count; i++)
		{
			if (!this.hits[i].Visible)
			{
				this.hits.RemoveAt(i);
				i = -1;
			}
		}
		for (int j = 0; j < this.hotspots.Count; j++)
		{
			if (!this.hotspots[j].Visible)
			{
				this.hotspots.RemoveAt(j);
				j = -1;
			}
		}
	}

	// Token: 0x06000A74 RID: 2676 RVA: 0x0007D824 File Offset: 0x0007BA24
	private void DrawRotatePoint(Vector3 position, float rotation, Texture2D icon)
	{
		if (icon != null)
		{
			Vector2 vector = this.WorldPosToRadar(position);
			this.lastMatrix = GUI.matrix;
			GUI.matrix = Matrix4x4.TRS(vector, Quaternion.Euler(0f, 0f, rotation), Vector3.one) * Matrix4x4.TRS(-vector, Quaternion.identity, Vector3.one);
			GUI.DrawTexture(new Rect(vector.x - (float)icon.width / 2f, vector.y - (float)icon.height / 2f, (float)icon.width, (float)icon.height), icon, ScaleMode.StretchToFill, true);
			GUI.matrix = this.lastMatrix;
		}
	}

	// Token: 0x06000A75 RID: 2677 RVA: 0x0007D8E4 File Offset: 0x0007BAE4
	public void DrawPoint(Vector3 position, Texture2D icon)
	{
		try
		{
			Vector2 vector = this.WorldPosToRadar(position);
			GUI.DrawTexture(new Rect(vector.x - (float)icon.width / 2f, vector.y - (float)icon.height / 2f, (float)icon.width, (float)icon.height), icon, ScaleMode.StretchToFill, true);
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000A76 RID: 2678 RVA: 0x0007D968 File Offset: 0x0007BB68
	protected Vector2 WorldPosToRadar(Vector3 pos)
	{
		if (pos == Vector3.zero)
		{
			return Vector2.zero;
		}
		return new Vector2(Mathf.Abs((this.UpperLeft.x - pos.x) / this._mapSize.x) * (float)this.level.width + this.mapPos.x, Mathf.Abs((pos.z - this.UpperLeft.z) / this._mapSize.z) * (float)this.level.height + this.mapPos.y);
	}

	// Token: 0x04000BE2 RID: 3042
	private List<EntityNetPlayer> allplayers;

	// Token: 0x04000BE3 RID: 3043
	private List<ClientEntity> entites;

	// Token: 0x04000BE4 RID: 3044
	private static RadarGUI I;

	// Token: 0x04000BE5 RID: 3045
	private TacticalPoint[] tacticalPoints;

	// Token: 0x04000BE6 RID: 3046
	public GUIStyle EntityInfoLabel;

	// Token: 0x04000BE7 RID: 3047
	public Texture2D background;

	// Token: 0x04000BE8 RID: 3048
	public Texture2D topliner;

	// Token: 0x04000BE9 RID: 3049
	public Texture2D player;

	// Token: 0x04000BEA RID: 3050
	public Texture2D team;

	// Token: 0x04000BEB RID: 3051
	public Texture2D enemy;

	// Token: 0x04000BEC RID: 3052
	public Texture2D radarMark;

	// Token: 0x04000BED RID: 3053
	public Texture2D radarCircle;

	// Token: 0x04000BEE RID: 3054
	public Texture2D radarTick;

	// Token: 0x04000BEF RID: 3055
	public Texture2D mortarStrike;

	// Token: 0x04000BF0 RID: 3056
	public Texture2D sonar;

	// Token: 0x04000BF1 RID: 3057
	public Texture2D placement;

	// Token: 0x04000BF2 RID: 3058
	public Texture2D grenade;

	// Token: 0x04000BF3 RID: 3059
	public Texture2D smallHPIcon;

	// Token: 0x04000BF4 RID: 3060
	public Texture2D[] friendlyClassIcons;

	// Token: 0x04000BF5 RID: 3061
	public Texture2D[] hostileClassIcons;

	// Token: 0x04000BF6 RID: 3062
	public AnimationCurve tickScale;

	// Token: 0x04000BF7 RID: 3063
	public AnimationCurve tickAlpha;

	// Token: 0x04000BF8 RID: 3064
	public AudioClip beep;

	// Token: 0x04000BF9 RID: 3065
	public Texture2D armorIcon;

	// Token: 0x04000BFA RID: 3066
	public Texture2D hostileIcon;

	// Token: 0x04000BFB RID: 3067
	public Texture2D hit_circle;

	// Token: 0x04000BFC RID: 3068
	public Texture2D hotspot;

	// Token: 0x04000BFD RID: 3069
	public AnimationCurve hotspotScale;

	// Token: 0x04000BFE RID: 3070
	public AnimationCurve hotspotAlpha;

	// Token: 0x04000BFF RID: 3071
	public Texture2D[] vipIcons;

	// Token: 0x04000C00 RID: 3072
	public Texture2D[] radioIcon;

	// Token: 0x04000C01 RID: 3073
	public Texture2D[] tcPointIcons;

	// Token: 0x04000C02 RID: 3074
	public Texture2D[] tcSlotIcons;

	// Token: 0x04000C03 RID: 3075
	public Texture2D[] tcPointIconsScreen;

	// Token: 0x04000C04 RID: 3076
	public Texture2D hc_topBar;

	// Token: 0x04000C05 RID: 3077
	public AnimationCurve VipIconCurve = new AnimationCurve();

	// Token: 0x04000C06 RID: 3078
	protected eTimer blinkTimer = new eTimer();

	// Token: 0x04000C07 RID: 3079
	protected bool isBlink;

	// Token: 0x04000C08 RID: 3080
	private bool canShowSupport;

	// Token: 0x04000C09 RID: 3081
	private bool enableNewRadar;

	// Token: 0x04000C0A RID: 3082
	private Keyframe[] tickScaleKeys;

	// Token: 0x04000C0B RID: 3083
	private Keyframe[] tickAlphaKeys;

	// Token: 0x04000C0C RID: 3084
	private TabGUI tabGUI;

	// Token: 0x04000C0D RID: 3085
	private KillsGUI killsGUI;

	// Token: 0x04000C0E RID: 3086
	private Color lastColorPlayerDistance;

	// Token: 0x04000C0F RID: 3087
	private float lastPlayerDisctance;

	// Token: 0x04000C10 RID: 3088
	private bool changedAlphaPlayerDistance;

	// Token: 0x04000C11 RID: 3089
	private Float alphaPlayerDistance = new Float(0f);

	// Token: 0x04000C12 RID: 3090
	private float tmpval;

	// Token: 0x04000C13 RID: 3091
	private List<EnemyRadarPosition> enemies = new List<EnemyRadarPosition>();

	// Token: 0x04000C14 RID: 3092
	private List<EnemyRadarPosition> hits = new List<EnemyRadarPosition>();

	// Token: 0x04000C15 RID: 3093
	private List<HotspotRadarState> hotspots = new List<HotspotRadarState>();

	// Token: 0x04000C16 RID: 3094
	private Texture2D level;

	// Token: 0x04000C17 RID: 3095
	private float scale = 1f;

	// Token: 0x04000C18 RID: 3096
	private Vector3 UpperLeft = Vector3.zero;

	// Token: 0x04000C19 RID: 3097
	private Vector3 LowerRight = Vector3.zero;

	// Token: 0x04000C1A RID: 3098
	private int entitiesIndex;

	// Token: 0x04000C1B RID: 3099
	private bool showPlayerClassIcon;

	// Token: 0x04000C1C RID: 3100
	private Vector2 mapPos = new Vector2(20f, 13f);

	// Token: 0x04000C1D RID: 3101
	private Matrix4x4 lastMatrix = default(Matrix4x4);

	// Token: 0x04000C1E RID: 3102
	public Vector3 _mapSize = default(Vector3);

	// Token: 0x04000C1F RID: 3103
	private Vector3 RadarSize = new Vector2(100f, 100f);

	// Token: 0x04000C20 RID: 3104
	private StandartRadar radar = new StandartRadar();
}
