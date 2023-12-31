using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x020000A7 RID: 167
[AddComponentMenu("Scripts/Engine/Foundation/UnityHttpRequest")]
internal class UnityHttpRequest : BaseHttpRequest
{
	// Token: 0x060003D3 RID: 979 RVA: 0x0001ADF0 File Offset: 0x00018FF0
	protected override IEnumerator DownloadEnumerator(string Url, string post)
	{
		byte[] simplePostData = new byte[1];
		Hashtable headers = new Hashtable();
		List<string> cookies = HtmlLayer.Cookies;
		lock (cookies)
		{
			for (int i = 0; i < HtmlLayer.Cookies.Count; i++)
			{
				headers.Add("Cookie", HtmlLayer.Cookies[i]);
			}
		}
		if (post != string.Empty)
		{
			this.www = new WWW(Url, Encoding.UTF8.GetBytes(post), headers);
		}
		else
		{
			this.www = new WWW(Url, simplePostData, headers);
		}
		yield return this.www;
		if (this.www.error != null && this.fail != null)
		{
			this.fail(new Exception(this.www.error), Url);
			Downloader.Delete(ref this.www);
			yield break;
		}
		if (CVars.n_httpDebug)
		{
			global::Console.print(this.www.text, Color.green);
		}
		if (this.finish != null)
		{
			List<string> cookies2 = HtmlLayer.Cookies;
			lock (cookies2)
			{
				if (HtmlLayer.Cookies.Count == 0)
				{
					foreach (KeyValuePair<string, string> pair in this.www.responseHeaders)
					{
						if (pair.Key.Contains("SET-COOKIE"))
						{
							HtmlLayer.Cookies.Add(pair.Value);
						}
					}
				}
			}
			this.finish(this.www.text, Url);
		}
		Downloader.Delete(ref this.www);
		UnityEngine.Object.Destroy(this);
		yield break;
	}
}
