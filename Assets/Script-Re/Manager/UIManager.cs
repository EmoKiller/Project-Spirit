using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SerializedMonoBehaviour
{
    public enum Script
    {
        UIManager
    }
    public static UIManager Instance = null;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
        //UiControllerHearts
        foreach (var item in grHeart)
        {
            item.CreateNewHeart = CreateNewHeart;
        }
        EventDispatcher.Addlistener<float>(Script.UIManager, Events.PlayerTakeDamage, TakeDamage);
        EventDispatcher.Addlistener<EnemGrHeart, int>(Script.UIManager, Events.RestoreHeart, RestoreHeart);

        //UI Infomation
        EventDispatcher.Addlistener(Script.UIManager, Events.UpdateValueAngry, UpdateValueAngry);
        EventDispatcher.Addlistener<Sprite>(Script.UIManager, Events.UpdateIconWeapon, UpdateIconWeapon);
        EventDispatcher.Addlistener<Sprite>(Script.UIManager, Events.UpdateIconCurses, UpdateIconCurses);
        EventDispatcher.Addlistener(Script.UIManager, Events.UpdateUICoin, UpdateUICoin);
        EventDispatcher.Addlistener(Script.UIManager, Events.UpdateValueHunger, UpdateValueHunger);
        //UIButtonAction
        UIButtonAction.OnButtonDown = ButtonDown;
        UIButtonAction.OnButtonUp = ButtonUp;
        UIButtonAction.OnTriggerUpdateFillValue = OnTriggerUpdateFillValue;
        
        UIButtonAction.TypeButton[TypeUIButton.ButtonE].gameObject.SetActive(false);
        UIButtonAction.TypeButton[TypeUIButton.Mouse].gameObject.SetActive(false);
        EventDispatcher.Addlistener<TypeShowButton, string>(Script.UIManager, Events.UIButtonOpen, UIButtonOpen);
        EventDispatcher.Addlistener(Script.UIManager, Events.UIButtonReset, ResetButton);
        //PopUp
        EventDispatcher.Addlistener<string, string, string, float, float>(Script.UIManager, Events.UpdateInfoWeapon, UpdateInfoWeapon);
        EventDispatcher.Addlistener<string, string, string>(Script.UIManager, Events.UpdateInfoCurses, UpdateInfoCurses);
        EventDispatcher.Addlistener(Script.UIManager, Events.SetDefault, SetDefault);
        //UIExp
        EventDispatcher.Addlistener(Script.UIManager, Events.UpdateValueExp, UpdateValueExp);
    }
    private void Start()
    {
        UIButtonAction.gameObject.SetActive(false);
        InfoWeapon.gameObject.SetActive(false);
        ShowUpTarot.gameObject.SetActive(false);
    }
    public void Init()
    {
        //UiControllerHearts
        //grHeart[(int)EnemGrPriteHeart.Red].SetStartMaxCurrentHP((int)InfomationPlayerManager.Instance.GetValueAtribute(AttributeType.MaxRedHeart));
        EventDispatcher.Register<EnemGrHeart, bool>(Script.UIManager, Events.CheckCurrentHP, CheckCurrentHP);
        
        //UIExp
    }
    /// <summary>
    /// UI Infomation
    /// </summary>
    [Header("UI Infomation")]
    
    private UiInfomation _UIInfomation = null;
    public UiInfomation UIInfomation
    {
        get
        {
            if (_UIInfomation == null)
            {
                _UIInfomation = GetComponentInChildren<UiInfomation>();
            }
            return _UIInfomation;
        }
    }
    public void UpdateValueAngry()
    {
        UIInfomation.ImgFillAngry = InfomationPlayerManager.Instance.GetValueAtribute(AttributeType.CurrentAngry);
    }
    private void UpdateIconWeapon(Sprite spr)
    {
        UIInfomation.IconWeapon = spr;
    }
    private void UpdateIconCurses(Sprite spr)
    {
        UIInfomation.IconCurses = spr;
    }
    private void UpdateUICoin()
    {
        UIInfomation.Coin = InfomationPlayerManager.Instance.GetValueAtribute(AttributeType.CurrentCoin).ToString();
    }
    public void UpdateValueHunger()
    {
        UIInfomation.ImgFillHunger = InfomationPlayerManager.Instance.GetValueAtribute(AttributeType.CurrentHunger);
    }
    /// <summary>
    /// UIExp
    /// </summary>
    [Header("Exp")]
    [SerializeField] private UIExp _UIExp = null;
    public UIExp UIExp
    {
        get => this.TryGetMonoComponentInChildren(ref _UIExp);
    }
    public void SetMaxExpOfLevel()
    {
        UIExp.MaxValue = InfomationPlayerManager.Instance.GetValueAtribute(AttributeType.MaxExpOfLevel);
    }
    public void UpdateValueExp()
    {
        UIExp.Value = InfomationPlayerManager.Instance.GetValueAtribute(AttributeType.CurrentExp);
    }

    /// <summary>
    /// UiControllerHearts
    /// </summary>
    [Header("Gr Heart")]
    [SerializeField] List<GrHeart> grHeart = new List<GrHeart>();
    public List<GrHeart> GroupHeart
    {
        get { return grHeart; }
    }
    public void UpdateHeartOfGroup(EnemGrHeart Group, AttributeType typeHeart)
    {
        GroupHeart[(int)Group].SetStartMaxCurrentHP((int)InfomationPlayerManager.Instance.GetValueAtribute(typeHeart));
    }
    private void CreateNewHeart(EnemGrPriteHeart grSprite)
    {
        EnemGrHeart grHearts = GameUtilities.ConvertGrSpriteToGrHeart(grSprite);
        UIHeart uiHeart = ObjectPooling.Instance.PopObjectFormPool(ObjectPooling.Instance.HeartObj, "UIHeart");
        uiHeart.Show();
        uiHeart.SetNewTypeHeart(grSprite);
        uiHeart.transform.SetParent(grHeart[(int)grHearts].rectGr,true);
        grHeart[(int)grHearts].Add(uiHeart);
    }
    [Button]
    private void TakeDamage(float valueHit)
    {
        for (int i = grHeart.Count - 1; i > -1; i--)
        {
            grHeart[i].TalkeDamage(ref valueHit);
            if (valueHit == 0)
                break;
        }
    }
    private void RestoreHeart(EnemGrHeart gr, int valueRestore)
    {
        grHeart[(int)gr].RestoreHeart(valueRestore);
    }
    private bool CheckCurrentHP(EnemGrHeart gr)
    {
        return grHeart[(int)gr].CheckCurrentHP();
    }

    /// <summary>
    /// UIHider {
    /// </summary>
    [Header("Hide Bar")]
    [SerializeField] UIHideBar _UIHideBar = null;
    public UIHideBar UIShowBar
    {
        get => this.TryGetMonoComponentInChildren(ref _UIHideBar);
    }
    /// <summary>
    ///  ButtonAction
    /// </summary>
    [Header("UI Button")]
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
    [Header("UI PopUp")]
    private InfoWeapon _InfoWeapon = null;
    public InfoWeapon InfoWeapon
    {
        get => this.TryGetMonoComponentInChildren(ref _InfoWeapon);
    }

    private void UpdateInfoWeapon(string nameWeapon, string quoteWeapon, string descriptionWeapon, float damage, float speed)
    {
        InfoWeapon.gameObject.SetActive(true);
        InfoWeapon.SetSizeImgRL(new Vector2(nameWeapon.Length * 25, 0));
        InfoWeapon.SetTextName(TypeInfoWeapon.NameWeapon, nameWeapon);
        InfoWeapon.SetTextName(TypeInfoWeapon.QueteWeapon, quoteWeapon);
        InfoWeapon.SetTextName(TypeInfoWeapon.Description, descriptionWeapon);
        InfoWeapon.SetTextName(TypeInfoWeapon.Damage, damage.ToString());
        InfoWeapon.SetTextName(TypeInfoWeapon.Speed, speed.ToString());
        SetUpDownValue(InfoWeapon.imageUpDownDamage, damage);
        SetUpDownValue(InfoWeapon.imageUpDownSpeed, speed);
    }
    private void UpdateInfoCurses(string nameWeapon, string quoteWeapon, string descriptionWeapon)
    {
        InfoWeapon.gameObject.SetActive(true);
        InfoWeapon.SetSizeImgRL(new Vector2(nameWeapon.Length * 25, 0));
        InfoWeapon.SetTextName(TypeInfoWeapon.NameWeapon, nameWeapon);
        InfoWeapon.SetTextName(TypeInfoWeapon.QueteWeapon, quoteWeapon);
        InfoWeapon.SetTextName(TypeInfoWeapon.Description, descriptionWeapon);
        InfoWeapon.imageUpDownDamage.gameObject.SetActive(false);
        InfoWeapon.imageUpDownSpeed.gameObject.SetActive(false);
    }
    private void SetDefault()
    {
        InfoWeapon.gameObject.SetActive(false);
        InfoWeapon.SetSizeImgRL(Vector2.zero);
        InfoWeapon.imageUpDownDamage.transform.DORotate(Vector3.zero,0);
        InfoWeapon.imageUpDownSpeed.transform.DORotate(Vector3.zero, 0);
    }
    private void SetUpDownValue(GameObject trans, float damage)
    {
        Image img = trans.GetComponent<Image>();

        if (damage > 0)
        {
            trans.transform.DORotate(new Vector3(0, 0, -90), 0);
            img.color = Color.green;
            return;
        }
        if (damage < 0)
        {
            trans.transform.DORotate(new Vector3(0, 0, 90), 0);
            img.color = Color.red;
            return;
        }
        img.color = Color.white;
    }
    [Header("ShowUp TarotCard")]
    ///ShowUpTarotCard
    [SerializeField] private ShowUpTarot _ShowUpTarotCard = null;
    public ShowUpTarot ShowUpTarot
    {
        get => this.TryGetMonoComponentInChildren(ref _ShowUpTarotCard);
    }
    [Button]
    public void ShowTarotCard(int Quanty)
    {
        TurnOffCard();
        ShowUpTarot.gameObject.SetActive(true);
        for (int i = 0; i < ShowUpTarot.ListCard.Count; i++)
        {
            if (i < Quanty)
            {
                int random = UnityEngine.Random.Range(0, 5);
                ShowUpTarot.ListCard[i].gameObject.SetActive(true);
                CardConfig Card = ConfigDataHelper.GameConfig.cardsConfig[(CardType)random];
                ShowUpTarot.ListCard[i].NameCard = Card.Type.ToString();
                ShowUpTarot.ListCard[i].QuoteCard = Card.quote;
                ShowUpTarot.ListCard[i].DescriptionCard = Card.description;
                ShowUpTarot.ListCard[i].CradFontSprite = AssetManager.Instance.spriteAtlasTarotCard.GetSprite(Card.Type.ToString());
                ShowUpTarot.ListCard[i].OnActiveCard = () =>
                {
                    InfomationPlayerManager.Instance.IncreaseAttributeOnChange(Card.AttributeAdded, Card.valueAdded);
                };
            }
            else
            {
                ShowUpTarot.ListCard[i].gameObject.SetActive(false);
            }
        }
    }
    //[Button]
    //private void SetNumberOfEnabledCard(int number)
    //{
    //    for (int i = 0; i < listCard.Count; i++)
    //    {
    //        TurnOffCard();
    //        if (i < number)
    //        {
    //            listCard[i].gameObject.SetActive(true);
    //            //lay thong tin tu RewardSystem
    //        }
    //        else
    //            listCard[i].gameObject.SetActive(false);

    //    }
    //}
    private void TurnOffCard()
    {
        for (int i = 0; i < ShowUpTarot.ListCard.Count; i++)
        {
            ShowUpTarot.ListCard[i].gameObject.SetActive(false);
        }
    }
}
