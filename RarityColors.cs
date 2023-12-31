using System;
using System.Collections.Generic;

// Token: 0x02000358 RID: 856
public static class RarityColors
{
	// Token: 0x06001C4A RID: 7242 RVA: 0x000FC024 File Offset: 0x000FA224
	public static void Initialize(string json)
	{
		Dictionary<string, object> dictionary = ArrayUtility.FromJSON(json, string.Empty);
		RarityColors.Colors = new Dictionary<string, object>(dictionary);
	}

	// Token: 0x04002110 RID: 8464
	public static Dictionary<string, object> Colors;
}
