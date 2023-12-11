using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class UIManager : SerializedMonoBehaviour
{
    public enum Script
    {
        UIManager
    }
    public void Init(int baseHP)
    {
        //UiControllerHearts
        MaxHp = baseHP;
        foreach (var item in grHeart)
        {
            item.CreateNewHeart = CreateNewHeart;
        }
        EventDispatcher.Addlistener(Script.UIManager, Events.PlayerTakeDamage, TakeDamage);
        EventDispatcher.Addlistener<EnemGrPriteHeart>(Script.UIManager, Events.CreateNewHeart, AddHeart);
        //UIButtonAction
        EventDispatcher.Addlistener<TypeShowButton, string>(Script.UIManager, Events.UIButtonOpen, UIButtonOpen);
        EventDispatcher.Addlistener(Script.UIManager, Events.UIButtonReset, ResetButton);
        UIButtonAction.OnButtonDown = ButtonDown;
        UIButtonAction.OnButtonUp = ButtonUp;
        UIButtonAction.OnTriggerUpdateFillValue = OnTriggerUpdateFillValue;
        UIButtonAction.gameObject.SetActive(false);
        UIButtonAction.TypeButton[TypeUIButton.ButtonE].gameObject.SetActive(false);
        UIButtonAction.TypeButton[TypeUIButton.Mouse].gameObject.SetActive(false);
    }
    /// <summary>
    /// UiControllerHearts
    /// </summary>
    [SerializeField] int maxHp;
    public int MaxHp
    {
        get { return maxHp; }
        set
        {
            maxHp = value;
        }
    }
    [SerializeField] int Heart;
    [SerializeField] int currentHp;
    [SerializeField] List<GrHeart> grHeart = new List<GrHeart>();
    private void AddHeart(EnemGrPriteHeart grSprite)
    {
        EnemGrHeart grHearts = ConvertGrSpriteToGrHeart(grSprite);
        foreach (Transform item in grHeart[(int)grHearts].rectGr)
            Destroy(item.gameObject);
        grHeart[(int)grHearts].MaxHP += ConvertInt(grSprite);
    }
    private void CreateNewHeart(EnemGrPriteHeart grSprite)
    {
        GameObject obj = Addressables.LoadAssetAsync<GameObject>(GameConstants.UIHeart).WaitForCompletion();
        UIHeart uiHeart = obj.GetComponent<UIHeart>();
        uiHeart.SetNewTypeHeart(grSprite);
        EnemGrHeart grHearts = ConvertGrSpriteToGrHeart(grSprite);
        UIHeart obj2 = Instantiate(uiHeart, grHeart[(int)grHearts].rectGr).GetComponent<UIHeart>();
        grHeart[(int)grHearts].Add(obj2);
    }
    private int ConvertInt(EnemGrPriteHeart grSprite)
    {
        switch (grSprite)
        {
            case EnemGrPriteHeart.Red:
            case EnemGrPriteHeart.Add:
            case EnemGrPriteHeart.Blue:
            case EnemGrPriteHeart.Black:
                return 2;
            case EnemGrPriteHeart.RedHalf:
            case EnemGrPriteHeart.AddHalf:
            case EnemGrPriteHeart.BlueHalf:
                return 1;
            default:
                throw new ArgumentOutOfRangeException();
        } 
    }
    private EnemGrHeart ConvertGrSpriteToGrHeart(EnemGrPriteHeart grSprite)
    {
        switch (grSprite)
        {
            case EnemGrPriteHeart.Red:
            case EnemGrPriteHeart.RedHalf:
                return EnemGrHeart.Red;
            case EnemGrPriteHeart.Add:
            case EnemGrPriteHeart.AddHalf:
                return EnemGrHeart.Add;
            case EnemGrPriteHeart.Blue:
            case EnemGrPriteHeart.BlueHalf:
                return EnemGrHeart.Blue;
            case EnemGrPriteHeart.Black:
                return EnemGrHeart.Black;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    public void TakeDamage()
    {
        bool isTake = false;
        for (int i = grHeart.Count - 1; i > -1; i--)
        {
            grHeart[i].TalkeDamage(ref isTake);
            if (isTake)
                break;
        }
    }
    //}
    /// <summary>
    /// UIHider {
    /// </summary>
    [SerializeField] UIHideBar _UIHideBar = null;
    public UIHideBar UIShowBar
    {
        get => this.TryGetMonoComponentInChildren(ref _UIHideBar);
    }
    /// <summary>
    ///  ButtonAction
    /// </summary>
    [SerializeField] UIButtonAction _UIButtonAction;
    public UIButtonAction UIButtonAction
    {
        get => this.TryGetMonoComponentInChildren(ref _UIButtonAction);
    }

    private void UIButtonOpen(TypeShowButton type, string str)
    {
        switch (type)
        {
            case TypeShowButton.Talk:
            case TypeShowButton.TakeWeapon:
                UIButtonAction.TypeButton[TypeUIButton.ButtonE].gameObject.SetActive(true);
                break;
            case TypeShowButton.Items:
                UIButtonAction.TypeButton[TypeUIButton.Mouse].gameObject.SetActive(true);
                break;
        }
        UIButtonAction.gameObject.SetActive(true);
        UIButtonAction.rectButton.sizeDelta += new Vector2(100, 0);
        UIButtonAction.rectButton.sizeDelta += new Vector2((str.Length * 25), 0);
        UIButtonAction.UpdateText(str);
    }
    private void ResetButton()
    {
        UIButtonAction.gameObject.SetActive(false);
        UIButtonAction.rectButton.sizeDelta = new Vector2(0, 110);
        UIButtonAction.TypeButton[TypeUIButton.ButtonE].FillAmount = 0f;
        UIButtonAction.TypeButton[TypeUIButton.ButtonE].gameObject.SetActive(false);
        UIButtonAction.TypeButton[TypeUIButton.Mouse].gameObject.SetActive(false);
    }
    private void ButtonDown()
    {
        if (UIButtonAction.fillReduceAction != null)
            StopCoroutine(UIButtonAction.fillReduceAction);
        UIButtonAction.fillIncreaseAction = StartCoroutine(FillIncreaseAction(UIButtonAction.FillValue()));
    }
    private void ButtonUp()
    {
        if (UIButtonAction.fillIncreaseAction != null)
            StopCoroutine(UIButtonAction.fillIncreaseAction);
        UIButtonAction.fillReduceAction = StartCoroutine(FillReduceAction(UIButtonAction.FillValue()));
    }
    private void OnTriggerUpdateFillValue()
    {
        EventDispatcher.Publish(TriggerWaitAction.Script.TriggerWaitAction, Events.OnTringgerWaitAction);
    }
    IEnumerator FillIncreaseAction(float amount)
    {
        while (amount <= 1)
        {
            amount += 1f * Time.deltaTime;
            UIButtonAction.FillUpdate(amount);
            yield return null;
        }
    }
    IEnumerator FillReduceAction(float amount)
    {
        while (amount > 0)
        {
            amount -= 1f * Time.deltaTime;
            UIButtonAction.FillUpdate(amount);
            yield return null;
        }
    }
    /// <summary>
    /// PopUp ui
    /// </summary>
    /// 





    private UIExp uiExp = null;
    public UIExp UIExp
    {
        get => this.TryGetMonoComponentInChildren(ref uiExp);
    }


}
