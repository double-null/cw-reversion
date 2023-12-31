using System;
using UnityEngine;

// Token: 0x0200029F RID: 671
[Serializable]
internal class MoveState : cwNetworkSerializable, ReusableClass<MoveState>
{
	// Token: 0x060012DB RID: 4827 RVA: 0x000CC494 File Offset: 0x000CA694
	public void Serialize(eNetworkStream stream)
	{
		BoolToInt boolToInt = new BoolToInt(0);
		stream.Serialize(ref this.pos);
		stream.Serialize(ref this.euler);
		stream.Serialize(ref this.velocity);
		stream.Serialize(ref this.jumpReducer);
		boolToInt.Push(this.isSprint);
		boolToInt.Push(this.isSeat);
		boolToInt.Push(this.isWalk);
		boolToInt.Push(this.isGrounded);
		boolToInt.Push(this.isFly);
		stream.Serialize(ref boolToInt.Compressed);
		stream.Serialize(ref this.sitTime);
		stream.Serialize(ref this.moveDir);
		stream.Serialize(ref this.hiPos);
		int num = (int)this.jumpState;
		stream.Serialize(ref num);
	}

	// Token: 0x060012DC RID: 4828 RVA: 0x000CC560 File Offset: 0x000CA760
	public void Deserialize(eNetworkStream stream)
	{
		BoolToInt boolToInt = new BoolToInt(5);
		stream.Serialize(ref this.pos);
		stream.Serialize(ref this.euler);
		stream.Serialize(ref this.velocity);
		stream.Serialize(ref this.jumpReducer);
		stream.Serialize(ref boolToInt.Compressed);
		this.isFly = boolToInt.Pop();
		this.isGrounded = boolToInt.Pop();
		this.isWalk = boolToInt.Pop();
		this.isSeat = boolToInt.Pop();
		this.isSprint = boolToInt.Pop();
		stream.Serialize(ref this.sitTime);
		stream.Serialize(ref this.moveDir);
		stream.Serialize(ref this.hiPos);
		int num = 0;
		stream.Serialize(ref num);
		this.jumpState = (JumpState)num;
	}

	// Token: 0x060012DD RID: 4829 RVA: 0x000CC628 File Offset: 0x000CA828
	public void Clear()
	{
		this.pos = Vector3.zero;
		this.euler = new Vector3(270f, 0f, 0f);
		this.velocity = Vector3.zero;
		this.jumpReducer = 1f;
		this.speedReducer = 1f;
		this.sprintDelay = 1f;
		this.isSprint = false;
		this.isWalk = false;
		this.isSeat = false;
		this.isGrounded = false;
		this.isFly = false;
		this.sitTime = 0.1f;
		this.moveDir = Vector3.zero;
		this.hiPos = float.NaN;
		this.jumpState = JumpState.None;
		this.wind = Vector3.zero;
	}

	// Token: 0x060012DE RID: 4830 RVA: 0x000CC6DC File Offset: 0x000CA8DC
	public void Clone(MoveState i)
	{
		this.pos = i.pos;
		this.euler = i.euler;
		this.velocity = i.velocity;
		this.jumpReducer = i.jumpReducer;
		this.speedReducer = i.speedReducer;
		this.sprintDelay = i.sprintDelay;
		this.isSprint = i.isSprint;
		this.isWalk = i.isWalk;
		this.isSeat = i.isSeat;
		this.isGrounded = i.isGrounded;
		this.isFly = i.isFly;
		this.sitTime = i.sitTime;
		this.moveDir = i.moveDir;
		this.hiPos = i.hiPos;
		this.jumpState = i.jumpState;
		this.wind = i.wind;
	}

	// Token: 0x040015AC RID: 5548
	public Vector3 pos = Vector3.zero;

	// Token: 0x040015AD RID: 5549
	public Vector3 euler = new Vector3(270f, 0f, 0f);

	// Token: 0x040015AE RID: 5550
	public Vector3 velocity = Vector3.zero;

	// Token: 0x040015AF RID: 5551
	public float jumpReducer = 1f;

	// Token: 0x040015B0 RID: 5552
	public float speedReducer = 1f;

	// Token: 0x040015B1 RID: 5553
	public float sprintDelay = 1f;

	// Token: 0x040015B2 RID: 5554
	public bool isSprint;

	// Token: 0x040015B3 RID: 5555
	public bool isWalk;

	// Token: 0x040015B4 RID: 5556
	public bool isSeat;

	// Token: 0x040015B5 RID: 5557
	public bool isGrounded;

	// Token: 0x040015B6 RID: 5558
	public bool isFly;

	// Token: 0x040015B7 RID: 5559
	public float sitTime = 0.1f;

	// Token: 0x040015B8 RID: 5560
	public Vector3 moveDir = Vector3.zero;

	// Token: 0x040015B9 RID: 5561
	public float hiPos = float.NaN;

	// Token: 0x040015BA RID: 5562
	public JumpState jumpState = JumpState.None;

	// Token: 0x040015BB RID: 5563
	public Vector3 wind = Vector3.zero;
}
