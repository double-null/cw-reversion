using System;
using System.Reflection;
using UnityEngine;

// Token: 0x0200039F RID: 927
[Obfuscation(Exclude = true)]
public static class StaticInfoHelper
{
	// Token: 0x06001D8E RID: 7566 RVA: 0x00103300 File Offset: 0x00101500
	public static string GetUserID()
	{
		if (SingletoneComponent<Main>.Instance == null || Main.UserInfo.userID == IDUtil.NoID || Main.UserInfo.userID == IDUtil.NoID2)
		{
			return "INVALID_ID";
		}
		return Main.UserInfo.userID.ToString();
	}

	// Token: 0x06001D8F RID: 7567 RVA: 0x00103364 File Offset: 0x00101564
	public static string GetUserDeviceIdMd5()
	{
		string result;
		try
		{
			result = SystemInfo.deviceUniqueIdentifier;
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.Message);
			result = "INVALID_ID";
		}
		return result;
	}

	// Token: 0x04002248 RID: 8776
	private const string INVALID_ID = "INVALID_ID";
}
