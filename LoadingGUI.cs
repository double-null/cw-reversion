using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x02000156 RID: 342
[AddComponentMenu("Scripts/GUI/LoadingGUI")]
internal class LoadingGUI : Form
{
	// Token: 0x06000874 RID: 2164 RVA: 0x0004CBD8 File Offset: 0x0004ADD8
	[Obfuscation(Exclude = true)]
	private void ShowLoading(object obj)
	{
		if (Main.UserInfo.settings.graphics.EnableFullScreenInBattle && !Screen.fullScreen)
		{
			Utility.SetResolution(Main.UserInfo.settings.resolution.width, Main.UserInfo.settings.resolution.height, true);
		}
		base.InvokeRepeating("LoadNewHint", 10f, 10f);
		this.Show(0.5f, 0f);
		base.StartCoroutine(this.downloadBG());
		this.backgroundAlpha.Show(0f, 0f);
	}

	// Token: 0x06000875 RID: 2165 RVA: 0x0004CC80 File Offset: 0x0004AE80
	[Obfuscation(Exclude = true)]
	private void HideLoading(object obj)
	{
		this.backgroundAlpha.Show(1f, 0f);
		base.CancelInvoke("LoadNewHint");
		this.Hide(0.35f);
	}

	// Token: 0x06000876 RID: 2166 RVA: 0x0004CCB0 File Offset: 0x0004AEB0
	private IEnumerator downloadBG()
	{
		string url = WWWUtil.levelsplashWWW(((Maps)Main.HostInfo.MapIndex).ToString());
		string hcurl = WWWUtil.levelsplashWWW("hardcore").ToString();
		WWW www = new WWW(url);
		WWW hcwww = new WWW(hcurl);
		yield return www;
		yield return hcwww;
		if (Peer.HardcoreMode && hcwww != null && hcwww.error == null)
		{
			yield return new WaitForSeconds(1f);
			this.loadedBG = hcwww.texture;
			this.isLoaded = true;
			this.textureAlpha1.Show(1f, 0f);
			yield return new WaitForSeconds(5f);
			this.textureAlpha1.Hide(1f, 0f);
			hcwww.Dispose();
			hcwww = null;
		}
		if (www != null && www.error == null)
		{
			yield return new WaitForSeconds(1f);
			this.loadedBG = www.texture;
			this.isLoaded = true;
			this.textureAlpha1.Show(1f, 0f);
			yield return new WaitForSeconds(2f);
			www.Dispose();
			www = null;
		}
		yield break;
	}

	// Token: 0x06000877 RID: 2167 RVA: 0x0004CCCC File Offset: 0x0004AECC
	[Obfuscation(Exclude = true)]
	private void LoadNewHint()
	{
		this.randomHintIndex = (int)((float)UnityEngine.Random.Range(0, Globals.I.hints.Length) + 0.5f);
	}

	// Token: 0x06000878 RID: 2168 RVA: 0x0004CCFC File Offset: 0x0004AEFC
	public override void MainInitialize()
	{
		LoadingGUI.I = this;
		this.isRendering = true;
		base.MainInitialize();
	}

	// Token: 0x06000879 RID: 2169 RVA: 0x0004CD14 File Offset: 0x0004AF14
	public override void Register()
	{
		EventFactory.Register("ShowLoading", this);
		EventFactory.Register("HideLoading", this);
	}

	// Token: 0x0600087A RID: 2170 RVA: 0x0004CD2C File Offset: 0x0004AF2C
	public override void LateGUI()
	{
		if (Main.IsGameLoading)
		{
			this.GameGUI();
		}
	}

