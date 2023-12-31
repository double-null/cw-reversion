using System;
using System.Collections.Generic;
using UnityEngine;

namespace HallOfFameNamespace
{
	// Token: 0x02000150 RID: 336
	internal class HallOfFameUnit
	{
		// Token: 0x06000843 RID: 2115 RVA: 0x00049F0C File Offset: 0x0004810C
		public void SetPos(int ix, int iy)
		{
			this.xpos = ix;
			this.ypos = iy;
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00049F1C File Offset: 0x0004811C
		public virtual void Show()
		{
			HallOfFameUnit.gui.TextField(new Rect((float)this.xpos, (float)(this.ypos - 25), 200f, 30f), this.HeaderInfo, 17, "#62aeea", TextAnchor.UpperCenter, false, false);
			HallOfFameUnit.gui.Picture(new Vector2((float)this.xpos, (float)this.ypos), HallOfFameUnit.halloffame_back);
			if (this.avatar != null)
			{
				HallOfFameUnit.gui.PictureSized(new Vector2((float)this.xpos, (float)this.ypos), this.avatar, new Vector2(54f, 54f));
			}
			else
			{
				HallOfFameUnit.gui.PictureSized(new Vector2((float)this.xpos, (float)this.ypos), HallOfFameUnit.avatarSample, new Vector2(54f, 54f));
			}
			if (this.lvlIcon != null)
			{
				HallOfFameUnit.gui.Picture(new Vector2((float)(this.xpos + 55), (float)(this.ypos + 10)), this.lvlIcon);
			}
			HallOfFameUnit.gui.TextField(new Rect((float)(this.xpos + 85), (float)(this.ypos + 25), 120f, 25f), this.Name, 14, "#a1a1a1", TextAnchor.UpperCenter, false, false);
			HallOfFameUnit.gui.TextField(new Rect((float)(this.xpos + 95), (float)(this.ypos + 7), 120f, 25f), this.Nick, 16, "#ffffff", TextAnchor.UpperCenter, false, false);
			if (this.classIcon != null)
			{
				HallOfFameUnit.gui.Picture(new Vector2((float)(this.xpos + 90), (float)(this.ypos + 3)), this.classIcon);
			}
			HallOfFameUnit.gui.TextField(new Rect((float)(this.xpos + 130), (float)(this.ypos + 52), 120f, 25f), this.someValue, 16, "#ffffff", TextAnchor.UpperCenter, false, false);
			if (this.userID != Main.UserInfo.userID)
			{
				HallOfFameUnit.gui.VoteWidget(new Vector2((float)(this.xpos - 20), (float)(this.ypos + 52)), this.userID, (float)this.repa, -1);
			}
			else
			{
				HallOfFameUnit.gui.VoteWidget(new Vector2((float)(this.xpos - 20), (float)(this.ypos + 52)), Main.UserInfo.userID, Main.UserInfo.repa, -1);
			}
			if (HallOfFameUnit.infoBtn != null)
			{
				HallOfFameUnit.gui.Button(new Vector2((float)(this.xpos + 61), (float)(this.ypos + 54)), HallOfFameUnit.infoBtn[0], HallOfFameUnit.infoBtn[1], HallOfFameUnit.infoBtn[0], string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null);
			}
			if (this.sameImg != null)
			{
				HallOfFameUnit.gui.Picture(new Vector2((float)(this.xpos + 150 - this.sameImg.width / 2), (float)(this.ypos + 64 - this.sameImg.height / 2)), this.sameImg);
			}
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0004A28C File Offset: 0x0004848C
		public void Parse(Dictionary<string, object> data)
		{
			if (data.ContainsKey("userID"))
			{
				this.userID = (int)data["userID"];
			}
			if (data.ContainsKey("lvl"))
			{
				this.lvl = (int)data["lvl"];
			}
			if (data.ContainsKey("nick"))
			{
				this.Nick = (string)data["nick"];
			}
			if (data.ContainsKey("name"))
			{
				this.Name = (string)data["name"];
			}
			if (data.ContainsKey("photo"))
			{
				this.PhotoUrl = (string)data["photo"];
			}
			if (data.ContainsKey("someValue"))
			{
				this.someValue = data["someValue"].ToString();
			}
			if (data.ContainsKey("reputation"))
			{
				this.repa = (int)data["reputation"];
			}
			this.lvlIcon = ((this.lvl >= HallOfFameUnit.gui.rank_icon.Length) ? HallOfFameUnit.gui.rank_icon[HallOfFameUnit.gui.rank_icon.Length - 1] : HallOfFameUnit.gui.rank_icon[this.lvl]);
		}

		// Token: 0x04000951 RID: 2385
		public static Texture2D halloffame_back;

		// Token: 0x04000952 RID: 2386
		public static Texture2D[] TabArrows;

		// Token: 0x04000953 RID: 2387
		public static Texture2D[] voteBtn;

		// Token: 0x04000954 RID: 2388
		public static Texture2D[] infoBtn;

		// Token: 0x04000955 RID: 2389
		public static Texture2D avatarSample;

		// Token: 0x04000956 RID: 2390
		public Texture2D sameImg;

		// Token: 0x04000957 RID: 2391
		public Texture2D classIcon;

		// Token: 0x04000958 RID: 2392
		public Texture2D lvlIcon;

		// Token: 0x04000959 RID: 2393
		public Texture2D avatar;

		// Token: 0x0400095A RID: 2394
		public int userID;

		// Token: 0x0400095B RID: 2395
		public int lvl;

		// Token: 0x0400095C RID: 2396
		public int repa;

		// Token: 0x0400095D RID: 2397
		public string PhotoUrl = string.Empty;

		// Token: 0x0400095E RID: 2398
		public string HeaderInfo;

		// Token: 0x0400095F RID: 2399
		public string Nick = "WWS Zlojopa2";

		// Token: 0x04000960 RID: 2400
		public string Name = "Леонид Прокопенко";

		// Token: 0x04000961 RID: 2401
		public string someValue = "99999999";

		// Token: 0x04000962 RID: 2402
		public static MainGUI gui;

		// Token: 0x04000963 RID: 2403
		public static float visibility;

		// Token: 0x04000964 RID: 2404
		private int xpos;

		// Token: 0x04000965 RID: 2405
		private int ypos;
	}
}
