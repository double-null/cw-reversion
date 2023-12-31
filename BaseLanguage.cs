using System;
using System.Reflection;
using UnityEngine;

// Token: 0x020002F7 RID: 759
internal class BaseLanguage
{
	// Token: 0x06001590 RID: 5520 RVA: 0x000E28E0 File Offset: 0x000E0AE0
	public BaseLanguage()
	{
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
		this.CWMainLoadRatingDesc = "Идет загрузка топ 300";
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
		this.LoadingBuild = "ЗАГРУЖАЕТСЯ ИГРА";
		this.LoadingProfileFailed = "ЗАГРУЗКА ПРОФИЛЯ НЕ УДАЛАСЬ";
		this.LoadingProfileCheckConnection = "Проверьте соединение c Интернет";
		this.LoadingProfileCheckSoftware = "(прокси, порты, антивирус/файервол)";
		this.LoadingProfileReloadApplication = "или перезагрузите приложение";
		this.BannerGUIKnife = "нож";
		this.BannerGUIGrenade = "граната";
		this.BannerGUIMortarStrike = "mortar strike";
		this.Push = "Нажмите";
		this.NeedMoreWarrior = "НЕДОСТАТОЧНО БОЙЦОВ ДЛЯ ЗАХВАТА";
		this.CapturingPoint = "ПРОИСХОДИТ ЗАХВАТ ТОЧКИ";
		this.EnemyCapturingYourPoint = "ВАШУ ТОЧКУ ЗАХВАТЫВАЮТ!";
		this.NeutralizePoint = "ПРОИСХОДИТ НЕЙТРАЛИЗАЦИЯ ТОЧКИ";
		this.FriendCaptured = "ТОЧКА НАХОДИТСЯ ПОД ВАШИМ КОНТРОЛЕМ";
		this.EnemyCaptured = "ТОЧКА НАХОДИТСЯ ПОД ВРАЖЕСКИМ КОНТРОЛЕМ";
		this.PlayerRecieveSonar = "получил сонар";
		this.PlayerRecieveMortar = "получил минометный удар";
		this.PlayerPlacedMarker = "установил маяк";
		this.PlayerDifuseMarker = "обезвредил маяк";
		this.PlayerMakeStormKill = "сделал stormkill";
		this.PlayerMakeProKill = "сделал prokill";
		this.PlayerMakeLegendaryKill = "сделал legendarykill";
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
		this.SpecTeamIsOverpowered = "Слишком сильная команда";
		this.SpecWaitForBeginRound = "Ожидайте начала следующего раунда! ";
		this.No = "НЕТ";
		this.NoSmall = "нет";
		this.StartGame = "НАЧАТЬ ИГРУ";
		this.PlayerAutoBallanced = "Вы были перекинуты в другую команду для балансировки";
		this.killedVIP = "убил VIP";
		this.VIPInYourTeam = "В вашей команде VIP. Защищайте его любой ценой";
		this.VIPInEnemyTeam = "Во вражеской команде VIP. Убейте его!";
		this.GameModeDescription_DM = "Deathmatch (DM) - режим игры Все Против Всех. Побеждает набравший максимальное количество очков опыта.";
		this.GameModeDescription_TDS = "TargetDesignation (TDS) - режим игры Засветка Цели. Одна команда должна уничтожить цель, установив маяк. Другая должна не дать его установить.";
		this.GameModeDescription_TE = "TeamElimination (TE) - режим игры Командное Уничтожение. Противостояние одной команды против другой с участием VIP бойцов.";
		this.GameModeDescription_TC = "TacticalConquest (TC) - режим игры Завоевание. Удержание тактических точек.";
		this.GameModeDescrCutted_DM_0 = "Deathmatch (DM) - режим игры Все Против Всех. Побеждает набравший";
		this.GameModeDescrCutted_DM_1 = "максимальное количество очков опыта.";
		this.GameModeDescrCutted_TDS_0 = "TargetDesignation (TDS) - режим игры Засветка Цели. Одна команда должна";
		this.GameModeDescrCutted_TDS_1 = "уничтожить цель, установив маяк. Другая должна не дать его установить.";
		this.GameModeDescrCutted_TE_0 = "TeamElimination (TE) - режим игры Командное Уничтожение. Противостояние";
		this.GameModeDescrCutted_TE_1 = "одной команды против другой с участием VIP бойцов.";
		this.GameModeDescrCutted_TC_0 = "TacticalConquest (TC) - режим игры Завоевание.";
		this.GameModeDescrCutted_TC_1 = "Захват и удержание тактических точек";
		this.QuickGameDescrCutted_0 = "Выберите предпочитаемый режим и(или) карту, либо оставьте все как";
		this.QuickGameDescrCutted_1 = "есть и подключитесь к случайной игре.";
		this.PlayerQuit = "Пользователь вышел из игры.";
		this.HallOfFameHeader_BestPoints = "ЛУЧШИЙ ПО ОЧКАМ";
		this.HallOfFameHeader_BestWins = "ЛУЧШИЙ ПО ПОБЕДАМ";
		this.HallOfFameHeader_BestAchievements = "ЛУЧШИЙ ПО ДОСТИЖЕНИЯМ";
		this.HallOfFameHeader_BestHeadshots = "ЛУЧШИЙ ПО ХЕДШОТАМ";
		this.HallOfFameHeader_BestKD = "ЛУЧШИЙ ПО K/D";
		this.HallOfFameHeader_BestAccuracy = "ЛУЧШИЙ ПО ТОЧНОСТИ";
		this.HallOfFameHeader_BestPumped = "САМЫЙ ПРОКАЧЕННЫЙ";
		this.HallOfFameHeader_BestBuyer = "ЛУЧШИЙ ПОКУПАТЕЛЬ";
		this.HallOfFameHeader_BestOnlineTime = "ЛУЧШИЙ ПО ВРЕМЕНИ В ИГРЕ";
		this.HallOfFameHeader_BestContracts = "ЛУЧШИЙ ПО КОНТРАКТАМ";
		this.HallOfFameHeader_BestProKills = "ЛУЧШИЙ ПО PRO-KILL'АМ";
		this.HallOfFameHeader_BestAssistant = "ЛУЧШИЙ ПОМОЩНИК";
		this.anyMale = "ЛЮБОЙ";
		this.anyFemale = "ЛЮБАЯ";
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
		this.GetGoldPoints = "Получи GP";
		this.GetGpNow = "Получи GP";
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
		this.S = "с";
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
		this.ResetSkillYouGet = "Вы получите: ";
		this.ResetSkillsFor = "Сбросить умения за:";
		this.Or = "или";
		this.ResetSkillAttention = "Внимание! Расчет стоимости ведется исходя из потраченных SP";
		this.EnterPassword = "Enter password:";
		this.OK = "OK";
		this.PromoHello = "Приветствую тебя, боец";
		this.PromoHelloForResourse0 = "Заказчик рад видеть тебя всегда";
		this.PromoHelloForResourse1 = "готовым к бою  и переводит на твой счет следующие ресурсы: ";
		this.PromoHelloForResourse2 = "Боец! Только сегодня лично для тебя особая персональная скидка на";
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
		this.tventyMinutes = "20 минут";
		this.discount = "СКИДКА";
		this.discountGreeteng2 = "Уникальные скидки действуют до 00:00 каждый день!";
		this.NoSeats = "Все сервера заняты";
		this.Later = "ПОЗЖЕ";
		this.JoinFight = "В БОЙ!";
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
		this.CarrClassName_Scout = "РАЗВЕДЧИК";
		this.CarrClassName_Shtormtrooper = "ШТУРМОВИК";
		this.CarrClassName_Destroyer = "РАЗРУШИТЕЛЬ";
		this.CarrClassName_Sniper = "СНАЙПЕР";
		this.CarrClassName_Armorer = "ОРУЖЕЙНИК";
		this.CarrClassName_Careerist = "КАРЬЕРИСТ";
		this.CarrClassified = "Информация засекречена";
		this.CarrUserForbidYourself = "Пользователь запретил просматривать информацию о себе.";
		this.CarrLVL = "Ур.";
		this.CarrNameSet = "НАЗВАНИЕ КОМПЛЕКТА";
		this.CarrToTheNextRank = "до следующего звания: ";
		this.CarrRank = "текущее звание";
		this.CarrNextRank = "следующее звание";
		this.CarrPlaceRanking = "место в общем рейтинге";
		this.CarrAchievementComplete = "достижений выполнено";
		this.CarrAchievementNext = "ближайшее достижение";
		this.CarrCollectCards = "собрано карт";
		this.CarrLastCollectedCard = "последняя полученная карта";
		this.CarrWTaskComplete = "W-task'ов выполнено";
		this.CarrContractsComplete = "контрактов выполнено";
		this.CarrCurrentContract = "текущий контракт";
		this.CarrSpecialBadges = "специальные знаки отличия";
		this.CarrSpecialBadge = "знак отличия";
		this.CarrProfile = "ПРОФИЛЬ";
		this.CarrTopFriends = "ТОР СРЕДИ ДРУЗЕЙ";
		this.CarrTopClans = "ТОР ПО КЛАНАМ";
		this.CarrClans = "КЛАНЫ";
		this.CarrPlace = "Место";
		this.CarrTOP = "ТОП";
		this.CarrTAG = "Тэг";
		this.CarrPlayers = "Игроки";
		this.CarrName = "Название";
		this.CarrEXP = "Опыт";
		this.CarrTop100lvl = "ТОП 300 ПО УРОВНЮ";
		this.CarrItemName = "Имя";
		this.CarrPoints = "Очки";
		this.CarrTop100EXP = "ТОП 300 ПО ОПЫТУ";
		this.CarrKills = "Убийства";
		this.CarrTop100Kills = "ТОП 300 ПО УБИЙСТВАМ";
		this.CarrDeath = "Смерти";
		this.CarrTop100Death = "ТОП 300 ПО СМЕРТЯМ";
		this.CarrTop100KD = "ТОП 300 ПО K/D";
		this.CarrReputation = "Репутация";
		this.SeasonReward = "Сезонная награда";
		this.CarrTop100Rep = "ТОП 300 ПО РЕПУТАЦИИ";
		this.CarrCardDescr = "Убить слона, засунув ему в хобот гранату";
		this.CarrAvaliableAt = "ДОСТУПНО НА";
		this.CarrMapName = "НАЗВАНИЕ КАРТЫ";
		this.CarrDaily = "ЕЖЕДНЕВНЫЕ";
		this.CarrCurrentContractsCAPS = "ТЕКУЩИЕ КОНТРАКТЫ";
		this.CarrNextContractsCAPS = "СЛЕДУЮЩИЕ КОНТРАКТЫ";
		this.CarrSkipContract = "ПРОПУСТИТЬ";
		this.CarrUpdateContractsPopupHeader = "Обновление контрактов";
		this.CarrUpdateContractsPopupBody0 = "Вы действительно хотите обновить контракты?";
		this.CarrUpdateContractsPopupBody1 = "Внимание! Невыполненные контракты будут обнулены!";
		this.CarrSkipContractPopupHeader = "Пропуск контракта";
		this.CarrSkipContractPopupBody0 = "Вы действительно хотите пропустить контракт?";
		this.CarrSkipContractPopupBody1 = "Внимание! Награду за пропущенный контракт вы не получите!";
		this.CarrSkipContractPopupBody2 = "Внимание! Вы не сможете вернуться к этому контракту!";
		this.CarrContractRefreshDescr0 = "ОБНОВЛЯЮТСЯ РАЗ В 24 ЧАСА";
		this.CarrContractRefreshDescr1 = "Выдача следующего контракта возможна только после выполнения текущего. ";
		this.CarrContractRefreshDescr2 = "Обновление и выдача новых контрактов происходит раз в 24 часа.";
		this.CarrContractRefreshDescr3 = "Если текущий контракт не выполнен за 24 часа, то он обнуляется и его нужно выполнять снова.";
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
		this.CWSAUpdateBalance = "ОБНОВИТЬ БАЛАНС";
		this.BankSPTitle = "ПОКУПКА ДОПОЛНИТЕЛЬНЫХ ОЧКОВ УМЕНИЙ (SP)";
		this.BankMPTitle = "ПОКУПКА MP (MODIFICATION POINTS)";
		this.BankCRTitle = "ПОКУПКА КРЕДИТОВ (CREDITS)";
		this.BankVKTitle0 = "ДЛЯ ДОСТУПА К PREMIUM КОНТЕНТУ НЕОБХОДИМО ИМЕТЬ GP, КОТОРЫЕ МОЖНО ПОЛУЧИТЬ ПЕРЕВОДОМ ГОЛОСОВ";
		this.BankVKTitle1 = "ДЛЯ ПОЛУЧЕНИЯ ДОПОЛНИТЕЛЬНЫХ КРЕДИТОВ МОЖНО ТАКЖЕ ПЕРЕВЕСТИ ГОЛОСА ВКОНТАКТЕ";
		this.BankFRTitle0 = string.Empty;
		this.BankFRTitle1 = string.Empty;
		this.BankFBTitle0 = string.Empty;
		this.BankFBTitle1 = string.Empty;
		this.BankFSTitle0 = "ДЛЯ ДОСТУПА К PREMIUM КОНТЕНТУ НЕОБХОДИМО ИМЕТЬ GP, КОТОРЫЕ МОЖНО ПОЛУЧИТЬ ПЕРЕВОДОМ ФМ";
		this.BankFSTitle1 = "ДЛЯ ПОЛУЧЕНИЯ ДОПОЛНИТЕЛЬНЫХ КРЕДИТОВ МОЖНО ТАКЖЕ ПЕРЕВЕСТИ ФМ";
		this.BankMailruTitle0 = "ДЛЯ ДОСТУПА К PREMIUM КОНТЕНТУ НУЖНО ИМЕТЬ GP, КОТОРЫЕ МОЖНО ПОЛУЧИТЬ ПЕРЕВОДОМ МЭЙЛИКОВ";
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
		this.TabLoading = "ЗАГРУЖАЕТСЯ";
		this.TabLeadingOnPoints = "лидирует по очкам";
		this.TabPing = "Пинг";
		this.TabSuspectPlayer = "пожаловаться";
		this.TabAlreadySuspectedPlayer = "Вы уже пожаловались на этого игрока";
		this.TabSuspected = "(подозреваемый)";
		this.TabSuspectCheat = "Подозрение на чит";
		this.TabSuspectBugUse = "Использование багов";
		this.TabSuspectAbuse = "Нецензурное общение";
		this.TabNeedReputation = "Необходимо " + CVars.MinReportReputation + " репутации для отправки жалобы";
		this.SettingsHigh = "Высокое";
		this.SettingsMiddle = "Среднее";
		this.SettingsLow = "Низкое";
		this.SettingsMax = "Максимальное";
		this.SettingsNickAllow = "ник доступен";
		this.SettingsNickNotAllow = "ник недоступен";
		this.SettingsGame = "ИГРА";
		this.SettingsVideoAudio = "ВИДЕО/АУДИО";
		this.SettingsControl = "УПРАВЛЕНИЕ";
		this.SettingsNetwork = "СЕТЬ";
		this.SettingsBonuses = "БОНУСЫ";
		this.SettingsApply = "ПРИМЕНИТЬ";
		this.SettingsClan = "Клан";
		this.SettingsNickCheck = "идет проверка ника";
		this.SettingsNickMaxLenght = "мин. длина ника 4 символа";
		this.SettingsYourNickUsedInGame = "Ваш ник, используемый в игре:";
		this.SettingsYouAllowToChangeNick = "Вы можете сменить ник ";
		this.SettingsTimes = " раз";
		this.SettingsBuyNickChange = "Купить 1 смену";
		this.SettingsBuyChange = "Покупка смены ника";
		this.SettingsBuyColorChange = "Покупка смены цвета ника";
		this.SettingsBuyChangePopUp = "Вы уверены, что хотите купить смену ника?";
		this.SettingsBuyChangeColorPopUp = "Вы уверены, что хотите купить смену цвета ника?";
		this.SettingsHopsBonus = "ВАШ ПЕРСОНАЛЬНЫЙ КОД ДЛЯ ПОЛУЧЕНИЯ БОНУСА В HIRED OPS";
		this.SettingsHopsBonusHint = "НАЖМИТЕ, ЧТОБЫ СКОПИРОВАТЬ В БУФЕР ОБМЕНА";
		this.SettingsHopsKey = "КЛЮЧ ДЛЯ АКТИВАЦИИ HIRED OPS В STEAM";
		this.SettingsTransoprentyRadar = "Прозрачность радара:";
		this.SettingsShowProgressContract = "Показывать прогресс контрактов";
		this.SettingsSimpleShowContract = "Упрощенный показ прогресса контрактов";
		this.SettingsAlwaysShowHpDef = "Показывать всегда здоровье и броню";
		this.SettingsAutorespawn = "Автовозрождение";
		this.SettingsSwitchOffChat = "Выключить чат";
		this.SettingsSecondaryEquiped = "Начинать с пистолетом";
		this.SettingsHideInterface = "Скрыть интерфейс";
		this.SettingsEnableFullScreenInBattle = "Включать полноэкранный режим в бою";
		this.SettingsScreenRez = "Разрешение экрана (при полноэкранном режиме):";
		this.SettingsModelQuality = "Модели низкого качества";
		this.SettingsPostEffect = "Эффекты пост-обработки";
		this.LimitFrameRate = "Ограничить FPS до 60";
		this.SettingsGraphicQuality = "Качество графики:";
		this.SettingsVeryLow = "Очень низкое (Сверхнизкое)";
		this.SettingsLowMiddle = "Ниже среднего";
		this.SettingsCustom = "Пользовательские";
		this.SettingsAdvancedSettingsGr = "Продвинутые настройки графики:";
		this.SettingsShadowRadius = "Радиус прорисовки теней:";
		this.SettingsPaltryObjects = "Радиус прорисовки мелких объектов:";
		this.SettingsAudioMusic = "Настройки звука и музыки";
		this.SettingsOverallVolume = "Общая громкость:";
		this.SettingsSoundVolume = "Громкость звуков:";
		this.SettingsRadioVolume = "Громкость радио:";
		this.SettingsTextureQuality = "Качество текстур:";
		this.SettingsShadowQuality = "Качество теней:";
		this.SettingsLightningQuality = "Качество освещения:";
		this.SettingsPhysicsQuality = "Качество физики:";
		this.SettingsDefaultValue = "ЗНАЧЕНИЯ ПО-УМОЛЧАНИЮ";
		this.SettingsAction = "Действие";
		this.SettingsContolButton = "Клавиша управления";
		this.SettingsMouseSensitivity = "Чувствительность мыши:";
		this.SettingsInvertMouse = "Инверсия мыши";
		this.SettingsHold = "По зажатию:";
		this.SettingsMoveForward = "Движение вперед";
		this.SettingsMoveBack = "Движение назад";
		this.SettingsMoveLeft = "Движение влево";
		this.SettingsMoveRight = "Движение вправо";
		this.SettingsJump = "Прыжок";
		this.SettingsWalk = "Ходьба";
		this.SettingsSit = "Приседание";
		this.SettingsFire = "Огонь";
		this.SettingsAim = "Прицеливание";
		this.SettingsRecharge = "Перезарядка";
		this.SettingsGrenade = "Бросок гранаты";
		this.SettingsKnife = "Удар ножом";
		this.SettingsFireMode = "Смена режима стрельбы";
		this.SettingsSwitchWeapon = "Переключение оружия";
		this.SettingsSelectPistol = "Выбор пистолета";
		this.SettingsSelectMainWeapon = "Выбор основного оружия";
		this.SettingsUse = "Взаимодействие";
		this.SettingsCallSupport = "Вызов поддержки";
		this.SettingsMatchStatistics = "Статистика матча";
		this.SettingsExitToMainMenu = "Выход в главное меню";
		this.SettingsFullScreen = "Полноэкранный режим";
		this.SettingsTeamChange = "Смена команды";
		this.SettingsTeamMessage = "Сообщение команде";
		this.SettingsMessageToAll = "Сообщение всем";
		this.SettingsRadioCommand = "Радиокоманды";
		this.SettingsScreenshot = "Снимок экрана";
		this.SettingsNetworkDescr0 = "Приведенные ниже опции настройки сети помогут ускорить загрузку игры в зависимости от пропускной способности вашего интернета. Также для ускорения загрузки рекомендуется увеличить кеш браузера до 150-200 Мб.";
		this.SettingsNetworkDescr1 = "Перед началом матча загружается весь игровой контент. Используйте этот режим при минимальной скорости интернета от 30 Мбит/с. При данном режиме отсутствуют все артефакты, возникающие при подгрузке контента.";
		this.SettingsNetworkDescr2 = "РЕЖИМ ЧАСТИЧНОЙ ПРЕДВАРИТЕЛЬНОЙ ЗАГРУЗКИ (РЕКОМЕНД.)";
		this.SettingsNetworkDescr3 = "Перед началом матча загружаются только модели и текстуры уровня в высоком качестве. Модели и текстуры оружия подгружаются динамически после начала игры. Возможно запаздывание отображения подобранного оружия всвязи с его подгрузкой в реальном времени";
		this.SettingsNetworkDescr4 = "Перед началом матча загружаются только базовый контент уровня и текстуры низкого разрешения. Все остальное подгружается в реальном времени динамически. Возможно запаздывание отображения подобранного оружия и текстур уровня всвязи с их подгрузкой в реальном времени. Данный режим рекомендуется пользователям с низкой скоростью интернета";
		this.SettingsNetworkDescr5 = "РЕЖИМ ПОЛНОЙ ПРЕДВАРИТЕЛЬНОЙ ЗАГРУЗКИ";
		this.SettingsNetworkDescr6 = "РЕЖИМ ДИНАМИЧЕСКОЙ ПОДГРУЗКИ";
		this.SGGlobal = "ОБЩИЙ";
		this.SGWhithoutRating = "БЕЗ РЕЙТИНГА";
		this.SGFritends = "ДРУЗЬЯ";
		this.SGFavorites = "ИЗБРАННЫЕ";
		this.SGLatests = "НЕДАВНИЕ";
		this.SGServer = "Сервер";
		this.SGMap = "Карта";
		this.SGRate = "Рейт";
		this.SGPlayers = "Игроки";
		this.SGMode = "Режим";
		this.SGEmpty = "Пустые";
		this.SGFull = "Полные";
		this.SGLevel = "Уровень";
		this.SGCreateServer = "СОЗДАТЬ СЕРВЕР";
		this.SGConnect = "ПОДКЛЮЧИТЬСЯ";
		this.ChToAll = "всем";
		this.ChToTeam = "команде";
		this.GrenadeThrowMessage1 = "Бандероль";
		this.GrenadeThrowMessage2 = "Своя пошла";
		this.Task = "ЗАДАНИЯ";
		this.Packages = "БОКСЫ";
		this.SetOfEquipment = "КОМПЛЕКТЫ СНАРЯЖЕНИЯ";
		this.SelectedSet = "ВЫБРАННЫЙ КОМПЛЕКТ";
		this.CharacterCamouflage = "КАМУФЛЯЖ ПЕРСОНАЖА";
		this.Select = "ВЫБРАТЬ";
		this.CamouflageSelection = "ВЫБОР КАМУФЛЯЖА ПЕРСОНАЖА";
		this.IdleKick = "Кик за неактивность (>90 секунд). Полученный опыт не сохранен.";
		this.PingKickHeader = "Высокий пинг";
		this.PingKickBody1 = "Ваш пинг больше чем ";
		this.PingKickBody2 = " выберите другой сервер";
		this.TeamKillKick = "Кик за убийства своих.";
		this.TeamKillBan = "Убийство своих в Hardcore режиме";
		this.ErrorNetworkProtocol = "Ошибка сетевого протокола.";
		this.FloodKick = "Кик за флуд.";
		this.ServerFullPlayers = "Сервер заполнен, максимальное количество пользователей достигнуто.";
		this.ServerFullSpec = "Сервер заполнен, максимальное количество наблюдателей достигнуто.";
		this.ServerFullSlot = "Сервер заполнен, максимальное количество слотов для игроков достигнуто.";
		this.ServerSlotAvaliable = "Слоты для игроков доступны.";
		this.ServerRestarted = "Сервер перезагружается, попробуйте подключиться к другому серверу.";
		this.UserQuited = "Пользователь вышел из игры.";
		this.ServerDisconnect = "Сервер разорвал соединение.";
		this.Dead = "мертв";
		this.ServerDisconnetProfileLoadError = "Сервер разорвал соединение. Сбой загрузки вашего профиля из базы данных контракторов.";
		this.ServerDisconnetKeepaliveError = "Сервер разорвал соединение. Потеряна связь с сервером.";
		this.ProjectNews = "Новости проекта";
		this.BadlyFinishedBoy = "Доигрался, сынок?";
		this.ExittingFromServer = "Происходит выход с сервера";
		this.WrongPassword = "Неверный пароль.";
		this.YouAreAlreadyAtServer = "Вы уже находитесь на сервере.";
		this.ClientNotMatchTheServerVersion = "Устаревшая версия клиента. Перезагрузите игру";
		this.ConnetionDropped = "Соединение разорвано";
		this.ServerForciblyClosed = "Сервер был принудительно закрыт";
		this.VotedFor = "проголосовал за";
		this.SuspectedUser = "Вы пожаловались на";
		this.PromoCodeAlreadyActivated = "Код уже активирован";
		this.PromoObsolete = "Код устарел";
		this.PromoCodeAlreadyActivatedThisMember = "Код этого выпуска уже активирован этим пользователем";
		this.PromoUnknownCode = "Неизвестный код";
		this.PromoUnknownError = "Неизвестная ошибка активации промо-кода";
		this.PromoErrorActivation = "Ошибка активации";
		this.PromoActivated = "Промокод активирован";
		this.ProcessingRequest = "Обработка запроса";
		this.FundsDelivery = "Идет доставка средств";
		this.FundsDeliveryFailed = "Доставка средств не удалась";
		this.ClanTransactionLoading = "Загрузка истории платежей";
		this.ClanTransactionLoadingFailed = "Загрузка истории платежей не удалась";
		this.BuyKit = "Покупка комплекта";
		this.BuyKitProcessing = "Производится покупка снаряжения";
		this.KitDelivered = "Снаряжение доставлено";
		this.BuySet = "Покупка набора";
		this.BuyBox = "Покупка бокса";
		this.BuyBoxRequest = "Вы уверены, что хотите купить";
		this.BuyBoxProcessing = "Производится покупка бокса";
		this.BoxDelivered = "Бокс доставлен";
		this.BuyBoxAttention = "Время уже арендованных навыков или оружия будет продлено";
		this.BuyNick = "Покупка смены ника";
		this.BuyNickColor = "Покупка цвета ника";
		this.BuyNickProcessing = "Производится покупка смены ника";
		this.BuyNickColorProcessing = "Производится покупка цвета ника";
		this.NickDelivered = "Покупка завершена";
		this.CrrierBonus = "Карьерный бонус";
		this.GetLevel0 = "Я взял";
		this.GetLevel1 = "уровень в игре Contract Wars! Присоединяйся!";
		this.Modification = "Модификация";
		this.ModificationProcessing = "Производится модификация снаряжения";
		this.Repair = "Ремонт";
		this.EquipmentRepaired = "Снаряжение отремонтировано";
		this.RepairFailure = "Отказ в ремонте";
		this.RepairProcessing = "Производится ремонт снаряжения";
		this.ServerLoadOnFail0 = "Сервер разорвал соединение. Вы забанены и не можете подключаться к играм.";
		this.ServerLoadOnFail1 = "Сервер разорвал соединение. У вас недостаточно прав доступа.";
		this.ServerLoadOnFail2 = "Сервер разорвал соединение. Режим игры TargetDesignation не доступен в гостевой учетной записи.";
		this.ServerLoadOnFail3 = "Сервер разорвал соединение. У вас недостаточно репутации для входа.";
		this.ServerLoadOnFail4 = "Сервер разорвал соединение. Ваш уровень не соответствует требованиям сервера.";
		this.DailyBonus = "ЕЖЕДНЕВНЫЙ БОНУС";
		this.StatsOneDone = "Один готов";
		this.StatsMinusOne = "Минус один";
		this.StatsThisOneDone = "Этот сделан";
		this.SUITE = "НАБОР";
		this.WTaskNotCount = "W-TASK НЕ ЗАСЧИТАН";
		this.ClassName_Scout = "РАЗВЕДЧИК";
		this.ClassName_Shtormtrooper = "ШТУРМОВИК";
		this.ClassName_Destroyer = "РАЗРУШИТЕЛЬ";
		this.ClassName_Sniper = "СНАЙПЕР";
		this.ClassName_Armorer = "ОРУЖЕЙНИК";
		this.ClassName_Careerist = "КАРЬЕРИСТ";
		this.HGUnlock = "Разблокировка";
		this.HGUnlockQuestion = "Разблокировать?";
		this.RepairQuestion = "Ремонтировать?";
		this.HGState = "Состояние";
		this.HGWeaponBrokenNeedRepair = "Оружие полностью сломано. Необходим полный ремонт";
		this.HGIndestructible = "Сделать оружие неломаемым";
		this.HGPayQuestion = "Оплатить?";
		this.HGRent = "Аренда";
		this.HGRentQuestion = "Арендовать?";
		this.HGWeapon = "оружия";
		this.HGPremiumBuy = "Премиум-покупка ";
		this.HGBuyQuestion = "Купить?";
		this.ForeverNormal = "Навсегда";
		this.HGAvaliable = "Осталось в наличии";
		this.HGNotAvaliable = "НЕТ В НАЛИЧИИ";
		this.MHGtabs_Menu = "МЕНЮ";
		this.MHGtabs_Weapons = "ОРУЖИЕ";
		this.MHGtabs_Skills = "УМЕНИЯ";
		this.MHGtabs_Battle = "БОЙ";
		this.MHGDescr_0 = "Блок кнопок главного меню";
		this.MHGDescr_1 = "Карьера это главное в жизни каждого бойца ЧВК";
		this.MHGDescr_2 = "Звание, псевдоним игрока";
		this.MHGDescr_3 = "Текущий показатель опыта";
		this.MHGDescr_4 = "Прокачивайтесь и получайте новые звания";
		this.MHGDescr_5 = "Проценты выполнения контрактов";
		this.MHGDescr_6 = "Получайте награды за выполнение ежедневных заданий";
		this.MHGDescr_7 = "Текущий баланс игрока";
		this.MHGDescr_8 = "SP - это очки умений";
		this.MHGDescr_9 = "Кнопка сохранения комплекта";
		this.MHGDescr_10 = "Не забывайте на нее жать после выбора оружия!";
		this.MHGDescr_11 = "Кнопка-сигнализатор умений";
		this.MHGDescr_12 = "Когда она мигает значит у вас есть доступные очки умений. По клику открывает окно умений в карьере";
		this.MHGDescr_13 = "Прогресс оружейных заданий (W-TASK) выбранного оружейного сета";
		this.MHGDescr_14 = "Выбранный комплект снаряжения";
		this.MHGDescr_15 = "В бою вы не сможете свободно менять оружие. Выбирайте и сохраняйте необходимые комплекты снаряжения заранее";
		this.MHGDescr_16 = "Кнопка режима “Задания”";
		this.MHGDescr_17 = "Нажмите для просмотра текущих оружейных заданий и их прогресса выполнения";
		this.MHGDescr_18 = "“Социальное” оружие";
		this.MHGDescr_19 = "Приглашайте друзей, чтобы получить особое оружие";
		this.MHGDescr_20 = "Время до истечения аренды";
		this.MHGDescr_21 = "Выбранное основное оружие";
		this.MHGDescr_22 = "Премиумное оружие, покупаемое за валюту GP";
		this.MHGDescr_23 = "Кнопки переключения оружейных наборов (сетов)";
		this.MHGDescr_24 = "Заблокированное оружие (необходиы кредиты для покупки)";
		this.MHGDescr_25 = "Выбранное вторичное оружие (пистолет)";
		this.MHGDescr_26 = "Кнопка перехода в полноэкранный режим";
		this.MHGDescr_27 = "Также, чтобы перейти в полноэкранный режим, вы можете нажать клавишу F12";
		this.MHGDescr_28 = "Кнопка вкл/выкл обвеса";
		this.MHGDescr_29 = "После того, как оружейное заданий выполнено, вы можете активировать модифицированную версию оружия";
		this.MHGDescr_30 = "Характеристики оружия";
		this.MHGDescr_31 = "Цветными полосками показаны влияния обвесов (зеленые) и умений (синие) на базовые характеристики. Красной полоской обозначается отрицательное влияние от обвесов (ухудшение точности, урона и т.п.)";
		this.MHGDescr_32 = "Характеристика боевой дальности оружия";
		this.MHGDescr_33 = "По вертикальной оси - единицы урона, по горизонтальной - расстояние. График характеризует падение урона от расстояния";
		this.MHGDescr_34 = "Прогресс оружейного задания";
		this.MHGDescr_35 = "Характеристики обвеса";
		this.MHGDescr_36 = "Всплывающее окно W-TASK’a";
		this.MHGDescr_37 = "Полоса прогресса выполнения";
		this.MHGDescr_38 = "Кнопка досрочного выполнения W-TASK’а";
		this.MHGDescr_39 = "Оружие в режиме отображения заданий W-TASK’ов";
		this.MHGDescr_40 = "Кнопка ремонта";
		this.MHGDescr_41 = "Кнопка трехмерного просмотра оружия";
		this.MHGDescr_42 = "Техническое состояние оружия в единицах прочности";
		this.MHGDescr_43 = "Полоса технического состояния оружия";
		this.MHGDescr_44 = "Информация о ходе матча";
		this.MHGDescr_45 = "Зона текстовых оповещений";
		this.MHGDescr_46 = "Здесь показывается чат, информация о первенстве в матче, значимых событиях у игроков и т.п.";
		this.MHGDescr_47 = "Отображении позиции врага";
		this.MHGDescr_48 = "Необходимо умение “Гарантированное определение”";
		this.MHGDescr_49 = "Зона различных влияний на игрока";
		this.MHGDescr_50 = "Здесь показываюстя значки эффектов-бафферов - регенерация, урон от огня ,временная неуязвимость и т.п.";
		this.MHGDescr_51 = "Здоровье";
		this.MHGDescr_52 = "Броня";
		this.MHGDescr_53 = "Отображение позиции брошенной гранаты";
		this.MHGDescr_54 = "Необходимо умение “Сигнализатор опасности”";
		this.MHGDescr_55 = "Зона отображения информации о том, кто вас убил, или кого вы убили";
		this.MHGDescr_56 = "Название оружия в руках";
		this.MHGDescr_57 = "Количество патронов в магазине";
		this.MHGDescr_58 = "Индикатор режима огня/поломки";
		this.MHGDescr_59 = "Количество патронов в запасе";
		this.MHGDescr_60 = "Кол-во доступных гранат";
		this.MHGDescr_61 = "Необходимо умение “Осколочная граната EFD”";
		this.MHGDescr_62 = "Зона отображения доступных средств поддержки";
		this.MHGDescr_63 = "Для выбора средства нажмите клавишу “3”";
		this.MHGDescr_64 = "Зона отображения информации о прогрессе выполнения W-TASK, достижений и контрактов";
		this.MHGDescr_65 = "Сводка по убийствам";
		this.MHGDescr_66 = "Показываются никнеймы, оружие из которого убили и тип убийства";
		this.MHGDescr_67 = "Событие";
		this.MHGDescr_68 = "Убийства, хедшоты, лонгшоты, серии убийств, награды";
		this.MHGDescr_69 = "Кол-во полученного опыта";
		this.MHGDescr_70 = "Сектор обстрела";
		this.MHGDescr_71 = "Необходим навык “Внимательность”";
		this.MHGDescr_72 = "Тактическая миникарта";
		this.MHGDescr_73 = "Кнопки-переключатели веток умений различных классов";
		this.MHGDescr_74 = "Умение, доступное для разблокировки";
		this.MHGDescr_75 = "Разблокированное умение";
		this.MHGDescr_76 = "Умение, недоступное для покупки";
		this.MHGDescr_77 = "Премиумное умение";
		this.MHGDescr_78 = "Детальная информация о выбранном умении";
		this.MHGDescr_79 = "Прогресс прокачки персонажа";
		this.MHGDescr_80 = "Выделенный синей полосой класс определяет специализацию вашего бойца ( в данном случае Разведчик)";
		this.MHGDescr_81 = "Кнопка сброса умений";
		this.MHGDescr_82 = "Возвращает потраченные очки умений SP";
		this.MHGDescr_83 = "Кнопка пополнения баланса";
		this.MHGDescr_84 = "Доступные очки умений";
		this.MHGDescr_85 = "Зарабатывайте очки умений, получая новые звания, выполняя контракты";
		this.MHGHelp = "ПОМОЩЬ";
		this.MRYourResult = "ВАШИ РЕЗУЛЬТАТЫ";
		this.MRBestResult = "РЕЗУЛЬТАТЫ ЛУЧШЕГО ИГРОКА";
		this.MRCreditsForProgress = "Кредиты за достижения";
		this.MRExpRate = "Рейт опыта карты";
		this.MRSkill = "Скилл";
		this.MRDoubleExp = "Увеличение опыта";
		this.MRPlayersTax = "Налог на опыт";
		this.MRNightCredits = "Ночной воин";
		this.MRNightExp = "Ночной сбор опыта";
		this.MRSPSpend = "Очков умений получено";
		this.MRMPSpend = "Очки модификации оружия";
		this.MRMatchBonus = "Очков за результат матча";
		this.MRClanExpDep = "Вклад опыта в клан";
		this.MRClanCrDep = "Вклад кредитов в клан";
		this.MRTotal = "Итого";
		this.MREarnExp = "ПОЛУЧЕННЫЙ ОПЫТ";
		this.MRBestResultForMatch = "ЛУЧШИЕ РЕЗУЛЬТАТЫ ЗА МАТЧ";
		this.MRNoAchievements = "НЕТ ВЫДАЮЩИХСЯ ДОСТИЖЕНИЙ";
		this.MRBestPlayer = "лучший игрок";
		this.MRWorthPlayer = "худший игрок";
		this.MRWin = "ПОБЕДА";
		this.MRDraw = "НИЧЬЯ";
		this.MRMatchResult = "РЕЗУЛЬТАТЫ МАТЧА";
		this.MRGameTime = "Время игры";
		this.MRKillsTotal = "Всего убийств";
		this.MRDeathTotal = "Всего смертей";
		this.MainMaxMatchesDescr = "Доистигнуто максимальное количество матчей, либо приложение превысило допустимый интервал по времени.";
		this.RadarBeaconSet = "маяк установлен";
		this.ServerGUICreatingServer = "Создание сервера";
		this.ServerGUICreate = "СОЗДАТЬ";
		this.ServerGUIName = "Название";
		this.ServerGUIPlayersCount = "Кол-во игроков";
		this.ServerGUIGameMode = "Тип игры";
		this.RadioEmpty0 = "Я пустой";
		this.RadioEmpty1 = "Нулевой";
		this.RadioStart = "ВПЕРЕД!";
		this.RadioStart0 = "Начали!";
		this.RadioStart1 = "Поехали!";
		this.RadioReceived = "ПРИНЯЛ";
		this.RadioReceived0 = "Принял";
		this.RadioReceived1 = "Подтверждаю";
		this.RadioCover = "ПРИКРОЙ";
		this.RadioCover0 = "Прикрой";
		this.RadioCover1 = "Нужно прикрытие";
		this.RadioAttention = "ВНИМАНИЕ!";
		this.RadioAttention0 = "Внимание!";
		this.RadioAttention1 = "Контроль!";
		this.RadioClear = "ЧИСТО";
		this.RadioClear0 = "Чисто";
		this.RadioClear1 = "Проверено";
		this.RadioStop = "СТОП!";
		this.RadioStop0 = "Стоять!";
		this.RadioStop1 = "Замри!";
		this.RadioGood = "МОЛОДЕЦ";
		this.RadioGood0 = "Ай красава";
		this.RadioGood1 = "Молодец боец";
		this.RadioFollowMe = "ЗА МНОЙ";
		this.RadioFollowMe0 = "За мной";
		this.RadioFollowMe1 = "Работаем группой";
		this.RadioHelp = "НА ПОМОЩЬ!";
		this.RadioHelp0 = "Нужна помощь!";
		this.RadioHelp1 = "Требуется поддержка!";
		this.RadioCancel = "НИКАК НЕТ";
		this.RadioCancel0 = "Отставить";
		this.RadioCancel1 = "Не согласен";
		this.PointBearCaptured = "ТОЧКА ЗАХВАЧЕНА BEAR";
		this.PointUsecCaptured = "ТОЧКА ЗАХВАЧЕНА USEC";
		this.PointPurification = "НЕОБХОДИМА ЗАЧИСТКА ТОЧКИ";
		this.NeedMoreBear = "ЗАХВАТ НЕВОЗМОЖЕН. В КОМАНДЕ BEAR МАЛО БОЙЦОВ";
		this.NeedMoreUsec = "ЗАХВАТ НЕВОЗМОЖЕН. В КОМАНДЕ USEC МАЛО БОЙЦОВ";
		this.PointName_none = "none";
		this.PointName_Base = "БАЗА";
		this.PointName_Lighthouse = "МАЯК";
		this.PointName_Station = "СТАНЦИЯ";
		this.PointName_Tunnel = "ТОННЕЛЬ";
		this.Point = "ТОЧКА";
		this.MatchExp = "Опыт за матч";
		this.TeamWin = "Команда выиграла";
		this.Accuracy = "Точность";
		this.Impact = "Отдача";
		this.Damage = "Убойность";
		this.FireRate = "Скорострельн.";
		this.Mobility = "Мобильность";
		this.ReloadRate = "Перезарядка";
		this.Ammunition = "Боезапас";
		this.Cartridge = "Тип патронов";
		this.Penetration = "Пробиваемость";
		this.Objective = "Задача";
		this.EffectiveDistance = "Эфф. дальность";
		this.ShotGrouping = "Разлет картечи";
		this.HearDistance = "Дист. слышимости";
		this.MainGUIFreeChooseWeapon = "СВОБОДНЫЙ ВЫБОР ОРУЖИЯ ВО ВРЕМЯ БОЯ";
		this.MainGUIBlocked = "ЗАБЛОКИРОВАН";
		this.MainGUIChooseSavedSets = "ВЫБИРАЙТЕ СОХРАНЕННЫЕ КОМПЛЕКТЫ СНАРЯЖЕНИЯ";
		this.Unlock = "РАЗБЛОКИРОВАТЬ";
		this.WeaponView = "Просмотр оружия";
		this.Spectators = "НАБЛЮДАТЕЛИ";
		this.InviteFriends = "ПРИГЛАСИТЬ ДРУЗЕЙ";
		this.YouMadeDoubleHeadshot = "Вы сделали DoubleHeadshot";
		this.YouMadeTripleKill = "Вы сделали TripleKill";
		this.YouMadeQuadKill = "Вы сделали QuadKill";
		this.YouMadeRageKill = "Вы сделали RageKill";
		this.YouMadeStormKill = "Вы сделали StormKill";
		this.YouMadeProKill = "Вы сделали ProKill";
		this.YouMadeLegendaryKill = "Вы сделали LegendaryKill";
		this.ChooseDislocate = "ВЫБЕРИТЕ ТОЧКУ ДИСЛОКАЦИИ";
		this.Dislocate = "ДИСЛОЦИРОВАТЬСЯ";
		this.SettingsCharacterQuality = "Оптимизированные персонажи";
		this.Ready = "Готов";
		this.TutorHintFitstTime = "Нажмите кнопку Помощь, чтобы получить справку по игре";
		this.TutorNickname = "Добро пожаловать в Contract Wars! Это ваш боевой псевдоним и уровень";
		this.TutorExpBar = "Это полоска опыта. Вам нужно зарабатывать его, чтобы открывать новое оружие, умения и т.д.";
		this.TutorContracts = "Это степени завершенности ежедневных контрактов. Выполняйте их для получения различных бонусов";
		this.TutorBalance = "Это баланс ваших ресурсов. Вы можешь добавить их, нажав кнопку ПОПОЛНИТЬ БАЛАНС";
		this.TutorBuyWeapon = "Кликайте сюда, чтобы купить основное оружие";
		this.TutorEquipPrimary = "Кликните на иконку оружия, чтобы снарядить его";
		this.TutorConfirmPayment = "Подтвердите покупку";
		this.TutorInstallWtask = "Это пистолет с выполненным оружейным заданием - w-task. Кликните на икноку звезды, чтобы установить обвес на оружие.";
		this.TutorEquipSecondary = "Кликните на иконку пистолета, чтобы снарядить вторичное оружие";
		this.TutorSaveWeaponKit = "Кликните на иконку дискеты, чтобы сохранить комплект";
		this.TutorSelectedKit = "Это ваш текущий выбранный комплект оружия. Теперь вы готовы к первому бою!";
		this.TutorQuickMatchOpen = "Нажмите БЫСТРАЯ ИГРА, чтобы открыть окно подбора матча";
		this.TutorQuickMatchGo = "Нажмите В БОЙ! (когда кнопка активна), чтобы начать загрузку матча. Приготовьтесь!";
		this.TutorFullScreen = "Нажмите эту кнопку (или клавишу F12), чтобы активировать полноэкранный режим";
		this.TutorHeader1 = "Базовое обучение";
		this.TutorHeader2 = "чтобы перейти к следующему шагу, кликните на подсвеченную область";
		this.TutorHeader3 = "закрыть";
		this.TutorInGameControlHeader = "ОПИСАНИЕ БАЗОВОГО УПРАВЛЕНИЯ В CONTRACT WARS";
		this.TutorInGameWeaponChange = "Смена оружия";
		this.TutorInGameMenu = "Главное меню";
		this.TutorInGameFSmode = "Полный экран";
		this.TutorInGameWalk = "Шаг";
		this.TutorInGameMovement = "Движение";
		this.TutorInGameReload = "Перезарядка";
		this.TutorInGameCrouch = "Присесть";
		this.TutorInGameKnife = "Нож";
		this.TutorInGameJump = "Прыжок";
		this.TutorInGameFire = "Огонь (ЛКМ)";
		this.TutorInGameAim = "Прицел (ПКМ)";
		this.TutorInGameContinue = "НАЖМИТЕ ЛЮБУЮ КЛАВИШУ, ЧТОБЫ ПРОДОЛЖИТЬ";
		this.TutorInGameGameplayHeader = "ОПИСАНИЕ БАЗОВЫХ ЗАДАЧ CONTRACT WARS";
		this.TutorInGameGameplayHint1_1 = "Уничтожейте врагов, выполняйте задачи, зарабатывайте Очки Опыта";
		this.TutorInGameGameplayHint1_2 = "и Кредиты";
		this.TutorInGameGameplayHint2 = "Повышайте свой уровень, получайте новые звания, открывайте новое оружие и зарабатывайте Очки Умений";
		this.TutorInGameGameplayHint3 = "Улучшайте своего персонажа, открывая новые умения и способности в меню Карьеры";
		this.TutorInGameGameplayHint4 = "Выполняйте задания (W-Task, контракты, достижения) чтобы получить улучшения оружия,";
		this.DN = "дн";
		this.Buyed = "куплено";
		this.SettingsCallMortarStrike = "Минометный удар";
		this.SettingsCallSonar = "Сонар";
		this.SettingsUseSpecEquipment = "Клановая спецспособность";
		this.SettingsHideShowInterface = "Скрыть / показать интерфейс";
		this.Cancel = "Отмена";
		this.FindInFull = "Искать на заполненных";
		this.ClansInfo = "КЛАН ИНФО";
		this.ClansCreate = "СОЗДАТЬ";
		this.ClansJoin = "ВСТУПИТЬ";
		this.ClansWars = "КЛАН ВОЙНЫ";
		this.ClansManagment = "УПРАВЛЕНИЕ";
		this.ClansLeveling = "ПРОКАЧКА";
		this.ClansName = "КЛАН";
		this.ClansLead = "Кланлидер";
		this.ClansLeadYou = "(ты)";
		this.ClansStats = "Статы клана";
		this.ClansYourStats = "Статы твоего клана";
		this.ClansYourContribution = "Твой вклад в клан";
		this.ClansYourContributionHint = "двигайте ползунок для установки доли опыта";
		this.ClansExpSliderHint = "Необходимо достичь 10 уровня";
		this.ClansBalanceHint = "Необходимо достичь 20 уровня";
		this.ClansExpSliderInGameHint = "Вы находитесь в бою";
		this.ClansSize = "Кол-во бойцов";
		this.ClansVictory = "Побед в клангонках";
		this.Officers = "Офицеров";
		this.ClansYourWarrior = "Твой боец";
		this.ClansRequest1 = "ПОДАТЬ";
		this.ClansRequest2 = "ЗАЯВКУ";
		this.ClansWithdraw = "ОТОЗВАТЬ";
		this.ClansCreateLabel = "СОЗДАНИЕ КЛАНА";
		this.ClansRequestLeft = "У ВАС ОСТАЛОСЬ ЗАЯВОК: ";
		this.ClansRaceBtn = "КЛАН ГОНКА";
		this.ClansWarsBtn = "ВОЙНЫ";
		this.ClansArmoryBtn = "АРСЕНАЛ";
		this.ClansCamouflageBtn = "КАМУФЛЯЖ";
		this.ClansHistory = "ИСТОРИЯ";
		this.ClansClantag = "Клантэг";
		this.ClansClantagColor = "Цвет клантэга";
		this.ChangeNickColor = "Цвет ника";
		this.ClansClanName = "Название клана";
		this.ClansBase = "Базовый клан";
		this.ClansExtended = "Расширенный";
		this.ClansPremium = "Премиумный";
		this.ClansCreateHint1 = "макс. 5 лат. симв. (без знаков)";
		this.ClansCreateHint2 = "в веб-формате (пр. baff00)";
		this.ClansCreateHint3 = "макс. 25 симв.";
		this.ClansCreateHint4 = "на 10 человек";
		this.ClansCreateHint5 = "на 20 человек";
		this.ClansCreateHint6 = "на 50 человек";
		this.ClansManagmentDiscard1 = "ОТКЛОНИТЬ";
		this.ClansManagmentDiscard2 = "ВСЕ ЗАЯВКИ";
		this.ClansManagmentExtend = "УВЕЛИЧИТЬ";
		this.ClansManagmentLeave = "ПОКИНУТЬ";
		this.ClansManagmentRequest = "Заявки на вступление в клан";
		this.ClansManagmentCurrent = "Действующие бойцы клана";
		this.ClansBalance = "ТЕКУЩИЙ БАЛАНС КЛАНА";
		this.ClansSkillAccess = "ДОСТУП К ПОКУПКЕ У КЛАН-ЛИДЕРА";
		this.ClansMinimalTransaction = "Сумма минимального перевода:";
		this.ClansTableContribution = "Вклад в клан";
		this.ClansTableDiff = "Отличия";
		this.ClansRaceAttention = "ВНИМАНИЕ!";
		this.ClansRaceHint = "ИДЕТ КЛАНОВАЯ ГОНКА!";
		this.ClansRaceHint1 = "НЕТ ТЕКУЩЕЙ КЛАНОВОЙ ГОНКИ";
		this.ClansRaceEnding = "ОКОНЧАНИЕ:";
		this.ClansRaceExp = "Опыт в клан гонке";
		this.ClansRaceKills = "Убийств в клан гонке";
		this.ClansDisbanded = "Расформирован";
		this.ClansPopupError = "Ошибка!";
		this.ClansPopupCreate = "Создание клана";
		this.ClansPopupCreateHint1 = "Вы действительно хотите создать клан?";
		this.ClansPopupCreateHint2 = "Базовый клан за:";
		this.ClansPopupCreateHint3 = "Расширенный клан за:";
		this.ClansPopupCreateHint4 = "Премиумный клан за:";
		this.ClansPopupCreateHint5 = "Внимание! Проверьте всю введенную информацию о клане";
		this.ClansPopupCreateError1 = "Создание клана невозможно.";
		this.ClansPopupCreateError2 = "Проверьте введенную информацию о клане.";
		this.ClansPopupExtend = "Увеличение клана";
		this.ClansPopupExtendHint = "Вы действительно хотите увеличить свободное место в клане на 5 человек?";
		this.ClansPopupRequest = "Подача заявки";
		this.ClansPopupRequestFailedByOrder1 = " У вас больше нет заявок на подачу.";
		this.ClansPopupRequestFailedByOrder2 = "Количество подач заявок зависит от вашего уровня.";
		this.ClansPopupRequestFailedByVacancy1 = "Вы не можете подать заявку.";
		this.ClansPopupRequestFailedByVacancy2 = "Максимальное количество заявок в этот клан достигнуто.";
		this.ClansPopupRequestFailedByVacancy3 = "Попробуйте подать заявку позже.";
		this.ClansPopupDiscard = "Отклонение заявок";
		this.ClansPopupDiscardHint = "Вы действительно хотите отклонить все заявки в ваш клан?";
		this.ClansPopupDismiss = "Увольнение бойца";
		this.ClansPopupDismissHint1 = "Вы действительно хотите уволить бойца из вашего клана?";
		this.ClansPopupDismissHint2 = "Вклад, внесенный бойцом в клан, аннулируется.";
		this.ClansPopupLeave = "Покинуть клан";
		this.ClansPopupLeaveHint = "Вы действительно хотите покинуть клан?";
		this.ClansPopupBalance = "Пополнение баланса клана";
		this.ClansPopupBalanceHint = "Укажите, сколько денег вы хотите перевести со своего счета на клановый баланс:";
		this.ClansHistoryWho = "Кто пополнил";
		this.CreateClan = "Создание клана";
		this.CreateClanProcessing = "Производится создание клана";
		this.CreateClanComplete = "Создание клана завершено";
		this.CreateClanErr = string.Empty;
		this.ExtendClan = "Увеличение клана";
		this.ExtendClanProcessing = "Производится увеличение клана";
		this.ExtendClanComplete = "Увеличение клана завершено";
		this.DeleteAllRequest = "Отклонение заявок";
		this.DeleteAllRequestProcessing = "Производится отклонение заявок";
		this.DeleteAllRequestComplete = "Заявки отклонены";
		this.KickFromClan = "Увольнение бойца";
		this.KickFromClanProcessing = "Производится увольнение бойца";
		this.KickFromClanComplete = "Боец уволен";
		this.SendRequest = "Подача заявки";
		this.SendRequestProcessing = "Производится подача заявки";
		this.SendRequestComplete = "Заявка подана";
		this.RevokeRequest = "Отклонение заявки";
		this.RevokeRequestProcessing = "Производится отклонение заявки";
		this.RevokeRequestComplete = "Заявка отклонена";
		this.AcceptRequest = "Подтверждение заявки";
		this.AcceptRequestProcessing = "Производится подтверждение заявки";
		this.AcceptRequestComplete = "Заявка принята";
		this.DeleteRequest = "Отклонение заявки";
		this.DeleteRequestProcessing = "Производится отклонение заявки";
		this.DeleteRequestComplete = "Заявка отклонена";
		this.ExitClan = "Покинуть клан";
		this.ExitClanProcessing = "Происходить выход из клана";
		this.ExitClanComplete = "Вы покинули клан";
		this.ClanListLoading = "Список кланов";
		this.ClanListLoadingDesc = "Идет загрузка списка кланов";
		this.ClanListLoadingFin = "Список кланов загружен";
		this.ClanListLoadingErrDesc = "Список кланов не загружен";
		this.ClanDetailLoading = "Информация о клане";
		this.ClanDetailLoadingDesc = "Идет загрузка информации о клане";
		this.ClanDetailLoadingFin = "Информация о клане загружена";
		this.ClanDetailLoadingErrDesc = "Информация о клане не загружена";
		this.ClansCheck = "проверка...";
		this.ClansAvailable = "доступно";
		this.ClansUnavailable = "недоступно";
		this.CurrentColor = "Текущий цвет";
		this.ClansHomePage = "Штаб-квартира (ссылка)";
		this.ClansHomePageHint = "Пример: http://contractwarsgame.com/ макс. 100 симв.";
		this.ClansHeadquarters1 = "ШТАБ";
		this.ClansHeadquarters2 = "КВАРТИРА";
		this.ClansLeader = "лидер";
		this.ClansLieutenant = "заместитель";
		this.ClansOfficer = "офицер";
		this.ClansContractor = "боец";
		this.ClansNotInClan = "не состоит в клане";
		this.ClansLeaderShort = "ГЛ";
		this.ClansLieutenantShort = "З";
		this.ClansOfficerShort = "ОФ";
		this.Earn = "вклад";
		this.Role = "роль";
		this.ClansEditPopupHeader = "Редактирование клана";
		this.ClansSetLeaderPopupHeader = "Назначение главы клана (кланлидера)";
		this.ClansSetLtPopupHeader = "Назначение заместителя";
		this.ClansSetOfficerPopupHeader = "Назначение офицера";
		this.ClansDismissLtPopupHeader = "Снятие заместителя с должности";
		this.ClansDismissOfficerPopupHeader = "Снятие офицера с должности";
		this.ClansSetLeaderPopupBody = "Вы действительно хотите назначить нового кланлидера?";
		this.ClansSetLtPopupBody = "Вы действительно хотите назначить нового заместителя?";
		this.ClansDismissLtPopupBody = "Вы действительно хотите снять с должности заместителя?";
		this.ClansSetOfficerPopupBody = "Вы действительно хотите назначить нового офицера?";
		this.ClansDismissOfficerPopupBody = "Вы действительно хотите снять с должности офицера?";
		this.ClansSetLeaderPopupHint = "Кланлидер имеет полные возможности по управлению кланом и его прокачкой.";
		this.ClansSetLtPopupHint = "Заместитель может все, кроме редактирования инфо о клане и смены главы и заместителя.";
		this.ClansSetOfficerPopupHint = "Офицеров может быть несколько. Они могут принимать или отклонять новых бойцов.";
		this.ClansEditMessagePopup = "Редактирование сообщения";
		this.ClansEditMessageCharactersleft = "Символов осталось";
		this.ClansDefaultMessage = "Кликните для редактирования сообщения";
		this.ClansError1001 = "Недостаточно полномочий.";
		this.ClansError1002 = "Неверный ID игрока.";
		this.ClansError1003 = "Игрок уже состоит в клане.";
		this.ClansError1004 = "В клане нет свободных мест.";
		this.ClansError1005 = "Нельзя удалить лидера.";
		this.ClansError1006 = "Игрок не состоит в клане.";
		this.ClansError1007 = "Вы не можите назначить себя.";
		this.ClansError1008 = "Не кланлидер.";
		this.ClansError1009 = "Неверный ID клана.";
		this.ClansError1010 = "Недостаточно GP в казне клана.";
		this.ClansError1011 = "Игрок уже является заместителем.";
		this.ClansError1012 = "Вы не лидер и не заместитель.";
		this.ClansError1013 = "Достигнуто максимальное количество офицеров.";
		this.ClansError1014 = "Игрок уже является офицером.";
		this.ClansError1015 = "Вы не можете разжаловать себя.";
		this.ClansError1016 = "Игрок не заместитель.";
		this.ClansError1017 = "Игрок не офицер.";
		this.ClansError1018 = "Already edited.";
		this.ClansError1019 = "Неверный формат цвета.";
		this.ClansError1020 = "Неверный формат ссылки.";
		this.ClansError1100 = "Ошибка базы данных.";
		this.NotEnoughWarriorTasks = "Недостаточно бойцов для выполнения задач";
		this.NotEnoughWarriorExp = "Недостаточно бойцов для получения опыта";
		this.Reward = "Награда";
		this.And = "и";
		this.For = "На";
		this.GotAchievement0 = "I've got an achievement";
		this.GotAchievement1 = "in Contract Wars online FPS here, on facebook!";
		this.Roulette = "Рулетка";
		this.RouletteTries = "Попыток";
		this.RouletteDescription = "каждый день вам дается максимум до двух бесплатных попыток крутить рулетку";
		this.RouleteTriesLeft = "Осталось попыток";
		this.RouletteTriesAdd = "Добавить попыток";
		this.RouletteRoll = "Крутить";
		this.RouletteWin = "Вы выиграли";
		this.RouletteWinSpecial = "Вы выиграли спецприз";
		this.RouletteWinSkill = "Вы выиграли умение";
		this.RouletteWinWeapon = "Вы выиграли оружие";
		this.RouletteLose = "Ничего";
		this.RouletteTryAgain = "Попробуйте ещё раз!";
		this.RouletteTriesEnded = "У вас кончились попытки";
		this.RouletteWaitOrBuy = "Дождитесь следующего дня, либо купите попытки за GP";
		this.RouletteSkill = "УМЕНИЕ";
		this.RouletteWeapon = "ОРУЖИЕ";
		this.RoulettePopupHeader = "Покупка попытки";
		this.RoulettePopupBody = "Вы действительно хотите купить попытку?";
		this.RouletteCamo = "Вы выиграли камуфляж";
		this.RouletteAttempt = "Вы получаете еще одну попытку";
		this.RouletteOneAttempt = "Попытка";
		this.RouletteCamouflage = "Камуфляж";
		this.DataBaseFailure = "Сбой базы данных";
		this.LevelFailure = "Ваш уровень не соответствует требованиям данной игры";
		this.LeagueRank = "Ранг";
		this.LeaguePlace = "место";
		this.LeagueLP = "очки LP";
		this.LeagueWins = "победы";
		this.LeagueDefeats = "пораж.";
		this.LeagueLeaves = "выходы";
		this.LeagueRatio = "w. ratio";
		this.LeagueRules = "ПРАВИЛА ЛИГИ";
		this.LeagueCurrentPrizes = "ПРИЗЫ ЗА ТЕКУЩИЙ СЕЗОН";
		this.LeaguePastPrizes = "ПРИЗЫ ЗА ПРОШЕДШИЙ СЕЗОН";
		this.LeagueFuturePrizes = "ПРИЗЫ ЗА БУДУЩИЙ СЕЗОН";
		this.LeagueRating = "РЕЙТИНГ ЛИГИ";
		this.LeagueSearchGame = "НАЙТИ ИГРУ";
		this.LeagueCancel = "ОТМЕНА";
		this.LeagueBoosters = "Бустеры";
		this.LeagueSeasonEnd = "ДО КОНЦА СЕЗОНА";
		this.LeagueSeasonBreak = "ПЕРЕРЫВ";
		this.LeagueRatingHeaderLvl = "Ур.";
		this.LeagueRatingHeaderNameNick = "Имя и никнейм";
		this.LeagueRatingHeaderLP = "Очки LP";
		this.LeagueRatingHeaderWins = "Поб.";
		this.LeagueRatingHeaderDefeats = "Пор.";
		this.LeagueRatingHeaderLeaves = "Вых.";
		this.LeagueSearching = "ПОИСК ИГРЫ";
		this.LeagueInQueue = "В ОЧЕРЕДИ:";
		this.LeaguePlaying = "ИГРАЕТ:";
		this.LeagueAdBoosters = "Покупай особые бустеры на игры лиги и побеждай";
		this.LeagueGameReady = "ВАША ИГРА ГОТОВА!";
		this.LeagueAccept = "ПРИНЯТЬ!";
		this.LeagueMap = "КАРТА:";
		this.LeagueMode = "РЕЖИМ:";
		this.LeagueMapLoading = "ЗАГРУЗКА КАРТЫ";
		this.LeagueGameStarted = "ИГРА НАЧАЛАСЬ";
		this.LeagueWaitingPlayers = "ОЖИДАНИЕ ИГРОКОВ";
		this.LeagueMatchStart = "НАЧАЛО МАТЧА";
		this.LeagueGoneGameResult = "РЕЗУЛЬТАТЫ ПРОШЕДШЕЙ ИГРЫ";
		this.LeagueYourTeamWon = "ВАША КОМАНДА ПОБЕДИЛА";
		this.LeagueYourTeamLose = "ВАША КОМАНДА ПРОИГРАЛА";
		this.LeagueTie = "НИЧЬЯ";
		this.LeagueShare = "ЗАПОСТИТЬ";
		this.LeagueNext = "ДАЛЕЕ";
		this.LeaguePointLeft = "ОСТАЛОСЬ LP ДО";
		this.LeagueYouLeave = "ВЫ ВЫШЛИ ИЗ ИГРЫ";
		this.LeagueNotAvailable = "НЕ ДОСТУПНО";
		this.LeaguePrizes = "ПРИЗЫ";
		this.LeagueNickname = "Никнейм";
		this.LeagueKills = "Убийств";
		this.LeagueDeaths = "Смертей";
		this.LeagueAssists = "Помощи";
		this.LeagueUnknown = "НЕИЗВЕСТНО.";
		this.LeagueOffSeason = "ИДЕТ МЕЖСЕЗОННЫЙ ПЕРЕРЫВ.";
		this.LeagueWinners = "ПОБЕДИТЕЛИ ПРОШЛОГО СЕЗОНА";
		this.LeaguePopupFirst = "1-ОЕ";
		this.LeaguePopupSecond = "2-ОЕ";
		this.LeaguePopupThird = "3-Е";
		this.LeaguePopupYourResults = "ваши результаты";
		this.LeaguePopupYourRewards = "ваши награды";
		this.LeaguePopupCongrats = "Поздравляем! Вы заняли призовое место лиги!";
		this.LeagueCurrent = "Текущий";
		this.LeaguePast = "Прошедший";
		this.LeagueFuture = "Будущий";
		this.LeagueNotification1 = "'CW Лига' находится на этапе бета-тестирования.";
		this.LeagueNotification2 = "Возможны возникновения багов и неполадок в рамках режима лиг.";
		this.League = "CW Лига";
		this.LeagueUpper = "CW ЛИГА";
		this.LeagueLoading = "Идет загрузка CW лиги";
		this.LeagueAvailableHintStart = "Необходимо достичь";
		this.LeagueAvailableHintEnd = "уровня";
		this.Hours = "ч";
		this.HightPacketLoss = "плохое соединение";
		this.HighPing = "высокий пинг";
		this.GettingServerList = "ПОЛУЧЕНИЕ СПИСКА ИГР";
		this.Sorting = "ИДЕТ СОРТИРОВКА";
		this.GamelistIsNotAvailable = "НЕТ ДОСТУПНЫХ ИГР";
		this.KillStreak = "Серия убийств";
		this.WeatherEffects = "Погодные эффекты";
		this.UnityCaching = "Использовать кэширование";
		this.ProKillScreenShotSetting = "Прокилл/легендари килл";
		this.QuadKillScreenShotSetting = "Квадкилл";
		this.LevelUpScreenShotSetting = "Взятие нового уровня";
		this.AchievementScreenShotSetting = "Получение достижения/в-таска";
		this.AutoScreenshotAt = "Автоскриншоты:";
		this.GoFullscreen = "на полный экран";
		this.FriendsShort = "Др";
		this.FriendsOne = "друг";
		this.FriendsSeveral = "друзей";
		this.AddToFavorites = "Добавление в избранное";
		this.RemoveFromFavorites = "Удаление из избранного";
		this.AddToFavoritesQuestion = "Вы действительно хотите добавить в избранное этого игрока?";
		this.AddToFavoritesHint = "Вы сможете отслеживать его в рейтинге и списке игр";
		this.RemoveFromFavoritesQuestion = "Вы действительно хотите убрать из избранного этого игрока?";
		this.AllowToAddMe = "Разрешить добавлять меня в избранное";
		this.AddToFavoritesLimitReached = "Максимальное количество игроков в избранном (200) достигнуто. Удалите кого-нибудь из своего списка избранных игроков и попробуйте еще раз.";
		this.AddToFavoritesDeniedByUser = "Игрок запретил добавлять себя в избранное.";
		this.HintRatingBtnTop = "Топ 300";
		this.SeasonTop = "Сезон Топ 300";
		this.Season = "СЕЗОН";
		this.HintRatingBtnHardcore = "Топ хардкор";
		this.HintRatingBtnTopOnline = "Топ онлайн";
		this.HintRatingBtnTopFriends = "Топ друзей";
		this.HintRatingBtnTopYourPosition = "Ваша позиция";
		this.HintRatingBtnFavorites = "Избранное";
		this.HintRatingBtnRefresh = "Обновить";
		this.HintRatingBtnInfo = "Сводка";
		this.HintRatingBtnAddToFavorites = "В избранное";
		this.WatchlistLoadingTitle = "Загрузка избранного";
		this.WatchlistLoadingBody = "Идет загрузка списка избранных игроков";
		this.WatchlistLoadedBody = "Список избранных загржен";
		this.BankTransactions = "История переводов";
		this.BankTransactionsLoading = "Идет загрузка истории переводов";
		this.BankTransactionsLoaded = "Загрузка истории переводов закончена";
		this.RepairAllWeaponPopupHeader = "Починка всего оружия";
		this.RepairAllWeaponPopupBody1 = "Вы действительно хотите починить все оружие?";
		this.RepairAllWeaponPopupBody2 = "Стоимость ремонта";
		this.ProfileReset = "Сброс профиля";
		this.ProfileResetNotification = "Сброс профиля доступен игрокам с 20 по 59 уровень";
		this.ProfileResetConfirmation0 = "Вы действительно хотите сбросить профиль?";
		this.ProfileResetConfirmation1 = "Внимание! Весь прогресс будет потерян.";
		this.ProfileResetConfirmation2 = "При злоумышленном обнулении профиля, его можно будет восстановить";
		this.DownloadWeapons = "Загружается оружие";
		this.WeaponsLoaded = "Все оружие загружено";
		this.DownloadMaps = "Загружаются карты";
		this.MapsLoaded = "Все карты загружены";
		this.ReceivingInformation = "Получение информации о кэше";
		this.ErrorDownloadingContent = "Часть контента не загрузилась";
		this.WeaponSizeLoaded = "Оружие ";
		this.SizeTotal = "Из ";
		this.DownloadAllWeapons = "Загрузить все оружие";
		this.MapsSizeLoaded = "Карты ";
		this.DownloadAllMaps = "Звгрузить все карты";
		this.MasteringPopupMetaBuyHeader = "Покупка метауровня";
		this.MasteringPopupMetaBuyBody = "Вы действительно хотите купить метауровень?";
		this.MasteringPopupModBuyHeader = "Покупка мода";
		this.MasteringPopupModBuyBody = "Вы действительно хотите купить мод?";
		this.Save = "Сохранить";
		this.MasteringPopupSaveModHeader = "Сохранение";
		this.MasteringPopupSaveModProcess = "Идет сохранение модификаций";
		this.MasteringPopupSaveModComplete = "Сохранение модификаций завершено";
		this.MasteringPopupSaveModError = "При сохранении модификаций произошла ошибка";
		this.MasteringPopupModBuyProcess = "Доставка мода";
		this.MasteringPopupModBuyComplete = "Мод доставлен";
		this.MasteringPopupMetaBuyProcess = "Получаем доступ к мета-уровню";
		this.MasteringPopupMetaBuyComplete = "Доступ к мета-уровню получен";
		this.MasteringNotEnoughMp = "Недостаточно MP";
		this.NotEnoughGp = "Недостаточно GP";
		this.NotEnoughCr = "Недостаточно CR";
		this.Sight = "Прицел";
		this.MuzzleDevice = "Дульное устройство";
		this.TacticalDevice = "Тактическое устройство";
		this.AmmoType = "Тип боеприпасов";
		this.ModSlotUnavailable = "Слот недоступен";
		this.MasteringWtaskHint = "Для начала кастомизации выполните w-task";
		this.MasteringWeaponExp = "Опыт на данном оружии";
		this.WeaponCustomization = "Кастомизация оружия";
		this.MpBuyError = "При попытке купить MP произошла ошибка";
		this.Purchase = "купить";
		this.GetFree = "получить";
		this.FreeCamouflage = "Вы можете получить этот камуфляж бесплатно";
		this.PopupCamouflageBuyHeader = "Покупка камуфляжа";
		this.PopupCamouflageBuyBody = "Вы действительно хотите купить камуфляж?";
		this.PopupCamouflageBuyComplete = "Камуфляж доставлен";
		this.PopupCamouflageBuyProcess = "Доставка камуфляжа";
		this.Equipped = "ВЫБРАН";
		this.SortByName = "сортировать по имени";
		this.SortByPrice = "сортировать по цене";
		this.RouletteDiscount1 = "Вы выиграли бонус к GP на ";
		this.RouletteDiscount2Part1 = "Получите ";
		this.RouletteDiscount2Part2 = " к сумме GP один раз при следующем пополнении баланса";
		this.RouletteDiscount3Part1 = "через ";
		this.RouletteDiscount3Part2 = " или после платежа бонус будет аннулирован";
		this.Bonus = "БОНУС";
		this.Achievement = "Достижение";
		this.StandaloneLoginCaption = "Войдите в учетную запись";
		this.StandaloneMailTextFieldCaption = "Email пользователя";
		this.StandalonePassTextFieldCaption = "Пароль";
		this.StandaloneSignUp = "Регистрация";
		this.StandaloneSignIn = "Войти";
		this.WrongLoginData = "Неправильный Email пользователя или пароль";
		this.ShowPassword = "Показать пароль";
		this.RetypePassword = "Пожалуйста введите пароль повторно";
		this.LoginAttemptsExceeded = "Лимит попток превышен. Следующая попытка через: ";
		this.Seconds = "секунд";
		this.UnknownReasonLoginFail = "Ошибка входа: неизвестная причина.";
		this.TransferProfile = "ПРИВЯЗАТЬ ПРОФИЛЬ";
		this.ProfileTransferedSuccessfully = "Профиль перенесен. Хэш для регистрации: ";
		this.ProfileAlreadyTransfered = "Профиль уже перенесен";
		this.ProfileTransferError = "Ошибка переноса профиля";
		this.NewLevel = "НОВЫЙ УРОВЕНЬ";
		this.PressToPost = "Нажмите чтобы запостить";
		this.Copy = "СКОПИРОВАТЬ";
		this.Exit = "ВЫХОД";
		this.VersionCheckCaption = "Проверка версии";
		this.VersionCheckDescription = "Доступна новая версия игры. Перезапустите лаунчер, чтобы скачать";
		this.Language = "Язык";
		this.RusLanguage = "Русский";
		this.EngLanguage = "Английский";
		this.SavePassword = "Сохранить пароль";
		this.AreYouSure = "ВЫ УВЕРЕНЫ?";
		this.AwardsHints = new string[]
		{
			"Альфа-тестер",
			"Бета-тестер",
			"Разработчик",
			"Забанен",
			"Особая благодарность от разработчиков",
			"Блэк Дивижен",
			"Первое место в сезоне",
			"TOP 10 сезона",
			"TOP 50 сезона",
			"TOP 100 сезона",
			"TOP 300 сезона",
			"Первое место в хардкор сезоне",
			"TOP 10 хардкор сезона",
			"TOP 50 хардкор сезона",
			"TOP 100 хардкор сезона",
			"TOP 300 хардкор сезона"
		};
		this.SeasonAward = "Сезонная награда";
		this.SeasonAwardDescription = "По результатам сезонной гонки вы получаете награду:";
		this.MipmapCheckFailCaption = "Ошибка запуска игры";
		this.MipmapCheckFailDescription = "Было обнаружено изменение настроек графики, при помощи стороннего ПО.";
		this.HopsKeyWonCaption = "Вы выиграли ключ от Hired Operations!";
		this.HopsKeyWonDescription = "Поздравляем, вы выиграли ключ Hired Ops, активируйте его в Steam, чтобы начать играть!\n\n";
		this.HopsActivationInstruction = "Инструкция по активации";
		base..ctor();
	}

