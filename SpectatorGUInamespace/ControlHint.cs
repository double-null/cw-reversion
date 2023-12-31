using System;
using UnityEngine;

namespace SpectatorGUInamespace
{
	// Token: 0x02000195 RID: 405
	internal class ControlHint : SpecHint
	{
		// Token: 0x06000B70 RID: 2928 RVA: 0x0008D408 File Offset: 0x0008B608
		public void OnGUI()
		{
			if (Peer.ClientGame.MatchState == MatchState.match_result || Peer.ClientGame.MatchState == MatchState.round_ended || Peer.ClientGame.MatchState == MatchState.round_pre_ended)
			{
				return;
			}
			if (Main.UserInfo.currentLevel != 0)
			{
				SpectactorGUI.I.ControlTutorShowed = true;
				return;
			}
			this.rect = new Rect((float)(Screen.width / 2 - 400), (float)(Screen.height - 300), 800f, 300f);
			if (this.alpha.MaxVisible && (Event.current.type == EventType.KeyUp || Event.current.type == EventType.MouseUp))
			{
				SpectactorGUI.I.ControlTutorShowed = true;
			}
			Color color = GUI.color;
			GUI.color = new Color(1f, 1f, 1f, (this.alpha.visibility >= 0.75f) ? 0.75f : this.alpha.visibility);
			GUI.DrawTexture(new Rect(0f, (float)(Screen.height - 250), (float)Screen.width, 250f), MainGUI.Instance.black);
			GUI.color = color;
			if (!LoadingGUI.I.Visible)
			{
				this.alpha.Show(1f, 0f);
			}
			if (this.alpha.visibility > 0f)
			{
				GUI.color = Colors.alpha(GUI.color, this.alpha.visibility);
				GUI.Label(new Rect(0f, (float)(Screen.height - 250), (float)Screen.width, 30f), Language.TutorInGameControlHeader, SpectactorGUI.I.BlueLabelStyle);
				GUI.Label(new Rect(0f, (float)(Screen.height - 30), (float)Screen.width, 30f), Language.TutorInGameContinue, SpectactorGUI.I.GrayLabelStyle);
				GUI.BeginGroup(this.rect);
				SpectactorGUI.I.WhiteLabelStyle.alignment = TextAnchor.MiddleCenter;
				GUI.DrawTexture(new Rect(600f, 90f, (float)SpectactorGUI.I.MBLeft.width, (float)SpectactorGUI.I.MBLeft.height), SpectactorGUI.I.MBLeft);
				GUI.DrawTexture(new Rect(660f, 90f, (float)SpectactorGUI.I.MBRight.width, (float)SpectactorGUI.I.MBRight.height), SpectactorGUI.I.MBRight);
				GUI.DrawTexture(new Rect(720f, 87f, (float)SpectactorGUI.I.MBMiddle.width, (float)SpectactorGUI.I.MBMiddle.height), SpectactorGUI.I.MBMiddle);
				GUI.Label(new Rect(593f, 145f, 50f, 30f), Language.TutorInGameFire, SpectactorGUI.I.WhiteLabelStyle);
				GUI.Label(new Rect(653f, 145f, 50f, 30f), Language.TutorInGameAim, SpectactorGUI.I.WhiteLabelStyle);
				GUI.Label(new Rect(713f, 145f, 50f, 30f), Language.TutorInGameWeaponChange, SpectactorGUI.I.WhiteLabelStyle);
				GUI.Label(new Rect(227f, 155f, 100f, 30f), Language.TutorInGameReload, SpectactorGUI.I.WhiteLabelStyle);
				GUI.Label(new Rect(132f, 182f, 100f, 30f), Language.TutorInGameMovement, SpectactorGUI.I.WhiteLabelStyle);
				GUI.Label(new Rect(55f, 223f, 50f, 30f), Language.TutorInGameWalk, SpectactorGUI.I.WhiteLabelStyle);
				GUI.Label(new Rect(225f, 223f, 100f, 30f), Language.TutorInGameCrouch, SpectactorGUI.I.WhiteLabelStyle);
				GUI.Label(new Rect(332f, 223f, 50f, 30f), Language.TutorInGameKnife, SpectactorGUI.I.WhiteLabelStyle);
				GUI.Label(new Rect(417f, 223f, 50f, 30f), Language.TutorInGameJump, SpectactorGUI.I.WhiteLabelStyle);
				SpectactorGUI.I.WhiteLabelStyle.alignment = TextAnchor.MiddleLeft;
				GUI.Label(new Rect(120f, 90f, 200f, 30f), Language.TutorInGameWeaponChange, SpectactorGUI.I.WhiteLabelStyle);
				GUI.Label(new Rect(300f, 90f, 200f, 30f), Language.TutorInGameMenu, SpectactorGUI.I.WhiteLabelStyle);
				GUI.Label(new Rect(450f, 90f, 200f, 30f), Language.TutorInGameFSmode, SpectactorGUI.I.WhiteLabelStyle);
				GUI.Button(new Rect(50f, 90f, (float)this.smallBtnWidth, (float)this.smallBtnHeight), "1", SpectactorGUI.I.SmallButton);
				GUI.Button(new Rect(80f, 90f, (float)this.smallBtnWidth, (float)this.smallBtnHeight), "2", SpectactorGUI.I.SmallButton);
				GUI.Button(new Rect(260f, 90f, (float)this.smallBtnWidth, (float)this.smallBtnHeight), "F10", SpectactorGUI.I.SmallButton);
				GUI.Button(new Rect(410f, 90f, (float)this.smallBtnWidth, (float)this.smallBtnHeight), "F12", SpectactorGUI.I.SmallButton);
				GUI.Button(new Rect(165f, 132f, (float)this.smallBtnWidth, (float)this.smallBtnHeight), Main.UserInfo.settings.binds.up.ToString(), SpectactorGUI.I.SmallButton);
				GUI.Button(new Rect(135f, 160f, (float)this.smallBtnWidth, (float)this.smallBtnHeight), Main.UserInfo.settings.binds.left.ToString(), SpectactorGUI.I.SmallButton);
				GUI.Button(new Rect(165f, 160f, (float)this.smallBtnWidth, (float)this.smallBtnHeight), Main.UserInfo.settings.binds.down.ToString(), SpectactorGUI.I.SmallButton);
				GUI.Button(new Rect(195f, 160f, (float)this.smallBtnWidth, (float)this.smallBtnHeight), Main.UserInfo.settings.binds.right.ToString(), SpectactorGUI.I.SmallButton);
				GUI.Button(new Rect(260f, 132f, (float)this.smallBtnWidth, (float)this.smallBtnHeight), Main.UserInfo.settings.binds.reload.ToString(), SpectactorGUI.I.SmallButton);
				GUI.Button(new Rect(260f, 200f, (float)this.smallBtnWidth, (float)this.smallBtnHeight), Main.UserInfo.settings.binds.sit.ToString(), SpectactorGUI.I.SmallButton);
				GUI.Button(new Rect(340f, 200f, (float)this.smallBtnWidth, (float)this.smallBtnHeight), Main.UserInfo.settings.binds.knife.ToString(), SpectactorGUI.I.SmallButton);
				GUI.Button(new Rect(50f, 200f, (float)this.bigBtnWidth, (float)this.smallBtnHeight), Main.UserInfo.settings.binds.walk.ToString(), SpectactorGUI.I.BigButton);
				GUI.Button(new Rect(410f, 200f, (float)this.bigBtnWidth, (float)this.smallBtnHeight), Main.UserInfo.settings.binds.jump.ToString(), SpectactorGUI.I.BigButton);
				GUI.EndGroup();
			}
		}

		// Token: 0x04000D48 RID: 3400
		public Alpha alpha = new Alpha();

		// Token: 0x04000D49 RID: 3401
		private Rect rect = default(Rect);

		// Token: 0x04000D4A RID: 3402
		private int bigBtnWidth = 64;

		// Token: 0x04000D4B RID: 3403
		private int smallBtnWidth = 34;

		// Token: 0x04000D4C RID: 3404
		private int smallBtnHeight = 33;
	}
}
