using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000094 RID: 148
[Serializable]
internal class PrefabHolder
{
	// Token: 0x17000062 RID: 98
	// (get) Token: 0x0600034B RID: 843 RVA: 0x00017DC8 File Offset: 0x00015FC8
	public string FileNameWithoutExtension
	{
		get
		{
			return this._downloader.FileNameWithoutExtension;
		}
	}

	// Token: 0x17000063 RID: 99
	// (get) Token: 0x0600034C RID: 844 RVA: 0x00017DD8 File Offset: 0x00015FD8
	public bool MainAssetLoaded
	{
		get
		{
			return this._mainAsset != null;
		}
	}

	// Token: 0x0600034D RID: 845 RVA: 0x00017DE8 File Offset: 0x00015FE8
	public void Init(Downloader downloader)
	{
		this._downloader = downloader;
	}

	// Token: 0x0600034E RID: 846 RVA: 0x00017DF4 File Offset: 0x00015FF4
	public IEnumerator LoadAllAsync()
	{
		if (!Main.ExternalContent)
		{
			yield break;
		}
		if (this.MainAssetLoaded)
		{
			yield break;
		}
		AssetBundleRequest op = null;
		if (this._downloader.AssetBundle)
		{
			op = this._downloader.AssetBundle.LoadAsync(string.Empty, typeof(UnityEngine.Object));
			op.priority = 0;
		}
		else
		{
			this._mainAsset = null;
		}
		yield return op;
		yield break;
	}

	// Token: 0x0600034F RID: 847 RVA: 0x00017E10 File Offset: 0x00016010
	public IEnumerator LoadAllAsyncNoError()
	{
		if (!Main.ExternalContent)
		{
			yield break;
		}
		if (this.MainAssetLoaded)
		{
			yield break;
		}
		AssetBundleRequest op = this._downloader.AssetBundle.LoadAsync(string.Empty, typeof(UnityEngine.Object));
		op.priority = 0;
		yield return op;
		yield break;
	}

	// Token: 0x06000350 RID: 848 RVA: 0x00017E2C File Offset: 0x0001602C
	public GameObject Generate()
	{
		if (Main.ExternalContent)
		{
			if (this._downloader.AssetBundle == null)
			{
				return null;
			}
			if (!this.MainAssetLoaded)
			{
				this._mainAsset = this._downloader.AssetBundle.mainAsset;
			}
		}
		else if (!this.MainAssetLoaded)
		{
			string assetPath = "Assets/" + this._downloader.Url.Replace(Application.dataPath, string.Empty).Replace("file://", string.Empty);
			this._mainAsset = Resources.LoadAssetAtPath(assetPath, typeof(GameObject));
		}
		return (GameObject)this._mainAsset;
	}

	// Token: 0x06000351 RID: 849 RVA: 0x00017EE4 File Offset: 0x000160E4
	public void Unload()
	{
		if (this._mainAsset != null)
		{
			if (Main.ExternalContent && CVars.n_unloadAssetBundle)
			{
				UnityEngine.Object.DestroyImmediate(this._mainAsset, true);
			}
			this._mainAsset = null;
		}
		if (Main.ExternalContent && CVars.n_unloadAssetBundle)
		{
			this._downloader.Unload();
		}
	}

	// Token: 0x04000379 RID: 889
	private Downloader _downloader;

	// Token: 0x0400037A RID: 890
	private UnityEngine.Object _mainAsset;
}
