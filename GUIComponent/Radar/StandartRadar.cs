using System;
using System.Collections.Generic;
using UnityEngine;

namespace GUIComponent.Radar
{
	// Token: 0x0200016A RID: 362
	internal class StandartRadar : IRadar
	{
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x00069670 File Offset: 0x00067870
		// (set) Token: 0x060009CE RID: 2510 RVA: 0x00069678 File Offset: 0x00067878
		public Texture2D MapTexture
		{
			get
			{
				return this._mapTexture;
			}
			set
			{
				if (value != null)
				{
					this._mapTexture = value;
					this._radar.RadarSize.x = (float)this._mapTexture.width;
					this._radar.RadarSize.y = (float)this._mapTexture.height;
				}
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x000696D0 File Offset: 0x000678D0
		// (set) Token: 0x060009D0 RID: 2512 RVA: 0x000696E0 File Offset: 0x000678E0
		public Vector2 RadarPos
		{
			get
			{
				return this._radar.RadarPos;
			}
			set
			{
				this._radar.RadarPos = value;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x000696F0 File Offset: 0x000678F0
		// (set) Token: 0x060009D2 RID: 2514 RVA: 0x00069700 File Offset: 0x00067900
		public Vector2 RadarSize
		{
			get
			{
				return this._radar.RadarSize;
			}
			set
			{
				this._radar.RadarSize = value;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x00069710 File Offset: 0x00067910
		// (set) Token: 0x060009D4 RID: 2516 RVA: 0x00069720 File Offset: 0x00067920
		public Vector3 MapSize
		{
			get
			{
				return this._radar.MapSize;
			}
			set
			{
				this._radar.MapSize = value;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x00069730 File Offset: 0x00067930
		// (set) Token: 0x060009D6 RID: 2518 RVA: 0x00069740 File Offset: 0x00067940
		public Vector3 LowerRight
		{
			get
			{
				return this._radar.LowerRight;
			}
			set
			{
				this._radar.UpperLeft = value;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x00069750 File Offset: 0x00067950
		// (set) Token: 0x060009D8 RID: 2520 RVA: 0x00069760 File Offset: 0x00067960
		public Vector3 UpperLeft
		{
			get
			{
				return this._radar.UpperLeft;
			}
			set
			{
				this._radar.UpperLeft = value;
			}
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x00069770 File Offset: 0x00067970
		public void OnConnected()
		{
			this.showPlayerClassIcon = Main.UserInfo.skillUnlocked(Skills.analyze3);
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x00069784 File Offset: 0x00067984
		public void DrawMapTexture()
		{
			if (this._mapTexture != null)
			{
				if (this.Background != null)
				{
					float num = (float)Mathf.Abs(this._mapTexture.width - this.Background.width) / 2f;
					GUI.DrawTexture(new Rect(this.RadarPos.x - num, this.RadarPos.y - num / 2f, (float)this.Background.width, (float)this.Background.height), this.Background, ScaleMode.ScaleToFit, true);
				}
				GUI.DrawTexture(new Rect(this.RadarPos.x, this.RadarPos.y, (float)this._mapTexture.width, (float)this._mapTexture.height), this._mapTexture, ScaleMode.ScaleToFit, true);
			}
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x00069870 File Offset: 0x00067A70
		public void DrawPoint(Vector3 pos, Texture2D icon)
		{
			this._radar.DrawPoint(pos, icon);
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00069880 File Offset: 0x00067A80
		public void DrawRotatePoint(Vector3 pos, float rotation, Texture2D icon)
		{
			this._radar.DrawRotatePoint(pos, rotation, icon);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00069890 File Offset: 0x00067A90
		public virtual void DrawAllPlayers(Texture2D icon)
		{
			if (this.players != null)
			{
				foreach (EntityNetPlayer entityNetPlayer in this.players)
				{
					if (!entityNetPlayer.isPlayer && entityNetPlayer.IsAlive)
					{
						this._radar.DrawRotatePoint(entityNetPlayer.Position, entityNetPlayer.Euler.y, icon);
					}
				}
			}
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00069930 File Offset: 0x00067B30
		public virtual void DrawLocalPlayer(Texture2D icon)
		{
			this._radar.DrawRotatePoint(Peer.ClientGame.LocalPlayer.Position, Peer.ClientGame.LocalPlayer.Euler.y, icon);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00069970 File Offset: 0x00067B70
		public virtual void DrawAllTeammate(Texture2D icon)
		{
			if (this.players != null)
			{
				foreach (EntityNetPlayer entityNetPlayer in this.players)
				{
					if (!entityNetPlayer.isPlayer && entityNetPlayer.IsBear == Peer.ClientGame.LocalPlayer.IsBear && entityNetPlayer.IsAlive)
					{
						this._radar.DrawRotatePoint(entityNetPlayer.Position, entityNetPlayer.Euler.y, icon);
					}
				}
			}
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x00069A2C File Offset: 0x00067C2C
		public virtual void DrawPlayerList(Dictionary<EntityNetPlayer, float> hotspot, Texture2D icon)
		{
			foreach (KeyValuePair<EntityNetPlayer, float> keyValuePair in hotspot)
			{
				if (keyValuePair.Key != null && keyValuePair.Key.IsAlive && keyValuePair.Value > Time.time)
				{
					this._radar.DrawRotatePoint(keyValuePair.Key.Position, keyValuePair.Key.Euler.y, icon);
				}
			}
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00069AE8 File Offset: 0x00067CE8
		public void OnGUI(Texture2D enemyIcon, Texture2D allyicon)
		{
			PlayerType playerType = Peer.ClientGame.LocalPlayer.playerInfo.playerType;
			EntityNetPlayer entity = Peer.ClientGame.LocalPlayer.Entity;
			float time = Time.time;
			ClientEntity clientEntity = null;
			if (playerType != PlayerType.spectactor)
			{
				if (this.entites.Count > this.entitiesIndex)
				{
					clientEntity = this.entites[this.entitiesIndex];
				}
				for (int i = 0; i < this.players.Count; i++)
				{
					EntityNetPlayer entityNetPlayer = this.players[i];
					if (entityNetPlayer.playerInfo.playerType != PlayerType.spectactor && entityNetPlayer.IsAlive && entityNetPlayer != entity)
					{
						if (Main.IsTeamGame && entityNetPlayer.PlayerTransform != null)
						{
							if (entityNetPlayer.playerInfo.playerType == playerType)
							{
								this._radar.DrawRotatePoint(entityNetPlayer.PlayerTransform.position, entityNetPlayer.Euler.y, allyicon);
							}
							else
							{
								if ((entityNetPlayer.lastFireTime > time && Vector3.Distance(entityNetPlayer.Position, entity.Position) < 15f) || entityNetPlayer.lastSonarTime > time)
								{
									if (this.showPlayerClassIcon && entityNetPlayer.playerInfo.playerClass != PlayerClass.none)
									{
										this.DrawPoint(entityNetPlayer.PlayerTransform.position, this.hostileClassIcons[(int)entityNetPlayer.playerInfo.playerClass]);
									}
									else
									{
										this.DrawPoint(entityNetPlayer.PlayerTransform.position, enemyIcon);
									}
								}
								if (clientEntity != null && clientEntity.EntityType == EntityType.sonar && Vector3.Distance(clientEntity.state.pos, entityNetPlayer.Position) < 20f && entityNetPlayer.lastSonarTime + 1f < Time.time)
								{
									entityNetPlayer.lastSonarTime = Time.time + 1f;
								}
							}
						}
						else
						{
							if ((entityNetPlayer.lastFireTime > time && Vector3.Distance(entityNetPlayer.Position, entity.Position) < 15f) || entityNetPlayer.lastSonarTime > time)
							{
								if (this.showPlayerClassIcon && entityNetPlayer.playerInfo.playerClass != PlayerClass.none)
								{
									this.DrawPoint(entityNetPlayer.PlayerTransform.position, this.hostileClassIcons[(int)entityNetPlayer.playerInfo.playerClass]);
								}
								else
								{
									this.DrawPoint(entityNetPlayer.PlayerTransform.position, enemyIcon);
								}
							}
							if (clientEntity != null && clientEntity.EntityType == EntityType.sonar && Vector3.Distance(clientEntity.state.pos, entityNetPlayer.Position) < 20f && entityNetPlayer.lastSonarTime + 1f < Time.time)
							{
								entityNetPlayer.lastSonarTime = Time.time + 1f;
							}
						}
					}
				}
				GUI.matrix = Matrix4x4.TRS(Vector2.zero, Quaternion.identity, Vector3.one);
				this.entitiesIndex++;
				if (this.entitiesIndex > this.entites.Count)
				{
					this.entitiesIndex = 0;
				}
			}
		}

		// Token: 0x04000B39 RID: 2873
		private Radar _radar = new Radar();

		// Token: 0x04000B3A RID: 2874
		private Texture2D _mapTexture;

		// Token: 0x04000B3B RID: 2875
		public Texture2D Background;

		// Token: 0x04000B3C RID: 2876
		public Texture2D[] hostileClassIcons;

		// Token: 0x04000B3D RID: 2877
		public Texture2D[] friendlyClassIcons;

		// Token: 0x04000B3E RID: 2878
		public List<EntityNetPlayer> players;

		// Token: 0x04000B3F RID: 2879
		public List<ClientEntity> entites;

		// Token: 0x04000B40 RID: 2880
		private int entitiesIndex;

		// Token: 0x04000B41 RID: 2881
		private bool showPlayerClassIcon;
	}
}
