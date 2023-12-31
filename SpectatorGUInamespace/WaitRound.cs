using System;
using UnityEngine;

namespace SpectatorGUInamespace
{
	// Token: 0x0200018E RID: 398
	internal class WaitRound : SpecHint
	{
		// Token: 0x06000B62 RID: 2914 RVA: 0x0008C1D0 File Offset: 0x0008A3D0
		public void OnGUI()
		{
			if (SpectactorGUI.I.bfirstGame)
			{
				Rect rect = new Rect((float)Screen.width * 0.5f - 145f, (float)Screen.height * 0.5f, (float)MainGUI.Instance.Width, (float)MainGUI.Instance.Height);
				this.lastVisibility = MainGUI.Instance.color.a;
				MainGUI.Instance.color = Colors.alpha(MainGUI.Instance.color, 1f);
				MainGUI.Instance.CompositeText(ref rect, Language.SpecWaitForBeginRound, 16, "#ffffff", TextAnchor.UpperCenter, 1f);
				MainGUI.Instance.CompositeText(ref rect, MainGUI.Instance.SecondsToStringMS((int)Peer.ClientGame.ElapsedNextEventTime), 16, Colors.RadarRedWeb, TextAnchor.UpperCenter, 1f);
				MainGUI.Instance.color = Colors.alpha(Color.white, this.lastVisibility);
			}
		}

		// Token: 0x04000D47 RID: 3399
		private float lastVisibility;
	}
}
