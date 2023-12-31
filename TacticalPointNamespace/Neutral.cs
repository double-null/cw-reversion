using System;

namespace TacticalPointNamespace
{
	// Token: 0x020002D2 RID: 722
	internal class Neutral : PointState
	{
		// Token: 0x060013C4 RID: 5060 RVA: 0x000D48B0 File Offset: 0x000D2AB0
		public Neutral(TacticalPoint point)
		{
			this.state = TacticalPointState.neutral;
			this.point = point;
			this.nextState = this;
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x000D48D0 File Offset: 0x000D2AD0
		public override void OnSet()
		{
			if (this.point.pointState == TacticalPointState.bear_captured)
			{
				this.point.AddExpForNeutralize(false);
			}
			if (this.point.pointState == TacticalPointState.usec_captured)
			{
				this.point.AddExpForNeutralize(true);
			}
			this.point.pointState = this.state;
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x000D4928 File Offset: 0x000D2B28
		private void CaptureCheck(int more, int less, ITacticalPointState moreP, ITacticalPointState lessP, int boost = 1)
		{
			if (more <= less)
			{
				return;
			}
			if (this.nextState == this)
			{
				this.nextState = moreP;
			}
			if (this.nextState == moreP && this.point.captureProgress < 1f)
			{
				this.point.captureProgress += this.point.captureStep * (float)boost;
				if (this.point.captureProgress >= 1f)
				{
					this.point.captureProgress = 1f;
					this.point.SetState(moreP);
					this.nextState = this;
				}
				this.point.nextPointState = moreP.State;
			}
			if (this.nextState != lessP || this.point.captureProgress > 1f)
			{
				return;
			}
			this.point.captureProgress -= this.point.captureStep * (float)boost;
			if (this.point.captureProgress > 0f)
			{
				return;
			}
			this.point.captureProgress = 0f;
			this.nextState = this;
			this.point.nextPointState = this.State;
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x000D4A60 File Offset: 0x000D2C60
		public override void Update()
		{
			if (this.point.bearIn < this.point.playersNeeded && this.point.usecIn < this.point.playersNeeded && !this.point.UsecSingleCapture && !this.point.BearSingleCapture)
			{
				if (this.point.captureProgress > 0f)
				{
					this.point.captureProgress -= this.point.captureStep;
				}
				if (this.point.captureProgress < 0f)
				{
					this.point.captureProgress = 0f;
				}
			}
			else
			{
				if (this.point.bearIn > this.point.usecIn)
				{
					this.CaptureCheck(this.point.bearIn, this.point.usecIn, this.point.BearCaptured, this.point.UsecCaptured, this.point.BearBoost);
				}
				if (this.point.usecIn > this.point.bearIn)
				{
					this.CaptureCheck(this.point.usecIn, this.point.bearIn, this.point.UsecCaptured, this.point.BearCaptured, this.point.UsecBoost);
				}
			}
		}

		// Token: 0x04001885 RID: 6277
		private ITacticalPointState nextState;
	}
}
