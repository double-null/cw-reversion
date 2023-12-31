using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x0200007B RID: 123
[AddComponentMenu("Scripts/Engine/Foundation/Downloader")]
internal class Downloader : MonoBehaviour
{
	// Token: 0x17000036 RID: 54
	// (get) Token: 0x0600027D RID: 637 RVA: 0x000144F8 File Offset: 0x000126F8
	public AssetBundle Asset
	{
		get
		{
			return this.assetBundle;
		}
	}

	// Token: 0x0600027E RID: 638 RVA: 0x00014500 File Offset: 0x00012700
	private static byte[] DecryptionMethod(byte[] bytes)
	{
		for (int i = 0; i < bytes.Length; i++)
		{
			int num = i;
			bytes[num] ^= 71;
		}
		return bytes;
	}

	// Token: 0x0600027F RID: 639 RVA: 0x00014530 File Offset: 0x00012730
	private static void Create(ref WWW www, string url, int version = 0)
	{
		if (url.Contains("unity3d") && CVars.UseUnityCache && !Peer.Dedicated && !CVars.IsStandaloneRealm)
		{
			bool flag = Caching.IsVersionCached(url, version);
			if (CVars.n_httpDebug)
			{
				Debug.Log(string.Concat(new object[]
				{
					"LoadFromCacheOrDownload ",
					version,
					" cached = ",
					flag,
					"\n",
					url
				}));
			}
			www = WWW.LoadFromCacheOrDownload(url, version);
		}
		else
		{
			if (CVars.n_httpDebug)
			{
				Debug.Log("new WWW\n" + url);
			}
			www = new WWW(url);
		}
		if (www != null)
		{
			www.threadPriority = ThreadPriority.Low;
		}
		if (CVars.n_httpAssetsDebug)
		{
			global::Console.print("Downloading: " + url, Color.yellow);
		}
	}

	// Token: 0x06000280 RID: 640 RVA: 0x0001461C File Offset: 0x0001281C
	public static void Delete(ref WWW www)
	{
		if (www != null && www.error == null && www.isDone)
		{
			www.Dispose();
		}
		www = null;
	}

	// Token: 0x06000281 RID: 641 RVA: 0x00014648 File Offset: 0x00012848
	public void Download(string url, DownloaderFinishedDownload finish, bool repeatAtFail, int maxTryCount)
	{
		this._repeatAtFail = repeatAtFail;
		this._maxTryCount = maxTryCount;
		this.finished = false;
		this.Url = url;
		this.FileNameWithoutExtension = Path.GetFileNameWithoutExtension(this.Url);
		this.finish = finish;
		base.StartCoroutine(this.DownloadEnumerator(url));
	}

	// Token: 0x06000282 RID: 642 RVA: 0x00014698 File Offset: 0x00012898
	private IEnumerator DownloadEnumerator(string contenturl)
	{
		if (this.assetBundle)
		{
			yield return new WaitForEndOfFrame();
			this.finished = true;
			this.finish(this);
			yield break;
		}
		int maxCount = this._maxTryCount;
		int tryCount = 0;
		while (this.www == null || !this.www.isDone || this.www.error != null)
		{
			this.contentHost = WWWUtil.ContentHost;
			string url = this.contentHost + contenturl;
			yield return new WaitForEndOfFrame();
			tryCount++;
			if (tryCount > maxCount || (!this._repeatAtFail && tryCount > 1))
			{
				break;
			}
			if (url.Contains(".unity3d"))
			{
				yield return base.StartCoroutine(this.DownloadMeta(url));
			}
			Downloader.Delete(ref this.www);
			if (CVars.IsStandaloneRealm)
			{
				AssetBundleCreateRequest assetCreateRequest = StandaloneCacher.GetCreateRequest(url, this._version);
				if (assetCreateRequest != null)
				{
					yield return assetCreateRequest;
					if (assetCreateRequest.assetBundle != null)
					{
						this.assetBundle = assetCreateRequest.assetBundle;
						yield return new WaitForEndOfFrame();
						this.finished = true;
						this.finish(this);
						yield break;
					}
					Debug.LogWarning("Can't load asset from cache");
				}
			}
			Downloader.Create(ref this.www, url, this._version);
			yield return base.StartCoroutine(this.ReloadByTimeout(url));
			if (this.www != null)
			{
				if (this.OnError(this.www))
				{
					yield return new WaitForSeconds(5f);
				}
				else if (Main.ExternalContent && (url.Contains(".unity3d") || url.Contains(".prefab")))
				{
					if (CVars.n_httpAssetsDebug)
					{
						global::Console.print(url + " Downloaded");
					}
					if (url.Contains("_cache_") || url.Contains("_newWWW_"))
					{
						yield return base.StartCoroutine(this.InitEncryptedBundle(url, tryCount));
					}
					else
					{
						this.InitBundle(tryCount, url);
					}
				}
			}
		}
		Downloader.Delete(ref this.www);
		yield return new WaitForEndOfFrame();
		this.finished = true;
		this.finish(this);
		yield break;
	}

