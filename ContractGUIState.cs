using System;

// Token: 0x0200012F RID: 303
[Serializable]
internal class ContractGUIState
{
	// Token: 0x060007F9 RID: 2041 RVA: 0x00048BEC File Offset: 0x00046DEC
	public ContractGUIState()
	{
	}

	// Token: 0x060007FA RID: 2042 RVA: 0x00048C00 File Offset: 0x00046E00
	public ContractGUIState(int ID, int current, int maxCount, ContractTaskType type, ContractInfo contract)
	{
		this.description = contract.description;
		this.MaxProgress = maxCount;
		this.currentProgress = current;
		this.iContractNum = ID;
		this.iRewardCR = contract.prizeCR;
		this.iRewardGP = contract.prizeGP;
		this.iRewardSP = contract.prizeSP;
		this.type = type;
		this.completed = (current == maxCount);
	}

	// Token: 0x04000871 RID: 2161
	public int iRewardCR;

	// Token: 0x04000872 RID: 2162
	public int iRewardGP;

	// Token: 0x04000873 RID: 2163
	public int iRewardSP;

	// Token: 0x04000874 RID: 2164
	public ContractTaskType type;

	// Token: 0x04000875 RID: 2165
	public int iContractNum;

	// Token: 0x04000876 RID: 2166
	public int MaxProgress;

	// Token: 0x04000877 RID: 2167
	public int currentProgress;

	// Token: 0x04000878 RID: 2168
	public string description = string.Empty;

	// Token: 0x04000879 RID: 2169
	public bool completed;
}
