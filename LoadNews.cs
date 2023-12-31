using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x02000221 RID: 545
[Obfuscation(Exclude = true, ApplyToMembers = true)]
[AddComponentMenu("Scripts/Game/Events/LoadNews")]
internal class LoadNews : DatabaseEvent
{
	// Token: 0x06001117 RID: 4375 RVA: 0x000BE8C8 File Offset: 0x000BCAC8
	public override void Initialize(params object[] args)
	{
		if (!Main.IsGameLoaded)
		{
			HtmlLayer.Request("newsfeed.php?action=get&social_id=" + Main.UserInfo.socialID, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
		}
	}

	// Token: 0x06001118 RID: 4376 RVA: 0x000BE91C File Offset: 0x000BCB1C
	[Obfuscation(Exclude = true)]
	protected override void OnResponse(string text, string url)
	{
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
			this.OnFail(new Exception(string.Concat(new string[]
			{
				"Data Server Error \n url: ",
				url,
				"\nJSON = \n",
				text,
				"\n "
			}), ex));
			return;
		}
		if ((int)dictionary["result"] == 2)
		{
			this.OnFail(new Exception("Data Server Error " + dictionary["error_msg"].ToString()));
			return;
		}
		if ((int)dictionary["result"] == 1)
		{
			string desc = (string)dictionary["text"];
			LoadNews.news_id_cached = (string)dictionary["news_id"];
			if (!PopupGUI.showingDaliyBonus)
			{
				EventFactory.Call("ShowPopup", new Popup(WindowsID.News, Language.ProjectNews, desc, PopupState.news, false, true, "MarkNewsRead", string.Empty));
			}
		}
		base.Invoke("UpdateNews", 500f);
	}

	// Token: 0x06001119 RID: 4377 RVA: 0x000BEA78 File Offset: 0x000BCC78
	[Obfuscation(Exclude = true)]
	private void UpdateNews()
	{
		this.Initialize(new object[0]);
	}

	// Token: 0x040010EA RID: 4330
	public static string news_id_cached = string.Empty;
}
