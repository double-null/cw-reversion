using System;

// Token: 0x02000078 RID: 120
internal interface ReusableClass<T> where T : class, new()
{
	// Token: 0x0600026B RID: 619
	void Clear();

	// Token: 0x0600026C RID: 620
	void Clone(T i);
}
