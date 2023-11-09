using UnityEngine;

public class DropItemsOnWorld : MonoBehaviour
{
    public ItemsType type;
    public GameObject Item = null;
    public bool IsTake
    {
        get { return isTake; }
        set { isTake = value; }
    }
    private bool isTake;
    public void TakeItems()
    {
        IsTake = true;
    }

    
}
