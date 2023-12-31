using System;
using System.Collections.Generic;

// Token: 0x0200007A RID: 122
[Serializable]
internal class CommandLineArgs
{
	// Token: 0x06000277 RID: 631 RVA: 0x00014330 File Offset: 0x00012530
	public CommandLineArgs()
	{
	}

	// Token: 0x06000278 RID: 632 RVA: 0x00014350 File Offset: 0x00012550
	public CommandLineArgs(string text)
	{
		this._cmdline = text;
		string[] array = text.Split(new char[]
		{
			' ',
			'\n'
		});
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].Contains("--"))
			{
				string[] array2 = array[i].Split(new char[]
				{
					'='
				});
				if (array2.Length == 2)
				{
					this.dict.Add(array2[0].Replace(" ", string.Empty).Replace("\n", string.Empty), array2[1].Replace(" ", string.Empty).Replace("\n", string.Empty));
				}
				else
				{
					this.dict.Add(array2[0].Replace(" ", string.Empty).Replace("\n", string.Empty), string.Empty);
				}
			}
		}
	}

	// Token: 0x17000035 RID: 53
	// (get) Token: 0x06000279 RID: 633 RVA: 0x00014460 File Offset: 0x00012660
	public string CmdLine
	{
		get
		{
			return this._cmdline;
		}
	}

	// Token: 0x0600027A RID: 634 RVA: 0x00014468 File Offset: 0x00012668
	public bool HasValue(string key)
	{
		return this.dict.ContainsKey(key);
	}

	// Token: 0x0600027B RID: 635 RVA: 0x00014478 File Offset: 0x00012678
	public string TryGetValue(string key)
	{
		string empty = string.Empty;
		this.dict.TryGetValue(key, out empty);
		return empty;
	}

	// Token: 0x04000323 RID: 803
	private Dictionary<string, string> dict = new Dictionary<string, string>();

	// Token: 0x04000324 RID: 804
	private string _cmdline = string.Empty;
}
