using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200033E RID: 830
internal class LoadModInfo : DatabaseEvent
{
	// Token: 0x06001BF1 RID: 7153 RVA: 0x000FA0C4 File Offset: 0x000F82C4
	public override void Initialize(params object[] args)
	{
		HtmlLayer.Request("adm.php?q=customization/client/mod", new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06001BF2 RID: 7154 RVA: 0x000FA100 File Offset: 0x000F8300
	protected override void OnResponse(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			throw new Exception("String is null or empty");
		}
		Dictionary<string, object> dictionary;
		try
		{
			dictionary = ArrayUtility.FromJSON(text, string.Empty);
		}
		catch (Exception innerException)
		{
			Debug.LogError(new Exception("JSON:" + text, innerException));
			return;
		}
		object obj;
		if (dictionary.TryGetValue("result", out obj))
		{
			if ((int)obj != 0)
			{
				throw new Exception("Bad result: " + obj);
			}
			ModsStorage.Instance().FillStorage(text);
		}
		this.SuccessAction();
	}
}
