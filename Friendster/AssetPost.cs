using System;

namespace Friendster
{
	// Token: 0x020000CA RID: 202
	internal class AssetPost
	{
		// Token: 0x040004A3 RID: 1187
		public string api_key = string.Empty;

		// Token: 0x040004A4 RID: 1188
		public string session_key = string.Empty;

		// Token: 0x040004A5 RID: 1189
		public string asset_serial = string.Empty;

		// Token: 0x040004A6 RID: 1190
		public byte[] bin = new byte[0];

		// Token: 0x040004A7 RID: 1191
		public int type;

		// Token: 0x040004A8 RID: 1192
		public float nonce;

		// Token: 0x040004A9 RID: 1193
		public string sig = string.Empty;

		// Token: 0x040004AA RID: 1194
		public string format = string.Empty;
	}
}