	// Token: 0x17000341 RID: 833
	// (get) Token: 0x06001591 RID: 5521 RVA: 0x000E6580 File Offset: 0x000E4780
	public virtual ELanguage Lang
	{
		get
		{
			return ELanguage.RU;
		}
	}

	// Token: 0x06001592 RID: 5522 RVA: 0x000E6584 File Offset: 0x000E4784
	public virtual void SetLanguage()
	{
	}

	// Token: 0x06001593 RID: 5523 RVA: 0x000E6588 File Offset: 0x000E4788
	public string GetField(string name)
	{
		FieldInfo field = base.GetType().GetField(name);
		if (field != null)
		{
			return (string)field.GetValue(this);
		}
		return string.Empty;
	}

	// Token: 0x06001594 RID: 5524 RVA: 0x000E65BC File Offset: 0x000E47BC
	public void ReAssembleMass()
	{
		this.CarrClassName = new string[]
		{
			this.CarrClassName_Scout,
			this.CarrClassName_Shtormtrooper,
			this.CarrClassName_Destroyer,
			this.CarrClassName_Sniper,
			this.CarrClassName_Armorer,
			this.CarrClassName_Careerist
		};
		this.GameModeDescription = new string[]
		{
			this.GameModeDescription_DM,
			this.GameModeDescription_TDS,
			this.GameModeDescription_TE,
			this.GameModeDescription_TC
		};
		this.QuickGameDescrCutted = new string[]
		{
			this.QuickGameDescrCutted_0,
			this.QuickGameDescrCutted_1
		};
		this.MHGtabs = new string[]
		{
			this.MHGtabs_Menu,
			this.MHGtabs_Weapons,
			this.MHGtabs_Skills,
			this.MHGtabs_Battle
		};
		this.MHGDescr = new string[]
		{
			this.MHGDescr_0,
			this.MHGDescr_1,
			this.MHGDescr_2,
			this.MHGDescr_3,
			this.MHGDescr_4,
			this.MHGDescr_5,
			this.MHGDescr_6,
			this.MHGDescr_7,
			this.MHGDescr_8,
			this.MHGDescr_9,
			this.MHGDescr_10,
			this.MHGDescr_11,
			this.MHGDescr_12,
			this.MHGDescr_13,
			this.MHGDescr_14,
			this.MHGDescr_15,
			this.MHGDescr_16,
			this.MHGDescr_17,
			this.MHGDescr_18,
			this.MHGDescr_19,
			this.MHGDescr_20,
			this.MHGDescr_21,
			this.MHGDescr_22,
			this.MHGDescr_23,
			this.MHGDescr_24,
			this.MHGDescr_25,
			this.MHGDescr_26,
			this.MHGDescr_27,
			this.MHGDescr_28,
			this.MHGDescr_29,
			this.MHGDescr_30,
			this.MHGDescr_31,
			this.MHGDescr_32,
			this.MHGDescr_33,
			this.MHGDescr_34,
			this.MHGDescr_35,
			this.MHGDescr_36,
			this.MHGDescr_37,
			this.MHGDescr_38,
			this.MHGDescr_39,
			this.MHGDescr_40,
			this.MHGDescr_41,
			this.MHGDescr_42,
			this.MHGDescr_43,
			this.MHGDescr_44,
			this.MHGDescr_45,
			this.MHGDescr_46,
			this.MHGDescr_47,
			this.MHGDescr_48,
			this.MHGDescr_49,
			this.MHGDescr_50,
			this.MHGDescr_51,
			this.MHGDescr_52,
			this.MHGDescr_53,
			this.MHGDescr_54,
			this.MHGDescr_55,
			this.MHGDescr_56,
			this.MHGDescr_57,
			this.MHGDescr_58,
			this.MHGDescr_59,
			this.MHGDescr_60,
			this.MHGDescr_61,
			this.MHGDescr_62,
			this.MHGDescr_63,
			this.MHGDescr_64,
			this.MHGDescr_65,
			this.MHGDescr_66,
			this.MHGDescr_67,
			this.MHGDescr_68,
			this.MHGDescr_69,
			this.MHGDescr_70,
			this.MHGDescr_71,
			this.MHGDescr_72,
			this.MHGDescr_73,
			this.MHGDescr_74,
			this.MHGDescr_75,
			this.MHGDescr_76,
			this.MHGDescr_77,
			this.MHGDescr_78,
			this.MHGDescr_79,
			this.MHGDescr_80,
			this.MHGDescr_81,
			this.MHGDescr_82,
			this.MHGDescr_83,
			this.MHGDescr_84,
			this.MHGDescr_85
		};
		this.PointName = new string[]
		{
			this.PointName_none,
			this.PointName_Base,
			this.PointName_Lighthouse,
			this.PointName_Station,
			this.PointName_Tunnel
		};
		this.ClassName = new string[]
		{
			this.CarrClassName_Scout,
			this.CarrClassName_Shtormtrooper,
			this.CarrClassName_Destroyer,
			this.CarrClassName_Sniper,
			this.CarrClassName_Armorer,
			this.CarrClassName_Careerist
		};
		string[,] array = new string[4, 2];
		array[0, 0] = this.GameModeDescrCutted_DM_0;
		array[0, 1] = this.GameModeDescrCutted_DM_1;
		array[1, 0] = this.GameModeDescrCutted_TDS_0;
		array[1, 1] = this.GameModeDescrCutted_TDS_1;
		array[2, 0] = this.GameModeDescrCutted_TE_0;
		array[2, 1] = this.GameModeDescrCutted_TE_1;
		array[3, 0] = this.GameModeDescrCutted_TC_0;
		array[3, 1] = this.GameModeDescrCutted_TC_1;
		this.GameModeDescrCutted = array;
		this.HallOfFameHeader = new string[]
		{
			this.HallOfFameHeader_BestPoints,
			this.HallOfFameHeader_BestWins,
			this.HallOfFameHeader_BestAchievements,
			this.HallOfFameHeader_BestHeadshots,
			this.HallOfFameHeader_BestKD,
			this.HallOfFameHeader_BestAccuracy,
			this.HallOfFameHeader_BestPumped,
			this.HallOfFameHeader_BestBuyer,
			this.HallOfFameHeader_BestOnlineTime,
			this.HallOfFameHeader_BestContracts,
			this.HallOfFameHeader_BestProKills,
			this.HallOfFameHeader_BestAssistant
		};
	}

