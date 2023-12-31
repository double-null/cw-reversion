using System;

namespace TacticalPointNamespace
{
	// Token: 0x020002D3 RID: 723
	internal class BearCaptured : PointState
	{
		// Token: 0x060013C8 RID: 5064 RVA: 0x000D4BD0 File Offset: 0x000D2DD0
		public BearCaptured(TacticalPoint point)
		{
			this.point = point;
			this.state = TacticalPointState.bear_captured;
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x000D4BE8 File Offset: 0x000D2DE8
		public override void OnSet()
		{
			this.point.pointState = this.state;
			Peer.ServerGame.EventMessage(string.Empty, ChatInfo.notify_message, string.Concat(new string[]
			{
				Language.Point,
				" ",
				this.point.Name,
				" ",
				Language.PointBearCaptured
			}));
			this.point.AddExpForCapture();
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x000D4C5C File Offset: 0x000D2E5C
		public override void Update()
		{
			if ((this.point.bearIn > this.point.usecIn || this.point.usecIn == 0) && !this.point.BearSingleCapture)
			{
				if (this.point.captureProgress < 1f)
				{
					this.point.captureProgress += this.point.captureStep * (float)this.point.BearBoost;
				}
				if (this.point.captureProgress > 1f)
				{
					this.point.captureProgress = 1f;
				}
			}
			if (this.point.bearIn >= this.point.usecIn || (this.point.usecIn < this.point.playersNeeded && !this.point.UsecSingleCapture))
			{
				return;
			}
			if (this.point.captureProgress <= 0f)
			{
				return;
			}
			this.point.captureProgress -= 2f * this.point.captureStep * (float)this.point.UsecBoost;
			if (this.point.captureProgress > 0f)
			{
				return;
			}
			this.point.captureProgress = 0f;
			this.point.SetState(this.point.Neutral);
		}
	}
}
