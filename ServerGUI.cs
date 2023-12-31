using System;
using System.Reflection;
using UnityEngine;

// Token: 0x0200017E RID: 382
[AddComponentMenu("Scripts/GUI/ServerGUI")]
internal class ServerGUI : Form
{
	// Token: 0x1700014D RID: 333
	// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x00084418 File Offset: 0x00082618
	private int localMapIndex
	{
		get
		{
			return (Main.HostInfo.MapIndex != -1) ? Main.HostInfo.MapIndex : 0;
		}
	}

	// Token: 0x06000AE4 RID: 2788 RVA: 0x00084448 File Offset: 0x00082648
	[Obfuscation(Exclude = true)]
	private void ShowCreateServer(object obj)
	{
	}

	// Token: 0x06000AE5 RID: 2789 RVA: 0x0008444C File Offset: 0x0008264C
	[Obfuscation(Exclude = true)]
	private void HideCreateServer(object obj)
	{
		this.Hide(0.35f);
	}

	// Token: 0x1700014E RID: 334
	// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x0008445C File Offset: 0x0008265C
	public override Rect Rect
	{
		get
		{
			return new Rect((float)(Screen.width / 2 - this.gui.settings_window[8].width / 2), (float)(Screen.height / 2 - this.gui.settings_window[8].height / 2), (float)this.gui.settings_window[8].width, (float)(this.gui.settings_window[8].height + 100));
		}
	}

	// Token: 0x06000AE7 RID: 2791 RVA: 0x000844D4 File Offset: 0x000826D4
	public override void MainInitialize()
	{
		this.isRendering = true;
		base.MainInitialize();
	}

	// Token: 0x06000AE8 RID: 2792 RVA: 0x000844E4 File Offset: 0x000826E4
	public override void Clear()
	{
		base.Clear();
		this.mapScrollPos = Vector2.zero;
		this.typeScrollPos = Vector2.zero;
		this.tempIndex = 0;
	}

	// Token: 0x06000AE9 RID: 2793 RVA: 0x0008450C File Offset: 0x0008270C
	public override void Register()
	{
		EventFactory.Register("ShowCreateServer", this);
		EventFactory.Register("HideCreateServer", this);
	}

