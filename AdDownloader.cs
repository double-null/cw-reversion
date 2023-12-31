using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000062 RID: 98
internal class AdDownloader : MonoBehaviour
{
	// Token: 0x1700001F RID: 31
	// (get) Token: 0x0600017E RID: 382 RVA: 0x0000D2C4 File Offset: 0x0000B4C4
	public Texture2D AdTexture
	{
		get
		{
			return this._adTexture;
		}
	}

	// Token: 0x0600017F RID: 383 RVA: 0x0000D2CC File Offset: 0x0000B4CC
	private IEnumerator DownloadAd()
	{
		string realm = CVars.realm;
		if (realm != null)
		{
			if (AdDownloader.<>f__switch$map2 == null)
			{
				AdDownloader.<>f__switch$map2 = new Dictionary<string, int>(3)
				{
					{
						"standalone",
						0
					},
					{
						"fb",
						1
					},
					{
						"kg",
						1
					}
				};
			}
			int num;
			if (AdDownloader.<>f__switch$map2.TryGetValue(realm, out num))
			{
				if (num == 0)
				{
					ELanguage currentLanguage = Language.CurrentLanguage;
					string lang;
					if (currentLanguage != ELanguage.EN)
					{
						if (currentLanguage == ELanguage.RU)
						{
							lang = "ru1";
							goto IL_C7;
						}
					}
					lang = "en";
					IL_C7:
					this._link = "https://a2-40-so.ssl.ucdn.com/cw/release/img/devgru-bonus-" + lang + ".jpg";
					goto IL_116;
				}
				if (num == 1)
				{
					this._link = "https://a2-40-so.ssl.ucdn.com/cw/release/img/devgru-bonus-en.jpg";
					goto IL_116;
				}
			}
		}
		this._link = "https://a2-40-so.ssl.ucdn.com/cw/release/img/devgru-bonus-ru1.jpg";
		IL_116:
		WWW www = new WWW(this._link);
		yield return www;
		this._adTexture = www.texture;
		yield break;
	}

	// Token: 0x06000180 RID: 384 RVA: 0x0000D2E8 File Offset: 0x0000B4E8
	public void Load()
	{
		base.StartCoroutine(this.DownloadAd());
	}

	// Token: 0x06000181 RID: 385 RVA: 0x0000D2F8 File Offset: 0x0000B4F8
	private void Start()
	{
		AdDownloader.I = this;
	}

	// Token: 0x040001FE RID: 510
	public static AdDownloader I;

	// Token: 0x040001FF RID: 511
	private Texture2D _adTexture;

	// Token: 0x04000200 RID: 512
	private string _link;
}
