using System;
using System.Collections.Generic;
using System.Reflection;
using cygwin_x32;
using cygwin_x32.ObscuredTypes;
using SpectatorGUInamespace;
using UnityEngine;

// Token: 0x02000180 RID: 384
[AddComponentMenu("Scripts/GUI/Spectactor")]
internal class SpectactorGUI : Form
{
	// Token: 0x17000150 RID: 336
	// (get) Token: 0x06000B04 RID: 2820 RVA: 0x00089BD8 File Offset: 0x00087DD8
	private bool NewTeamBalancing
	{
		get
		{
			return Globals.I.NewTeamBalancing > 0;
		}
	}

	// Token: 0x17000151 RID: 337
	// (get) Token: 0x06000B05 RID: 2821 RVA: 0x00089BE8 File Offset: 0x00087DE8
	private bool BalanceClanPlayers
	{
		get
		{
			return Globals.I.NewTeamBalancing > 1;
		}
	}

	// Token: 0x17000152 RID: 338
	// (get) Token: 0x06000B06 RID: 2822 RVA: 0x00089BF8 File Offset: 0x00087DF8
	private UserInfo userInfo
	{
		get
		{
			return Main.UserInfo;
		}
	}

	// Token: 0x17000153 RID: 339
	// (get) Token: 0x06000B07 RID: 2823 RVA: 0x00089C00 File Offset: 0x00087E00
	public ClientNetPlayer player
	{
		get
		{
			return Peer.ClientGame.LocalPlayer;
		}
	}

	// Token: 0x06000B08 RID: 2824 RVA: 0x00089C0C File Offset: 0x00087E0C
	[Obfuscation(Exclude = true)]
	private void ShowSpectactorCamera(object obj)
	{
		base.Invoke("IvokeSpecPlans", (float)obj);
	}

	// Token: 0x06000B09 RID: 2825 RVA: 0x00089C20 File Offset: 0x00087E20
	[Obfuscation(Exclude = true)]
	public void IvokeSpecPlans(object ojb)
	{
		this.camSpectator.SetState(this.camSpectator.spectatePlans);
	}

	// Token: 0x06000B0A RID: 2826 RVA: 0x00089C38 File Offset: 0x00087E38
	[Obfuscation(Exclude = true)]
	public void ShowTeamChoose(object obj)
	{
		this.showTeamChooseAlpha.Show((float)obj, 0f);
	}

	// Token: 0x06000B0B RID: 2827 RVA: 0x00089C50 File Offset: 0x00087E50
	[Obfuscation(Exclude = true)]
	public void HideTeamChoose(object obj)
	{
		this.showTeamChooseAlpha.Hide((float)obj, 0f);
	}

	// Token: 0x06000B0C RID: 2828 RVA: 0x00089C68 File Offset: 0x00087E68
	[Obfuscation(Exclude = true)]
	public void HideSpectactor(object obj)
	{
		this.Hide(0.35f);
		this.camSpectator.Disable();
	}

	// Token: 0x17000154 RID: 340
	// (get) Token: 0x06000B0D RID: 2829 RVA: 0x00089C80 File Offset: 0x00087E80
	public float WaitForSpawn
	{
		get
		{
			float result;
			try
			{
				bool flag = Main.UserInfo.skillUnlocked(Skills.car_respawn);
				ObscuredFloat value;
				if (Main.IsTeamElimination)
				{
					value = 5f;
					if (flag)
					{
						value /= 2f;
					}
				}
				else
				{
					value = 3f;
					if (flag)
					{
						value /= 2f;
					}
				}
				result = value;
			}
			catch (Exception)
			{
				result = 100500f;
			}
			return result;
		}
	}

