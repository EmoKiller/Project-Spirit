using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class ObseverConstants
{
    public static UnityEvent<AttributeType, float> OnAttributeValueChanged = new UnityEvent<AttributeType, float>();
}
