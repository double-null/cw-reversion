using System;
using System.Collections.Generic;

// Token: 0x0200009A RID: 154
[Serializable]
internal class Slot
{
	// Token: 0x06000377 RID: 887 RVA: 0x00019398 File Offset: 0x00017598
	public Slot()
	{
		for (int i = 1; i <= 30; i++)
		{
			this.groups.Add(i);
		}
	}

	// Token: 0x17000066 RID: 102
	// (get) Token: 0x06000378 RID: 888 RVA: 0x000193D8 File Offset: 0x000175D8
	public bool HaveSlot
	{
		get
		{
			return this.groups.Count != 0;
		}
	}

	// Token: 0x06000379 RID: 889 RVA: 0x000193EC File Offset: 0x000175EC
	public int Spawn()
	{
		int result = this.groups[0];
		this.groups.RemoveAt(0);
		return result;
	}

	// Token: 0x0600037A RID: 890 RVA: 0x00019414 File Offset: 0x00017614
	public void Despawn(int group)
	{
		this.groups.Add(group);
	}

	// Token: 0x04000398 RID: 920
	private List<int> groups = new List<int>();
}
