using System;
using UnityEngine;

// Token: 0x020001C4 RID: 452
[AddComponentMenu("Scripts/Game/Components/DebugAnimations")]
internal class DebugAnimations : MonoBehaviour
{
	// Token: 0x06000F6E RID: 3950 RVA: 0x000B11BC File Offset: 0x000AF3BC
	private void FixedUpdate()
	{
		if (Peer.ClientGame.LocalPlayer.IsDeadOrSpectactor)
		{
			return;
		}
		EventFactory.Call("ShowTeamChoose", 0.5f);
		SingletoneComponent<Main>.Instance.enabled = false;
		Screen.lockCursor = false;
		if (this.Ammo == null)
		{
			this.Ammo = Peer.ClientGame.LocalPlayer.Ammo;
			this.handsAnimation = Peer.ClientGame.LocalPlayer.Animations.handsAnimation;
			this.handsAnimation.Stop();
			this.CurrentWeapon = base.GetComponent<BaseAmmunitions>().CurrentWeapon;
			this.CurrentWeaponAnimation = this.CurrentWeapon.GetComponentInChildren<Animation>();
			this.CurrentWeaponAnimation.Stop();
		}
		this.CurrentWeapon.Tick(Time.fixedDeltaTime);
		if (this.handsAnimation[this.CurrentWeapon.type.ToString() + "_reload"].normalizedTime == 0f)
		{
			this.handsAnimation.Stop();
			this.handsAnimation.Play(this.CurrentWeapon.type.ToString() + "_reload");
			this.CurrentWeapon.state.clips = 99;
			this.CurrentWeapon.state.bagSize = 100;
			this.CurrentWeapon.Cancel();
			this.CurrentWeapon.Reload();
		}
	}

	// Token: 0x06000F6F RID: 3951 RVA: 0x000B1338 File Offset: 0x000AF538
	private void OnGUI()
	{
		GUI.depth = -100;
		if (this.Ammo == null || this.CurrentWeapon == null || this.CurrentWeaponAnimation == null || this.handsAnimation == null)
		{
			return;
		}
		string text = string.Empty;
		text = text + "Object " + base.name + ":\n";
		foreach (object obj in this.handsAnimation)
		{
			AnimationState animationState = (AnimationState)obj;
			if (this.handsAnimation.IsPlaying(animationState.name))
			{
				this.stateRef = animationState;
			}
		}
		foreach (object obj2 in this.CurrentWeaponAnimation)
		{
			AnimationState animationState2 = (AnimationState)obj2;
			if (this.CurrentWeaponAnimation.IsPlaying(animationState2.name))
			{
				this.gunRef = animationState2;
			}
		}
		text = text + this.GetTextDesc(this.stateRef) + "\n";
		GUI.Box(new Rect(50f, 50f, 250f, 250f), string.Empty);
		GUILayout.BeginArea(new Rect(50f, 50f, 250f, 250f));
		GUILayout.BeginVertical(new GUILayoutOption[0]);
		GUILayout.Box(text, new GUILayoutOption[0]);
		GUILayout.EndVertical();
		GUILayout.EndArea();
		GUI.Box(new Rect(50f, 320f, 250f, 85f), string.Empty);
		GUILayout.BeginArea(new Rect(55f, 325f, 240f, 75f));
		GUILayout.BeginVertical(new GUILayoutOption[0]);
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.Label("Local Time", new GUILayoutOption[0]);
		if (this.oneFrame != 0 && this.stateRef.enabled)
		{
			this.stateRef.enabled = false;
			this.gunRef.enabled = false;
			this.oneFrame = 0;
		}
		float time = this.stateRef.time;
		this.stateRef.time = GUILayout.HorizontalSlider(this.stateRef.time, 0f, this.stateRef.length, new GUILayoutOption[0]);
		this.gunRef.time = this.stateRef.time;
		if (time != this.stateRef.time)
		{
			this.stateRef.enabled = true;
			this.gunRef.enabled = true;
			this.oneFrame = 1;
		}
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		GUILayout.Label("Time Scale", new GUILayoutOption[0]);
		Time.timeScale = GUILayout.HorizontalSlider(Time.timeScale, 0f, 1f, new GUILayoutOption[0]);
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal(new GUILayoutOption[0]);
		if (GUILayout.Button("Play/Stop", new GUILayoutOption[0]))
		{
			this.stateRef.enabled = !this.stateRef.enabled;
			this.gunRef.enabled = !this.stateRef.enabled;
		}
		GUILayout.EndHorizontal();
		GUILayout.EndVertical();
		GUILayout.EndArea();
	}

	// Token: 0x06000F70 RID: 3952 RVA: 0x000B16EC File Offset: 0x000AF8EC
	private string GetTextDesc(AnimationState st)
	{
		return string.Format("{0}: {1} {2:0.00} / {3:0.00} {4}", new object[]
		{
			st.name,
			st.wrapMode,
			st.time,
			st.length,
			st.speed
		});
	}

	// Token: 0x04000FC3 RID: 4035
	private int oneFrame;

	// Token: 0x04000FC4 RID: 4036
	private AnimationState stateRef;

	// Token: 0x04000FC5 RID: 4037
	private AnimationState gunRef;

	// Token: 0x04000FC6 RID: 4038
	private BaseAmmunitions Ammo;

	// Token: 0x04000FC7 RID: 4039
	private BaseWeapon CurrentWeapon;

	// Token: 0x04000FC8 RID: 4040
	private Animation handsAnimation;

	// Token: 0x04000FC9 RID: 4041
	private Animation CurrentWeaponAnimation;
}
