using Unity.VisualScripting;
using UnityEngine;

public class RemoveDecay : MonoBehaviour
{
    private void Awake()
    {
        this.DelayCall(0.5f, () =>
        {
            Destroy(gameObject);
        });
    }
}
