using UnityEngine.Events;

public static class ObseverConstants
{
    public static UnityEvent<AttributeType, float> OnAttributeValueChanged = new UnityEvent<AttributeType, float>();
    public static UnityEvent<AttributeType, float> OnIncreaseAttributeValue = new UnityEvent<AttributeType, float>();
    public static UnityEvent OnClickButtonStart = new UnityEvent();
    public static UnityEvent OnClickButtonContinue = new UnityEvent();
    public static UnityEvent<float> OnBlackHeartBreak = new UnityEvent<float>();
}
 