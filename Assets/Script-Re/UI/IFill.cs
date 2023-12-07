using UnityEngine;
using UnityEngine.UI;

public class IFill : MonoBehaviour
{
    public TypeFIll Type;
    [SerializeField] Image filled;
    public float FilledMount
    {
        get
        {
            return filled.fillAmount;
        }
        set
        {
            filled.fillAmount = value;
        }
    }
}
