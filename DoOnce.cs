using System;

// Token: 0x0200037B RID: 891
internal class DoOnce
{
	// Token: 0x06001CDE RID: 7390 RVA: 0x000FF384 File Offset: 0x000FD584
	public bool Do()
	{
		if (this._do)
		{
			this._do = false;
			return true;
		}
		return false;
	}

	// Token: 0x06001CDF RID: 7391 RVA: 0x000FF39C File Offset: 0x000FD59C
	public void Clear()
	{
		this._do = true;
	}

	// Token: 0x04002198 RID: 8600
	private bool _do = true;
}
