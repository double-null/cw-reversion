using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x020000F8 RID: 248
[AddComponentMenu("Scripts/GUI/ChatGUI")]
internal class ChatGUI : Form
{
	// Token: 0x060006B1 RID: 1713 RVA: 0x0003C410 File Offset: 0x0003A610
	[Obfuscation(Exclude = true)]
	private void ChatMessage(object obj)
	{
		object[] array = (object[])obj;
		ChatInfo info = (ChatInfo)((int)array[0]);
		if (Main.UserInfo.settings.graphics.ShouldSwitchOffChat)
		{
			return;
		}
		ChatData chatData = new ChatData
		{
			info = info,
			nick = (string)array[1],
			msg = (string)array[2]
		};
		chatData.a.Once(5f);
		this.messages.Add(chatData);
	}

	// Token: 0x060006B2 RID: 1714 RVA: 0x0003C490 File Offset: 0x0003A690
	private float DrawMsg(ChatData info, float posY)
	{
		string text = info.nick + ":";
		string color = Colors.RadarWhiteWeb;
		string str = Colors.RadarWhiteWeb;
		switch (info.info)
		{
		case ChatInfo.enemy_message:
			color = Colors.RadarRedWeb;
			break;
		case ChatInfo.radio_message:
			color = Colors.RadarBlueWeb;
			this.bgradio = true;
			break;
		case ChatInfo.teammate_message:
			color = Colors.RadarBlueWeb;
			break;
		case ChatInfo.gameflow_message:
			text = info.nick;
			break;
		case ChatInfo.network_message:
			text = info.nick;
			str = Colors.RadarGrayWeb;
			color = Colors.RadarGrayWeb;
			break;
		case ChatInfo.notify_message:
			text = info.nick;
			str = Colors.RadarRedWeb;
			color = Colors.RadarRedWeb;
			break;
		case ChatInfo.radio_message_at_hit:
			color = Colors.RadarBlueWeb;
			this.bgradio = true;
			break;
		}
		float num = this.gui.CalcHeight(text + info.msg, 185f, this.gui.fontTahoma, 10);
		this.gui.color = new Color(1f, 1f, 1f, info.a.visibility);
		this.gui.BeginGroup(new Rect(0f, posY, 1024f, num));
		if (this.bgradio)
		{
			this.gui.PictureSized(Vector2.zero, this.backgroundRadio, new Vector2((float)(this.backgroundRadio.width - 17), 15f));
			this.bgradio = false;
		}
		else
		{
			this.gui.PictureSized(Vector2.zero, this.background75, new Vector2((float)(this.background75.width - 17), num));
		}
		this.gui.TextLabel(new Rect(2f, -1f, 185f, num), Helpers.ColoredText(text, color) + " " + info.msg, 10, str + "_Tahoma", TextAnchor.UpperLeft, true);
		this.gui.EndGroup();
		return num;
	}

	// Token: 0x170000D5 RID: 213
	// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0003C69C File Offset: 0x0003A89C
	public override Rect Rect
	{
		get
		{
			return new Rect(13f, 265f, 1024f, 1024f);
		}
	}

	// Token: 0x060006B4 RID: 1716 RVA: 0x0003C6B8 File Offset: 0x0003A8B8
	public override void OnInitialized()
	{
		this.isGameHandler = true;
		this.isRendering = true;
		this.isUpdating = true;
		base.MainInitialize();
	}

	// Token: 0x060006B5 RID: 1717 RVA: 0x0003C6D8 File Offset: 0x0003A8D8
	public override void Register()
	{
		EventFactory.Register("ChatMessage", this);
	}

