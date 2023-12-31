using System;
using System.Collections.Generic;
using UnityEngine;

namespace CWSARequests
{
	// Token: 0x02000057 RID: 87
	internal class TransferHashGenRequest : CWSARequest
	{
		// Token: 0x0600014C RID: 332 RVA: 0x0000C768 File Offset: 0x0000A968
		public override void Initialize(params object[] args)
		{
			if (string.IsNullOrEmpty(TransferHashGenRequest._cachedUrl))
			{
				HtmlLayer.Request("?action=hashGen", new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
			}
			else
			{
				Application.ExternalCall("window.open", new object[]
				{
					TransferHashGenRequest._cachedUrl
				});
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000C7D0 File Offset: 0x0000A9D0
		protected override void OnResponse(string text)
		{
			base.OnResponse(text);
			Dictionary<string, object> dictionary = ArrayUtility.FromJSON(text, string.Empty);
			object obj;
			if (!dictionary.TryGetValue("result", out obj))
			{
				return;
			}
			if ((int)obj == 0 || true)
			{
				TransferHashGenRequest._cachedUrl = (string)dictionary["url"];
				Application.ExternalCall("window.open", new object[]
				{
					TransferHashGenRequest._cachedUrl
				});
			}
			else
			{
				EventFactory.Call("ShowPopup", new Popup(WindowsID.PopupGUI, string.Empty, Language.ProfileAlreadyTransfered, PopupState.information, false, true, string.Empty, string.Empty));
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000C870 File Offset: 0x0000AA70
		protected override void OnFail(Exception e, string url)
		{
			base.OnFail(e, url);
			EventFactory.Call("ShowPopup", new Popup(WindowsID.PopupGUI, string.Empty, Language.ProfileTransferError, PopupState.information, false, true, string.Empty, string.Empty));
			Main.UserInfo.IsProfileTransfered = false;
		}

		// Token: 0x040001DB RID: 475
		private static string _cachedUrl;
	}
}
