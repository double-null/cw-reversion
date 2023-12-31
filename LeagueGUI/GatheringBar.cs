using System;
using ClanSystemGUI;
using LeagueSystem;
using UnityEngine;

namespace LeagueGUI
{
	// Token: 0x0200032B RID: 811
	internal class GatheringBar : IScrollListItem, IComparable
	{
		// Token: 0x06001B67 RID: 7015 RVA: 0x000F6F00 File Offset: 0x000F5100
		public GatheringBar(PlayerJsonData data, EntityNetPlayer player = null)
		{
			this._timer = new Timer();
			this._timer.Start();
			this._data = data;
			this._player = player;
			this._red = LeagueWindow.I.Textures.OnlineSpot[1];
			this._green = LeagueWindow.I.Textures.OnlineSpot[0];
			int num;
			int.TryParse(this._data.player_class, out num);
			if (num > 0)
			{
				this._classIcon = ClanSystemWindow.I.Textures.statsClass[num - 1];
			}
			this._levelIcon = MainGUI.Instance.rank_icon[this._data.level];
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06001B68 RID: 7016 RVA: 0x000F6FB4 File Offset: 0x000F51B4
		public float Width
		{
			get
			{
				return 386f;
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06001B69 RID: 7017 RVA: 0x000F6FBC File Offset: 0x000F51BC
		public float Height
		{
			get
			{
				return 47f;
			}
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x000F6FC4 File Offset: 0x000F51C4
		public int CompareTo(object obj)
		{
			return 0;
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x000F6FC8 File Offset: 0x000F51C8
		private void FindPlayer()
		{
			if (!Peer.ClientGame)
			{
				return;
			}
			this._timer.Start();
			foreach (EntityNetPlayer entityNetPlayer in Peer.ClientGame.AllPlayers)
			{
				if (entityNetPlayer.playerInfo.userID.ToString() == this._data.uid)
				{
					this._player = entityNetPlayer;
				}
			}
		}

		// Token: 0x06001B6C RID: 7020 RVA: 0x000F7074 File Offset: 0x000F5274
		public void OnGUI(float x, float y, int index)
		{
			if (this._timer.Time > 0.5f && this._player == null)
			{
				this.FindPlayer();
			}
			this.DrawBar(x - 20f, y);
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x000F70BC File Offset: 0x000F52BC
		private void DrawBar(float x, float y)
		{
			GUI.DrawTexture(new Rect(x, y, this.Width, this.Height), LeagueWindow.I.Textures.DarkGray);
			LeagueWindow.I.Styles.WhiteLabel16.alignment = TextAnchor.MiddleCenter;
			if (this._player == null)
			{
				GUI.DrawTexture(new Rect(x + 30f, 5f + y, (float)this._red.width, (float)this._red.height), this._red);
			}
			else if (this._player.playerInfo.userID != Main.UserInfo.userID && this._player.IsSpectactor && this._player.playerInfo.loading < 100)
			{
				GUI.Label(new Rect(x + 30f, y + 5f, (float)this._green.width, 14f), Helpers.ColoredText(this._player.playerInfo.loading + "%", "#e10000"), LeagueWindow.I.Styles.WhiteLabel14);
			}
			else
			{
				GUI.DrawTexture(new Rect(x + 30f, 5f + y, (float)this._green.width, (float)this._green.height), this._green);
			}
			GUI.Label(new Rect(x + 232f, 24f + y, 0f, 0f), this._data.lp.ToString(), LeagueWindow.I.Styles.WhiteLabel16);
			GUI.Label(new Rect(x + 275f, 24f + y, 0f, 0f), this._data.wins.ToString(), LeagueWindow.I.Styles.WhiteLabel16);
			GUI.Label(new Rect(x + 305f, 24f + y, 0f, 0f), this._data.loss.ToString(), LeagueWindow.I.Styles.WhiteLabel16);
			GUI.Label(new Rect(x + 335f, 24f + y, 0f, 0f), this._data.leav.ToString(), LeagueWindow.I.Styles.WhiteLabel16);
			LeagueWindow.I.Styles.WhiteLabel16.alignment = TextAnchor.MiddleLeft;
			LeagueHelpers.DrawRank(x + 31f, y + 26f, this._data.lp, 1f, false);
			this.DrawPlayerInfo(x, y);
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x000F7384 File Offset: 0x000F5584
		public void DrawPlayerInfo(float x, float y)
		{
			float num = 0f;
			GUI.DrawTexture(new Rect(x + 55f, y + 5f, (float)this._levelIcon.width, (float)this._levelIcon.height), this._levelIcon);
			if (this._classIcon)
			{
				GUI.DrawTexture(new Rect(x + 80f, y - 1f, (float)this._classIcon.width, (float)this._classIcon.height), this._classIcon);
				num += (float)(50 + this._levelIcon.width + this._classIcon.width);
			}
			else
			{
				num += (float)(50 + this._levelIcon.width + 10);
			}
			LeagueWindow.I.Styles.WhiteLabel16.clipping = TextClipping.Clip;
			GUI.Label(new Rect(x + num, y + 8f, 100f, 16f), Helpers.ColoredTag(this._data.tag) + " " + this._data.user_name, LeagueWindow.I.Styles.WhiteLabel16);
			LeagueWindow.I.Styles.WhiteLabel16.clipping = TextClipping.Overflow;
			num = (float)(50 + ((!this._classIcon) ? 31 : this._classIcon.width) + 8);
			LeagueWindow.I.Styles.GrayLabel.clipping = TextClipping.Clip;
			GUI.Label(new Rect(x + num, y + 26f, 120f, 14f), this._data.first_name + " " + this._data.last_name, LeagueWindow.I.Styles.GrayLabel);
			LeagueWindow.I.Styles.GrayLabel.clipping = TextClipping.Overflow;
		}

		// Token: 0x0400203C RID: 8252
		private PlayerJsonData _data;

		// Token: 0x0400203D RID: 8253
		private EntityNetPlayer _player;

		// Token: 0x0400203E RID: 8254
		private Texture2D _red;

		// Token: 0x0400203F RID: 8255
		private Texture2D _green;

		// Token: 0x04002040 RID: 8256
		private Texture2D _classIcon;

		// Token: 0x04002041 RID: 8257
		private Texture2D _levelIcon;

		// Token: 0x04002042 RID: 8258
		private Timer _timer;
	}
}
