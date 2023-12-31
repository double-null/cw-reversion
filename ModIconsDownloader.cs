using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000361 RID: 865
internal class ModIconsDownloader : MonoBehaviour
{
	// Token: 0x17000829 RID: 2089
	// (get) Token: 0x06001C58 RID: 7256 RVA: 0x000FC39C File Offset: 0x000FA59C
	public bool DataLoadingCompleted
	{
		get
		{
			return this._smallIconsLoaded && this._bigIconsLoaded && this._camouflagesLoaded;
		}
	}

	// Token: 0x06001C59 RID: 7257 RVA: 0x000FC3C0 File Offset: 0x000FA5C0
	private IEnumerator DownloadInstalledModsIcons()
	{
		foreach (KeyValuePair<int, Suit> suit in MasteringSuitsInfo.Instance.Suits)
		{
			foreach (KeyValuePair<int, WeaponMods> weapon in suit.Value.CurrentWeaponsMods)
			{
				foreach (KeyValuePair<ModType, int> mod in weapon.Value.Mods)
				{
					MasteringMod currentMod = ModsStorage.Instance().GetModById(mod.Value);
					if (!(currentMod.SmallIcon != null))
					{
						if (currentMod.Type != ModType.camo || !currentMod.IsBasic)
						{
							string url = WWWUtil.modIconWWW(string.Concat(new object[]
							{
								currentMod.Type,
								"_",
								currentMod.EngShortName,
								"_small"
							}));
							if (currentMod.Type == ModType.camo)
							{
								url = WWWUtil.camoIconWWW(string.Concat(new object[]
								{
									currentMod.Type,
									"_",
									currentMod.EngShortName,
									"_small"
								}));
							}
							WWW www = new WWW(url);
							yield return www;
							currentMod.SmallIcon = www.texture;
						}
					}
				}
			}
		}
		yield break;
	}

	// Token: 0x06001C5A RID: 7258 RVA: 0x000FC3D4 File Offset: 0x000FA5D4
	private IEnumerator DownloadSmallIcon()
	{
		foreach (KeyValuePair<int, MasteringMod> pair in ModsStorage.Instance().Mods)
		{
			MasteringMod currentMod = pair.Value;
			if (!(currentMod.SmallIcon != null))
			{
				if (currentMod.Type != ModType.camo || !currentMod.IsBasic)
				{
					string url = WWWUtil.modIconWWW(string.Concat(new object[]
					{
						currentMod.Type,
						"_",
						currentMod.EngShortName,
						"_small"
					}));
					if (currentMod.Type == ModType.camo)
					{
						url = WWWUtil.camoIconWWW(string.Concat(new object[]
						{
							currentMod.Type,
							"_",
							currentMod.EngShortName,
							"_small"
						}));
					}
					WWW www = new WWW(url);
					yield return www;
					currentMod.SmallIcon = www.texture;
				}
			}
		}
		this._smallIconsLoaded = true;
		yield break;
	}

	// Token: 0x06001C5B RID: 7259 RVA: 0x000FC3F0 File Offset: 0x000FA5F0
	private IEnumerator DownloadBigIcon()
	{
		foreach (KeyValuePair<int, MasteringMod> pair in ModsStorage.Instance().Mods)
		{
			MasteringMod currentMod = pair.Value;
			if (!(currentMod.BigIcon != null))
			{
				if (currentMod.Type != ModType.camo || !currentMod.IsBasic)
				{
					string url = WWWUtil.modIconWWW(string.Concat(new object[]
					{
						currentMod.Type,
						"_",
						currentMod.EngShortName,
						"_big"
					}));
					if (currentMod.Type == ModType.camo)
					{
						url = WWWUtil.camoIconWWW(string.Concat(new object[]
						{
							currentMod.Type,
							"_",
							currentMod.EngShortName,
							"_big"
						}));
					}
					WWW www = new WWW(url);
					yield return www;
					currentMod.BigIcon = www.texture;
				}
			}
		}
		this._bigIconsLoaded = true;
		yield break;
	}

