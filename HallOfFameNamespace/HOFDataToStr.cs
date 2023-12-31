using System;

namespace HallOfFameNamespace
{
	// Token: 0x02000152 RID: 338
	internal class HOFDataToStr
	{
		// Token: 0x0600084C RID: 2124 RVA: 0x0004A550 File Offset: 0x00048750
		public static string GetData(int m)
		{
			m--;
			if (m < HOFDataToStr.month.Length && m > 0)
			{
				return HOFDataToStr.month[m];
			}
			return string.Empty;
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0004A584 File Offset: 0x00048784
		public static string GetData(int m, int y)
		{
			m--;
			if (m < 0)
			{
				m = m % 12 * -1;
				y -= m / 12;
			}
			if (m > 12)
			{
				m %= 12;
				y += m / 12;
			}
			return HOFDataToStr.month[m] + " " + y.ToString();
		}

		// Token: 0x04000969 RID: 2409
		private static string[] month = new string[]
		{
			"Январь",
			"Февраль",
			"Март",
			"Апрель",
			"Май",
			"Июнь",
			"Июль",
			"Август",
			"Сентябрь",
			"Октябрь",
			"Ноябрь",
			"Декабрь"
		};
	}
}
