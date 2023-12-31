using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x0200021F RID: 543
[Obfuscation(Exclude = true, ApplyToMembers = true)]
[AddComponentMenu("Scripts/Game/Events/InitUser")]
internal class InitUser : DatabaseEvent
{
	// Token: 0x0600110E RID: 4366 RVA: 0x000BDD6C File Offset: 0x000BBF6C
	public override void Initialize(params object[] args)
	{
		if (args.Length == 0)
		{
			this.JustUpdateSession = false;
		}
		else
		{
			this.JustUpdateSession = true;
		}
		global::Console.print("InitUser", Color.grey);
		if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--mailru"))
		{
			string actions = "?action=init&platform=mailru&" + Main.AuthData.mailru_request;
			global::Console.print("InitUser Request");
			HtmlLayer.Request(actions, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), "mid=" + Main.UserInfo.socialID, string.Empty);
		}
		else if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--ok"))
		{
			HtmlLayer.Request(string.Concat(new string[]
			{
				"?action=init&platform=ok&auth_sig=",
				Main.AuthData.auth_sig,
				"&logged_user_id=",
				Main.AuthData.logged_user_id,
				"&session_key=",
				Main.AuthData.session_key,
				"&mid=",
				Main.UserInfo.socialID,
				"&ok_sig=",
				Main.AuthData.ok_sig,
				"&application_key=",
				Main.AuthData.app_key,
				"&session_secret_key=",
				Main.AuthData.session_secret_key,
				"&referer=",
				Main.AuthData.referer
			}), new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), "mid=" + Main.UserInfo.socialID, string.Empty);
		}
		else if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--kg"))
		{
			HtmlLayer.Request("?action=init&platform=kg&auth_key=" + Main.AuthData.auth_key + "&mid=" + Main.UserInfo.socialID, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), "mid=" + Main.UserInfo.socialID, string.Empty);
		}
		else if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--ag"))
		{
			HtmlLayer.Request("?action=init&platform=ag&auth_key=" + Main.AuthData.auth_key + "&mid=" + Main.UserInfo.socialID, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), "mid=" + Main.UserInfo.socialID, string.Empty);
		}
		else if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--mc"))
		{
			HtmlLayer.Request("?action=init&platform=mc&auth_key=" + Main.AuthData.auth_key + "&mid=" + Main.UserInfo.socialID, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), "mid=" + Main.UserInfo.socialID, string.Empty);
		}
		else if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--yg"))
		{
			HtmlLayer.Request("?action=init_yg&auth_key=" + Main.AuthData.auth_key + "&mid=" + Main.UserInfo.socialID, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), "mid=" + Main.UserInfo.socialID, string.Empty);
		}
		else if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--fb") || SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--fb-omega"))
		{
			HtmlLayer.Request("?action=init&platform=fb&auth_key=" + Main.AuthData.authentication_key + "&mid=" + Main.UserInfo.socialID, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), "mid=" + Main.UserInfo.socialID, string.Empty);
		}
		else if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--fr"))
		{
			HtmlLayer.SendCompressed("?action=init_fr", InitUser.AuthParams, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail));
		}
		else if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--standalone"))
		{
			bool flag = true;
			if (Application.isEditor)
			{
				flag &= SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--auth");
			}
			string actions2 = string.Concat(new object[]
			{
				"?action=init&platform=standalone&version=",
				CVars.Version,
				"&email=",
				WWW.EscapeURL(Main.StandaloneMail),
				"&password=",
				WWW.EscapeURL(Main.StandalonePass),
				"&savePass=",
				Main.SavePass && flag,
				"&passHashed=",
				Main.PassHashed && flag
			});
			HtmlLayer.Request(actions2, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
		}
		else if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--vk"))
		{
			HtmlLayer.Request("?action=init&platform=vk&auth_key=" + Main.AuthData.auth_key + "&mid=" + Main.UserInfo.socialID, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), "mid=" + Main.UserInfo.socialID, string.Empty);
		}
		else
		{
			HtmlLayer.Request(string.Concat(new string[]
			{
				"?action=init&platform=",
				CVars.realm,
				"&auth_key=",
				Main.AuthData.auth_key,
				"&mid=",
				Main.UserInfo.socialID
			}), new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), "mid=" + Main.UserInfo.socialID, string.Empty);
		}
	}

	// Token: 0x0600110F RID: 4367 RVA: 0x000BE3CC File Offset: 0x000BC5CC
	protected override void OnResponse(string text, string url)
	{
		base.OnResponse(text, url);
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
			this.OnFail(new Exception(string.Concat(new object[]
			{
				"DataServer LoginError \n",
				text,
				"\n",
				ex
			})));
			return;
		}
		int num = (int)dictionary["result"];
		if (num != 0)
		{
			if (CVars.IsStandaloneRealm)
			{
				StandaloneLoginRequest componentInChildren = SingletoneComponent<Main>.Instance.GetComponentInChildren<StandaloneLoginRequest>();
				if (componentInChildren != null)
				{
					componentInChildren.Failresult = num;
					if (componentInChildren.Failresult == 1006)
					{
						componentInChildren.TimeLeft = (int)dictionary["time_left"];
					}
				}
			}
			string text2 = "DataServer LoginError";
			object arg;
			if (dictionary.TryGetValue("message", out arg))
			{
				text2 += arg;
			}
			this.OnFail(new Exception(text2));
			return;
		}
		Main.CheckVersion(dictionary);
		if (CVars.IsStandaloneRealm)
		{
			PlayerPrefs.SetString("Login", Main.StandaloneMail);
			PlayerPrefs.SetInt("SavePass", (!Main.SavePass) ? 0 : 1);
			object obj;
			if (Main.SavePass && dictionary.TryGetValue("sessionHash", out obj))
			{
				PlayerPrefs.SetString("Password", obj as string);
			}
			else
			{
				PlayerPrefs.SetString("Password", string.Empty);
			}
			PlayerPrefs.Save();
		}
		global::Console.print("InitUser Finished", Color.green);
		EventFactory.Call("HidePopup", new Popup(WindowsID.InitUser, Language.CWMainLoading, Language.CWMainInitUserDesc, PopupState.progress, false, false, string.Empty, string.Empty));
		string empty = string.Empty;
		JSON.ReadWrite(dictionary, "hash", ref empty, false);
		Main.UserInfo.HashID = empty;
		global::Console.print((int)dictionary["user_id"]);
		Main.UserInfo.userID = (int)dictionary["user_id"];
		Main.AuthData.sharedSecret = (string)dictionary["ss"];
		HtmlLayer.InitServerTime(dictionary);
		if (!this.JustUpdateSession)
		{
			this.SuccessAction();
		}
	}

	// Token: 0x06001110 RID: 4368 RVA: 0x000BE648 File Offset: 0x000BC848
	protected override void OnFail(Exception e)
	{
		base.OnFail(e);
		this.FailedAction();
	}

	// Token: 0x040010E7 RID: 4327
	private bool JustUpdateSession;

	// Token: 0x040010E8 RID: 4328
	public static string AuthParams = string.Empty;
}
