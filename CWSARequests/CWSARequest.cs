using System;
using System.Collections.Generic;

namespace CWSARequests
{
	// Token: 0x02000055 RID: 85
	internal class CWSARequest : DatabaseEvent
	{
		// Token: 0x06000147 RID: 327 RVA: 0x0000C62C File Offset: 0x0000A82C
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
