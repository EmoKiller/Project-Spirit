
public enum TalkScript
{
    IntroGame1,
    IntroGame2,
    IntroGame3
}

public enum Events
{
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
    //UiDungeonManager
    UpdateIconWeapon,
    UpdateIconCurses,
    //UIManager
    UpdateInfoWeapon,
    //Cammera
    CameraChangeColorBackGround,
    CameraDefault,
    CameraFocus,
    CameraChangeTarget,
    CameraTargetPlayer,
    // UIDialogBox
    DialogBoxChangeTalkScript,
    //UIButtonAction
    UIButtonOpen,
    UIButtonUpdateText,
    UIButtonReset,
    //OnTringgerWaitAction
    OnTringgerWaitAction,
    //TriggerTalk
    TheScriptTalkEnd,

    MoveToWaypoint,
    SetDefault,
    
    
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
public enum SpaceState
{
    TheBase,
    DarkWood,
    Anura,
    AnchorDeep,
    SilkCradle
}
public enum TypeMenuTab
{
    Inventory,
    Player,
    Clut,
    Quests
}
public enum Enemys
{
    Chaser
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
public enum AttributeTypeOfHero
{
    baseAttribute,
}
public enum AttributeType
{
    HP,
    MP,
    SP
}
public enum ItemsType
{
    Weapon,
    Items
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



