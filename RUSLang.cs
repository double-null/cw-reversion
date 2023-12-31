using System;
using UnityEngine;

// Token: 0x020002FB RID: 763
internal class RUSLang : BaseLanguage
{
	// Token: 0x170007C5 RID: 1989
	// (get) Token: 0x06001A28 RID: 6696 RVA: 0x000EDC20 File Offset: 0x000EBE20
	public override ELanguage Lang
	{
		get
		{
			return ELanguage.RU;
		}
	}

	// Token: 0x06001A29 RID: 6697 RVA: 0x000EDC24 File Offset: 0x000EBE24
	public override void SetLanguage()
	{
		this.CWMainLoading = "Загрузка";
		this.CWMainGlobalInfoLoading = "Загружаются глобальные данные";
		this.CWMainGlobalInfoLoadingFinished = "Глобальные данные успешно загрузились";
		this.CWMainLoginDesc = "Соединяемся с социальной сетью";
		this.CWMainLoginFinishedDesc = "Соединение прошло успешно";
		this.CWMainInitUserDesc = "Соединяемся с базой данных";
		this.CWMainInitUserFinishedDesc = "Соединение прошло успешно";
		this.CWMainLoad = "Загружаем профиль";
		this.CWMainLoadDesc = "Загружаем данные профиля из базы";
		this.CWMainLoadFinishedDesc = "Загрузка прошла успешно";
		this.CWMainLoadError = "Ошибка";
		this.CWMainLoadErrorDesc = "Загрузка профиля не удалось";
		this.CWMainSave = "Сохраняем профиль";
		this.CWMainSaveDesc = "Сохраняем данные профиля в базу";
		this.CWMainSaveFinishedDesc = "Сохранение прошло успешно";
		this.CWMainSaveError = "Ошибка";
		this.CWMainSaveErrorDesc = "Сохранение профиля не удалось";
		this.CWMainWeaponUnlock = "Покупка";
		this.CWMainWeaponUnlockDesc = "Идет доставка снаряжения";
		this.CWMainWeaponUnlockFinishedDesc = "Снаряжение доставлено";
		this.CWMainWeaponUnlockError = "Ошибка";
		this.CWMainWeaponUnlockErrorDesc = "Отказ в доставке";
		this.CWMainSkillUnlock = "Покупка";
		this.CWMainSkillUnlockDesc = "Идет доставка учебных материалов";
		this.CWMainSkillUnlockFinishedDesc = "Учебные материалы доставлены";
		this.CWMainSkillUnlockError = "Ошибка";
		this.CWMainSkillUnlockErrorDesc = "Отказ в доставке";
		this.CWMainLoadRating = "Загружаем рейтинг";
		this.CWMainLoadRatingDesc = "Идет загрузка топ-100";
		this.CWMainLoadRatingFinishedDesc = "Рейтинг загружен";
		this.CWMainLoadRatingError = "Ошибка";
		this.CWMainLoadRatingErrorDesc = "Рейтинг не загружен";
		this.DownloadAdditionalGameDataDesc = "Загружается оружие";
		this.Error = "Ошибка";
		this.Connection = "Соединение";
		this.TryingConnection = "Попытка подключения к серверу";
		this.ConnectionFailed = "Подключение не удалось";
		this.ConnectionFailedOnDelay = "Подключение не удалось. Связь не установлена.";
		this.ConnectionCompleted = "Подключение к серверу прошло удачно";
		this.ServerDisconnectYou = "Сервер разорвал соединение";
		this.FailedToConnectToMasterServer = "Не удалось подключиться к мастер серверу.\nБез мастер сервера не будут видны игры других игроков.\n";
		this.ServerCreation = "Создание сервера";
		this.ServerCreationFailed = "Неудалось создать сервер";
		this.LoadingProfile = "ЗАГРУЖАЕТСЯ ПРОФИЛЬ";
		this.BannerGUIKnife = "нож";
		this.BannerGUIGrenade = "граната";
		this.BannerGUIMortarStrike = "минометный удар";
		this.Push = "Нажмите";
		this.GrenadeThrowMessage1 = "Бандероль";
		this.GrenadeThrowMessage2 = "Своя пошла";
		this.NeedMoreWarrior = "НЕДОСТАТОЧНО БОЙЦОВ ДЛЯ ЗАХВАТА";
		this.CapturingPoint = "ПРОИСХОДИТ ЗАХВАТ ТОЧКИ";
		this.EnemyCapturingYourPoint = "ВАШУ ТОЧКУ ЗАХВАТЫВАЮТ!";
		this.NeutralizePoint = "ПРОИСХОДИТ НЕЙТРАЛИЗАЦИЯ ТОЧКИ";
		this.FriendCaptured = "НАХОДИТСЯ ПОД ВАШИМ КОНТРОЛЕМ";
		this.EnemyCaptured = "НАХОДИТСЯ ПОД ВРАЖЕСКИМ КОНТРОЛЕМ";
		this.PointBearCaptured = "ЗАХВАЧЕНА BEAR";
		this.PointUsecCaptured = "ЗАХВАЧЕНА USEC";
		this.PointPurification = "НЕОБХОДИМА ЗАЧИСТКА ТОЧКИ";
		this.Point = "ТОЧКА";
		this.PointName[0] = "none";
		this.PointName[1] = "БАЗА";
		this.PointName[2] = "МАЯК";
		this.PointName[3] = "СТАНЦИЯ";
		this.PointName[4] = "ТОННЕЛЬ";
		this.PlayerRecieveSonar = "получил сонар";
		this.PlayerRecieveMortar = "получил минометный удар";
		this.PlayerPlacedMarker = "установил маяк";
		this.PlayerDifuseMarker = "обезвредил маяк";
		this.PlayerMakeStormKill = "сделал stormkill";
		this.PlayerMakeProKill = "сделал prokill";
		this.PlayerGainedAchivment = "получил достижение";
		this.PlayerCapturedPoint = "захватил точку";
		this.AutoTeamBalance = "Внимание! В следующем раунде будет проведена автобалансировка команд";
		this.ServerRestarting = "Внимание! Игровая сессия окончена. Сервер будет перезапущен для обеспечения стабильности.";
		this.PlayerConnected = "подключился";
		this.PlayerDisconnected = "отключился";
		this.SpecChangeCam_Spacebar = "Нажмите ПРОБЕЛ для циклической смены камер";
		this.SpecChangeCam_MidleButton = "Нажмите СКМ для смены режима камеры";
		this.SpecPressMToSelectTeam = "Нажмите М для выбора команды";
		this.SpecViewaAt = "ВЫ НАБЛЮДАЕТЕ ЗА:";
		this.SpecChooseTeam = "ВЫБЕРИТЕ КОМАНДУ";
		this.SpecPremium = "PREMIUM";
		this.SpecGamers = " игроков";
		this.SpecSpectator = "НАБЛЮДАТЕЛЬ";
		this.SpecAutobalance = "АВТОРАСПРЕДЕЛЕНИЕ";
		this.SpecBEARWins = "ПОБЕДА BATTLE ENCOUNTER ASSAULT REGIMENT";
		this.SpecUSECWins = "ПОБЕДА UNITED SECURITY";
		this.SpecWin = "ВЫ ВЫИГРАЛИ";
		this.SpecLoose = "ВЫ ПРОИГРАЛИ";
		this.SpecServerIsFull = "Сервер заполнен, ожидайте освобождения места";
		this.SpecRessurectAfter = "Возрождение через: ";
		this.Space = "ПРОБЕЛ";
		this.SpecForCycleCamChanged = "для циклической смены камер";
		this.MMB = "СКМ";
		this.ForCamChanged = "для смены режима камеры";
		this.LMB = "ЛКМ";
		this.SpecForBeginGame = "для вступления в игру";
		this.SpecForChooseTeam = "для выбора команды";
		this.SpecCantChangeTeam = "Вы не можете сейчас выбрать другую команду";
		this.SpecTeamIsOverpowered = "Слишком много бойцов";
		this.SpecWaitForBeginRound = "Ожидайте начала следующего раунда! ";
		this.No = "НЕТ";
		this.NoSmall = "нет";
		this.StartGame = "НАЧАТЬ ИГРУ";
		this.killedVIP = "убил VIP";
		this.VIPInYourTeam = "В вашей команде VIP. Защищайте его любой ценой";
		this.VIPInEnemyTeam = "Во вражеской команде VIP. Убейте его!";
		this.GameModeDescription = new string[]
		{
			"Deathmatch (DM) - режим игры Все Против Всех. Побеждает набравший максимальное количество очков опыта.",
			"TargetDesignation (TDS) - режим игры Засветка Цели. Одна команда должна уничтожить цель, установив маяк. Другая должна не дать его установить.",
			"TeamElimination (TE) - режим игры Командное Уничтожение. Противостояние одной команды против другой с участием VIP бойцов.",
			"TacticalConquest (TC) - режим игры Завоевание. Удержание тактических точек."
		};
		string[,] array = new string[4, 2];
		array[0, 0] = "Deathmatch (DM) - режим игры Все Против Всех. Побеждает набравший";
		array[0, 1] = "максимальное количество очков опыта.";
		array[1, 0] = "TargetDesignation (TDS) - режим игры Засветка Цели. Одна команда должна";
		array[1, 1] = "уничтожить цель, установив маяк. Другая должна не дать его установить.";
		array[2, 0] = "TeamElimination (TE) - режим игры Командное Уничтожение. Противостояние";
		array[2, 1] = "одной команды против другой с участием VIP бойцов.";
		array[3, 0] = "TacticalConquest (TC) - режим игры Завоевание.";
		array[3, 1] = "Захват и удержание тактических точек";
		this.GameModeDescrCutted = array;
		this.QuickGameDescrCutted = new string[]
		{
			"Выберите предпочитаемый режим и(или) карту, либо оставьте все как",
			"есть и подключитесь к случайной игре."
		};
		this.PlayerQuit = "Пользователь вышел из игры.";
		this.HallOfFameHeader = new string[]
		{
			"ЛУЧШИЙ ПО ОЧКАМ",
			"ЛУЧШИЙ ПО ПОБЕДАМ",
			"ЛУЧШИЙ ПО ДОСТИЖЕНИЯМ",
			"ЛУЧШИЙ ПО ХЕДШОТАМ",
			"ЛУЧШИЙ ПО K/D",
			"ЛУЧШИЙ ПО ТОЧНОСТИ",
			"САМЫЙ ПРОКАЧЕННЫЙ",
			"ЛУЧШИЙ ПОКУПАТЕЛЬ",
			"ЛУЧШИЙ ПО ВРЕМЕНИ В ИГРЕ",
			"ЛУЧШИЙ ПО КОНТРАКТАМ",
			"ЛУЧШИЙ ПО PRO-KILL'АМ",
			"ЛУЧШИЙ ПОМОЩНИК"
		};
		this.Completed = "ВЫПОЛНЕНО";
		this.magazPo = " магаз. по ";
		this.patr = " патр.";
		this.ReturnToTheGame = "ВЕРНУТЬСЯ В ИГРУ";
		this.ExitFromTheServer = "ВЫЙТИ С СЕРВЕРА";
		this.QuickPlay = "БЫСТРАЯ ИГРА";
		this.SearchGames = "ПОИСК ИГР";
		this.Settings = "НАСТРОЙКИ";
		this.Career = "КАРЬЕРА";
		this.Help = "ПОМОЩЬ";
		this.GatherTheTeam = "Собери команду";
		this.FriendsInvited = "ДРУЗЕЙ ПРИГЛАШЕНО";
		this.ThePurchaseOfModification = "Покупка модификации ";
		this.ProgressDailyContracts = "Прогресс ежедневных контрактов";
		this.SetWeapons = "cет оружия";
		this.LockedRequired = "ЗАБЛОКИРОВАН. НЕОБХОДИМ ";
		this.Level = " УРОВЕНЬ";
		this.EarlyAccessCaps = "РАННИЙ ДОСТУП";
		this.EarlyAccess = "Ранний доступ";
		this.TextToOpenLastSet = "Для открытия этого набора предыдущий должен быть разблокирован";
		this.UnlockingTheSet = "Разблокировка набора";
		this.ProfileNotLoaded = "Профиль не загружен";
		this.FillUpBalance = "ПОПОЛНИТЬ БАЛАНС";
		this.Set = "КОМПЛЕКТ ";
		this.Votes = "Голосов: ";
		this.BonusString0 = "Добро пожаловать в ваш личный аккаунт Contract Wars! В связи с началом карьеры контрактора ЧВК, командование выделяет вам следующие ресурсы:";
		this.BonusString3 = "Заказчик доволен вашим прогрессом и обеспечивает вас дополнительными ресурсами.";
		this.BonusString10 = "Поздравлям с взятием 10-го уровня и разблокировкой второго блока оружия! Заказчик очень доволен и обеспечивает вас следующими полезными ресурсами";
		this.BonusString20 = "Поздравлям с взятием 20-го уровня и разблокировкой третьего блока! Заказчик доволен вашей работой и обеспечивает вас дополнительным финансированием и премиумным оружием";
		this.BonusString60 = "Заказчик поздравляет вас с первым легендарным званием и обеспечивает дополнительными ресурсами:";
		this.BonusString61 = "Поздравляем с получением легендарного звания the Death Grim! Заказчик обеспечивает вас дополнительными ресурсами:";
		this.BonusString62 = "Поздравляем с получением легендарного звания the Dog of War! Заказчик обеспечивает вас дополнительными ресурсами:";
		this.BonusString63 = "Поздравляем с получением легендарного звания the Reaver! Заказчик обеспечивает вас дополнительными ресурсами:";
		this.BonusString64 = "Поздравляем с получением легендарного звания the Predator! Заказчик обеспечивает вас дополнительными ресурсами:";
		this.BonusString65 = "Поздравляем с получением легендарного звания the Warblessed! Заказчик обеспечивает вас дополнительными ресурсами:";
		this.BonusString66 = "Поздравляем с получением легендарного звания the Knight-Protector! Заказчик ценит вашу лояльность и перечисляет вам награду:";
		this.BonusString67 = "Поздравляем с получением легендарного звания the Lone Reaper! Заказчик ценит вашу лояльность и перечисляет вам награду:";
		this.BonusString68 = "Поздравляем с получением легендарного звания the Sharkkiller! Заказчик ценит вашу лояльность и перечисляет вам награду:";
		this.BonusString69 = "Поздравляем с получением легендарного звания the Overlord! Заказчик ценит вашу лояльность и перечисляет вам награду:";
		this.BonusString70 = "Добро пожаловать к лучшим из лучших! Вам присуждается легендарное звание Contract Warrior! Архангел свяжется с вами";
		this.TheTerm = "Срок:";
		this.Day = "день";
		this.Days_dnya = "дня";
		this.Days_dney = "дней";
		this.D = "д ";
		this.H = "ч ";
		this.M = "м ";
		this.Reason = "Причина:";
		this.FriendText = "Вступай в бои плечом к плечу с друзьями! Сплоченная боевая группа - залог победы";
		this.FollowByLink = "ПЕРЕЙТИ ПО ССЫЛКЕ";
		this.InOtherNews = "К ДРУГИМ НОВОСТЯМ";
		this.TextUnlockKit = "Новый комплект снаряжения позволяет сохранить выбор оружия и спецсредств. Во время боя возможен только выбор комплектов.";
		this.UnlockFor = "Разблокировать за:";
		this.InsufficientFunds = "недостаточно средств";
		this.TextUnlockSet = "Ранний доступ к оружейному набору позволяет открыть его без достижения необходимого уровня. Оружие разблокированного блока необходимо также покупать за кредиты или GP!";
		this.InsufficientFundsNeed = "недостаточно средств, требуется:";
		this.TextUnlockWTask = "Вместо выполнения оружейного задания (W-Task) вы можете купить необходимую модификацию мгновенно.";
		this.BuyModFor = "Купить модификацию за:";
		this.RentSkill = "Арендовать?";
		this.Price = "ЦЕНА:";
		this.Cost = "Стоимость:";
		this.UnlockQuestion = "Разблокировать?";
		this.Yes = "ДА";
		this.TextResetSkills = "Вы действительно хотите сбросить имеющиеся умения, чтобы получить использованные очки умения обратно?";
		this.ResetSkillsFor = "Сбросить умения за:";
		this.Or = "или";
		this.ResetSkillAttention = "Внимание! Расчет стоимости ведется исходя из потраченных SP";
		this.EnterPassword = "Enter password:";
		this.OK = "OK";
		this.PromoHello = "Приветствую тебя, боец";
		this.PromoHelloForResourse0 = "Заказчик рад видеть тебя всегда";
		this.PromoHelloForResourse1 = "готовым к бою  и переводит на твой счет следующие ресурсы: ";
		this.HundredsOfOil = "СОТНИ НЕФТИ";
		this.Forever = " НАВСЕГДА";
		this.WellcomeToCWandGetBonus = "Заходи в Contract Wars и получай случайный бонус";
		this.CRGPSPEveryDay = "- кредиты, оружие, GP и даже SP каждый день!";
		this.Map = "Карта:";
		this.Mode = "Режим:";
		this.BuySPAttention = "Внимание! Необходимо подтверждение покупки очка умений (SP)";
		this.BuySPFor = "Купить 1 SP за:";
		this.GoToTheBattle = "В БОЙ!";
		this.GamesNotFound = "ИГР НЕ НАЙДЕНО!";
		this.AchievementWillNotCount = "ДОСТИЖЕНИЕ НЕ ЗАСЧИТАНО";
		this.DMHeader = "ВСЕ ПРОТИВ ВСЕХ";
		this.DMDescription = "Уничтожайте вражеских бойцов";
		this.VIPHeader = "ВЫ VIP!";
		this.VIPDescription = "Теперь вражеская команда охотится за вами";
		this.TDSHeader = "ЗАСВЕТКА ЦЕЛИ";
		this.TDSDescriptionB = "Уничтожьте объект, установив маяк в одной из целевых точек";
		this.TDSDescriptionU = "Не дайте вражеской команде уничтожить объект";
		this.TEHeader = "КОМАНДНОЕ УНИЧТОЖЕНИЕ";
		this.TEDescription = "Уничтожайте бойцов вражеской команды";
		this.TCHeader = "ЗАВОЕВАНИЕ";
		this.TCDescription = "Захватите тактические точки";
		this.YouKill = "ВЫ УБИЛИ:";
		this.YouKilled = "ВЫ УБИТЫ:";
		this.WTaskProgress = "W-TASK ПРОГРЕСС";
		this.ProgressTowards = "ПРОГРЕСС ДОСТИЖЕНИЯ";
		this.CallForSupport = "для вызова поддержки";
		this.BeginInstallingTheBeacon = "чтобы начать установку маяка";
		this.ClearanceToStartBeacon = "чтобы начать обезвреживание маяка";
		this.CompletedSmall = "Выполнен";
		this.CarrClassName = new string[]
		{
			"РАЗВЕДЧИК",
			"ШТУРМОВИК",
			"РАЗРУШИТЕЛЬ",
			"СНАЙПЕР",
			"ОРУЖЕЙНИК",
			"КАРЬЕРИСТ"
		};
		this.CarrClassified = "Информация засекречена";
		this.CarrUserForbidYourself = "Пользователь запретил просматривать информацию о себе.";
		this.CarrLVL = "УР.";
		this.CarrNameSet = "НАЗВАНИЕ КОМПЛЕКТА";
		this.CarrToTheNextRank = "до следующего звания: ";
		this.CarrRank = "текущее звание";
		this.CarrNextRank = "следующее звание";
		this.CarrPlaceRanking = "место в общем рейтинге";
		this.CarrAchievementComplete = "достижений выполнено";
		this.CarrAchievementNext = "ближайшее достижение";
		this.CarrWTaskComplete = "W-task'ов выполнено";
		this.CarrContractsComplete = "контрактов выполнено";
		this.CarrCurrentContract = "текущий контракт";
		this.CarrSpecialBadges = "специальные знаки отличия";
		this.CarrProfile = "ПРОФИЛЬ";
		this.CarrTopFriends = "ТОР СРЕДИ ДРУЗЕЙ";
		this.CarrTopClans = "ТОР ПО КЛАНАМ";
		this.CarrClans = "КЛАНЫ";
		this.CarrPlace = "Место";
		this.CarrTAG = "ТЭГ";
		this.CarrPlayers = "ИГРОКИ";
		this.CarrName = "НАЗВАНИЕ";
		this.CarrEXP = "ОПЫТ";
		this.CarrTop100lvl = "ТОР-300 ПО УРОВНЮ";
		this.CarrItemName = "Имя";
		this.CarrPoints = "Очки";
		this.CarrTop100EXP = "ТОР-300 ПО ОПЫТУ";
		this.CarrKills = "Убийства";
		this.CarrTop100Kills = "ТОР-300 ПО УБИЙСТВАМ";
		this.CarrDeath = "Смерти";
		this.CarrTop100Death = "ТОР-300 ПО СМЕРТЯМ";
		this.CarrTop100KD = "ТОР-300 ПО K/D";
		this.CarrReputation = "Репутация";
		this.CarrTop100Rep = "ТОР-300 ПО РЕПУТАЦИИ";
		this.CarrCardDescr = "Убить слона, засунув ему в хобот гранату";
		this.CarrAvaliableAt = "ДОСТУПНО НА";
		this.CarrMapName = "НАЗВАНИЕ КАРТЫ";
		this.CarrDaily = "ЕЖЕДНЕВНЫЕ";
		this.CarrCurrentContractsCAPS = "ТЕКУЩИЕ КОНТРАКТЫ";
		this.CarrNextContractsCAPS = "СЛЕДУЮЩИЕ КОНТРАКТЫ";
		this.CarrContractRefreshDescr0 = "ОБНОВЛЯЮТСЯ РАЗ В 24 ЧАСА";
		this.CarrContractRefreshDescr1 = "Выдача следующего контракта возможна только после выполнения текущего. ";
		this.CarrContractRefreshDescr2 = "Обновление и выдача новых контрактов происходит раз в 24 часа.";
		this.CarrContractRefreshDescr3 = "Если текущий контракт не выполнен за 24 часа, то он обнуляется и его нужно выполнять по новой.";
		this.CarrOnlineTime = "Время в онлайн";
		this.CarrHardcoreTime = "Время в hardcore";
		this.CarrWins = "Побед";
		this.CarrLoose = "Поражений";
		this.CarrDamage = "Нанесено повреждений";
		this.CarrUsedBullets = "Использовано патронов";
		this.CarrHeadShots = "Выстрелов в голову";
		this.CarrDoubleHeadShots = "Двойных хедшотов";
		this.CarrLongHeadShots = "Дальних хедшотов";
		this.CarrDoubleKills = "Двойных убийств";
		this.CarrTripleKills = "Тройных убийств";
		this.CarrAssists = "Помощь в устранении";
		this.CarrCreditSpend = "Потрачено кредитов";
		this.CarrWTaskOpened = "W-task открыто";
		this.CarrAchievingGetted = "Достижений получено";
		this.CarrArmstreakGetted = "Армстриков получено";
		this.CarrSupportCaused = "Поддержки вызвано";
		this.CarrKnifeKill = "Убийств ножом";
		this.CarrGrenadeKill = "Убийств гранатой";
		this.CarrTeammateKill = "Убийств своих";
		this.CarrSuicides = "Самоубийств";
		this.CarrBEARKills = "Убийств бойцов B.E.A.R";
		this.CarrUsecKills = "Убийств бойцов USEC";
		this.CarrFavoriteWeapon = "Любимое оружие";
		this.CarrTotalAccuracy = "Общая меткость";
		this.CarrMatchesCompleted = "Матчей сыграно";
		this.CarrStormtrooper = "ШТУРМОВИК";
		this.CarrDestroyer = "РАЗРУШИТЕЛЬ";
		this.CarrSniper = "СНАЙПЕР";
		this.CarrArmorer = "ОРУЖЕЙНИК";
		this.CarrCareerist = "КАРЬЕРИСТ";
		this.CarrCurrentBalance = "ТЕКУЩИЙ БАЛАНС";
		this.CarrResetSkills = "Cброс умений";
		this.CarrBootSkills = "УМЕНИЯ БОЙЦА";
		this.CarrBonus = "Бонус:";
		this.CarrRentTime = "Время аренды:";
		this.CarrUnlocked = "Разблокировано.";
		this.CarrYouInTheBattle = "ВЫ НАХОДИТЕСЬ В БОЮ";
		this.CarrTemporarilyUnavailable = "ВРЕМЕННО НЕДОСТУПЕН";
		this.CarrSkills = "УМЕНИЯ";
		this.CarrRating = "РЕЙТИНГ";
		this.CarrSummary = "СВОДКА";
		this.CarrAchievements = "ДОСТИЖЕНИЯ";
		this.CarrContracts = "КОНТРАКТЫ";
		this.CarrStatistics = "СТАТИСТИКА";
		this.BankGet = "ПОЛУЧИТЬ";
		this.BankAvaliable = "ДОСТУПНО";
		this.BankCostCAPS = "СТОИМОСТЬ";
		this.BankBuySP = "Покупка очков умений (SP)";
		this.BankFor = "ЗА";
		this.BankVote = "ГОЛОС";
		this.BankVote_golosov = "ГОЛОСОВ";
		this.BankVote_golosa = "ГОЛОСA";
		this.BankDiscount = "СКИДКА";
		this.BankCurrency = "РУБ.";
		this.BankCurFS = "ФМ";
		this.BankCurMailru = "МЭЙЛИКОВ";
		this.BankCurOK = "ОК";
		this.BankSPTitle = "ПОКУПКА ДОПОЛНИТЕЛЬНЫХ ОЧКОВ УМЕНИЙ (SP)";
		this.BankVKTitle0 = "ДЛЯ ДОСТУПА К PREMIUM КОНТЕНТУ НЕОБХОДИМО ИМЕТЬ GP, КОТОРЫЕ МОЖНО ПОЛУЧИТЬ ПЕРЕВОДОМ ГОЛОСОВ";
		this.BankVKTitle1 = "ДЛЯ ПОЛУЧЕНИЯ ДОПОЛНИТЕЛЬНЫХ КРЕДИТОВ МОЖНО ТАКЖЕ ПЕРЕВЕСТИ ГОЛОСА ВКОНТАКТЕ";
		this.BankFSTitle0 = "ДЛЯ ДОСТУПА К PREMIUM КОНТЕНТУ НЕОБХОДИМО ИМЕТЬ GP, КОТОРЫЕ МОЖНО ПОЛУЧИТЬ ПЕРЕВОДОМ ФМ";
		this.BankFSTitle1 = "ДЛЯ ПОЛУЧЕНИЯ ДОПОЛНИТЕЛЬНЫХ КРЕДИТОВ МОЖНО ТАКЖЕ ПЕРЕВЕСТИ ФМ";
		this.BankMailruTitle0 = "ДЛЯ ДОСТУПА К PREMIUM КОНТЕНТУ НЕОБХОДИМО ИМЕТЬ GP, КОТОРЫЕ МОЖНО ПОЛУЧИТЬ ПЕРЕВОДОМ ФМ";
		this.BankMailruTitle1 = "ДЛЯ ПОЛУЧЕНИЯ ДОПОЛНИТЕЛЬНЫХ КРЕДИТОВ МОЖНО ТАКЖЕ ПЕРЕВЕСТИ МЭЙЛИКИ";
		this.BankOKTitle0 = "ДЛЯ ДОСТУПА К PREMIUM КОНТЕНТУ НЕОБХОДИМО ИМЕТЬ GP, КОТОРЫЕ МОЖНО ПОЛУЧИТЬ ПЕРЕВОДОМ ОК";
		this.BankOKTitle1 = "ДЛЯ ПОЛУЧЕНИЯ ДОПОЛНИТЕЛЬНЫХ КРЕДИТОВ МОЖНО ТАКЖЕ ПЕРЕВЕСТИ ОК";
		this.BankTransaction = "ПЕРЕВОД";
		this.BankHistory = "ИСТОРИЯ";
		this.BankServices = "УСЛУГИ";
		this.BankOperationHistory = "ИСТОРИЯ ОПЕРАЦИЙ";
		this.BankQuantity = "Количество";
		this.BankValuta = "Валюта";
		this.BankComment = "Комментарий";
		this.BankDate = "Дата";
	}

	// Token: 0x06001A2A RID: 6698 RVA: 0x000EEBD4 File Offset: 0x000ECDD4
	public override string PressM(KeyCode button)
	{
		base.PressM(button);
		return "Нажмите " + button.ToString().Replace("Alpha", string.Empty) + " для вызова окна смены команды";
	}

	// Token: 0x06001A2B RID: 6699 RVA: 0x000EEC14 File Offset: 0x000ECE14
	public override string Press3Mortar(KeyCode button)
	{
		base.Press3Mortar(button);
		return "Нажмите " + button.ToString().Replace("Alpha", string.Empty) + " для вызова поддержки";
	}

	// Token: 0x06001A2C RID: 6700 RVA: 0x000EEC54 File Offset: 0x000ECE54
	public override string PressMouse2(KeyCode button)
	{
		base.PressMouse2(button);
		return "Нажмите " + button.ToString().Replace("Alpha", string.Empty) + " для смены режима камеры";
	}
}
