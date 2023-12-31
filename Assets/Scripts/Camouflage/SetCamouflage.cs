using System;
using System.Collections.Generic;

namespace Assets.Scripts.Camouflage
{
	// Token: 0x0200005C RID: 92
	internal class SetCamouflage : DatabaseEvent
	{
		// Token: 0x06000160 RID: 352 RVA: 0x0000CD04 File Offset: 0x0000AF04
		public override void Initialize(params object[] args)
		{
			object obj = Crypt.ResolveVariable(args, 0, 0);
			object obj2 = Crypt.ResolveVariable(args, 0, 1);
			string actions = string.Concat(new object[]
			{
				"adm.php?q=customization/player/camo/set/",
				obj,
				"/",
				obj2
			});
			HtmlLayer.Request(actions, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000CD7C File Offset: 0x0000AF7C
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
