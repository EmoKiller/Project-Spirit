using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        
        //UI Infomation

        EventDispatcher.Addlistener<Sprite>(Script.UIManager, Events.UpdateIconWeapon, UpdateIconWeapon);
        EventDispatcher.Addlistener<Sprite>(Script.UIManager, Events.UpdateIconCurses, UpdateIconCurses);

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
        foreach (var item in grHeart)
        {
            item.CreateNewHeart = CreateNewHeart;
            ObseverConstants.OnAttributeValueChanged.AddListener(item.SetStartMaxCurrentHP);
            ObseverConstants.OnIncreaseAttributeValue.AddListener(item.RestoreHeart);
            //item.SetStartMaxCurrentHP(item.TypeHeart, InfomationPlayerManager.Instance.GetValueAttribute(item.TypeHeart));
            InfomationPlayerManager.Instance.UpdateValueOf(item.TypeHeart, InfomationPlayerManager.Instance.GetValueAttribute(item.TypeHeart));
        }
        EventDispatcher.Addlistener<float>(Script.UIManager, Events.PlayerTakeDmg, TakeDamage);
        List<UI_Attribute> _UI_Attribute = GetComponentsInChildren<UI_Attribute>().ToList();
        foreach (var item in _UI_Attribute)
        {
            item.Init();
        }

        //UiControllerHearts
        //grHeart[(int)EnemGrPriteHeart.Red].SetStartMaxCurrentHP((int)InfomationPlayerManager.Instance.GetValueAtribute(AttributeType.MaxRedHeart));

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

    private void UpdateIconWeapon(Sprite spr)
    {
        UIInfomation.IconWeapon = spr;
    }
    private void UpdateIconCurses(Sprite spr)
    {
        UIInfomation.IconCurses = spr;
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
    private void CreateNewHeart(EnemGrPriteHeart grSprite)
    {
        EnemGrHeart grHearts = GameUtilities.ConvertGrSpriteToGrHeart(grSprite);
        UIHeart obj = ObjectPooling.Instance.PopUIpHeart(true);
        obj.SetNewTypeHeart(grSprite);
        obj.transform.SetParent(grHeart[(int)grHearts].rectGr, true);
        grHeart[(int)grHearts].Add(obj);
    }
    [Button]
    public void TakeDamage(float valueHit)
    {
        for (int i = grHeart.Count - 1; i > -1; i--)
        {
            grHeart[i].TakeDamage(ref valueHit);
            if (valueHit == 0)
            {
                break;
            }   
        }
    }
    //public void RestoreHeart(EnemGrHeart gr, float valueRestore)
    //{
    //    grHeart[(int)gr].RestoreHeart(valueRestore);
    //}

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
        SetUpDownValue(InfoWeapon.imageArrowDamage, damage);
        SetUpDownValue(InfoWeapon.imageArrowSpeed, speed);
    }
    private void UpdateInfoCurses(string nameWeapon, string quoteWeapon, string descriptionWeapon)
    {
        InfoWeapon.gameObject.SetActive(true);
        InfoWeapon.SetSizeImgRL(new Vector2(nameWeapon.Length * 25, 0));
        InfoWeapon.SetTextName(TypeInfoWeapon.NameWeapon, nameWeapon);
        InfoWeapon.SetTextName(TypeInfoWeapon.QueteWeapon, quoteWeapon);
        InfoWeapon.SetTextName(TypeInfoWeapon.Description, descriptionWeapon);
        InfoWeapon.ObjNumberDamge.gameObject.SetActive(false);
        InfoWeapon.ObjNumberSpeed.gameObject.SetActive(false);
    }
    private void SetDefault()
    {
        InfoWeapon.gameObject.SetActive(false);
        InfoWeapon.SetSizeImgRL(Vector2.zero);
        InfoWeapon.imageArrowDamage.transform.DORotate(Vector3.zero,0);
        InfoWeapon.imageArrowSpeed.transform.DORotate(Vector3.zero, 0);
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
                CardConfig Card = ConfigDataHelper.GameConfig.cardsConfig[(CardType)random];
                ShowUpTarot.ListCard[i].gameObject.SetActive(true);
                ShowUpTarot.ListCard[i].NameCard = Card.Type.ToString();
                ShowUpTarot.ListCard[i].QuoteCard = Card.quote;
                ShowUpTarot.ListCard[i].DescriptionCard = Card.description;
                ShowUpTarot.ListCard[i].CradFontSprite = ObjectPooling.Instance.SpriteAtlasTarotCard.GetSprite(Card.Type.ToString());
                ShowUpTarot.ListCard[i].OnActiveCard = () =>
                {
                    InfomationPlayerManager.Instance.IncreaseValueOf(Card.AttributeAdded, Card.valueAdded);
                    TurnOffCard();
                };
            }
            else
            {
                ShowUpTarot.ListCard[i].gameObject.SetActive(false);
            }
        }
    }
    private void TurnOffCard()
    {
        for (int i = 0; i < ShowUpTarot.ListCard.Count; i++)
        {
            ShowUpTarot.ListCard[i].gameObject.SetActive(false);
        }
    }
}
