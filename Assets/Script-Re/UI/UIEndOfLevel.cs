using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEndOfLevel : MonoBehaviour
{
    [SerializeField] TMP_Text _textTop;
    public string TextTop
    {
        get { return _textTop.text; }
        set { _textTop.text = value; }
    }
    [SerializeField] TMP_Text _textNoteTop;
    public string TextNoteTop
    {
        get { return _textNoteTop.text; }
        set { _textNoteTop.text = value; }
    }
    [SerializeField] TMP_Text _textClock;
    public string TextTimeCLock
    {
        get { return _textClock.text; }
        set { _textClock.text = value; }
    }
    [SerializeField] TMP_Text _textTotalKillEnemy;
    public string TextTotalKillEnemy
    {
        get { return _textTotalKillEnemy.text; }
        set { _textTotalKillEnemy.text = value; }
    }
    [SerializeField] Button _Button;
    public Button ButtonContinue
    {
        get { return _Button; }
    }

}
