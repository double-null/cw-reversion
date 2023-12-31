using System;
using System.Collections;
using UnityEngine;

namespace PrefabDownloadNamespace
{
	// Token: 0x02000064 RID: 100
	internal class Prefab
	{
		// Token: 0x06000187 RID: 391 RVA: 0x0000D498 File Offset: 0x0000B698
		public Prefab(WWW www)
		{
			this.www = www;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000D4A8 File Offset: 0x0000B6A8
		public Prefab()
		{
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000D4B0 File Offset: 0x0000B6B0
		public IEnumerator Download(OnPrefabDownloaded done)
		{
			while (!this.www.isDone)
			{
				yield return new WaitForSeconds(0.1f);
			}
			if (this.www.error == null)
			{
				if (CVars.IsStandaloneRealm)
				{
					StandaloneCacher.Cache(this.www.bytes, this.www.url, Globals.I.content_version);
				}
				this.bundle = this.www.assetBundle;
				if (done != null)
				{
					done(this);
				}
			}
			else if (CVars.n_httpDebug)
			{
				Debug.Log(this.www.error);
			}
			yield break;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000D4DC File Offset: 0x0000B6DC
		internal IEnumerator StandaloneDownloadFromCache(OnPrefabDownloaded done, AssetBundleCreateRequest request, string url)
		{
			yield return request;
			this.bundle = request.assetBundle;
			if (done != null)
			{
				done(this);
			}
			yield break;
		}

		// Token: 0x04000205 RID: 517
		private WWW www;

		// Token: 0x04000206 RID: 518
		public AssetBundle bundle;

		// Token: 0x04000207 RID: 519
		public GameObject obj;
	}
}
