using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000092 RID: 146
internal static class MonoExtensionMethods
{
	// Token: 0x06000344 RID: 836 RVA: 0x00017D28 File Offset: 0x00015F28
	public static void printList(this MonoBehaviour obj, params object[] list)
	{
		string text = string.Empty;
		for (int i = 0; i < list.Length; i++)
		{
			text = text + list[i] + ((i != list.Length - 1) ? " | " : string.Empty);
		}
		MonoBehaviour.print(text);
	}

	// Token: 0x06000345 RID: 837 RVA: 0x00017D7C File Offset: 0x00015F7C
	public static IEnumerator WaitForRealTimeSeconds(this MonoBehaviour obj, float duration)
	{
		float last = Time.realtimeSinceStartup;
		float delta = 0f;
		while (delta < duration)
		{
			delta += Time.timeScale * (Time.realtimeSinceStartup - last);
			last = Time.realtimeSinceStartup;
			yield return null;
		}
		yield break;
	}
}
