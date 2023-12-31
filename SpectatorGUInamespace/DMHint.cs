using System;
using UnityEngine;

namespace SpectatorGUInamespace
{
	// Token: 0x02000190 RID: 400
	internal class DMHint : SpecHint
	{
		// Token: 0x06000B66 RID: 2918 RVA: 0x0008C414 File Offset: 0x0008A614
		public void OnGUI()
		{
			MainGUI.Instance.color = Colors.alpha(MainGUI.Instance.color, 1f);
			Rect rect = new Rect((float)(Screen.width - MainGUI.Instance.Width) * 0.5f, (float)(Screen.height - MainGUI.Instance.Height) * 0.5f, (float)MainGUI.Instance.Width, (float)MainGUI.Instance.Height);
			MainGUI.Instance.BeginGroup(rect, false);
			float num = MainGUI.Instance.CalcWidth(string.Concat(new string[]
			{
				Language.Push,
				" ",
				Language.Space,
				" ",
				Language.SpecForCycleCamChanged
			}), MainGUI.Instance.fontDNC57, 16);
			Rect rect2 = new Rect((rect.width - num) * 0.5f, 410f, rect.width, 50f);
			MainGUI.Instance.CompositeText(ref rect2, Language.Push, 16, Colors.greyDDDDDD, TextAnchor.UpperCenter, 1f);
			MainGUI.Instance.CompositeText(ref rect2, Language.Space, 16, Colors.RadarBlueWeb, TextAnchor.UpperCenter, 1f);
			MainGUI.Instance.CompositeText(ref rect2, Language.SpecForCycleCamChanged, 16, Colors.greyDDDDDD, TextAnchor.UpperCenter, 1f);
			if (CVars.g_allowDMSpectate)
			{
				num = MainGUI.Instance.CalcWidth(string.Concat(new string[]
				{
					Language.Push,
					" ",
					Language.MMB,
					" ",
					Language.ForCamChanged
				}), MainGUI.Instance.fontDNC57, 16);
				rect2.Set((rect.width - num) * 0.5f, 430f, rect.width, 50f);
				MainGUI.Instance.CompositeText(ref rect2, Language.Push, 16, Colors.greyDDDDDD, TextAnchor.UpperCenter, 1f);
				MainGUI.Instance.CompositeText(ref rect2, Language.MMB, 16, Colors.RadarBlueWeb, TextAnchor.UpperCenter, 1f);
				MainGUI.Instance.CompositeText(ref rect2, Language.ForCamChanged, 16, Colors.greyDDDDDD, TextAnchor.UpperCenter, 1f);
			}
			if (!Peer.ClientGame.IsFull || Peer.ClientGame.LocalPlayer.IsTeamChoosed)
			{
				num = MainGUI.Instance.CalcWidth(string.Concat(new string[]
				{
					Language.Push,
					" ",
					Language.LMB,
					" ",
					Language.SpecForBeginGame
				}), MainGUI.Instance.fontDNC57, 24);
				rect2.Set((rect.width - num) * 0.5f, (float)((!CVars.g_allowDMSpectate) ? 435 : 455), rect.width, 50f);
				MainGUI.Instance.CompositeText(ref rect2, Language.Push, 24, Colors.greyDDDDDD, TextAnchor.UpperCenter, 1f);
				MainGUI.Instance.CompositeText(ref rect2, Language.LMB, 24, Colors.RadarBlueWeb, TextAnchor.UpperCenter, 1f);
				MainGUI.Instance.CompositeText(ref rect2, Language.SpecForBeginGame, 24, Colors.greyDDDDDD, TextAnchor.UpperCenter, 1f);
			}
			else
			{
				num = MainGUI.Instance.CalcWidth(Language.SpecServerIsFull, MainGUI.Instance.fontDNC57, 16);
				rect2.Set((rect.width - num) * 0.5f, (float)((!CVars.g_allowDMSpectate) ? 430 : 450), rect.width, 50f);
				MainGUI.Instance.CompositeText(ref rect2, Language.SpecServerIsFull, 16, Colors.RadarRedWeb, TextAnchor.UpperCenter, 1f);
			}
			MainGUI.Instance.EndGroup();
			MainGUI.Instance.color = Colors.alpha(MainGUI.Instance.color, SpectactorGUI.I.visibility);
		}
	}
}