	// Token: 0x06001595 RID: 5525 RVA: 0x000E6B60 File Offset: 0x000E4D60
	public virtual string PressM(KeyCode button)
	{
		return string.Empty;
	}

	// Token: 0x06001596 RID: 5526 RVA: 0x000E6B68 File Offset: 0x000E4D68
	public virtual string Press3Mortar(KeyCode button)
	{
		return string.Empty;
	}

	// Token: 0x06001597 RID: 5527 RVA: 0x000E6B70 File Offset: 0x000E4D70
	public virtual string PressMouse2(KeyCode button)
	{
		return string.Empty;
	}

	// Token: 0x040019C0 RID: 6592
	public string[] GameModeDescription = new string[]
	{
		"Deathmatch (DM) - режим игры Все Против Всех. Побеждает набравший максимальное количество очков опыта.",
		"TargetDesignation (TDS) - режим игры Засветка Цели. Одна команда должна уничтожить цель, установив маяк. Другая должна не дать его установить.",
		"TeamElimination (TE) - режим игры Командное Уничтожение. Противостояние одной команды против другой с участием VIP бойцов.",
		"TacticalConquest (TC) - режим игры Завоевание. Удержание тактических точек."
	};

	// Token: 0x040019C1 RID: 6593
	public string[] CarrClassName = new string[]
	{
		"РАЗВЕДЧИК",
		"ШТУРМОВИК",
		"РАЗРУШИТЕЛЬ",
		"СНАЙПЕР",
		"ОРУЖЕЙНИК",
		"КАРЬЕРИСТ"
	};

