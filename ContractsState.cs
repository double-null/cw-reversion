using System;
using System.Collections.Generic;

// Token: 0x02000255 RID: 597
[Serializable]
internal class ContractsState : Convertible
{
	// Token: 0x06001241 RID: 4673 RVA: 0x000C8EFC File Offset: 0x000C70FC
	public ContractsState()
	{
	}

	// Token: 0x06001242 RID: 4674 RVA: 0x000C8F2C File Offset: 0x000C712C
	public ContractsState(Dictionary<string, object> dict)
	{
		this.easy = this.ParseContractArray(dict, "easy");
		this.normal = this.ParseContractArray(dict, "normal");
		this.hard = this.ParseContractArray(dict, "hard");
	}

	// Token: 0x17000277 RID: 631
	// (get) Token: 0x06001243 RID: 4675 RVA: 0x000C8F94 File Offset: 0x000C7194
	public ContractInfo[] Easy
	{
		get
		{
			return this.easy;
		}
	}

	// Token: 0x17000278 RID: 632
	// (get) Token: 0x06001244 RID: 4676 RVA: 0x000C8F9C File Offset: 0x000C719C
	public ContractInfo[] Normal
	{
		get
		{
			return this.normal;
		}
	}

	// Token: 0x17000279 RID: 633
	// (get) Token: 0x06001245 RID: 4677 RVA: 0x000C8FA4 File Offset: 0x000C71A4
	public ContractInfo[] Hard
	{
		get
		{
			return this.hard;
		}
	}

	// Token: 0x1700027A RID: 634
	// (get) Token: 0x06001246 RID: 4678 RVA: 0x000C8FAC File Offset: 0x000C71AC
	public ContractInfo CurrentEasy
	{
		get
		{
			if (this.easyIndex >= 0)
			{
				return this.easy[this.easyIndex];
			}
			return this.easy[0];
		}
	}

	// Token: 0x1700027B RID: 635
	// (get) Token: 0x06001247 RID: 4679 RVA: 0x000C8FDC File Offset: 0x000C71DC
	public ContractInfo CurrentNormal
	{
		get
		{
			if (this.easyIndex >= 0)
			{
				return this.normal[this.normalIndex];
			}
			return this.normal[0];
		}
	}

	// Token: 0x1700027C RID: 636
	// (get) Token: 0x06001248 RID: 4680 RVA: 0x000C900C File Offset: 0x000C720C
	public ContractInfo CurrentHard
	{
		get
		{
			if (this.easyIndex >= 0)
			{
				return this.hard[this.hardIndex];
			}
			return this.hard[0];
		}
	}

	// Token: 0x1700027D RID: 637
	// (get) Token: 0x06001249 RID: 4681 RVA: 0x000C903C File Offset: 0x000C723C
	// (set) Token: 0x0600124A RID: 4682 RVA: 0x000C9044 File Offset: 0x000C7244
	public int CurrentEasyIndex
	{
		get
		{
			return this.easyIndex;
		}
		set
		{
			this.easyIndex = value;
		}
	}

	// Token: 0x1700027E RID: 638
	// (get) Token: 0x0600124B RID: 4683 RVA: 0x000C9050 File Offset: 0x000C7250
	// (set) Token: 0x0600124C RID: 4684 RVA: 0x000C9058 File Offset: 0x000C7258
	public int CurrentNormalIndex
	{
		get
		{
			return this.normalIndex;
		}
		set
		{
			this.normalIndex = value;
		}
	}

	// Token: 0x1700027F RID: 639
	// (get) Token: 0x0600124D RID: 4685 RVA: 0x000C9064 File Offset: 0x000C7264
	// (set) Token: 0x0600124E RID: 4686 RVA: 0x000C906C File Offset: 0x000C726C
	public int CurrentHardIndex
	{
		get
		{
			return this.hardIndex;
		}
		set
		{
			this.hardIndex = value;
		}
	}

	// Token: 0x17000280 RID: 640
	// (get) Token: 0x0600124F RID: 4687 RVA: 0x000C9078 File Offset: 0x000C7278
	// (set) Token: 0x06001250 RID: 4688 RVA: 0x000C9080 File Offset: 0x000C7280
	public int CurrentEasyCount
	{
		get
		{
			return this.easyCounter;
		}
		set
		{
			this.easyCounter = value;
		}
	}

