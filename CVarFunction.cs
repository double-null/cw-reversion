using System;
using System.Reflection;

// Token: 0x02000074 RID: 116
[AttributeUsage(AttributeTargets.All)]
[Obfuscation(Exclude = true, ApplyToMembers = true)]
internal class CVarFunction : Attribute
{
	// Token: 0x0600021B RID: 539 RVA: 0x000120EC File Offset: 0x000102EC
	public CVarFunction(string description, CVarType type = CVarType.t_unknown, EPermission permission = EPermission.Moder)
	{
		this.description = description;
		this.type = type;
		this.Permission = permission;
	}

	// Token: 0x0400027C RID: 636
	public CVarType type;

	// Token: 0x0400027D RID: 637
	public EPermission Permission = EPermission.Moder;

	// Token: 0x0400027E RID: 638
	public string description = string.Empty;
}
