using System;

// Token: 0x020002A9 RID: 681
[Serializable]
internal class PlayerInput
{
	// Token: 0x06001331 RID: 4913 RVA: 0x000CF390 File Offset: 0x000CD590
	public PlayerInput()
	{
		this.buffer.Add(new CWInput());
	}

	// Token: 0x06001332 RID: 4914 RVA: 0x000CF3B8 File Offset: 0x000CD5B8
	public void Update()
	{
		eCache.cwInput.Clear();
		eCache.cwInput.Update();
		this.buffer.Add(eCache.cwInput);
		if (this.buffer.Length > 1)
		{
			this.buffer.RemoveAt(0);
		}
	}

	// Token: 0x06001333 RID: 4915 RVA: 0x000CF408 File Offset: 0x000CD608
	public void Tick()
	{
		if (this.buffer.Length > 1)
		{
			this.buffer.RemoveAt(0);
		}
		else
		{
			this.Current.FixedUpdate();
		}
	}

	// Token: 0x06001334 RID: 4916 RVA: 0x000CF438 File Offset: 0x000CD638
	public void Clear()
	{
		this.buffer.Clear();
	}

	// Token: 0x170002BC RID: 700
	// (get) Token: 0x06001335 RID: 4917 RVA: 0x000CF448 File Offset: 0x000CD648
	public CWInput Current
	{
		get
		{
			return (this.buffer.Length <= 0) ? new CWInput() : this.buffer[0];
		}
	}

	// Token: 0x04001631 RID: 5681
	private ClassArray<CWInput> buffer = new ClassArray<CWInput>(10);
}
