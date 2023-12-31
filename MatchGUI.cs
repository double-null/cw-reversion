using System;
using System.Reflection;
using UnityEngine;

// Token: 0x02000165 RID: 357
[AddComponentMenu("Scripts/GUI/MatchGUI")]
internal class MatchGUI : Form
{
	// Token: 0x0600098D RID: 2445 RVA: 0x0006535C File Offset: 0x0006355C
	public bool isBannerShowed()
	{
		return this.bBlinkIsShow;
	}

	// Token: 0x0600098E RID: 2446 RVA: 0x00065364 File Offset: 0x00063564
	[Obfuscation(Exclude = true)]
	public void BannerShowed(object obj)
	{
		this.bBlinkIsShow = true;
	}

	// Token: 0x0600098F RID: 2447 RVA: 0x00065370 File Offset: 0x00063570
	[Obfuscation(Exclude = true)]
	private void Blink(object obj)
	{
		this.bBlinkIsShow = true;
		Vector3 vector = (Vector3)obj;
		this.blink_position = vector;
		this.blinkGNAME.InitTimer(this.blink_alphaKeys[this.blink_alphaKeys.Length - 1].time);
	}

	// Token: 0x06000990 RID: 2448 RVA: 0x000653B8 File Offset: 0x000635B8
	[Obfuscation(Exclude = true)]
	private void ShowRoundEnd(object obj)
	{
		this.bearWins = (bool)obj;
		this.victoryA.Show(0.5f, 0f);
		if (this.bearWins)
		{
			Audio.Play(Vector3.zero, this.bearWin, -1f, -1f);
		}
		else
		{
			Audio.Play(Vector3.zero, this.UsecWin, -1f, -1f);
		}
	}

	// Token: 0x06000991 RID: 2449 RVA: 0x0006542C File Offset: 0x0006362C
	public override void MainInitialize()
	{
		this.isRendering = true;
		base.MainInitialize();
		this.blink_alphaKeys = this.blink_alpha.keys;
	}

	// Token: 0x06000992 RID: 2450 RVA: 0x0006544C File Offset: 0x0006364C
	public override void OnConnected()
	{
		base.OnConnected();
		this.Clear();
	}

	// Token: 0x06000993 RID: 2451 RVA: 0x0006545C File Offset: 0x0006365C
	public override void OnDisconnect()
	{
		base.OnDisconnect();
		this.Clear();
	}

	// Token: 0x06000994 RID: 2452 RVA: 0x0006546C File Offset: 0x0006366C
	public override void Clear()
	{
		base.Clear();
		this.bBlinkIsShow = false;
		this.blink_position = Vector3.zero;
		this.blinkGNAME = new GraphicValue();
		this.victoryA = new Alpha();
		this.bearWins = false;
		if (base.audio)
		{
			base.audio.Stop();
		}
	}

	// Token: 0x06000995 RID: 2453 RVA: 0x000654CC File Offset: 0x000636CC
	public override void Register()
	{
		EventFactory.Register("Blink", this);
		EventFactory.Register("BannerShowed", this);
		EventFactory.Register("ShowRoundEnd", this);
	}

	// Token: 0x06000996 RID: 2454 RVA: 0x000654F0 File Offset: 0x000636F0
	public override void OnSpawn()
	{
		this.Clear();
	}

	// Token: 0x06000997 RID: 2455 RVA: 0x000654F8 File Offset: 0x000636F8
	public override void OnDie()
	{
		this.Clear();
	}

	// Token: 0x06000998 RID: 2456 RVA: 0x00065500 File Offset: 0x00063700
	public override void OnRoundStart()
	{
		this.Clear();
	}

	// Token: 0x06000999 RID: 2457 RVA: 0x00065508 File Offset: 0x00063708
	public override void OnRoundEnd()
	{
	}

	// Token: 0x0600099A RID: 2458 RVA: 0x0006550C File Offset: 0x0006370C
	public override void OnMatchStart()
	{
		this.Clear();
	}

