using System;

namespace TacticalPointNamespace
{
	// Token: 0x020002D4 RID: 724
	internal class UsecCaptured : PointState
	{
		// Token: 0x060013CB RID: 5067 RVA: 0x000D4DD4 File Offset: 0x000D2FD4
		public UsecCaptured(TacticalPoint point)
		{
			this.point = point;
			this.state = TacticalPointState.usec_captured;
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x000D4DEC File Offset: 0x000D2FEC
		public override void OnSet()
		{
			this.point.pointState = this.state;
			Peer.ServerGame.EventMessage(string.Empty, ChatInfo.notify_message, string.Concat(new string[]
			{
				Language.Point,
				" ",
				this.point.Name,
				" ",
				Language.PointUsecCaptured
			}));
			this.point.AddExpForCapture();
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x000D4E60 File Offset: 0x000D3060
		public override void Update()
		{
			if ((this.point.bearIn < this.point.usecIn || this.point.bearIn == 0) && !this.point.BearSingleCapture)
			{
				if (this.point.captureProgress < 1f)
				{
					this.point.captureProgress += this.point.captureStep * (float)this.point.UsecBoost;
				}
				if (this.point.captureProgress > 1f)
				{
					this.point.captureProgress = 1f;
				}
			}
			if (this.point.bearIn <= this.point.usecIn || (this.point.bearIn < this.point.playersNeeded && !this.point.BearSingleCapture))
			{
				return;
			}
			if (this.point.captureProgress <= 0f)
			{
				return;
			}
			this.point.captureProgress -= 2f * this.point.captureStep * (float)this.point.BearBoost;
			if (this.point.captureProgress > 0f)
			{
				return;
			}
			this.point.captureProgress = 0f;
			this.point.SetState(this.point.Neutral);
		}
	}
}