	// Token: 0x060006B6 RID: 1718 RVA: 0x0003C6E8 File Offset: 0x0003A8E8
	public override void GameGUI()
	{
		if (SingletoneForm<global::Console>.Instance.Visible || this.gui.Visible)
		{
			return;
		}
		if (Main.UserInfo.settings.graphics.HideInterface)
		{
			return;
		}
		this.gui.BeginGroup(this.Rect, false);
		float num = 0f;
		if (this.chatVisible)
		{
			num += this.gui.CalcHeight(this.msg, 155f, this.gui.fontTahoma, 10);
			this.gui.color = new Color(1f, 1f, 1f, 1f);
			this.gui.PictureSized(Vector2.zero, this.background, new Vector2((float)(this.background.width - 17), num));
			this.gui.textMaxSize = 100;
			string str = (!this.talkAll) ? Language.ChToTeam : Language.ChToAll;
			Rect rect = (!this.talkAll) ? new Rect(50f, -1f, 135f, num) : new Rect(30f, -1f, 155f, num);
			this.gui.TextLabel(new Rect(2f, -1f, 155f, 30f), str + ": ", 10, Colors.RadarYellowWeb + "_Tahoma", TextAnchor.UpperLeft, true);
			GUI.SetNextControlName("chat");
			this.msg = this.gui.TextField(rect, this.msg, 10, Colors.RadarWhiteWeb + "_Tahoma", TextAnchor.UpperLeft, true, false);
			GUI.FocusControl("chat");
			this.gui.textMaxSize = 16;
		}
		for (int i = 0; i < this.messages.Count; i++)
		{
			num += this.DrawMsg(this.messages[this.messages.Count - i - 1], num + 2f) + 2f;
		}
		this.gui.EndGroup();
	}

	// Token: 0x060006B7 RID: 1719 RVA: 0x0003C910 File Offset: 0x0003AB10
	public override void PreGUI()
	{
		if (SingletoneForm<global::Console>.Instance.Visible || this.gui.Visible || Main.UserInfo.userID == IDUtil.GuestID)
		{
			return;
		}
		if (!CVars.g_chatEnabled || Main.UserInfo.repa < (float)CVars.g_chatRepaRequired)
		{
			return;
		}
		for (int i = 0; i < this.messages.Count; i++)
		{
			if (!this.messages[i].a.Visible)
			{
				this.messages.RemoveAt(i);
				break;
			}
		}
		bool shouldSwitchOffChat = Main.UserInfo.settings.graphics.ShouldSwitchOffChat;
		if (this.chatVisible)
		{
			if (!this.gui.GetKeyDown(KeyCode.Return))
			{
				return;
			}
			if (this.msg != string.Empty)
			{
				Peer.ClientGame.LocalPlayer.Chat(this.msg, (!this.talkAll) ? ChatInfo.teammate_message : ChatInfo.enemy_message);
			}
			this.Hide(0.35f);
			this.chatVisible = false;
			this.msg = string.Empty;
		}
		else if (!shouldSwitchOffChat)
		{
			if (this.gui.GetKeyDown(Main.Binds.talkAll) || this.gui.GetKeyDown(KeyCode.Return))
			{
				this.Show(0.5f, 0f);
				this.chatVisible = true;
				this.talkAll = true;
				this.msg = string.Empty;
			}
			else if (this.gui.GetKeyDown(Main.Binds.talkTeam) || this.gui.GetKeyDown(KeyCode.Return))
			{
				this.talkAll = !Main.IsTeamGame;
				this.Show(0.5f, 0f);
				this.chatVisible = true;
				this.msg = string.Empty;
			}
		}
	}

	// Token: 0x0400073C RID: 1852
	public Texture2D background;

	// Token: 0x0400073D RID: 1853
	public Texture2D background75;

	// Token: 0x0400073E RID: 1854
	public Texture2D backgroundRadio;

	// Token: 0x0400073F RID: 1855
	[HideInInspector]
	public bool chatVisible;

	// Token: 0x04000740 RID: 1856
	private bool talkAll;

	// Token: 0x04000741 RID: 1857
	private bool bgradio;

	// Token: 0x04000742 RID: 1858
	private List<ChatData> messages = new List<ChatData>();

	// Token: 0x04000743 RID: 1859
	private string msg = string.Empty;
}
