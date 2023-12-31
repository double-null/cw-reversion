using System;
using UnityEngine;

namespace SpectatorGUInamespace
{
	// Token: 0x0200018F RID: 399
	internal class WaitRessurect : SpecHint
	{
		// Token: 0x06000B64 RID: 2916 RVA: 0x0008C2CC File Offset: 0x0008A4CC
		public void OnGUI()
		{
			if (SpectactorGUI.I.fSpawnTimer - HtmlLayer.serverUtc <= 0)
			{
				return;
			}
			Rect rect = new Rect((float)(Screen.width - MainGUI.Instance.Width) * 0.5f, (float)(Screen.height - MainGUI.Instance.Height) * 0.5f, (float)MainGUI.Instance.Width, (float)MainGUI.Instance.Height);
			MainGUI.Instance.color = Colors.alpha(Color.white, 1f);
			MainGUI.Instance.BeginGroup(rect, false);
			float num = MainGUI.Instance.CalcWidth(Language.SpecRessurectAfter + "00", MainGUI.Instance.fontDNC57, 16);
			Rect rect2 = new Rect((rect.width - num) * 0.5f, 450f, rect.width, 50f);
			MainGUI.Instance.CompositeText(ref rect2, Language.SpecRessurectAfter, 16, Colors.greyDDDDDD, TextAnchor.UpperCenter, 1f);
			MainGUI.Instance.CompositeText(ref rect2, (SpectactorGUI.I.fSpawnTimer - HtmlLayer.serverUtc).ToString("D2"), 16, Colors.RadarBlueWeb, TextAnchor.UpperCenter, 1f);
			MainGUI.Instance.EndGroup();
		}
	}
}
