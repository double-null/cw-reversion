using System;
using System.Collections.Generic;
using UnityEngine;

namespace CWGUInamespace.Tutor
{
	// Token: 0x020000E1 RID: 225
	[Serializable]
	internal class TutorContainer
	{
		// Token: 0x060005F0 RID: 1520 RVA: 0x0002DCB4 File Offset: 0x0002BEB4
		public void OnGUI(Rect borderRect, TutorContainer.HintPosition position)
		{
			if (this.enabled || this.alpha.visibility > 0f)
			{
				if (this.animate == null)
				{
					this.animate = new Animate(this.arrow);
					this.animate.fadePeriod = 0.4f;
				}
				GUI.color = Colors.alpha(GUI.color, this.alpha.visibility);
				this.border.OnGUI(borderRect);
				if (this.timer.Disabled && this.SelfClosing && !this.tutorialAborted)
				{
					this.timer.Start();
				}
				if (this.tutorialAborted)
				{
					this.timer.Stop();
				}
				if ((borderRect.Contains(Event.current.mousePosition) && Input.GetMouseButtonDown(0)) || this.timer.Elapsed > 6f)
				{
					this.timer.Stop();
					this.Hide();
				}
				if (position == TutorContainer.HintPosition.middleRight)
				{
					if (this.animate != null)
					{
						this.animate.OnGUI(borderRect.xMax - 10f, borderRect.y - borderRect.height / 2f);
					}
					this.content.OnGUI(borderRect.xMax + this.arrow.Rect.width + 2f, borderRect.y + borderRect.height + 3f);
				}
				else if (position == TutorContainer.HintPosition.bottomRight)
				{
					CWGUI.RotateGUI(90f, new Vector2(borderRect.x + (float)(this.arrow.arrow.width / 2), borderRect.y + (float)(this.arrow.arrow.height / 2)));
					if (this.animate != null)
					{
						this.animate.OnGUI(borderRect.x + borderRect.height - 15f, borderRect.y - borderRect.width + 20f);
					}
					CWGUI.RotateGUI(0f, Vector2.zero);
					this.content.OnGUI(borderRect.xMax - this.arrow.Rect.width, borderRect.y + borderRect.height + 30f);
				}
				else if (position == TutorContainer.HintPosition.bottomLeft)
				{
					CWGUI.RotateGUI(90f, new Vector2(borderRect.x + (float)(this.arrow.arrow.width / 2), borderRect.y + (float)(this.arrow.arrow.height / 2)));
					if (this.animate != null)
					{
						this.animate.OnGUI(borderRect.x + borderRect.height - 15f, borderRect.y - 10f);
					}
					CWGUI.RotateGUI(0f, Vector2.zero);
					this.content.OnGUI(borderRect.x, borderRect.y + borderRect.height + 30f);
				}
				else if (position == TutorContainer.HintPosition.upperLeft)
				{
					CWGUI.RotateGUI(270f, new Vector2(borderRect.x + (float)(this.arrow.arrow.width / 2), borderRect.y + (float)(this.arrow.arrow.height / 2)));
					if (this.animate != null)
					{
						this.animate.OnGUI(borderRect.x + 30f, borderRect.y + 10f);
					}
					CWGUI.RotateGUI(0f, Vector2.zero);
					this.content.OnGUI(borderRect.x, borderRect.y - borderRect.height);
				}
				else if (position == TutorContainer.HintPosition.doubleHint)
				{
					CWGUI.RotateGUI(90f, new Vector2(borderRect.x + (float)(this.arrow.arrow.width / 2), borderRect.y + (float)(this.arrow.arrow.height / 2)));
					if (this.animate != null)
					{
						this.animate.OnGUI(borderRect.x - 18f, borderRect.y + 220f);
					}
					CWGUI.RotateGUI(0f, Vector2.zero);
					CWGUI.RotateGUI(180f, new Vector2(borderRect.x + (float)(this.arrow.arrow.width / 2), borderRect.y + (float)(this.arrow.arrow.height / 2)));
					if (this.animate != null)
					{
						this.animate.OnGUI(borderRect.x + 20f, borderRect.y - 25f);
					}
					CWGUI.RotateGUI(0f, Vector2.zero);
					this.content.OnGUI(borderRect.x - 250f, borderRect.y + borderRect.height - 40f);
				}
				if (this.tutorialAborted)
				{
					this.button.OnGUI(borderRect.xMax + 250f, borderRect.yMax);
					if (this.alpha.Hiding)
					{
						MainGUI.Instance.tutorialComplete = true;
						this.tutorialAborted = false;
					}
				}
			}
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0002E218 File Offset: 0x0002C418
		public void Show(string Hint)
		{
			this.enabled = true;
			if (!this.alpha.Showing)
			{
				this.alpha.Show(1f, 0f);
			}
			this.content.content.text = Hint;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0002E258 File Offset: 0x0002C458
		public void Hide()
		{
			this.HintCounter += 1;
			this.enabled = false;
			if (!this.alpha.Hiding)
			{
				this.alpha.Hide(0.25f, 0f);
			}
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0002E298 File Offset: 0x0002C498
		public void OnStart()
		{
			this.button.action = delegate()
			{
				this.Hide();
			};
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0002E2B4 File Offset: 0x0002C4B4
		public void InitHintList()
		{
			this.Hints.Add(Language.TutorNickname);
			this.Hints.Add(Language.TutorExpBar);
			this.Hints.Add(Language.TutorContracts);
			this.Hints.Add(Language.TutorBalance);
			this.Hints.Add(Language.TutorFullScreen);
			this.Hints.Add(Language.TutorBuyWeapon);
			this.Hints.Add(Language.TutorConfirmPayment);
			this.Hints.Add(Language.TutorEquipPrimary);
			this.Hints.Add(Language.TutorInstallWtask);
			this.Hints.Add(Language.TutorEquipSecondary);
			this.Hints.Add(Language.TutorSaveWeaponKit);
			this.Hints.Add(Language.TutorSelectedKit);
			this.Hints.Add(Language.TutorQuickMatchOpen);
			this.Hints.Add(Language.TutorQuickMatchGo);
			this.Hints.Add(Language.TutorHintFitstTime);
			this.BorderPosition.Add(new Rect(7f, 207f, 150f, 46f));
			this.BorderPosition.Add(new Rect(6f, 256f, 195f, 29f));
			this.BorderPosition.Add(new Rect(4f, 282f, 200f, 54f));
			this.BorderPosition.Add(new Rect(2f, 350f, 200f, 65f));
			this.BorderPosition.Add(new Rect(7f, 10f, 39f, 32f));
			this.BorderPosition.Add(new Rect(306f, 52f, 234f, 70f));
			this.BorderPosition.Add(new Rect(409f, 349f, 84f, 26f));
			this.BorderPosition.Add(new Rect(540f, 124f, 234f, 70f));
			this.BorderPosition.Add(new Rect(216f, 93f, 25f, 25f));
			this.BorderPosition.Add(new Rect(213f, 55f, 90f, 65f));
			this.BorderPosition.Add(new Rect(7f, 444f, 31f, 22f));
			this.BorderPosition.Add(new Rect(212f, 525f, 564f, 75f));
			this.BorderPosition.Add(new Rect(4f, 52f, 192f, 23f));
			this.BorderPosition.Add(new Rect(462f, 295f, 107f, 42f));
			this.BorderPosition.Add(new Rect(4f, 148f, (float)MainGUI.Instance.mainMenuButtons[0].width, (float)MainGUI.Instance.mainMenuButtons[0].height));
			this.Position.Add(TutorContainer.HintPosition.bottomRight);
			this.Position.Add(TutorContainer.HintPosition.bottomRight);
			this.Position.Add(TutorContainer.HintPosition.bottomRight);
			this.Position.Add(TutorContainer.HintPosition.bottomRight);
			this.Position.Add(TutorContainer.HintPosition.bottomRight);
			this.Position.Add(TutorContainer.HintPosition.bottomLeft);
			this.Position.Add(TutorContainer.HintPosition.bottomLeft);
			this.Position.Add(TutorContainer.HintPosition.doubleHint);
			this.Position.Add(TutorContainer.HintPosition.bottomLeft);
			this.Position.Add(TutorContainer.HintPosition.bottomLeft);
			this.Position.Add(TutorContainer.HintPosition.bottomLeft);
			this.Position.Add(TutorContainer.HintPosition.upperLeft);
			this.Position.Add(TutorContainer.HintPosition.middleRight);
			this.Position.Add(TutorContainer.HintPosition.bottomRight);
			this.Position.Add(TutorContainer.HintPosition.middleRight);
		}

		// Token: 0x04000625 RID: 1573
		private bool enabled = true;

		// Token: 0x04000626 RID: 1574
		private Alpha alpha = new Alpha();

		// Token: 0x04000627 RID: 1575
		public bool tutorialAborted;

		// Token: 0x04000628 RID: 1576
		public bool tutorialComplete;

		// Token: 0x04000629 RID: 1577
		public bool SelfClosing = true;

		// Token: 0x0400062A RID: 1578
		public byte HintCounter;

		// Token: 0x0400062B RID: 1579
		public Rectangle border = new Rectangle();

		// Token: 0x0400062C RID: 1580
		public Arrow arrow = new Arrow();

		// Token: 0x0400062D RID: 1581
		public Text content = new Text();

		// Token: 0x0400062E RID: 1582
		public Animate animate;

		// Token: 0x0400062F RID: 1583
		public Button button = new Button();

		// Token: 0x04000630 RID: 1584
		public List<string> Hints = new List<string>();

		// Token: 0x04000631 RID: 1585
		public List<Rect> BorderPosition = new List<Rect>();

		// Token: 0x04000632 RID: 1586
		public List<TutorContainer.HintPosition> Position = new List<TutorContainer.HintPosition>();

		// Token: 0x04000633 RID: 1587
		private eTimer timer = new eTimer();

		// Token: 0x020000E2 RID: 226
		public enum HintPosition
		{
			// Token: 0x04000635 RID: 1589
			doubleHint,
			// Token: 0x04000636 RID: 1590
			emptyHint,
			// Token: 0x04000637 RID: 1591
			bottomRight,
			// Token: 0x04000638 RID: 1592
			bottomLeft,
			// Token: 0x04000639 RID: 1593
			upperLeft,
			// Token: 0x0400063A RID: 1594
			middleRight
		}
	}
}
