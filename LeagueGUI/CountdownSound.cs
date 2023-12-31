using System;

namespace LeagueGUI
{
	// Token: 0x0200032E RID: 814
	internal class CountdownSound
	{
		// Token: 0x06001B7A RID: 7034 RVA: 0x000F7A4C File Offset: 0x000F5C4C
		public void Play(int time)
		{
			if (time > 10 || time < 0)
			{
				return;
			}
			if (this._previousTime != time)
			{
				Audio.Play(LeagueWindow.I.TimerClip);
				this._previousTime = time;
			}
		}

		// Token: 0x0400204A RID: 8266
		private int _previousTime;
	}
}
