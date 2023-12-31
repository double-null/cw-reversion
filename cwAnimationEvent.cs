using System;

// Token: 0x020002E3 RID: 739
[Serializable]
public class cwAnimationEvent
{
	// Token: 0x0600147F RID: 5247 RVA: 0x000D8D9C File Offset: 0x000D6F9C
	public override string ToString()
	{
		return this.clipName + " - " + this.methodName;
	}

	// Token: 0x0400193F RID: 6463
	public string clipName = string.Empty;

	// Token: 0x04001940 RID: 6464
	public string methodName = string.Empty;

	// Token: 0x04001941 RID: 6465
	public float time;
}