	// Token: 0x040019C2 RID: 6594
	public string[] QuickGameDescrCutted = new string[]
	{
		"Выберите предпочитаемый режим и(или) карту, либо оставьте все как",
		"есть и подключитесь к случайной игре."
	};

	// Token: 0x040019C3 RID: 6595
	public string[] MHGtabs = new string[]
	{
		"МЕНЮ",
		"ОРУЖИЕ",
		"УМЕНИЯ",
		"БОЙ"
	};

	// Token: 0x040019C4 RID: 6596
	public string[] MHGDescr = new string[]
	{
		"Блок кнопок главного меню",
		"Карьера это главное в жизни каждого бойца ЧВК",
		"Звание, псевдоним игрока",
		"Текущий показатель опыта",
		"Прокачивайтесь и получайте новые звания",
		"Проценты выполнения контрактов",
		"Получайте награды за выполнение ежедневных заданий",
		"Текущий баланс игрока",
		"SP - это очки умений",
		"Кнопка сохранения комплекта",
		"Не забывайте на нее жать после выбора оружия!",
		"Кнопка-сигнализатор умений",
		"Когда она мигает значит у вас есть доступные очки умений. По клику открывает окно умений в карьере",
		"Прогресс оружейных заданий (W-TASK) выбранного оружейного сета",
		"Выбранный комплект снаряжения",
		"В бою вы не сможете свободно менять оружие. Выбирайте и сохраняйте необходимые комплекты снаряжения заранее",
		"Кнопка режима “Задания”",
		"Нажмите для просмотра текущих оружейных заданий и их прогресса выполнения",
		"“Социальное” оружие",
		"Приглашайте друзей, чтобы получить особое оружие",
		"Время до истечения аренды",
		"Выбранное основное оружие",
		"Премиумное оружие, покупаемое за валюту GP",
		"Кнопки переключения оружейных наборов (сетов)",
		"Заблокированное оружие (необходиы кредиты для покупки)",
		"Выбранное вторичное оружие (пистолет)",
		"Кнопка перехода в полноэкранный режим",
		"Также, чтобы перейти в полноэкранный режим, вы можете нажать клавишу F12",
		"Кнопка вкл/выкл обвеса",
		"После того, как оружейное заданий выполнено, вы можете активировать модифицированную версию оружия",
		"Характеристики оружия",
		"Цветными полосками показаны влияния обвесов (зеленые) и умений (синие) на базовые характеристики. Красной полоской обозначается отрицательное влияние от обвесов (ухудшение точности, урона и т.п.)",
		"Характеристика боевой дальности оружия",
		"По вертикальной оси - единицы урона, по горизонтальной - расстояние. График характеризует падение урона от расстояния",
		"Прогресс оружейного задания",
		"Характеристики обвеса",
		"Всплывающее окно W-TASK’a",
		"Полоса прогресса выполнения",
		"Кнопка досрочного выполнения W-TASK’а",
		"Оружие в режиме отображения заданий W-TASK’ов",
		"Кнопка ремонта",
		"Кнопка трехмерного просмотра оружия",
		"Техническое состояние оружия в единицах прочности",
		"Полоса технического состояния оружия",
		"Информация о ходе матча",
		"Зона текстовых оповещений",
		"Здесь показывается чат, информация о первенстве в матче, значимых событиях у игроков и т.п.",
		"Отображении позиции врага",
		"Необходимо умение “Гарантированное определение”",
		"Зона различных влияний на игрока",
		"Здесь показываюстя значки эффектов-бафферов - регенерация, урон от огня ,временная неуязвимость и т.п.",
		"Здоровье",
		"Броня",
		"Отображение позиции брошенной гранаты",
		"Необходимо умение “Сигнализатор опасности”",
		"Зона отображения информации о том, кто вас убил, или кого вы убили",
		"Название оружия в руках",
		"Количество патронов в магазине",
		"Индикатор режима огня/поломки",
		"Количество патронов в запасе",
		"Кол-во доступных гранат",
		"Необходимо умение “Осколочная граната EFD”",
		"Зона отображения доступных средств поддержки",
		"Для выбора средства нажмите клавишу “3”",
		"Зона отображения информации о прогрессе выполнения W-TASK, достижений и контрактов",
		"Сводка по убийствам",
		"Показываются никнеймы, оружие из которого убили и тип убийства",
		"Событие",
		"Убийства, хедшоты, лонгшоты, серии убийств, награды",
		"Кол-во полученного опыта",
		"Сектор обстрела",
		"Необходим навык “Внимательность”",
		"Тактическая миникарта",
		"Кнопки-переключатели веток умений различных классов",
		"Умение, доступное для разблокировки",
		"Разблокированное умение",
		"Умение, недоступное для покупки",
		"Премиумное умение",
		"Детальная информация о выбранном умении",
		"Прогресс прокачки персонажа",
		"Выделенный синей полосой класс определяет специализацию вашего бойца ( в данном случае Разведчик)",
		"Кнопка сброса умений",
		"Возвращает потраченные очки умений SP",
		"Кнопка пополнения баланса",
		"Доступные очки умений",
		"Зарабатывайте очки умений, получая новые звания, выполняя контракты"
	};

	// Token: 0x040019C5 RID: 6597
	public string[] PointName = new string[]
	{
		"none",
		"БАЗА",
		"МАЯК",
		"СТАНЦИЯ",
		"ТОННЕЛЬ"
	};

	// Token: 0x040019C6 RID: 6598
	public string[] ClassName = new string[]
	{
		"РАЗВЕДЧИК",
		"ШТУРМОВИК",
		"РАЗРУШИТЕЛЬ",
		"СНАЙПЕР",
		"ОРУЖЕЙНИК",
		"КАРЬЕРИСТ"
	};

	// Token: 0x040019C7 RID: 6599
	public string[,] GameModeDescrCutted;

	// Token: 0x040019C8 RID: 6600
	public string[] HallOfFameHeader;

	// Token: 0x040019C9 RID: 6601
	public string CWMainLoading;

	// Token: 0x040019CA RID: 6602
	public string CWMainGlobalInfoLoading;

	// Token: 0x040019CB RID: 6603
	public string CWMainGlobalInfoLoadingFinished;

	// Token: 0x040019CC RID: 6604
	public string CWMainLoginDesc;

	// Token: 0x040019CD RID: 6605
	public string CWMainLoginFinishedDesc;

	// Token: 0x040019CE RID: 6606
	public string CWMainInitUserDesc;

	// Token: 0x040019CF RID: 6607
	public string CWMainInitUserFinishedDesc;

	// Token: 0x040019D0 RID: 6608
	public string CWMainLoad;

	// Token: 0x040019D1 RID: 6609
	public string CWMainLoadDesc;

	// Token: 0x040019D2 RID: 6610
	public string CWMainLoadFinishedDesc;

	// Token: 0x040019D3 RID: 6611
	public string CWMainLoadError;

	// Token: 0x040019D4 RID: 6612
	public string CWMainLoadErrorDesc;

	// Token: 0x040019D5 RID: 6613
	public string CWMainSave;

	// Token: 0x040019D6 RID: 6614
	public string CWMainSaveDesc;

	// Token: 0x040019D7 RID: 6615
	public string CWMainSaveFinishedDesc;

	// Token: 0x040019D8 RID: 6616
	public string CWMainSaveError;

	// Token: 0x040019D9 RID: 6617
	public string CWMainSaveErrorDesc;

	// Token: 0x040019DA RID: 6618
	public string CWMainWeaponUnlock;

	// Token: 0x040019DB RID: 6619
	public string CWMainWeaponUnlockDesc;

	// Token: 0x040019DC RID: 6620
	public string CWMainWeaponUnlockFinishedDesc;

	// Token: 0x040019DD RID: 6621
	public string CWMainWeaponUnlockError;

	// Token: 0x040019DE RID: 6622
	public string CWMainWeaponUnlockErrorDesc;

	// Token: 0x040019DF RID: 6623
	public string CWMainSkillUnlock;

	// Token: 0x040019E0 RID: 6624
	public string CWMainSkillUnlockDesc;

	// Token: 0x040019E1 RID: 6625
	public string CWMainSkillUnlockFinishedDesc;

	// Token: 0x040019E2 RID: 6626
	public string CWMainSkillUnlockError;

	// Token: 0x040019E3 RID: 6627
	public string CWMainSkillUnlockErrorDesc;

	// Token: 0x040019E4 RID: 6628
	public string CWMainLoadRating;

	// Token: 0x040019E5 RID: 6629
	public string CWMainLoadRatingDesc;

	// Token: 0x040019E6 RID: 6630
	public string CWMainLoadRatingFinishedDesc;

	// Token: 0x040019E7 RID: 6631
	public string CWMainLoadRatingError;

	// Token: 0x040019E8 RID: 6632
	public string CWMainLoadRatingErrorDesc;

	// Token: 0x040019E9 RID: 6633
	public string DownloadAdditionalGameDataDesc;

	// Token: 0x040019EA RID: 6634
	public string Error;

	// Token: 0x040019EB RID: 6635
	public string Connection;

	// Token: 0x040019EC RID: 6636
	public string TryingConnection;

	// Token: 0x040019ED RID: 6637
	public string ConnectionFailed;

	// Token: 0x040019EE RID: 6638
	public string ConnectionFailedOnDelay;

	// Token: 0x040019EF RID: 6639
	public string ConnectionCompleted;

	// Token: 0x040019F0 RID: 6640
	public string ServerDisconnectYou;

	// Token: 0x040019F1 RID: 6641
	public string FailedToConnectToMasterServer;

	// Token: 0x040019F2 RID: 6642
	public string ServerCreation;

	// Token: 0x040019F3 RID: 6643
	public string ServerCreationFailed;

	// Token: 0x040019F4 RID: 6644
	public string LoadingProfile;

	// Token: 0x040019F5 RID: 6645
	public string LoadingBuild;

	// Token: 0x040019F6 RID: 6646
	public string LoadingProfileFailed;

	// Token: 0x040019F7 RID: 6647
	public string LoadingProfileCheckConnection;

	// Token: 0x040019F8 RID: 6648
	public string LoadingProfileCheckSoftware;

	// Token: 0x040019F9 RID: 6649
	public string LoadingProfileReloadApplication;

	// Token: 0x040019FA RID: 6650
	public string BannerGUIKnife;

	// Token: 0x040019FB RID: 6651
	public string BannerGUIGrenade;

	// Token: 0x040019FC RID: 6652
	public string BannerGUIMortarStrike;

	// Token: 0x040019FD RID: 6653
	public string Push;

	// Token: 0x040019FE RID: 6654
	public string NeedMoreWarrior;

	// Token: 0x040019FF RID: 6655
	public string CapturingPoint;

	// Token: 0x04001A00 RID: 6656
	public string EnemyCapturingYourPoint;

	// Token: 0x04001A01 RID: 6657
	public string NeutralizePoint;

	// Token: 0x04001A02 RID: 6658
	public string FriendCaptured;

	// Token: 0x04001A03 RID: 6659
	public string EnemyCaptured;

	// Token: 0x04001A04 RID: 6660
	public string PlayerRecieveSonar;

	// Token: 0x04001A05 RID: 6661
	public string PlayerRecieveMortar;

	// Token: 0x04001A06 RID: 6662
	public string PlayerPlacedMarker;

	// Token: 0x04001A07 RID: 6663
	public string PlayerDifuseMarker;

	// Token: 0x04001A08 RID: 6664
	public string PlayerMakeStormKill;

	// Token: 0x04001A09 RID: 6665
	public string PlayerMakeProKill;

	// Token: 0x04001A0A RID: 6666
	public string PlayerMakeLegendaryKill;

	// Token: 0x04001A0B RID: 6667
	public string PlayerGainedAchivment;

	// Token: 0x04001A0C RID: 6668
	public string PlayerCapturedPoint;

	// Token: 0x04001A0D RID: 6669
	public string AutoTeamBalance;

	// Token: 0x04001A0E RID: 6670
	public string ServerRestarting;

	// Token: 0x04001A0F RID: 6671
	public string PlayerConnected;

	// Token: 0x04001A10 RID: 6672
	public string PlayerDisconnected;

	// Token: 0x04001A11 RID: 6673
	public string SpecChangeCam_Spacebar;

	// Token: 0x04001A12 RID: 6674
	public string SpecChangeCam_MidleButton;

	// Token: 0x04001A13 RID: 6675
	public string SpecPressMToSelectTeam;

	// Token: 0x04001A14 RID: 6676
	public string SpecViewaAt;

	// Token: 0x04001A15 RID: 6677
	public string SpecChooseTeam;

	// Token: 0x04001A16 RID: 6678
	public string SpecPremium;

	// Token: 0x04001A17 RID: 6679
	public string SpecGamers;

	// Token: 0x04001A18 RID: 6680
	public string SpecSpectator;

	// Token: 0x04001A19 RID: 6681
	public string SpecAutobalance;

	// Token: 0x04001A1A RID: 6682
	public string SpecBEARWins;

	// Token: 0x04001A1B RID: 6683
	public string SpecUSECWins;

	// Token: 0x04001A1C RID: 6684
	public string SpecWin;

	// Token: 0x04001A1D RID: 6685
	public string SpecLoose;

	// Token: 0x04001A1E RID: 6686
	public string SpecServerIsFull;

	// Token: 0x04001A1F RID: 6687
	public string SpecRessurectAfter;

	// Token: 0x04001A20 RID: 6688
	public string Space;

	// Token: 0x04001A21 RID: 6689
	public string SpecForCycleCamChanged;

	// Token: 0x04001A22 RID: 6690
	public string MMB;

	// Token: 0x04001A23 RID: 6691
	public string ForCamChanged;

