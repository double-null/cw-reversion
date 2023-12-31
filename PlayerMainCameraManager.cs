using System;
using UnityEngine;

// Token: 0x02000393 RID: 915
internal static class PlayerMainCameraManager
{
	// Token: 0x06001D5D RID: 7517 RVA: 0x00102818 File Offset: 0x00100A18
	public static GameObject GetCamera()
	{
		if (PlayerMainCameraManager._playerMainCamera == null)
		{
			PlayerMainCameraManager._playerMainCamera = SingletoneForm<PoolManager>.Instance["Player Main Camera"].Spawn();
			if (PlayerMainCameraManager._playerMainCamera.transform.childCount != 0)
			{
				GameObject gameObject = PlayerMainCameraManager._playerMainCamera.transform.GetChild(0).gameObject;
				PlayerMainCameraManager._playerMainCamera.camera.cullingMask |= 1280;
				PlayerMainCameraManager._playerMainCamera.camera.nearClipPlane = 0.03f;
				if (gameObject != null)
				{
					ComponentCopyPaste.CopyFromTo(gameObject, PlayerMainCameraManager._playerMainCamera, new Type[]
					{
						typeof(BloomAndLensFlares),
						typeof(DepthOfField34),
						typeof(AntialiasingAsPostEffect),
						typeof(ColorCorrectionCurves),
						typeof(DesaturateEffect),
						typeof(LightenEffect)
					});
				}
				UnityEngine.Object.Destroy(gameObject);
				SunOnGlass.AddTo(PlayerMainCameraManager._playerMainCamera);
			}
		}
		PlayerMainCameraManager._playerMainCamera.SetActive(true);
		return PlayerMainCameraManager._playerMainCamera;
	}

	// Token: 0x06001D5E RID: 7518 RVA: 0x00102934 File Offset: 0x00100B34
	public static void OffCamera()
	{
		PlayerMainCameraManager._playerMainCamera.SetActive(false);
	}

	// Token: 0x04002203 RID: 8707
	private static GameObject _playerMainCamera;
}
