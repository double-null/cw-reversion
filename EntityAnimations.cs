using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001C5 RID: 453
[AddComponentMenu("Scripts/Game/Components/EntityAnimations")]
public class EntityAnimations : PoolableBehaviour
{
	// Token: 0x06000F73 RID: 3955 RVA: 0x000B1764 File Offset: 0x000AF964
	private void OnEnable()
	{
		EntityAnimations.I = this;
	}

	// Token: 0x06000F74 RID: 3956 RVA: 0x000B176C File Offset: 0x000AF96C
	private void OnDisable()
	{
		EntityAnimations.I = null;
	}

	// Token: 0x04000FCA RID: 4042
	public static EntityAnimations I;

	// Token: 0x04000FCB RID: 4043
	public List<EntityAnimations.EntityAnimationState> states = new List<EntityAnimations.EntityAnimationState>();

	// Token: 0x020001C6 RID: 454
	public enum EntityAnimationPlayType
	{
		// Token: 0x04000FCD RID: 4045
		Play,
		// Token: 0x04000FCE RID: 4046
		PlayQueued,
		// Token: 0x04000FCF RID: 4047
		CrossFade,
		// Token: 0x04000FD0 RID: 4048
		CrossFadeQueued,
		// Token: 0x04000FD1 RID: 4049
		Blend
	}

	// Token: 0x020001C7 RID: 455
	public class EntityAnimationState
	{
		// Token: 0x06000F75 RID: 3957 RVA: 0x000B1774 File Offset: 0x000AF974
		public EntityAnimationState(AnimationState state)
		{
			this.clip = state.clip;
			this.blendMode = state.blendMode;
			this.wrapMode = state.wrapMode;
			this.layer = state.layer;
			this.speed = state.speed;
			this.time = state.clip.length;
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x000B17F4 File Offset: 0x000AF9F4
		public void Copy(EntityAnimations.EntityAnimationState state, bool PT = true, bool BM = true, bool WM = true, bool L = true, bool S = true, bool T = true)
		{
			if (PT)
			{
				this.playType = state.playType;
			}
			if (BM)
			{
				this.blendMode = state.blendMode;
			}
			if (WM)
			{
				this.wrapMode = state.wrapMode;
			}
			if (L)
			{
				this.layer = state.layer;
			}
			if (S)
			{
				this.speed = state.speed;
			}
			if (T)
			{
				this.time = state.time;
			}
		}

		// Token: 0x04000FD2 RID: 4050
		[NonSerialized]
		public bool fold = true;

		// Token: 0x04000FD3 RID: 4051
		public AnimationClip clip;

		// Token: 0x04000FD4 RID: 4052
		public AnimationBlendMode blendMode;

		// Token: 0x04000FD5 RID: 4053
		public WrapMode wrapMode = WrapMode.Once;

		// Token: 0x04000FD6 RID: 4054
		public EntityAnimations.EntityAnimationPlayType playType = EntityAnimations.EntityAnimationPlayType.CrossFade;

		// Token: 0x04000FD7 RID: 4055
		public int layer;

		// Token: 0x04000FD8 RID: 4056
		public float speed = 1f;

		// Token: 0x04000FD9 RID: 4057
		public float time;
	}
}