	// Token: 0x04001A24 RID: 6692
	public string LMB;

	// Token: 0x04001A25 RID: 6693
	public string SpecForBeginGame;

	// Token: 0x04001A26 RID: 6694
	public string SpecForChooseTeam;

	// Token: 0x04001A27 RID: 6695
	public string SpecCantChangeTeam;

	// Token: 0x04001A28 RID: 6696
	public string SpecTeamIsOverpowered;

	// Token: 0x04001A29 RID: 6697
	public string SpecWaitForBeginRound;

	// Token: 0x04001A2A RID: 6698
	public string No;

	// Token: 0x04001A2B RID: 6699
	public string NoSmall;

	// Token: 0x04001A2C RID: 6700
	public string StartGame;

	// Token: 0x04001A2D RID: 6701
	public string PlayerAutoBallanced;

	// Token: 0x04001A2E RID: 6702
	public string killedVIP;

	// Token: 0x04001A2F RID: 6703
	public string VIPInYourTeam;

	// Token: 0x04001A30 RID: 6704
	public string VIPInEnemyTeam;

	// Token: 0x04001A31 RID: 6705
	public string GameModeDescription_DM;

	// Token: 0x04001A32 RID: 6706
	public string GameModeDescription_TDS;

	// Token: 0x04001A33 RID: 6707
	public string GameModeDescription_TE;

	// Token: 0x04001A34 RID: 6708
	public string GameModeDescription_TC;

	// Token: 0x04001A35 RID: 6709
	public string GameModeDescrCutted_DM_0;

	// Token: 0x04001A36 RID: 6710
	public string GameModeDescrCutted_DM_1;

	// Token: 0x04001A37 RID: 6711
	public string GameModeDescrCutted_TDS_0;

	// Token: 0x04001A38 RID: 6712
	public string GameModeDescrCutted_TDS_1;

	// Token: 0x04001A39 RID: 6713
	public string GameModeDescrCutted_TE_0;

	// Token: 0x04001A3A RID: 6714
	public string GameModeDescrCutted_TE_1;

	// Token: 0x04001A3B RID: 6715
	public string GameModeDescrCutted_TC_0;

	// Token: 0x04001A3C RID: 6716
	public string GameModeDescrCutted_TC_1;

	// Token: 0x04001A3D RID: 6717
	public string QuickGameDescrCutted_0;

	// Token: 0x04001A3E RID: 6718
	public string QuickGameDescrCutted_1;

	// Token: 0x04001A3F RID: 6719
	public string PlayerQuit;

	// Token: 0x04001A40 RID: 6720
	public string HallOfFameHeader_BestPoints;

	// Token: 0x04001A41 RID: 6721
	public string HallOfFameHeader_BestWins;

	// Token: 0x04001A42 RID: 6722
	public string HallOfFameHeader_BestAchievements;

	// Token: 0x04001A43 RID: 6723
	public string HallOfFameHeader_BestHeadshots;

	// Token: 0x04001A44 RID: 6724
	public string HallOfFameHeader_BestKD;

	// Token: 0x04001A45 RID: 6725
	public string HallOfFameHeader_BestAccuracy;

	// Token: 0x04001A46 RID: 6726
	public string HallOfFameHeader_BestPumped;

	// Token: 0x04001A47 RID: 6727
	public string HallOfFameHeader_BestBuyer;

	// Token: 0x04001A48 RID: 6728
	public string HallOfFameHeader_BestOnlineTime;

	// Token: 0x04001A49 RID: 6729
	public string HallOfFameHeader_BestContracts;

	// Token: 0x04001A4A RID: 6730
	public string HallOfFameHeader_BestProKills;

	// Token: 0x04001A4B RID: 6731
	public string HallOfFameHeader_BestAssistant;

	// Token: 0x04001A4C RID: 6732
	public string anyMale;

	// Token: 0x04001A4D RID: 6733
	public string anyFemale;

	// Token: 0x04001A4E RID: 6734
	public string Completed;

	// Token: 0x04001A4F RID: 6735
	public string magazPo;

	// Token: 0x04001A50 RID: 6736
	public string patr;

	// Token: 0x04001A51 RID: 6737
	public string ReturnToTheGame;

	// Token: 0x04001A52 RID: 6738
	public string ExitFromTheServer;

	// Token: 0x04001A53 RID: 6739
	public string QuickPlay;

	// Token: 0x04001A54 RID: 6740
	public string SearchGames;

	// Token: 0x04001A55 RID: 6741
	public string Settings;

	// Token: 0x04001A56 RID: 6742
	public string Career;

	// Token: 0x04001A57 RID: 6743
	public string Help;

	// Token: 0x04001A58 RID: 6744
	public string GatherTheTeam;

	// Token: 0x04001A59 RID: 6745
	public string GetGoldPoints;

	// Token: 0x04001A5A RID: 6746
	public string GetGpNow;

	// Token: 0x04001A5B RID: 6747
	public string FriendsInvited;

	// Token: 0x04001A5C RID: 6748
	public string ThePurchaseOfModification;

	// Token: 0x04001A5D RID: 6749
	public string ProgressDailyContracts;

	// Token: 0x04001A5E RID: 6750
	public string SetWeapons;

	// Token: 0x04001A5F RID: 6751
	public string LockedRequired;

	// Token: 0x04001A60 RID: 6752
	public string Level;

	// Token: 0x04001A61 RID: 6753
	public string EarlyAccessCaps;

	// Token: 0x04001A62 RID: 6754
	public string EarlyAccess;

	// Token: 0x04001A63 RID: 6755
	public string TextToOpenLastSet;

	// Token: 0x04001A64 RID: 6756
	public string UnlockingTheSet;

	// Token: 0x04001A65 RID: 6757
	public string ProfileNotLoaded;

	// Token: 0x04001A66 RID: 6758
	public string FillUpBalance;

	// Token: 0x04001A67 RID: 6759
	public string Set;

	// Token: 0x04001A68 RID: 6760
	public string Votes;

	// Token: 0x04001A69 RID: 6761
	public string BonusString0;

	// Token: 0x04001A6A RID: 6762
	public string BonusString3;

	// Token: 0x04001A6B RID: 6763
	public string BonusString10;

	// Token: 0x04001A6C RID: 6764
	public string BonusString20;

	// Token: 0x04001A6D RID: 6765
	public string BonusString60;

	// Token: 0x04001A6E RID: 6766
	public string BonusString61;

	// Token: 0x04001A6F RID: 6767
	public string BonusString62;

	// Token: 0x04001A70 RID: 6768
	public string BonusString63;

	// Token: 0x04001A71 RID: 6769
	public string BonusString64;

	// Token: 0x04001A72 RID: 6770
	public string BonusString65;

	// Token: 0x04001A73 RID: 6771
	public string BonusString66;

	// Token: 0x04001A74 RID: 6772
	public string BonusString67;

	// Token: 0x04001A75 RID: 6773
	public string BonusString68;

	// Token: 0x04001A76 RID: 6774
	public string BonusString69;

	// Token: 0x04001A77 RID: 6775
	public string BonusString70;

	// Token: 0x04001A78 RID: 6776
	public string TheTerm;

	// Token: 0x04001A79 RID: 6777
	public string Day;

	// Token: 0x04001A7A RID: 6778
	public string Days_dnya;

	// Token: 0x04001A7B RID: 6779
	public string Days_dney;

	// Token: 0x04001A7C RID: 6780
	public string D;

	// Token: 0x04001A7D RID: 6781
	public string H;

	// Token: 0x04001A7E RID: 6782
	public string M;

	// Token: 0x04001A7F RID: 6783
	public string S;

	// Token: 0x04001A80 RID: 6784
	public string Reason;

	// Token: 0x04001A81 RID: 6785
	public string FriendText;

	// Token: 0x04001A82 RID: 6786
	public string FollowByLink;

	// Token: 0x04001A83 RID: 6787
	public string InOtherNews;

	// Token: 0x04001A84 RID: 6788
	public string TextUnlockKit;

	// Token: 0x04001A85 RID: 6789
	public string UnlockFor;

	// Token: 0x04001A86 RID: 6790
	public string InsufficientFunds;

	// Token: 0x04001A87 RID: 6791
	public string TextUnlockSet;

	// Token: 0x04001A88 RID: 6792
	public string InsufficientFundsNeed;

	// Token: 0x04001A89 RID: 6793
	public string TextUnlockWTask;

	// Token: 0x04001A8A RID: 6794
	public string BuyModFor;

	// Token: 0x04001A8B RID: 6795
	public string RentSkill;

	// Token: 0x04001A8C RID: 6796
	public string Price;

	// Token: 0x04001A8D RID: 6797
	public string Cost;

	// Token: 0x04001A8E RID: 6798
	public string UnlockQuestion;

	// Token: 0x04001A8F RID: 6799
	public string Yes;

	// Token: 0x04001A90 RID: 6800
	public string TextResetSkills;

	// Token: 0x04001A91 RID: 6801
	public string ResetSkillYouGet;

	// Token: 0x04001A92 RID: 6802
	public string ResetSkillsFor;

	// Token: 0x04001A93 RID: 6803
	public string Or;

	// Token: 0x04001A94 RID: 6804
	public string ResetSkillAttention;

	// Token: 0x04001A95 RID: 6805
	public string EnterPassword;

	// Token: 0x04001A96 RID: 6806
	public string OK;

	// Token: 0x04001A97 RID: 6807
	public string PromoHello;

	// Token: 0x04001A98 RID: 6808
	public string PromoHelloForResourse0;

	// Token: 0x04001A99 RID: 6809
	public string PromoHelloForResourse1;

	// Token: 0x04001A9A RID: 6810
	public string PromoHelloForResourse2;

	// Token: 0x04001A9B RID: 6811
	public string HundredsOfOil;

	// Token: 0x04001A9C RID: 6812
	public string Forever;

	// Token: 0x04001A9D RID: 6813
	public string WellcomeToCWandGetBonus;

	// Token: 0x04001A9E RID: 6814
	public string CRGPSPEveryDay;

	// Token: 0x04001A9F RID: 6815
	public string Map;

	// Token: 0x04001AA0 RID: 6816
	public string Mode;

	// Token: 0x04001AA1 RID: 6817
	public string BuySPAttention;

	// Token: 0x04001AA2 RID: 6818
	public string BuySPFor;

	// Token: 0x04001AA3 RID: 6819
	public string GoToTheBattle;

	// Token: 0x04001AA4 RID: 6820
	public string GamesNotFound;

	// Token: 0x04001AA5 RID: 6821
	public string tventyMinutes;

	// Token: 0x04001AA6 RID: 6822
	public string discount;

	// Token: 0x04001AA7 RID: 6823
	public string discountGreeteng2;

	// Token: 0x04001AA8 RID: 6824
	public string NoSeats;

	// Token: 0x04001AA9 RID: 6825
	public string Later;

	// Token: 0x04001AAA RID: 6826
	public string JoinFight;

	// Token: 0x04001AAB RID: 6827
	public string AchievementWillNotCount;

	// Token: 0x04001AAC RID: 6828
	public string DMHeader;

	// Token: 0x04001AAD RID: 6829
	public string DMDescription;

	// Token: 0x04001AAE RID: 6830
	public string VIPHeader;

	// Token: 0x04001AAF RID: 6831
	public string VIPDescription;

	// Token: 0x04001AB0 RID: 6832
	public string TDSHeader;

	// Token: 0x04001AB1 RID: 6833
	public string TDSDescriptionB;

	// Token: 0x04001AB2 RID: 6834
	public string TDSDescriptionU;

	// Token: 0x04001AB3 RID: 6835
	public string TEHeader;

	// Token: 0x04001AB4 RID: 6836
	public string TEDescription;

	// Token: 0x04001AB5 RID: 6837
	public string TCHeader;

	// Token: 0x04001AB6 RID: 6838
	public string TCDescription;

	// Token: 0x04001AB7 RID: 6839
	public string YouKill;

	// Token: 0x04001AB8 RID: 6840
	public string YouKilled;

	// Token: 0x04001AB9 RID: 6841
	public string WTaskProgress;

	// Token: 0x04001ABA RID: 6842
	public string ProgressTowards;

	// Token: 0x04001ABB RID: 6843
	public string CallForSupport;

	// Token: 0x04001ABC RID: 6844
	public string BeginInstallingTheBeacon;

	// Token: 0x04001ABD RID: 6845
	public string ClearanceToStartBeacon;

	// Token: 0x04001ABE RID: 6846
	public string CompletedSmall;

	// Token: 0x04001ABF RID: 6847
	public string CarrClassName_Scout;

	// Token: 0x04001AC0 RID: 6848
	public string CarrClassName_Shtormtrooper;

	// Token: 0x04001AC1 RID: 6849
	public string CarrClassName_Destroyer;

	// Token: 0x04001AC2 RID: 6850
	public string CarrClassName_Sniper;

	// Token: 0x04001AC3 RID: 6851
	public string CarrClassName_Armorer;

	// Token: 0x04001AC4 RID: 6852
	public string CarrClassName_Careerist;

	// Token: 0x04001AC5 RID: 6853
	public string CarrClassified;

	// Token: 0x04001AC6 RID: 6854
	public string CarrUserForbidYourself;

	// Token: 0x04001AC7 RID: 6855
	public string CarrLVL;

	// Token: 0x04001AC8 RID: 6856
	public string CarrNameSet;

	// Token: 0x04001AC9 RID: 6857
	public string CarrToTheNextRank;

	// Token: 0x04001ACA RID: 6858
	public string CarrRank;

	// Token: 0x04001ACB RID: 6859
	public string CarrNextRank;

	// Token: 0x04001ACC RID: 6860
	public string CarrPlaceRanking;

	// Token: 0x04001ACD RID: 6861
	public string CarrAchievementComplete;

	// Token: 0x04001ACE RID: 6862
	public string CarrAchievementNext;

	// Token: 0x04001ACF RID: 6863
	public string CarrCollectCards;

	// Token: 0x04001AD0 RID: 6864
	public string CarrLastCollectedCard;

	// Token: 0x04001AD1 RID: 6865
	public string CarrWTaskComplete;

	// Token: 0x04001AD2 RID: 6866
	public string CarrContractsComplete;

	// Token: 0x04001AD3 RID: 6867
	public string CarrCurrentContract;

	// Token: 0x04001AD4 RID: 6868
	public string CarrSpecialBadges;

	// Token: 0x04001AD5 RID: 6869
	public string CarrSpecialBadge;

	// Token: 0x04001AD6 RID: 6870
	public string CarrProfile;

	// Token: 0x04001AD7 RID: 6871
	public string CarrTopFriends;

	// Token: 0x04001AD8 RID: 6872
	public string CarrTopClans;

	// Token: 0x04001AD9 RID: 6873
	public string CarrClans;

	// Token: 0x04001ADA RID: 6874
	public string CarrPlace;

	// Token: 0x04001ADB RID: 6875
	public string CarrTOP;

	// Token: 0x04001ADC RID: 6876
	public string CarrTAG;

	// Token: 0x04001ADD RID: 6877
	public string CarrPlayers;

	// Token: 0x04001ADE RID: 6878
	public string CarrName;

	// Token: 0x04001ADF RID: 6879
	public string CarrEXP;

	// Token: 0x04001AE0 RID: 6880
	public string CarrTop100lvl;

	// Token: 0x04001AE1 RID: 6881
	public string CarrItemName;

	// Token: 0x04001AE2 RID: 6882
	public string CarrPoints;

	// Token: 0x04001AE3 RID: 6883
	public string CarrTop100EXP;

	// Token: 0x04001AE4 RID: 6884
	public string CarrKills;

	// Token: 0x04001AE5 RID: 6885
	public string CarrTop100Kills;

	// Token: 0x04001AE6 RID: 6886
	public string CarrDeath;

	// Token: 0x04001AE7 RID: 6887
	public string CarrTop100Death;

	// Token: 0x04001AE8 RID: 6888
	public string CarrTop100KD;

	// Token: 0x04001AE9 RID: 6889
	public string CarrReputation;

	// Token: 0x04001AEA RID: 6890
	public string SeasonReward;

	// Token: 0x04001AEB RID: 6891
	public string CarrTop100Rep;

	// Token: 0x04001AEC RID: 6892
	public string CarrCardDescr;

	// Token: 0x04001AED RID: 6893
	public string CarrAvaliableAt;

	// Token: 0x04001AEE RID: 6894
	public string CarrMapName;

	// Token: 0x04001AEF RID: 6895
	public string CarrDaily;

	// Token: 0x04001AF0 RID: 6896
	public string CarrCurrentContractsCAPS;

	// Token: 0x04001AF1 RID: 6897
	public string CarrNextContractsCAPS;

	// Token: 0x04001AF2 RID: 6898
	public string CarrSkipContract;

	// Token: 0x04001AF3 RID: 6899
	public string CarrUpdateContractsPopupHeader;

	// Token: 0x04001AF4 RID: 6900
	public string CarrUpdateContractsPopupBody0;

	// Token: 0x04001AF5 RID: 6901
	public string CarrUpdateContractsPopupBody1;

	// Token: 0x04001AF6 RID: 6902
	public string CarrSkipContractPopupHeader;

	// Token: 0x04001AF7 RID: 6903
	public string CarrSkipContractPopupBody0;

	// Token: 0x04001AF8 RID: 6904
	public string CarrSkipContractPopupBody1;

	// Token: 0x04001AF9 RID: 6905
	public string CarrSkipContractPopupBody2;

	// Token: 0x04001AFA RID: 6906
	public string CarrContractRefreshDescr0;

	// Token: 0x04001AFB RID: 6907
	public string CarrContractRefreshDescr1;

	// Token: 0x04001AFC RID: 6908
	public string CarrContractRefreshDescr2;

	// Token: 0x04001AFD RID: 6909
	public string CarrContractRefreshDescr3;

	// Token: 0x04001AFE RID: 6910
	public string CarrOnlineTime;

	// Token: 0x04001AFF RID: 6911
	public string CarrHardcoreTime;

	// Token: 0x04001B00 RID: 6912
	public string CarrWins;

	// Token: 0x04001B01 RID: 6913
	public string CarrLoose;

	// Token: 0x04001B02 RID: 6914
	public string CarrDamage;

	// Token: 0x04001B03 RID: 6915
	public string CarrUsedBullets;

	// Token: 0x04001B04 RID: 6916
	public string CarrHeadShots;

	// Token: 0x04001B05 RID: 6917
	public string CarrDoubleHeadShots;

	// Token: 0x04001B06 RID: 6918
	public string CarrLongHeadShots;

	// Token: 0x04001B07 RID: 6919
	public string CarrDoubleKills;

	// Token: 0x04001B08 RID: 6920
	public string CarrTripleKills;

	// Token: 0x04001B09 RID: 6921
	public string CarrAssists;

	// Token: 0x04001B0A RID: 6922
	public string CarrCreditSpend;

	// Token: 0x04001B0B RID: 6923
	public string CarrWTaskOpened;

	// Token: 0x04001B0C RID: 6924
	public string CarrAchievingGetted;

	// Token: 0x04001B0D RID: 6925
	public string CarrArmstreakGetted;

	// Token: 0x04001B0E RID: 6926
	public string CarrSupportCaused;

	// Token: 0x04001B0F RID: 6927
	public string CarrKnifeKill;

	// Token: 0x04001B10 RID: 6928
	public string CarrGrenadeKill;

	// Token: 0x04001B11 RID: 6929
	public string CarrTeammateKill;

	// Token: 0x04001B12 RID: 6930
	public string CarrSuicides;

	// Token: 0x04001B13 RID: 6931
	public string CarrBEARKills;

	// Token: 0x04001B14 RID: 6932
	public string CarrUsecKills;

	// Token: 0x04001B15 RID: 6933
	public string CarrFavoriteWeapon;

	// Token: 0x04001B16 RID: 6934
	public string CarrTotalAccuracy;

	// Token: 0x04001B17 RID: 6935
	public string CarrMatchesCompleted;

	// Token: 0x04001B18 RID: 6936
	public string CarrStormtrooper;

	// Token: 0x04001B19 RID: 6937
	public string CarrDestroyer;

	// Token: 0x04001B1A RID: 6938
	public string CarrSniper;

	// Token: 0x04001B1B RID: 6939
	public string CarrArmorer;

	// Token: 0x04001B1C RID: 6940
	public string CarrCareerist;

	// Token: 0x04001B1D RID: 6941
	public string CarrCurrentBalance;

	// Token: 0x04001B1E RID: 6942
	public string CarrResetSkills;

	// Token: 0x04001B1F RID: 6943
	public string CarrBootSkills;

	// Token: 0x04001B20 RID: 6944
	public string CarrBonus;

	// Token: 0x04001B21 RID: 6945
	public string CarrRentTime;

	// Token: 0x04001B22 RID: 6946
	public string CarrUnlocked;

	// Token: 0x04001B23 RID: 6947
	public string CarrYouInTheBattle;

	// Token: 0x04001B24 RID: 6948
	public string CarrTemporarilyUnavailable;

	// Token: 0x04001B25 RID: 6949
	public string CarrSkills;

	// Token: 0x04001B26 RID: 6950
	public string CarrRating;

	// Token: 0x04001B27 RID: 6951
	public string CarrSummary;

	// Token: 0x04001B28 RID: 6952
	public string CarrAchievements;

	// Token: 0x04001B29 RID: 6953
	public string CarrContracts;

	// Token: 0x04001B2A RID: 6954
	public string CarrStatistics;

	// Token: 0x04001B2B RID: 6955
	public string BankGet;

	// Token: 0x04001B2C RID: 6956
	public string BankAvaliable;

	// Token: 0x04001B2D RID: 6957
	public string BankCostCAPS;

	// Token: 0x04001B2E RID: 6958
	public string BankBuySP;

	// Token: 0x04001B2F RID: 6959
	public string BankFor;

	// Token: 0x04001B30 RID: 6960
	public string BankVote;

	// Token: 0x04001B31 RID: 6961
	public string BankVote_golosov;

	// Token: 0x04001B32 RID: 6962
	public string BankVote_golosa;

	// Token: 0x04001B33 RID: 6963
	public string BankDiscount;

	// Token: 0x04001B34 RID: 6964
	public string BankCurrency;

	// Token: 0x04001B35 RID: 6965
	public string BankCurFS;

	// Token: 0x04001B36 RID: 6966
	public string BankCurMailru;

	// Token: 0x04001B37 RID: 6967
	public string BankCurOK;

	// Token: 0x04001B38 RID: 6968
	public string CWSAUpdateBalance;

	// Token: 0x04001B39 RID: 6969
	public string BankSPTitle;

	// Token: 0x04001B3A RID: 6970
	public string BankMPTitle;

	// Token: 0x04001B3B RID: 6971
	public string BankCRTitle;

	// Token: 0x04001B3C RID: 6972
	public string BankVKTitle0;

	// Token: 0x04001B3D RID: 6973
	public string BankVKTitle1;

	// Token: 0x04001B3E RID: 6974
	public string BankFRTitle0;

	// Token: 0x04001B3F RID: 6975
	public string BankFRTitle1;

	// Token: 0x04001B40 RID: 6976
	public string BankFBTitle0;

	// Token: 0x04001B41 RID: 6977
	public string BankFBTitle1;

	// Token: 0x04001B42 RID: 6978
	public string BankFSTitle0;

	// Token: 0x04001B43 RID: 6979
	public string BankFSTitle1;

	// Token: 0x04001B44 RID: 6980
	public string BankMailruTitle0;

	// Token: 0x04001B45 RID: 6981
	public string BankMailruTitle1;

	// Token: 0x04001B46 RID: 6982
	public string BankOKTitle0;

	// Token: 0x04001B47 RID: 6983
	public string BankOKTitle1;

	// Token: 0x04001B48 RID: 6984
	public string BankTransaction;

	// Token: 0x04001B49 RID: 6985
	public string BankHistory;

	// Token: 0x04001B4A RID: 6986
	public string BankServices;

	// Token: 0x04001B4B RID: 6987
	public string BankOperationHistory;

	// Token: 0x04001B4C RID: 6988
	public string BankQuantity;

	// Token: 0x04001B4D RID: 6989
	public string BankValuta;

	// Token: 0x04001B4E RID: 6990
	public string BankComment;

	// Token: 0x04001B4F RID: 6991
	public string BankDate;

	// Token: 0x04001B50 RID: 6992
	public string TabLoading;

