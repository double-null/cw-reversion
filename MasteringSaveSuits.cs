using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000347 RID: 839
internal class MasteringSaveSuits : DatabaseEvent
{
	// Token: 0x06001C0B RID: 7179 RVA: 0x000FAA10 File Offset: 0x000F8C10
	public override void Initialize(params object[] args)
	{
		object obj = Crypt.ResolveVariable(args, -1, 0);
		string data = (string)Crypt.ResolveVariable(args, string.Empty, 1);
		object obj2 = Crypt.ResolveVariable(args, -1, 2);
		string actions = string.Concat(new object[]
		{
			"adm.php?q=customization/player/save_suits/",
			obj2,
			"/",
			obj
		});
		HtmlLayer.SendCompressed(actions, data, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail));
	}

	// Token: 0x06001C0C RID: 7180 RVA: 0x000FAA90 File Offset: 0x000F8C90
	protected override void OnResponse(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			throw new Exception("String is null or empty");
		}
		try
		{
			Dictionary<string, object> dictionary = ArrayUtility.FromJSON(text, string.Empty);
			if (dictionary.ContainsKey("result") && (int)dictionary["result"] == 0)
			{
				this.SuccessAction();
			}
			else
			{
				this.FailedAction();
			}
		}
		catch (Exception message)
		{
			Debug.Log(message);
			this.FailedAction();
		}
	}
}
