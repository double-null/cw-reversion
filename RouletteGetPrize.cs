using System;
using System.Collections.Generic;
using Assets.Scripts.Game.Foundation;
using UnityEngine;

// Token: 0x0200022F RID: 559
internal class RouletteGetPrize : DatabaseEvent
{
	// Token: 0x06001167 RID: 4455 RVA: 0x000C1F18 File Offset: 0x000C0118
	private static int ConvertToTimestamp(DateTime value)
	{
		return (int)(value - RouletteGetPrize.Epoch).TotalSeconds;
	}

	// Token: 0x06001168 RID: 4456 RVA: 0x000C1F3C File Offset: 0x000C013C
	public override void Initialize(params object[] args)
	{
		global::Console.print("Roll", Color.grey);
		HtmlLayer.Request("?action=spinRoulette", new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06001169 RID: 4457 RVA: 0x000C1F7C File Offset: 0x000C017C
	protected override void OnResponse(string text, string url)
	{
		if (string.IsNullOrEmpty(text))
		{
			global::Console.print(new Exception(text));
			this.FailedAction();
			return;
		}
		Dictionary<string, object> dictionary;
		try
		{
			dictionary = ArrayUtility.FromJSON(text, string.Empty);
		}
		catch (Exception ex)
		{
			if (Application.isEditor || Peer.Dedicated)
			{
				global::Console.print(ex.ToString());
				global::Console.print(text);
			}
			this.OnFail(new Exception(Language.DataBaseFailure));
			return;
		}
		if ((int)dictionary["result"] != 0)
		{
			global::Console.print("result is: " + (int)dictionary["result"], Color.red);
			this.OnFail(new Exception(Language.DataBaseFailure));
		}
		else if (dictionary.ContainsKey("index"))
		{
			string text2 = dictionary["index"].ToString();
			RouletteGUI.Result = text2;
			if (text2.Contains("plusone"))
			{
				Main.UserInfo.WonAttempt = true;
			}
			if (text2.Contains("camo"))
			{
				string[] array = dictionary["index"].ToString().Split(new char[]
				{
					'-'
				});
				int item = int.Parse(array[1]);
				int key = int.Parse(array[2]);
				if (Main.UserInfo.Mastering.WeaponsStats.ContainsKey(key))
				{
					Main.UserInfo.Mastering.WeaponsStats[key].Camouflages.Add(item);
				}
			}
			if (text2.Contains("cr"))
			{
				int num = Mathf.Max(Main.UserInfo.currentLevel, 1);
				Main.UserInfo.CR += Globals.I.RouletteInfo.amountCR[Convert.ToInt32(text2.Substring(3))] * num;
			}
			if (text2.Contains("gp"))
			{
				Main.UserInfo.GP += Globals.I.RouletteInfo.amountGP[Convert.ToInt32(text2.Substring(3))];
			}
			if (text2.Contains("mp"))
			{
				Main.UserInfo.Mastering.MasteringPoints += Globals.I.RouletteInfo.amountMP[Convert.ToInt32(text2.Substring(3))];
			}
			if (text2.Contains("sp"))
			{
				Main.UserInfo.SP += Globals.I.RouletteInfo.amountSP;
			}
			if (text2.Contains("gpdiscount"))
			{
				Main.UserInfo.PersonaBankDiscount = new PersonalBankDiscount();
			}
			if (text2.Contains("blackDivision"))
			{
				if (Globals.I.RouletteInfo.BDCurrency == 1)
				{
					Main.UserInfo.CR += Globals.I.RouletteInfo.amountBD;
				}
				if (Globals.I.RouletteInfo.BDCurrency == 2)
				{
					Main.UserInfo.GP += Globals.I.RouletteInfo.amountBD;
				}
			}
			if (text2.Contains("weapon"))
			{
				try
				{
					int num2 = Convert.ToInt32(text2.Substring(7));
					Main.UserInfo.weaponsStates[num2].Unlocked = true;
					Main.UserInfo.weaponsStates[num2].rentEnd = RouletteGetPrize.ConvertToTimestamp(DateTime.UtcNow) + Globals.I.RouletteInfo.WeaponRentTerm * 3600;
				}
				catch (Exception ex2)
				{
					Debug.Log(ex2.ToString());
					Debug.Log("On response: " + text);
				}
			}
			if (text2.Contains("skill"))
			{
				int num3 = Convert.ToInt32(dictionary["index"].ToString().Substring(6));
				Main.UserInfo.skillsInfos[num3].Unlocked = true;
				Main.UserInfo.skillsInfos[num3].rentEnd = RouletteGetPrize.ConvertToTimestamp(DateTime.UtcNow) + Globals.I.RouletteInfo.SkillRentTerm * 3600;
			}
			this.SuccessAction();
		}
	}

	// Token: 0x0600116A RID: 4458 RVA: 0x000C2400 File Offset: 0x000C0600
	protected override void OnFail(Exception e)
	{
		base.OnFail(e);
		this.FailedAction();
	}

	// Token: 0x0400111A RID: 4378
	private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
}
