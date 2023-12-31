using System;
using UnityEngine;

namespace DownloadGlobals
{
	// Token: 0x02000061 RID: 97
	internal class DownloadGlobal
	{
		// Token: 0x06000172 RID: 370 RVA: 0x0000D088 File Offset: 0x0000B288
		public DownloadGlobal()
		{
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000D0F8 File Offset: 0x0000B2F8
		public DownloadGlobal(string URL)
		{
			this.URL = URL;
			this.GetResponce();
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000D178 File Offset: 0x0000B378
		public void GetResponce(RequestFinished finished, RequestFailed failed)
		{
			this.finished = finished;
			this.failed = failed;
			this.success = false;
			if (!string.IsNullOrEmpty(this.URL))
			{
				HtmlLayer.DirectRequest(this.URL, new RequestFinished(this.Success), new RequestFailed(this.Failed));
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
		public void GetResponce()
		{
			this.success = false;
			HtmlLayer.DirectRequest(this.URL, new RequestFinished(this.Success), new RequestFailed(this.Failed));
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000D208 File Offset: 0x0000B408
		public void GetJSon(out string json)
		{
			json = this.responce;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000D214 File Offset: 0x0000B414
		protected void Failed(Exception e, string url = "")
		{
			this.success = false;
			if (DownloadGlobal.debug)
			{
				Debug.Log("ConnectError to: \n" + url);
				Debug.LogError(e);
			}
			this.IsDone = true;
			this.failed(e, url);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000D254 File Offset: 0x0000B454
		protected void Success(string text, string url)
		{
			this.success = true;
			if (DownloadGlobal.debug)
			{
				Debug.Log("ConnectSuccess to: " + url);
				Debug.Log("JSON: \n" + text);
			}
			this.responce = text;
			this.IsDone = true;
			this.finished(text, url);
		}

		// Token: 0x040001F5 RID: 501
		public static bool debug;

		// Token: 0x040001F6 RID: 502
		public bool success;

		// Token: 0x040001F7 RID: 503
		public bool IsDone;

		// Token: 0x040001F8 RID: 504
		public string URL = string.Empty;

		// Token: 0x040001F9 RID: 505
		protected string responce = string.Empty;

		// Token: 0x040001FA RID: 506
		private RequestFinished finished = delegate(string text, string url)
		{
		};

		// Token: 0x040001FB RID: 507
		private RequestFailed failed = delegate(Exception e, string url)
		{
		};
	}
}
