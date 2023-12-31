using System;
using UnityEngine;

namespace LeagueGUI
{
	// Token: 0x02000328 RID: 808
	internal class AdvertisingFrame : AbstractFrame
	{
		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06001B58 RID: 7000 RVA: 0x000F657C File Offset: 0x000F477C
		private static Rect AdFrameRect
		{
			get
			{
				return new Rect((float)(Screen.width - 800) * 0.5f + 4f, (float)(Screen.height - 600) * 0.5f + 180f, 792f, 600f);
			}
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x000F65C8 File Offset: 0x000F47C8
		public override void OnStart()
		{
			this.SlidesInitialize();
			this._inactive = LeagueWindow.I.Styles.InactiveAdBtnStyle;
			this._active = LeagueWindow.I.Styles.ActiveAdBtnStyle;
			this._smallBtnWidth = (float)this._active.normal.background.width;
			this._smallBtnHeight = (float)this._active.normal.background.height;
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x000F6640 File Offset: 0x000F4840
		public override void OnGUI()
		{
			GUI.BeginGroup(AdvertisingFrame.AdFrameRect);
			GUI.DrawTexture(new Rect(0f, 0f, AdvertisingFrame.AdFrameRect.width, 315f), LeagueWindow.I.Textures.Gray);
			this._currentSlide.DrawSlide(2f, 2f);
			this.SlideControllButtons();
			GUI.EndGroup();
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x000F66AC File Offset: 0x000F48AC
		public override void OnUpdate()
		{
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x000F66B0 File Offset: 0x000F48B0
		private void SlidesInitialize()
		{
			if (LeagueWindow.I.LeagueInfo.Offseason)
			{
				return;
			}
			LeagueInfo.AdInfo[] ads = LeagueWindow.I.LeagueInfo.Ads;
			this._slides = new Slide[ads.Length];
			for (int i = 0; i < this._slides.Length; i++)
			{
				this._slides[i] = new Slide
				{
					Banner = ads[i].AdBanner,
					Title = ads[i].Title,
					Description = ads[i].Description
				};
				if (this._currentSlide == null)
				{
					this.SetCurrentSlide(this._slides[i]);
				}
			}
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x000F675C File Offset: 0x000F495C
		private void SetCurrentSlide(Slide slide)
		{
			this._currentSlide = slide;
			this._currentSlide.Awake();
			this._slideTimer.Start();
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x000F677C File Offset: 0x000F497C
		private void SlideControllButtons()
		{
			for (int i = 0; i < this._slides.Length; i++)
			{
				if (this._slideTimer.Time > LeagueWindow.I.LeagueInfo.AdShowTime)
				{
					this._index = ((this._index >= this._slides.Length - 1) ? 0 : (this._index + 1));
					this.SetCurrentSlide(this._slides[this._index]);
				}
				if (GUI.Button(new Rect((float)(790 - 30 * this._slides.Length + 30 * i), 330f, this._smallBtnWidth, this._smallBtnHeight), string.Empty, (this._index != i) ? this._inactive : this._active))
				{
					if (this._currentSlide != this._slides[i])
					{
						this.SetCurrentSlide(this._slides[i]);
						this._index = i;
					}
				}
			}
		}

		// Token: 0x0400202F RID: 8239
		private Slide[] _slides;

		// Token: 0x04002030 RID: 8240
		private Slide _currentSlide;

		// Token: 0x04002031 RID: 8241
		private GUIStyle _active;

		// Token: 0x04002032 RID: 8242
		private GUIStyle _inactive;

		// Token: 0x04002033 RID: 8243
		private float _smallBtnWidth;

		// Token: 0x04002034 RID: 8244
		private float _smallBtnHeight;

		// Token: 0x04002035 RID: 8245
		private int _index;

		// Token: 0x04002036 RID: 8246
		private readonly Timer _slideTimer = new Timer();
	}
}
