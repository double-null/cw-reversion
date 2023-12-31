using System;
using UnityEngine;

namespace BannerGUINamespace.BannerQueueNamespace
{
	// Token: 0x020000DA RID: 218
	internal class BannerMortar : BannerQueueItem
	{
		// Token: 0x060005C2 RID: 1474 RVA: 0x0002B908 File Offset: 0x00029B08
		public override void Init(float speed)
		{
			this.inited = true;
			this.speed = speed;
			if (this.currentArmstr.ready != null && !Peer.HardcoreMode)
			{
				Audio.Play(this.currentArmstr.ready);
			}
			this.initTimer = BannerGUI.I.armstrBig_AlphaKeys[BannerGUI.I.armstrBig_AlphaKeys.Length - 1].time;
			this.GNAME.InitTimer(this.initTimer / this.speed);
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0002B994 File Offset: 0x00029B94
		public override void OnGUI()
		{
			base.OnGUI();
			if (this.buttonWidth == 0f)
			{
				this.buttonWidth = MainGUI.Instance.CalcWidth(Language.Push + this.button + Language.CallForSupport, MainGUI.Instance.fontDNC57, 16);
			}
			MainGUI.Instance.color = new Color(1f, 1f, 1f, BannerGUI.I.armstrBig_Alpha.Evaluate(this.GNAME.Get() * this.speed));
			MainGUI.Instance.PictureCentered(new Vector2((float)(Screen.width / 2), (float)(this.currentArmstr.tex_big.height / 2)), this.currentArmstr.tex_big, Vector2.one);
			this.TextRect.Set((float)(Screen.width / 2) - this.buttonWidth / 2f, (float)(Screen.height - 100), this.buttonWidth, 20f);
			MainGUI.Instance.CompositeText(ref this.TextRect, Language.Push, 16, "#FFFFFF", TextAnchor.UpperCenter, BannerGUI.I.armstrBig_Alpha.Evaluate(this.GNAME.Get() * this.speed));
			MainGUI.Instance.CompositeText(ref this.TextRect, this.button, 16, "#62aeea", TextAnchor.UpperCenter, BannerGUI.I.armstrBig_Alpha.Evaluate(this.GNAME.Get() * this.speed));
			MainGUI.Instance.CompositeText(ref this.TextRect, Language.CallForSupport, 16, "#FFFFFF", TextAnchor.UpperCenter, BannerGUI.I.armstrBig_Alpha.Evaluate(this.GNAME.Get() * this.speed));
		}

		// Token: 0x0400058F RID: 1423
		public string button = string.Empty;

		// Token: 0x04000590 RID: 1424
		public float buttonWidth;

		// Token: 0x04000591 RID: 1425
		public ArmstrData currentArmstr = new ArmstrData();

		// Token: 0x04000592 RID: 1426
		private Rect TextRect = default(Rect);
	}
}
