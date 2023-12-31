using System;
using UnityEngine;

// Token: 0x02000392 RID: 914
internal class PhysicsUtility
{
	// Token: 0x17000847 RID: 2119
	// (get) Token: 0x06001D57 RID: 7511 RVA: 0x001016D8 File Offset: 0x000FF8D8
	public static int level_layers
	{
		get
		{
			return 1 << LayerMask.NameToLayer("physics") | 1 << LayerMask.NameToLayer("indoor") | 1 << LayerMask.NameToLayer("indoor_culling") | 1 << LayerMask.NameToLayer("outdoor") | 1 << LayerMask.NameToLayer("outdoor_culling") | 1 << LayerMask.NameToLayer("terrain");
		}
	}

	// Token: 0x17000848 RID: 2120
	// (get) Token: 0x06001D58 RID: 7512 RVA: 0x00101744 File Offset: 0x000FF944
	public static int physics_layers
	{
		get
		{
			return 1 << LayerMask.NameToLayer("physics") | 1 << LayerMask.NameToLayer("physics_move") | 1 << LayerMask.NameToLayer("terrain");
		}
	}

	// Token: 0x06001D59 RID: 7513 RVA: 0x00101778 File Offset: 0x000FF978
	private static void GenerateComplementBasis(ref Vector3 vec0, ref Vector3 vec1, ref Vector3 vec2)
	{
		if (Mathf.Abs(vec2[0]) >= Mathf.Abs(vec2[1]))
		{
			float num = 1f / Mathf.Sqrt(vec2[0] * vec2[0] + vec2[2] * vec2[2]);
			vec0[0] = -vec2[2] * num;
			vec0[1] = 0f;
			vec0[2] = vec2[0] * num;
			vec1[0] = vec2[1] * vec0[2];
			vec1[1] = vec2[2] * vec0[0] - vec2[0] * vec0[2];
			vec1[2] = -vec2[1] * vec0[0];
		}
		else
		{
			float num = 1f / Mathf.Sqrt(vec2[1] * vec2[1] + vec2[2] * vec2[2]);
			vec0[0] = 0f;
			vec0[1] = vec2[2] * num;
			vec0[2] = -vec2[1] * num;
			vec1[0] = vec2[1] * vec0[2] - vec2[2] * vec0[1];
			vec1[1] = -vec2[0] * vec0[2];
			vec1[2] = vec2[0] * vec0[1];
		}
	}

	// Token: 0x06001D5A RID: 7514 RVA: 0x00101900 File Offset: 0x000FFB00
	public static RaycastHit[] SphereCastAll(Ray ray, Vector3 center, float radius)
	{
		float[] array = new float[2];
		Vector3[] array2 = new Vector3[2];
		RaycastHit[] array3 = new RaycastHit[0];
		Vector3 vector = ray.origin - center;
		float num = Vector3.Dot(vector, vector) - radius * radius;
		float num2;
		float num3;
		if (num <= 0f)
		{
			num2 = Vector3.Dot(ray.direction, vector);
			num3 = num2 * num2 - num;
			float num4 = Mathf.Sqrt(num3);
			array[0] = -num2 + num4;
			array2[0] = ray.origin + array[0] * ray.direction;
			array3 = new RaycastHit[1];
			array3[0].point = array2[0];
			return array3;
		}
		num2 = Vector3.Dot(ray.direction, vector);
		if (num2 >= 0f)
		{
			return array3;
		}
		num3 = num2 * num2 - num;
		int num5;
		if (num3 < 0f)
		{
			num5 = 0;
		}
		else if (num3 >= 1E-06f)
		{
			float num4 = Mathf.Sqrt(num3);
			array[0] = -num2 - num4;
			array[1] = -num2 + num4;
			array2[0] = ray.origin + array[0] * ray.direction;
			array2[1] = ray.origin + array[1] * ray.direction;
			num5 = 2;
		}
		else
		{
			array[0] = -num2;
			array2[0] = ray.origin + array[0] * ray.direction;
			num5 = 1;
		}
		array3 = new RaycastHit[num5];
		for (int i = 0; i < num5; i++)
		{
			array3[i].point = array2[i];
		}
		return array3;
	}

