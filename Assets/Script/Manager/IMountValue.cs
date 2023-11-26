using System;
using TMPro;
using UnityEngine;

public class IMountValue : MonoBehaviour
{
    public TypeAmount Type;
    [SerializeField]TMP_Text _text;
    public string Text
    {
        get
        {
            return _text.text;
        }
        set { _text.text = value; }
    }

}
