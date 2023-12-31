using System;
using System.Reflection;
using UnityEngine;

// Token: 0x02000233 RID: 563
[AddComponentMenu("Scripts/Game/Events/Server/ServerEvent")]
internal class ServerEvent : PoolableBehaviour
{
	// Token: 0x06001173 RID: 4467 RVA: 0x000C26C4 File Offset: 0x000C08C4
	public virtual void Initialize(PoolableBehaviour player, params object[] args)
	{
	}

	// Token: 0x06001174 RID: 4468 RVA: 0x000C26C8 File Offset: 0x000C08C8
	public virtual void Cancel()
	{
		if (this.request != null)
		{
			this.request.Cancel();
			UnityEngine.Object.DestroyObject(this.request);
			this.request = null;
		}
	}

	// Token: 0x06001175 RID: 4469 RVA: 0x000C2704 File Offset: 0x000C0904
	[Obfuscation(Exclude = true)]
	protected void OnResponse(string text)
	{
	}

	// Token: 0x06001176 RID: 4470 RVA: 0x000C2708 File Offset: 0x000C0908
	protected void OnFail(Exception e, string url)
	{
		this.OnFail(e);
	}

	// Token: 0x06001177 RID: 4471 RVA: 0x000C2714 File Offset: 0x000C0914
	[Obfuscation(Exclude = true)]
	protected void OnFail(Exception e)
	{
		global::Console.print(base.GetType().ToString() + " Error", Color.red);
		global::Console.exception(e);
	}

	// Token: 0x0400111B RID: 4379
	protected ServerNetPlayer player;

	// Token: 0x0400111C RID: 4380
	protected BaseHttpRequest request;
}
