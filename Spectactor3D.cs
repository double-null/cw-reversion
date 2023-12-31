using System;
using System.Reflection;
using UnityEngine;

// Token: 0x020002B5 RID: 693
[AddComponentMenu("Scripts/Game/Foundation/Spectactor3D")]
internal class Spectactor3D : PoolableBehaviour
{
	// Token: 0x06001371 RID: 4977 RVA: 0x000D0C64 File Offset: 0x000CEE64
	public override void OnPoolSpawn()
	{
		this.position = base.transform.localPosition;
		this.euler = base.transform.localEulerAngles;
		base.OnPoolSpawn();
	}

	// Token: 0x06001372 RID: 4978 RVA: 0x000D0C9C File Offset: 0x000CEE9C
	public override void OnPoolDespawn()
	{
		base.transform.localPosition = this.position;
		base.transform.localEulerAngles = this.euler;
		base.enabled = false;
		this.watched = null;
		this.NPC_Neck = null;
		this.camEuler = new Vector3(315f, 0f, 0f);
		this.targetPoint = Vector3.zero;
		this.camRad = 2f;
		this.camRaycastRad = 5f;
		this.camMaxRad = 5f;
		base.OnPoolDespawn();
	}

	// Token: 0x06001373 RID: 4979 RVA: 0x000D0D2C File Offset: 0x000CEF2C
	[Obfuscation(Exclude = true)]
	public void OnEntityDead()
	{
		SpectactorGUI.I.camSpectator.next();
	}

	// Token: 0x06001374 RID: 4980 RVA: 0x000D0D40 File Offset: 0x000CEF40
	public void LookAt(EntityNetPlayer tr)
	{
		if (!Peer.ClientGame.LocalPlayer.IsDeadOrSpectactor)
		{
			return;
		}
		base.CancelInvoke();
		this.refreshOnce = true;
		base.enabled = true;
		this.watched = tr;
		this.NPC_Neck = Utility.FindHierarchy(this.watched.transform, "NPC_Neck");
		if (this.NPC_Neck != null)
		{
			this.targetPoint = this.NPC_Neck.position;
		}
	}

	// Token: 0x06001375 RID: 4981 RVA: 0x000D0DBC File Offset: 0x000CEFBC
	private void Update()
	{
		if (!Peer.ClientGame.LocalPlayer.IsDeadOrSpectactor)
		{
			return;
		}
		if (this.watched.NeckPosition == Vector3.zero || this.watched.IsDeadOrSpectactor)
		{
			this.NPC_Neck = null;
		}
		if (this.camRad < this.camRaycastRad)
		{
			this.camMaxRad -= Input.GetAxis("Mouse ScrollWheel") * 100f * Time.deltaTime;
			if (this.camMaxRad < 0.5f)
			{
				this.camMaxRad = 0.5f;
			}
			if (this.camMaxRad > 10f)
			{
				this.camMaxRad = 10f;
			}
			if (this.camRad > this.camMaxRad)
			{
				this.camRad = this.camMaxRad;
			}
			if (this.camRad < this.camMaxRad)
			{
				this.camRad += Time.deltaTime * 10f;
			}
		}
		if (Input.GetMouseButton(1))
		{
			float num = Input.GetAxis("Mouse X");
			float num2 = Input.GetAxis("Mouse Y");
			if (Forms.noSpecLock)
			{
				num = 0f;
				num2 = 0f;
			}
			if (Main.UserInfo.settings.binds.invertMouse)
			{
				num2 *= -1f;
			}
			this.camEuler.y = this.camEuler.y + Main.UserInfo.settings.binds.sens * num * 270f * Time.deltaTime;
			this.camEuler.x = this.camEuler.x - Main.UserInfo.settings.binds.sens * num2 * 270f * Time.deltaTime;
			if (this.camEuler.x < 180f)
			{
				this.camEuler.x = 180f;
			}
			if (this.camEuler.x > 360f)
			{
				this.camEuler.x = 360f;
			}
		}
	}

	// Token: 0x06001376 RID: 4982 RVA: 0x000D0FCC File Offset: 0x000CF1CC
	private void LateUpdate()
	{
		if (!Peer.ClientGame.LocalPlayer.IsDeadOrSpectactor)
		{
			return;
		}
		Quaternion rotation = Quaternion.Euler(this.camEuler);
		Vector3 direction = rotation * Vector3.up;
		Vector3 b = rotation * Vector3.left * 0.25f;
		Vector3 b2 = rotation * Vector3.forward * 0.25f;
		if (this.NPC_Neck != null)
		{
			this.targetPoint = this.NPC_Neck.position;
		}
		else if (this.refreshOnce)
		{
			base.Invoke("OnEntityDead", this.changeCamDelay);
			this.refreshOnce = false;
		}
		RaycastHit raycastHit;
		if (Physics.Raycast(new Ray(this.targetPoint, direction), out raycastHit, 1000f, PhysicsUtility.level_layers) && Physics.Raycast(new Ray(this.targetPoint + b, direction), 1000f, PhysicsUtility.level_layers) && Physics.Raycast(new Ray(this.targetPoint - b, direction), 1000f, PhysicsUtility.level_layers) && Physics.Raycast(new Ray(this.targetPoint + b2, direction), 1000f, PhysicsUtility.level_layers) && Physics.Raycast(new Ray(this.targetPoint - b2, direction), 1000f, PhysicsUtility.level_layers))
		{
			this.camRaycastRad = (raycastHit.point - this.targetPoint).magnitude * 0.75f;
			if (this.camRaycastRad < this.camRad)
			{
				if (this.camBringCloserTime < 0f)
				{
					this.camBringCloserTime = Time.time + 0.3f;
				}
				else if (this.camBringCloserTime < Time.time)
				{
					this.camRad = this.camRaycastRad + 0.1f;
				}
			}
			else
			{
				this.camBringCloserTime = -1f;
			}
		}
		else
		{
			this.camBringCloserTime = -1f;
			this.camRaycastRad = 1000f;
		}
		base.transform.position = this.targetPoint + Quaternion.Euler(this.camEuler) * Vector3.up * this.camRad;
		base.transform.LookAt(this.targetPoint);
	}

	// Token: 0x0400168F RID: 5775
	private float changeCamDelay = 3f;

	// Token: 0x04001690 RID: 5776
	[HideInInspector]
	public EntityNetPlayer watched;

	// Token: 0x04001691 RID: 5777
	private Transform NPC_Neck;

	// Token: 0x04001692 RID: 5778
	private Vector3 camEuler = new Vector3(315f, 0f, 0f);

	// Token: 0x04001693 RID: 5779
	private Vector3 targetPoint = Vector3.zero;

	// Token: 0x04001694 RID: 5780
	private float camRad = 2f;

	// Token: 0x04001695 RID: 5781
	private float camRaycastRad = 5f;

	// Token: 0x04001696 RID: 5782
	private float camMaxRad = 5f;

	// Token: 0x04001697 RID: 5783
	private Vector3 position;

	// Token: 0x04001698 RID: 5784
	private Vector3 euler;

	// Token: 0x04001699 RID: 5785
	private bool refreshOnce;

	// Token: 0x0400169A RID: 5786
	private float camBringCloserTime;
}
