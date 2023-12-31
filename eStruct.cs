using System;

// Token: 0x020000A4 RID: 164
internal interface eStruct<T> where T : struct
{
	// Token: 0x060003C0 RID: 960
	void Clone(T i);

	// Token: 0x060003C1 RID: 961
	void Clear();
}
