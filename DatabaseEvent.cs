using System;
using System.Reflection;
using UnityEngine;

// Token: 0x02000216 RID: 534
[AddComponentMenu("Scripts/Game/Events/DatabaseEvent")]
internal class DatabaseEvent : PoolableBehaviour
{
	// Token: 0x060010E4 RID: 4324 RVA: 0x000BD30C File Offset: 0x000BB50C
	public virtual void Initialize(params object[] args)
	{
	}

	// Token: 0x060010E5 RID: 4325 RVA: 0x000BD310 File Offset: 0x000BB510
	protected virtual void OnResponse(string text, string url)
	{
		this.OnResponse(text);
	}

	// Token: 0x060010E6 RID: 4326 RVA: 0x000BD31C File Offset: 0x000BB51C
	protected virtual void OnFail(Exception e, string url)
	{
		Debug.Log(url + "\n" + e);
	}

	// Token: 0x060010E7 RID: 4327 RVA: 0x000BD330 File Offset: 0x000BB530
	[Obfuscation(Exclude = true)]
	protected virtual void OnResponse(string text)
	{
		if (CVars.n_httpDebug)
		{
			global::Console.print("response:" + text);
		}
	}

	// Token: 0x060010E8 RID: 4328 RVA: 0x000BD34C File Offset: 0x000BB54C
	[Obfuscation(Exclude = true)]
	protected virtual void OnFail(Exception e)
	{
		global::Console.print(base.GetType().ToString() + " Error, Reload", Color.red);
		global::Console.exception(e);
	}

	// Token: 0x040010DC RID: 4316
	public DatabaseEvent.SomeAction SuccessAction = delegate()
	{
	};

	// Token: 0x040010DD RID: 4317
	public DatabaseEvent.SomeAction FailedAction = delegate()
	{
	};

	// Token: 0x020003AA RID: 938
	// (Invoke) Token: 0x06001E10 RID: 7696
	public delegate void SomeAction();
}
