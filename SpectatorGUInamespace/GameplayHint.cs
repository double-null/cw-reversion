using System;
using UnityEngine;

namespace SpectatorGUInamespace
{
	// Token: 0x02000196 RID: 406
	internal class GameplayHint : SpecHint
	{
		// Token: 0x06000B72 RID: 2930 RVA: 0x0008DC8C File Offset: 0x0008BE8C
		public void OnGUI()
		{
			if (Peer.ClientGame.MatchState == MatchState.match_result || Peer.ClientGame.MatchState == MatchState.round_ended || Peer.ClientGame.MatchState == MatchState.round_pre_ended)
			{
				return;
			}
			if (Main.UserInfo.currentLevel != 0)
			{
				SpectactorGUI.I.GameplayTutorShowed = true;
				return;
			}
			if (this.showed && !this.alpha.Visible)
			{
				SpectactorGUI.I.GameplayTutorShowed = true;
			}
			if (this.alpha.MaxVisible && (Event.current.type == EventType.KeyUp || Event.current.type == EventType.MouseUp))
			{
				this.showed = true;
				this.alpha.Hide(0.5f, 0f);
			}
			Color color = GUI.color;
			GUI.color = new Color(1f, 1f, 1f, (this.alpha.visibility >= 0.75f || !this.alpha.Hiding) ? 0.75f : this.alpha.visibility);
			GUI.DrawTexture(new Rect(0f, (float)(Screen.height - 250), (float)Screen.width, 250f), MainGUI.Instance.black);
			GUI.color = color;
			if (SpectactorGUI.I.ControlTutorShowed && !this.alpha.Hiding && !this.showed)
			{
				this.alpha.Show(1f, 0f);
			}
			if (this.alpha.visibility > 0f)
			{
				GUI.color = Colors.alpha(GUI.color, this.alpha.visibility);
				SpectactorGUI.I.WhiteLabelStyle.alignment = TextAnchor.MiddleCenter;
				GUI.Label(new Rect(0f, (float)(Screen.height - 250), (float)Screen.width, 30f), Language.TutorInGameGameplayHeader, SpectactorGUI.I.BlueLabelStyle);
				GUI.Label(new Rect(0f - MainGUI.Instance.CalcWidth(Language.TutorInGameGameplayHint1_2, MainGUI.Instance.fontDNC57, 18), (float)(Screen.height - 200), (float)Screen.width, 30f), Language.TutorInGameGameplayHint1_1, SpectactorGUI.I.WhiteLabelStyle);
				GUI.Label(new Rect(0f, (float)(Screen.height - 165), (float)Screen.width, 30f), Language.TutorInGameGameplayHint2, SpectactorGUI.I.WhiteLabelStyle);
				GUI.Label(new Rect(0f, (float)(Screen.height - 130), (float)Screen.width, 30f), Language.TutorInGameGameplayHint3, SpectactorGUI.I.WhiteLabelStyle);
				GUI.Label(new Rect(0f - MainGUI.Instance.CalcWidth(Language.And, MainGUI.Instance.fontDNC57, 18), (float)(Screen.height - 95), (float)Screen.width, 30f), Language.TutorInGameGameplayHint4, SpectactorGUI.I.WhiteLabelStyle);
				GUI.Label(new Rect(0f, (float)(Screen.height - 30), (float)Screen.width, 30f), Language.TutorInGameContinue, SpectactorGUI.I.GrayLabelStyle);
				GUI.Label(new Rect(((float)Screen.width + MainGUI.Instance.CalcWidth(Language.TutorInGameGameplayHint4, MainGUI.Instance.fontDNC57, 18)) / 2f, (float)(Screen.height - 95), 36f, 30f), Language.And, SpectactorGUI.I.WhiteLabelStyle);
				SpectactorGUI.I.WhiteLabelStyle.alignment = TextAnchor.MiddleLeft;
				GUI.Label(new Rect(((float)Screen.width + MainGUI.Instance.CalcWidth(Language.TutorInGameGameplayHint1_1, MainGUI.Instance.fontDNC57, 18)) / 2f - 15f, (float)(Screen.height - 200), 100f, 30f), Language.TutorInGameGameplayHint1_2, SpectactorGUI.I.WhiteLabelStyle);
				GUI.DrawTexture(new Rect(((float)Screen.width + MainGUI.Instance.CalcWidth(Language.TutorInGameGameplayHint1_1, MainGUI.Instance.fontDNC57, 18)) / 2f - MainGUI.Instance.CalcWidth(Language.TutorInGameGameplayHint1_2, MainGUI.Instance.fontDNC57, 18), (float)(Screen.height - 200), (float)SpectactorGUI.I.ExpIcon.width, (float)SpectactorGUI.I.ExpIcon.height), SpectactorGUI.I.ExpIcon);
				GUI.DrawTexture(new Rect(((float)Screen.width + MainGUI.Instance.CalcWidth(Language.TutorInGameGameplayHint1_1 + Language.TutorInGameGameplayHint1_2, MainGUI.Instance.fontDNC57, 18) + (float)SpectactorGUI.I.ExpIcon.width) / 2f, (float)(Screen.height - 200), (float)MainGUI.Instance.crIcon.width, (float)MainGUI.Instance.crIcon.height), MainGUI.Instance.crIcon);
				GUI.DrawTexture(new Rect(((float)Screen.width + MainGUI.Instance.CalcWidth(Language.TutorInGameGameplayHint2, MainGUI.Instance.fontDNC57, 18)) / 2f, (float)(Screen.height - 165), (float)MainGUI.Instance.spIcon_med.width, (float)MainGUI.Instance.spIcon_med.height), MainGUI.Instance.spIcon_med);
				GUI.DrawTexture(new Rect(((float)Screen.width + MainGUI.Instance.CalcWidth(Language.TutorInGameGameplayHint4, MainGUI.Instance.fontDNC57, 18)) / 2f - MainGUI.Instance.CalcWidth(Language.And, MainGUI.Instance.fontDNC57, 18), (float)(Screen.height - 95), (float)MainGUI.Instance.crIcon.width, (float)MainGUI.Instance.crIcon.height), MainGUI.Instance.crIcon);
				GUI.DrawTexture(new Rect(((float)Screen.width + MainGUI.Instance.CalcWidth(Language.TutorInGameGameplayHint4 + Language.And, MainGUI.Instance.fontDNC57, 18)) / 2f + (float)MainGUI.Instance.crIcon.width, (float)(Screen.height - 95), (float)MainGUI.Instance.spIcon_med.width, (float)MainGUI.Instance.spIcon_med.height), MainGUI.Instance.spIcon_med);
			}
		}

		// Token: 0x04000D4D RID: 3405
		private bool showed;

		// Token: 0x04000D4E RID: 3406
		public Alpha alpha = new Alpha();
	}
}
