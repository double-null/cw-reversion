using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JsonFx.Json;
using UnityEngine;

// Token: 0x02000167 RID: 359
internal class NetworkSettingsController
{
	// Token: 0x060009B0 RID: 2480 RVA: 0x00068D50 File Offset: 0x00066F50
	private NetworkSettingsController()
	{
	}

	// Token: 0x1700012E RID: 302
	// (get) Token: 0x060009B1 RID: 2481 RVA: 0x00068D58 File Offset: 0x00066F58
	public int TotalWeaponsSize
	{
		get
		{
			return this.totalWeaponsSize;
		}
	}

	// Token: 0x1700012F RID: 303
	// (get) Token: 0x060009B2 RID: 2482 RVA: 0x00068D60 File Offset: 0x00066F60
	public int TotalMapsSize
	{
		get
		{
			return this.totalMapsSize;
		}
	}

	// Token: 0x17000130 RID: 304
	// (get) Token: 0x060009B3 RID: 2483 RVA: 0x00068D68 File Offset: 0x00066F68
	public string TotalWeaponsSizeStr
	{
		get
		{
			return this.divideIntoOrders(this.totalWeaponsSize);
		}
	}

	// Token: 0x17000131 RID: 305
	// (get) Token: 0x060009B4 RID: 2484 RVA: 0x00068D78 File Offset: 0x00066F78
	public string TotalMapsSizeStr
	{
		get
		{
			return this.divideIntoOrders(this.totalMapsSize);
		}
	}

	// Token: 0x17000132 RID: 306
	// (get) Token: 0x060009B5 RID: 2485 RVA: 0x00068D88 File Offset: 0x00066F88
	public bool Prepared
	{
		get
		{
			return this.prepared;
		}
	}

	// Token: 0x17000133 RID: 307
	// (get) Token: 0x060009B6 RID: 2486 RVA: 0x00068D90 File Offset: 0x00066F90
	// (set) Token: 0x060009B7 RID: 2487 RVA: 0x00068D98 File Offset: 0x00066F98
	public bool ReceivingCanceled
	{
		get
		{
			return this.receivingCanceled;
		}
		set
		{
			this.receivingCanceled = value;
		}
	}

	// Token: 0x060009B8 RID: 2488 RVA: 0x00068DA4 File Offset: 0x00066FA4
	public void cancelReceive()
	{
		this.receivingCanceled = true;
		this.prepared = false;
	}

	// Token: 0x060009B9 RID: 2489 RVA: 0x00068DB4 File Offset: 0x00066FB4
	private void initWWWStrings()
	{
		this.WWWStrings = new List<string>();
		for (int i = 0; i < 101; i++)
		{
			this.WWWStrings.Add(WWWUtil.lodsWWW(EnumNames.Weapons(i)).Replace(".unity3d", ".meta"));
			this.WWWStrings.Add(WWWUtil.weaponsWWW(EnumNames.Weapons(i)).Replace(".unity3d", ".meta"));
			this.WWWStrings.Add(WWWUtil.handsWWW(EnumNames.Weapons(i)).Replace(".unity3d", ".meta"));
		}
		for (int j = 0; j < Globals.I.maps.Length; j++)
		{
			this.WWWStrings.Add(WWWUtil.levelsWWW(Globals.I.maps[j].Name).Replace(".unity3d", ".meta"));
		}
	}

	// Token: 0x17000134 RID: 308
	// (get) Token: 0x060009BA RID: 2490 RVA: 0x00068E9C File Offset: 0x0006709C
	public static NetworkSettingsController Instance
	{
		get
		{
			if (NetworkSettingsController.instance == null)
			{
				NetworkSettingsController.instance = new NetworkSettingsController();
			}
			return NetworkSettingsController.instance;
		}
	}

