using System;
using System.Collections.Generic;

// Token: 0x02000351 RID: 849
internal class MasteringSuitsInfo
{
	// Token: 0x06001C3E RID: 7230 RVA: 0x000FBD80 File Offset: 0x000F9F80
	private MasteringSuitsInfo()
	{
	}

	// Token: 0x17000824 RID: 2084
	// (get) Token: 0x06001C40 RID: 7232 RVA: 0x000FBD94 File Offset: 0x000F9F94
	public static MasteringSuitsInfo Instance
	{
		get
		{
			return MasteringSuitsInfo._instance;
		}
	}

	// Token: 0x06001C41 RID: 7233 RVA: 0x000FBD9C File Offset: 0x000F9F9C
	public void Initialize(string json)
	{
		Dictionary<string, object> dictionary = ArrayUtility.FromJSON(json, string.Empty);
		this.Suits = new Dictionary<int, Suit>();
		foreach (KeyValuePair<string, object> keyValuePair in dictionary)
		{
			int num = int.Parse(keyValuePair.Key);
			this.Suits.Add(num, new Suit(num, (Dictionary<string, object>)keyValuePair.Value));
		}
	}

	// Token: 0x04002105 RID: 8453
	private static readonly MasteringSuitsInfo _instance = new MasteringSuitsInfo();

	// Token: 0x04002106 RID: 8454
	public Dictionary<int, Suit> Suits;
}
