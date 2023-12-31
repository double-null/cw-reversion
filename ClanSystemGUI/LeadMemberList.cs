using System;
using System.Collections.Generic;

namespace ClanSystemGUI
{
	// Token: 0x02000124 RID: 292
	internal class LeadMemberList : ScrollList
	{
		// Token: 0x060007D1 RID: 2001 RVA: 0x00047ED4 File Offset: 0x000460D4
		public void AddRequests(IScrollListItem item)
		{
			if (this.ItemList.Count < 1)
			{
				this.requests.Add(item);
				this.Add(this.UpperHeader);
				this.Add(item);
				this.Add(this.LowerHeader);
			}
			else
			{
				this.requests.Add(item);
				this.ItemList.Insert(1, item);
			}
			this.RefreshTitles();
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00047F44 File Offset: 0x00046144
		public void RefreshTitles()
		{
			((TitleBar)this.UpperHeader).SetStr(Language.ClansManagmentRequest + "(" + this.requests.Count.ToString() + ")");
			((TitleBar)this.LowerHeader).SetStr(Language.ClansManagmentCurrent + "(" + (this.ItemList.Count - this.requests.Count - ((this.requests.Count <= 0) ? 0 : 2)).ToString() + ")");
			this.DeltaIndex = -((this.requests.Count <= 1) ? 0 : (this.requests.Count + 2));
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00048010 File Offset: 0x00046210
		public void MoveToApproved(IScrollListItem item)
		{
			this.RemoveRquest(item);
			this.Add(new CurrentInfoBar(this, ((RequestInfoBar)item).RequestInfo));
			this.RefreshTitles();
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00048044 File Offset: 0x00046244
		public void RemoveRquest(IScrollListItem item)
		{
			this.requests.Remove(item);
			this.Remove(item);
			if (this.requests.Count == 0)
			{
				this.scrollPosMin = 0f;
				this.ScrollPosition.y = 0f;
				this.Remove(this.UpperHeader);
				this.Remove(this.LowerHeader);
			}
			this.RefreshTitles();
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x000480B0 File Offset: 0x000462B0
		public void ClearRequests()
		{
			for (int i = 0; i < this.requests.Count; i++)
			{
				this.Remove(this.requests[i]);
			}
			this.requests.Clear();
			this.scrollPosMin = 0f;
			this.ScrollPosition.y = 0f;
			this.Remove(this.UpperHeader);
			this.Remove(this.LowerHeader);
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0004812C File Offset: 0x0004632C
		public override void Clear()
		{
			this.requests.Clear();
			base.Clear();
			this.RefreshTitles();
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00048148 File Offset: 0x00046348
		public override void OnGUI()
		{
			base.OnGUI();
		}

		// Token: 0x04000842 RID: 2114
		private IScrollListItem UpperHeader = new TitleBar(Language.ClansManagmentRequest + " (1)");

		// Token: 0x04000843 RID: 2115
		private IScrollListItem LowerHeader = new TitleBar(Language.ClansManagmentCurrent + " (0)");

		// Token: 0x04000844 RID: 2116
		protected List<IScrollListItem> requests = new List<IScrollListItem>();
	}
}
