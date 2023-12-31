using System;
using System.Reflection;
using UnityEngine;

// Token: 0x02000071 RID: 113
[AddComponentMenu("Scripts/Engine/Forms")]
internal class Forms : SingletoneComponent<Forms>
{
	// Token: 0x060001F5 RID: 501 RVA: 0x00011528 File Offset: 0x0000F728
	public static object Get(Type mono)
	{
		MonoEvented[] array = SingletoneComponent<Forms>.instance.forms;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].GetType() == mono)
			{
				return array[i];
			}
		}
		return null;
	}

	// Token: 0x1700002D RID: 45
	// (get) Token: 0x060001F6 RID: 502 RVA: 0x00011568 File Offset: 0x0000F768
	public static bool keyboardLock
	{
		get
		{
			return Forms.noSpecLock || (Main.IsGameLoaded && Main.IsDeadOrSpectactor) || !Main.IsGameLoaded || !Main.IsAlive;
		}
	}

	// Token: 0x1700002E RID: 46
	// (get) Token: 0x060001F7 RID: 503 RVA: 0x000115B0 File Offset: 0x0000F7B0
	public static bool noSpecLock
	{
		get
		{
			return (Main.IsGameLoaded && Main.IsShowingMatchResult) || (Main.IsGameLoaded && SingletoneComponent<Forms>.Instance.spectactor.teamChoosing) || (SingletoneComponent<Forms>.Instance.mainGUI.Visible || SingletoneComponent<Forms>.Instance.chatGUI.Visible || SingletoneComponent<Forms>.Instance.matchResultGUI.Visible || SingletoneComponent<Forms>.Instance.console.Visible);
		}
	}

	// Token: 0x1700002F RID: 47
	// (get) Token: 0x060001F8 RID: 504 RVA: 0x00011648 File Offset: 0x0000F848
	public static bool chatLock
	{
		get
		{
			return !(SingletoneComponent<Forms>.Instance.chatGUI == null) && SingletoneComponent<Forms>.Instance.chatGUI.Visible;
		}
	}

	// Token: 0x17000030 RID: 48
	// (get) Token: 0x060001F9 RID: 505 RVA: 0x0001167C File Offset: 0x0000F87C
	public static bool mouseLock
	{
		get
		{
			return !(SingletoneComponent<Forms>.Instance.radioGUI == null) && (!SingletoneComponent<Forms>.Instance.tabGUI.Visible || !SingletoneComponent<Forms>.Instance.tabGUI.IsMouseClicked) && !SingletoneComponent<Forms>.Instance.radioGUI.Visible;
		}
	}

	// Token: 0x060001FA RID: 506 RVA: 0x000116DC File Offset: 0x0000F8DC
	[Obfuscation(Exclude = true)]
	private void ShowInterface(object obj)
	{
		EventFactory.Call("HideCreateServer", null);
		EventFactory.Call("HideSettings", null);
		EventFactory.Call("HideCarrier", null);
		EventFactory.Call("HideBankGUI", null);
		EventFactory.Call("ShowMainGUI", null);
		EventFactory.Call("HideMainHelpGUI", null);
		EventFactory.Call("HideHallOfFameGUI", null);
	}

	// Token: 0x060001FB RID: 507 RVA: 0x00011738 File Offset: 0x0000F938
	[Obfuscation(Exclude = true)]
	private void HideInterface(object obj)
	{
		EventFactory.Call("HideMainGUI", null);
		EventFactory.Call("HideSearchGames", null);
		EventFactory.Call("HideCreateServer", null);
		EventFactory.Call("HideSettings", null);
		EventFactory.Call("HideCarrier", null);
		EventFactory.Call("HideBankGUI", null);
		EventFactory.Call("HideMainHelpGUI", null);
		EventFactory.Call("HideHallOfFameGUI", null);
	}

	// Token: 0x060001FC RID: 508 RVA: 0x000117A0 File Offset: 0x0000F9A0
	public static void UpdateForms()
	{
		SingletoneComponent<Forms>.Instance.forms = (MonoEvented[])UnityEngine.Object.FindObjectsOfType(typeof(MonoEvented));
		Array.Sort<MonoEvented>(SingletoneComponent<Forms>.Instance.forms, new Comparison<MonoEvented>(Form.Compare));
	}

	// Token: 0x060001FD RID: 509 RVA: 0x000117E8 File Offset: 0x0000F9E8
	public static void MainInitialize()
	{
		SingletoneComponent<Forms>.Instance.forms = (MonoEvented[])UnityEngine.Object.FindObjectsOfType(typeof(MonoEvented));
		MonoEvented[] array = SingletoneComponent<Forms>.instance.forms;
		for (int i = 0; i < array.Length; i++)
		{
			try
			{
				array[i].MainInitialize();
			}
			catch (Exception e)
			{
				global::Console.exception(e);
			}
		}
		Array.Sort<MonoEvented>(SingletoneComponent<Forms>.Instance.forms, new Comparison<MonoEvented>(Form.Compare));
		Forms.Register();
	}

	// Token: 0x060001FE RID: 510 RVA: 0x00011888 File Offset: 0x0000FA88
	public static void OnInitialized()
	{
		MonoEvented[] array = SingletoneComponent<Forms>.instance.forms;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] is SpectactorGUI)
			{
				SingletoneComponent<Forms>.instance.spectactor = (SpectactorGUI)array[i];
			}
			if (array[i] is ChatGUI)
			{
				SingletoneComponent<Forms>.instance.chatGUI = (ChatGUI)array[i];
			}
			if (array[i] is global::Console)
			{
				SingletoneComponent<Forms>.instance.console = (global::Console)array[i];
			}
			if (array[i] is MainGUI)
			{
				SingletoneComponent<Forms>.instance.mainGUI = (MainGUI)array[i];
			}
			if (array[i] is MatchResultGUI)
			{
				SingletoneComponent<Forms>.instance.matchResultGUI = (MatchResultGUI)array[i];
			}
			if (array[i] is RadioGUI)
			{
				SingletoneComponent<Forms>.instance.radioGUI = (RadioGUI)array[i];
			}
			if (array[i] is TabGUI)
			{
				SingletoneComponent<Forms>.instance.tabGUI = (TabGUI)array[i];
			}
			array[i].OnInitialized();
		}
	}

	// Token: 0x060001FF RID: 511 RVA: 0x00011998 File Offset: 0x0000FB98
	public static void OnConnected()
	{
		MonoEvented[] array = SingletoneComponent<Forms>.instance.forms;
		Array.Sort<MonoEvented>(array, new Comparison<MonoEvented>(Form.Compare));
		for (int i = 0; i < array.Length; i++)
		{
			array[i].OnConnected();
		}
	}

	// Token: 0x06000200 RID: 512 RVA: 0x000119E0 File Offset: 0x0000FBE0
	public static void OnDisconnect()
	{
		MonoEvented[] array = SingletoneComponent<Forms>.instance.forms;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].OnDisconnect();
		}
	}

	// Token: 0x06000201 RID: 513 RVA: 0x00011A14 File Offset: 0x0000FC14
	public static void OnLevelLoaded()
	{
		Forms.UpdateForms();
		MonoEvented[] array = SingletoneComponent<Forms>.instance.forms;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].OnLevelLoaded();
		}
	}

	// Token: 0x06000202 RID: 514 RVA: 0x00011A50 File Offset: 0x0000FC50
	public static void OnLevelUnloaded()
	{
		Forms.UpdateForms();
		MonoEvented[] array = SingletoneComponent<Forms>.instance.forms;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].OnLevelUnloaded();
		}
	}

	// Token: 0x06000203 RID: 515 RVA: 0x00011A8C File Offset: 0x0000FC8C
	public static void OnQuit()
	{
		MonoEvented[] array = SingletoneComponent<Forms>.instance.forms;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].OnQuit();
		}
	}

	// Token: 0x06000204 RID: 516 RVA: 0x00011AC0 File Offset: 0x0000FCC0
	public static void Register()
	{
		EventFactory.Register("ShowInterface", SingletoneComponent<Forms>.instance);
		EventFactory.Register("HideInterface", SingletoneComponent<Forms>.instance);
	}

	// Token: 0x06000205 RID: 517 RVA: 0x00011AE0 File Offset: 0x0000FCE0
	public static void MasterGUI()
	{
		if (SingletoneComponent<Forms>.Instance.mainGUI)
		{
			SingletoneComponent<Forms>.instance.mainGUI.WorkWithFocus(SingletoneComponent<Forms>.instance.forms);
		}
		for (int i = 0; i < SingletoneComponent<Forms>.instance.forms.Length; i++)
		{
			if (SingletoneComponent<Forms>.instance.forms[i].isRendering)
			{
				SingletoneComponent<Forms>.instance.forms[i].MasterGUI();
			}
		}
	}

	// Token: 0x06000206 RID: 518 RVA: 0x00011B60 File Offset: 0x0000FD60
	public static void GameGUI()
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
		MonoEvented[] array = SingletoneComponent<Forms>.instance.forms;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].isRendering)
			{
				array[i].GameGUI();
			}
		}
	}

	// Token: 0x06000207 RID: 519 RVA: 0x00011BAC File Offset: 0x0000FDAC
	public static void PopupInterfaceGUI()
	{
		PopupGUI popupGUI = null;
		for (int i = 0; i < SingletoneComponent<Forms>.instance.forms.Length; i++)
		{
			MonoEvented monoEvented = SingletoneComponent<Forms>.instance.forms[i];
			PopupGUI popupGUI2 = monoEvented as PopupGUI;
			if (!(popupGUI2 == null))
			{
				popupGUI = popupGUI2;
				break;
			}
		}
		if (popupGUI != null)
		{
			popupGUI.InterfaceGUI();
		}
	}

	// Token: 0x06000208 RID: 520 RVA: 0x00011C18 File Offset: 0x0000FE18
	public static void InterfaceGUI()
	{
		MonoEvented[] array = SingletoneComponent<Forms>.instance.forms;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].isRendering && (array[i] as Form).Visible)
			{
				array[i].InterfaceGUI();
			}
		}
	}

	// Token: 0x06000209 RID: 521 RVA: 0x00011C6C File Offset: 0x0000FE6C
	public static void PreGUI()
	{
		MonoEvented[] array = SingletoneComponent<Forms>.instance.forms;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].PreGUI();
		}
	}

	// Token: 0x0600020A RID: 522 RVA: 0x00011CA0 File Offset: 0x0000FEA0
	public static void LateGUI()
	{
		MonoEvented[] array = SingletoneComponent<Forms>.instance.forms;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].isRendering && (array[i] as Form).Visible)
			{
				array[i].LateGUI();
			}
		}
	}

	// Token: 0x0600020B RID: 523 RVA: 0x00011CF4 File Offset: 0x0000FEF4
	public static void OnUpdate()
	{
		MonoEvented[] array = SingletoneComponent<Forms>.instance.forms;
		bool isGameLoaded = Main.IsGameLoaded;
		for (int i = 0; i < array.Length; i++)
		{
			if (!array[i].isGameHandler || isGameLoaded)
			{
				if (array[i].isUpdating)
				{
					array[i].OnUpdate();
				}
			}
		}
	}

	// Token: 0x0600020C RID: 524 RVA: 0x00011D54 File Offset: 0x0000FF54
	public static void OnFixedUpdate()
	{
		for (int i = 0; i < SingletoneComponent<Forms>.instance.forms.Length; i++)
		{
			if (!SingletoneComponent<Forms>.instance.forms[i].isGameHandler || Main.IsGameLoaded)
			{
				if (SingletoneComponent<Forms>.instance.forms[i].isUpdating)
				{
					SingletoneComponent<Forms>.instance.forms[i].OnFixedUpdate();
				}
			}
		}
	}

	// Token: 0x0600020D RID: 525 RVA: 0x00011DCC File Offset: 0x0000FFCC
	public static void OnLateUpdate()
	{
		MonoEvented[] array = SingletoneComponent<Forms>.instance.forms;
		bool isGameLoaded = Main.IsGameLoaded;
		for (int i = 0; i < array.Length; i++)
		{
			if (!array[i].isGameHandler || isGameLoaded)
			{
				if (array[i].isUpdating)
				{
					array[i].OnLateUpdate();
				}
			}
		}
	}

	// Token: 0x0600020E RID: 526 RVA: 0x00011E2C File Offset: 0x0001002C
	public static void OnSpawn()
	{
		MonoEvented[] array = SingletoneComponent<Forms>.instance.forms;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].OnSpawn();
		}
	}

	// Token: 0x0600020F RID: 527 RVA: 0x00011E60 File Offset: 0x00010060
	public static void OnDie()
	{
		MonoEvented[] array = SingletoneComponent<Forms>.instance.forms;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].OnDie();
		}
	}

	// Token: 0x06000210 RID: 528 RVA: 0x00011E94 File Offset: 0x00010094
	public static void OnRoundStart()
	{
		MonoEvented[] array = SingletoneComponent<Forms>.instance.forms;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].OnRoundStart();
		}
	}

	// Token: 0x06000211 RID: 529 RVA: 0x00011EC8 File Offset: 0x000100C8
	public static void OnRoundEnd()
	{
		MonoEvented[] array = SingletoneComponent<Forms>.instance.forms;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].OnRoundEnd();
		}
	}

	// Token: 0x06000212 RID: 530 RVA: 0x00011EFC File Offset: 0x000100FC
	public static void OnMatchStart()
	{
		MonoEvented[] array = SingletoneComponent<Forms>.instance.forms;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].OnMatchStart();
		}
	}

	// Token: 0x06000213 RID: 531 RVA: 0x00011F30 File Offset: 0x00010130
	public static void OnMatchEnd()
	{
		MonoEvented[] array = SingletoneComponent<Forms>.instance.forms;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].OnMatchEnd();
		}
	}

	// Token: 0x0400026B RID: 619
	private MonoEvented[] forms;

	// Token: 0x0400026C RID: 620
	private MainGUI mainGUI;

	// Token: 0x0400026D RID: 621
	private ChatGUI chatGUI;

	// Token: 0x0400026E RID: 622
	private MatchResultGUI matchResultGUI;

	// Token: 0x0400026F RID: 623
	private global::Console console;

	// Token: 0x04000270 RID: 624
	private SpectactorGUI spectactor;

	// Token: 0x04000271 RID: 625
	private RadioGUI radioGUI;

	// Token: 0x04000272 RID: 626
	private TabGUI tabGUI;
}