	// Token: 0x06001C5C RID: 7260 RVA: 0x000FC40C File Offset: 0x000FA60C
	private IEnumerator DownloadCamouflageTexture()
	{
		foreach (KeyValuePair<int, MasteringMod> pair in ModsStorage.Instance().Mods)
		{
			MasteringMod currentMod = pair.Value;
			if (currentMod.Type == ModType.camo && !(currentMod.Texture != null))
			{
				if (currentMod.Type != ModType.camo || !currentMod.IsBasic)
				{
					string url = WWWUtil.camoTextureWWW(string.Concat(new object[]
					{
						currentMod.Type,
						"_",
						currentMod.EngShortName,
						"_texture"
					}));
					WWW www = new WWW(url);
					yield return www;
					if (!string.IsNullOrEmpty(www.error))
					{
						Debug.LogError(www.url + "\n" + www.error);
					}
					else
					{
						Texture2D texture = UnityEngine.Object.Instantiate(www.assetBundle.mainAsset) as Texture2D;
						currentMod.Texture = texture;
					}
				}
			}
		}
		this._camouflagesLoaded = true;
		yield break;
	}

	// Token: 0x06001C5D RID: 7261 RVA: 0x000FC428 File Offset: 0x000FA628
	private IEnumerator DownloadSelected(List<MasteringMod> mods)
	{
		foreach (MasteringMod currentMod in mods)
		{
			if (currentMod.Type != ModType.camo || !currentMod.IsBasic)
			{
				if (currentMod.BigIcon == null)
				{
					string url = WWWUtil.modIconWWW(string.Concat(new object[]
					{
						currentMod.Type,
						"_",
						currentMod.EngShortName,
						"_big"
					}));
					if (currentMod.Type == ModType.camo)
					{
						url = WWWUtil.camoIconWWW(string.Concat(new object[]
						{
							currentMod.Type,
							"_",
							currentMod.EngShortName,
							"_big"
						}));
					}
					WWW www = new WWW(url);
					yield return www;
					currentMod.BigIcon = www.texture;
				}
				if (currentMod.SmallIcon == null)
				{
					string url = WWWUtil.modIconWWW(string.Concat(new object[]
					{
						currentMod.Type,
						"_",
						currentMod.EngShortName,
						"_small"
					}));
					if (currentMod.Type == ModType.camo)
					{
						url = WWWUtil.camoIconWWW(string.Concat(new object[]
						{
							currentMod.Type,
							"_",
							currentMod.EngShortName,
							"_small"
						}));
					}
					WWW www = new WWW(url);
					yield return www;
					currentMod.SmallIcon = www.texture;
				}
				if (currentMod.Type == ModType.camo || currentMod.Texture == null)
				{
					string url = WWWUtil.camoTextureWWW(string.Concat(new object[]
					{
						currentMod.Type,
						"_",
						currentMod.EngShortName,
						"_texture"
					}));
					WWW www = new WWW(url);
					yield return www;
					Texture2D texture = UnityEngine.Object.Instantiate(www.assetBundle.mainAsset) as Texture2D;
					currentMod.Texture = texture;
				}
			}
		}
		yield break;
	}

	// Token: 0x06001C5E RID: 7262 RVA: 0x000FC44C File Offset: 0x000FA64C
	public void LoadModIcons()
	{
		base.StartCoroutine(this.DownloadSmallIcon());
		base.StartCoroutine(this.DownloadBigIcon());
		base.StartCoroutine(this.DownloadCamouflageTexture());
	}

	// Token: 0x06001C5F RID: 7263 RVA: 0x000FC480 File Offset: 0x000FA680
	public void LoadInstalledModsIcons()
	{
		base.StartCoroutine(this.DownloadInstalledModsIcons());
	}

	// Token: 0x06001C60 RID: 7264 RVA: 0x000FC490 File Offset: 0x000FA690
	private void Start()
	{
		ModIconsDownloader.Instance = this;
	}

	// Token: 0x04002136 RID: 8502
	public static ModIconsDownloader Instance;

	// Token: 0x04002137 RID: 8503
	private bool _smallIconsLoaded;

	// Token: 0x04002138 RID: 8504
	private bool _bigIconsLoaded;

	// Token: 0x04002139 RID: 8505
	private bool _camouflagesLoaded;
}
