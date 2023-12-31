using System;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace Friendster
{
	// Token: 0x020000CD RID: 205
	internal class WallPost
	{
		// Token: 0x0600053D RID: 1341 RVA: 0x00021604 File Offset: 0x0001F804
		public string GetParam(string paramName, string param)
		{
			if (param.Length > 0)
			{
				return paramName + "=" + param;
			}
			return string.Empty;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00021624 File Offset: 0x0001F824
		private string ReflectionCollectFiled()
		{
			StringBuilder stringBuilder = new StringBuilder();
			FieldInfo[] fields = typeof(WallPost).GetFields(BindingFlags.Instance | BindingFlags.Public);
			Array.Sort<FieldInfo>(fields, (FieldInfo info1, FieldInfo info2) => string.Compare(info1.Name, info2.Name, StringComparison.Ordinal));
			stringBuilder.Append(this._type);
			for (int i = 0; i < fields.Length; i++)
			{
				FieldInfo fieldInfo = fields[i];
				string param = this.GetParam(fieldInfo.Name, (fieldInfo.FieldType != typeof(float)) ? fieldInfo.GetValue(this).ToString() : ((float)fieldInfo.GetValue(this)).ToString("G"));
				if (param.Length > 0)
				{
					if (i > 0 && i < fields.Length - 1)
					{
						stringBuilder.Append("&");
					}
					stringBuilder.Append(param);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0002171C File Offset: 0x0001F91C
		public string Query(string path)
		{
			Uri uri = new Uri(path);
			Debug.Log(uri.PathAndQuery + this._type);
			this.sig = this.CalculateSig(uri.PathAndQuery + this._type).ToLower();
			return path + this.ReflectionCollectFiled();
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00021774 File Offset: 0x0001F974
		private string CalculateSig(string prefix)
		{
			StringBuilder stringBuilder = new StringBuilder();
			FieldInfo[] fields = typeof(WallPost).GetFields(BindingFlags.Instance | BindingFlags.Public);
			Array.Sort<FieldInfo>(fields, (FieldInfo info1, FieldInfo info2) => string.Compare(info1.Name, info2.Name, StringComparison.Ordinal));
			foreach (FieldInfo fieldInfo in fields)
			{
				string param = this.GetParam(fieldInfo.Name, (fieldInfo.FieldType != typeof(float)) ? fieldInfo.GetValue(this).ToString() : ((float)fieldInfo.GetValue(this)).ToString("G"));
				if (param.Length > 0 && fieldInfo.Name != "sig")
				{
					stringBuilder.Append(param);
				}
			}
			this._presig = stringBuilder.ToString();
			Debug.Log("SIG FROM: " + prefix + this._presig);
			return this.CalculateMD5Hash(prefix + this._presig);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00021884 File Offset: 0x0001FA84
		public string CalculateMD5Hash(string input)
		{
			MD5 md = MD5.Create();
			byte[] bytes = Encoding.ASCII.GetBytes(input);
			byte[] array = md.ComputeHash(bytes);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i].ToString("X2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040004AB RID: 1195
		public string api_key = string.Empty;

		// Token: 0x040004AC RID: 1196
		public string sig = string.Empty;

		// Token: 0x040004AD RID: 1197
		public string session_key = string.Empty;

		// Token: 0x040004AE RID: 1198
		public string nonce = string.Empty;

		// Token: 0x040004AF RID: 1199
		public string event_icon_id = string.Empty;

		// Token: 0x040004B0 RID: 1200
		public string template = "<fb:me/>";

		// Token: 0x040004B1 RID: 1201
		public string subject = string.Empty;

		// Token: 0x040004B2 RID: 1202
		public string label = string.Empty;

		// Token: 0x040004B3 RID: 1203
		public string url_fragment = string.Empty;

		// Token: 0x040004B4 RID: 1204
		public string format = string.Empty;

		// Token: 0x040004B5 RID: 1205
		private string _type = "wall?";

		// Token: 0x040004B6 RID: 1206
		private string _presig = string.Empty;
	}
}
