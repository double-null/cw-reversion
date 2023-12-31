using System;
using System.Collections.Generic;
using UnityEngine;

namespace LeagueGUI
{
	// Token: 0x0200031C RID: 796
	internal class RatingFrame : AbstractFrame
	{
		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06001AFA RID: 6906 RVA: 0x000F3EDC File Offset: 0x000F20DC
		private bool RefreshAvailable
		{
			get
			{
				return !this.refreshTimer.IsStarted || this.refreshTimer.Time > this.refreshCD;
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06001AFB RID: 6907 RVA: 0x000F3F04 File Offset: 0x000F2104
		private Rect RatingFrameRect
		{
			get
			{
				return new Rect((float)Screen.width * 0.5f + 4f, (float)(Screen.height - 600) * 0.5f + 120f, 395f, 470f);
			}
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x000F3F40 File Offset: 0x000F2140
		public override void OnStart()
		{
			this._current = Language.LeagueCurrent.ToUpper();
			this._past = Language.LeaguePast.ToUpper();
			this._future = Language.LeagueFuture.ToUpper();
			this._showDropDown = false;
			this.scroll.Clear();
			this._btnTitle = ((!LeagueWindow.I.LeagueInfo.Offseason) ? this._current : this._past);
			this.scroll.Add(this._current);
			this.scroll.Add(this._past);
			this.scroll.Add(this._future);
			if (LeagueWindow.I.LeagueInfo.Offseason)
			{
				this.LoadPasRating();
			}
			else
			{
				this.LoadRating(false);
			}
			this._twister = new Twister();
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x000F4020 File Offset: 0x000F2220
		public override void OnGUI()
		{
			GUI.BeginGroup(this.RatingFrameRect);
			GUI.DrawTexture(new Rect(0f, 0f, this.RatingFrameRect.width, 20f), LeagueWindow.I.Textures.Black);
			GUI.DrawTexture(new Rect(0f, 20f, this.RatingFrameRect.width - 4f, this.RatingFrameRect.height - 20f), LeagueWindow.I.Textures.Gray);
			LeagueWindow.I.Styles.BrownLabel.alignment = TextAnchor.MiddleLeft;
			GUI.Label(new Rect(5f, 4f, 50f, 14f), (LeagueWindow.I.SeasonState != SeasonState.past) ? Language.LeagueRating : Language.LeagueWinners, LeagueWindow.I.Styles.BrownLabel);
			if (!LeagueWindow.I.LeagueInfo.Offseason)
			{
				this.tempColor = GUI.color;
				if (this.refreshTimer.IsStarted)
				{
					GUI.color = new Color(1f, this.refreshTimer.Time / this.refreshCD, this.refreshTimer.Time / this.refreshCD);
				}
				if (GUI.Button(new Rect(373f, 21f, 17f, 16f), string.Empty, LeagueWindow.I.Styles.RefreshBtnStyle) && this.RefreshAvailable)
				{
					this.refreshTimer.Start();
					Main.AddDatabaseRequestCallBack<LoadProfileInfo>(delegate
					{
						TopFrame.RefreshPlayerData = true;
					}, delegate
					{
					}, new object[0]);
					this.LoadRating(false);
				}
				GUI.color = this.tempColor;
				if (GUI.Button(new Rect(277f, 1f, 20f, 15f), string.Empty, LeagueWindow.I.Styles.SmallBtnStyle))
				{
					foreach (LeagueRatingInfo leagueRatingInfo2 in LeagueWindow.I.LeagueRatingInfo)
					{
						if (leagueRatingInfo2.uid == Main.UserInfo.userID)
						{
							this._playerInTop = true;
							this._scrollTo = leagueRatingInfo2.place;
						}
					}
					if (!this._playerInTop)
					{
						this.LoadRating(true);
					}
					else
					{
						LeagueWindow.I.Lists.RatingList.ScrollPosition.y = LeagueWindow.I.Lists.RatingList.FillDensity * (float)(this._scrollTo - 1);
					}
				}
			}
			GUI.DrawTexture(new Rect(285f, 3f, 5f, 11f), LeagueWindow.I.Textures.MyPositionIcon);
			if (GUI.Button(new Rect(300f, 1f, 88f, 15f), this._btnTitle, LeagueWindow.I.Styles.LongBtnStyle))
			{
				this._showDropDown = !this._showDropDown;
			}
			GUI.DrawTexture(new Rect(375f, 4f, (float)LeagueWindow.I.Textures.Arrows[0].width, (float)LeagueWindow.I.Textures.Arrows[0].height), LeagueWindow.I.Textures.Arrows[0]);
			LeagueWindow.I.Styles.BrownLabel.alignment = TextAnchor.MiddleCenter;
			GUI.Label(LeagueWindow.I.Rects.RatingHeaderRects[0], "#", LeagueWindow.I.Styles.DarkGrayLabel);
			GUI.Label(LeagueWindow.I.Rects.RatingHeaderRects[1], Language.LeagueRank, LeagueWindow.I.Styles.DarkGrayLabel);
			GUI.Label(LeagueWindow.I.Rects.RatingHeaderRects[2], Language.LeagueRatingHeaderLvl, LeagueWindow.I.Styles.DarkGrayLabel);
			GUI.Label(LeagueWindow.I.Rects.RatingHeaderRects[3], Language.LeagueRatingHeaderNameNick, LeagueWindow.I.Styles.DarkGrayLabel);
			GUI.Label(LeagueWindow.I.Rects.RatingHeaderRects[4], Language.LeagueRatingHeaderLP, LeagueWindow.I.Styles.DarkGrayLabel);
			GUI.Label(LeagueWindow.I.Rects.RatingHeaderRects[5], Language.LeagueRatingHeaderWins, LeagueWindow.I.Styles.DarkGrayLabel);
			GUI.Label(LeagueWindow.I.Rects.RatingHeaderRects[6], Language.LeagueRatingHeaderDefeats, LeagueWindow.I.Styles.DarkGrayLabel);
			GUI.Label(LeagueWindow.I.Rects.RatingHeaderRects[7], Language.LeagueRatingHeaderLeaves, LeagueWindow.I.Styles.DarkGrayLabel);
			GUI.enabled = !this._showDropDown;
			if (this._ratingLoaded)
			{
				if (this._btnTitle == this._past)
				{
					LeagueWindow.I.Lists.RatingPastList.OnGUI();
				}
				else if (this._btnTitle == this._future)
				{
					LeagueWindow.I.Lists.RatingFutureList.OnGUI();
				}
				else
				{
					LeagueWindow.I.Lists.RatingList.OnGUI();
				}
			}
			else
			{
				this._twister.OnGUI(170f, 180f, 180f);
				LeagueWindow.I.Styles.GrayLabel.alignment = TextAnchor.MiddleCenter;
				GUI.Label(new Rect(0f, 235f, this.RatingFrameRect.width, 14f), Language.CWMainLoadRating.ToLower(), LeagueWindow.I.Styles.GrayLabel);
				LeagueWindow.I.Styles.GrayLabel.alignment = TextAnchor.MiddleLeft;
			}
			GUI.enabled = true;
			if (this._showDropDown)
			{
				this.ShowDropDownScroll();
			}
			GUI.EndGroup();
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x000F46A8 File Offset: 0x000F28A8
		private void ShowDropDownScroll()
		{
			float width = (float)LeagueWindow.I.Styles.LongBtnStyle.normal.background.width;
			float height = (float)LeagueWindow.I.Styles.LongBtnStyle.normal.background.height;
			byte b = 0;
			while ((int)b < this.scroll.Count)
			{
				if (this.scroll[(int)b] == this._btnTitle)
				{
					this.scroll.Remove(this.scroll[(int)b]);
					this.scroll.Add(this._btnTitle);
				}
				else
				{
					if (LeagueWindow.I.LeagueInfo.Offseason && this.scroll[(int)b] == this._current)
					{
						GUI.enabled = false;
					}
					if (!LeagueWindow.I.LeagueInfo.Offseason && this.scroll[(int)b] == this._future)
					{
						GUI.enabled = false;
					}
					if (GUI.Button(new Rect(300f, (float)(20 + 20 * b), width, height), this.scroll[(int)b], LeagueWindow.I.Styles.LongBtnStyle))
					{
						this._btnTitle = this.scroll[(int)b];
						if (this._btnTitle == this._current)
						{
							LeagueWindow.I.SeasonState = SeasonState.current;
						}
						else if (this._btnTitle == this._past)
						{
							LeagueWindow.I.SeasonState = SeasonState.past;
						}
						else
						{
							LeagueWindow.I.SeasonState = SeasonState.future;
							this.LoadFutureRating();
						}
						this._showDropDown = !this._showDropDown;
					}
					GUI.enabled = true;
				}
				b += 1;
			}
			if (Event.current.isMouse)
			{
				this._showDropDown = !this._showDropDown;
			}
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x000F48A4 File Offset: 0x000F2AA4
		private void GenerateList()
		{
			LeagueWindow.I.Lists.RatingList.Clear();
			for (int i = 0; i < LeagueWindow.I.LeagueRatingInfo.Length; i++)
			{
				LeagueWindow.I.Lists.RatingList.Add(new RatingBar(LeagueWindow.I.LeagueRatingInfo[i]));
			}
			this._ratingLoaded = true;
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x000F4910 File Offset: 0x000F2B10
		private void GeneratePastList()
		{
			LeagueWindow.I.Lists.RatingPastList.Clear();
			for (int i = 0; i < LeagueWindow.I.LeagueRatingInfo.Length; i++)
			{
				LeagueWindow.I.Lists.RatingPastList.Add(new ExtendedRatingBar(LeagueWindow.I.LeagueRatingInfo[i]));
			}
			this._ratingLoaded = true;
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x000F497C File Offset: 0x000F2B7C
		private void GenerateFutureList()
		{
			LeagueWindow.I.Lists.RatingFutureList.Clear();
			for (int i = 0; i < LeagueWindow.I.LeagueRatingInfo.Length; i++)
			{
				LeagueWindow.I.Lists.RatingFutureList.Add(new RatingBar(LeagueWindow.I.LeagueRatingInfo[i]));
			}
			this._ratingLoaded = true;
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x000F49E8 File Offset: 0x000F2BE8
		private void LoadRating(bool loadPlayerPosition = false)
		{
			this._ratingLoaded = false;
			Main.AddDatabaseRequestCallBack<LoadLeagueRating>(delegate
			{
				this.GenerateList();
			}, delegate
			{
			}, new object[]
			{
				loadPlayerPosition
			});
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x000F4A3C File Offset: 0x000F2C3C
		private void LoadPasRating()
		{
			Main.AddDatabaseRequestCallBack<LoadLeagueRating>(delegate
			{
				this.GeneratePastList();
			}, delegate
			{
			}, new object[0]);
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x000F4A80 File Offset: 0x000F2C80
		private void LoadFutureRating()
		{
			Main.AddDatabaseRequestCallBack<LoadLeagueRating>(delegate
			{
				this.GenerateFutureList();
			}, delegate
			{
			}, new object[0]);
		}

		// Token: 0x04001FFA RID: 8186
		private bool _showDropDown;

		// Token: 0x04001FFB RID: 8187
		private bool _playerInTop;

		// Token: 0x04001FFC RID: 8188
		private bool _ratingLoaded;

		// Token: 0x04001FFD RID: 8189
		private int _scrollTo;

		// Token: 0x04001FFE RID: 8190
		private string _current;

		// Token: 0x04001FFF RID: 8191
		private string _past;

		// Token: 0x04002000 RID: 8192
		private string _future;

		// Token: 0x04002001 RID: 8193
		private List<string> scroll = new List<string>();

		// Token: 0x04002002 RID: 8194
		private string _btnTitle = string.Empty;

		// Token: 0x04002003 RID: 8195
		private Twister _twister;

		// Token: 0x04002004 RID: 8196
		private float refreshCD = 10f;

		// Token: 0x04002005 RID: 8197
		private Timer refreshTimer = new Timer();

		// Token: 0x04002006 RID: 8198
		private Color tempColor;
	}
}
