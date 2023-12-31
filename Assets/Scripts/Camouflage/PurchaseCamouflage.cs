using System;
using System.Collections.Generic;

namespace Assets.Scripts.Camouflage
{
	// Token: 0x0200005B RID: 91
	internal class PurchaseCamouflage : DatabaseEvent
	{
		// Token: 0x0600015D RID: 349 RVA: 0x0000CC38 File Offset: 0x0000AE38
		public override void Initialize(params object[] args)
		{
			int num = (int)Crypt.ResolveVariable(args, 0, 0);
			string actions = "adm.php?q=customization/player/camo/buy/" + num;
			HtmlLayer.Request(actions, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000CC94 File Offset: 0x0000AE94
		protected override void OnResponse(string text)
		{
			Dictionary<string, object> dictionary = ArrayUtility.FromJSON(text, string.Empty);
			object obj;
			if (dictionary.TryGetValue("result", out obj))
			{
				if ((int)obj == 0)
				{
					this.SuccessAction();
				}
				else
				{
					this.FailedAction();
				}
			}
			else
			{
				this.FailedAction();
			}
			base.OnResponse(text);
		}
	}
}