	// Token: 0x06001D5B RID: 7515 RVA: 0x00101AE4 File Offset: 0x000FFCE4
	public static eRaycastHit[] CapsuleCastAll(Ray ray, CapsuleCollider capsule, Vector3 position, Quaternion rotation)
	{
		Vector3 b = position + rotation * capsule.center;
		Vector3 point = Vector3.up;
		if (capsule.direction == 0)
		{
			point = Vector3.left;
		}
		if (capsule.direction == 1)
		{
			point = Vector3.up;
		}
		if (capsule.direction == 2)
		{
			point = Vector3.forward;
		}
		Vector3 vector = rotation * point;
		float radius = capsule.radius;
		float height = capsule.height;
		float[] array = new float[2];
		Vector3 zero = Vector3.zero;
		Vector3 zero2 = Vector3.zero;
		Vector3 lhs = vector;
		PhysicsUtility.GenerateComplementBasis(ref zero, ref zero2, ref lhs);
		float num = radius * radius;
		float num2 = Mathf.Max(height / 2f - radius, 0f);
		Vector3 rhs = ray.origin - b;
		Vector3 vector2 = new Vector3(Vector3.Dot(zero, rhs), Vector3.Dot(zero2, rhs), Vector3.Dot(lhs, rhs));
		float num3 = Vector3.Dot(lhs, ray.direction);
		if (Mathf.Abs(num3) >= 0.999999f)
		{
			float num4 = num - vector2.x * vector2.x - vector2.y * vector2.y;
			if (num4 < 0f)
			{
				return new eRaycastHit[0];
			}
			float num5 = Mathf.Sqrt(num4) + num2;
			if (num3 > 0f)
			{
				array[0] = -vector2.z - num5;
				array[1] = -vector2.z + num5;
			}
			else
			{
				array[0] = vector2.z - num5;
				array[1] = vector2.z + num5;
			}
			eRaycastHit[] array2;
			if (array[0] <= 0f)
			{
				array2 = new eRaycastHit[1];
				array2[0].point = ray.origin + ray.direction * array[1];
				array2[0].distance = array[1];
			}
			else if (array[1] <= 0f)
			{
				array2 = new eRaycastHit[1];
				array2[0].point = ray.origin + ray.direction * array[0];
				array2[0].distance = array[0];
			}
			else
			{
				array2 = new eRaycastHit[2];
				array2[0].point = ray.origin + ray.direction * array[0];
				array2[1].point = ray.origin + ray.direction * array[1];
				array2[0].distance = array[0];
				array2[1].distance = array[1];
			}
			return array2;
		}
		else
		{
			Vector3 vector3 = new Vector3(Vector3.Dot(zero, ray.direction), Vector3.Dot(zero2, ray.direction), num3);
			float num6 = vector2.x * vector2.x + vector2.y * vector2.y - num;
			float num7 = vector2.x * vector3.x + vector2.y * vector3.y;
			float num8 = vector3.x * vector3.x + vector3.y * vector3.y;
			float num9 = num7 * num7 - num6 * num8;
			if (num9 < 0f)
			{
				return new eRaycastHit[0];
			}
			int num10 = 0;
			if (num9 > 1E-06f)
			{
				float num11 = Mathf.Sqrt(num9);
				float num12 = 1f / num8;
				float num13 = (-num7 - num11) * num12;
				float num14 = vector2.z + num13 * vector3.z;
				if (Mathf.Abs(num14) <= num2)
				{
					array[num10++] = num13;
				}
				num13 = (-num7 + num11) * num12;
				num14 = vector2.z + num13 * vector3.z;
				if (Mathf.Abs(num14) <= num2)
				{
					array[num10++] = num13;
				}
				if (num10 == 2)
				{
					eRaycastHit[] array3 = new eRaycastHit[2];
					array3[0].point = ray.origin + ray.direction * array[0];
					array3[1].point = ray.origin + ray.direction * array[1];
					array3[0].distance = array[0];
					array3[1].distance = array[1];
					return array3;
				}
			}
			else
			{
				float num13 = -num7 / num8;
				float num14 = vector2.z + num13 * vector3.z;
				if (Mathf.Abs(num14) <= num2)
				{
					array[0] = num13;
					eRaycastHit[] array4 = new eRaycastHit[1];
					array4[0].point = ray.origin + ray.direction * array[0];
					return array4;
				}
			}
			float num15 = vector2.z + num2;
			num7 += num15 * vector3.z;
			num6 += num15 * num15;
			num9 = num7 * num7 - num6;
			if (num9 > 1E-06f)
			{
				float num11 = Mathf.Sqrt(num9);
				float num13 = -num7 - num11;
				float num14 = vector2.z + num13 * vector3.z;
				if (num14 <= -num2)
				{
					array[num10++] = num13;
					if (num10 == 2)
					{
						if (array[0] > array[1])
						{
							float num16 = array[0];
							array[0] = array[1];
							array[1] = num16;
						}
						eRaycastHit[] array5 = new eRaycastHit[2];
						array5[0].point = ray.origin + ray.direction * array[0];
						array5[1].point = ray.origin + ray.direction * array[1];
						array5[0].distance = array[0];
						array5[1].distance = array[1];
						return array5;
					}
				}
				num13 = -num7 + num11;
				num14 = vector2.z + num13 * vector3.z;
				if (num14 <= -num2)
				{
					array[num10++] = num13;
					if (num10 == 2)
					{
						if (array[0] > array[1])
						{
							float num17 = array[0];
							array[0] = array[1];
							array[1] = num17;
						}
						eRaycastHit[] array6 = new eRaycastHit[2];
						array6[0].point = ray.origin + ray.direction * array[0];
						array6[1].point = ray.origin + ray.direction * array[1];
						array6[0].distance = array[0];
						array6[1].distance = array[1];
						return array6;
					}
				}
			}
			else if (Mathf.Abs(num9) <= 1E-06f)
			{
				float num13 = -num7;
				float num14 = vector2.z + num13 * vector3.z;
				if (num14 <= -num2)
				{
					array[num10++] = num13;
					if (num10 == 2)
					{
						if (array[0] > array[1])
						{
							float num18 = array[0];
							array[0] = array[1];
							array[1] = num18;
						}
						eRaycastHit[] array7 = new eRaycastHit[2];
						array7[0].point = ray.origin + ray.direction * array[0];
						array7[1].point = ray.origin + ray.direction * array[1];
						array7[0].distance = array[0];
						array7[1].distance = array[1];
						return array7;
					}
				}
			}
			num7 -= 2f * num2 * vector3.z;
			num6 -= 4f * num2 * vector2.z;
			num9 = num7 * num7 - num6;
			if (num9 > 1E-06f)
			{
				float num11 = Mathf.Sqrt(num9);
				float num13 = -num7 - num11;
				float num14 = vector2.z + num13 * vector3.z;
				if (num14 >= num2)
				{
					array[num10++] = num13;
					if (num10 == 2)
					{
						if (array[0] > array[1])
						{
							float num19 = array[0];
							array[0] = array[1];
							array[1] = num19;
						}
						eRaycastHit[] array8 = new eRaycastHit[2];
						array8[0].point = ray.origin + ray.direction * array[0];
						array8[1].point = ray.origin + ray.direction * array[1];
						array8[0].distance = array[0];
						array8[1].distance = array[1];
						return array8;
					}
				}
				num13 = -num7 + num11;
				num14 = vector2.z + num13 * vector3.z;
				if (num14 >= num2)
				{
					array[num10++] = num13;
					if (num10 == 2)
					{
						if (array[0] > array[1])
						{
							float num20 = array[0];
							array[0] = array[1];
							array[1] = num20;
						}
						eRaycastHit[] array9 = new eRaycastHit[2];
						array9[0].point = ray.origin + ray.direction * array[0];
						array9[1].point = ray.origin + ray.direction * array[1];
						array9[0].distance = array[0];
						array9[1].distance = array[1];
						return array9;
					}
				}
			}
			else if (Mathf.Abs(num9) <= 1E-06f)
			{
				float num13 = -num7;
				float num14 = vector2.z + num13 * vector3.z;
				if (num14 >= num2)
				{
					array[num10++] = num13;
					if (num10 == 2)
					{
						if (array[0] > array[1])
						{
							float num21 = array[0];
							array[0] = array[1];
							array[1] = num21;
						}
						eRaycastHit[] array10 = new eRaycastHit[2];
						array10[0].point = ray.origin + ray.direction * array[0];
						array10[1].point = ray.origin + ray.direction * array[1];
						array10[0].distance = array[0];
						array10[1].distance = array[1];
						return array10;
					}
				}
			}
			eRaycastHit[] array11 = new eRaycastHit[num10];
			for (int i = 0; i < num10; i++)
			{
				array11[i].point = ray.origin + ray.direction * array[i];
				array11[i].distance = array[i];
			}
			return array11;
		}
	}