	// Token: 0x17000281 RID: 641
	// (get) Token: 0x06001251 RID: 4689 RVA: 0x000C908C File Offset: 0x000C728C
	// (set) Token: 0x06001252 RID: 4690 RVA: 0x000C9094 File Offset: 0x000C7294
	public int CurrentNormalCount
	{
		get
		{
			return this.normalCounter;
		}
		set
		{
			this.normalCounter = value;
		}
	}

	// Token: 0x17000282 RID: 642
	// (get) Token: 0x06001253 RID: 4691 RVA: 0x000C90A0 File Offset: 0x000C72A0
	// (set) Token: 0x06001254 RID: 4692 RVA: 0x000C90A8 File Offset: 0x000C72A8
	public int CurrentHardCount
	{
		get
		{
			return this.hardCounter;
		}
		set
		{
			this.hardCounter = value;
		}
	}

	// Token: 0x17000283 RID: 643
	// (get) Token: 0x06001255 RID: 4693 RVA: 0x000C90B4 File Offset: 0x000C72B4
	// (set) Token: 0x06001256 RID: 4694 RVA: 0x000C9128 File Offset: 0x000C7328
	public int DeltaTime
	{
		get
		{
			int num = this.timerEnd - HtmlLayer.serverUtc;
			if (num < 0)
			{
				if (!Main.IsGameLoaded && !this.bContractDatbaseSend)
				{
					Main.AddDatabaseRequest<LoadProfile>(new object[0]);
					this.bContractDatbaseSend = true;
				}
				return num;
			}
			if (this.timerEnd - HtmlLayer.serverUtc >= 0)
			{
				this.bContractDatbaseSend = false;
			}
			return this.timerEnd - HtmlLayer.serverUtc;
		}
		set
		{
			this.timerEnd = value;
		}
	}

	// Token: 0x06001257 RID: 4695 RVA: 0x000C9134 File Offset: 0x000C7334
	private ContractInfo[] ParseContractArray(Dictionary<string, object> dict, string name)
	{
		Dictionary<string, object>[] array = (Dictionary<string, object>[])dict[name];
		ContractInfo[] array2 = new ContractInfo[array.Length];
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i] = new ContractInfo(array[i], name, i);
		}
		return array2;
	}

	// Token: 0x06001258 RID: 4696 RVA: 0x000C917C File Offset: 0x000C737C
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		JSON.ReadWrite(dict, "current_easy", ref this.easyIndex, isWrite);
		JSON.ReadWrite(dict, "current_normal", ref this.normalIndex, isWrite);
		JSON.ReadWrite(dict, "current_hard", ref this.hardIndex, isWrite);
		JSON.ReadWrite(dict, "easy_counter", ref this.easyCounter, isWrite);
		JSON.ReadWrite(dict, "normal_counter", ref this.normalCounter, isWrite);
		JSON.ReadWrite(dict, "hard_counter", ref this.hardCounter, isWrite);
		JSON.ReadWrite(dict, "timer_end", ref this.timerEnd, isWrite);
	}

	// Token: 0x06001259 RID: 4697 RVA: 0x000C9208 File Offset: 0x000C7408
	public int getCurrentCount(int index)
	{
		switch (index)
		{
		case 0:
			return this.easyCounter;
		case 1:
			return this.normalCounter;
		case 2:
			return this.hardCounter;
		default:
			return 0;
		}
	}

	// Token: 0x040011EE RID: 4590
	private bool bContractDatbaseSend;

	// Token: 0x040011EF RID: 4591
	public ContractInfo[] easy;

	// Token: 0x040011F0 RID: 4592
	public ContractInfo[] normal;

	// Token: 0x040011F1 RID: 4593
	public ContractInfo[] hard;

	// Token: 0x040011F2 RID: 4594
	private int easyIndex;

	// Token: 0x040011F3 RID: 4595
	private int normalIndex;

	// Token: 0x040011F4 RID: 4596
	private int hardIndex;

	// Token: 0x040011F5 RID: 4597
	public int easyCounter = -1;

	// Token: 0x040011F6 RID: 4598
	public int normalCounter = -1;

	// Token: 0x040011F7 RID: 4599
	public int hardCounter = -1;

	// Token: 0x040011F8 RID: 4600
	private int timerEnd = -1;
}
