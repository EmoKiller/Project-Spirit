using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoWeapon : SerializedMonoBehaviour
{
    [SerializeField] RectTransform imageRL;
    public readonly GameObject imageArrowDamage;
    public readonly GameObject imageArrowSpeed;
    public readonly GameObject ObjNumberDamge;
    public readonly GameObject ObjNumberSpeed;

    [SerializeField]private Dictionary<TypeInfoWeapon,Itext> _ListText = new Dictionary<TypeInfoWeapon,Itext>();
    public Dictionary<TypeInfoWeapon, Itext> ListText
    {
        get
        {
            return _ListText;
        }
    }
    private void Awake()
    {
        if(_ListText.Count == 0)
        {
            List<Itext> list = GetComponentsInChildren<Itext>().ToList();
            foreach (var item in list)
            {
                _ListText.Add(item.Type, item);
            }
        }
    }
    public void SetSizeImgRL(Vector2 value)
    {
        imageRL.sizeDelta = value;
    }
    public void SetTextName(TypeInfoWeapon type, string str)
    {
        ListText[type].Text = str;
    }
    public void TurnOffObj(TypeInfoWeapon type, bool value)
    {
        ListText[type].gameObject.SetActive(value);
    }
    
    
}
