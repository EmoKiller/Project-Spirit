using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IButtonLevelDifficult : MonoBehaviour
{
    public TypeLevelDifficult Type;
    [SerializeField] Button _button;
    public Button Button
    {
        get { return _button; }
    }
    private void Awake()
    {
        _button.onClick.AddListener(OnSelectButton);
    }
    public void OnSelectButton()
    {
        InfomationPlayerManager.Instance.SelectDifficult(Type);
    }

}
