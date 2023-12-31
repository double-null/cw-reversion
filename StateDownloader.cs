using System;
using System.Collections.Generic;

// Token: 0x0200009C RID: 156
[Serializable]
internal class StateDownloader
{
	// Token: 0x06000380 RID: 896 RVA: 0x000195F8 File Offset: 0x000177F8
	public void Init(StateDownloaderFinishedCallback finish, StateDownloaderFinishedCallback finishCallback = null)
	{
		this._finish = finish;
		this._finishCallback = finishCallback;
	}

	// Token: 0x06000381 RID: 897 RVA: 0x00019608 File Offset: 0x00017808
	public void Download(string url)
	{
		this._urls.Add(url);
	}

	// Token: 0x06000382 RID: 898 RVA: 0x00019618 File Offset: 0x00017818
	public void Finish(List<Downloader> downloaders)
	{
		if (this._finish != null)
		{
			this._finish();
		}
		if (this._finishCallback != null)
		{
			this._finishCallback();
		}
	}

	// Token: 0x06000383 RID: 899 RVA: 0x00019654 File Offset: 0x00017854
	public bool IsFinished(List<Downloader> downloaders)
	{
		foreach (string b in this._urls)
		{
			bool flag = false;
			foreach (Downloader downloader in downloaders)
			{
				if (downloader.Url == b && downloader.Finished)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000384 RID: 900 RVA: 0x00019738 File Offset: 0x00017938
	public float Progress(List<Downloader> downloaders)
	{
		float num = 0f;
		float num2 = 0f;
		for (int i = 0; i < this._urls.Count; i++)
		{
			for (int j = 0; j < downloaders.Count; j++)
			{
				if (downloaders[j].Url == this._urls[i])
				{
					if (downloaders[j].FileSize == 0f)
					{
						return 1f;
					}
					num += downloaders[j].DownloadedSize;
					num2 += downloaders[j].FileSize;
				}
			}
		}
		return num / num2;
	}

	// Token: 0x06000385 RID: 901 RVA: 0x000197E4 File Offset: 0x000179E4
	public List<Downloader> GetDownloaders(List<Downloader> downloaders)
	{
		List<Downloader> list = new List<Downloader>();
		foreach (string b in this._urls)
		{
			foreach (Downloader downloader in downloaders)
			{
				if (downloader.Url == b)
				{
					list.Add(downloader);
				}
			}
		}
		return list;
	}

	// Token: 0x0400039A RID: 922
	public string Name = string.Empty;

	// Token: 0x0400039B RID: 923
	private List<string> _urls = new List<string>();

	// Token: 0x0400039C RID: 924
	private StateDownloaderFinishedCallback _finish;

	// Token: 0x0400039D RID: 925
	private StateDownloaderFinishedCallback _finishCallback;
}
