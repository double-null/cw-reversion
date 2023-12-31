using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x0200021A RID: 538
internal class GetCurrencyInfo : DatabaseEvent
{
	// Token: 0x060010F8 RID: 4344 RVA: 0x000BD60C File Offset: 0x000BB80C
	public override void Initialize(params object[] args)
	{
		global::Console.print("Get currency", Color.grey);
		Application.ExternalCall("game.setCurrency", new object[0]);
	}

	// Token: 0x060010F9 RID: 4345 RVA: 0x000BD630 File Offset: 0x000BB830
	protected override void OnResponse(string text)
	{
		Debug.Log("on responce get currency: " + text);
		base.OnResponse(text);
		Dictionary<string, object> dictionary = null;
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
			this.OnFail(new Exception("Data Server Error"));
			return;
		}
		if (dictionary != null)
		{
			Main.UserInfo.CurrencyInfo.Convert(dictionary, false);
			Main.UserInfo.CurrencyInfo.ParsePrices((Dictionary<string, object>)dictionary["prices"]);
			Main.UserInfo.CurrencyInfo.ParseMultipliers((Dictionary<string, object>)dictionary["multiplier"]);
		}
	}

	// Token: 0x060010FA RID: 4346 RVA: 0x000BD718 File Offset: 0x000BB918
	protected override void OnFail(Exception e)
	{
		base.OnFail(e);
	}

	// Token: 0x060010FB RID: 4347 RVA: 0x000BD724 File Offset: 0x000BB924
	[Obfuscation(Exclude = true)]
	private void setCurrency(string s)
	{
		Debug.Log("string. set currency: " + s);
		this.OnResponse(s);
	}

	// Token: 0x060010FC RID: 4348 RVA: 0x000BD740 File Offset: 0x000BB940
	[Obfuscation(Exclude = true)]
	private void setCurrency(object s)
	{
		Debug.Log("object. set currency: " + s.ToString());
		this.OnResponse(s.ToString());
	}
}
