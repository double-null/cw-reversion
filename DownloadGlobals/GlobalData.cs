using System;
using System.Collections.Generic;
using JsonFx.Json;
using UnityEngine;

namespace DownloadGlobals
{
	// Token: 0x02000060 RID: 96
	internal class GlobalData<T> : IDataDownloader
	{
		// Token: 0x0600016A RID: 362 RVA: 0x0000CEB4 File Offset: 0x0000B0B4
		public GlobalData(string uri, string Key = "")
		{
			this.uri = uri;
			this.Key = Key;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600016B RID: 363 RVA: 0x0000CEEC File Offset: 0x0000B0EC
		public bool IsDone
		{
			get
			{
				return this._isDone;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600016C RID: 364 RVA: 0x0000CEF4 File Offset: 0x0000B0F4
		public bool IsSuccess
		{
			get
			{
				return this._isSuccess;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600016D RID: 365 RVA: 0x0000CEFC File Offset: 0x0000B0FC
		public bool IsError
		{
			get
			{
				return this._isError;
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000CF04 File Offset: 0x0000B104
		public void Download()
		{
			this._isSuccess = false;
			this._isDone = false;
			this.download = new DownloadGlobal();
			this.download.URL = this.uri;
			this.download.GetResponce(new RequestFinished(this.Success), new RequestFailed(this.Failed));
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000CF60 File Offset: 0x0000B160
		private void Success(string json, string url)
		{
			try
			{
				if (string.IsNullOrEmpty(this.Key))
				{
					this.Data = (T)((object)this.JsonToDict(json));
				}
				else
				{
					this.Data = (T)((object)this.JsonToDict(json)[this.Key]);
				}
				this._isSuccess = true;
			}
			catch (Exception innerException)
			{
				this._isError = true;
				this._isSuccess = false;
				Debug.Log(new Exception(string.Concat(new string[]
				{
					"\n Key: ",
					this.Key,
					" \n URL: ",
					url,
					" JSON:\n",
					json
				}), innerException));
			}
			this._isDone = true;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000D034 File Offset: 0x0000B234
		private void Failed(Exception e, string url)
		{
			Debug.Log(string.Concat(new object[]
			{
				"Download failed ",
				url,
				"\n",
				e
			}));
			this._isError = true;
			this._isDone = true;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000D078 File Offset: 0x0000B278
		protected Dictionary<string, object> JsonToDict(string json)
		{
			return (Dictionary<string, object>)JsonReader.Deserialize(json);
		}

		// Token: 0x040001EE RID: 494
		public T Data;

		// Token: 0x040001EF RID: 495
		private bool _isDone;

		// Token: 0x040001F0 RID: 496
		private bool _isSuccess;

		// Token: 0x040001F1 RID: 497
		private bool _isError;

		// Token: 0x040001F2 RID: 498
		private DownloadGlobal download;

		// Token: 0x040001F3 RID: 499
		private string Key = string.Empty;

		// Token: 0x040001F4 RID: 500
		private string uri = string.Empty;
	}
}
