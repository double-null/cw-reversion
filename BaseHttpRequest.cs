using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000072 RID: 114
[AddComponentMenu("Scripts/Engine/Foundation/BaseHttpRequest")]
internal class BaseHttpRequest : MonoBehaviour
{
	// Token: 0x06000215 RID: 533 RVA: 0x00011F6C File Offset: 0x0001016C
	public void Init(RequestFinished finish, RequestFailed fail, string url = "")
	{
		this.finish = finish;
		this.fail = fail;
		this.fail = fail;
	}

	// Token: 0x06000216 RID: 534 RVA: 0x00011F84 File Offset: 0x00010184
	public void Download(string url, string post = "")
	{
		if (CVars.n_httpDebug)
		{
			global::Console.print(url, Color.yellow);
			if (post != string.Empty)
			{
				global::Console.print(post, new Color(0.25f, 0.5f, 0.1f, 1f));
			}
		}
		base.StartCoroutine(this.DownloadEnumerator(url, post));
	}

	// Token: 0x06000217 RID: 535 RVA: 0x00011FE4 File Offset: 0x000101E4
	public void Cancel()
	{
		base.StopAllCoroutines();
		base.CancelInvoke();
	}

	// Token: 0x06000218 RID: 536 RVA: 0x00011FF4 File Offset: 0x000101F4
	protected virtual IEnumerator DownloadEnumerator(string Url, string post)
	{
		Request request = new Request("POST", Url, true, false);
		request.acceptGzip = true;
		request.Text = post;
		request.Send();
		while (!request.isDone)
		{
			yield return new WaitForEndOfFrame();
		}
		if (request.exception != null && this.fail != null)
		{
			Debug.Log(Url);
			this.fail(request.exception, Url);
			yield break;
		}
		yield return new WaitForEndOfFrame();
		if (CVars.n_httpDebug)
		{
			global::Console.print(request.response.Text, Color.green);
		}
		if (this.finish != null)
		{
			this.finish(request.response.Text, Url);
		}
		UnityEngine.Object.Destroy(this);
		yield break;
	}

	// Token: 0x04000273 RID: 627
	protected WWW www;

	// Token: 0x04000274 RID: 628
	protected RequestFailed fail;

	// Token: 0x04000275 RID: 629
	protected RequestFinished finish;
}
