using System;
using UnityEngine;

namespace LeagueGUI
{
	// Token: 0x02000326 RID: 806
	internal class SearchFrame : AbstractFrame
	{
		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06001B4C RID: 6988 RVA: 0x000F6358 File Offset: 0x000F4558
		// (set) Token: 0x06001B4D RID: 6989 RVA: 0x000F6360 File Offset: 0x000F4560
		public static bool Accepted { get; set; }

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06001B4E RID: 6990 RVA: 0x000F6368 File Offset: 0x000F4568
		public static Rect SearchFrameRect
		{
			get
			{
				return new Rect((float)(Screen.width - 800) * 0.5f + 4f, (float)(Screen.height - 600) * 0.5f + 120f, 792f, 58f);
			}
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x000F63B4 File Offset: 0x000F45B4
		public void SetFrame(IFrame frame)
		{
			this._currentFrame = frame;
			this._currentFrame.OnStart();
			if (this._currentFrame == this.ReadyFrame)
			{
				this.TIME.Start();
			}
			if (this._currentFrame == this.MapLoadingFrame)
			{
				SearchFrame.Countdown.Start();
			}
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x000F640C File Offset: 0x000F460C
		public IFrame GetFrame()
		{
			return this._currentFrame;
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x000F6414 File Offset: 0x000F4614
		public override void OnStart()
		{
			if (this._currentFrame == null)
			{
				this.SetFrame(this.SearchingFrame);
			}
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x000F6430 File Offset: 0x000F4630
		public override void OnGUI()
		{
			GUI.DrawTexture(SearchFrame.SearchFrameRect, LeagueWindow.I.Textures.Gray);
			GUI.BeginGroup(SearchFrame.SearchFrameRect);
			this._currentFrame.OnGUI();
			GUI.EndGroup();
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x000F6468 File Offset: 0x000F4668
		public override void OnUpdate()
		{
		}

		// Token: 0x04002022 RID: 8226
		private IFrame _currentFrame;

		// Token: 0x04002023 RID: 8227
		public Timer TIME = new Timer();

		// Token: 0x04002024 RID: 8228
		public static Timer Countdown = new Timer();

		// Token: 0x04002025 RID: 8229
		public readonly IFrame SearchingFrame = new SearchingBar();

		// Token: 0x04002026 RID: 8230
		public readonly IFrame ReadyFrame = new ReadyBar();

		// Token: 0x04002027 RID: 8231
		public readonly IFrame MapLoadingFrame = new MapLoadingBar();

		// Token: 0x04002028 RID: 8232
		public readonly IFrame PlayersWaitingFrame = new PlayersWaitingBar();

		// Token: 0x04002029 RID: 8233
		public readonly IFrame MatchStartFrame = new MatchStartBar();
	}
}
