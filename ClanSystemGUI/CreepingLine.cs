using System;
using UnityEngine;

namespace ClanSystemGUI
{
	// Token: 0x02000126 RID: 294
	internal class CreepingLine
	{
		// Token: 0x060007DC RID: 2012 RVA: 0x0004818C File Offset: 0x0004638C
		public void OnGUI(Rect areaRect, bool editable = false)
		{
			this._tempColor = GUI.color;
			if (string.IsNullOrEmpty(this._message) && !string.IsNullOrEmpty(Main.UserInfo.ClanMessage))
			{
				this._message = Main.UserInfo.ClanMessage;
			}
			if (!this.calculateWidth)
			{
				if (!string.IsNullOrEmpty(this._message))
				{
					this._messageLength = MainGUI.Instance.CalcWidth(this._message, MainGUI.Instance.fontDNC57, 20);
				}
				this.calculateWidth = true;
			}
			this._messageEndPosition = this._messageStartPosition + this._messageLength;
			if (this._messageEndPosition < areaRect.x)
			{
				this._messageStartPosition = areaRect.width;
			}
			if (this._messageLength > areaRect.width)
			{
				this._messageStartPosition -= 100f * Time.deltaTime;
			}
			if (editable && areaRect.Contains(Event.current.mousePosition))
			{
				GUI.color = new Color(1f, 1f, 1f, 0.5f);
			}
			GUI.DrawTexture(new Rect(45f, 162f, (float)ClanSystemWindow.I.Textures.MessageArrow.width, (float)ClanSystemWindow.I.Textures.MessageArrow.height), ClanSystemWindow.I.Textures.MessageArrow);
			GUI.DrawTexture(new Rect(25f, 191f, 658f, 30f), ClanSystemWindow.I.Textures.MessageBack);
			GUI.color = this._tempColor;
			GUI.BeginGroup(areaRect);
			GUI.Label(new Rect(this._messageStartPosition, 0f, this._messageLength, areaRect.height), this._message, ClanSystemWindow.I.Styles.LeaderMessageLabel);
			if (GUI.Button(new Rect(0f, 0f, areaRect.width, areaRect.height), string.Empty, ClanSystemWindow.I.Styles.styleWhiteLabel14) && editable)
			{
				EventFactory.Call("ShowPopup", new Popup(WindowsID.EditClanMessage, Language.ClansEditMessagePopup, string.Empty, delegate()
				{
					Main.AddDatabaseRequestCallBack<EditClanMessage>(delegate
					{
						this._messageStartPosition = 0f;
						this.calculateWidth = false;
						this._message = CreepingLine.TempMessage;
						CreepingLine.TempMessage = string.Empty;
					}, delegate
					{
					}, new object[]
					{
						CreepingLine.TempMessage
					});
				}, PopupState.editClanMessage, false, true, new object[]
				{
					this._message
				}));
			}
			GUI.EndGroup();
		}

		// Token: 0x04000845 RID: 2117
		private string _message = string.Empty;

		// Token: 0x04000846 RID: 2118
		private float _messageLength = -1f;

		// Token: 0x04000847 RID: 2119
		private float _messageStartPosition;

		// Token: 0x04000848 RID: 2120
		private float _messageEndPosition;

		// Token: 0x04000849 RID: 2121
		private bool calculateWidth;

		// Token: 0x0400084A RID: 2122
		public static string TempMessage = string.Empty;

		// Token: 0x0400084B RID: 2123
		private Color _tempColor;
	}
}