	// Token: 0x04001B51 RID: 6993
	public string TabLeadingOnPoints;

	// Token: 0x04001B52 RID: 6994
	public string TabPing;

	// Token: 0x04001B53 RID: 6995
	public string TabSuspectPlayer;

	// Token: 0x04001B54 RID: 6996
	public string TabAlreadySuspectedPlayer;

	// Token: 0x04001B55 RID: 6997
	public string TabSuspected;

	// Token: 0x04001B56 RID: 6998
	public string TabSuspectCheat;

	// Token: 0x04001B57 RID: 6999
	public string TabSuspectBugUse;

	// Token: 0x04001B58 RID: 7000
	public string TabSuspectAbuse;

	// Token: 0x04001B59 RID: 7001
	public string TabNeedReputation;

	// Token: 0x04001B5A RID: 7002
	public string SettingsHigh;

	// Token: 0x04001B5B RID: 7003
	public string SettingsMiddle;

	// Token: 0x04001B5C RID: 7004
	public string SettingsLow;

	// Token: 0x04001B5D RID: 7005
	public string SettingsMax;

	// Token: 0x04001B5E RID: 7006
	public string SettingsNickAllow;

	// Token: 0x04001B5F RID: 7007
	public string SettingsNickNotAllow;

	// Token: 0x04001B60 RID: 7008
	public string SettingsGame;

	// Token: 0x04001B61 RID: 7009
	public string SettingsVideoAudio;

	// Token: 0x04001B62 RID: 7010
	public string SettingsControl;

	// Token: 0x04001B63 RID: 7011
	public string SettingsNetwork;

	// Token: 0x04001B64 RID: 7012
	public string SettingsBonuses;

	// Token: 0x04001B65 RID: 7013
	public string SettingsApply;

	// Token: 0x04001B66 RID: 7014
	public string SettingsClan;

	// Token: 0x04001B67 RID: 7015
	public string SettingsNickCheck;

	// Token: 0x04001B68 RID: 7016
	public string SettingsNickMaxLenght;

	// Token: 0x04001B69 RID: 7017
	public string SettingsYourNickUsedInGame;

	// Token: 0x04001B6A RID: 7018
	public string SettingsYouAllowToChangeNick;

	// Token: 0x04001B6B RID: 7019
	public string SettingsTimes;

	// Token: 0x04001B6C RID: 7020
	public string SettingsBuyNickChange;

	// Token: 0x04001B6D RID: 7021
	public string SettingsBuyChange;

	// Token: 0x04001B6E RID: 7022
	public string SettingsBuyColorChange;

	// Token: 0x04001B6F RID: 7023
	public string SettingsBuyChangePopUp;

	// Token: 0x04001B70 RID: 7024
	public string SettingsBuyChangeColorPopUp;

	// Token: 0x04001B71 RID: 7025
	public string SettingsHopsBonus;

	// Token: 0x04001B72 RID: 7026
	public string SettingsHopsBonusHint;

	// Token: 0x04001B73 RID: 7027
	public string SettingsHopsKey;

	// Token: 0x04001B74 RID: 7028
	public string SettingsTransoprentyRadar;

	// Token: 0x04001B75 RID: 7029
	public string SettingsShowProgressContract;

	// Token: 0x04001B76 RID: 7030
	public string SettingsSimpleShowContract;

	// Token: 0x04001B77 RID: 7031
	public string SettingsAlwaysShowHpDef;

	// Token: 0x04001B78 RID: 7032
	public string SettingsAutorespawn;

	// Token: 0x04001B79 RID: 7033
	public string SettingsSwitchOffChat;

	// Token: 0x04001B7A RID: 7034
	public string SettingsSecondaryEquiped;

	// Token: 0x04001B7B RID: 7035
	public string SettingsHideInterface;

	// Token: 0x04001B7C RID: 7036
	public string SettingsEnableFullScreenInBattle;

	// Token: 0x04001B7D RID: 7037
	public string SettingsScreenRez;

	// Token: 0x04001B7E RID: 7038
	public string SettingsModelQuality;

	// Token: 0x04001B7F RID: 7039
	public string SettingsPostEffect;

	// Token: 0x04001B80 RID: 7040
	public string LimitFrameRate;

	// Token: 0x04001B81 RID: 7041
	public string SettingsGraphicQuality;

	// Token: 0x04001B82 RID: 7042
	public string SettingsVeryLow;

	// Token: 0x04001B83 RID: 7043
	public string SettingsLowMiddle;

	// Token: 0x04001B84 RID: 7044
	public string SettingsCustom;

	// Token: 0x04001B85 RID: 7045
	public string SettingsAdvancedSettingsGr;

	// Token: 0x04001B86 RID: 7046
	public string SettingsShadowRadius;

	// Token: 0x04001B87 RID: 7047
	public string SettingsPaltryObjects;

	// Token: 0x04001B88 RID: 7048
	public string SettingsAudioMusic;

	// Token: 0x04001B89 RID: 7049
	public string SettingsOverallVolume;

	// Token: 0x04001B8A RID: 7050
	public string SettingsSoundVolume;

	// Token: 0x04001B8B RID: 7051
	public string SettingsRadioVolume;

	// Token: 0x04001B8C RID: 7052
	public string SettingsTextureQuality;

	// Token: 0x04001B8D RID: 7053
	public string SettingsShadowQuality;

	// Token: 0x04001B8E RID: 7054
	public string SettingsLightningQuality;

	// Token: 0x04001B8F RID: 7055
	public string SettingsPhysicsQuality;

	// Token: 0x04001B90 RID: 7056
	public string SettingsDefaultValue;

	// Token: 0x04001B91 RID: 7057
	public string SettingsAction;

	// Token: 0x04001B92 RID: 7058
	public string SettingsContolButton;

	// Token: 0x04001B93 RID: 7059
	public string SettingsMouseSensitivity;

	// Token: 0x04001B94 RID: 7060
	public string SettingsInvertMouse;

	// Token: 0x04001B95 RID: 7061
	public string SettingsHold;

	// Token: 0x04001B96 RID: 7062
	public string SettingsMoveForward;

	// Token: 0x04001B97 RID: 7063
	public string SettingsMoveBack;

	// Token: 0x04001B98 RID: 7064
	public string SettingsMoveLeft;

	// Token: 0x04001B99 RID: 7065
	public string SettingsMoveRight;

	// Token: 0x04001B9A RID: 7066
	public string SettingsJump;

	// Token: 0x04001B9B RID: 7067
	public string SettingsWalk;

	// Token: 0x04001B9C RID: 7068
	public string SettingsSit;

	// Token: 0x04001B9D RID: 7069
	public string SettingsFire;

	// Token: 0x04001B9E RID: 7070
	public string SettingsAim;

	// Token: 0x04001B9F RID: 7071
	public string SettingsRecharge;

	// Token: 0x04001BA0 RID: 7072
	public string SettingsGrenade;

	// Token: 0x04001BA1 RID: 7073
	public string SettingsKnife;

	// Token: 0x04001BA2 RID: 7074
	public string SettingsFireMode;

	// Token: 0x04001BA3 RID: 7075
	public string SettingsSwitchWeapon;

	// Token: 0x04001BA4 RID: 7076
	public string SettingsSelectPistol;

	// Token: 0x04001BA5 RID: 7077
	public string SettingsSelectMainWeapon;

	// Token: 0x04001BA6 RID: 7078
	public string SettingsUse;

	// Token: 0x04001BA7 RID: 7079
	public string SettingsCallSupport;

	// Token: 0x04001BA8 RID: 7080
	public string SettingsMatchStatistics;

	// Token: 0x04001BA9 RID: 7081
	public string SettingsExitToMainMenu;

	// Token: 0x04001BAA RID: 7082
	public string SettingsFullScreen;

	// Token: 0x04001BAB RID: 7083
	public string SettingsTeamChange;

	// Token: 0x04001BAC RID: 7084
	public string SettingsTeamMessage;

	// Token: 0x04001BAD RID: 7085
	public string SettingsMessageToAll;

	// Token: 0x04001BAE RID: 7086
	public string SettingsRadioCommand;

	// Token: 0x04001BAF RID: 7087
	public string SettingsScreenshot;

	// Token: 0x04001BB0 RID: 7088
	public string SettingsNetworkDescr0;

	// Token: 0x04001BB1 RID: 7089
	public string SettingsNetworkDescr1;

	// Token: 0x04001BB2 RID: 7090
	public string SettingsNetworkDescr2;

	// Token: 0x04001BB3 RID: 7091
	public string SettingsNetworkDescr3;

	// Token: 0x04001BB4 RID: 7092
	public string SettingsNetworkDescr4;

	// Token: 0x04001BB5 RID: 7093
	public string SettingsNetworkDescr5;

	// Token: 0x04001BB6 RID: 7094
	public string SettingsNetworkDescr6;

	// Token: 0x04001BB7 RID: 7095
	public string SGGlobal;

	// Token: 0x04001BB8 RID: 7096
	public string SGWhithoutRating;

	// Token: 0x04001BB9 RID: 7097
	public string SGFritends;

	// Token: 0x04001BBA RID: 7098
	public string SGFavorites;

	// Token: 0x04001BBB RID: 7099
	public string SGLatests;

	// Token: 0x04001BBC RID: 7100
	public string SGServer;

	// Token: 0x04001BBD RID: 7101
	public string SGMap;

	// Token: 0x04001BBE RID: 7102
	public string SGRate;

	// Token: 0x04001BBF RID: 7103
	public string SGPlayers;

	// Token: 0x04001BC0 RID: 7104
	public string SGMode;

	// Token: 0x04001BC1 RID: 7105
	public string SGEmpty;

	// Token: 0x04001BC2 RID: 7106
	public string SGFull;

	// Token: 0x04001BC3 RID: 7107
	public string SGLevel;

	// Token: 0x04001BC4 RID: 7108
	public string SGCreateServer;

	// Token: 0x04001BC5 RID: 7109
	public string SGConnect;

	// Token: 0x04001BC6 RID: 7110
	public string ChToAll;

	// Token: 0x04001BC7 RID: 7111
	public string ChToTeam;

	// Token: 0x04001BC8 RID: 7112
	public string GrenadeThrowMessage1;

	// Token: 0x04001BC9 RID: 7113
	public string GrenadeThrowMessage2;

	// Token: 0x04001BCA RID: 7114
	public string Task;

	// Token: 0x04001BCB RID: 7115
	public string Packages;

	// Token: 0x04001BCC RID: 7116
	public string SetOfEquipment;

	// Token: 0x04001BCD RID: 7117
	public string SelectedSet;

	// Token: 0x04001BCE RID: 7118
	public string CharacterCamouflage;

	// Token: 0x04001BCF RID: 7119
	public string Select;

	// Token: 0x04001BD0 RID: 7120
	public string CamouflageSelection;

	// Token: 0x04001BD1 RID: 7121
	public string IdleKick;

	// Token: 0x04001BD2 RID: 7122
	public string PingKickHeader;

	// Token: 0x04001BD3 RID: 7123
	public string PingKickBody1;

	// Token: 0x04001BD4 RID: 7124
	public string PingKickBody2;

	// Token: 0x04001BD5 RID: 7125
	public string TeamKillKick;

	// Token: 0x04001BD6 RID: 7126
	public string TeamKillBan;

	// Token: 0x04001BD7 RID: 7127
	public string ErrorNetworkProtocol;

	// Token: 0x04001BD8 RID: 7128
	public string FloodKick;

	// Token: 0x04001BD9 RID: 7129
	public string ServerFullPlayers;

	// Token: 0x04001BDA RID: 7130
	public string ServerFullSpec;

	// Token: 0x04001BDB RID: 7131
	public string ServerFullSlot;

	// Token: 0x04001BDC RID: 7132
	public string ServerSlotAvaliable;

	// Token: 0x04001BDD RID: 7133
	public string ServerRestarted;

	// Token: 0x04001BDE RID: 7134
	public string UserQuited;

	// Token: 0x04001BDF RID: 7135
	public string ServerDisconnect;

	// Token: 0x04001BE0 RID: 7136
	public string Dead;

	// Token: 0x04001BE1 RID: 7137
	public string ServerDisconnetProfileLoadError;

	// Token: 0x04001BE2 RID: 7138
	public string ServerDisconnetKeepaliveError;

	// Token: 0x04001BE3 RID: 7139
	public string ProjectNews;

	// Token: 0x04001BE4 RID: 7140
	public string BadlyFinishedBoy;

	// Token: 0x04001BE5 RID: 7141
	public string ExittingFromServer;

	// Token: 0x04001BE6 RID: 7142
	public string WrongPassword;

	// Token: 0x04001BE7 RID: 7143
	public string YouAreAlreadyAtServer;

	// Token: 0x04001BE8 RID: 7144
	public string ClientNotMatchTheServerVersion;

	// Token: 0x04001BE9 RID: 7145
	public string ConnetionDropped;

	// Token: 0x04001BEA RID: 7146
	public string ServerForciblyClosed;

	// Token: 0x04001BEB RID: 7147
	public string VotedFor;

	// Token: 0x04001BEC RID: 7148
	public string SuspectedUser;

	// Token: 0x04001BED RID: 7149
	public string PromoCodeAlreadyActivated;

	// Token: 0x04001BEE RID: 7150
	public string PromoObsolete;

	// Token: 0x04001BEF RID: 7151
	public string PromoCodeAlreadyActivatedThisMember;

	// Token: 0x04001BF0 RID: 7152
	public string PromoUnknownCode;

	// Token: 0x04001BF1 RID: 7153
	public string PromoUnknownError;

	// Token: 0x04001BF2 RID: 7154
	public string PromoErrorActivation;

	// Token: 0x04001BF3 RID: 7155
	public string PromoActivated;

	// Token: 0x04001BF4 RID: 7156
	public string ProcessingRequest;

	// Token: 0x04001BF5 RID: 7157
	public string FundsDelivery;

	// Token: 0x04001BF6 RID: 7158
	public string FundsDeliveryFailed;

	// Token: 0x04001BF7 RID: 7159
	public string ClanTransactionLoading;

	// Token: 0x04001BF8 RID: 7160
	public string ClanTransactionLoadingFailed;

	// Token: 0x04001BF9 RID: 7161
	public string BuyKit;

	// Token: 0x04001BFA RID: 7162
	public string BuyKitProcessing;

	// Token: 0x04001BFB RID: 7163
	public string KitDelivered;

	// Token: 0x04001BFC RID: 7164
	public string BuySet;

	// Token: 0x04001BFD RID: 7165
	public string BuyBox;

	// Token: 0x04001BFE RID: 7166
	public string BuyBoxRequest;

	// Token: 0x04001BFF RID: 7167
	public string BuyBoxProcessing;

	// Token: 0x04001C00 RID: 7168
	public string BoxDelivered;

	// Token: 0x04001C01 RID: 7169
	public string BuyBoxAttention;

	// Token: 0x04001C02 RID: 7170
	public string BuyNick;

	// Token: 0x04001C03 RID: 7171
	public string BuyNickColor;

	// Token: 0x04001C04 RID: 7172
	public string BuyNickProcessing;

	// Token: 0x04001C05 RID: 7173
	public string BuyNickColorProcessing;

	// Token: 0x04001C06 RID: 7174
	public string NickDelivered;

	// Token: 0x04001C07 RID: 7175
	public string CrrierBonus;

	// Token: 0x04001C08 RID: 7176
	public string GetLevel0;

	// Token: 0x04001C09 RID: 7177
	public string GetLevel1;

	// Token: 0x04001C0A RID: 7178
	public string Modification;

	// Token: 0x04001C0B RID: 7179
	public string ModificationProcessing;

	// Token: 0x04001C0C RID: 7180
	public string Repair;

	// Token: 0x04001C0D RID: 7181
	public string EquipmentRepaired;

	// Token: 0x04001C0E RID: 7182
	public string RepairFailure;

	// Token: 0x04001C0F RID: 7183
	public string RepairProcessing;

	// Token: 0x04001C10 RID: 7184
	public string ServerLoadOnFail0;

	// Token: 0x04001C11 RID: 7185
	public string ServerLoadOnFail1;

	// Token: 0x04001C12 RID: 7186
	public string ServerLoadOnFail2;

	// Token: 0x04001C13 RID: 7187
	public string ServerLoadOnFail3;

	// Token: 0x04001C14 RID: 7188
	public string ServerLoadOnFail4;

	// Token: 0x04001C15 RID: 7189
	public string DailyBonus;

	// Token: 0x04001C16 RID: 7190
	public string StatsOneDone;

	// Token: 0x04001C17 RID: 7191
	public string StatsMinusOne;

	// Token: 0x04001C18 RID: 7192
	public string StatsThisOneDone;

	// Token: 0x04001C19 RID: 7193
	public string SUITE;

	// Token: 0x04001C1A RID: 7194
	public string WTaskNotCount;

	// Token: 0x04001C1B RID: 7195
	public string ClassName_Scout;

	// Token: 0x04001C1C RID: 7196
	public string ClassName_Shtormtrooper;

	// Token: 0x04001C1D RID: 7197
	public string ClassName_Destroyer;

	// Token: 0x04001C1E RID: 7198
	public string ClassName_Sniper;

	// Token: 0x04001C1F RID: 7199
	public string ClassName_Armorer;

	// Token: 0x04001C20 RID: 7200
	public string ClassName_Careerist;

	// Token: 0x04001C21 RID: 7201
	public string HGUnlock;

	// Token: 0x04001C22 RID: 7202
	public string HGUnlockQuestion;

	// Token: 0x04001C23 RID: 7203
	public string RepairQuestion;

	// Token: 0x04001C24 RID: 7204
	public string HGState;

	// Token: 0x04001C25 RID: 7205
	public string HGWeaponBrokenNeedRepair;

	// Token: 0x04001C26 RID: 7206
	public string HGIndestructible;

	// Token: 0x04001C27 RID: 7207
	public string HGPayQuestion;

	// Token: 0x04001C28 RID: 7208
	public string HGRent;

	// Token: 0x04001C29 RID: 7209
	public string HGRentQuestion;

	// Token: 0x04001C2A RID: 7210
	public string HGWeapon;

	// Token: 0x04001C2B RID: 7211
	public string HGPremiumBuy;

	// Token: 0x04001C2C RID: 7212
	public string HGBuyQuestion;

	// Token: 0x04001C2D RID: 7213
	public string ForeverNormal;

	// Token: 0x04001C2E RID: 7214
	public string HGAvaliable;

	// Token: 0x04001C2F RID: 7215
	public string HGNotAvaliable;

	// Token: 0x04001C30 RID: 7216
	public string MHGtabs_Menu;

	// Token: 0x04001C31 RID: 7217
	public string MHGtabs_Weapons;

	// Token: 0x04001C32 RID: 7218
	public string MHGtabs_Skills;

	// Token: 0x04001C33 RID: 7219
	public string MHGtabs_Battle;

	// Token: 0x04001C34 RID: 7220
	public string MHGDescr_0;

	// Token: 0x04001C35 RID: 7221
	public string MHGDescr_1;

	// Token: 0x04001C36 RID: 7222
	public string MHGDescr_2;

	// Token: 0x04001C37 RID: 7223
	public string MHGDescr_3;

	// Token: 0x04001C38 RID: 7224
	public string MHGDescr_4;

	// Token: 0x04001C39 RID: 7225
	public string MHGDescr_5;

	// Token: 0x04001C3A RID: 7226
	public string MHGDescr_6;

	// Token: 0x04001C3B RID: 7227
	public string MHGDescr_7;

	// Token: 0x04001C3C RID: 7228
	public string MHGDescr_8;

	// Token: 0x04001C3D RID: 7229
	public string MHGDescr_9;

	// Token: 0x04001C3E RID: 7230
	public string MHGDescr_10;

	// Token: 0x04001C3F RID: 7231
	public string MHGDescr_11;

	// Token: 0x04001C40 RID: 7232
	public string MHGDescr_12;

	// Token: 0x04001C41 RID: 7233
	public string MHGDescr_13;

	// Token: 0x04001C42 RID: 7234
	public string MHGDescr_14;

	// Token: 0x04001C43 RID: 7235
	public string MHGDescr_15;

	// Token: 0x04001C44 RID: 7236
	public string MHGDescr_16;

	// Token: 0x04001C45 RID: 7237
	public string MHGDescr_17;

	// Token: 0x04001C46 RID: 7238
	public string MHGDescr_18;

	// Token: 0x04001C47 RID: 7239
	public string MHGDescr_19;

	// Token: 0x04001C48 RID: 7240
	public string MHGDescr_20;

	// Token: 0x04001C49 RID: 7241
	public string MHGDescr_21;

	// Token: 0x04001C4A RID: 7242
	public string MHGDescr_22;

	// Token: 0x04001C4B RID: 7243
	public string MHGDescr_23;

	// Token: 0x04001C4C RID: 7244
	public string MHGDescr_24;

	// Token: 0x04001C4D RID: 7245
	public string MHGDescr_25;

	// Token: 0x04001C4E RID: 7246
	public string MHGDescr_26;

	// Token: 0x04001C4F RID: 7247
	public string MHGDescr_27;

	// Token: 0x04001C50 RID: 7248
	public string MHGDescr_28;

	// Token: 0x04001C51 RID: 7249
	public string MHGDescr_29;

	// Token: 0x04001C52 RID: 7250
	public string MHGDescr_30;

	// Token: 0x04001C53 RID: 7251
	public string MHGDescr_31;

	// Token: 0x04001C54 RID: 7252
	public string MHGDescr_32;

	// Token: 0x04001C55 RID: 7253
	public string MHGDescr_33;

	// Token: 0x04001C56 RID: 7254
	public string MHGDescr_34;

	// Token: 0x04001C57 RID: 7255
	public string MHGDescr_35;

	// Token: 0x04001C58 RID: 7256
	public string MHGDescr_36;

	// Token: 0x04001C59 RID: 7257
	public string MHGDescr_37;

	// Token: 0x04001C5A RID: 7258
	public string MHGDescr_38;

	// Token: 0x04001C5B RID: 7259
	public string MHGDescr_39;

	// Token: 0x04001C5C RID: 7260
	public string MHGDescr_40;

	// Token: 0x04001C5D RID: 7261
	public string MHGDescr_41;

	// Token: 0x04001C5E RID: 7262
	public string MHGDescr_42;

	// Token: 0x04001C5F RID: 7263
	public string MHGDescr_43;

	// Token: 0x04001C60 RID: 7264
	public string MHGDescr_44;

	// Token: 0x04001C61 RID: 7265
	public string MHGDescr_45;

	// Token: 0x04001C62 RID: 7266
	public string MHGDescr_46;

	// Token: 0x04001C63 RID: 7267
	public string MHGDescr_47;

	// Token: 0x04001C64 RID: 7268
	public string MHGDescr_48;

	// Token: 0x04001C65 RID: 7269
	public string MHGDescr_49;

	// Token: 0x04001C66 RID: 7270
	public string MHGDescr_50;

	// Token: 0x04001C67 RID: 7271
	public string MHGDescr_51;

	// Token: 0x04001C68 RID: 7272
	public string MHGDescr_52;

	// Token: 0x04001C69 RID: 7273
	public string MHGDescr_53;

	// Token: 0x04001C6A RID: 7274
	public string MHGDescr_54;

	// Token: 0x04001C6B RID: 7275
	public string MHGDescr_55;

	// Token: 0x04001C6C RID: 7276
	public string MHGDescr_56;

	// Token: 0x04001C6D RID: 7277
	public string MHGDescr_57;

	// Token: 0x04001C6E RID: 7278
	public string MHGDescr_58;

	// Token: 0x04001C6F RID: 7279
	public string MHGDescr_59;

	// Token: 0x04001C70 RID: 7280
	public string MHGDescr_60;

	// Token: 0x04001C71 RID: 7281
	public string MHGDescr_61;

	// Token: 0x04001C72 RID: 7282
	public string MHGDescr_62;

	// Token: 0x04001C73 RID: 7283
	public string MHGDescr_63;

	// Token: 0x04001C74 RID: 7284
	public string MHGDescr_64;

	// Token: 0x04001C75 RID: 7285
	public string MHGDescr_65;

	// Token: 0x04001C76 RID: 7286
	public string MHGDescr_66;

	// Token: 0x04001C77 RID: 7287
	public string MHGDescr_67;

	// Token: 0x04001C78 RID: 7288
	public string MHGDescr_68;

