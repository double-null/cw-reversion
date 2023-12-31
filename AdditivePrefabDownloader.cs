using System;
using System.Collections.Generic;
using PrefabDownloadNamespace;
using UnityEngine;

// Token: 0x02000063 RID: 99
internal class AdditivePrefabDownloader : MonoBehaviour
{
	// Token: 0x06000184 RID: 388 RVA: 0x0000D318 File Offset: 0x0000B518
	private void Awake()
	{
		if (AdditivePrefabDownloader.I == null)
		{
			AdditivePrefabDownloader.I = this;
		}
	}

	// Token: 0x06000185 RID: 389 RVA: 0x0000D330 File Offset: 0x0000B530
	public static void Download(string name, OnPrefabDownloaded done)
	{
		if (AdditivePrefabDownloader.I)
		{
			AdditivePrefabDownloader.I.DownloadPrefab(name, done);
		}
	}

	// Token: 0x06000186 RID: 390 RVA: 0x0000D350 File Offset: 0x0000B550
	private void DownloadPrefab(string name, OnPrefabDownloaded done)
	{
		Prefab prefab;
		if (this.data.TryGetValue(name, out prefab))
		{
			done(prefab);
		}
		else
		{
			string text = WWWUtil.AdditiveAsset(name);
			if (Main.ExternalContent)
			{
				if (CVars.IsStandaloneRealm)
				{
					AssetBundleCreateRequest createRequest = StandaloneCacher.GetCreateRequest(text, Globals.I.content_version);
					if (createRequest != null)
					{
						prefab = new Prefab();
						this.data.Add(name, prefab);
						base.StartCoroutine(prefab.StandaloneDownloadFromCache(done, createRequest, text));
						return;
					}
				}
				WWW www;
				if (!AdditivePrefabDownloader.UseCache || CVars.IsStandaloneRealm)
				{
					www = new WWW(text);
				}
				else
				{
					www = WWW.LoadFromCacheOrDownload(text, Globals.I.content_version);
				}
				if (CVars.n_httpDebug)
				{
					Debug.Log(text);
				}
				prefab = new Prefab(www);
				this.data.Add(name, prefab);
				base.StartCoroutine(prefab.Download(done));
			}
			else
			{
				prefab = new Prefab();
				prefab.obj = (GameObject)Resources.LoadAssetAtPath("Assets" + text.Replace(Application.dataPath, string.Empty).Replace("file://", string.Empty), typeof(GameObject));
				this.data.Add(name, prefab);
				done(prefab);
			}
		}
	}

	// Token: 0x04000202 RID: 514
	private static AdditivePrefabDownloader I;

	// Token: 0x04000203 RID: 515
	private static bool UseCache;

	// Token: 0x04000204 RID: 516
	private Dictionary<string, Prefab> data = new Dictionary<string, Prefab>();
}
