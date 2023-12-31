using System;
using UnityEngine;

// Token: 0x020000E0 RID: 224
internal class CWGUI : MonoBehaviour
{
	// Token: 0x170000C3 RID: 195
	// (get) Token: 0x060005E7 RID: 1511 RVA: 0x0002DAC8 File Offset: 0x0002BCC8
	public static CWGUI p
	{
		get
		{
			return CWGUI.pointer;
		}
	}

	// Token: 0x060005E8 RID: 1512 RVA: 0x0002DAD0 File Offset: 0x0002BCD0
	public static float CalcCentredOffset(float width1, float width2)
	{
		return Mathf.Abs((width1 - width2) / 2f);
	}

	// Token: 0x060005E9 RID: 1513 RVA: 0x0002DAE0 File Offset: 0x0002BCE0
	private void Start()
	{
		if (CWGUI.pointer == null)
		{
			CWGUI.pointer = this;
		}
	}

	// Token: 0x060005EA RID: 1514 RVA: 0x0002DAF8 File Offset: 0x0002BCF8
	public static void RotateGUI(float angle, Vector2 center)
	{
		if (angle == 0f)
		{
			GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one);
		}
		else
		{
			GUIUtility.RotateAroundPivot(angle, center);
		}
	}

	// Token: 0x060005EB RID: 1515 RVA: 0x0002DB38 File Offset: 0x0002BD38
	public static void PictureCentered(Vector2 pos, Texture2D picture, Vector2 scale)
	{
		GUI.DrawTexture(new Rect(pos.x - (float)picture.width * scale.x / 2f, pos.y - (float)picture.height * scale.y / 2f, (float)picture.width * scale.x, (float)picture.height * scale.y), picture, ScaleMode.ScaleToFit, true);
	}

	// Token: 0x060005EC RID: 1516 RVA: 0x0002DBAC File Offset: 0x0002BDAC
	public static bool Button(Rect position, GUIContent content, GUIStyle style)
	{
		if (GUI.Button(position, content, style))
		{
			Audio.Play(Vector3.zero, MainGUI.Instance.clickSoundPrefab, -1f, -1f);
			return true;
		}
		return false;
	}

	// Token: 0x060005ED RID: 1517 RVA: 0x0002DBE0 File Offset: 0x0002BDE0
	public static bool Button(Rect position, string text, GUIStyle style)
	{
		CWGUI.tmpContent.text = text;
		if (GUI.Button(position, CWGUI.tmpContent, style))
		{
			Audio.Play(Vector3.zero, MainGUI.Instance.clickSoundPrefab, -1f, -1f);
			return true;
		}
		return false;
	}

	// Token: 0x060005EE RID: 1518 RVA: 0x0002DC2C File Offset: 0x0002BE2C
	private void OnGUI()
	{
	}

	// Token: 0x040005CF RID: 1487
	private static GUIContent tmpContent = new GUIContent();

	// Token: 0x040005D0 RID: 1488
	private static CWGUI pointer = null;

	// Token: 0x040005D1 RID: 1489
	public GUIStyle spawnChooseButton = new GUIStyle();

	// Token: 0x040005D2 RID: 1490
	public GUIStyle spawnChooseButtonPushed = new GUIStyle();

	// Token: 0x040005D3 RID: 1491
	public GUIStyle blackBackText = new GUIStyle();

	// Token: 0x040005D4 RID: 1492
	public GUIStyle menuButton = new GUIStyle();

	// Token: 0x040005D5 RID: 1493
	public GUIStyle rifleButton = new GUIStyle();

	// Token: 0x040005D6 RID: 1494
	public GUIStyle spawnMapBackGround = new GUIStyle();

	// Token: 0x040005D7 RID: 1495
	public GUIStyle standartWhiteKorataki = new GUIStyle();

	// Token: 0x040005D8 RID: 1496
	public GUIStyle emptyStile = new GUIStyle();

	// Token: 0x040005D9 RID: 1497
	public GUIStyle startButton = new GUIStyle();

	// Token: 0x040005DA RID: 1498
	public GUIStyle fullscreenButton = new GUIStyle();

	// Token: 0x040005DB RID: 1499
	public GUIStyle mainMenuButton = new GUIStyle();

	// Token: 0x040005DC RID: 1500
	public GUIStyle secondaryWeaponButton = new GUIStyle();

	// Token: 0x040005DD RID: 1501
	public GUIStyle primaryWeaponButton = new GUIStyle();

	// Token: 0x040005DE RID: 1502
	public GUIStyle weaponSetButton = new GUIStyle();

	// Token: 0x040005DF RID: 1503
	public GUIStyle weaponKitButton = new GUIStyle();

	// Token: 0x040005E0 RID: 1504
	public GUIStyle saveWeaponKitButon = new GUIStyle();

	// Token: 0x040005E1 RID: 1505
	public GUIStyle renameWeaponKitButton = new GUIStyle();

	// Token: 0x040005E2 RID: 1506
	public GUIStyle wtaskButton = new GUIStyle();

	// Token: 0x040005E3 RID: 1507
	public GUIStyle wtaskActiveSecondaryButton = new GUIStyle();

	// Token: 0x040005E4 RID: 1508
	public GUIStyle wtaskActivePrimaryButton = new GUIStyle();

	// Token: 0x040005E5 RID: 1509
	public GUIStyle playerInfoButton = new GUIStyle();

	// Token: 0x040005E6 RID: 1510
	public GUIStyle addBalanceButton = new GUIStyle();

	// Token: 0x040005E7 RID: 1511
	public GUIStyle friendInviteButton = new GUIStyle();

	// Token: 0x040005E8 RID: 1512
	public GUIStyle repairButton = new GUIStyle();

	// Token: 0x040005E9 RID: 1513
	public GUIStyle checkboxButton = new GUIStyle();

	// Token: 0x040005EA RID: 1514
	public GUIStyle yesnoButton = new GUIStyle();

	// Token: 0x040005EB RID: 1515
	public GUIStyle closeButton = new GUIStyle();

	// Token: 0x040005EC RID: 1516
	public GUIStyle closeButtonSmall = new GUIStyle();

	// Token: 0x040005ED RID: 1517
	public GUIStyle findGamesMainButton = new GUIStyle();

	// Token: 0x040005EE RID: 1518
	public GUIStyle findGamesCheckboxButton = new GUIStyle();

	// Token: 0x040005EF RID: 1519
	public GUIStyle findGamesRefreshButton = new GUIStyle();

	// Token: 0x040005F0 RID: 1520
	public GUIStyle findGamesConnectButton = new GUIStyle();

	// Token: 0x040005F1 RID: 1521
	public GUIStyle careerMainButton = new GUIStyle();

	// Token: 0x040005F2 RID: 1522
	public GUIStyle careerSkillsMainButton = new GUIStyle();

	// Token: 0x040005F3 RID: 1523
	public GUIStyle careerSkillsButton = new GUIStyle();

	// Token: 0x040005F4 RID: 1524
	public GUIStyle careerSkillsAddBalanceButton = new GUIStyle();

	// Token: 0x040005F5 RID: 1525
	public GUIStyle careerSkillsResetButton = new GUIStyle();

	// Token: 0x040005F6 RID: 1526
	public GUIStyle careerSkillsUnlockButton = new GUIStyle();

	// Token: 0x040005F7 RID: 1527
	public GUIStyle careerSummaryButton = new GUIStyle();

	// Token: 0x040005F8 RID: 1528
	public GUIStyle careerRatingClansButton = new GUIStyle();

	// Token: 0x040005F9 RID: 1529
	public GUIStyle careerRatingSmallButton = new GUIStyle();

	// Token: 0x040005FA RID: 1530
	public GUIStyle careerRatingVotesButton = new GUIStyle();

	// Token: 0x040005FB RID: 1531
	public GUIStyle bankMainButton = new GUIStyle();

	// Token: 0x040005FC RID: 1532
	public GUIStyle bankBuyButton = new GUIStyle();

	// Token: 0x040005FD RID: 1533
	public GUIStyle awardsStyle = new GUIStyle();

	// Token: 0x040005FE RID: 1534
	public GUIStyle healthStyle = new GUIStyle();

	// Token: 0x040005FF RID: 1535
	public GUIStyle standartDNC5714 = new GUIStyle();

	// Token: 0x04000600 RID: 1536
	public GUIStyle standartKorataki18 = new GUIStyle();

	// Token: 0x04000601 RID: 1537
	public GUIStyle standartTahoma9 = new GUIStyle();

	// Token: 0x04000602 RID: 1538
	public GUIStyle smallCheckBox = new GUIStyle();

	// Token: 0x04000603 RID: 1539
	public GUIStyle hardcoreCheckBox = new GUIStyle();

	// Token: 0x04000604 RID: 1540
	public GUIStyle friendsCheckBox = new GUIStyle();

	// Token: 0x04000605 RID: 1541
	public GUIStyle EmptyButton = new GUIStyle();

	// Token: 0x04000606 RID: 1542
	public GUIStyle FavoriteButton = new GUIStyle();

	// Token: 0x04000607 RID: 1543
	public GUIStyle NotFavoriteButton = new GUIStyle();

	// Token: 0x04000608 RID: 1544
	public Texture2D FavoriteIcon;

	// Token: 0x04000609 RID: 1545
	public Texture2D black;

	// Token: 0x0400060A RID: 1546
	public GUIStyle MastCharacteristicStyle = new GUIStyle();

	// Token: 0x0400060B RID: 1547
	public GUIStyle MastWindowHeaderStyle = new GUIStyle();

	// Token: 0x0400060C RID: 1548
	public GUIStyle MastUnlockedMetaLevelIndexStyle = new GUIStyle();

	// Token: 0x0400060D RID: 1549
	public GUIStyle MastLockedMetaLevelIndexStyle = new GUIStyle();

	// Token: 0x0400060E RID: 1550
	public GUIStyle MastProgressBarLabelStyle = new GUIStyle();

	// Token: 0x0400060F RID: 1551
	public GUIStyle MastModDescHeaderStyle = new GUIStyle();

	// Token: 0x04000610 RID: 1552
	public GUIStyle MastModDescContentStyle = new GUIStyle();

	// Token: 0x04000611 RID: 1553
	public GUIStyle MastCurrentModsHeaderStyle = new GUIStyle();

	// Token: 0x04000612 RID: 1554
	public GUIStyle MastCurrentModsNameStyle = new GUIStyle();

	// Token: 0x04000613 RID: 1555
	public GUIStyle MastCurrentModsNotAllowedStyle = new GUIStyle();

	// Token: 0x04000614 RID: 1556
	public GUIStyle MastCharactPosStyle = new GUIStyle();

	// Token: 0x04000615 RID: 1557
	public GUIStyle MastCharactNegStyle = new GUIStyle();

	// Token: 0x04000616 RID: 1558
	public GUIStyle MastCharactOldStyle = new GUIStyle();

	// Token: 0x04000617 RID: 1559
	public GUIStyle MastCharactNewStyle = new GUIStyle();

	// Token: 0x04000618 RID: 1560
	public GUIStyle MastMPCountStyle = new GUIStyle();

	// Token: 0x04000619 RID: 1561
	public GUIStyle MastExpCountStyle = new GUIStyle();

	// Token: 0x0400061A RID: 1562
	public GUIStyle MastMPPriceStyle = new GUIStyle();

	// Token: 0x0400061B RID: 1563
	public GUIStyle MastWTaskProgressStyle = new GUIStyle();

	// Token: 0x0400061C RID: 1564
	public GUIStyle MastUnlockBtnStyle = new GUIStyle();

	// Token: 0x0400061D RID: 1565
	public GUIStyle MastEquipModBtnStyle = new GUIStyle();

	// Token: 0x0400061E RID: 1566
	public GUIStyle MastViewerBtnStyle = new GUIStyle();

	// Token: 0x0400061F RID: 1567
	public GUIStyle MastViewerZoomInBtnStyle = new GUIStyle();

	// Token: 0x04000620 RID: 1568
	public GUIStyle MastViewerZoomOutBtnStyle = new GUIStyle();

	// Token: 0x04000621 RID: 1569
	public GUIStyle MastModBtnStyle = new GUIStyle();

	// Token: 0x04000622 RID: 1570
	public GUIStyle MastSaveBtnStyle = new GUIStyle();

	// Token: 0x04000623 RID: 1571
	public GUIStyle MastFillUpBalanceBtnStyle = new GUIStyle();

	// Token: 0x04000624 RID: 1572
	public GUIStyle MastWtaskBtnStyle = new GUIStyle();
}
