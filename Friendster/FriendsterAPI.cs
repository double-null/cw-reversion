using System;
using System.Collections;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace Friendster
{
	// Token: 0x020000CB RID: 203
	internal class FriendsterAPI : MonoBehaviour
	{
		// Token: 0x06000537 RID: 1335 RVA: 0x0002130C File Offset: 0x0001F50C
		private void Start()
		{
			string url = "https://alfa2.contractwarsgame.com/content/index.html?src=canvas&api_domain=www.friendster.com&api_key=7f96cb416716d2263e0ecc5f38a76d25&expires=1409324981&instance_id=1&lang=en&nonce=1409238581.8233156&sandbox=true&session_key=d7713974-7b7e-0d8d-3798-d34bf9fad5c9&country=RU&user_id=206245710&signed_keys=src,api_domain,api_key,expires,instance_id,lang,nonce,sandbox,session_key,country,user_id,signed_keys&sig=cb87116323fbfda1b8bbbf5e82a806a6";
			WallPost wallPost = new WallPost();
			this.ParseURL<WallPost>(url, wallPost);
			wallPost.label = "sdfsfgsfg";
			wallPost.subject = "sdfgsfg";
			string text = wallPost.Query("http://api.friendster.com/v1/");
			Debug.Log(text);
			WWW www = new WWW(text, new byte[0]);
			base.StartCoroutine(this.Post(www));
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00021370 File Offset: 0x0001F570
		private IEnumerator Post(WWW www)
		{
			yield return 0;
			while (!www.isDone)
			{
				yield return new WaitForEndOfFrame();
			}
			Debug.Log(www.text);
			yield break;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00021394 File Offset: 0x0001F594
		public void ParseURL<T>(string url, T post)
		{
			string[] array = url.Split(new char[]
			{
				'?'
			});
			if (array.Length > 1)
			{
				string[] array2 = array[1].Split(new char[]
				{
					'&'
				});
				foreach (string text in array2)
				{
					string[] array4 = text.Split(new char[]
					{
						'='
					});
					if (array4.Length > 1)
					{
						FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.Public);
						foreach (FieldInfo fieldInfo in fields)
						{
							if (fieldInfo.Name == array4[0])
							{
								if (fieldInfo.FieldType == typeof(string))
								{
									fieldInfo.SetValue(post, array4[1]);
								}
								if (fieldInfo.FieldType == typeof(int))
								{
									int num = 0;
									if (int.TryParse(array4[1], out num))
									{
										fieldInfo.SetValue(post, num);
									}
								}
								if (fieldInfo.FieldType == typeof(float))
								{
									float num2 = 0f;
									if (float.TryParse(array4[1], out num2))
									{
										fieldInfo.SetValue(post, num2);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00021500 File Offset: 0x0001F700
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
	}
}
