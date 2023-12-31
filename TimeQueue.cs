using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003A0 RID: 928
internal class TimeQueue
{
	// Token: 0x06001D91 RID: 7569 RVA: 0x001033D8 File Offset: 0x001015D8
	public void Update()
	{
		this.Update(Time.time);
	}

	// Token: 0x06001D92 RID: 7570 RVA: 0x001033E8 File Offset: 0x001015E8
	public void Update(float time)
	{
		if (this._off)
		{
			return;
		}
		if (time < this._nextTime)
		{
			return;
		}
		this._nextAction();
		this.Next(time);
	}

	// Token: 0x06001D93 RID: 7571 RVA: 0x00103418 File Offset: 0x00101618
	public void Add(float keyTime, Action action)
	{
		this.Add(Time.time, keyTime, action);
	}

	// Token: 0x06001D94 RID: 7572 RVA: 0x00103428 File Offset: 0x00101628
	public void Add(float time, float keyTime, Action action)
	{
		if (time >= keyTime)
		{
			action();
			return;
		}
		if (this._off)
		{
			this.AddToQueue(keyTime, action);
			this._off = false;
			this.Next(time);
		}
		else if (this._nextTime < keyTime)
		{
			this.AddToQueue(keyTime, action);
		}
		else
		{
			this.AddToQueue(this._nextTime, this._nextAction);
			this._nextTime = keyTime;
			this._nextAction = action;
		}
	}

	// Token: 0x06001D95 RID: 7573 RVA: 0x001034A4 File Offset: 0x001016A4
	private void AddToQueue(float keyTime, Action action)
	{
		Action oldAction;
		if (this._vals.TryGetValue(keyTime, out oldAction))
		{
			this._vals[keyTime] = delegate()
			{
				oldAction();
				action();
			};
		}
		else
		{
			this._vals.Add(keyTime, action);
		}
	}

	// Token: 0x06001D96 RID: 7574 RVA: 0x00103504 File Offset: 0x00101704
	private void Next(float time)
	{
		while (this._vals.Count != 0)
		{
			using (SortedDictionary<float, Action>.Enumerator enumerator = this._vals.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					KeyValuePair<float, Action> keyValuePair = enumerator.Current;
					this._nextTime = keyValuePair.Key;
					this._nextAction = keyValuePair.Value;
					this._vals.Remove(this._nextTime);
				}
			}
			if (time <= this._nextTime)
			{
				return;
			}
			this._nextAction();
		}
		this._off = true;
	}

	// Token: 0x04002249 RID: 8777
	private SortedDictionary<float, Action> _vals = new SortedDictionary<float, Action>();

	// Token: 0x0400224A RID: 8778
	private float _nextTime;

	// Token: 0x0400224B RID: 8779
	private Action _nextAction;

	// Token: 0x0400224C RID: 8780
	private bool _off = true;
}
