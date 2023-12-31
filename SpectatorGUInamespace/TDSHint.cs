using System;
using UnityEngine;

namespace SpectatorGUInamespace
{
	// Token: 0x02000193 RID: 403
	internal class TDSHint : SpecHint
	{
		// Token: 0x06000B6C RID: 2924 RVA: 0x0008CFCC File Offset: 0x0008B1CC
		public void OnGUI()
		{
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
			if (!ClientLeagueSystem.IsLeagueGame)
			{
				if ((Peer.ClientGame.ElapsedNextEventTime > Main.GameModeInfo.matchRoundTime / 4f && SpectactorGUI.I.changeTeamCount > 0) || Peer.ClientGame.MatchState == MatchState.alone)
				{
					if (!Peer.ClientGame.IsFull)
					{
						num = MainGUI.Instance.CalcWidth(Language.Push + " M " + Language.SpecForChooseTeam, MainGUI.Instance.fontDNC57, 16);
						rect2.Set((rect.width - num) * 0.5f, 450f, rect.width, 50f);
						MainGUI.Instance.CompositeText(ref rect2, Language.Push, 16, Colors.greyDDDDDD, TextAnchor.UpperCenter, 1f);
						MainGUI.Instance.CompositeText(ref rect2, "M", 16, Colors.RadarBlueWeb, TextAnchor.UpperCenter, 1f);
						MainGUI.Instance.CompositeText(ref rect2, Language.SpecForChooseTeam, 16, Colors.greyDDDDDD, TextAnchor.UpperCenter, 1f);
					}
					else if (!Peer.ClientGame.LocalPlayer.IsTeamChoosed)
					{
						num = MainGUI.Instance.CalcWidth(Language.SpecServerIsFull, MainGUI.Instance.fontDNC57, 16);
						rect2.Set((rect.width - num) * 0.5f, 450f, rect.width, 50f);
						MainGUI.Instance.CompositeText(ref rect2, Language.SpecServerIsFull, 16, Colors.RadarRedWeb, TextAnchor.UpperCenter, 1f);
					}
				}
				else
				{
					num = MainGUI.Instance.CalcWidth(Language.SpecCantChangeTeam, MainGUI.Instance.fontDNC57, 16);
					rect2.Set((rect.width - num) * 0.5f, 450f, rect.width, 50f);
					MainGUI.Instance.CompositeText(ref rect2, Language.SpecCantChangeTeam, 16, Colors.RadarRedWeb, TextAnchor.UpperCenter, 1f);
				}
			}
			MainGUI.Instance.EndGroup();
		}
	}
}
