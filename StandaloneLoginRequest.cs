using System;

// Token: 0x02000237 RID: 567
internal class StandaloneLoginRequest : Login
{
	// Token: 0x0600118B RID: 4491 RVA: 0x000C31EC File Offset: 0x000C13EC
	public override void Initialize(params object[] args)
	{
		this.OnResponse(string.Empty);
	}

	// Token: 0x04001120 RID: 4384
	public const int WRONG_EMAIL_OR_PASS = 1001;

	// Token: 0x04001121 RID: 4385
	public const int ATTEMPTS_LIMIT_EXCEEDED = 1006;

	// Token: 0x04001122 RID: 4386
	public const int WRONG_HASH = 1007;

	// Token: 0x04001123 RID: 4387
	public int Failresult;

	// Token: 0x04001124 RID: 4388
	public int TimeLeft;
}