	// Token: 0x06000B0E RID: 2830 RVA: 0x00089D34 File Offset: 0x00087F34
	public void TeamSelect()
	{
		if (Main.IsRoundGoingOrAlone && Main.IsDeadOrSpectactor && this.fSpawnTimer - HtmlLayer.serverUtc < 0)
		{
			this.gui.color = Colors.alpha(Color.white, 1f);
			Rect rect = new Rect((float)(Screen.width / 2 - this.gui.Width / 2), (float)(Screen.height / 2 - this.gui.Height / 2), (float)this.gui.Width, (float)this.gui.Height);
			this.gui.BeginGroup(rect, false);
			if (!Main.IsDeathMatch)
			{
				if (!Main.IsTeamElimination)
				{
					if (Main.IsTargetDesignation)
					{
					}
				}
			}
			this.gui.EndGroup();
			this.gui.color = Colors.alpha(Color.white, base.visibility);
		}
	}

	// Token: 0x06000B0F RID: 2831 RVA: 0x00089E28 File Offset: 0x00088028
	private void TeamChooseWindow()
	{
		if (!this.GameplayTutorShowed || ClientLeagueSystem.IsLeagueGame)
		{
			return;
		}
		if (Peer.ClientGame.IsFull)
		{
			this.teamChoosing = false;
			if (!this.showTeamChooseAlpha.Hiding)
			{
				this.showTeamChooseAlpha.Hide(0.5f, 0f);
			}
			return;
		}
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		int teamWeightOdds = Globals.I.TeamWeightOdds;
		int teamPlayersOdds = Globals.I.TeamPlayersOdds;
		for (int i = 0; i < Peer.ClientGame.AllPlayers.Count; i++)
		{
			EntityNetPlayer entityNetPlayer = Peer.ClientGame.AllPlayers[i];
			if (!entityNetPlayer.IsSpectactor)
			{
				if (!EntityNetPlayer.IsClientPlayer(entityNetPlayer.ID))
				{
					if (entityNetPlayer.IsBear)
					{
						num++;
						num3 += entityNetPlayer.Weight;
						if (!string.IsNullOrEmpty(entityNetPlayer.ClanTag) && !list.Contains(entityNetPlayer.ClanTag))
						{
							list.Add(entityNetPlayer.ClanTag);
						}
					}
					else
					{
						num2++;
						num4 += entityNetPlayer.Weight;
						if (!string.IsNullOrEmpty(entityNetPlayer.ClanTag) && !list2.Contains(entityNetPlayer.ClanTag))
						{
							list2.Add(entityNetPlayer.ClanTag);
						}
					}
				}
			}
		}
		if (!this.NewTeamBalancing)
		{
			if (num2 - num <= 0 || num2 + num < teamPlayersOdds)
			{
				flag = true;
			}
			if (num - num2 <= 0 || num2 + num < teamPlayersOdds)
			{
				flag2 = true;
			}
		}
		else
		{
			if (this.userInfo.clanID != 0 && (num > 0 || num2 > 0))
			{
				flag3 = list.Contains(this.userInfo.clanTag);
				flag4 = list2.Contains(this.userInfo.clanTag);
				if (num - num2 > teamPlayersOdds)
				{
					flag3 = false;
				}
				if (num2 - num > teamPlayersOdds)
				{
					flag4 = false;
				}
			}
			if (this.BalanceClanPlayers)
			{
				flag3 = false;
				flag4 = false;
			}
			if (Mathf.Abs(num3 - num4) <= teamWeightOdds)
			{
				flag2 = true;
				flag = true;
				if (num - num2 > teamPlayersOdds)
				{
					flag2 = false;
				}
				if (num2 - num > teamPlayersOdds)
				{
					flag = false;
				}
				if (!this.player.IsSpectactor)
				{
					if (this.player.IsBear)
					{
						int num5 = Mathf.Abs(num3 - this.player.Weight - (num4 + this.player.Weight));
						if (num5 > teamWeightOdds && !flag4)
						{
							flag = false;
						}
					}
					else
					{
						int num6 = Mathf.Abs(num4 - this.player.Weight - (num3 + this.player.Weight));
						if (num6 > teamWeightOdds && !flag3)
						{
							flag2 = false;
						}
					}
				}
			}
			else if (num3 < num4)
			{
				flag2 = true;
				if (flag4)
				{
					flag = true;
				}
				if (num - num2 > teamPlayersOdds)
				{
					flag2 = false;
					flag = true;
				}
				if (!this.player.IsSpectactor && !this.player.IsBear)
				{
					int num7 = num3 + this.player.Weight - (num4 - this.player.Weight);
					if (num7 > teamWeightOdds && !flag3 && num > 0)
					{
						flag2 = false;
					}
					flag = true;
				}
			}
			else
			{
				flag = true;
				if (flag3)
				{
					flag2 = true;
				}
				if (num2 - num > teamPlayersOdds)
				{
					flag2 = true;
					flag = false;
				}
				if (!this.player.IsSpectactor && this.player.IsBear)
				{
					int num8 = num4 + this.player.Weight - (num3 - this.player.Weight);
					if (num8 > teamWeightOdds && !flag4 && num2 > 0)
					{
						flag = false;
					}
					flag2 = true;
				}
			}
		}
		Rect rect = new Rect((float)(Screen.width / 2 - this.background.width / 2), (float)(Screen.height / 2 - this.background.height / 2), (float)this.background.width, (float)this.background.height);
		this.gui.BeginGroup(rect, false);
		this.gui.color = Colors.alpha(this.gui.color, this.showTeamChooseAlpha.visibility);
		if ((Main.IsPlayerSpectactor || this.teamChoosing) && this.showTeamChooseAlpha.visibility > 0.01f)
		{
			this.gui.Picture(new Vector2(0f, 0f), this.background);
			if (!flag)
			{
				this.gui.color = Colors.alpha(this.gui.color, this.showTeamChooseAlpha.visibility * 0.5f);
			}
			if (this.gui.Button(new Vector2(130f, 70f), this.usecUnSelected, (!flag) ? this.usecUnSelected : this.usecSelected, (!flag) ? this.usecUnSelected : this.usecSelected, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked && this.showTeamChooseAlpha.MaxVisible && flag)
			{
				this.HideTeamChoose(0.5f);
				this.InitTacticalPoints();
				Peer.ClientGame.LocalPlayer.ChooseTeam(PlayerType.usec);
				this.teamChoosing = false;
				if (Peer.ClientGame.LocalPlayer.IsBear)
				{
					this.changeTeamCount--;
				}
				if (Main.IsTacticalConquest)
				{
					this.spawnWindow.SelectDefaultPoint(false);
				}
			}
			if (!flag)
			{
				this.gui.color = Colors.alpha(this.gui.color, this.showTeamChooseAlpha.visibility);
			}
			if (!flag2)
			{
				this.gui.color = Colors.alpha(this.gui.color, this.showTeamChooseAlpha.visibility * 0.5f);
			}
			if (this.gui.Button(new Vector2(398f, 30f), this.bearUnSelected, (!flag2) ? this.bearUnSelected : this.bearSelected, (!flag2) ? this.bearUnSelected : this.bearSelected, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked && this.showTeamChooseAlpha.MaxVisible && flag2)
			{
				this.HideTeamChoose(0.5f);
				this.InitTacticalPoints();
				this.teamChoosing = false;
				Peer.ClientGame.LocalPlayer.ChooseTeam(PlayerType.bear);
				if (!Peer.ClientGame.LocalPlayer.IsBear)
				{
					this.changeTeamCount--;
				}
				if (Main.IsTacticalConquest)
				{
					this.spawnWindow.SelectDefaultPoint(true);
				}
			}
			if (!flag2)
			{
				this.gui.color = Colors.alpha(this.gui.color, this.showTeamChooseAlpha.visibility);
			}
			if (Main.IsPlayerSpectactor && this.gui.Button(new Vector2(158f, 370f), this.gui.mainMenuButtons[0], (!flag && !flag2) ? this.gui.mainMenuButtons[0] : this.gui.mainMenuButtons[1], (!flag && !flag2) ? this.gui.mainMenuButtons[0] : this.gui.mainMenuButtons[1], Language.SpecAutobalance, 15, "#adb0af", TextAnchor.MiddleCenter, null, null, null, null).Clicked && this.showTeamChooseAlpha.MaxVisible && (flag || flag2))
			{
				this.HideTeamChoose(0.5f);
				this.teamChoosing = false;
				if (this.NewTeamBalancing)
				{
					Peer.ClientGame.LocalPlayer.ChooseTeam((num3 <= num4) ? PlayerType.bear : PlayerType.usec);
				}
				else
				{
					Peer.ClientGame.LocalPlayer.ChooseTeam((num <= num2) ? PlayerType.bear : PlayerType.usec);
				}
				this.changeTeamCount--;
				if (Main.IsTacticalConquest)
				{
					this.spawnWindow.SelectDefaultPoint((!this.NewTeamBalancing) ? (num < num2) : (num3 < num4));
				}
			}
			if (this.gui.Button(new Vector2((!Main.IsPlayerSpectactor) ? ((rect.width - (float)this.gui.mainMenuButtons[0].width) / 2f) : 373f, 370f), this.gui.mainMenuButtons[0], this.gui.mainMenuButtons[1], this.gui.mainMenuButtons[1], Language.SpecSpectator, 15, "#adb0af", TextAnchor.MiddleCenter, null, null, null, null).Clicked && this.showTeamChooseAlpha.MaxVisible)
			{
				this.HideTeamChoose(0.5f);
				this.teamChoosing = false;
				if (Main.IsAlive)
				{
					Peer.ClientGame.LocalPlayer.ChooseTeam(PlayerType.spectactor);
				}
				Peer.ClientGame.LocalPlayer.IsTeamChoosed = false;
			}
			if (!flag2)
			{
				this.gui.TextLabel(new Rect(447f, 237f, 100f, 50f), Language.SpecTeamIsOverpowered, 15, Colors.RadarRedWeb, TextAnchor.UpperCenter, true);
			}
			if (!flag)
			{
				this.gui.TextLabel(new Rect(190f, 237f, 100f, 50f), Language.SpecTeamIsOverpowered, 15, Colors.RadarRedWeb, TextAnchor.UpperCenter, true);
			}
			this.gui.TextLabel(new Rect(151f, 21f, 400f, 50f), Language.SpecChooseTeam, 16, "#FFFFFF_Micra", TextAnchor.UpperCenter, true);
			this.gui.TextLabel(new Rect(444f, 300f, 100f, 50f), num + Language.SpecGamers, 20, "#c9c9c9", TextAnchor.UpperCenter, true);
			this.gui.TextLabel(new Rect(190f, 300f, 100f, 50f), num2 + Language.SpecGamers, 20, "#c9c9c9", TextAnchor.UpperCenter, true);
			if (Main.UserInfo.Permission >= EPermission.Admin)
			{
				this.gui.TextLabel(new Rect(444f, 330f, 100f, 50f), num3, 20, "#c9c9c9", TextAnchor.UpperCenter, true);
				this.gui.TextLabel(new Rect(190f, 330f, 100f, 50f), num4, 20, "#c9c9c9", TextAnchor.UpperCenter, true);
			}
		}
		this.gui.color = Colors.alpha(this.gui.color, base.visibility);
		this.gui.EndGroup();
	}

	// Token: 0x06000B10 RID: 2832 RVA: 0x0008A9CC File Offset: 0x00088BCC
	public void TextFieldBacgr(Rect textRect, string str, int fontsize, string colortext, TextAnchor alignment = TextAnchor.UpperCenter)
	{
		this.gui.color = Colors.alpha(this.gui.color, 0.4f);
		this.gui.PictureSized(new Vector2((textRect.xMax + textRect.xMin - this.gui.CalcWidth(str, this.gui.fontDNC57, fontsize)) / 2f, textRect.yMin + 2f), this.gui.black, new Vector2(this.gui.CalcWidth(str, this.gui.fontDNC57, fontsize) + 2f, this.gui.CalcHeight(str, textRect.width, this.gui.fontDNC57, fontsize) - 5f));
		this.gui.color = Colors.alpha(this.gui.color, base.visibility);
		this.gui.TextLabel(textRect, str, fontsize, colortext, alignment, true);
	}

	// Token: 0x06000B11 RID: 2833 RVA: 0x0008AACC File Offset: 0x00088CCC
	public int GetFontSize()
	{
		return 16;
	}

	// Token: 0x06000B12 RID: 2834 RVA: 0x0008AAD0 File Offset: 0x00088CD0
	private float GetCentred(float x1, float x2)
	{
		return Mathf.Abs((x1 - x2) / 2f);
	}

	// Token: 0x06000B13 RID: 2835 RVA: 0x0008AAE0 File Offset: 0x00088CE0
	private Rect GetCenterdRect(float x, float y, float rectwidth, float rectheight)
	{
		return new Rect((x - rectwidth) / 2f, (y - rectheight) / 2f, (x - rectwidth) / 2f + rectwidth, (y - rectheight) / 2f + (float)this.Height);
	}

	// Token: 0x06000B14 RID: 2836 RVA: 0x0008AB18 File Offset: 0x00088D18
	public override void MainInitialize()
	{
		SpectactorGUI.I = this;
		base.MainInitialize();
	}

	// Token: 0x06000B15 RID: 2837 RVA: 0x0008AB28 File Offset: 0x00088D28
	public override void OnInitialized()
	{
		this.spawnWindow.OnStart();
		base.OnInitialized();
	}

	// Token: 0x06000B16 RID: 2838 RVA: 0x0008AB3C File Offset: 0x00088D3C
	public override void Register()
	{
		base.Register();
		EventFactory.Register("ShowSpectactorCamera", this);
		EventFactory.Register("HideSpectactor", this);
		EventFactory.Register("ShowTeamChoose", this);
		EventFactory.Register("HideTeamChoose", this);
	}

	// Token: 0x06000B17 RID: 2839 RVA: 0x0008AB7C File Offset: 0x00088D7C
	public override void Clear()
	{
		base.Clear();
		this.fSpawnTimer = -1;
		this.redPos = Vector3.zero;
		this.killAlpha = new Alpha();
		this.showTeamChooseAlpha = new Alpha();
	}

	// Token: 0x06000B18 RID: 2840 RVA: 0x0008ABB4 File Offset: 0x00088DB4
	public override void OnLevelLoaded()
	{
		this.MainInitialize();
		base.OnLevelLoaded();
	}

	// Token: 0x06000B19 RID: 2841 RVA: 0x0008ABC4 File Offset: 0x00088DC4
	public override void OnConnected()
	{
		this.timeFirstSpawn = 0f;
		this.fSpawnTimer = -1;
		this.bfirstGame = true;
		if (this.WaitForSpawn != 0f)
		{
			Main.UserInfo.settings.graphics.SelfRagDollTime = this.WaitForSpawn;
		}
		else
		{
			Main.UserInfo.settings.graphics.SelfRagDollTime = 5f;
		}
		this.spawnController.OnConnected();
		this.spawnWindow.Clear();
		this.Clear();
		this.level = SingletoneForm<LevelSettings>.Instance.radar;
		this.spawnWindow.spawnMap = this.level;
		this.isGameHandler = true;
		this.isUpdating = true;
		this.isRendering = true;
		this.UpperLeft = ((UpperLeftPoint)UnityEngine.Object.FindObjectOfType(typeof(UpperLeftPoint))).transform.position;
		this.LowerRight = ((LowerRightPoint)UnityEngine.Object.FindObjectOfType(typeof(LowerRightPoint))).transform.position;
		this.spawnWindow.UpperLeft = this.UpperLeft;
		this.spawnWindow.LowerRight = this.LowerRight;
		this.camSpectator.OnConnected();
		this.camSpectator.SetState(this.camSpectator.spectatePlans);
		this.InitTacticalPoints();
		if ((Peer.HardcoreMode || !CVars.InGameChangeTeam) && Peer.ClientGame.LocalPlayer != null)
		{
			if (Peer.ClientGame.LocalPlayer.IsTeamChoosed || ClientLeagueSystem.IsLeagueGame)
			{
				this.changeTeamCount = 0;
			}
			else
			{
				this.changeTeamCount = 1;
			}
		}
		else if (ClientLeagueSystem.IsLeagueGame)
		{
			this.changeTeamCount = 0;
		}
		else
		{
			this.changeTeamCount = 2;
		}
	}

	// Token: 0x06000B1A RID: 2842 RVA: 0x0008AD94 File Offset: 0x00088F94
	private void InitTacticalPoints()
	{
		if (!Main.IsTacticalConquest || this.tacticalPoints != null)
		{
			return;
		}
		ClientTacticalConquestGame.InitTP();
		this.tacticalPoints = (Peer.ClientGame as ClientTacticalConquestGame).TacticalPoints;
		this.spawnWindow.SetTacticalPoints(this.tacticalPoints);
	}

	// Token: 0x06000B1B RID: 2843 RVA: 0x0008ADE4 File Offset: 0x00088FE4
	public override void OnDisconnect()
	{
		this.tacticalPoints = null;
		this.Clear();
		base.CancelInvoke();
		this.spawnWindow.Clear();
		this.camSpectator.Disable();
		this.isGameHandler = true;
		this.isUpdating = true;
		this.isRendering = true;
		this.teamChoosing = false;
		this.showTeamChooseAlpha.Hide(0.5f, 0f);
	}

	// Token: 0x06000B1C RID: 2844 RVA: 0x0008AE4C File Offset: 0x0008904C
	[Obfuscation(Exclude = true)]
	public void InvokeSpecOnDie()
	{
		EventFactory.Call("ClearBlood", null);
		this.camSpectator.SetState(this.camSpectator.spectatePlayer);
		CygWin32L.Instance.GayCheck();
	}

	// Token: 0x06000B1D RID: 2845 RVA: 0x0008AE88 File Offset: 0x00089088
	public override void OnDie()
	{
		base.Invoke("InvokeSpecOnDie", Main.UserInfo.settings.graphics.SelfRagDollTime);
		ObscuredFloat value = 100500f;
		if ((!Main.IsRoundedGame || Main.IsTeamElimination) && Main.IsRoundGoingOrAlone)
		{
			if (Math.Abs(this.WaitForSpawn) > 0.01f)
			{
				this.fSpawnTimer = HtmlLayer.serverUtc + (int)this.WaitForSpawn;
			}
			else
			{
				this.fSpawnTimer = HtmlLayer.serverUtc + (int)value;
			}
		}
		if (Main.IsTacticalConquest)
		{
			if (this.WaitForSpawn != 0f)
			{
				this.spawnWindow.SetTimerButton(this.WaitForSpawn);
			}
			else
			{
				this.spawnWindow.SetTimerButton(value);
			}
		}
	}

	// Token: 0x06000B1E RID: 2846 RVA: 0x0008AF60 File Offset: 0x00089160
	public override void OnSpawn()
	{
		base.OnSpawn();
		this.camSpectator.Disable();
		base.CancelInvoke("InvokeSpecOnDie");
	}

	// Token: 0x06000B1F RID: 2847 RVA: 0x0008AF80 File Offset: 0x00089180
	public override void OnMatchStart()
	{
		if (Peer.HardcoreMode)
		{
			this.changeTeamCount = ((!Peer.ClientGame.LocalPlayer.IsTeamChoosed) ? 1 : 0);
		}
		else if (ClientLeagueSystem.IsLeagueGame)
		{
			this.changeTeamCount = 0;
		}
		else
		{
			this.changeTeamCount = 2;
		}
		base.OnMatchStart();
		if (this.tacticalPoints != null || !Main.IsTacticalConquest)
		{
			return;
		}
		this.spawnWindow.SetTacticalPoints(this.tacticalPoints);
		this.spawnWindow.SelectDefaultPoint(Main.LocalPlayer.IsBear);
	}

	// Token: 0x06000B20 RID: 2848 RVA: 0x0008B01C File Offset: 0x0008921C
	public override void OnMatchEnd()
	{
		this.camSpectator.SetState(this.camSpectator.spectatePlans);
		this.bfirstGame = false;
		this.teamChoosing = false;
		this.showTeamChooseAlpha.Hide(0.5f, 0f);
	}

	// Token: 0x06000B21 RID: 2849 RVA: 0x0008B058 File Offset: 0x00089258
	public override void OnRoundEnd()
	{
		this.camSpectator.SetState(this.camSpectator.spectatePlans);
		this.bfirstGame = false;
	}

	// Token: 0x06000B22 RID: 2850 RVA: 0x0008B078 File Offset: 0x00089278
	public override void OnRoundStart()
	{
		if (this.tacticalPoints == null && Main.IsTacticalConquest)
		{
			this.spawnWindow.SelectDefaultPoint(Main.LocalPlayer.IsBear);
		}
		if (Peer.HardcoreMode)
		{
			this.changeTeamCount = ((!Peer.ClientGame.LocalPlayer.IsTeamChoosed) ? 1 : 0);
		}
		else if (ClientLeagueSystem.IsLeagueGame)
		{
			this.changeTeamCount = 0;
		}
		else
		{
			this.changeTeamCount = 2;
		}
		this.camSpectator.SetState(this.camSpectator.spectatePlans);
	}

	// Token: 0x06000B23 RID: 2851 RVA: 0x0008B114 File Offset: 0x00089314
	public override void OnUpdate()
	{
		if (Main.IsShowingMatchResult || MainGUI.Instance.Visible)
		{
			return;
		}
		if (Main.IsTacticalConquest)
		{
			this.spawnWindow.OnUpdate();
		}
		if (Main.IsDeadOrSpectactor)
		{
			if (Input.GetMouseButtonDown(2))
			{
				if (this.camSpectator.CurrentState != this.camSpectator.spectatePlans)
				{
					this.camSpectator.SetState(this.camSpectator.spectatePlans);
				}
				else
				{
					this.camSpectator.SetState(this.camSpectator.spectatePlayer);
				}
			}
			if (Input.GetKeyDown(KeyCode.Space))
			{
				this.camSpectator.next();
			}
		}
		else if (this.camSpectator.Enabled)
		{
			this.camSpectator.Disable();
		}
		if (Peer.ClientGame.MatchState != MatchState.match_result && SingletoneForm<Loader>.Instance.IsGameLoadedAndClicked)
		{
			this.spawnController.OnUpdate();
		}
	}

	// Token: 0x06000B24 RID: 2852 RVA: 0x0008B214 File Offset: 0x00089414
	public override void GameGUI()
	{
		if (MainGUI.Instance.Visible)
		{
			return;
		}
		if (!SpectactorGUI.I.ControlTutorShowed)
		{
			this.controlHint.OnGUI();
		}
		else if (!SpectactorGUI.I.GameplayTutorShowed)
		{
			this.gameplayHint.OnGUI();
		}
		if (Main.IsTacticalConquest && Main.IsRoundGoingOrAlone)
		{
			if (!Peer.ClientGame.LocalPlayer.IsAlive && !Main.IsPlayerSpectactor && !Main.UserInfo.settings.graphics.HideInterface)
			{
				this.gui.color = Colors.alpha(this.gui.color, 1f);
				this.spawnWindow.OnGUI();
				this.gui.color = Colors.alpha(this.gui.color, base.visibility);
			}
			else if (!this.spawnWindow.isHidden)
			{
				this.spawnWindow.Hide();
			}
		}
		if (Main.IsDeadOrSpectactor && !this.showTeamChooseAlpha.Visible && !Main.UserInfo.settings.graphics.HideInterface)
		{
			this.spawnController.OnGUI();
			this.camSpectator.OnGUI();
		}
		if (this.teamChoosing)
		{
			this.TeamChooseWindow();
		}
	}

	// Token: 0x04000D09 RID: 3337
	public static SpectactorGUI I;

	// Token: 0x04000D0A RID: 3338
	public CamSpectator camSpectator = new CamSpectator();

	// Token: 0x04000D0B RID: 3339
	public SpawnContoller spawnController = new SpawnContoller();

	// Token: 0x04000D0C RID: 3340
	public SelectSpawnWindow spawnWindow = new SelectSpawnWindow();

	// Token: 0x04000D0D RID: 3341
	private TacticalPoint[] tacticalPoints;

	// Token: 0x04000D0E RID: 3342
	private Vector3 UpperLeft = default(Vector3);

	// Token: 0x04000D0F RID: 3343
	private Vector3 LowerRight = default(Vector3);

	// Token: 0x04000D10 RID: 3344
	private Texture2D level;

	// Token: 0x04000D11 RID: 3345
	public Texture2D background;

	// Token: 0x04000D12 RID: 3346
	public Texture2D usecSelected;

	// Token: 0x04000D13 RID: 3347
	public Texture2D usecUnSelected;

	// Token: 0x04000D14 RID: 3348
	public Texture2D bearSelected;

	// Token: 0x04000D15 RID: 3349
	public Texture2D bearUnSelected;

	// Token: 0x04000D16 RID: 3350
	public Texture2D weaponLevel;

	// Token: 0x04000D17 RID: 3351
	public Texture2D KeyBig;

	// Token: 0x04000D18 RID: 3352
	public Texture2D KeySmall;

	// Token: 0x04000D19 RID: 3353
	public Texture2D MBLeft;

	// Token: 0x04000D1A RID: 3354
	public Texture2D MBRight;

	// Token: 0x04000D1B RID: 3355
	public Texture2D MBMiddle;

	// Token: 0x04000D1C RID: 3356
	public Texture2D ExpIcon;

	// Token: 0x04000D1D RID: 3357
	public GUIStyle SmallButton = new GUIStyle();

	// Token: 0x04000D1E RID: 3358
	public GUIStyle BigButton = new GUIStyle();

	// Token: 0x04000D1F RID: 3359
	public GUIStyle WhiteLabelStyle = new GUIStyle();

	// Token: 0x04000D20 RID: 3360
	public GUIStyle GrayLabelStyle = new GUIStyle();

	// Token: 0x04000D21 RID: 3361
	public GUIStyle BlueLabelStyle = new GUIStyle();

	// Token: 0x04000D22 RID: 3362
	private Vector2 redPos = Vector3.zero;

	// Token: 0x04000D23 RID: 3363
	private Alpha killAlpha = new Alpha();

	// Token: 0x04000D24 RID: 3364
	public Alpha showTeamChooseAlpha = new Alpha();

	// Token: 0x04000D25 RID: 3365
	public bool teamChoosing;

	// Token: 0x04000D26 RID: 3366
	public bool ControlTutorShowed;

	// Token: 0x04000D27 RID: 3367
	public bool GameplayTutorShowed;

	// Token: 0x04000D28 RID: 3368
	private ControlHint controlHint = new ControlHint();

	// Token: 0x04000D29 RID: 3369
	private GameplayHint gameplayHint = new GameplayHint();

	// Token: 0x04000D2A RID: 3370
	public int changeTeamCount;

	// Token: 0x04000D2B RID: 3371
	public int fSpawnTimer = -1;

	// Token: 0x04000D2C RID: 3372
	public bool bfirstGame = true;

	// Token: 0x04000D2D RID: 3373
	public static string ButtonColor = "#62aeea";

	// Token: 0x04000D2E RID: 3374
	public static string TextColor = "#DDDDDD";

	// Token: 0x04000D2F RID: 3375
	public float timeFirstSpawn;
}
