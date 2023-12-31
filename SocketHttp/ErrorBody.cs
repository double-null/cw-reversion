using System;
using System.Text;

namespace SocketHttp
{
	// Token: 0x02000367 RID: 871
	internal class ErrorBody : Body
	{
		// Token: 0x06001C6F RID: 7279 RVA: 0x000FC6B0 File Offset: 0x000FA8B0
		public ErrorBody(string str)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(str);
			this._bytes.Write(bytes, 0, bytes.Length);
		}
	}
}
