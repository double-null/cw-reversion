using System;
using System.Collections.Generic;

namespace Samples
{
	// Token: 0x02000177 RID: 375
	internal class SpawnEngine
	{
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x00080B68 File Offset: 0x0007ED68
		public int SelectedID
		{
			get
			{
				if (this.selectedItem != null)
				{
					return this.selectedItem.GetID();
				}
				return -1;
			}
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00080B84 File Offset: 0x0007ED84
		public void Add(item it)
		{
			if (this.list.Contains(it))
			{
				return;
			}
			it.Set(this);
			this.list.Add(it);
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x00080BAC File Offset: 0x0007EDAC
		public void Clear()
		{
			this.selectedItem = null;
			this.list.Clear();
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00080BC0 File Offset: 0x0007EDC0
		public void DeleteByID(int ID)
		{
			for (int i = 0; i < this.list.Count; i++)
			{
				if (this.list[i].GetID() == ID)
				{
					this.list.RemoveAt(i);
				}
			}
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x00080C0C File Offset: 0x0007EE0C
		public void ClickByID(int ID)
		{
			for (int i = 0; i < this.list.Count; i++)
			{
				if (this.list[i].GetID() == ID)
				{
					this.list[i].OnClick();
				}
			}
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x00080C60 File Offset: 0x0007EE60
		public void OnClick(item it)
		{
			this.selectedItem = it;
			for (int i = 0; i < this.list.Count; i++)
			{
				if (it != this.list[i])
				{
					this.list[i].Reset();
				}
			}
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00080CB4 File Offset: 0x0007EEB4
		public void OnGUI()
		{
			for (int i = 0; i < this.list.Count; i++)
			{
				this.list[i].OnGUI();
			}
		}

		// Token: 0x04000C7A RID: 3194
		private item selectedItem;

		// Token: 0x04000C7B RID: 3195
		private List<item> list = new List<item>();
	}
}
