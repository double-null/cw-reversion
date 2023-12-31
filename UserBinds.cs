using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002D7 RID: 727
[Serializable]
internal class UserBinds : Convertible
{
	// Token: 0x060013D1 RID: 5073 RVA: 0x000D5124 File Offset: 0x000D3324
	public void Convert(Dictionary<string, object> dict, bool set)
	{
		if (dict == null)
		{
			return;
		}
		JSON.ReadWrite(dict, "left", ref this.left, set);
		JSON.ReadWrite(dict, "right", ref this.right, set);
		JSON.ReadWrite(dict, "up", ref this.up, set);
		JSON.ReadWrite(dict, "down", ref this.down, set);
		JSON.ReadWrite(dict, "sit", ref this.sit, set);
		JSON.ReadWrite(dict, "walk", ref this.walk, set);
		JSON.ReadWrite(dict, "interaction", ref this.interaction, set);
		JSON.ReadWrite(dict, "knife", ref this.knife, set);
		JSON.ReadWrite(dict, "pistol", ref this.pistol, set);
		JSON.ReadWrite(dict, "weapon", ref this.weapon, set);
		JSON.ReadWrite(dict, "support", ref this.support, set);
		JSON.ReadWrite(dict, "mortar", ref this.mortar, set);
		JSON.ReadWrite(dict, "thermalVision", ref this.clanSpecialAbility, set);
		JSON.ReadWrite(dict, "hide_interface", ref this.hideInterface, set);
		JSON.ReadWrite(dict, "flashlight", ref this.flashlight, set);
		JSON.ReadWrite(dict, "toggle", ref this.toggle, set);
		JSON.ReadWrite(dict, "reload", ref this.reload, set);
		JSON.ReadWrite(dict, "burst", ref this.burst, set);
		JSON.ReadWrite(dict, "fire", ref this.fire, set);
		JSON.ReadWrite(dict, "aim", ref this.aim, set);
		JSON.ReadWrite(dict, "jump", ref this.jump, set);
		JSON.ReadWrite(dict, "grenade", ref this.grenade, set);
		JSON.ReadWrite(dict, "menu", ref this.menu, set);
		JSON.ReadWrite(dict, "statistics", ref this.statistics, set);
		JSON.ReadWrite(dict, "fullscreen", ref this.fullscreen, set);
		JSON.ReadWrite(dict, "teamChoose", ref this.teamChoose, set);
		JSON.ReadWrite(dict, "talkAll", ref this.talkAll, set);
		JSON.ReadWrite(dict, "talkTeam", ref this.talkTeam, set);
		JSON.ReadWrite(dict, "radio", ref this.radio, set);
		JSON.ReadWrite(dict, "screenshot", ref this.screenshot, set);
		JSON.ReadWrite(dict, "sens", ref this.sens, set);
		JSON.ReadWrite(dict, "invertMouse", ref this.invertMouse, set);
	}

	// Token: 0x060013D2 RID: 5074 RVA: 0x000D5378 File Offset: 0x000D3578
	public static KeyCode? getKey()
	{
		for (int i = 0; i < 409; i++)
		{
			if (Input.GetKeyDown((KeyCode)i))
			{
				return new KeyCode?((KeyCode)i);
			}
		}
		return null;
	}

	// Token: 0x04001886 RID: 6278
	public KeyCode left = KeyCode.A;

	// Token: 0x04001887 RID: 6279
	public KeyCode right = KeyCode.D;

	// Token: 0x04001888 RID: 6280
	public KeyCode up = KeyCode.W;

	// Token: 0x04001889 RID: 6281
	public KeyCode down = KeyCode.S;

	// Token: 0x0400188A RID: 6282
	public KeyCode sit = KeyCode.C;

	// Token: 0x0400188B RID: 6283
	public KeyCode walk = KeyCode.LeftShift;

	// Token: 0x0400188C RID: 6284
	public KeyCode interaction = KeyCode.F;

	// Token: 0x0400188D RID: 6285
	public KeyCode knife = KeyCode.V;

	// Token: 0x0400188E RID: 6286
	public KeyCode pistol = KeyCode.Alpha1;

	// Token: 0x0400188F RID: 6287
	public KeyCode weapon = KeyCode.Alpha2;

	// Token: 0x04001890 RID: 6288
	public KeyCode support = KeyCode.Alpha3;

	// Token: 0x04001891 RID: 6289
	public KeyCode mortar = KeyCode.Alpha4;

	// Token: 0x04001892 RID: 6290
	public KeyCode clanSpecialAbility = KeyCode.B;

	// Token: 0x04001893 RID: 6291
	public KeyCode flashlight = KeyCode.L;

	// Token: 0x04001894 RID: 6292
	public KeyCode hideInterface = KeyCode.K;

	// Token: 0x04001895 RID: 6293
	public KeyCode toggle = KeyCode.Mouse2;

	// Token: 0x04001896 RID: 6294
	public KeyCode reload = KeyCode.R;

	// Token: 0x04001897 RID: 6295
	public KeyCode burst = KeyCode.Z;

	// Token: 0x04001898 RID: 6296
	public KeyCode fire = KeyCode.Mouse0;

	// Token: 0x04001899 RID: 6297
	public KeyCode aim = KeyCode.Mouse1;

	// Token: 0x0400189A RID: 6298
	public KeyCode jump = KeyCode.Space;

	// Token: 0x0400189B RID: 6299
	public KeyCode grenade = KeyCode.G;

	// Token: 0x0400189C RID: 6300
	public KeyCode menu = KeyCode.F10;

	// Token: 0x0400189D RID: 6301
	public KeyCode statistics = KeyCode.Tab;

	// Token: 0x0400189E RID: 6302
	public KeyCode fullscreen = KeyCode.F12;

	// Token: 0x0400189F RID: 6303
	public KeyCode teamChoose = KeyCode.M;

	// Token: 0x040018A0 RID: 6304
	public KeyCode talkTeam = KeyCode.T;

	// Token: 0x040018A1 RID: 6305
	public KeyCode talkAll = KeyCode.Y;

	// Token: 0x040018A2 RID: 6306
	public KeyCode radio = KeyCode.Q;

	// Token: 0x040018A3 RID: 6307
	public KeyCode screenshot = KeyCode.F5;

	// Token: 0x040018A4 RID: 6308
	public float sens = 1f;

	// Token: 0x040018A5 RID: 6309
	public bool invertMouse;

	// Token: 0x040018A6 RID: 6310
	public bool holdAim;

	// Token: 0x040018A7 RID: 6311
	public bool holdWalk = true;

	// Token: 0x040018A8 RID: 6312
	public bool holdSit;
}
