using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiInfomation : MonoBehaviour
{
    [SerializeField] private Image _ImgFillAngry;
    public float ImgFillAngry
    {
        get { return _ImgFillAngry.fillAmount; }
        set { _ImgFillAngry.fillAmount = value; }
    }
    [SerializeField] private Image _IconWeapon;
    public Sprite IconWeapon
    {
        get { return _IconWeapon.sprite; }
        set { _IconWeapon.sprite = value; } 
    }
    [SerializeField] private Image _IconCurses;
    public Sprite IconCurses
    {
        get { return _IconCurses.sprite; }
        set { _IconCurses.sprite = value; }
    }
    [SerializeField] private TMP_Text _Coin;
    public string Coin
    {
        get { return _Coin.text; }
        set { _Coin.text = value; }
    }
    [SerializeField] private Image _ImgFillHunger;
    public float ImgFillHunger
    {
        get { return _ImgFillHunger.fillAmount; }
        set { _ImgFillHunger.fillAmount = value; }
    }

}
