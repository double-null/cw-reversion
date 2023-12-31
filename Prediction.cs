using System;
using UnityEngine;

// Token: 0x020002AA RID: 682
[Serializable]
internal class Prediction
{
	// Token: 0x06001337 RID: 4919 RVA: 0x000CF48C File Offset: 0x000CD68C
	public PredictionState StateOf(int number)
	{
		for (int i = 0; i < this.qstates.Lenght; i++)
		{
			if (this.qstates[i].clientState.playerInfo.number == number)
			{
				return this.qstates[i];
			}
		}
		return null;
	}

	// Token: 0x06001338 RID: 4920 RVA: 0x000CF4E4 File Offset: 0x000CD6E4
	public int IndexOf(int number)
	{
		for (int i = 0; i < this.qstates.Lenght; i++)
		{
			if (this.qstates[i].clientState.playerInfo.number == number)
			{
				return i;
			}
		}
		return IDUtil.NoID;
	}

	// Token: 0x06001339 RID: 4921 RVA: 0x000CF538 File Offset: 0x000CD738
	public int IndexOf(PredictionState state)
	{
		for (int i = 0; i < this.qstates.Lenght; i++)
		{
			if (this.qstates[i].clientState.playerInfo.number == state.clientState.playerInfo.number)
			{
				return i;
			}
		}
		return IDUtil.NoID;
	}

	// Token: 0x0600133A RID: 4922 RVA: 0x000CF598 File Offset: 0x000CD798
	public void Clear()
	{
		this.qstates.Clear();
		this.currentState = null;
		this.NeedFullUpdate = false;
		this.NeedMoveDrop = false;
	}

	// Token: 0x0600133B RID: 4923 RVA: 0x000CF5C8 File Offset: 0x000CD7C8
	public void AddClient(PredictionState client)
	{
		if (this._predicting)
		{
			return;
		}
		if (this.currentState == null || this.currentState.inMinThreshold)
		{
			client.inMinThreshold = true;
			client.SetPredicted(client.clientState.playerInfo, client.clientState.ammoState, client.clientState.moveState);
		}
		this.clientCurrentState = client;
		this.qstates.Add(client);
		if (this.currentState == null || this.currentState.inMinThreshold)
		{
			this.currentState = this.StateOf(client.clientState.playerInfo.number);
		}
	}

	// Token: 0x0600133C RID: 4924 RVA: 0x000CF674 File Offset: 0x000CD874
	public void AddServer(EntityPacket serverState)
	{
		if (this._predicting)
		{
			return;
		}
		if (serverState.playerInfo.number == IDUtil.NoID)
		{
			return;
		}
		int num = this.IndexOf(serverState.playerInfo.number);
		if (num != IDUtil.NoID)
		{
			PredictionState predictionState = this.qstates[num];
			if (num > 1)
			{
				this.qstates.RemoveFromFirstTo(num - 1);
			}
			if (!predictionState.server)
			{
				predictionState.SetServer(serverState.playerInfo, serverState.ammoState, serverState.moveState);
				Vector3 a = predictionState.clientState.moveState.pos - serverState.moveState.pos;
				float magnitude = a.magnitude;
				if (magnitude > 0.001f)
				{
					this.Instantly = (magnitude > 1.2f);
					if (this.Instantly)
					{
						this.Target = serverState.moveState.pos;
					}
					this._delta = -a;
					this._inerpolating = 0f;
					this._predicting = true;
					for (int i = 0; i < this.qstates.Lenght; i++)
					{
						this.qstates[i].ClearPredicted();
						this.qstates[i].inMinThreshold = false;
					}
					this.currentState = predictionState;
					this.currentState.SetPredicted(serverState.playerInfo, serverState.ammoState, serverState.moveState);
				}
				predictionState.clientState.ammoState.OnChange(predictionState.serverState.ammoState);
			}
		}
		else if (this.ConnectionProblemTimer < Time.time)
		{
			this.ConnectionProblemTimer = Time.time + 5f;
			Vector3 a2 = this.clientCurrentState.clientState.moveState.pos - serverState.moveState.pos;
			float magnitude2 = a2.magnitude;
			if (magnitude2 > 6f)
			{
				this.Instantly = (magnitude2 > 6f);
				if (this.Instantly)
				{
					this.Target = serverState.moveState.pos;
				}
				this._delta = -a2;
				this._inerpolating = 0f;
				this._predicting = true;
			}
		}
	}

	// Token: 0x0600133D RID: 4925 RVA: 0x000CF8B4 File Offset: 0x000CDAB4
	public Vector3 AddPredictedDelta()
	{
		if (this.currentState == null || !this.currentState.predicted)
		{
			return Vector3.zero;
		}
		return Vector3.zero;
	}

