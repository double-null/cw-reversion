using System;
using System.Reflection;

// Token: 0x02000073 RID: 115
[AttributeUsage(AttributeTargets.All)]
[Obfuscation(Exclude = true, ApplyToMembers = true)]
internal class CVar : Attribute
{
	// Token: 0x06000219 RID: 537 RVA: 0x0001202C File Offset: 0x0001022C
	public CVar(string description, string minValue = "", string maxValue = "", CVarType type = CVarType.t_unknown, EPermission permission = EPermission.Moder)
	{
		this.description = description;
		this.minValue = minValue;
		this.maxValue = maxValue;
		this.type = type;
		this.Permission = permission;
	}

	// Token: 0x0600021A RID: 538 RVA: 0x00012098 File Offset: 0x00010298
	public CVar(string description, CVarType type)
	{
		this.description = description;
		this.type = type;
	}

	// Token: 0x04000276 RID: 630
	public string default_value = string.Empty;

	// Token: 0x04000277 RID: 631
	public CVarType type;

	// Token: 0x04000278 RID: 632
	public EPermission Permission = EPermission.Moder;

	// Token: 0x04000279 RID: 633
	public string description = string.Empty;

	// Token: 0x0400027A RID: 634
	public string minValue = string.Empty;

	// Token: 0x0400027B RID: 635
	public string maxValue = string.Empty;
}