	// Token: 0x060009BB RID: 2491 RVA: 0x00068EB8 File Offset: 0x000670B8
	private void countTotalAndDownloadedSize()
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		for (int i = 0; i < this.WWWs.Count<WWW>(); i++)
		{
			if (this.WWWs[i].error == null)
			{
				Dictionary<string, object> dict = new Dictionary<string, object>();
				try
				{
					dict = ArrayUtility.FromJSON(this.WWWs[i].text, string.Empty);
					JSON.ReadWrite(dict, "size", ref num, false);
				}
				catch (JsonDeserializationException arg)
				{
					Debug.Log("JsonDeserializationException " + arg);
				}
				if (this.WWWs[i].url.Contains("weapons"))
				{
					this.totalWeaponsSize += num / 1024;
					if (Caching.IsVersionCached(this.WWWs[i].url.Replace(".meta", ".unity3d"), this.checkVersion(this.WWWs[i])))
					{
						num2 += num / 1024;
					}
				}
				else if (this.WWWs[i].url.Contains("Levels"))
				{
					this.totalMapsSize += num / 1024;
					if (Caching.IsVersionCached(this.WWWs[i].url.Replace(".meta", ".unity3d"), this.checkVersion(this.WWWs[i])))
					{
						num3 += num / 1024;
					}
				}
			}
		}
		this.weaponsSizeDownloaded = Math.Max(this.weaponsSizeDownloaded, num2);
		this.mapsSizeDownloaded = Math.Max(this.mapsSizeDownloaded, num3);
	}

	// Token: 0x060009BC RID: 2492 RVA: 0x0006908C File Offset: 0x0006728C
	public IEnumerator Prepare()
	{
		if (!this.prepared)
		{
			EventFactory.Call("ShowPopup", new Popup(WindowsID.Load, string.Empty, Language.ReceivingInformation, PopupState.progress, false, true, "cancelReceive", string.Empty));
			this.initWWWStrings();
			this.WWWs = new List<WWW>();
			foreach (string wwwStr in this.WWWStrings)
			{
				WWW www = new WWW(wwwStr);
				while (!www.isDone)
				{
					yield return new WaitForEndOfFrame();
				}
				if (www.error == null)
				{
					this.WWWs.Add(www);
				}
			}
			this.prepared = true;
		}
		if (!this.receivingCanceled && this.prepared)
		{
			this.countTotalAndDownloadedSize();
			EventFactory.Call("HidePopup", new Popup(WindowsID.Load, string.Empty, Language.ReceivingInformation, PopupState.progress, false, true, string.Empty, string.Empty));
		}
		yield break;
	}

	// Token: 0x060009BD RID: 2493 RVA: 0x000690A8 File Offset: 0x000672A8
	private int checkVersion(WWW www)
	{
		string empty = string.Empty;
		int result = 0;
		if (www.responseHeaders.TryGetValue("LAST-MODIFIED", out empty))
		{
			DateTime dateTime = default(DateTime);
			if (DateTime.TryParse(empty, out dateTime))
			{
				result = Mathf.Abs((int)dateTime.TimeOfDay.TotalSeconds);
			}
		}
		return result;
	}

	// Token: 0x060009BE RID: 2494 RVA: 0x00069100 File Offset: 0x00067300
	public void reset()
	{
		this.totalWeaponsSize = 0;
		this.totalMapsSize = 0;
	}

	// Token: 0x17000135 RID: 309
	// (get) Token: 0x060009BF RID: 2495 RVA: 0x00069110 File Offset: 0x00067310
	public int WeaponsSizeDownloaded
	{
		get
		{
			if (this.weaponsSizeDownloaded == this.totalWeaponsSize)
			{
				return this.weaponsSizeDownloaded;
			}
			List<Downloader> weaponsLoaders = SingletoneForm<Loader>.Instance.WeaponsLoaders;
			int num = 0;
			for (int i = 0; i < weaponsLoaders.Count; i++)
			{
				num += (int)(weaponsLoaders[i].DownloadedSize / 1024f);
			}
			this.weaponsSizeDownloaded = Math.Max(this.weaponsSizeDownloaded, num);
			if (this.weaponsSizeDownloaded != 0 && this.weaponsSizeDownloaded >= this.totalWeaponsSize)
			{
				this.weaponsSizeDownloaded = this.totalWeaponsSize;
				Loader.Unload(false);
				EventFactory.Call("HidePopup", new Popup(WindowsID.Load, string.Empty, Language.DownloadWeapons, PopupState.progress, false, true, string.Empty, string.Empty));
			}
			return this.weaponsSizeDownloaded;
		}
	}

	// Token: 0x17000136 RID: 310
	// (get) Token: 0x060009C0 RID: 2496 RVA: 0x000691E0 File Offset: 0x000673E0
	public string WeaponsSizeDownloadedStr
	{
		get
		{
			return this.divideIntoOrders(this.weaponsSizeDownloaded);
		}
	}

	// Token: 0x17000137 RID: 311
	// (get) Token: 0x060009C1 RID: 2497 RVA: 0x000691F0 File Offset: 0x000673F0
	public int MapsSizeDownloaded
	{
		get
		{
			if (this.mapsSizeDownloaded == this.totalMapsSize)
			{
				return this.mapsSizeDownloaded;
			}
			List<Downloader> mapsLoaders = SingletoneForm<Loader>.Instance.MapsLoaders;
			int num = 0;
			for (int i = 0; i < mapsLoaders.Count; i++)
			{
				num += (int)(mapsLoaders[i].DownloadedSize / 1024f);
			}
			this.mapsSizeDownloaded = Math.Max(this.mapsSizeDownloaded, num);
			if (this.mapsSizeDownloaded != 0 && this.mapsSizeDownloaded >= this.totalMapsSize)
			{
				this.mapsSizeDownloaded = this.totalMapsSize;
				Loader.Unload(false);
				EventFactory.Call("HidePopup", new Popup(WindowsID.Load, string.Empty, Language.DownloadMaps, PopupState.progress, false, true, string.Empty, string.Empty));
			}
			return this.mapsSizeDownloaded;
		}
	}

	// Token: 0x17000138 RID: 312
	// (get) Token: 0x060009C2 RID: 2498 RVA: 0x000692C0 File Offset: 0x000674C0
	public string MapsSizeDownloadedStr
	{
		get
		{
			return this.divideIntoOrders(this.mapsSizeDownloaded);
		}
	}

	// Token: 0x060009C3 RID: 2499 RVA: 0x000692D0 File Offset: 0x000674D0
	private string divideIntoOrders(int number)
	{
		int i = number;
		List<int> list = new List<int>();
		while (i > 0)
		{
			list.Add(i % 1000);
			i /= 1000;
		}
		list.Reverse();
		StringBuilder stringBuilder = new StringBuilder();
		if (list.Count > 0)
		{
			stringBuilder.Append(list[0] + " ");
			for (int j = 1; j < list.Count; j++)
			{
				stringBuilder.Append(list[j].ToString("D3") + " ");
			}
		}
		else
		{
			stringBuilder.Append("0 ");
		}
		return stringBuilder.ToString();
	}

	// Token: 0x04000B2A RID: 2858
	private static NetworkSettingsController instance;

	// Token: 0x04000B2B RID: 2859
	private List<string> WWWStrings;

	// Token: 0x04000B2C RID: 2860
	private List<WWW> WWWs;

	// Token: 0x04000B2D RID: 2861
	private int totalWeaponsSize;

	// Token: 0x04000B2E RID: 2862
	private int totalMapsSize;

	// Token: 0x04000B2F RID: 2863
	private int weaponsSizeDownloaded;

	// Token: 0x04000B30 RID: 2864
	private int mapsSizeDownloaded;

	// Token: 0x04000B31 RID: 2865
	private bool prepared;

	// Token: 0x04000B32 RID: 2866
	public bool receivingCanceled;
}