	// Token: 0x06000283 RID: 643 RVA: 0x000146C4 File Offset: 0x000128C4
	private void InitBundle(int tryCount, string url)
	{
		try
		{
			if (this.www.error == null)
			{
				if (CVars.IsStandaloneRealm)
				{
					StandaloneCacher.Cache(this.www.bytes, url, this._version);
				}
				this.assetBundle = this.www.assetBundle;
			}
			else
			{
				Debug.Log("www.error " + this.www.error);
			}
		}
		catch (Exception innerException)
		{
			Debug.LogWarning(new Exception(string.Concat(new object[]
			{
				"[",
				tryCount,
				"]",
				url
			}), innerException));
			Downloader.Delete(ref this.www);
			this.contentHost = WWWUtil.GetRandomContentServer();
			this.assetBundle = null;
		}
	}

	// Token: 0x06000284 RID: 644 RVA: 0x000147AC File Offset: 0x000129AC
	private IEnumerator DownloadMeta(string url)
	{
		Downloader.Delete(ref this.www);
		string metaUrl = url.Replace(".unity3d", ".meta");
		metaUrl = metaUrl + "?v=" + (int)DateTime.Now.TimeOfDay.TotalSeconds;
		Downloader.Create(ref this.www, metaUrl, 0);
		yield return this.www;
		if (this.OnError(this.www))
		{
			yield return new WaitForSeconds(5f);
		}
		else
		{
			string stringTime = string.Empty;
			if (this.www.responseHeaders.TryGetValue("LAST-MODIFIED", out stringTime))
			{
				DateTime datetime = default(DateTime);
				if (DateTime.TryParse(stringTime, out datetime))
				{
					this._version = Mathf.Abs((int)datetime.TimeOfDay.TotalSeconds);
				}
			}
			if (CVars.n_httpDebug)
			{
				global::Console.print("stringTime " + stringTime + "\nurl = " + metaUrl, Color.yellow);
			}
			if (this.www.error == null)
			{
				Dictionary<string, object> dict = ArrayUtility.FromJSON(this.www.text, string.Empty);
				JSON.ReadWrite(dict, "size", ref this._fileSize, false);
			}
			else
			{
				Debug.Log("www.error " + this.www.error + " " + url);
			}
		}
		yield break;
	}

	// Token: 0x06000285 RID: 645 RVA: 0x000147D8 File Offset: 0x000129D8
	private IEnumerator ReloadByTimeout(string url)
	{
		float progress = 0f;
		float time = 0f;
		while (this.www != null && !this.www.isDone)
		{
			if (progress < this.www.progress)
			{
				progress = this.www.progress;
				time = 0f;
			}
			else
			{
				time += 0.2f;
				if (time > 8f)
				{
					Debug.LogWarning(url + " TimeoutDrop");
					Downloader.Delete(ref this.www);
					this.contentHost = WWWUtil.GetRandomContentServer();
					break;
				}
			}
			yield return new WaitForSeconds(0.2f);
		}
		yield break;
	}

	// Token: 0x06000286 RID: 646 RVA: 0x00014804 File Offset: 0x00012A04
	private IEnumerator InitEncryptedBundle(string url, int tryCount)
	{
		byte[] encryptedData;
		if (CVars.UseUnityCache)
		{
			TextAsset textAsset = (TextAsset)this.www.assetBundle.Load(Path.GetFileNameWithoutExtension(url), typeof(TextAsset));
			encryptedData = textAsset.bytes;
			this.parentAssetBundle = this.www.assetBundle;
		}
		else
		{
			encryptedData = this.www.bytes;
		}
		byte[] decryptedData = Downloader.DecryptionMethod(encryptedData);
		AssetBundleCreateRequest acr = AssetBundle.CreateFromMemory(decryptedData);
		yield return acr;
		try
		{
			if (this.www.error == null)
			{
				this.assetBundle = acr.assetBundle;
			}
			else
			{
				Debug.Log("www.error " + this.www.error);
			}
		}
		catch (Exception ex)
		{
			Exception e = ex;
			Debug.LogWarning(new Exception(string.Concat(new object[]
			{
				"[",
				tryCount,
				"]",
				url
			}), e));
			Downloader.Delete(ref this.www);
			this.contentHost = WWWUtil.GetRandomContentServer();
			this.assetBundle = null;
		}
		yield break;
	}

