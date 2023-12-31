using System;
using System.Reflection;
using UnityEngine;

// Token: 0x0200021C RID: 540
[AddComponentMenu("Scripts/Game/Events/GetRealmSettings")]
internal class GetRealmSettings : DatabaseEvent
{
	// Token: 0x06001102 RID: 4354 RVA: 0x000BD858 File Offset: 0x000BBA58
	public override void Initialize(params object[] args)
	{
		if (Application.isWebPlayer)
		{
			Application.ExternalCall("GetRealmSettings", new object[0]);
		}
		else if (Application.isEditor)
		{
			if (SingletoneComponent<SplashController>.Instance == null)
			{
				this.SetRealmSettings(SingletoneComponent<Main>.Instance.commandLine);
			}
			else
			{
				this.SetRealmSettings(SingletoneComponent<SplashController>.Instance.commandLine);
			}
		}
		else
		{
			this.SetRealmSettings("--standalone");
		}
	}

	// Token: 0x06001103 RID: 4355 RVA: 0x000BD8D4 File Offset: 0x000BBAD4
	[Obfuscation(Exclude = true)]
	public void SetRealmSettings(string message)
	{
		if (SingletoneComponent<SplashController>.Instance == null)
		{
			SingletoneComponent<Main>.Instance.ParseCommandLineArgs(message);
			SingletoneComponent<Main>.Instance.Init();
		}
		else
		{
			SingletoneComponent<SplashController>.Instance.ParseCommandLineArgs(message);
			base.StartCoroutine(SingletoneComponent<SplashController>.Instance.OnGetRealmSettingsFinished());
		}
	}
}
