using System;
using System.Collections.Generic;
using JsonFx.Json;

// Token: 0x0200008B RID: 139
internal static class JSONExtensions
{
	// Token: 0x06000303 RID: 771 RVA: 0x00015ACC File Offset: 0x00013CCC
	public static void Read(this Convertible obj, Dictionary<string, object> dict)
	{
		obj.Convert(dict, false);
	}

	// Token: 0x06000304 RID: 772 RVA: 0x00015AD8 File Offset: 0x00013CD8
	public static void Write(this Convertible obj, Dictionary<string, object> dict)
	{
		obj.Convert(dict, true);
	}

	// Token: 0x06000305 RID: 773 RVA: 0x00015AE4 File Offset: 0x00013CE4
	public static string ToJSON(this Convertible obj)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		obj.Write(dictionary);
		return JsonWriter.Serialize(dictionary);
	}

	// Token: 0x06000306 RID: 774 RVA: 0x00015B04 File Offset: 0x00013D04
	public static void FromJSON(this Convertible obj, string json)
	{
		Dictionary<string, object> dict = (Dictionary<string, object>)JsonReader.Deserialize(json);
		obj.Convert(dict, false);
	}
}
