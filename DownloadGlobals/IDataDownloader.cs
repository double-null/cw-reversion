using System;

namespace DownloadGlobals
{
	// Token: 0x0200005F RID: 95
	internal interface IDataDownloader
	{
		// Token: 0x06000166 RID: 358
		void Download();

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000167 RID: 359
		bool IsDone { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000168 RID: 360
		bool IsSuccess { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000169 RID: 361
		bool IsError { get; }
	}
}
