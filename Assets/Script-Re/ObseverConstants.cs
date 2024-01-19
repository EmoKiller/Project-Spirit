using UnityEngine.Events;

public static class ObseverConstants
{
    public static UnityEvent<AttributeType, float> OnAttributeValueChanged = new UnityEvent<AttributeType, float>();
    public static UnityEvent<AttributeType, float> OnIncreaseAttributeValue = new UnityEvent<AttributeType, float>();
    public static UnityEvent<AttributeType, float> OnRestoreHeart = new UnityEvent<AttributeType, float>();
    public static UnityEvent ReloadScene = new UnityEvent();
    public static UnityEvent OnClickButtonStart = new UnityEvent();
    public static UnityEvent OnClickButtonContinue = new UnityEvent();
    public static UnityEvent<float> OnBlackHeartBreak = new UnityEvent<float>();
    public static UnityEvent OnSpawnBoss = new UnityEvent();
    public static UnityEvent OnBossDeath = new UnityEvent();
}
 