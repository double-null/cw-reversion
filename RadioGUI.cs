using System;
using UnityEngine;

// Token: 0x02000171 RID: 369
[AddComponentMenu("Scripts/GUI/RadioGUI")]
internal class RadioGUI : Form
{
	// Token: 0x06000A78 RID: 2680 RVA: 0x0007DA38 File Offset: 0x0007BC38
	public void FloodCounters()
	{
		if (!this.floodTimerOn)
		{
			this.floodTimer.Start();
			this.floodTimerOn = true;
		}
		this.floodCounter++;
	}

	// Token: 0x06000A79 RID: 2681 RVA: 0x0007DA68 File Offset: 0x0007BC68
	public void RadioOn()
	{
		this.radioOn = true;
		this.currentTime = Time.realtimeSinceStartup;
	}

	// Token: 0x06000A7A RID: 2682 RVA: 0x0007DA7C File Offset: 0x0007BC7C
	public override void GameGUI()
	{
		if (!CVars.g_radio || base.visibility < 0.1f)
		{
			return;
		}
		if (Main.IsAlive && Main.IsTeamGame)
		{
			if (Peer.ClientGame.LocalPlayer.Ammo.cPrimary != null)
			{
				if (!Peer.ClientGame.LocalPlayer.Ammo.cSecondary.Empty)
				{
					this.noAmmo = false;
				}
				if (Peer.ClientGame.LocalPlayer.Ammo.cPrimary.Empty && Peer.ClientGame.LocalPlayer.Ammo.cSecondary.Empty && !this.noAmmo)
				{
					this.noAmmo = true;
					this.temp = this.rnd.Next(2);
					if (this.temp == 0)
					{
						Peer.ClientGame.LocalPlayer.Chat(Language.RadioEmpty0, ChatInfo.radio_message_at_hit);
						Peer.ClientGame.LocalPlayer.Radio(RadioEnum.bear_empty1);
					}
					else
					{
						Peer.ClientGame.LocalPlayer.Chat(Language.RadioEmpty1, ChatInfo.radio_message_at_hit);
						Peer.ClientGame.LocalPlayer.Radio(RadioEnum.bear_empty2);
					}
				}
			}
			else
			{
				if (!Peer.ClientGame.LocalPlayer.Ammo.cSecondary.Empty)
				{
					this.noAmmo = false;
				}
				if (Peer.ClientGame.LocalPlayer.Ammo.cSecondary.Empty && !this.noAmmo)
				{
					this.noAmmo = true;
					this.temp = this.rnd.Next(2);
					if (this.temp == 0)
					{
						Peer.ClientGame.LocalPlayer.Chat(Language.RadioEmpty0, ChatInfo.radio_message_at_hit);
						Peer.ClientGame.LocalPlayer.Radio(RadioEnum.bear_empty1);
					}
					else
					{
						Peer.ClientGame.LocalPlayer.Chat(Language.RadioEmpty1, ChatInfo.radio_message_at_hit);
						Peer.ClientGame.LocalPlayer.Radio(RadioEnum.bear_empty2);
					}
				}
			}
			this.gui.color = new Color(1f, 1f, 1f, base.visibility);
			this.gui.BeginGroup(new Rect((float)(Screen.width / 2 - 162), (float)(Screen.height / 2 - 76), 324f, 152f), !base.Visible);
			this.gui.Picture(new Vector2(0f, 0f), this.gui.radioChat[0]);
			if (this.gui.Button(new Vector2(34f, 4f), null, this.gui.radioChat[1], null, Language.RadioStart, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				this.FloodCounters();
				this.RadioOn();
				if (this.rnd.Next(2) == 0)
				{
					Peer.ClientGame.LocalPlayer.Chat(Language.RadioStart0, ChatInfo.radio_message);
					Peer.ClientGame.LocalPlayer.Radio(RadioEnum.bear_gogogo1);
				}
				else
				{
					Peer.ClientGame.LocalPlayer.Chat(Language.RadioStart1, ChatInfo.radio_message);
					Peer.ClientGame.LocalPlayer.Radio(RadioEnum.bear_gogogo2);
				}
			}
			if (this.gui.Button(new Vector2(8f, 40f), null, this.gui.radioChat[2], null, Language.RadioReceived, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				this.FloodCounters();
				this.RadioOn();
				if (this.rnd.Next(2) == 0)
				{
					Peer.ClientGame.LocalPlayer.Chat(Language.RadioReceived0, ChatInfo.radio_message);
					Peer.ClientGame.LocalPlayer.Radio(RadioEnum.bear_rogerthat1);
				}
				else
				{
					Peer.ClientGame.LocalPlayer.Chat(Language.RadioReceived1, ChatInfo.radio_message);
					Peer.ClientGame.LocalPlayer.Radio(RadioEnum.bear_rogerthat2);
				}
			}
			if (this.gui.Button(new Vector2(5f, 65f), null, this.gui.radioChat[3], null, Language.RadioCover, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				this.FloodCounters();
				this.RadioOn();
				if (this.rnd.Next(2) == 0)
				{
					Peer.ClientGame.LocalPlayer.Chat(Language.RadioCover0, ChatInfo.radio_message);
					Peer.ClientGame.LocalPlayer.Radio(RadioEnum.bear_coverme1);
				}
				else
				{
					Peer.ClientGame.LocalPlayer.Chat(Language.RadioCover1, ChatInfo.radio_message);
					Peer.ClientGame.LocalPlayer.Radio(RadioEnum.bear_coverme2);
				}
			}
			if (this.gui.Button(new Vector2(8f, 92f), null, this.gui.radioChat[4], null, Language.RadioAttention, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				this.FloodCounters();
				this.RadioOn();
				if (this.rnd.Next(2) == 0)
				{
					Peer.ClientGame.LocalPlayer.Chat(Language.RadioAttention0, ChatInfo.radio_message);
					Peer.ClientGame.LocalPlayer.Radio(RadioEnum.bear_eyesopen1);
				}
				else
				{
					Peer.ClientGame.LocalPlayer.Chat(Language.RadioAttention1, ChatInfo.radio_message);
					Peer.ClientGame.LocalPlayer.Radio(RadioEnum.bear_eyesopen2);
				}
			}
			if (this.gui.Button(new Vector2(38f, 116f), null, this.gui.radioChat[5], null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				this.FloodCounters();
				this.RadioOn();
				if (this.rnd.Next(2) == 0)
				{
					Peer.ClientGame.LocalPlayer.Chat(Language.RadioClear0, ChatInfo.radio_message);
					Peer.ClientGame.LocalPlayer.Radio(RadioEnum.bear_clear1);
				}
				else
				{
					Peer.ClientGame.LocalPlayer.Chat(Language.RadioClear1, ChatInfo.radio_message);
					Peer.ClientGame.LocalPlayer.Radio(RadioEnum.bear_clear2);
				}
			}
			if (this.gui.Button(new Vector2(129f, 117f), null, this.gui.radioChat[6], null, Language.RadioStop, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				this.FloodCounters();
				this.RadioOn();
				if (this.rnd.Next(2) == 0)
				{
					Peer.ClientGame.LocalPlayer.Chat(Language.RadioStop0, ChatInfo.radio_message);
					Peer.ClientGame.LocalPlayer.Radio(RadioEnum.bear_stop1);
				}
				else
				{
					Peer.ClientGame.LocalPlayer.Chat(Language.RadioStop1, ChatInfo.radio_message);
					Peer.ClientGame.LocalPlayer.Radio(RadioEnum.bear_stop2);
				}
			}
			if (this.gui.Button(new Vector2(200f, 116f), null, this.gui.radioChat[7], null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				this.FloodCounters();
				this.RadioOn();
				if (this.rnd.Next(2) == 0)
				{
					Peer.ClientGame.LocalPlayer.Chat(Language.RadioGood0, ChatInfo.radio_message);
					Peer.ClientGame.LocalPlayer.Radio(RadioEnum.bear_goodwork1);
				}
				else
				{
					Peer.ClientGame.LocalPlayer.Chat(Language.RadioGood1, ChatInfo.radio_message);
					Peer.ClientGame.LocalPlayer.Radio(RadioEnum.bear_goodwork2);
				}
			}
			if (this.gui.Button(new Vector2(197f, 92f), null, this.gui.radioChat[8], null, Language.RadioFollowMe, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				this.FloodCounters();
				this.RadioOn();
				if (this.rnd.Next(2) == 0)
				{
					Peer.ClientGame.LocalPlayer.Chat(Language.RadioFollowMe0, ChatInfo.radio_message);
					Peer.ClientGame.LocalPlayer.Radio(RadioEnum.bear_followme1);
				}
				else
				{
					Peer.ClientGame.LocalPlayer.Chat(Language.RadioFollowMe1, ChatInfo.radio_message);
					Peer.ClientGame.LocalPlayer.Radio(RadioEnum.bear_followme2);
				}
			}
			if (this.gui.Button(new Vector2(227f, 65f), null, this.gui.radioChat[9], null, Language.RadioHelp, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				this.FloodCounters();
				this.RadioOn();
				if (this.rnd.Next(2) == 0)
				{
					Peer.ClientGame.LocalPlayer.Chat(Language.RadioHelp0, ChatInfo.radio_message);
					Peer.ClientGame.LocalPlayer.Radio(RadioEnum.bear_needhelp);
				}
				else
				{
					Peer.ClientGame.LocalPlayer.Chat(Language.RadioHelp1, ChatInfo.radio_message);
					Peer.ClientGame.LocalPlayer.Radio(RadioEnum.bear_needhelp2);
				}
			}
			if (this.gui.Button(new Vector2(195f, 40f), null, this.gui.radioChat[10], null, Language.RadioCancel, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				this.FloodCounters();
				this.RadioOn();
				if (this.rnd.Next(2) == 0)
				{
					Peer.ClientGame.LocalPlayer.Chat(Language.RadioCancel0, ChatInfo.radio_message);
					Peer.ClientGame.LocalPlayer.Radio(RadioEnum.bear_negative1);
				}
				else
				{
					Peer.ClientGame.LocalPlayer.Chat(Language.RadioCancel1, ChatInfo.radio_message);
					Peer.ClientGame.LocalPlayer.Radio(RadioEnum.bear_negative2);
				}
			}
			this.gui.Button(new Vector2(60f, 94f), null, null, null, Language.RadioClear, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null);
			this.gui.Button(new Vector2(194f, 94f), null, null, null, Language.RadioGood, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null);
			this.gui.EndGroup();
		}
	}

	// Token: 0x06000A7B RID: 2683 RVA: 0x0007E564 File Offset: 0x0007C764
	public override void MainInitialize()
	{
		this.isGameHandler = true;
		this.isRendering = true;
		this.isUpdating = true;
		base.MainInitialize();
	}

	// Token: 0x06000A7C RID: 2684 RVA: 0x0007E584 File Offset: 0x0007C784
	public override void OnUpdate()
	{
		if (!CVars.g_radio)
		{
			return;
		}
		if (this.radioOn && this.currentTime + this.delay < Time.realtimeSinceStartup)
		{
			this.radioOn = false;
		}
		if (Input.GetKeyDown(Main.UserInfo.settings.binds.radio) && Main.IsTeamGame && !this.radioOn)
		{
			this.Show(0.1f, 0f);
		}
		if ((Input.GetKeyUp(Main.UserInfo.settings.binds.radio) || Input.GetKeyUp(KeyCode.Mouse0)) && base.visibility > 0.1f)
		{
			this.Hide(0.1f);
		}
		base.OnUpdate();
	}

	// Token: 0x04000C21 RID: 3105
	private System.Random rnd = new System.Random();

	// Token: 0x04000C22 RID: 3106
	private bool noAmmo;

	// Token: 0x04000C23 RID: 3107
	private bool radioOn;

	// Token: 0x04000C24 RID: 3108
	private float currentTime;

	// Token: 0x04000C25 RID: 3109
	private float delay = 1.5f;

	// Token: 0x04000C26 RID: 3110
	private eTimer floodTimer = new eTimer();

	// Token: 0x04000C27 RID: 3111
	private int floodCounter;

	// Token: 0x04000C28 RID: 3112
	private bool floodTimerOn;

	// Token: 0x04000C29 RID: 3113
	private int temp;
}
