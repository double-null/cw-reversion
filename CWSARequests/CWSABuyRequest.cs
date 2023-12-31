using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CWSARequests
{
	// Token: 0x02000054 RID: 84
	internal class CWSABuyRequest : CWSARequest
	{
		// Token: 0x06000143 RID: 323 RVA: 0x0000C4D0 File Offset: 0x0000A6D0
		public override void Initialize(params object[] args)
		{
			bool flag = (bool)args[0];
			int num = (int)args[1];
			if (num < 1)
			{
				Debug.LogError("WrongPackageNumber");
				return;
			}
			string text = (!flag) ? "cr_" : "gp_";
			ELanguage currentLanguage = Language.CurrentLanguage;
			string text2;
			if (currentLanguage != ELanguage.EN)
			{
				if (currentLanguage != ELanguage.RU)
				{
					text2 = "En";
				}
				else
				{
					text2 = "Ru";
				}
			}
			else
			{
				text2 = "En";
			}
			string actions = string.Concat(new object[]
			{
				"?action=buyGP&sku=",
				text,
				num,
				"&lang=",
				text2
			});
			HtmlLayer.Request(actions, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000C5AC File Offset: 0x0000A7AC
		protected override void OnResponse(string text)
		{
			base.OnResponse(text);
			Dictionary<string, object> dictionary = ArrayUtility.FromJSON(text, string.Empty);
			string text2 = (string)dictionary["url"];
			if (!text2.Contains("https://"))
			{
				text2 = "https://" + text2;
			}
			Application.OpenURL(text2);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000C600 File Offset: 0x0000A800
		private IEnumerator OnApplicationFocus(bool focus)
		{
			if (!focus)
			{
				yield break;
			}
			yield return new WaitForSeconds(1f);
			Main.AddDatabaseRequest<CWSAUpdateBalanceRequest>(new object[0]);
			yield break;
		}
	}
}
