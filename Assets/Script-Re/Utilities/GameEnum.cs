public enum TypeInfomation
{
    Angry,
    IconWeapon,
    IconCurses,
    CardTarot,
    Coin,
    Hunger
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
    Scamp,
    SwordsMan,
    Chaser
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
    Tentacles
}
public enum LevelItems
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
    /// <summary>
    /// /
    /// </summary>
    DebugAction,
    //IntroGame
    GoToMap1,
    GoToMap2,
    EnemyGoToWayPoint,
    BruterTriggerAni,
    WhoWaitTriggerAni,
    SetVideoIntro,
    PlayTalkScript3,
    //player
    PlayerTransform,
    PlayerDirection,//func
    PlayerTriggerAni,
    PlayerChangeWeapon,
    SetWeapon,
    //UiDungeonManager
    UpdateIconWeapon,
    UpdateIconCurses,
    //UIManager
    UpdateInfoWeapon,
    UpdateInfoCurses,
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
    //UIManager
    PlayerTakeDamage,
    CreateNewHeart,
    UIButtonOpen,
    UIButtonReset,

}
public enum TypeSave
{
    Level,
    Exp,
    AmountFollowers,
    AmountOfCoin
}
public enum TypeFIll
{
    Angry,
    Faith,
    Hygiene,
    Hunger
}
public enum TypeAmount
{
    Coin,
    Followers
}
public enum ListCharacterAction
{
    Actions,
    Attack,
    OnDead
}
public enum TypeItemsCanDrop
{
    Coin,
    TarotCard,
    BerryBushSeed,
    Necklace
}
public enum ChestType
{
    Common,
    UnCommon,
    Race,
    EndOfFloor
}
public enum TypeMenuTab
{
    Inventory,
    Player,
    Clut,
    Quests
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
    Play,
    Settings,
    Credits,
    RoadMap,
    Quit,
    SaveGame1,
    SaveGame2,
    SaveGame3,
    Back,
    DeleteSaveGame
}
public enum UIsizeObj
{
    Nomal,
    Large,
    Half
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


