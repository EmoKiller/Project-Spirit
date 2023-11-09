using Unity.VisualScripting;
using UnityEngine;

public class RemoveDecay : MonoBehaviour
{
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