	// Token: 0x0600099B RID: 2459 RVA: 0x00065514 File Offset: 0x00063714
	public override void GameGUI()
	{
		if (this.blinkGNAME.Exist() && CameraListener.Camera)
		{
			Vector2 pos = CameraListener.Camera.WorldToScreenPoint(this.blink_position);
			this.gui.color = new Color(1f, 1f, 1f, this.blink_alpha.Evaluate(this.blinkGNAME.Get()));
			this.gui.PictureCentered(pos, this.blink, Vector2.one * this.blink_scale.Evaluate(this.blinkGNAME.Get()));
			this.gui.color = new Color(1f, 1f, 1f, this.crashedWindow_alpha.Evaluate(this.blinkGNAME.Get()));
			GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), this.crashedWindow, ScaleMode.StretchToFill, true);
		}
		if (this.victoryA.Visible)
		{
			this.gui.color = new Color(1f, 1f, 1f, this.victoryA.visibility);
			Rect rect = new Rect((float)(Screen.width / 2 - this.gui.Width / 2), (float)(Screen.height / 2 - this.gui.Height / 2), (float)this.gui.Width, (float)this.gui.Height);
			this.gui.BeginGroup(rect, false);
			this.gui.color = new Color(1f, 1f, 1f, this.victoryA.visibility);
			if (this.bearWins)
			{
				this.gui.Picture(new Vector2(296f, 74f), this.bearSelected);
				this.TextMatchResult(Language.SpecBEARWins, "#FFFFFF", ref this.gui.fontDNC57, 55);
			}
			else
			{
				this.gui.Picture(new Vector2(278f, 117f), this.usecSelected);
				this.TextMatchResult(Language.SpecUSECWins, "#FFFFFF", ref this.gui.fontDNC57, 55);
			}
			if (!Main.IsPlayerSpectactor)
			{
				if (Peer.ClientGame.LocalPlayer.IsTeam(this.bearWins))
				{
					this.TextMatchResult(Language.SpecWin, "#6daf07_Micra", ref this.gui.fontMicra, 75);
				}
				else
				{
					this.TextMatchResult(Language.SpecLoose, "#ad0505_Micra", ref this.gui.fontMicra, 75);
				}
			}
			this.gui.EndGroup();
		}
	}

	// Token: 0x0600099C RID: 2460 RVA: 0x000657D0 File Offset: 0x000639D0
	private void TextMatchResult(string str, string strcolor, ref Font font, int y = 55)
	{
		this.gui.TextField(new Rect(((float)this.gui.Width - this.gui.CalcWidth(str, font, 20)) / 2f, (float)(this.gui.Height / 2 + y), this.gui.CalcWidth(str, font, 20), 50f), str, 20, strcolor, TextAnchor.UpperLeft, false, false);
	}

	// Token: 0x04000B0A RID: 2826
	public Texture2D blink;

	// Token: 0x04000B0B RID: 2827
	public Texture2D crashedWindow;

	// Token: 0x04000B0C RID: 2828
	public Texture2D usecSelected;

	// Token: 0x04000B0D RID: 2829
	public Texture2D bearSelected;

	// Token: 0x04000B0E RID: 2830
	public AnimationCurve blink_alpha;

	// Token: 0x04000B0F RID: 2831
	public AnimationCurve crashedWindow_alpha;

	// Token: 0x04000B10 RID: 2832
	public AnimationCurve blink_scale;

	// Token: 0x04000B11 RID: 2833
	public AudioClip bearWin;

	// Token: 0x04000B12 RID: 2834
	public AudioClip UsecWin;

	// Token: 0x04000B13 RID: 2835
	private Keyframe[] blink_alphaKeys;

	// Token: 0x04000B14 RID: 2836
	private Vector3 blink_position = Vector3.zero;

	// Token: 0x04000B15 RID: 2837
	private GraphicValue blinkGNAME = new GraphicValue();

	// Token: 0x04000B16 RID: 2838
	private bool bBlinkIsShow;

	// Token: 0x04000B17 RID: 2839
	private Alpha victoryA = new Alpha();

	// Token: 0x04000B18 RID: 2840
	private bool bearWins;
}
