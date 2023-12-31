using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x02000155 RID: 341
[AddComponentMenu("Scripts/GUI/KillsGUI")]
internal class KillsGUI : Form
{
	// Token: 0x0600086A RID: 2154 RVA: 0x0004C530 File Offset: 0x0004A730
	[Obfuscation(Exclude = true)]
	public void AddBeef(object obj)
	{
		object[] array = (object[])obj;
		KillData killData = new KillData();
		killData.headShot = (bool)array[3];
		if ((int)array[2] == 123)
		{
			killData.self = true;
		}
		else
		{
			killData.method = (Weapons)((int)array[2]);
		}
		killData.beef = (string)array[4];
		killData.beefColor = (string)array[5];
		killData.killer = (string)array[0];
		killData.killerColor = (string)array[1];
		killData.isBearBeef = (bool)array[7];
		killData.isBearKiller = (bool)array[6];
		killData.LifeTime = Time.realtimeSinceStartup;
		this.kills.Add(killData);
		if (this.kills.Count > 5)
		{
			this.kills.RemoveAt(0);
		}
	}

	// Token: 0x0600086B RID: 2155 RVA: 0x0004C60C File Offset: 0x0004A80C
	public override void MainInitialize()
	{
		this.Clear();
		this.isUpdating = true;
		base.MainInitialize();
	}

	// Token: 0x0600086C RID: 2156 RVA: 0x0004C624 File Offset: 0x0004A824
	public override void OnConnected()
	{
		this.isRendering = true;
		this.Clear();
	}

	// Token: 0x0600086D RID: 2157 RVA: 0x0004C634 File Offset: 0x0004A834
	public override void OnDisconnect()
	{
		this.isRendering = false;
		this.Clear();
	}

	// Token: 0x0600086E RID: 2158 RVA: 0x0004C644 File Offset: 0x0004A844
	public override void Clear()
	{
		this.kills = new List<KillData>();
		this.textureIndex = 0;
	}

	// Token: 0x0600086F RID: 2159 RVA: 0x0004C658 File Offset: 0x0004A858
	public override void Register()
	{
		EventFactory.Register("AddBeef", this);
	}

	// Token: 0x06000870 RID: 2160 RVA: 0x0004C668 File Offset: 0x0004A868
	public override void OnUpdate()
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
	}

	// Token: 0x06000871 RID: 2161 RVA: 0x0004C678 File Offset: 0x0004A878
	public override void GameGUI()
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
		if (Main.UserInfo.settings.graphics.HideInterface)
		{
			return;
		}
		for (int i = 0; i < this.kills.Count; i++)
		{
			this.textureIndex = (int)this.kills[i].method;
			this.colorAlpha = this.showKillCurve.Evaluate(Time.realtimeSinceStartup - this.kills[i].LifeTime);
			this.wr = this.whiteToRedCurve.Evaluate(Time.realtimeSinceStartup - this.kills[i].LifeTime);
			this.gui.color = new Color(1f, 1f, 1f, 1f - (Time.realtimeSinceStartup - this.kills[i].LifeTime) * 0.1f);
			this.gui.BeginGroup(new Rect((float)(Screen.width - this.background.width * 2 - 2), (float)(5 + (this.kills.Count - i - 1) * (this.background.height + 2) - this.background.height), (float)(this.background.width * 2), (float)(this.background.height * 2)));
			this.gui.color = new Color(0.9f, 0.9f, 0.9f, this.colorAlpha);
			this.gui.Picture(new Vector2((float)this.background.width, (float)this.background.height), this.background);
			if (this.kills[i].headShot)
			{
				this.gui.Picture(new Vector2(355f, 14f), this.killMethods[125]);
			}
			if (this.textureIndex < 101 && Main.UserInfo.weaponsStates[this.textureIndex].CurrentWeapon.isPremium)
			{
				this.gui.color = Colors.alpha(Colors.RadarWhite * this.wr * 0.9f + Colors.RadarYellow * (1f - this.wr) * 0.9f, this.colorAlpha);
			}
			if (this.kills[i].self)
			{
				this.gui.Picture(new Vector2(400f, 14f), this.killMethods[123]);
			}
			else
			{
				this.gui.Picture(new Vector2(369f, 14f), this.killMethods[this.textureIndex]);
			}
			this.gui.color = new Color(0.83f, 0.83f, 0.83f, this.showKillCurve.Evaluate(Time.realtimeSinceStartup - this.kills[i].LifeTime));
			if (Main.IsTeamGame)
			{
				if (this.kills[i].isBearKiller ^ !Peer.ClientGame.LocalPlayer.IsBear)
				{
					this.gui.TextLabel(new Rect(155f, 13f, 200f, 25f), this.kills[i].killer, 14, "#8fcce4", TextAnchor.UpperRight, true);
				}
				else
				{
					this.gui.TextLabel(new Rect(155f, 13f, 200f, 25f), this.kills[i].killer, 14, "#e9e9e9", TextAnchor.UpperRight, true);
				}
				if (this.kills[i].isBearBeef ^ !Peer.ClientGame.LocalPlayer.IsBear)
				{
					this.gui.TextLabel(new Rect(418f, 13f, 200f, 25f), this.kills[i].beef, 14, "#8fcce4", TextAnchor.UpperLeft, true);
				}
				else
				{
					this.gui.TextLabel(new Rect(418f, 13f, 200f, 25f), this.kills[i].beef, 14, "#e9e9e9", TextAnchor.UpperLeft, true);
				}
			}
			else
			{
				this.gui.TextLabel(new Rect(155f, 13f, 200f, 25f), this.kills[i].killer, 14, this.kills[i].killerColor, TextAnchor.UpperRight, true);
				this.gui.TextLabel(new Rect(418f, 13f, 200f, 25f), this.kills[i].beef, 14, this.kills[i].beefColor, TextAnchor.UpperLeft, true);
			}
			this.gui.EndGroup();
		}
	}

	// Token: 0x04000988 RID: 2440
	public Texture2D[] killMethods;

	// Token: 0x04000989 RID: 2441
	public Texture2D background;

	// Token: 0x0400098A RID: 2442
	public AnimationCurve showKillCurve;

	// Token: 0x0400098B RID: 2443
	public AnimationCurve whiteToRedCurve;

	// Token: 0x0400098C RID: 2444
	private List<KillData> kills = new List<KillData>();

	// Token: 0x0400098D RID: 2445
	private int textureIndex;

	// Token: 0x0400098E RID: 2446
	private float colorAlpha;

	// Token: 0x0400098F RID: 2447
	private float wr;
}