	// Token: 0x06000AEA RID: 2794 RVA: 0x00084524 File Offset: 0x00082724
	public override void InterfaceGUI()
	{
		if (Peer.Info == null)
		{
			return;
		}
		this.gui.color = new Color(1f, 1f, 1f, base.visibility);
		Rect rect = new Rect((float)(Screen.width / 2 - this.gui.settings_window[8].width / 2), (float)(Screen.height / 2 - this.gui.settings_window[8].height / 2), (float)this.gui.settings_window[8].width, (float)(this.gui.settings_window[8].height * 2));
		this.gui.BeginGroup(rect, this.windowID != this.gui.FocusedWindow || this.typeDropDown || this.mapDropDown);
		this.gui.Picture(new Vector2(0f, 0f), this.gui.settings_window[8]);
		if (this.gui.Button(new Vector2(335f, 22f), this.gui.server_window[3], this.gui.server_window[4], null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			this.Hide(0.35f);
			Peer.Info = null;
			return;
		}
		this.gui.TextField(new Rect(35f, 13f, (float)this.gui.weapon_info.width, 50f), Language.ServerGUICreatingServer, 18, "#000000", TextAnchor.MiddleLeft, false, false);
		this.gui.TextField(new Rect(35f, 12f, (float)this.gui.weapon_info.width, 50f), Language.ServerGUICreatingServer, 18, "#FFFFFF", TextAnchor.MiddleLeft, false, false);
		if (this.gui.Button(new Vector2(140f, 153f), this.gui.mainMenuButtons[0], this.gui.mainMenuButtons[1], this.gui.mainMenuButtons[1], Language.ServerGUICreate, 15, "#adb0af", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			EventFactory.Call("HideSearchGames", null);
			this.Hide(0.35f);
			Peer.IsHost = true;
			Peer.CreateServer();
		}
		this.gui.TextField(new Rect(30f, 60f, 150f, 30f), Language.ServerGUIName + ": ", 16, "#FFFFFF", TextAnchor.UpperLeft, false, false);
		this.gui.Picture(new Vector2(90f, 60f), this.servername_field);
		this.gui.textMaxSize = 24;
		Main.HostInfo.Name = this.gui.TextField(new Rect(110f, 60f, 160f, 25f), Main.HostInfo.Name, 16, "#FFFFFF", TextAnchor.UpperLeft, true, true);
		this.gui.textMaxSize = 16;
		if (Main.HostInfo.Name == "New Game")
		{
			Main.HostInfo.Name = Main.UserInfo.nick + "'i game";
		}
		this.gui.BeginGroup(new Rect(280f, 60f, 400f, 250f), this.windowID != this.gui.FocusedWindow || this.typeDropDown || this.mapDropDown);
		this.gui.TextField(new Rect(10f, 0f, 150f, 30f), Language.ServerGUIPlayersCount + ": ", 16, "#FFFFFF", TextAnchor.UpperLeft, false, false);
		this.gui.Picture(new Vector2(100f, 0f), this.playercount_fieldl);
		try
		{
			Main.HostInfo.MaxPlayers = Convert.ToInt32(this.gui.TextField(new Rect(105f, 0f, 100f, 30f), Main.HostInfo.MaxPlayers, 16, "#FFFFFF", TextAnchor.UpperLeft, true, true));
			Main.HostInfo.MaxPlayers = Mathf.Clamp(Main.HostInfo.MaxPlayers, 0, 30);
		}
		catch (Exception e)
		{
			global::Console.exception(e);
		}
		this.gui.EndGroup();
		this.gui.BeginGroup(new Rect(80f, 120f, 800f, 600f), this.windowID != this.gui.FocusedWindow || this.mapDropDown);
		this.gui.TextField(new Rect(0f, 0f, 150f, 30f), Language.ServerGUIGameMode + ": ", 16, "#FFFFFF", TextAnchor.UpperLeft, false, false);
		this.DropDown<GameMode>(new Vector2(60f, 0f), ref this.typeScrollPos, ref this.typeDropDown, Globals.I.maps[this.localMapIndex].Modes.ToArray(), ref this.tempIndex);
		if (this.tempIndex != -1)
		{
			Peer.Info.GameMode = Globals.I.maps[this.localMapIndex].Modes[this.tempIndex];
		}
		this.gui.EndGroup();
		this.gui.BeginGroup(new Rect(80f, 90f, 800f, 600f), this.windowID != this.gui.FocusedWindow || this.typeDropDown);
		this.gui.TextField(new Rect(0f, 0f, 150f, 30f), Language.Map + ": ", 16, "#FFFFFF", TextAnchor.UpperLeft, false, false);
		int mapIndex = Main.HostInfo.MapIndex;
		this.DropDown<string>(new Vector2(60f, 0f), ref this.mapScrollPos, ref this.mapDropDown, Globals.I.mapNames, ref mapIndex);
		Main.HostInfo.MapIndex = mapIndex;
		this.gui.EndGroup();
		this.gui.EndGroup();
	}

	// Token: 0x06000AEB RID: 2795 RVA: 0x00084BD4 File Offset: 0x00082DD4
	public void DropDown<T>(Vector2 pos, ref Vector2 resScrollPos, ref bool dropDown, T[] array, ref int index)
	{
		if (index >= array.Length)
		{
			index = 0;
		}
		if (this.gui.Button(pos, this.gui.mainMenuButtons[0], this.gui.mainMenuButtons[1], this.gui.mainMenuButtons[1], array[index].ToString(), 17, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			dropDown = !dropDown;
		}
		if (!dropDown)
		{
			this.gui.Picture(new Vector2(pos.x + 165f, pos.y + 8f), this.gui.settings_window[2]);
		}
		if (dropDown)
		{
			this.gui.Picture(new Vector2(pos.x - 5f, pos.y + (float)this.gui.mainMenuButtons[0].height), this.gui.settings_window[4]);
			if (resScrollPos.y > (float)(array.Length * (this.gui.mainMenuButtons[0].height + 5) - 112))
			{
				resScrollPos.y = (float)(array.Length * (this.gui.mainMenuButtons[0].height + 5) - 112);
			}
			this.gui.BeginGroup(new Rect(0f, 0f, 4096f, 4096f), !dropDown);
			if (array.Length > 4)
			{
				this.gui.Picture(new Vector2(pos.x + 200f, pos.y + (float)this.gui.mainMenuButtons[0].height + 3f), this.gui.settings_window[7]);
				resScrollPos = this.gui.BeginScrollView(new Rect(pos.x - 5f, pos.y + (float)this.gui.mainMenuButtons[0].height + 4f, (float)(this.gui.settings_window[4].width - 5), (float)(this.gui.settings_window[4].height - 8)), resScrollPos, new Rect(0f, 0f, (float)(this.gui.settings_window[4].width - 25), (float)(array.Length * (this.gui.mainMenuButtons[0].height + 5) + 40)), float.MaxValue);
			}
			else
			{
				this.gui.BeginGroup(new Rect(pos.x - 5f, pos.y + (float)this.gui.mainMenuButtons[0].height + 4f, (float)(this.gui.settings_window[4].width - 5), (float)(this.gui.settings_window[4].height - 8)));
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (this.gui.Button(new Vector2(4f, (float)(0 + (this.gui.mainMenuButtons[0].height + 5) * i + 3)), null, this.gui.mainMenuButtons[1], this.gui.mainMenuButtons[1], array[i].ToString(), 17, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
				{
					dropDown = false;
					index = i;
				}
			}
			if (array.Length > 4)
			{
				this.gui.EndScrollView();
			}
			else
			{
				this.gui.EndGroup();
			}
			this.gui.EndGroup();
			if (!this.gui.inRect(new Rect(pos.x - 5f, pos.y + (float)this.gui.mainMenuButtons[0].height + 4f, (float)(this.gui.settings_window[4].width - 5), (float)(this.gui.settings_window[4].height - 8)), this.gui.upper, this.gui.cursorPosition) && dropDown && Input.GetMouseButtonDown(0))
			{
				dropDown = false;
			}
		}
	}

	// Token: 0x04000CEB RID: 3307
	public Texture2D servername_field;

	// Token: 0x04000CEC RID: 3308
	public Texture2D playercount_fieldl;

	// Token: 0x04000CED RID: 3309
	private Vector2 typeScrollPos = Vector2.zero;

	// Token: 0x04000CEE RID: 3310
	private Vector2 mapScrollPos = Vector2.zero;

	// Token: 0x04000CEF RID: 3311
	private int tempIndex;

	// Token: 0x04000CF0 RID: 3312
	private bool mapDropDown;

	// Token: 0x04000CF1 RID: 3313
	private bool typeDropDown;
}
