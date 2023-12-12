using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Itext : MonoBehaviour
{
    public TypeInfoWeapon Type;
    private TMP_Text _Text = null;
    public TMP_Text text
    {
        get => this.TryGetMonoComponent(ref _Text);
    }
    public string Text
    {
        get
        {
            return text.text;
        }
        set
        {
            text.text = value;
        }
    }
}