	// Token: 0x04001C79 RID: 7289
	public string MHGDescr_69;

	// Token: 0x04001C7A RID: 7290
	public string MHGDescr_70;

	// Token: 0x04001C7B RID: 7291
	public string MHGDescr_71;

	// Token: 0x04001C7C RID: 7292
	public string MHGDescr_72;

	// Token: 0x04001C7D RID: 7293
	public string MHGDescr_73;

	// Token: 0x04001C7E RID: 7294
	public string MHGDescr_74;

	// Token: 0x04001C7F RID: 7295
	public string MHGDescr_75;

	// Token: 0x04001C80 RID: 7296
	public string MHGDescr_76;

	// Token: 0x04001C81 RID: 7297
	public string MHGDescr_77;

	// Token: 0x04001C82 RID: 7298
	public string MHGDescr_78;

	// Token: 0x04001C83 RID: 7299
	public string MHGDescr_79;

	// Token: 0x04001C84 RID: 7300
	public string MHGDescr_80;

	// Token: 0x04001C85 RID: 7301
	public string MHGDescr_81;

	// Token: 0x04001C86 RID: 7302
	public string MHGDescr_82;

	// Token: 0x04001C87 RID: 7303
	public string MHGDescr_83;

	// Token: 0x04001C88 RID: 7304
	public string MHGDescr_84;

	// Token: 0x04001C89 RID: 7305
	public string MHGDescr_85;

	// Token: 0x04001C8A RID: 7306
	public string MHGHelp;

	// Token: 0x04001C8B RID: 7307
	public string MRYourResult;

	// Token: 0x04001C8C RID: 7308
	public string MRBestResult;

	// Token: 0x04001C8D RID: 7309
	public string MRCreditsForProgress;

	// Token: 0x04001C8E RID: 7310
	public string MRExpRate;

	// Token: 0x04001C8F RID: 7311
	public string MRSkill;

	// Token: 0x04001C90 RID: 7312
	public string MRDoubleExp;

	// Token: 0x04001C91 RID: 7313
	public string MRPlayersTax;

	// Token: 0x04001C92 RID: 7314
	public string MRNightCredits;

	// Token: 0x04001C93 RID: 7315
	public string MRNightExp;

	// Token: 0x04001C94 RID: 7316
	public string MRSPSpend;

	// Token: 0x04001C95 RID: 7317
	public string MRMPSpend;

	// Token: 0x04001C96 RID: 7318
	public string MRMatchBonus;

	// Token: 0x04001C97 RID: 7319
	public string MRClanExpDep;

	// Token: 0x04001C98 RID: 7320
	public string MRClanCrDep;

	// Token: 0x04001C99 RID: 7321
	public string MRTotal;

	// Token: 0x04001C9A RID: 7322
	public string MREarnExp;

	// Token: 0x04001C9B RID: 7323
	public string MRBestResultForMatch;

	// Token: 0x04001C9C RID: 7324
	public string MRNoAchievements;

	// Token: 0x04001C9D RID: 7325
	public string MRBestPlayer;

	// Token: 0x04001C9E RID: 7326
	public string MRWorthPlayer;

	// Token: 0x04001C9F RID: 7327
	public string MRWin;

	// Token: 0x04001CA0 RID: 7328
	public string MRDraw;

	// Token: 0x04001CA1 RID: 7329
	public string MRMatchResult;

	// Token: 0x04001CA2 RID: 7330
	public string MRGameTime;

	// Token: 0x04001CA3 RID: 7331
	public string MRKillsTotal;

	// Token: 0x04001CA4 RID: 7332
	public string MRDeathTotal;

	// Token: 0x04001CA5 RID: 7333
	public string MainMaxMatchesDescr;

	// Token: 0x04001CA6 RID: 7334
	public string RadarBeaconSet;

	// Token: 0x04001CA7 RID: 7335
	public string ServerGUICreatingServer;

	// Token: 0x04001CA8 RID: 7336
	public string ServerGUICreate;

	// Token: 0x04001CA9 RID: 7337
	public string ServerGUIName;

	// Token: 0x04001CAA RID: 7338
	public string ServerGUIPlayersCount;

	// Token: 0x04001CAB RID: 7339
	public string ServerGUIGameMode;

	// Token: 0x04001CAC RID: 7340
	public string RadioEmpty0;

	// Token: 0x04001CAD RID: 7341
	public string RadioEmpty1;

	// Token: 0x04001CAE RID: 7342
	public string RadioStart;

	// Token: 0x04001CAF RID: 7343
	public string RadioStart0;

	// Token: 0x04001CB0 RID: 7344
	public string RadioStart1;

	// Token: 0x04001CB1 RID: 7345
	public string RadioReceived;

	// Token: 0x04001CB2 RID: 7346
	public string RadioReceived0;

	// Token: 0x04001CB3 RID: 7347
	public string RadioReceived1;

	// Token: 0x04001CB4 RID: 7348
	public string RadioCover;

	// Token: 0x04001CB5 RID: 7349
	public string RadioCover0;

	// Token: 0x04001CB6 RID: 7350
	public string RadioCover1;

	// Token: 0x04001CB7 RID: 7351
	public string RadioAttention;

	// Token: 0x04001CB8 RID: 7352
	public string RadioAttention0;

	// Token: 0x04001CB9 RID: 7353
	public string RadioAttention1;

	// Token: 0x04001CBA RID: 7354
	public string RadioClear;

	// Token: 0x04001CBB RID: 7355
	public string RadioClear0;

	// Token: 0x04001CBC RID: 7356
	public string RadioClear1;

	// Token: 0x04001CBD RID: 7357
	public string RadioStop;

	// Token: 0x04001CBE RID: 7358
	public string RadioStop0;

	// Token: 0x04001CBF RID: 7359
	public string RadioStop1;

	// Token: 0x04001CC0 RID: 7360
	public string RadioGood;

	// Token: 0x04001CC1 RID: 7361
	public string RadioGood0;

	// Token: 0x04001CC2 RID: 7362
	public string RadioGood1;

	// Token: 0x04001CC3 RID: 7363
	public string RadioFollowMe;

	// Token: 0x04001CC4 RID: 7364
	public string RadioFollowMe0;

	// Token: 0x04001CC5 RID: 7365
	public string RadioFollowMe1;

	// Token: 0x04001CC6 RID: 7366
	public string RadioHelp;

	// Token: 0x04001CC7 RID: 7367
	public string RadioHelp0;

	// Token: 0x04001CC8 RID: 7368
	public string RadioHelp1;

	// Token: 0x04001CC9 RID: 7369
	public string RadioCancel;

	// Token: 0x04001CCA RID: 7370
	public string RadioCancel0;

	// Token: 0x04001CCB RID: 7371
	public string RadioCancel1;

	// Token: 0x04001CCC RID: 7372
	public string PointBearCaptured;

	// Token: 0x04001CCD RID: 7373
	public string PointUsecCaptured;

	// Token: 0x04001CCE RID: 7374
	public string PointPurification;

	// Token: 0x04001CCF RID: 7375
	public string NeedMoreBear;

	// Token: 0x04001CD0 RID: 7376
	public string NeedMoreUsec;

	// Token: 0x04001CD1 RID: 7377
	public string PointName_none;

	// Token: 0x04001CD2 RID: 7378
	public string PointName_Base;

	// Token: 0x04001CD3 RID: 7379
	public string PointName_Lighthouse;

	// Token: 0x04001CD4 RID: 7380
	public string PointName_Station;

	// Token: 0x04001CD5 RID: 7381
	public string PointName_Tunnel;

	// Token: 0x04001CD6 RID: 7382
	public string Point;

	// Token: 0x04001CD7 RID: 7383
	public string MatchExp;

	// Token: 0x04001CD8 RID: 7384
	public string TeamWin;

	// Token: 0x04001CD9 RID: 7385
	public string Accuracy;

	// Token: 0x04001CDA RID: 7386
	public string Impact;

	// Token: 0x04001CDB RID: 7387
	public string Damage;

	// Token: 0x04001CDC RID: 7388
	public string FireRate;

	// Token: 0x04001CDD RID: 7389
	public string Mobility;

	// Token: 0x04001CDE RID: 7390
	public string ReloadRate;

	// Token: 0x04001CDF RID: 7391
	public string Ammunition;

	// Token: 0x04001CE0 RID: 7392
	public string Cartridge;

	// Token: 0x04001CE1 RID: 7393
	public string Penetration;

	// Token: 0x04001CE2 RID: 7394
	public string Objective;

	// Token: 0x04001CE3 RID: 7395
	public string EffectiveDistance;

	// Token: 0x04001CE4 RID: 7396
	public string ShotGrouping;

	// Token: 0x04001CE5 RID: 7397
	public string HearDistance;

	// Token: 0x04001CE6 RID: 7398
	public string MainGUIFreeChooseWeapon;

	// Token: 0x04001CE7 RID: 7399
	public string MainGUIBlocked;

	// Token: 0x04001CE8 RID: 7400
	public string MainGUIChooseSavedSets;

	// Token: 0x04001CE9 RID: 7401
	public string Unlock;

	// Token: 0x04001CEA RID: 7402
	public string WeaponView;

	// Token: 0x04001CEB RID: 7403
	public string Spectators;

	// Token: 0x04001CEC RID: 7404
	public string InviteFriends;

	// Token: 0x04001CED RID: 7405
	public string YouMadeDoubleHeadshot;

	// Token: 0x04001CEE RID: 7406
	public string YouMadeTripleKill;

	// Token: 0x04001CEF RID: 7407
	public string YouMadeQuadKill;

	// Token: 0x04001CF0 RID: 7408
	public string YouMadeRageKill;

	// Token: 0x04001CF1 RID: 7409
	public string YouMadeStormKill;

	// Token: 0x04001CF2 RID: 7410
	public string YouMadeProKill;

	// Token: 0x04001CF3 RID: 7411
	public string YouMadeLegendaryKill;

	// Token: 0x04001CF4 RID: 7412
	public string ChooseDislocate;

	// Token: 0x04001CF5 RID: 7413
	public string Dislocate;

	// Token: 0x04001CF6 RID: 7414
	public string SettingsCharacterQuality;

	// Token: 0x04001CF7 RID: 7415
	public string Ready;

	// Token: 0x04001CF8 RID: 7416
	public string TutorHintFitstTime;

	// Token: 0x04001CF9 RID: 7417
	public string TutorNickname;

	// Token: 0x04001CFA RID: 7418
	public string TutorExpBar;

	// Token: 0x04001CFB RID: 7419
	public string TutorContracts;

	// Token: 0x04001CFC RID: 7420
	public string TutorBalance;

	// Token: 0x04001CFD RID: 7421
	public string TutorBuyWeapon;

	// Token: 0x04001CFE RID: 7422
	public string TutorEquipPrimary;

	// Token: 0x04001CFF RID: 7423
	public string TutorConfirmPayment;

	// Token: 0x04001D00 RID: 7424
	public string TutorInstallWtask;

	// Token: 0x04001D01 RID: 7425
	public string TutorEquipSecondary;

	// Token: 0x04001D02 RID: 7426
	public string TutorSaveWeaponKit;

	// Token: 0x04001D03 RID: 7427
	public string TutorSelectedKit;

	// Token: 0x04001D04 RID: 7428
	public string TutorQuickMatchOpen;

	// Token: 0x04001D05 RID: 7429
	public string TutorQuickMatchGo;

	// Token: 0x04001D06 RID: 7430
	public string TutorFullScreen;

	// Token: 0x04001D07 RID: 7431
	public string TutorHeader1;

	// Token: 0x04001D08 RID: 7432
	public string TutorHeader2;

	// Token: 0x04001D09 RID: 7433
	public string TutorHeader3;

	// Token: 0x04001D0A RID: 7434
	public string TutorInGameControlHeader;

	// Token: 0x04001D0B RID: 7435
	public string TutorInGameWeaponChange;

	// Token: 0x04001D0C RID: 7436
	public string TutorInGameMenu;

	// Token: 0x04001D0D RID: 7437
	public string TutorInGameFSmode;

	// Token: 0x04001D0E RID: 7438
	public string TutorInGameWalk;

	// Token: 0x04001D0F RID: 7439
	public string TutorInGameMovement;

	// Token: 0x04001D10 RID: 7440
	public string TutorInGameReload;

	// Token: 0x04001D11 RID: 7441
	public string TutorInGameCrouch;

	// Token: 0x04001D12 RID: 7442
	public string TutorInGameKnife;

	// Token: 0x04001D13 RID: 7443
	public string TutorInGameJump;

	// Token: 0x04001D14 RID: 7444
	public string TutorInGameFire;

	// Token: 0x04001D15 RID: 7445
	public string TutorInGameAim;

	// Token: 0x04001D16 RID: 7446
	public string TutorInGameContinue;

	// Token: 0x04001D17 RID: 7447
	public string TutorInGameGameplayHeader;

	// Token: 0x04001D18 RID: 7448
	public string TutorInGameGameplayHint1_1;

	// Token: 0x04001D19 RID: 7449
	public string TutorInGameGameplayHint1_2;

	// Token: 0x04001D1A RID: 7450
	public string TutorInGameGameplayHint2;

	// Token: 0x04001D1B RID: 7451
	public string TutorInGameGameplayHint3;

	// Token: 0x04001D1C RID: 7452
	public string TutorInGameGameplayHint4;

	// Token: 0x04001D1D RID: 7453
	public string DN;

	// Token: 0x04001D1E RID: 7454
	public string Buyed;

	// Token: 0x04001D1F RID: 7455
	public string SettingsCallMortarStrike;

	// Token: 0x04001D20 RID: 7456
	public string SettingsCallSonar;

	// Token: 0x04001D21 RID: 7457
	public string SettingsUseSpecEquipment;

	// Token: 0x04001D22 RID: 7458
	public string SettingsHideShowInterface;

	// Token: 0x04001D23 RID: 7459
	public string Cancel;

	// Token: 0x04001D24 RID: 7460
	public string FindInFull;

	// Token: 0x04001D25 RID: 7461
	public string ClansInfo;

	// Token: 0x04001D26 RID: 7462
	public string ClansCreate;

	// Token: 0x04001D27 RID: 7463
	public string ClansJoin;

	// Token: 0x04001D28 RID: 7464
	public string ClansWars;

	// Token: 0x04001D29 RID: 7465
	public string ClansManagment;

	// Token: 0x04001D2A RID: 7466
	public string ClansLeveling;

	// Token: 0x04001D2B RID: 7467
	public string ClansName;

	// Token: 0x04001D2C RID: 7468
	public string ClansLead;

	// Token: 0x04001D2D RID: 7469
	public string ClansLeadYou;

	// Token: 0x04001D2E RID: 7470
	public string ClansStats;

	// Token: 0x04001D2F RID: 7471
	public string ClansYourStats;

	// Token: 0x04001D30 RID: 7472
	public string ClansYourContribution;

	// Token: 0x04001D31 RID: 7473
	public string ClansYourContributionHint;

	// Token: 0x04001D32 RID: 7474
	public string ClansExpSliderHint;

	// Token: 0x04001D33 RID: 7475
	public string ClansBalanceHint;

	// Token: 0x04001D34 RID: 7476
	public string ClansExpSliderInGameHint;

	// Token: 0x04001D35 RID: 7477
	public string ClansSize;

	// Token: 0x04001D36 RID: 7478
	public string ClansVictory;

	// Token: 0x04001D37 RID: 7479
	public string Officers;

	// Token: 0x04001D38 RID: 7480
	public string ClansYourWarrior;

	// Token: 0x04001D39 RID: 7481
	public string ClansRequest1;

	// Token: 0x04001D3A RID: 7482
	public string ClansRequest2;

	// Token: 0x04001D3B RID: 7483
	public string ClansWithdraw;

	// Token: 0x04001D3C RID: 7484
	public string ClansCreateLabel;

	// Token: 0x04001D3D RID: 7485
	public string ClansRequestLeft;

	// Token: 0x04001D3E RID: 7486
	public string ClansRaceBtn;

	// Token: 0x04001D3F RID: 7487
	public string ClansWarsBtn;

	// Token: 0x04001D40 RID: 7488
	public string ClansArmoryBtn;

	// Token: 0x04001D41 RID: 7489
	public string ClansCamouflageBtn;

	// Token: 0x04001D42 RID: 7490
	public string ClansHistory;

	// Token: 0x04001D43 RID: 7491
	public string ClansClantag;

	// Token: 0x04001D44 RID: 7492
	public string ClansClantagColor;

	// Token: 0x04001D45 RID: 7493
	public string ChangeNickColor;

	// Token: 0x04001D46 RID: 7494
	public string ClansClanName;

	// Token: 0x04001D47 RID: 7495
	public string ClansBase;

	// Token: 0x04001D48 RID: 7496
	public string ClansExtended;

	// Token: 0x04001D49 RID: 7497
	public string ClansPremium;

	// Token: 0x04001D4A RID: 7498
	public string ClansCreateHint1;

	// Token: 0x04001D4B RID: 7499
	public string ClansCreateHint2;

	// Token: 0x04001D4C RID: 7500
	public string ClansCreateHint3;

	// Token: 0x04001D4D RID: 7501
	public string ClansCreateHint4;

	// Token: 0x04001D4E RID: 7502
	public string ClansCreateHint5;

	// Token: 0x04001D4F RID: 7503
	public string ClansCreateHint6;

	// Token: 0x04001D50 RID: 7504
	public string ClansManagmentDiscard1;

	// Token: 0x04001D51 RID: 7505
	public string ClansManagmentDiscard2;

	// Token: 0x04001D52 RID: 7506
	public string ClansManagmentExtend;

	// Token: 0x04001D53 RID: 7507
	public string ClansManagmentLeave;

	// Token: 0x04001D54 RID: 7508
	public string ClansManagmentRequest;

	// Token: 0x04001D55 RID: 7509
	public string ClansManagmentCurrent;

	// Token: 0x04001D56 RID: 7510
	public string ClansBalance;

	// Token: 0x04001D57 RID: 7511
	public string ClansSkillAccess;

	// Token: 0x04001D58 RID: 7512
	public string ClansMinimalTransaction;

	// Token: 0x04001D59 RID: 7513
	public string ClansTableContribution;

	// Token: 0x04001D5A RID: 7514
	public string ClansTableDiff;

	// Token: 0x04001D5B RID: 7515
	public string ClansRaceAttention;

	// Token: 0x04001D5C RID: 7516
	public string ClansRaceHint;

	// Token: 0x04001D5D RID: 7517
	public string ClansRaceHint1;

	// Token: 0x04001D5E RID: 7518
	public string ClansRaceEnding;

	// Token: 0x04001D5F RID: 7519
	public string ClansRaceExp;

	// Token: 0x04001D60 RID: 7520
	public string ClansRaceKills;

	// Token: 0x04001D61 RID: 7521
	public string ClansDisbanded;

	// Token: 0x04001D62 RID: 7522
	public string ClansPopupError;

	// Token: 0x04001D63 RID: 7523
	public string ClansPopupCreate;

	// Token: 0x04001D64 RID: 7524
	public string ClansPopupCreateHint1;

	// Token: 0x04001D65 RID: 7525
	public string ClansPopupCreateHint2;

	// Token: 0x04001D66 RID: 7526
	public string ClansPopupCreateHint3;

	// Token: 0x04001D67 RID: 7527
	public string ClansPopupCreateHint4;

	// Token: 0x04001D68 RID: 7528
	public string ClansPopupCreateHint5;

	// Token: 0x04001D69 RID: 7529
	public string ClansPopupCreateError1;

	// Token: 0x04001D6A RID: 7530
	public string ClansPopupCreateError2;

	// Token: 0x04001D6B RID: 7531
	public string ClansPopupExtend;

	// Token: 0x04001D6C RID: 7532
	public string ClansPopupExtendHint;

	// Token: 0x04001D6D RID: 7533
	public string ClansPopupRequest;

	// Token: 0x04001D6E RID: 7534
	public string ClansPopupRequestFailedByOrder1;

	// Token: 0x04001D6F RID: 7535
	public string ClansPopupRequestFailedByOrder2;

	// Token: 0x04001D70 RID: 7536
	public string ClansPopupRequestFailedByVacancy1;

	// Token: 0x04001D71 RID: 7537
	public string ClansPopupRequestFailedByVacancy2;

	// Token: 0x04001D72 RID: 7538
	public string ClansPopupRequestFailedByVacancy3;

	// Token: 0x04001D73 RID: 7539
	public string ClansPopupDiscard;

	// Token: 0x04001D74 RID: 7540
	public string ClansPopupDiscardHint;

	// Token: 0x04001D75 RID: 7541
	public string ClansPopupDismiss;

	// Token: 0x04001D76 RID: 7542
	public string ClansPopupDismissHint1;

	// Token: 0x04001D77 RID: 7543
	public string ClansPopupDismissHint2;

	// Token: 0x04001D78 RID: 7544
	public string ClansPopupLeave;

	// Token: 0x04001D79 RID: 7545
	public string ClansPopupLeaveHint;

	// Token: 0x04001D7A RID: 7546
	public string ClansPopupBalance;

	// Token: 0x04001D7B RID: 7547
	public string ClansPopupBalanceHint;

	// Token: 0x04001D7C RID: 7548
	public string ClansHistoryWho;

	// Token: 0x04001D7D RID: 7549
	public string CreateClan;

	// Token: 0x04001D7E RID: 7550
	public string CreateClanProcessing;

	// Token: 0x04001D7F RID: 7551
	public string CreateClanComplete;

	// Token: 0x04001D80 RID: 7552
	public string CreateClanErr;

	// Token: 0x04001D81 RID: 7553
	public string ExtendClan;

	// Token: 0x04001D82 RID: 7554
	public string ExtendClanProcessing;

	// Token: 0x04001D83 RID: 7555
	public string ExtendClanComplete;

	// Token: 0x04001D84 RID: 7556
	public string DeleteAllRequest;

	// Token: 0x04001D85 RID: 7557
	public string DeleteAllRequestProcessing;

	// Token: 0x04001D86 RID: 7558
	public string DeleteAllRequestComplete;

	// Token: 0x04001D87 RID: 7559
	public string KickFromClan;

	// Token: 0x04001D88 RID: 7560
	public string KickFromClanProcessing;

	// Token: 0x04001D89 RID: 7561
	public string KickFromClanComplete;

	// Token: 0x04001D8A RID: 7562
	public string SendRequest;

	// Token: 0x04001D8B RID: 7563
	public string SendRequestProcessing;

	// Token: 0x04001D8C RID: 7564
	public string SendRequestComplete;

	// Token: 0x04001D8D RID: 7565
	public string RevokeRequest;

	// Token: 0x04001D8E RID: 7566
	public string RevokeRequestProcessing;

	// Token: 0x04001D8F RID: 7567
	public string RevokeRequestComplete;

	// Token: 0x04001D90 RID: 7568
	public string AcceptRequest;

	// Token: 0x04001D91 RID: 7569
	public string AcceptRequestProcessing;

	// Token: 0x04001D92 RID: 7570
	public string AcceptRequestComplete;

	// Token: 0x04001D93 RID: 7571
	public string DeleteRequest;

	// Token: 0x04001D94 RID: 7572
	public string DeleteRequestProcessing;

	// Token: 0x04001D95 RID: 7573
	public string DeleteRequestComplete;

	// Token: 0x04001D96 RID: 7574
	public string ExitClan;

	// Token: 0x04001D97 RID: 7575
	public string ExitClanProcessing;

	// Token: 0x04001D98 RID: 7576
	public string ExitClanComplete;

	// Token: 0x04001D99 RID: 7577
	public string ClanListLoading;

	// Token: 0x04001D9A RID: 7578
	public string ClanListLoadingDesc;

	// Token: 0x04001D9B RID: 7579
	public string ClanListLoadingFin;

	// Token: 0x04001D9C RID: 7580
	public string ClanListLoadingErrDesc;

	// Token: 0x04001D9D RID: 7581
	public string ClanDetailLoading;

	// Token: 0x04001D9E RID: 7582
	public string ClanDetailLoadingDesc;

	// Token: 0x04001D9F RID: 7583
	public string ClanDetailLoadingFin;

	// Token: 0x04001DA0 RID: 7584
	public string ClanDetailLoadingErrDesc;

	// Token: 0x04001DA1 RID: 7585
	public string ClansCheck;

	// Token: 0x04001DA2 RID: 7586
	public string ClansAvailable;

