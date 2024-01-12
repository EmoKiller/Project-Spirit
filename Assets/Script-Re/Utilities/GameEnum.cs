public enum TypeLevelDifficult
{
    Easy,
    Medium,
    Hard,
    ExtraHard
}
public enum TypeControlDifficult
{
    MaxRedHeart
}
public enum TypeEffectEnemy
{
    ArrowEnemy,
    FireballsEnemy,
}
public enum TypeEffectAnimation
{
    HitObject,
    HitObject2,
    SpawnEnemyEffect
}
public enum OnScenes
{
    IntroGame,
    VampireSurvivor
}
public enum SaveGameSlot
{
    Slot1,
    Slot2,
    Slot3
}
public enum StateSaveGame
{
    NewGame,
    Continue
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
    IchorThrown,
    PointOfCorruption,
    PathOfTheRighteous,
    CallOfTheCrown,
    TouchOfTurua,
    Maelstrom,
    TouchOfIthaqua,
    TouchOfTheRevenant

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
    MovementSpeed,
    ElapsedTime,
    CountKillEnemy,

}

public enum CardType
{
    TheHeartsI,
    TheHeartsII,
    TheHeartsIII,
    TheLoversI,
    TheLoversII,
    DiseasedHeart,
    TheArachnid,
    DivineStrength,
    ThePath,
    FervoursHarvest,
    SoulSnatcher,
    ShieldOfFaith,
    TheBomb,
    DivineCurse,
    FortuneBlessing,
    DeathsDoor,
    RabbitsFoot,
    Ambrosia,
    GiftFromBelow,
    TrueSight
}
public enum TypeCoins
{
    Coin1 = 1,
    Coin2 = 2,
    Coin3 = 3
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
    Splatters,
    Tentacles,
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
public enum Events
{
    //Cammera
    CameraChangeColorBackGround,
    CameraDefault,
    CameraFocus,
    CameraChangeTarget,
    CameraTargetPlayer,
    //IntroGame
    GoToMap1,
    GoToMap2,
    EnemyGoToWayPoint,
    BruterTriggerAni,
    WhoWaitTriggerAni,
    SetVideoIntro,
    PlayTalkScript3,
    //player

    OnAttackHitEnemy,



    PlayerTransform,
    PlayerDirection,//func
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
    //UIManager
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
    //popup

    // UIDialogBox
    DialogBoxChangeTalkScript,
    //OnTringgerWaitAction
    OnTringgerWaitAction,
    //TriggerTalk
    TheScriptTalkEnd,

    MoveToWaypoint,
    SetDefault,
    //GameManager
    BaseStartGame, //Func

}
public enum TypeItemsCanDrop
{
    ObjDropCoin,
    ObjDropTarotCard,
    ObjDropAngry,
    ObjDropHeart,
    ObjDropExp,
}
public enum ChestType
{
    Common,
    UnCommon,
    Race
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
    BackToMenu
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

