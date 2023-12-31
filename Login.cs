using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using JsonFx.Json;
using UnityEngine;

// Token: 0x02000224 RID: 548
[AddComponentMenu("Scripts/Game/Events/Login")]
internal class Login : DatabaseEvent
{
	// Token: 0x0600112C RID: 4396 RVA: 0x000C036C File Offset: 0x000BE56C
	public override void Initialize(params object[] args)
	{
		if (!LoadProfile.splashScreenOn)
		{
			EventFactory.Call("ShowPopup", new Popup(WindowsID.Login, Language.CWMainLoading, Language.CWMainLoginDesc, PopupState.progress, false, false, string.Empty, string.Empty));
		}
		global::Console.print("Login", Color.grey);
		EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.StandaloneLoginCaption, string.Empty, PopupState.StandaloneLogin, false, true, "ExitGame", string.Empty));
	}

	// Token: 0x0600112D RID: 4397 RVA: 0x000C03E4 File Offset: 0x000BE5E4
	protected override void OnResponse(string str)
	{
		Dictionary<string, object> dictionary;
		try
		{
			if (string.IsNullOrEmpty(str))
			{
				dictionary = new Dictionary<string, object>();
			}
			else
			{
				dictionary = (Dictionary<string, object>)JsonReader.Deserialize(str);
			}
		}
		catch (Exception ex)
		{
			dictionary = new Dictionary<string, object>();
		}
		object obj = null;
		if (dictionary.TryGetValue("login", out obj))
		{
			str = (string)obj;
		}
		if (dictionary.TryGetValue("friends", out obj))
		{
			Login.FriendListRaw = (string)obj;
		}
		base.OnResponse(str);
		Main.AuthData = new AuthData();
		try
		{
			Main.AuthData.mailru_request = str;
			string[] array = str.Split(new char[]
			{
				'&'
			});
			foreach (string text in array)
			{
				if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--mailru"))
				{
					if (text.Contains("is_app_user"))
					{
						array = text.Split(new char[]
						{
							'='
						});
						Main.AuthData.is_app_user = Convert.ToInt32(array[1]);
					}
					if (text.Contains("session_key"))
					{
						array = text.Split(new char[]
						{
							'='
						});
						Main.AuthData.session_key = array[1];
					}
					if (text.Contains("vid"))
					{
						array = text.Split(new char[]
						{
							'='
						});
						global::Console.print(array[1]);
						Main.UserInfo.socialID = array[1];
						Main.AuthData.vid = Main.UserInfo.socialID;
					}
					if (text.Contains("oid"))
					{
						array = text.Split(new char[]
						{
							'='
						});
						Main.AuthData.oid = array[1];
					}
					if (text.Contains("app_id"))
					{
						array = text.Split(new char[]
						{
							'='
						});
						Main.AuthData.app_id = Convert.ToInt32(array[1]);
					}
					if (text.Contains("authentication_key"))
					{
						array = text.Split(new char[]
						{
							'='
						});
						Main.AuthData.authentication_key = array[1];
					}
					if (text.Contains("session_expire"))
					{
						array = text.Split(new char[]
						{
							'='
						});
						Main.AuthData.session_expire = (long)Convert.ToInt32(array[1]);
					}
					if (text.Contains("ext_perm"))
					{
						array = text.Split(new char[]
						{
							'='
						});
						Main.AuthData.ext_perm = array[1];
					}
					if (text.Contains("sig"))
					{
						array = text.Split(new char[]
						{
							'='
						});
						Main.AuthData.sig = array[1];
					}
					if (text.Contains("window_id"))
					{
						array = text.Split(new char[]
						{
							'='
						});
						Main.AuthData.window_id = array[1];
					}
					if (text.Contains("partner_id"))
					{
						array = text.Split(new char[]
						{
							'='
						});
						Main.AuthData.partnerID = array[1];
					}
					if (text.Contains("referer_type"))
					{
						array = text.Split(new char[]
						{
							'='
						});
						Main.AuthData.referer_type = array[1];
					}
					if (text.Contains("referer_id"))
					{
						array = text.Split(new char[]
						{
							'='
						});
						Main.AuthData.referer_id = array[1];
					}
					if (text.Contains("view"))
					{
						array = text.Split(new char[]
						{
							'='
						});
						Main.AuthData.view = array[1];
					}
				}
				else if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--ok"))
				{
					if (text.Contains("logged_user_id"))
					{
						array = text.Split(new char[]
						{
							'='
						});
						Main.UserInfo.socialID = array[1];
						Main.AuthData.logged_user_id = array[1];
					}
					if (text.Contains("auth_sig"))
					{
						array = text.Split(new char[]
						{
							'='
						});
						Main.AuthData.auth_sig = array[1];
					}
					if (text.Contains("session_key"))
					{
						array = text.Split(new char[]
						{
							'='
						});
						Main.AuthData.session_key = array[1];
					}
					if (text.Contains("sig"))
					{
						array = text.Split(new char[]
						{
							'='
						});
						Main.AuthData.ok_sig = array[1];
					}
					if (text.Contains("session_secret_key"))
					{
						array = text.Split(new char[]
						{
							'='
						});
						Main.AuthData.session_secret_key = array[1];
					}
					if (text.Contains("application_key"))
					{
						array = text.Split(new char[]
						{
							'='
						});
						Main.AuthData.app_key = array[1];
					}
					if (text.Contains("referer"))
					{
						array = text.Split(new char[]
						{
							'='
						});
						Main.AuthData.referer = array[1];
					}
				}
				else if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--fb") || SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--fb-omega"))
				{
					if (text.Contains("signed_request"))
					{
						array = text.Split(new char[]
						{
							'='
						});
						Main.AuthData.authentication_key = array[1];
					}
				}
				else if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--kg"))
				{
					if (text.Contains("auth_key"))
					{
						array = text.Split(new char[]
						{
							'='
						});
						Main.AuthData.auth_key = array[1];
					}
					if (text.Contains("user_id"))
					{
						array = text.Split(new char[]
						{
							'='
						});
						Main.UserInfo.socialID = array[1];
					}
				}
				else if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--ag"))
				{
					if (text.Contains("user_id"))
					{
						array = text.Split(new char[]
						{
							'='
						});
						Main.UserInfo.socialID = array[1];
					}
					if (text.Contains("api_key"))
					{
						array = text.Split(new char[]
						{
							'='
						});
						Main.AuthData.app_key = array[1];
					}
					if (text.Contains("auth_token"))
					{
						array = text.Split(new char[]
						{
							'='
						});
						Main.AuthData.auth_key = array[1];
					}
				}
				else if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--mc"))
				{
					if (text.Contains("user_id"))
					{
						array = text.Split(new char[]
						{
							'='
						});
						Main.UserInfo.socialID = array[1];
					}
					if (text.Contains("access_token"))
					{
						array = text.Split(new char[]
						{
							'='
						});
						Main.AuthData.auth_key = array[1];
					}
				}
				else if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--yg"))
				{
					if (text.Contains("user_id"))
					{
						array = text.Split(new char[]
						{
							'='
						});
						Main.UserInfo.socialID = array[1];
					}
					if (text.Contains("access_token"))
					{
						array = text.Split(new char[]
						{
							'='
						});
						Main.AuthData.auth_key = array[1];
					}
				}
				else
				{
					if (text.Contains("mid"))
					{
						array = text.Split(new char[]
						{
							'='
						});
						Main.UserInfo.socialID = array[1];
					}
					if (text.Contains("auth_key"))
					{
						array = text.Split(new char[]
						{
							'='
						});
						Main.AuthData.auth_key = array[1];
					}
				}
			}
		}
		catch (Exception e)
		{
			this.OnFail(e);
			return;
		}
		EventFactory.Call("HidePopup", new Popup(WindowsID.Login, Language.CWMainLoading, Language.CWMainLoginFinishedDesc, PopupState.progress, false, false, string.Empty, string.Empty));
		global::Console.print("Login Finished", Color.green);
		global::Console.print("InitUser start");
		Main.AddDatabaseRequestCallBack<InitUser>(new DatabaseEvent.SomeAction(this.OnInitUserSuccess), new DatabaseEvent.SomeAction(this.OnInitUserFail), new object[0]);
	}

	// Token: 0x0600112E RID: 4398 RVA: 0x000C0CD4 File Offset: 0x000BEED4
	private void OnInitUserSuccess()
	{
		Debug.Log("InitUserComplete");
		if (Application.isWebPlayer)
		{
			Application.ExternalCall("appFriends", new object[0]);
		}
		if (!WWWUtil.contentIpLoaded)
		{
			Main.AddDatabaseRequest<GetContentIP>(new object[0]);
		}
		if (!Peer.Dedicated)
		{
			Main.AddDatabaseRequestCallBack<PassFriendList>(delegate
			{
				Main.AddDatabaseRequest<LoadProfile>(new object[0]);
			}, delegate
			{
				Main.AddDatabaseRequest<LoadProfile>(new object[0]);
			}, new object[]
			{
				Login.FriendListRaw
			});
		}
		else
		{
			LoadProfile.ServerInit();
		}
		this.SuccessAction();
		if (CVars.realm == "fb")
		{
			this.getGurrency(JsonWriter.Serialize(Globals.I.Bank));
		}
	}

	// Token: 0x0600112F RID: 4399 RVA: 0x000C0DB0 File Offset: 0x000BEFB0
	private void OnInitUserFail()
	{
		global::Console.print("InitUser failed");
		this.FailedAction();
	}

	// Token: 0x06001130 RID: 4400 RVA: 0x000C0DC8 File Offset: 0x000BEFC8
	private void getGurrency(string json)
	{
		global::Console.print("Get currency", Color.grey);
		Application.ExternalCall("game.setCurrency", new object[]
		{
			json
		});
	}

	// Token: 0x06001131 RID: 4401 RVA: 0x000C0DF0 File Offset: 0x000BEFF0
	[Obfuscation(Exclude = true)]
	private void setCurrency(object s)
	{
		this.parseCurrency(s.ToString());
	}

	// Token: 0x06001132 RID: 4402 RVA: 0x000C0E00 File Offset: 0x000BF000
	private void parseCurrency(string text)
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
			return;
		}
		if (dictionary != null)
		{
			Main.UserInfo.CurrencyInfo.Convert(dictionary, false);
			Main.UserInfo.CurrencyInfo.ParsePrices((Dictionary<string, object>)dictionary["prices"]);
			Main.UserInfo.CurrencyInfo.ParseMultipliers((Dictionary<string, object>)dictionary["multiplier"]);
		}
	}

	// Token: 0x06001133 RID: 4403 RVA: 0x000C0EC4 File Offset: 0x000BF0C4
	[Obfuscation(Exclude = true)]
	public void BalanceChanged(object balance)
	{
		Main.Reload();
	}

	// Token: 0x06001134 RID: 4404 RVA: 0x000C0ECC File Offset: 0x000BF0CC
	[Obfuscation(Exclude = true)]
	public void BalanceChangedMailru(int balance)
	{
		global::Console.print("SN balance change request");
		Main.AddDatabaseRequest<BalanceChangedMailruRequest>(new object[0]);
	}

	// Token: 0x06001135 RID: 4405 RVA: 0x000C0EE4 File Offset: 0x000BF0E4
	[Obfuscation(Exclude = true)]
	public void LoginFinished(string msg)
	{
		this.OnResponse(msg);
	}

	// Token: 0x06001136 RID: 4406 RVA: 0x000C0EF0 File Offset: 0x000BF0F0
	[Obfuscation(Exclude = true)]
	public void LoginError(Exception e)
	{
		base.OnFail(e);
	}

	// Token: 0x06001137 RID: 4407 RVA: 0x000C0EFC File Offset: 0x000BF0FC
	[Obfuscation(Exclude = true)]
	public void WallPostDone()
	{
		Main.AddDatabaseRequest<ClearBonusInfo>(new object[0]);
	}

	// Token: 0x06001138 RID: 4408 RVA: 0x000C0F0C File Offset: 0x000BF10C
	[Obfuscation(Exclude = true)]
	public void FriendListStore(string message)
	{
		if (string.IsNullOrEmpty(message))
		{
			return;
		}
		if (CVars.n_httpDebug)
		{
			global::Console.print(message);
		}
		Login.FriendListRaw = message;
	}

	// Token: 0x06001139 RID: 4409 RVA: 0x000C0F3C File Offset: 0x000BF13C
	[Obfuscation(Exclude = true)]
	public void VKUploadScreenshot(string uploadURL)
	{
		base.StartCoroutine(this.UploadScreenshot(uploadURL));
	}

	// Token: 0x0600113A RID: 4410 RVA: 0x000C0F4C File Offset: 0x000BF14C
	[Obfuscation(Exclude = true)]
	private IEnumerator UploadScreenshot(string url)
	{
		WWWForm form = new WWWForm();
		form.AddBinaryData("file1", Main.capturedPNG, "screenshot.png", "image/png");
		WWW request = new WWW(url, form);
		yield return request;
		if (request.error != null)
		{
			Debug.Log(request.error);
		}
		else
		{
			Main.isUploadingScreenshot = false;
			this.VKSave(request.text);
		}
		yield break;
	}

	// Token: 0x0600113B RID: 4411 RVA: 0x000C0F78 File Offset: 0x000BF178
	[Obfuscation(Exclude = true)]
	private IEnumerator FBUploadScreenshot(string url)
	{
		WWWForm form = new WWWForm();
		form.AddBinaryData("source", Main.capturedPNG, "screenshot.png", "image/png");
		WWW request = new WWW(url, form);
		yield return request;
		if (request.error != null)
		{
			Debug.Log(request.error);
		}
		else
		{
			Main.isUploadingScreenshot = false;
			this.VKSave(request.text);
		}
		yield break;
	}

	// Token: 0x0600113C RID: 4412 RVA: 0x000C0FA4 File Offset: 0x000BF1A4
	[Obfuscation(Exclude = true)]
	public void VKSave(string response)
	{
		response = response.Replace("\\\"", "&&&&");
		response = response.Replace("\"", "****");
		Application.ExternalCall("VKSavePNG", new object[]
		{
			response.ToString()
		});
	}

	// Token: 0x040010F5 RID: 4341
	public static string FriendListRaw = string.Empty;
}
