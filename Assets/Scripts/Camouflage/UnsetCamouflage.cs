using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Camouflage
{
	// Token: 0x0200005D RID: 93
	internal class UnsetCamouflage : DatabaseEvent
	{
		// Token: 0x06000163 RID: 355 RVA: 0x0000CDEC File Offset: 0x0000AFEC
		public override void Initialize(params object[] args)
		{
			object arg = Crypt.ResolveVariable(args, 0, 0);
			HtmlLayer.Request("adm.php?q=customization/player/camo/unset/" + arg, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000CE3C File Offset: 0x0000B03C
		protected override void OnResponse(string text)
		{
			Debug.Log(text);
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