	// Token: 0x0600133E RID: 4926 RVA: 0x000CF8E8 File Offset: 0x000CDAE8
	public Vector3 SmoothDir(MoveState current)
	{
		if (!this._predicting)
		{
			return Vector3.zero;
		}
		if (this.Instantly)
		{
			this.Instantly = false;
			this._predicting = false;
			this.qstates.Clear();
			return Vector3.zero;
		}
		this._inerpolating += Time.deltaTime * 12f;
		if (this._inerpolating >= 1f)
		{
			this._predicting = false;
			this.qstates.Clear();
			return Vector3.zero;
		}
		return this._delta * 12f;
	}

	// Token: 0x0600133F RID: 4927 RVA: 0x000CF980 File Offset: 0x000CDB80
	public void Draw()
	{
		for (int i = 0; i < this.qstates.Lenght; i++)
		{
			this.qstates[i].Draw();
		}
	}

	// Token: 0x04001632 RID: 5682
	private const float teleportationMinDist = 1.2f;

	// Token: 0x04001633 RID: 5683
	private Prediction.Qeue<PredictionState> qstates = new Prediction.Qeue<PredictionState>(CVars.g_tickrate);

	// Token: 0x04001634 RID: 5684
	public PredictionState currentState;

	// Token: 0x04001635 RID: 5685
	private PredictionState clientCurrentState;

	// Token: 0x04001636 RID: 5686
	public bool NeedFullUpdate;

	// Token: 0x04001637 RID: 5687
	public bool NeedMoveDrop;

	// Token: 0x04001638 RID: 5688
	private Vector3 _delta;

	// Token: 0x04001639 RID: 5689
	private float _inerpolating;

	// Token: 0x0400163A RID: 5690
	private bool _predicting;

	// Token: 0x0400163B RID: 5691
	public bool Instantly;

	// Token: 0x0400163C RID: 5692
	public Vector3 Target;

	// Token: 0x0400163D RID: 5693
	private float ConnectionProblemTimer;

	// Token: 0x0400163E RID: 5694
	private int PacketsCount;

	// Token: 0x0400163F RID: 5695
	private int LostCount;

	// Token: 0x020002AB RID: 683
	private class Qeue<T> where T : class, ReusableClass<T>, new()
	{
		// Token: 0x06001340 RID: 4928 RVA: 0x000CF9BC File Offset: 0x000CDBBC
		public Qeue(int size)
		{
			this._size = size;
			this.Data = new T[size];
			for (int i = 0; i < this.Data.Length; i++)
			{
				this.Data[i] = Activator.CreateInstance<T>();
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06001341 RID: 4929 RVA: 0x000CFA0C File Offset: 0x000CDC0C
		public int Lenght
		{
			get
			{
				return this._lenght;
			}
		}

		// Token: 0x170002BE RID: 702
		public T this[int index]
		{
			get
			{
				return this.Data[(this._firstindex + index) % this._size];
			}
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x000CFA30 File Offset: 0x000CDC30
		private void FirstInc()
		{
			this._firstindex++;
			if (this._firstindex >= this._size)
			{
				this._firstindex = 0;
			}
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x000CFA64 File Offset: 0x000CDC64
		public bool RemoveFromFirstTo(int position)
		{
			if (position < 0)
			{
				return false;
			}
			if (position > this._lenght)
			{
				return false;
			}
			this._firstindex += position;
			this._firstindex %= this._size;
			this._lenght -= position;
			return true;
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x000CFAB8 File Offset: 0x000CDCB8
		public bool RemoveFromLastTo(int position)
		{
			if (position < 0)
			{
				return false;
			}
			if (position > this._lenght)
			{
				return false;
			}
			this._lenght = position;
			return true;
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x000CFADC File Offset: 0x000CDCDC
		public void Add(T val)
		{
			this.Data[(this._firstindex + this._lenght) % this._size].Clone(val);
			if (this._lenght == this._size)
			{
				this.FirstInc();
			}
			this._lenght++;
			if (this._lenght > this._size)
			{
				this._lenght = this._size;
			}
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x000CFB58 File Offset: 0x000CDD58
		public void Clear()
		{
			this._firstindex = 0;
			this._lenght = 0;
		}

		// Token: 0x04001640 RID: 5696
		private int _firstindex;

		// Token: 0x04001641 RID: 5697
		private int _size;

		// Token: 0x04001642 RID: 5698
		private int _lenght;

		// Token: 0x04001643 RID: 5699
		public T[] Data;
	}
}
