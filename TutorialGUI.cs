using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x0200019B RID: 411
[AddComponentMenu("Scripts/GUI/TutorialGUI")]
internal class TutorialGUI : Form
{
	// Token: 0x06000B9B RID: 2971 RVA: 0x00090E24 File Offset: 0x0008F024
	[Obfuscation(Exclude = true)]
	private void ShowTutorialGUI(object[] obj)
	{
		this.Clear();
		this.Show(0.5f, 0f);
	}

	// Token: 0x06000B9C RID: 2972 RVA: 0x00090E3C File Offset: 0x0008F03C
	[Obfuscation(Exclude = true)]
	private void HideTutorialGUI(object obj)
	{
		this.Hide(0.35f);
	}

	// Token: 0x06000B9D RID: 2973 RVA: 0x00090E4C File Offset: 0x0008F04C
	[Obfuscation(Exclude = true)]
	private void TrainingTrigger(object obj)
	{
		TrainingTrigger item = (TrainingTrigger)obj;
		if (!this.triggers.Contains(item))
		{
			if (base.Visible)
			{
				this.Hide(0.35f);
				if (this.triggers.Count > 0)
				{
					for (int i = 0; i < this.triggers[this.triggers.Count - 1].events.Length; i++)
					{
						this.triggers[this.triggers.Count - 1].events[i].SetTaskEnd();
					}
				}
			}
			else
			{
				this.Show(0.5f, 0f);
				this.trigger = item;
				this.triggers.Add(item);
			}
		}
	}

	// Token: 0x17000159 RID: 345
	// (get) Token: 0x06000B9E RID: 2974 RVA: 0x00090F1C File Offset: 0x0008F11C
	public override int Width
	{
		get
		{
			return this.background.width;
		}
	}

	// Token: 0x1700015A RID: 346
	// (get) Token: 0x06000B9F RID: 2975 RVA: 0x00090F2C File Offset: 0x0008F12C
	public override int Height
	{
		get
		{
			return this.background.height;
		}
	}

	// Token: 0x06000BA0 RID: 2976 RVA: 0x00090F3C File Offset: 0x0008F13C
	public override void MainInitialize()
	{
		this.isUpdating = true;
		this.isRendering = true;
		this.isGameHandler = true;
		base.MainInitialize();
	}

	// Token: 0x06000BA1 RID: 2977 RVA: 0x00090F5C File Offset: 0x0008F15C
	public override void Clear()
	{
		base.Clear();
	}

	// Token: 0x06000BA2 RID: 2978 RVA: 0x00090F64 File Offset: 0x0008F164
	public override void Register()
	{
		EventFactory.Register("ShowTutorialGUI", this);
		EventFactory.Register("HideTutorialGUI", this);
		EventFactory.Register("TrainingTrigger", this);
	}

	// Token: 0x06000BA3 RID: 2979 RVA: 0x00090F88 File Offset: 0x0008F188
	public override void OnUpdate()
	{
	}

	// Token: 0x06000BA4 RID: 2980 RVA: 0x00090F8C File Offset: 0x0008F18C
	public override void OnLateUpdate()
	{
	}

	// Token: 0x06000BA5 RID: 2981 RVA: 0x00090F90 File Offset: 0x0008F190
	public override void OnConnected()
	{
		this.Clear();
		base.OnConnected();
	}

	// Token: 0x06000BA6 RID: 2982 RVA: 0x00090FA0 File Offset: 0x0008F1A0
	public override void GameGUI()
	{
	}

	// Token: 0x06000BA7 RID: 2983 RVA: 0x00090FA4 File Offset: 0x0008F1A4
	private bool ShowTMessage(int n)
	{
		if (!this.trigger.events[n].TaskEnd())
		{
			this.DrawTextTexture(this.trigger.events[n].description);
			this.gui.TextField(this.TextRect, this.trigger.events[n].description, 18, "#FFFFFF", TextAnchor.UpperCenter, false, false);
			if (this.trigger.events[n].isTimered && !this.trigger.events[n].isTimerEnabled())
			{
				this.trigger.events[n].TimerOn();
			}
			if (this.trigger.events[n].isTimered && this.trigger.events[n].TimerElapsed() > 5f)
			{
				this.trigger.events[n].SetTaskEnd();
			}
			for (int i = 0; i < this.trigger.events[n].keys.Length; i++)
			{
				if (!this.trigger.events[n].TaskEnd() && Peer.ClientGame.LocalPlayer.PlayerInput.Current.GetKeyDown(this.trigger.events[n].keys[i], true))
				{
					this.trigger.events[n].Click(i);
				}
			}
			return true;
		}
		return false;
	}

	// Token: 0x06000BA8 RID: 2984 RVA: 0x00091118 File Offset: 0x0008F318
	private void DrawTextTexture(string text)
	{
		if (this.TextTexture)
		{
			float num = this.gui.CalcWidth(text, this.gui.fontDNC57, 18);
			float num2 = num / ((float)this.TextTexture.width * 0.8f);
			float num3 = ((float)this.TextTexture.width - this.TextRect.width) / 2f;
			if (num3 < 0f)
			{
				num3 *= -1f;
			}
			if (num2 < 1f)
			{
				num2 = 1f;
			}
			int num4 = 0;
			while ((float)num4 < num2)
			{
				this.gui.Picture(new Vector2(this.TextRect.xMin - num3, this.TextRect.yMin + (float)(this.TextTexture.height * num4)), this.TextTexture);
				num4++;
			}
		}
	}

	// Token: 0x04000D7E RID: 3454
	public Texture2D background;

	// Token: 0x04000D7F RID: 3455
	private List<TrainingTrigger> triggers = new List<TrainingTrigger>();

	// Token: 0x04000D80 RID: 3456
	private TrainingTrigger trigger;

	// Token: 0x04000D81 RID: 3457
	private Rect TextRect = new Rect(60f, (float)Screen.height * 0.61f, 380f, 160f);

	// Token: 0x04000D82 RID: 3458
	public Texture2D TextTexture;
}
