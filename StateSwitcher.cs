using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200038B RID: 907
internal class StateSwitcher
{
	// Token: 0x17000841 RID: 2113
	// (get) Token: 0x06001D2C RID: 7468 RVA: 0x00100A44 File Offset: 0x000FEC44
	// (set) Token: 0x06001D2D RID: 7469 RVA: 0x00100A4C File Offset: 0x000FEC4C
	public StateSwitcher.State SwitchState { get; private set; }

	// Token: 0x17000842 RID: 2114
	// (get) Token: 0x06001D2E RID: 7470 RVA: 0x00100A58 File Offset: 0x000FEC58
	// (set) Token: 0x06001D2F RID: 7471 RVA: 0x00100A60 File Offset: 0x000FEC60
	public bool On { get; private set; }

	// Token: 0x17000843 RID: 2115
	// (get) Token: 0x06001D30 RID: 7472 RVA: 0x00100A6C File Offset: 0x000FEC6C
	// (set) Token: 0x06001D31 RID: 7473 RVA: 0x00100A74 File Offset: 0x000FEC74
	public bool InProcess { get; private set; }

	// Token: 0x17000844 RID: 2116
	// (get) Token: 0x06001D32 RID: 7474 RVA: 0x00100A80 File Offset: 0x000FEC80
	// (set) Token: 0x06001D33 RID: 7475 RVA: 0x00100A88 File Offset: 0x000FEC88
	public float Value { get; private set; }

	// Token: 0x17000845 RID: 2117
	// (get) Token: 0x06001D34 RID: 7476 RVA: 0x00100A94 File Offset: 0x000FEC94
	// (set) Token: 0x06001D35 RID: 7477 RVA: 0x00100A9C File Offset: 0x000FEC9C
	public AnimationCurve SwitchingOn
	{
		get
		{
			return this._switchingOn;
		}
		set
		{
			if (value == this._switchingOn)
			{
				return;
			}
			this._switchingOn = value;
			this._switchingOnEnd = this._switchingOn[this._switchingOn.length - 1].time;
		}
	}

	// Token: 0x17000846 RID: 2118
	// (get) Token: 0x06001D36 RID: 7478 RVA: 0x00100AE4 File Offset: 0x000FECE4
	// (set) Token: 0x06001D37 RID: 7479 RVA: 0x00100AEC File Offset: 0x000FECEC
	public AnimationCurve SwitchingOff
	{
		get
		{
			return this._switchingOff;
		}
		set
		{
			if (value == this._switchingOff)
			{
				return;
			}
			this._switchingOff = value;
			this._switchingOffEnd = this._switchingOff[this._switchingOff.length - 1].time;
		}
	}

	// Token: 0x06001D38 RID: 7480 RVA: 0x00100B34 File Offset: 0x000FED34
	public void Process(float deltaTime)
	{
		if (!this.InProcess)
		{
			return;
		}
		this.t += deltaTime;
		if (this.SwitchState == StateSwitcher.State.switchingOn)
		{
			this.Value = this.SwitchingOn.Evaluate(this.t);
			this.ProcessCase();
			if (this.t >= this._switchingOnEnd)
			{
				this.SwitchState = StateSwitcher.State.on;
				this.On = true;
				this.InProcess = false;
			}
		}
		else
		{
			this.Value = this.SwitchingOff.Evaluate(this.t);
			this.ProcessCase();
			if (this.t >= this._switchingOffEnd)
			{
				this.SwitchState = StateSwitcher.State.off;
				this.On = false;
				this.InProcess = false;
			}
		}
	}

	// Token: 0x06001D39 RID: 7481 RVA: 0x00100BF4 File Offset: 0x000FEDF4
	public bool Switch(bool on)
	{
		if (on == ((!this.InProcess) ? this.On : (!this.On)))
		{
			return false;
		}
		this.t = 0f;
		this.SwitchState = ((!on) ? StateSwitcher.State.switchingOff : StateSwitcher.State.switchingOn);
		this.InProcess = true;
		if (this.getList(on) != null)
		{
			this._caseEnumerator = this.getList(on).GetEnumerator();
			this._caseEnumerator.MoveNext();
			KeyValuePair<float, Action> keyValuePair = this._caseEnumerator.Current;
			this._caseTime = keyValuePair.Key;
		}
		return true;
	}

	// Token: 0x06001D3A RID: 7482 RVA: 0x00100C94 File Offset: 0x000FEE94
	public void ForceSwitch(bool on)
	{
		this.t = 0f;
		this.SwitchState = ((!on) ? StateSwitcher.State.off : StateSwitcher.State.on);
		this.On = on;
		this.InProcess = false;
	}

	// Token: 0x06001D3B RID: 7483 RVA: 0x00100CD0 File Offset: 0x000FEED0
	public void AddCase(float time, Action Func, bool switchingOn)
	{
		if (this.getList(switchingOn) == null)
		{
			if (switchingOn)
			{
				this._switchingOnList = new SortedList<float, Action>();
			}
			else
			{
				this._switchingOffList = new SortedList<float, Action>();
			}
		}
		this.getList(switchingOn).Add(time, Func);
	}

	// Token: 0x06001D3C RID: 7484 RVA: 0x00100D10 File Offset: 0x000FEF10
	private SortedList<float, Action> getList(bool on)
	{
		return (!on) ? this._switchingOffList : this._switchingOnList;
	}

	// Token: 0x06001D3D RID: 7485 RVA: 0x00100D2C File Offset: 0x000FEF2C
	private void ProcessCase()
	{
		if (this._caseEnumerator != null && this._caseTime < this.t)
		{
			KeyValuePair<float, Action> keyValuePair = this._caseEnumerator.Current;
			keyValuePair.Value();
			if (this._caseEnumerator.MoveNext())
			{
				KeyValuePair<float, Action> keyValuePair2 = this._caseEnumerator.Current;
				this._caseTime = keyValuePair2.Key;
			}
			else
			{
				this._caseEnumerator = null;
			}
		}
	}

	// Token: 0x040021DC RID: 8668
	private AnimationCurve _switchingOn;

	// Token: 0x040021DD RID: 8669
	private AnimationCurve _switchingOff;

	// Token: 0x040021DE RID: 8670
	private float _switchingOnEnd;

	// Token: 0x040021DF RID: 8671
	private float _switchingOffEnd;

	// Token: 0x040021E0 RID: 8672
	private float t;

	// Token: 0x040021E1 RID: 8673
	private SortedList<float, Action> _switchingOnList;

	// Token: 0x040021E2 RID: 8674
	private SortedList<float, Action> _switchingOffList;

	// Token: 0x040021E3 RID: 8675
	private IEnumerator<KeyValuePair<float, Action>> _caseEnumerator;

	// Token: 0x040021E4 RID: 8676
	private float _caseTime;

	// Token: 0x0200038C RID: 908
	public enum State
	{
		// Token: 0x040021EA RID: 8682
		off,
		// Token: 0x040021EB RID: 8683
		on,
		// Token: 0x040021EC RID: 8684
		switchingOff = 3,
		// Token: 0x040021ED RID: 8685
		switchingOn = 2
	}
}
