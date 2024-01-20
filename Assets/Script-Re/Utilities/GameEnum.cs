public enum Events
{
    CameraChangeColorBackGround,
    CameraDefault,
    CameraFocus,
    CameraChangeTarget,
    CameraTargetPlayer,
    GoToMap1,
    GoToMap2,
    EnemyGoToWayPoint,
    BruterTriggerAni,
    WhoWaitTriggerAni,
    SetVideoIntro,
    PlayTalkScript3,
    OnAttackHitEnemy,
    PlayerTransform,
    PlayerDirection,
    PlayerTriggerAni,
    PlayerChangeWeapon,
    PlayerChangeCurses,
    PlayerDied,
    PlayerEndLevel,
    BlackHeartbreak,
    SetWeapon,
    SetOnEvent,
    UpdateAttackRate,
    PlayerTakeDmg,
    UpdateValueAngry,
    UpdateInfoWeapon,
    UpdateInfoCurses,
    UpdateIconWeapon,
    UpdateIconCurses,
    UpdateUICoin,
    UpdateValueHunger,
    UpdateValueExp,
    AddHeartAndRestoreFull,
    RestoreHeart,
    CheckCurrentHP,
    UIButtonOpen,
    UIButtonReset,
    DialogBoxChangeTalkScript,
    OnTringgerWaitAction,
    TheScriptTalkEnd,
    MoveToWaypoint,
    SetDefault,
    BaseStartGame
}
public enum MenuType
{
    PressToPlay,
    Play,
    Settings,
    Credits,
    RoadMap,
    Quit,
    buttonAccept,
    buttonBack,
    buttonReset,
    buttonOnQuitBack,
    buttonOnQuitAccept,
    backOnStartMenu,
    SaveGame1,
    SaveGame2,
    SaveGame3,
    DeleteSaveGame,
    BackToMenu,
    QuitGame,
    Resume,
    Help,
    MainMenu,
}
public enum TypeEnemy
{
    EneScamp,
    SwordsMan,
    EnemyBat,
    EnemyBigBat,
    DeathCatEyeball,
    EneScampArcher,
    EneScampShield,
    Scythesman,
    EneScampBoom,
    EneSummoner,
    EneHealer,
    EneRedArcher,
    EneGuardian,
    EneRedGuardian
}
public enum NameCurses
{
    FlamingShot,
    CleansingFire,
    HoundsOfFate,
    DivineBlast,
    DivineGuardian,
    DivineBlizzard,
    DivineBlight,
    DeatsSweep,
    OathOfTheCrown,
    DeathsAttendant,
    DeathsSquall,
}
public enum TypeEffectEnemy
{
    ArrowEnemy,
    FireballsEnemy,
    ObjBoomEnemy,
    HoundsofFate,
    Fireballs,
    DivineGuardian,
    DivineBlizzard,
    DivineBlight,
    DivineBlast,
    ObjBoom,
    Slashes,
    ObjRingDeadEnemy,
}
public enum TypeSword
{
    CrusadersBlade,
    BaneSword,
    NecromanticSword,
    VampiricSword,
    MercilessSword,
    ZealousSword,
    GodlySword
}
public enum TypeLevelDifficult
{
    Easy,
    Medium,
    Hard,
    ExtraHard
}
public enum TypeEffectAnimation
{
    HitObject,
    HitObject2,
    SpawnEnemyEffect,
    FxAniSlash
}
public enum OnScenes
{
    IntroGame,
    VampireSurvivor,
    MenuVampireSurvivor
}

public enum AttributeType
{
    Level,
    MaxRedHeart,
    CurrentRedHeart,
    MaxRedAddHeart,
    CurrentRedAddHeart,
    MaxBlueHeart,
    CurrentBlueHeart,
    MaxBlackHeart,
    CurrentBlackHeart,
    MaxExpOfLevel,
    CurrentExp,
    MaxValueAngry,
    CurrentAngry,
    CurrentCoin,
    MaxValueHunger,
    CurrentHunger,
    CriticalHit,
    AttackRate,
    IncreasedMovementSpeed,
    ChanceOfHealing,
    ChanceOfNegatingDamage,
    LuckyChest,
    ElapsedTime,
    CountKillEnemy,
    TypeLvlSword,
    TypeLvlCruses,
    CursesConsumeLess,
    TheBomb,
    BombDamage,
    CurseDamageMultiple
}
public enum ShopPowerAttributes
{
    MaxValueAngry,
    MaxBlackHeart,
    CriticalHit,
    AttackRate,
    IncreasedMovementSpeed,
    ChanceOfHealing,
    ChanceOfNegatingDamage,
    MaxRedAddHeart,
    TypeLvlSword,
    TypeLvlCruses,
    CursesConsumeLess,
    BombDamage,
    CurseDamageMultiple,
}
public enum CardType
{
    TheHeartsI,
    TheHeartsII,
    TheHeartsIII,
    TheLoversI,
    TheLoversII,
    DiseasedHeart,
    DivineStrength,
    ThePath,
    SoulSnatcher,
    ShieldOfFaith,
    RabbitsFoot,
    TrueSight,
    DivineCurse,
    TheBomb
}
public enum SaveGameSlot
{
    Slot1,
    Slot2,
    Slot3,
    Slot4
}
public enum StateSaveGame
{
    NewGame,
    Continue
}
public enum ListTypeEffects
{
    None,
    EffectDestroyGrass,
    EffectDestroySkeleton,
    EffectDestroyStone
}
public enum TypeUIButton
{
    ButtonE,
    Mouse,
    TextShow
}
public enum TalkScript
{
    IntroGame1,
    IntroGame2,
    IntroGame3
}
public enum ChestType
{
    Common,
    UnCommon,
    Race
}
public enum TypeInfoWeapon
{
    NameWeapon,
    QueteWeapon,
    Description,
    Damage,
    Speed
}
public enum TypeCurses
{
    Fireballs,
    Blasts,
    Slashes,
    Null
}
public enum LevelRomanNumerals
{
    I = 1,
    II = 2,
    III = 3,
    IV = 4,
    V = 5,
    VI = 6,
    VII = 7,
    VIII = 8,
    IX = 9,
    X = 10,
}

public enum TypeItemsCanDrop
{
    ObjDropCoin,
    ObjDropTarotCard,
    ObjDropAngry,
    ObjDropHeart,
    ObjDropExp,
}
public enum TypeMenuTab
{
    Inventory,
    Player,
    Clut,
    Quests,
    PowerUp,
    Tarrot
}

public enum TypeTabInfomation
{
    Inventory,
    Player,
    Clut,
    Quest
}
public enum TypeShowButton
{
    Talk,
    Items,
    TakeWeapon,
    None
}
public enum EnemGrHeart
{
    Red,
    Add,
    Blue,
    Black
}
public enum EnemGrPriteHeart
{
    Red,
    RedHalf,
    Add,
    AddHalf,
    Blue,
    BlueHalf,
    Black
}
public enum HeartType
{
    Half = 1,
    Full = 2
}
public enum HeartInfo
{
    Empty,
    Half,
    Full
}
public enum TypeListAudio
{
    ListAudioStartGame,
    ListAudioShop
}
public enum ListAudioOnRound
{
    AudioOnRound1,
    AudioOnRound2,
    AudioOnRound3,
    AudioOnRound4,
    AudioOnRound5,
}
public enum ListAudioShop
{
    Shop1,
    Shop2,
}
public enum TypeControlDifficult
{
    MaxRedHeart
}

