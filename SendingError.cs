using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200017D RID: 381
internal class SendingError
{
	// Token: 0x06000AE1 RID: 2785 RVA: 0x00084354 File Offset: 0x00082554
	public static void SendError()
	{
		SendingError._data.Clear();
		SendingError._errorList.Clear();
		SendingError._data.Add("SystemInfo", PerformanceMeter.GetSystemInfo());
		for (int i = 0; i < global::Console.Entries.Count; i++)
		{
			if (global::Console.Entries[i].IsErrorEntry)
			{
				SendingError._errorList.Add(global::Console.Entries[i].ToDict());
			}
		}
		SendingError._data.Add("Errors", SendingError._errorList);
		string message = ArrayUtility.ToJSON<string, object>(SendingError._data);
		Debug.Log(message);
	}

	// Token: 0x04000CE9 RID: 3305
	private static Dictionary<string, object> _data = new Dictionary<string, object>();

	// Token: 0x04000CEA RID: 3306
	private static List<object> _errorList = new List<object>();
}
