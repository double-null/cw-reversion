using System;
using UnityEngine;

namespace ClanSystemGUI
{
	// Token: 0x02000110 RID: 272
	internal class ClanSkills : AbstractClanPage
	{
		// Token: 0x06000739 RID: 1849 RVA: 0x00041628 File Offset: 0x0003F828
		public override void OnStart()
		{
			ClanSkills.requestSended = false;
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00041630 File Offset: 0x0003F830
		public override void OnGUI()
		{
			if (this._shortClanInfo == null || !ClanSkills.requestSended)
			{
				this._shortClanInfo = new ShortClanInfo();
				this._shortClanInfo.clanID = Main.UserInfo.clanID;
				this._shortClanInfo.ReloadAdditionInfo(delegate
				{
					this.clanInfo = this._shortClanInfo.DetailInfo;
				}, delegate
				{
				}, true);
				ClanSkills.requestSended = true;
			}
			this.DrawGrid(ref this.clanSkills, ref ClanSystemWindow.I.Textures.clanSkillsGrid);
			this.DrawBalanceInfo(445f, 80f);
			this.DrawSkillInfo(385f, 300f, this.currentSkill);
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x000416F0 File Offset: 0x0003F8F0
		public override void OnUpdate()
		{
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x000416F4 File Offset: 0x0003F8F4
		private void DrawGrid(ref int[,] grid, ref Texture2D TextureGrid)
		{
			GUI.DrawTexture(new Rect(50f, 100f, (float)TextureGrid.width, (float)TextureGrid.height), TextureGrid);
			this.DrawClanSkills(ref grid, ref this.selectedSkill);
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00041734 File Offset: 0x0003F934
		private void DrawClanSkills(ref int[,] grid, ref int[] selectedSkill)
		{
			this.showHint = false;
			int num = 50;
			int num2 = 100;
			int width = ClanSystemWindow.I.Textures.clanSkills[0].width;
			int height = ClanSystemWindow.I.Textures.clanSkills[0].height;
			for (int i = 0; i < 9; i++)
			{
				for (int j = 0; j < 7; j++)
				{
					if (grid[i, j] != -1 && grid[i, j] < ClanSystemWindow.I.Textures.clanSkills.Length)
					{
						int num3 = num - 24 + (height + 10) * j;
						int num4 = num2 - 24 + height * i;
						GUI.DrawTexture(new Rect((float)num3, (float)num4, (float)width, (float)height), ClanSystemWindow.I.Textures.clanSkills[grid[i, j]]);
						if ((selectedSkill[0] != i || selectedSkill[1] != j) && !this.isMayUnlock(grid[i, j]) && !this.isSkillUnlock(grid[i, j]))
						{
							GUI.DrawTexture(new Rect((float)num3, (float)(num4 + 1), (float)CarrierGUI.I.Class_skill_button[4].width, (float)CarrierGUI.I.Class_skill_button[4].height), CarrierGUI.I.Class_skill_button[4]);
						}
						else if (!Main.UserInfo.ClanSkillsInfos[grid[i, j]].IsUnlocked && this.isMayUnlock(grid[i, j]))
						{
							MainGUI.Instance.color = Colors.alpha(Color.white, CarrierGUI.I.visibility * Mathf.Abs(Mathf.Sin(Time.realtimeSinceStartup)));
							GUI.DrawTexture(new Rect((float)num3, (float)(num4 + 1), (float)CarrierGUI.I.Class_skill_button[7].width, (float)CarrierGUI.I.Class_skill_button[7].height), CarrierGUI.I.Class_skill_button[7]);
							MainGUI.Instance.color = Colors.alpha(Color.white, CarrierGUI.I.visibility);
						}
						if (Main.UserInfo.ClanSkillsInfos[grid[i, j]].IsPremium)
						{
							GUI.DrawTexture(new Rect((float)num3, (float)(num4 + 1), (float)CarrierGUI.I.Class_skill_button[2].width, (float)CarrierGUI.I.Class_skill_button[2].height), CarrierGUI.I.Class_skill_button[2]);
						}
						if (Main.UserInfo.ClanSkillsInfos[grid[i, j]].IsUnlocked)
						{
							GUI.DrawTexture(new Rect((float)num3, (float)(num4 + 1), (float)CarrierGUI.I.Class_skill_button[5].width, (float)CarrierGUI.I.Class_skill_button[5].height), CarrierGUI.I.Class_skill_button[5]);
						}
						this.btnState = MainGUI.Instance.Button(new Vector2((float)num3, (float)num4), null, CarrierGUI.I.Class_skill_button[1], null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null);
						if (this.btnState.Clicked)
						{
							selectedSkill[0] = i;
							selectedSkill[1] = j;
							this.currentSkill = grid[i, j];
						}
						if (this.btnState.Hover)
						{
							if (!Main.UserInfo.ClanSkillsInfos[grid[i, j]].IsUnlocked)
							{
								GUI.DrawTexture(new Rect((float)(num3 + 28), (float)(num4 + 24), (float)CarrierGUI.I.Class_skill_button[6].width, (float)CarrierGUI.I.Class_skill_button[6].height), CarrierGUI.I.Class_skill_button[6]);
							}
							if (Main.UserInfo.ClanSkillsInfos[grid[i, j]].RentEnd > 0)
							{
								this.onHoverSkillID = (int)Main.UserInfo.ClanSkillsInfos[grid[i, j]].Type;
								this.hintXpos = num3;
								this.hintYpos = num4;
								this.showHint = true;
							}
						}
					}
				}
			}
			if (selectedSkill[0] != -1 && selectedSkill[1] != -1)
			{
				GUI.DrawTexture(new Rect((float)(num - 58 + (width + 10) * selectedSkill[1]), (float)(num2 - 58 + height * selectedSkill[0]), (float)CarrierGUI.I.Class_skill_button[3].width, (float)CarrierGUI.I.Class_skill_button[3].height), CarrierGUI.I.Class_skill_button[3]);
			}
			if (this.showHint)
			{
				this.ShowHint(MainGUI.Instance.SecondsToStringHHHMMSS(Main.UserInfo.ClanSkillsInfos[this.onHoverSkillID].RentEnd - HtmlLayer.serverUtc));
			}
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00041C18 File Offset: 0x0003FE18
		private bool isMayUnlock(int i)
		{
			if (Main.UserInfo.ClanSkillsInfos[i].Requirements == null)
			{
				return true;
			}
			if (Main.UserInfo.ClanSkillsInfos[i].Requirements.Length > 0)
			{
				for (int j = 0; j < Main.UserInfo.ClanSkillsInfos[i].Requirements.Length; j++)
				{
					if (Main.UserInfo.ClanSkillsInfos[(int)Main.UserInfo.ClanSkillsInfos[i].Requirements[j]].IsUnlocked)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00041CA8 File Offset: 0x0003FEA8
		private bool isSkillUnlock(int i)
		{
			return i < Main.UserInfo.ClanSkillsInfos.Length && Main.UserInfo.ClanSkillsInfos[i].IsUnlocked;
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00041CD0 File Offset: 0x0003FED0
		private bool isMayBuy(int i)
		{
			return Main.UserInfo.ClanSkillsInfos[i].PriceCR <= Main.UserInfo.CR && Main.UserInfo.ClanSkillsInfos[i].PriceGP <= Main.UserInfo.GP;
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00041D34 File Offset: 0x0003FF34
		private void ShowHint(string str)
		{
			GUI.DrawTexture(new Rect((float)(this.hintXpos + 48), (float)(this.hintYpos + 5), MainGUI.Instance.CalcWidth(str, MainGUI.Instance.fontTahoma, 9) - 3f, 11f), MainGUI.Instance.black);
			GUI.Label(new Rect((float)(this.hintXpos + 48), (float)(this.hintYpos + 5), 100f, 10f), str, ClanSystemWindow.I.Styles.styleWhiteTahomaLable);
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00041DC4 File Offset: 0x0003FFC4
		private void DrawBalanceInfo(float xPos, float yPos)
		{
			GUI.DrawTexture(new Rect(xPos, yPos, (float)ClanSystemWindow.I.Textures.clanBalanceBack.width, (float)ClanSystemWindow.I.Textures.clanBalanceBack.height), ClanSystemWindow.I.Textures.clanBalanceBack);
			GUI.Label(new Rect(xPos + 40f, yPos + 5f, 200f, 20f), Language.ClansBalance, ClanSystemWindow.I.Styles.styleWhiteLabel14);
			if (this.refreshTimer < Time.realtimeSinceStartup && GUI.Button(new Rect(xPos + 177f, yPos + 3.5f, 28f, 23f), string.Empty, ClanSystemWindow.I.Styles.styleJoinRefreshBtn))
			{
				this.refreshTimer = Time.realtimeSinceStartup + 10f;
				ClanSkills.requestSended = false;
			}
			GUI.DrawTexture(new Rect(xPos + 182f, yPos + 5.5f, (float)ClanSystemWindow.I.Textures.refreshIcon.width, (float)ClanSystemWindow.I.Textures.refreshIcon.height), ClanSystemWindow.I.Textures.refreshIcon);
			if (Main.IsGameLoaded || Main.UserInfo.currentLevel < 20)
			{
				GUI.enabled = false;
			}
			if (GUI.Button(new Rect(xPos + 202f, yPos + 3f, 37f, 26f), string.Empty, ClanSystemWindow.I.Styles.styleAddBalanceBtn))
			{
				if (!PopupGUI.IsAnyPopupShow)
				{
					ClanSystemWindow.I.crToClan = 0;
					ClanSystemWindow.I.gpToClan = 0;
				}
				EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.ClansPopupBalance, string.Empty, delegate()
				{
					this.clanInfo.clanCR += (long)ClanSystemWindow.I.crToClan;
					this.clanInfo.clanGP += ClanSystemWindow.I.gpToClan;
					ClanSystemWindow.I.crToClan = 0;
					ClanSystemWindow.I.gpToClan = 0;
				}, PopupState.fillUpClanBalance, false, true, new object[0]));
			}
			GUI.enabled = true;
			ClanSystemWindow.I.Styles.styleWhiteLabel16.fontSize = 18;
			ClanSystemWindow.I.Styles.styleWhiteLabel16.alignment = TextAnchor.MiddleRight;
			GUI.Label(new Rect(xPos, yPos + 32f, 95f, 20f), Helpers.SeparateNumericString(this.clanInfo.clanCR.ToString()), ClanSystemWindow.I.Styles.styleWhiteLabel16);
			GUI.DrawTexture(new Rect(xPos + 100f, yPos + 30f, (float)MainGUI.Instance.crIcon.width, (float)MainGUI.Instance.crIcon.height), MainGUI.Instance.crIcon);
			GUI.Label(new Rect(xPos, yPos + 56f, 95f, 20f), Helpers.SeparateNumericString(this.clanInfo.clanGP.ToString()), ClanSystemWindow.I.Styles.styleWhiteLabel16);
			GUI.DrawTexture(new Rect(xPos + 101f, yPos + 54f, (float)MainGUI.Instance.gldIcon.width, (float)MainGUI.Instance.gldIcon.height), MainGUI.Instance.gldIcon);
			ClanSystemWindow.I.Styles.styleWhiteLabel16.fontSize = 38;
			GUI.Label(new Rect(xPos + 77f, yPos + 33f, 100f, 38f), Helpers.SeparateNumericString(this.clanInfo.clanBG.ToString()), ClanSystemWindow.I.Styles.styleWhiteLabel16);
			GUI.DrawTexture(new Rect(xPos + 180f, yPos + 35f, (float)MainGUI.Instance.bgIcon.width, (float)MainGUI.Instance.bgIcon.height), MainGUI.Instance.bgIcon);
			ClanSystemWindow.I.Styles.styleWhiteLabel16.fontSize = 16;
			ClanSystemWindow.I.Styles.styleWhiteLabel16.alignment = TextAnchor.MiddleLeft;
			if (Main.IsGameLoaded)
			{
				Helpers.Hint(new Rect(xPos + 202f, yPos + 3f, 37f, 26f), Language.ClansExpSliderInGameHint, ClanSystemWindow.I.Styles.styleWhiteLabel14MC, Helpers.HintAlignment.Rigth, 0f, 40f);
			}
			else if (Main.UserInfo.currentLevel < 20)
			{
				Helpers.Hint(new Rect(xPos + 202f, yPos + 3f, 37f, 26f), Language.ClansBalanceHint, ClanSystemWindow.I.Styles.styleWhiteLabel14MC, Helpers.HintAlignment.Rigth, 0f, 40f);
			}
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0004224C File Offset: 0x0004044C
		private void DrawSkillInfo(float xPos, float yPos, int currentSkill)
		{
			int width = ClanSystemWindow.I.Textures.clanSkills[0].width;
			int height = ClanSystemWindow.I.Textures.clanSkills[0].height;
			GUI.DrawTexture(new Rect(xPos, yPos, (float)ClanSystemWindow.I.Textures.clanSkillsDescriptionBack.width, (float)ClanSystemWindow.I.Textures.clanSkillsDescriptionBack.height), ClanSystemWindow.I.Textures.clanSkillsDescriptionBack);
			if (currentSkill == -1)
			{
				return;
			}
			ClanSystemWindow.I.Styles.styleWhiteLabel16.alignment = TextAnchor.MiddleCenter;
			GUI.Label(new Rect(xPos + 70f, yPos + 2f, 215f, 20f), Main.UserInfo.ClanSkillsInfos[currentSkill].Name, ClanSystemWindow.I.Styles.styleWhiteLabel16);
			ClanSystemWindow.I.Styles.styleWhiteLabel16.alignment = TextAnchor.MiddleLeft;
			ClanSystemWindow.I.Styles.styleWhiteLabel14.fontSize = 12;
			this.tmpStyle.textColor = ClanSystemWindow.I.Styles.styleWhiteLabel14.normal.textColor;
			ClanSystemWindow.I.Styles.styleWhiteLabel14.normal.textColor = Colors.RadarGreen;
			GUI.Label(new Rect(xPos + 70f, yPos + 95f, 50f, 20f), Language.CarrBonus.ToUpper(), ClanSystemWindow.I.Styles.styleWhiteLabel14);
			ClanSystemWindow.I.Styles.styleWhiteLabel14.normal.textColor = Colors.RadarBlue;
			GUI.Label(new Rect(xPos + 70f, yPos + 155f, 50f, 20f), Language.Cost.ToUpper(), ClanSystemWindow.I.Styles.styleWhiteLabel14);
			ClanSystemWindow.I.Styles.styleWhiteLabel14.fontSize = 14;
			ClanSystemWindow.I.Styles.styleWhiteLabel14.normal.textColor = this.tmpStyle.textColor;
			ClanSystemWindow.I.Styles.styleWhiteLabel14.wordWrap = true;
			ClanSystemWindow.I.Styles.styleWhiteLabel14.alignment = TextAnchor.UpperLeft;
			GUI.Label(new Rect(xPos + 70f, yPos + 25f, 210f, 100f), Main.UserInfo.ClanSkillsInfos[currentSkill].Function, ClanSystemWindow.I.Styles.styleWhiteLabel14);
			GUI.Label(new Rect(xPos + 70f, yPos + 115f, 175f, 40f), Main.UserInfo.ClanSkillsInfos[currentSkill].Bonus, ClanSystemWindow.I.Styles.styleWhiteLabel14);
			ClanSystemWindow.I.Styles.styleWhiteLabel14.wordWrap = false;
			ClanSystemWindow.I.Styles.styleWhiteLabel14.alignment = TextAnchor.MiddleLeft;
			GUI.DrawTexture(new Rect(xPos + 245f, yPos + 104f, (float)width, (float)height), ClanSystemWindow.I.Textures.clanSkills[currentSkill]);
			if (!Main.UserInfo.ClanSkillsInfos[currentSkill].IsUnlocked)
			{
				if (Main.UserInfo.ClanSkillsInfos[currentSkill].IsPremium)
				{
					if (Main.UserInfo.ClanSkillsInfos[currentSkill].RentPrice == null)
					{
						GUI.Label(new Rect(xPos + 65f, yPos + 187f, 100f, 25f), Helpers.SeparateNumericString(Main.UserInfo.ClanSkillsInfos[currentSkill].PriceGP.Value.ToString("F0")), ClanSystemWindow.I.Styles.styleGoldLabel);
					}
					else
					{
						GUI.Label(new Rect(xPos + 65f, yPos + 187f, 100f, 25f), Helpers.SeparateNumericString(Main.UserInfo.ClanSkillsInfos[currentSkill].RentPrice[0].ToString("F0")), ClanSystemWindow.I.Styles.styleGoldLabel);
					}
					GUI.DrawTexture(new Rect(xPos + 170f, yPos + 188f, (float)MainGUI.Instance.crIcon.width, (float)MainGUI.Instance.crIcon.height), MainGUI.Instance.gldIcon);
				}
				else
				{
					ClanSystemWindow.I.Styles.styleWhiteLabel16.alignment = TextAnchor.MiddleRight;
					ClanSystemWindow.I.Styles.styleWhiteLabel16.fontSize = 20;
					if (Main.UserInfo.ClanSkillsInfos[currentSkill].RentPrice == null)
					{
						GUI.Label(new Rect(xPos + 65f, yPos + 187f, 100f, 25f), Helpers.SeparateNumericString(Main.UserInfo.ClanSkillsInfos[currentSkill].PriceCR.Value.ToString("F0")), ClanSystemWindow.I.Styles.styleWhiteLabel16);
					}
					else
					{
						GUI.Label(new Rect(xPos + 65f, yPos + 187f, 100f, 25f), Helpers.SeparateNumericString(Main.UserInfo.ClanSkillsInfos[currentSkill].RentPrice[0].ToString("F0")), ClanSystemWindow.I.Styles.styleWhiteLabel16);
					}
					GUI.DrawTexture(new Rect(xPos + 170f, yPos + 188f, (float)MainGUI.Instance.crIcon.width, (float)MainGUI.Instance.crIcon.height), MainGUI.Instance.crIcon);
					ClanSystemWindow.I.Styles.styleWhiteLabel16.alignment = TextAnchor.MiddleLeft;
					ClanSystemWindow.I.Styles.styleWhiteLabel16.fontSize = 16;
				}
			}
			if (!Main.UserInfo.ClanSkillsInfos[currentSkill].IsUnlocked && this.isMayUnlock(currentSkill) && (Main.UserInfo.IsClanLeader || Main.UserInfo.ClanRole == Role.lt))
			{
				if (!Main.IsGameLoaded)
				{
					if (GUI.Button(new Rect(xPos + 192f, yPos + 176f, 104f, 48f), Language.Unlock, ClanSystemWindow.I.Styles.styleUnlockSkillBtn))
					{
						object[] args = new object[]
						{
							currentSkill,
							this.clanInfo
						};
						EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Main.UserInfo.ClanSkillsInfos[currentSkill].Name, string.Empty, delegate()
						{
							if (Main.UserInfo.ClanSkillsInfos[currentSkill].RentPrice != null)
							{
								if (Main.UserInfo.ClanSkillsInfos[currentSkill].IsPremium)
								{
									this.clanInfo.clanGP -= ClanSkills.Rent_price;
								}
								else
								{
									this.clanInfo.clanCR -= (long)ClanSkills.Rent_price;
								}
							}
							this.clanInfo.clanCR -= (long)Main.UserInfo.ClanSkillsInfos[currentSkill].PriceCR;
							DetailClanInfo detailClanInfo = this.clanInfo;
							detailClanInfo.clanGP -= Main.UserInfo.ClanSkillsInfos[currentSkill].PriceGP;
						}, PopupState.unlockClanSkill, false, true, args));
					}
				}
				else if (this.isMayUnlock(currentSkill))
				{
					ClanSystemWindow.I.Styles.styleRedLabel.alignment = TextAnchor.MiddleCenter;
					GUI.Label(new Rect(xPos + 200f, yPos + 180f, 80f, 40f), Language.CarrYouInTheBattle, ClanSystemWindow.I.Styles.styleRedLabel);
					ClanSystemWindow.I.Styles.styleRedLabel.alignment = TextAnchor.MiddleLeft;
				}
			}
			else if (Main.UserInfo.ClanSkillsInfos[currentSkill].IsUnlocked && Main.UserInfo.ClanSkillsInfos[currentSkill].RentTime == null)
			{
				GUI.Label(new Rect(xPos + 70f, yPos + 190f, 100f, 20f), Language.CarrUnlocked, ClanSystemWindow.I.Styles.styleWhiteLabel14);
			}
			else if (Main.UserInfo.ClanSkillsInfos[currentSkill].RentTime != null && Main.UserInfo.ClanSkillsInfos[currentSkill].IsUnlocked)
			{
				int seconds = Main.UserInfo.ClanSkillsInfos[currentSkill].RentEnd - HtmlLayer.serverUtc;
				GUI.Label(new Rect(xPos + 70f, yPos + 190f, 100f, 20f), Language.CarrRentTime, ClanSystemWindow.I.Styles.styleWhiteLabel14);
				GUI.DrawTexture(new Rect(xPos + 165f, yPos + 185f, (float)CarrierGUI.I.stats[1].width, (float)CarrierGUI.I.stats[1].height), CarrierGUI.I.stats[1]);
				GUI.Label(new Rect(xPos + 200f, yPos + 190f, 100f, 20f), MainGUI.Instance.SecondsToStringHHHMMSS(seconds), ClanSystemWindow.I.Styles.styleWhiteLabel14);
			}
			else if (Main.UserInfo.IsClanLeader)
			{
				GUI.enabled = false;
				GUI.Button(new Rect(xPos + 192f, yPos + 176f, 104f, 48f), Language.Unlock, ClanSystemWindow.I.Styles.styleUnlockSkillBtn);
				GUI.enabled = true;
			}
			else
			{
				ClanSystemWindow.I.Styles.styleRedLabel.alignment = TextAnchor.MiddleCenter;
				ClanSystemWindow.I.Styles.styleRedLabel.fontSize = 12;
				GUI.Label(new Rect(xPos + 200f, yPos + 180f, 80f, 40f), Language.ClansSkillAccess, ClanSystemWindow.I.Styles.styleRedLabel);
				ClanSystemWindow.I.Styles.styleRedLabel.fontSize = 14;
				ClanSystemWindow.I.Styles.styleRedLabel.alignment = TextAnchor.MiddleLeft;
			}
		}

		// Token: 0x04000801 RID: 2049
		public static int Rent_price;

		// Token: 0x04000802 RID: 2050
		public static int Rent_end;

		// Token: 0x04000803 RID: 2051
		private bool showHint;

		// Token: 0x04000804 RID: 2052
		private int hintXpos;

		// Token: 0x04000805 RID: 2053
		private int hintYpos;

		// Token: 0x04000806 RID: 2054
		private int onHoverSkillID;

		// Token: 0x04000807 RID: 2055
		private int[] selectedSkill = new int[]
		{
			-1,
			-1
		};

		// Token: 0x04000808 RID: 2056
		private ButtonState btnState;

		// Token: 0x04000809 RID: 2057
		private int currentSkill = -1;

		// Token: 0x0400080A RID: 2058
		private float refreshTimer;

		// Token: 0x0400080B RID: 2059
		public static bool requestSended;

		// Token: 0x0400080C RID: 2060
		private DetailClanInfo clanInfo = new DetailClanInfo();

		// Token: 0x0400080D RID: 2061
		private ShortClanInfo _shortClanInfo;

		// Token: 0x0400080E RID: 2062
		private GUIStyleState tmpStyle = new GUIStyleState();

		// Token: 0x0400080F RID: 2063
		private int[,] clanSkills = new int[,]
		{
			{
				30,
				-1,
				3,
				6,
				24,
				-1,
				-1
			},
			{
				0,
				-1,
				4,
				7,
				25,
				-1,
				-1
			},
			{
				-1,
				-1,
				5,
				8,
				26,
				-1,
				-1
			},
			{
				-1,
				-1,
				-1,
				-1,
				27,
				-1,
				-1
			},
			{
				1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			},
			{
				2,
				9,
				12,
				15,
				18,
				21,
				-1
			},
			{
				31,
				10,
				13,
				16,
				19,
				22,
				-1
			},
			{
				29,
				11,
				14,
				17,
				20,
				23,
				-1
			},
			{
				-1,
				-1,
				-1,
				-1,
				-1,
				-1,
				-1
			}
		};
	}
}
