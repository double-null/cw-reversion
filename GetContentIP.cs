using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x02000219 RID: 537
[Obfuscation(Exclude = true, ApplyToMembers = true)]
[AddComponentMenu("Scripts/Game/Events/GetContentIP")]
internal class GetContentIP : DatabaseEvent
{
	// Token: 0x060010F4 RID: 4340 RVA: 0x000BD4C0 File Offset: 0x000BB6C0
	public override void Initialize(params object[] args)
	{
		global::Console.print("GetContentIP", Color.grey);
		HtmlLayer.Request("getcontentinfo.php?", new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x060010F5 RID: 4341 RVA: 0x000BD500 File Offset: 0x000BB700
	[Obfuscation(Exclude = true)]
	protected override void OnResponse(string text, string url)
	{
		base.OnResponse(text);
		Dictionary<string, object> dictionary;
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
			this.OnFail(new Exception("Data Server Error"));
			return;
		}
		if ((int)dictionary["result"] != 0)
		{
			this.OnFail(new Exception("Data Server Error"));
			return;
		}
		WWWUtil.mirror = (dictionary["server"] as string);
		global::Console.print("GetContentIP Finished", Color.green);
		WWWUtil.contentIpLoaded = true;
	}

	// Token: 0x060010F6 RID: 4342 RVA: 0x000BD5D4 File Offset: 0x000BB7D4
	[Obfuscation(Exclude = true)]
	protected override void OnFail(Exception e)
	{
		global::Console.print(base.GetType() + " Error, Reload", Color.red);
		global::Console.exception(e);
	}
}