	// Token: 0x06001D5C RID: 7516 RVA: 0x001025FC File Offset: 0x001007FC
	public static void SetCollisionMatrix()
	{
		Debug.LogWarning("Should not use this!!!");
		for (int i = 0; i < 32; i++)
		{
			for (int j = 0; j < 32; j++)
			{
				Physics.IgnoreLayerCollision(i, j, true);
			}
		}
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("client_capsules"), LayerMask.NameToLayer("terrain"), false);
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("client_capsules"), LayerMask.NameToLayer("physics"), false);
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("client_capsules"), LayerMask.NameToLayer("physics_move"), false);
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("server_capsules"), LayerMask.NameToLayer("terrain"), false);
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("server_capsules"), LayerMask.NameToLayer("physics"), false);
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("server_capsules"), LayerMask.NameToLayer("physics_move"), false);
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("client_ragdoll"), LayerMask.NameToLayer("physics"), false);
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("client_ragdoll"), LayerMask.NameToLayer("indoor"), false);
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("client_ragdoll"), LayerMask.NameToLayer("indoor_culling"), false);
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("client_ragdoll"), LayerMask.NameToLayer("outdoor"), false);
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("client_ragdoll"), LayerMask.NameToLayer("outdoor_culling"), false);
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("client_ragdoll"), LayerMask.NameToLayer("terrain"), false);
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("server_ragdoll"), LayerMask.NameToLayer("physics"), false);
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("server_ragdoll"), LayerMask.NameToLayer("indoor"), false);
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("server_ragdoll"), LayerMask.NameToLayer("indoor_culling"), false);
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("server_ragdoll"), LayerMask.NameToLayer("outdoor"), false);
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("server_ragdoll"), LayerMask.NameToLayer("outdoor_culling"), false);
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("server_ragdoll"), LayerMask.NameToLayer("terrain"), false);
	}

	// Token: 0x04002202 RID: 8706
	private const float ZERO_TOLERANCE = 1E-06f;
}
