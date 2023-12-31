using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200023C RID: 572
[AddComponentMenu("Scripts/Game/Events/Vote")]
internal class Vote : DatabaseEvent
{
	// Token: 0x0600119B RID: 4507 RVA: 0x000C3B68 File Offset: 0x000C1D68
	public override void Initialize(params object[] args)
	{
		int num = (int)Crypt.ResolveVariable(args, 0, 1);
		int num2 = (int)Crypt.ResolveVariable(args, 0, 2);
		int num3 = (int)Crypt.ResolveVariable(args, -1, 3);
		EventFactory.Call("StartVoting", null);
		HtmlLayer.Request(string.Concat(new object[]
		{
			"?action=vote&pos=",
			num3,
			"&votefor=",
			num.ToString(),
			"&vote=",
			num2.ToString()
		}), new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
		int[] array = new int[Main.UserInfo.voteInfo.Length + 1];
		Main.UserInfo.voteInfo.CopyTo(array, 0);
		array[array.Length - 1] = num;
		Main.UserInfo.voteInfo = array;
		Vote.isVoting = true;
	}

	// Token: 0x0600119C RID: 4508 RVA: 0x000C3C60 File Offset: 0x000C1E60
	protected override void OnResponse(string text)
	{
		Dictionary<string, object> dictionary = null;
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
		if ((int)dictionary["result"] == 0)
		{
			global::Console.print((string)dictionary["message"], Color.green);
			Main.UserInfo.votes = (int)dictionary["new_votes"];
			Vote.repaToChange.Value = (float)((int)dictionary["new_repa"]);
			Vote.repaToChange = null;
		}
		else
		{
			this.OnFail(new Exception("Vote failed: " + (string)dictionary["message"]));
		}
		Vote.isVoting = false;
	}

	// Token: 0x0600119D RID: 4509 RVA: 0x000C3D7C File Offset: 0x000C1F7C
	protected override void OnFail(Exception e)
	{
		Vote.isVoting = false;
		EventFactory.Call("ShowPopup", new Popup(WindowsID.VoteError, "Vote failed", "Vote Failed", PopupState.information, true, true, string.Empty, string.Empty));
		EventFactory.Call("StopVoting", null);
		global::Console.print(e.ToString(), Color.red);
		Vote.repaToChange = null;
	}

	// Token: 0x0400112B RID: 4395
	public static Float repaToChange;

	// Token: 0x0400112C RID: 4396
	public static bool isVoting;
}