	// Token: 0x0600087B RID: 2171 RVA: 0x0004CD40 File Offset: 0x0004AF40
	public override void GameGUI()
	{
		if (!base.Visible && !this.backgroundAlpha.Visible)
		{
			return;
		}
		if (Main.HostInfo == null)
		{
			return;
		}
		this.gui.color = new Color(1f, 1f, 1f, this.backgroundAlpha.visibility);
		GUI.DrawTexture(new Rect(-10f, -10f, (float)(Screen.width + 20), (float)(Screen.height + 20)), this.gui.black);
		if (this.isLoaded)
		{
			Rect source = new Rect((float)(Screen.width / 2 - this.loadedBG.width / 2), (float)(Screen.height / 2 - this.loadedBG.height / 2), (float)this.loadedBG.width, (float)this.loadedBG.height);
			this.gui.BeginGroup(new Rect(source));
			this.gui.Picture(new Vector2(0f, 0f), this.loadedBG);
			this.gui.EndGroup();
		}
		Rect rect = new Rect((float)(Screen.width / 2 - this.gui.Width / 2), (float)(Screen.height / 2 - this.gui.Height / 2), (float)this.gui.Width, (float)this.gui.Height);
		this.gui.BeginGroup(rect, false);
		if (this.connected)
		{
			this.gui.Picture(new Vector2(649f, 506f), this.mouseIcon);
			GUI.Label(new Rect(649f, 507f, (float)this.mouseIcon.width, (float)this.mouseIcon.height), Language.Ready.ToUpper() + "!", this.readyStyleBg);
			GUI.Label(new Rect(649f, 506f, (float)this.mouseIcon.width, (float)this.mouseIcon.height), Language.Ready.ToUpper() + "!", this.readyStyle);
			if (Input.GetMouseButtonDown(0) && base.MaxVisible && SingletoneForm<Loader>.Instance.IsGameLoaded)
			{
				this.Hide(0.35f);
				this.backgroundAlpha.Hide(0.5f, 0f);
				base.CancelInvoke("LoadNewHint");
				SingletoneForm<Loader>.Instance.IsGameLoadedAndClicked = true;
				if (Peer.ClientGame.IsTeamGame)
				{
					EventFactory.Call("ShowTeamChoose", 1f);
					SpectactorGUI.I.teamChoosing = true;
				}
				SpectactorGUI.I.timeFirstSpawn = Time.realtimeSinceStartup + 0.3f;
			}
		}
		else
		{
			this.angle = 180f * Time.realtimeSinceStartup * 1.5f;
			this.gui.RotateGUI(this.angle, new Vector2((float)(670 + this.gui.settings_window[9].width / 2), (float)(505 + this.gui.settings_window[9].height / 2)));
			this.gui.Picture(new Vector2(670f, 505f), this.gui.settings_window[9]);
			this.gui.RotateGUI(0f, Vector2.zero);
			int num = (this.ToDownload != 0) ? ((int)((float)this.DownloadProgress / (float)this.ToDownload * 100f)) : 100;
			this.gui.TextField(new Rect(562f, 522f, 100f, 50f), num + "%", 11, "#b1b0b1_Micra", TextAnchor.UpperRight, false, false);
			if (CVars.n_httpDebug && (Main.UserInfo.userID == -13 || Network.player.ipAddress == "192.168.1.5" || Main.UserInfo.Permission >= EPermission.Admin))
			{
				List<Downloader> downloadersForLoadingGui = SingletoneForm<Loader>.Instance.DownloadersForLoadingGui;
				this.ToDownload = 0;
				this.DownloadProgress = 0;
				for (int i = 0; i < downloadersForLoadingGui.Count; i++)
				{
					this.gui.TextField(new Rect(470f, 370f - (float)i * 31f + 1f, 300f, 30f), downloadersForLoadingGui[i].FileNameWithoutExtension, 16, "#000000", TextAnchor.MiddleLeft, false, false);
					this.gui.TextField(new Rect(470f, 370f - (float)i * 31f, 300f, 30f), downloadersForLoadingGui[i].FileNameWithoutExtension, 16, "#11FF11", TextAnchor.MiddleLeft, false, false);
					this.ToDownload += (int)(downloadersForLoadingGui[i].FileSize / 1024f);
					this.DownloadProgress += (int)(downloadersForLoadingGui[i].DownloadedSize / 1024f);
					if (!downloadersForLoadingGui[i].Finished)
					{
						this.gui.TextField(new Rect(470f, 370f - (float)i * 31f + 1f, 300f, 30f), string.Concat(new object[]
						{
							(int)(downloadersForLoadingGui[i].DownloadedSize / 1024f),
							"/",
							(int)(downloadersForLoadingGui[i].FileSize / 1024f),
							" kb at ",
							(int)(downloadersForLoadingGui[i].RatePerSecond / 1024f),
							" kbps"
						}), 16, "#000000", TextAnchor.MiddleRight, false, false);
						this.gui.TextField(new Rect(470f, 370f - (float)i * 31f, 300f, 30f), string.Concat(new object[]
						{
							(int)(downloadersForLoadingGui[i].DownloadedSize / 1024f),
							"/",
							(int)(downloadersForLoadingGui[i].FileSize / 1024f),
							" kb at ",
							(int)(downloadersForLoadingGui[i].RatePerSecond / 1024f),
							" kbps"
						}), 16, "#11FF11", TextAnchor.MiddleRight, false, false);
					}
				}
				this.gui.TextField(new Rect(562f, 520f, 100f, 50f), string.Concat(new object[]
				{
					this.DownloadProgress,
					" / ",
					this.ToDownload,
					" KB"
				}), 10, "#b1b0b1_Tahoma", TextAnchor.MiddleRight, false, false);
			}
			else
			{
				List<Downloader> downloadersForLoadingGui2 = SingletoneForm<Loader>.Instance.DownloadersForLoadingGui;
				this.ToDownload = 0;
				this.DownloadProgress = 0;
				for (int j = 0; j < downloadersForLoadingGui2.Count; j++)
				{
					this.ToDownload += (int)(downloadersForLoadingGui2[j].FileSize / 1024f);
					this.DownloadProgress += (int)(downloadersForLoadingGui2[j].DownloadedSize / 1024f);
				}
				this.gui.TextField(new Rect(562f, 520f, 100f, 50f), string.Concat(new object[]
				{
					this.DownloadProgress,
					" / ",
					this.ToDownload,
					" KB"
				}), 10, "#b1b0b1_Tahoma", TextAnchor.MiddleRight, false, false);
			}
			if (SingletoneForm<Loader>.Instance.CanBeCanceled && this.gui.Button(new Vector2(692f, 23f), this.gui.server_window[3], this.gui.server_window[4], null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				Peer.Disconnect(true);
				this.isLoaded = false;
				EventFactory.Call("ShowInterface", null);
			}
		}
		this.gui.TextField(new Rect(43f, 506f, 540f, 49f), Globals.I.hints[this.randomHintIndex], 18, "#ffffff", TextAnchor.UpperLeft, false, false);
		this.gui.EndGroup();
	}

	// Token: 0x0600087C RID: 2172 RVA: 0x0004D610 File Offset: 0x0004B810
	public override void OnConnected()
	{
		this.connected = true;
	}

	// Token: 0x0600087D RID: 2173 RVA: 0x0004D61C File Offset: 0x0004B81C
	public override void OnDisconnect()
	{
		this.connected = false;
		this.isLoaded = false;
		this.Hide(0.35f);
	}

	// Token: 0x04000990 RID: 2448
	public static LoadingGUI I;

	// Token: 0x04000991 RID: 2449
	public Texture2D mouseIcon;

	// Token: 0x04000992 RID: 2450
	public GUIStyle readyStyle = new GUIStyle();

	// Token: 0x04000993 RID: 2451
	public GUIStyle readyStyleBg = new GUIStyle();

	// Token: 0x04000994 RID: 2452
	public Texture2D loadedBG;

	// Token: 0x04000995 RID: 2453
	public bool isLoaded;

	// Token: 0x04000996 RID: 2454
	private Alpha textureAlpha1 = new Alpha();

	// Token: 0x04000997 RID: 2455
	private float angle;

	// Token: 0x04000998 RID: 2456
	private bool connected;

	// Token: 0x04000999 RID: 2457
	private int randomHintIndex;

	// Token: 0x0400099A RID: 2458
	private int ToDownload;

	// Token: 0x0400099B RID: 2459
	private int DownloadProgress;

	// Token: 0x0400099C RID: 2460
	private Alpha backgroundAlpha = new Alpha();
}