	// Token: 0x06000287 RID: 647 RVA: 0x0001483C File Offset: 0x00012A3C
	private bool OnError(WWW www)
	{
		if (www.error != null && this._repeatAtFail)
		{
			if (www.error != this.errorString)
			{
				Debug.LogWarning(new Exception("Downloader exception at " + www.url + ": " + www.error));
				this.errorString = www.error;
			}
			this.contentHost = WWWUtil.GetRandomContentServer();
			return true;
		}
		return false;
	}

	// Token: 0x06000288 RID: 648 RVA: 0x000148B4 File Offset: 0x00012AB4
	public void Unload()
	{
		base.CancelInvoke();
		base.StopAllCoroutines();
		if (this.assetBundle)
		{
			this.assetBundle.Unload(true);
			UnityEngine.Object.DestroyImmediate(this.assetBundle, true);
			this.assetBundle = null;
		}
		if (this.parentAssetBundle)
		{
			this.parentAssetBundle.Unload(true);
			UnityEngine.Object.DestroyImmediate(this.parentAssetBundle, true);
			this.parentAssetBundle = null;
		}
	}

	// Token: 0x17000037 RID: 55
	// (get) Token: 0x06000289 RID: 649 RVA: 0x0001492C File Offset: 0x00012B2C
	public bool Finished
	{
		get
		{
			return this.finished;
		}
	}

	// Token: 0x17000038 RID: 56
	// (get) Token: 0x0600028A RID: 650 RVA: 0x00014934 File Offset: 0x00012B34
	public float DownloadedSize
	{
		get
		{
			if (this.assetBundle != null)
			{
				return (float)this._fileSize;
			}
			if (this.www == null || this.www.error != null)
			{
				return 0f;
			}
			if (this.www.isDone)
			{
				return (float)this._fileSize;
			}
			return this.www.progress * (float)this._fileSize;
		}
	}

	// Token: 0x17000039 RID: 57
	// (get) Token: 0x0600028B RID: 651 RVA: 0x000149A8 File Offset: 0x00012BA8
	public float FileSize
	{
		get
		{
			return (float)this._fileSize;
		}
	}

	// Token: 0x1700003A RID: 58
	// (get) Token: 0x0600028C RID: 652 RVA: 0x000149B4 File Offset: 0x00012BB4
	// (set) Token: 0x0600028D RID: 653 RVA: 0x000149BC File Offset: 0x00012BBC
	public AssetBundle AssetBundle
	{
		get
		{
			return this.assetBundle;
		}
		set
		{
			this.assetBundle = value;
		}
	}

	// Token: 0x1700003B RID: 59
	// (get) Token: 0x0600028E RID: 654 RVA: 0x000149C8 File Offset: 0x00012BC8
	public float RatePerSecond
	{
		get
		{
			if (!this.timer.Enabled || this.timer.Elapsed >= 1f)
			{
				this.tempRate = this.DownloadedSize - this.prevSize;
				this.prevSize = this.DownloadedSize;
				this.timer.Start();
			}
			return this.tempRate;
		}
	}

	// Token: 0x04000325 RID: 805
	private bool _repeatAtFail = true;

	// Token: 0x04000326 RID: 806
	private int _maxTryCount = 100;

	// Token: 0x04000327 RID: 807
	public string FileNameWithoutExtension = string.Empty;

	// Token: 0x04000328 RID: 808
	public string Url = string.Empty;

	// Token: 0x04000329 RID: 809
	private WWW www;

	// Token: 0x0400032A RID: 810
	private AssetBundle assetBundle;

	// Token: 0x0400032B RID: 811
	private AssetBundle parentAssetBundle;

	// Token: 0x0400032C RID: 812
	private DownloaderFinishedDownload finish;

	// Token: 0x0400032D RID: 813
	private bool finished;

	// Token: 0x0400032E RID: 814
	private string errorString = string.Empty;

	// Token: 0x0400032F RID: 815
	private string contentHost = string.Empty;

	// Token: 0x04000330 RID: 816
	private long _fileSize;

	// Token: 0x04000331 RID: 817
	private int _version;

	// Token: 0x04000332 RID: 818
	private eTimer timer = new eTimer();

	// Token: 0x04000333 RID: 819
	private float tempRate;

	// Token: 0x04000334 RID: 820
	private float prevSize;
}