	// Token: 0x04001DA3 RID: 7587
	public string ClansUnavailable;

	// Token: 0x04001DA4 RID: 7588
	public string CurrentColor;

	// Token: 0x04001DA5 RID: 7589
	public string ClansHomePage;

	// Token: 0x04001DA6 RID: 7590
	public string ClansHomePageHint;

	// Token: 0x04001DA7 RID: 7591
	public string ClansHeadquarters1;

	// Token: 0x04001DA8 RID: 7592
	public string ClansHeadquarters2;

	// Token: 0x04001DA9 RID: 7593
	public string ClansLeader;

	// Token: 0x04001DAA RID: 7594
	public string ClansLieutenant;

	// Token: 0x04001DAB RID: 7595
	public string ClansOfficer;

	// Token: 0x04001DAC RID: 7596
	public string ClansContractor;

	// Token: 0x04001DAD RID: 7597
	public string ClansNotInClan;

	// Token: 0x04001DAE RID: 7598
	public string ClansLeaderShort;

	// Token: 0x04001DAF RID: 7599
	public string ClansLieutenantShort;

	// Token: 0x04001DB0 RID: 7600
	public string ClansOfficerShort;

	// Token: 0x04001DB1 RID: 7601
	public string Earn;

	// Token: 0x04001DB2 RID: 7602
	public string Role;

	// Token: 0x04001DB3 RID: 7603
	public string ClansEditPopupHeader;

	// Token: 0x04001DB4 RID: 7604
	public string ClansSetLeaderPopupHeader;

	// Token: 0x04001DB5 RID: 7605
	public string ClansSetLtPopupHeader;

	// Token: 0x04001DB6 RID: 7606
	public string ClansSetOfficerPopupHeader;

	// Token: 0x04001DB7 RID: 7607
	public string ClansDismissLtPopupHeader;

	// Token: 0x04001DB8 RID: 7608
	public string ClansDismissOfficerPopupHeader;

	// Token: 0x04001DB9 RID: 7609
	public string ClansSetLeaderPopupBody;

	// Token: 0x04001DBA RID: 7610
	public string ClansSetLtPopupBody;

	// Token: 0x04001DBB RID: 7611
	public string ClansDismissLtPopupBody;

	// Token: 0x04001DBC RID: 7612
	public string ClansSetOfficerPopupBody;

	// Token: 0x04001DBD RID: 7613
	public string ClansDismissOfficerPopupBody;

	// Token: 0x04001DBE RID: 7614
	public string ClansSetLeaderPopupHint;

	// Token: 0x04001DBF RID: 7615
	public string ClansSetLtPopupHint;

	// Token: 0x04001DC0 RID: 7616
	public string ClansSetOfficerPopupHint;

	// Token: 0x04001DC1 RID: 7617
	public string ClansEditMessagePopup;

	// Token: 0x04001DC2 RID: 7618
	public string ClansEditMessageCharactersleft;

	// Token: 0x04001DC3 RID: 7619
	public string ClansDefaultMessage;

	// Token: 0x04001DC4 RID: 7620
	public string ClansError1001;

	// Token: 0x04001DC5 RID: 7621
	public string ClansError1002;

	// Token: 0x04001DC6 RID: 7622
	public string ClansError1003;

	// Token: 0x04001DC7 RID: 7623
	public string ClansError1004;

	// Token: 0x04001DC8 RID: 7624
	public string ClansError1005;

	// Token: 0x04001DC9 RID: 7625
	public string ClansError1006;

	// Token: 0x04001DCA RID: 7626
	public string ClansError1007;

	// Token: 0x04001DCB RID: 7627
	public string ClansError1008;

	// Token: 0x04001DCC RID: 7628
	public string ClansError1009;

	// Token: 0x04001DCD RID: 7629
	public string ClansError1010;

	// Token: 0x04001DCE RID: 7630
	public string ClansError1011;

	// Token: 0x04001DCF RID: 7631
	public string ClansError1012;

	// Token: 0x04001DD0 RID: 7632
	public string ClansError1013;

	// Token: 0x04001DD1 RID: 7633
	public string ClansError1014;

	// Token: 0x04001DD2 RID: 7634
	public string ClansError1015;

	// Token: 0x04001DD3 RID: 7635
	public string ClansError1016;

	// Token: 0x04001DD4 RID: 7636
	public string ClansError1017;

	// Token: 0x04001DD5 RID: 7637
	public string ClansError1018;

	// Token: 0x04001DD6 RID: 7638
	public string ClansError1019;

	// Token: 0x04001DD7 RID: 7639
	public string ClansError1020;

	// Token: 0x04001DD8 RID: 7640
	public string ClansError1100;

	// Token: 0x04001DD9 RID: 7641
	public string NotEnoughWarriorTasks;

	// Token: 0x04001DDA RID: 7642
	public string NotEnoughWarriorExp;

	// Token: 0x04001DDB RID: 7643
	public string Reward;

	// Token: 0x04001DDC RID: 7644
	public string And;

	// Token: 0x04001DDD RID: 7645
	public string For;

	// Token: 0x04001DDE RID: 7646
	public string GotAchievement0;

	// Token: 0x04001DDF RID: 7647
	public string GotAchievement1;

	// Token: 0x04001DE0 RID: 7648
	public string Roulette;

	// Token: 0x04001DE1 RID: 7649
	public string RouletteTries;

	// Token: 0x04001DE2 RID: 7650
	public string RouletteDescription;

	// Token: 0x04001DE3 RID: 7651
	public string RouleteTriesLeft;

	// Token: 0x04001DE4 RID: 7652
	public string RouletteTriesAdd;

	// Token: 0x04001DE5 RID: 7653
	public string RouletteRoll;

	// Token: 0x04001DE6 RID: 7654
	public string RouletteWin;

	// Token: 0x04001DE7 RID: 7655
	public string RouletteWinSpecial;

	// Token: 0x04001DE8 RID: 7656
	public string RouletteWinSkill;

	// Token: 0x04001DE9 RID: 7657
	public string RouletteWinWeapon;

	// Token: 0x04001DEA RID: 7658
	public string RouletteLose;

	// Token: 0x04001DEB RID: 7659
	public string RouletteTryAgain;

	// Token: 0x04001DEC RID: 7660
	public string RouletteTriesEnded;

	// Token: 0x04001DED RID: 7661
	public string RouletteWaitOrBuy;

	// Token: 0x04001DEE RID: 7662
	public string RouletteSkill;

	// Token: 0x04001DEF RID: 7663
	public string RouletteWeapon;

	// Token: 0x04001DF0 RID: 7664
	public string RoulettePopupHeader;

	// Token: 0x04001DF1 RID: 7665
	public string RoulettePopupBody;

	// Token: 0x04001DF2 RID: 7666
	public string RouletteCamo;

	// Token: 0x04001DF3 RID: 7667
	public string RouletteAttempt;

	// Token: 0x04001DF4 RID: 7668
	public string RouletteOneAttempt;

	// Token: 0x04001DF5 RID: 7669
	public string RouletteCamouflage;

	// Token: 0x04001DF6 RID: 7670
	public string DataBaseFailure;

	// Token: 0x04001DF7 RID: 7671
	public string LevelFailure;

	// Token: 0x04001DF8 RID: 7672
	public string LeagueRank;

	// Token: 0x04001DF9 RID: 7673
	public string LeaguePlace;

	// Token: 0x04001DFA RID: 7674
	public string LeagueLP;

	// Token: 0x04001DFB RID: 7675
	public string LeagueWins;

	// Token: 0x04001DFC RID: 7676
	public string LeagueDefeats;

	// Token: 0x04001DFD RID: 7677
	public string LeagueLeaves;

	// Token: 0x04001DFE RID: 7678
	public string LeagueRatio;

	// Token: 0x04001DFF RID: 7679
	public string LeagueRules;

	// Token: 0x04001E00 RID: 7680
	public string LeagueCurrentPrizes;

	// Token: 0x04001E01 RID: 7681
	public string LeaguePastPrizes;

	// Token: 0x04001E02 RID: 7682
	public string LeagueFuturePrizes;

	// Token: 0x04001E03 RID: 7683
	public string LeagueRating;

	// Token: 0x04001E04 RID: 7684
	public string LeagueSearchGame;

	// Token: 0x04001E05 RID: 7685
	public string LeagueCancel;

	// Token: 0x04001E06 RID: 7686
	public string LeagueBoosters;

	// Token: 0x04001E07 RID: 7687
	public string LeagueSeasonEnd;

	// Token: 0x04001E08 RID: 7688
	public string LeagueSeasonBreak;

	// Token: 0x04001E09 RID: 7689
	public string LeagueRatingHeaderLvl;

	// Token: 0x04001E0A RID: 7690
	public string LeagueRatingHeaderNameNick;

	// Token: 0x04001E0B RID: 7691
	public string LeagueRatingHeaderLP;

	// Token: 0x04001E0C RID: 7692
	public string LeagueRatingHeaderWins;

	// Token: 0x04001E0D RID: 7693
	public string LeagueRatingHeaderDefeats;

	// Token: 0x04001E0E RID: 7694
	public string LeagueRatingHeaderLeaves;

	// Token: 0x04001E0F RID: 7695
	public string LeagueSearching;

	// Token: 0x04001E10 RID: 7696
	public string LeagueInQueue;

	// Token: 0x04001E11 RID: 7697
	public string LeaguePlaying;

	// Token: 0x04001E12 RID: 7698
	public string LeagueAdBoosters;

	// Token: 0x04001E13 RID: 7699
	public string LeagueGameReady;

	// Token: 0x04001E14 RID: 7700
	public string LeagueAccept;

	// Token: 0x04001E15 RID: 7701
	public string LeagueMap;

	// Token: 0x04001E16 RID: 7702
	public string LeagueMode;

	// Token: 0x04001E17 RID: 7703
	public string LeagueMapLoading;

	// Token: 0x04001E18 RID: 7704
	public string LeagueGameStarted;

	// Token: 0x04001E19 RID: 7705
	public string LeagueWaitingPlayers;

	// Token: 0x04001E1A RID: 7706
	public string LeagueMatchStart;

	// Token: 0x04001E1B RID: 7707
	public string LeagueGoneGameResult;

	// Token: 0x04001E1C RID: 7708
	public string LeagueYourTeamWon;

	// Token: 0x04001E1D RID: 7709
	public string LeagueYourTeamLose;

	// Token: 0x04001E1E RID: 7710
	public string LeagueTie;

	// Token: 0x04001E1F RID: 7711
	public string LeagueShare;

	// Token: 0x04001E20 RID: 7712
	public string LeagueNext;

	// Token: 0x04001E21 RID: 7713
	public string LeaguePointLeft;

	// Token: 0x04001E22 RID: 7714
	public string LeagueYouLeave;

	// Token: 0x04001E23 RID: 7715
	public string LeagueNotAvailable;

	// Token: 0x04001E24 RID: 7716
	public string LeaguePrizes;

	// Token: 0x04001E25 RID: 7717
	public string LeagueNickname;

	// Token: 0x04001E26 RID: 7718
	public string LeagueKills;

	// Token: 0x04001E27 RID: 7719
	public string LeagueDeaths;

	// Token: 0x04001E28 RID: 7720
	public string LeagueAssists;

	// Token: 0x04001E29 RID: 7721
	public string LeagueUnknown;

	// Token: 0x04001E2A RID: 7722
	public string LeagueOffSeason;

	// Token: 0x04001E2B RID: 7723
	public string LeagueWinners;

	// Token: 0x04001E2C RID: 7724
	public string LeaguePopupFirst;

	// Token: 0x04001E2D RID: 7725
	public string LeaguePopupSecond;

	// Token: 0x04001E2E RID: 7726
	public string LeaguePopupThird;

	// Token: 0x04001E2F RID: 7727
	public string LeaguePopupYourResults;

	// Token: 0x04001E30 RID: 7728
	public string LeaguePopupYourRewards;

	// Token: 0x04001E31 RID: 7729
	public string LeaguePopupCongrats;

	// Token: 0x04001E32 RID: 7730
	public string LeagueCurrent;

	// Token: 0x04001E33 RID: 7731
	public string LeaguePast;

	// Token: 0x04001E34 RID: 7732
	public string LeagueFuture;

	// Token: 0x04001E35 RID: 7733
	public string LeagueNotification1;

	// Token: 0x04001E36 RID: 7734
	public string LeagueNotification2;

	// Token: 0x04001E37 RID: 7735
	public string League;

	// Token: 0x04001E38 RID: 7736
	public string LeagueUpper;

	// Token: 0x04001E39 RID: 7737
	public string LeagueLoading;

	// Token: 0x04001E3A RID: 7738
	public string LeagueAvailableHintStart;

	// Token: 0x04001E3B RID: 7739
	public string LeagueAvailableHintEnd;

	// Token: 0x04001E3C RID: 7740
	public string Hours;

	// Token: 0x04001E3D RID: 7741
	public string HightPacketLoss;

	// Token: 0x04001E3E RID: 7742
	public string HighPing;

	// Token: 0x04001E3F RID: 7743
	public string GettingServerList;

	// Token: 0x04001E40 RID: 7744
	public string Sorting;

	// Token: 0x04001E41 RID: 7745
	public string GamelistIsNotAvailable;

	// Token: 0x04001E42 RID: 7746
	public string KillStreak;

	// Token: 0x04001E43 RID: 7747
	public string WeatherEffects;

	// Token: 0x04001E44 RID: 7748
	public string UnityCaching;

	// Token: 0x04001E45 RID: 7749
	public string ProKillScreenShotSetting;

	// Token: 0x04001E46 RID: 7750
	public string QuadKillScreenShotSetting;

	// Token: 0x04001E47 RID: 7751
	public string LevelUpScreenShotSetting;

	// Token: 0x04001E48 RID: 7752
	public string AchievementScreenShotSetting;

	// Token: 0x04001E49 RID: 7753
	public string AutoScreenshotAt;

	// Token: 0x04001E4A RID: 7754
	public string GoFullscreen;

	// Token: 0x04001E4B RID: 7755
	public string FriendsShort;

	// Token: 0x04001E4C RID: 7756
	public string FriendsOne;

	// Token: 0x04001E4D RID: 7757
	public string FriendsSeveral;

	// Token: 0x04001E4E RID: 7758
	public string AddToFavorites;

	// Token: 0x04001E4F RID: 7759
	public string RemoveFromFavorites;

	// Token: 0x04001E50 RID: 7760
	public string AddToFavoritesQuestion;

	// Token: 0x04001E51 RID: 7761
	public string AddToFavoritesHint;

	// Token: 0x04001E52 RID: 7762
	public string RemoveFromFavoritesQuestion;

	// Token: 0x04001E53 RID: 7763
	public string AllowToAddMe;

	// Token: 0x04001E54 RID: 7764
	public string AddToFavoritesLimitReached;

	// Token: 0x04001E55 RID: 7765
	public string AddToFavoritesDeniedByUser;

	// Token: 0x04001E56 RID: 7766
	public string HintRatingBtnTop;

	// Token: 0x04001E57 RID: 7767
	public string SeasonTop;

	// Token: 0x04001E58 RID: 7768
	public string Season;

	// Token: 0x04001E59 RID: 7769
	public string HintRatingBtnHardcore;

	// Token: 0x04001E5A RID: 7770
	public string HintRatingBtnTopOnline;

	// Token: 0x04001E5B RID: 7771
	public string HintRatingBtnTopFriends;

	// Token: 0x04001E5C RID: 7772
	public string HintRatingBtnTopYourPosition;

	// Token: 0x04001E5D RID: 7773
	public string HintRatingBtnFavorites;

	// Token: 0x04001E5E RID: 7774
	public string HintRatingBtnRefresh;

	// Token: 0x04001E5F RID: 7775
	public string HintRatingBtnInfo;

	// Token: 0x04001E60 RID: 7776
	public string HintRatingBtnAddToFavorites;

	// Token: 0x04001E61 RID: 7777
	public string WatchlistLoadingTitle;

	// Token: 0x04001E62 RID: 7778
	public string WatchlistLoadingBody;

	// Token: 0x04001E63 RID: 7779
	public string WatchlistLoadedBody;

	// Token: 0x04001E64 RID: 7780
	public string BankTransactions;

	// Token: 0x04001E65 RID: 7781
	public string BankTransactionsLoading;

	// Token: 0x04001E66 RID: 7782
	public string BankTransactionsLoaded;

	// Token: 0x04001E67 RID: 7783
	public string RepairAllWeaponPopupHeader;

	// Token: 0x04001E68 RID: 7784
	public string RepairAllWeaponPopupBody1;

	// Token: 0x04001E69 RID: 7785
	public string RepairAllWeaponPopupBody2;

	// Token: 0x04001E6A RID: 7786
	public string ProfileReset;

	// Token: 0x04001E6B RID: 7787
	public string ProfileResetNotification;

	// Token: 0x04001E6C RID: 7788
	public string ProfileResetConfirmation0;

	// Token: 0x04001E6D RID: 7789
	public string ProfileResetConfirmation1;

	// Token: 0x04001E6E RID: 7790
	public string ProfileResetConfirmation2;

	// Token: 0x04001E6F RID: 7791
	public string DownloadWeapons;

	// Token: 0x04001E70 RID: 7792
	public string WeaponsLoaded;

	// Token: 0x04001E71 RID: 7793
	public string DownloadMaps;

	// Token: 0x04001E72 RID: 7794
	public string MapsLoaded;

	// Token: 0x04001E73 RID: 7795
	public string ReceivingInformation;

	// Token: 0x04001E74 RID: 7796
	public string ErrorDownloadingContent;

	// Token: 0x04001E75 RID: 7797
	public string WeaponSizeLoaded;

	// Token: 0x04001E76 RID: 7798
	public string SizeTotal;

	// Token: 0x04001E77 RID: 7799
	public string DownloadAllWeapons;

	// Token: 0x04001E78 RID: 7800
	public string MapsSizeLoaded;

	// Token: 0x04001E79 RID: 7801
	public string DownloadAllMaps;

	// Token: 0x04001E7A RID: 7802
	public string MasteringPopupMetaBuyHeader;

	// Token: 0x04001E7B RID: 7803
	public string MasteringPopupMetaBuyBody;

	// Token: 0x04001E7C RID: 7804
	public string MasteringPopupModBuyHeader;

	// Token: 0x04001E7D RID: 7805
	public string MasteringPopupModBuyBody;

	// Token: 0x04001E7E RID: 7806
	public string Save;

	// Token: 0x04001E7F RID: 7807
	public string MasteringPopupSaveModHeader;

	// Token: 0x04001E80 RID: 7808
	public string MasteringPopupSaveModProcess;

	// Token: 0x04001E81 RID: 7809
	public string MasteringPopupSaveModComplete;

	// Token: 0x04001E82 RID: 7810
	public string MasteringPopupSaveModError;

	// Token: 0x04001E83 RID: 7811
	public string MasteringPopupModBuyProcess;

	// Token: 0x04001E84 RID: 7812
	public string MasteringPopupModBuyComplete;

	// Token: 0x04001E85 RID: 7813
	public string MasteringPopupMetaBuyProcess;

	// Token: 0x04001E86 RID: 7814
	public string MasteringPopupMetaBuyComplete;

	// Token: 0x04001E87 RID: 7815
	public string MasteringNotEnoughMp;

	// Token: 0x04001E88 RID: 7816
	public string NotEnoughGp;

	// Token: 0x04001E89 RID: 7817
	public string NotEnoughCr;

	// Token: 0x04001E8A RID: 7818
	public string Sight;

	// Token: 0x04001E8B RID: 7819
	public string MuzzleDevice;

	// Token: 0x04001E8C RID: 7820
	public string TacticalDevice;

	// Token: 0x04001E8D RID: 7821
	public string AmmoType;

	// Token: 0x04001E8E RID: 7822
	public string ModSlotUnavailable;

	// Token: 0x04001E8F RID: 7823
	public string MasteringWtaskHint;

	// Token: 0x04001E90 RID: 7824
	public string MasteringWeaponExp;

	// Token: 0x04001E91 RID: 7825
	public string WeaponCustomization;

	// Token: 0x04001E92 RID: 7826
	public string MpBuyError;

	// Token: 0x04001E93 RID: 7827
	public string Purchase;

	// Token: 0x04001E94 RID: 7828
	public string GetFree;

	// Token: 0x04001E95 RID: 7829
	public string FreeCamouflage;

	// Token: 0x04001E96 RID: 7830
	public string PopupCamouflageBuyHeader;

	// Token: 0x04001E97 RID: 7831
	public string PopupCamouflageBuyBody;

	// Token: 0x04001E98 RID: 7832
	public string PopupCamouflageBuyComplete;

	// Token: 0x04001E99 RID: 7833
	public string PopupCamouflageBuyProcess;

	// Token: 0x04001E9A RID: 7834
	public string Equipped;

	// Token: 0x04001E9B RID: 7835
	public string SortByName;

	// Token: 0x04001E9C RID: 7836
	public string SortByPrice;

	// Token: 0x04001E9D RID: 7837
	public string RouletteDiscount1;

	// Token: 0x04001E9E RID: 7838
	public string RouletteDiscount2Part1;

	// Token: 0x04001E9F RID: 7839
	public string RouletteDiscount2Part2;

	// Token: 0x04001EA0 RID: 7840
	public string RouletteDiscount3Part1;

	// Token: 0x04001EA1 RID: 7841
	public string RouletteDiscount3Part2;

	// Token: 0x04001EA2 RID: 7842
	public string Bonus;

	// Token: 0x04001EA3 RID: 7843
	public string Achievement;

	// Token: 0x04001EA4 RID: 7844
	public string StandaloneLoginCaption;

	// Token: 0x04001EA5 RID: 7845
	public string StandaloneMailTextFieldCaption;

	// Token: 0x04001EA6 RID: 7846
	public string StandalonePassTextFieldCaption;

	// Token: 0x04001EA7 RID: 7847
	public string StandaloneSignUp;

	// Token: 0x04001EA8 RID: 7848
	public string StandaloneSignIn;

	// Token: 0x04001EA9 RID: 7849
	public string WrongLoginData;

	// Token: 0x04001EAA RID: 7850
	public string ShowPassword;

	// Token: 0x04001EAB RID: 7851
	public string RetypePassword;

	// Token: 0x04001EAC RID: 7852
	public string LoginAttemptsExceeded;

	// Token: 0x04001EAD RID: 7853
	public string Seconds;

	// Token: 0x04001EAE RID: 7854
	public string UnknownReasonLoginFail;

	// Token: 0x04001EAF RID: 7855
	public string TransferProfile;

	// Token: 0x04001EB0 RID: 7856
	public string ProfileTransferedSuccessfully;

	// Token: 0x04001EB1 RID: 7857
	public string ProfileAlreadyTransfered;

	// Token: 0x04001EB2 RID: 7858
	public string ProfileTransferError;

	// Token: 0x04001EB3 RID: 7859
	public string NewLevel;

	// Token: 0x04001EB4 RID: 7860
	public string PressToPost;

	// Token: 0x04001EB5 RID: 7861
	public string Copy;

	// Token: 0x04001EB6 RID: 7862
	public string Exit;

	// Token: 0x04001EB7 RID: 7863
	public string VersionCheckCaption;

	// Token: 0x04001EB8 RID: 7864
	public string VersionCheckDescription;

	// Token: 0x04001EB9 RID: 7865
	public string Language;

	// Token: 0x04001EBA RID: 7866
	public string RusLanguage;

	// Token: 0x04001EBB RID: 7867
	public string EngLanguage;

	// Token: 0x04001EBC RID: 7868
	public string SavePassword;

	// Token: 0x04001EBD RID: 7869
	public string AreYouSure;

	// Token: 0x04001EBE RID: 7870
	public string[] AwardsHints;

	// Token: 0x04001EBF RID: 7871
	public string SeasonAward;

	// Token: 0x04001EC0 RID: 7872
	public string SeasonAwardDescription;

	// Token: 0x04001EC1 RID: 7873
	public string MipmapCheckFailCaption;

	// Token: 0x04001EC2 RID: 7874
	public string MipmapCheckFailDescription;

	// Token: 0x04001EC3 RID: 7875
	public string HopsKeyWonCaption;

	// Token: 0x04001EC4 RID: 7876
	public string HopsKeyWonDescription;

	// Token: 0x04001EC5 RID: 7877
	public string HopsActivationInstruction;
}
