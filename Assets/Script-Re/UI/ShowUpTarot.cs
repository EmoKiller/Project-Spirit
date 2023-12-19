using System.Collections.Generic;
using UnityEngine;

public class ShowUpTarot : MonoBehaviour
{
    [SerializeField] List<UICard> listCard = new List<UICard>();
    public List<UICard> ListCard
    {
        get { return listCard; }
    }

}
