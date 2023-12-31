using System;
using System.Collections.Generic;

namespace CWSARequests
{
	// Token: 0x02000056 RID: 86
	internal class CWSAUpdateBalanceRequest : CWSARequest
	{
		// Token: 0x06000149 RID: 329 RVA: 0x0000C69C File Offset: 0x0000A89C
		public override void Initialize(params object[] args)
		{
			HtmlLayer.Request("?action=getBalance", new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000C6D8 File Offset: 0x0000A8D8
		protected override void OnResponse(string text)
		{
			base.OnResponse(text);
			Dictionary<string, object> dictionary = ArrayUtility.FromJSON(text, string.Empty);
			if (dictionary.ContainsKey("gp"))
			{
				Main.UserInfo.GP = int.Parse(dictionary["gp"].ToString());
			}
			if (dictionary.ContainsKey("cr"))
			{
				Main.UserInfo.CR = int.Parse(dictionary["cr"].ToString());
			}
		}
	}
}
