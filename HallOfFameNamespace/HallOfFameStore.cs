using System;
using System.Collections.Generic;

namespace HallOfFameNamespace
{
	// Token: 0x02000151 RID: 337
	internal class HallOfFameStore
	{
		// Token: 0x06000847 RID: 2119 RVA: 0x0004A41C File Offset: 0x0004861C
		public void Add(List<HallOfFameUnit> units, int y, int m)
		{
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0004A420 File Offset: 0x00048620
		public List<HallOfFameUnit> GetStore(int y, int m)
		{
			for (int i = 0; i < this.year.Count; i++)
			{
				if (this.year[i] == y && this.month[i] == m)
				{
					return this.storage[i];
				}
			}
			return null;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0004A47C File Offset: 0x0004867C
		public bool InStorage(int y, int m)
		{
			for (int i = 0; i < this.year.Count; i++)
			{
				if (this.year[i] == y && this.month[i] == m)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000966 RID: 2406
		private List<List<HallOfFameUnit>> storage = new List<List<HallOfFameUnit>>();

		// Token: 0x04000967 RID: 2407
		private List<int> year = new List<int>();

		// Token: 0x04000968 RID: 2408
		private List<int> month = new List<int>();
	}
}
