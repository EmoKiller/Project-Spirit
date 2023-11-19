using UnityEngine;

public class ItemsDrop : MonoBehaviour
{
    private Sprite _Item = null;
    public Sprite ItemSpr
    {
        get
        { 
            if (_Item == null)
                _Item = GetComponentInChildren<Sprite>();
            return _Item;
        }
        set
        {
            _Item = value;
        }
    }

}
