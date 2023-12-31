using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002DA RID: 730
[Serializable]
internal class UserSettings : Convertible
{
	// Token: 0x0600145A RID: 5210 RVA: 0x000D7F40 File Offset: 0x000D6140
	public bool LoaderBool(LoaderSettings s)
	{
		if (s == LoaderSettings.average)
		{
			return this.loaderSettings == LoaderSettings.average;
		}
		if (s == LoaderSettings.full)
		{
			return this.loaderSettings == LoaderSettings.full;
		}
		return s == LoaderSettings.lq && this.loaderSettings == LoaderSettings.lq;
	}

	// Token: 0x0600145B RID: 5211 RVA: 0x000D7F78 File Offset: 0x000D6178
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		if (isWrite)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			int width = this.resolution.width;
			int height = this.resolution.height;
			JSON.ReadWrite(dictionary, "width", ref width, isWrite);
			JSON.ReadWrite(dictionary, "height", ref height, isWrite);
			dict.Add("res", dictionary);
		}
		else
		{
			Dictionary<string, object> dict2 = (Dictionary<string, object>)dict["res"];
			int width2 = this.resolution.width;
			int height2 = this.resolution.height;
			JSON.ReadWrite(dict2, "width", ref width2, isWrite);
			JSON.ReadWrite(dict2, "height", ref height2, isWrite);
			this.resolution.width = width2;
			this.resolution.height = height2;
		}
		JSON.ReadWrite(dict, "fullScreenOnStart", ref this.fullScreenOnStart, isWrite);
		JSON.ReadWrite<UserGraphics>(dict, "gr", ref this.graphics, isWrite);
		JSON.ReadWrite(dict, "disableShadows", ref this.disableShadows, isWrite);
		JSON.ReadWrite(dict, "disableBlur", ref this.disableBlur, isWrite);
		JSON.ReadWrite(dict, "radarAlpha", ref this.radarAlpha, isWrite);
		JSON.ReadWrite(dict, "globalLoudness", ref this.globalLoudness, isWrite);
		JSON.ReadWrite(dict, "soundLoudness", ref this.soundLoudness, isWrite);
		JSON.ReadWrite(dict, "musicLoudness", ref this.musicLoudness, isWrite);
		JSON.ReadWrite<UserBinds>(dict, "binds", ref this.binds, isWrite);
		JSON.ReadWriteEnum<LoaderSettings>(dict, "loaderSettings", ref this.loaderSettings, isWrite);
	}

	// Token: 0x040018FF RID: 6399
	public UserGraphics graphics = new UserGraphics();

	// Token: 0x04001900 RID: 6400
	public Resolution resolution;

	// Token: 0x04001901 RID: 6401
	public bool fullScreenOnStart;

	// Token: 0x04001902 RID: 6402
	public bool disableShadows;

	// Token: 0x04001903 RID: 6403
	public bool disableBlur;

	// Token: 0x04001904 RID: 6404
	public float radarAlpha = 1f;

	// Token: 0x04001905 RID: 6405
	public float globalLoudness = 1f;

	// Token: 0x04001906 RID: 6406
	public float soundLoudness = 1f;

	// Token: 0x04001907 RID: 6407
	public float musicLoudness = 1f;

	// Token: 0x04001908 RID: 6408
	public UserBinds binds = new UserBinds();

	// Token: 0x04001909 RID: 6409
	public LoaderSettings loaderSettings;
}
