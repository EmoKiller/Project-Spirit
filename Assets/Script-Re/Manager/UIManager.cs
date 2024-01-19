using DG.Tweening;
using Sirenix.OdinInspector;
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

    private bool isOnUIEndOfLevel = false;
    public bool IsOnUIEndOfLevel { get { return isOnUIEndOfLevel; } }

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
        
    }
    [Button]
    public void Init()
    {
        Debug.Log("Init uimanager");
        UIButtonAction.gameObject.SetActive(false);
        InfoWeapon.gameObject.SetActive(false);
        ShowUpTarot.gameObject.SetActive(false);
        UIEndOfLevel.gameObject.SetActive(false);
        objMainSelect.gameObject.SetActive(false);
        EventDispatcher.Addlistener<float>(Script.UIManager, Events.PlayerTakeDmg, TakeDamage);
        if (InfomationPlayerManager.Instance.GetSelectDifficut())
            OnSelectButtonLevelDifficult();
        //UiControllerHearts
        foreach (var item in grHeart)
        {
            item.CreateNewHeart = CreateNewHeart;
            ObseverConstants.OnAttributeValueChanged.AddListener(item.SetStartMaxCurrentHP);
            ObseverConstants.OnRestoreHeart.AddListener(item.RestoreHeart);
            //InfomationPlayerManager.Instance.UpdateValueOf(item.TypeHeart, InfomationPlayerManager.Instance.GetValueAttribute(item.TypeHeart));
        }
        grHeart[(int)EnemGrHeart.Black].SpecialHeart = BlackHeartBreak;
        foreach (var item in _difficultButtons)
        {
            item.Value.Button.onClick.AddListener(OnSelectButtonLevelDifficult);
        }
        foreach (var item in _PowerUP.AttributePowerUPs)
        {
            item.Init();
        }
        //MainSelect
        PowerUP.Init();
        //UIShowBar.Init();
        PowerUP.ShowButton.onClick.AddListener(OnBuy);
        buttonStartGame.onClick.AddListener(OnClickButtonStart);
        UIEndOfLevel.ButtonContinue.onClick.AddListener(OnClickContinue);
        ObseverConstants.OnSpawnBoss.AddListener(OnSpawnBoss);
        ObseverConstants.OnBossDeath.AddListener(OnBossDeath);
    }
    private void OnClickButtonStart()
    {
        ObseverConstants.OnClickButtonStart?.Invoke();
        Time.timeScale = 1;
        OffMainSelect();
    }
    #region UIInfomation
    [Header("UI Infomation")]

    [SerializeField] private UiInfomation _UIInfomation = null;
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
    #endregion

    #region UiControllerHearts
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
        if (grHeart == null)
            return;
        for (int i = grHeart.Count - 1; i > -1; i--)
        {
            grHeart[i].TakeDamage(ref valueHit);
            if (valueHit == 0)
            {
                break;
            }
        }
    }
    private void BlackHeartBreak()
    {
        ObseverConstants.OnBlackHeartBreak?.Invoke(2);
    }
    #endregion

    #region UIHider
    [Header("Hide Bar")]
    [SerializeField] UIHideBar _UIHideBar = null;
    public UIHideBar UIShowBar
    {
        get => this.TryGetMonoComponentInChildren(ref _UIHideBar);
    }
    #endregion

    #region ButtonAction
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
        ButtonUp();
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
    #endregion

    #region PopUpUi
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
        InfoWeapon.imageArrowDamage.transform.DORotate(Vector3.zero, 0);
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
    #endregion

    #region ShowUpTarotCard
    [Header("ShowUp TarotCard")]
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
                int random = UnityEngine.Random.Range(0, 14);
                CardConfig Card = ConfigDataHelper.GameConfig.cardsConfig[(CardType)random];
                ShowUpTarot.ListCard[i].gameObject.SetActive(true);
                ShowUpTarot.ListCard[i].NameCard = Card.Type.ToString();
                ShowUpTarot.ListCard[i].QuoteCard = Card.quote;
                ShowUpTarot.ListCard[i].DescriptionCard = Card.description;
                ShowUpTarot.ListCard[i].CradFontSprite = ObjectPooling.Instance.SpriteAtlasTarotCard.GetSprite(Card.Type.ToString());
                ShowUpTarot.ListCard[i].OnActiveCard = () =>
                {
                    InfomationPlayerManager.Instance.TarrotIncreaseValueOf(Card.AttributeAdded, Card.valueAdded);
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
    #endregion

    #region UIEndOfLevel
    [Header("UIEndOfLevel")]
    [SerializeField] private UIEndOfLevel _UiEndOfLevel = null;
    public UIEndOfLevel UIEndOfLevel
    {
        get => this.TryGetMonoComponentInChildren(ref _UiEndOfLevel);
    }
    [Button]
    public void ShowUIEndOfLevel(Events type)
    {
        UIEndOfLevel.gameObject.SetActive(true);
        Time.timeScale = 0;
        isOnUIEndOfLevel = true;
        if (type == Events.PlayerDied)
        {
            UIEndOfLevel.TextTop = "YOU DIED";
        }
        if (type == Events.PlayerEndLevel)
        {
            UIEndOfLevel.TextTop = "VICTORY";
        }

        float time = Time.time - InfomationPlayerManager.Instance.GetElapsedTime();
        int hour = Mathf.FloorToInt(time / 3600);
        int min = Mathf.FloorToInt((time % 3600) / 60);
        int sec = Mathf.FloorToInt(time % 60);
        UIEndOfLevel.TextTimeCLock = string.Format("{0:00}:{1:00}:{2:00}", hour, min, sec);
        UIEndOfLevel.TextTotalKillEnemy = InfomationPlayerManager.Instance.GetValueAttribute(AttributeType.CountKillEnemy).ToString();
    }
    private void OnClickContinue()
    {
        ObseverConstants.OnClickButtonContinue?.Invoke();
    }
    #endregion

    #region UISelectDifficult
    [Header("UISelectDifficult")]
    [SerializeField] Dictionary<TypeLevelDifficult, IButtonLevelDifficult> _difficultButtons = new Dictionary<TypeLevelDifficult, IButtonLevelDifficult>();
    public Dictionary<TypeLevelDifficult, IButtonLevelDifficult> DifficultButtons
    {
        get { return _difficultButtons; }
    }
    [SerializeField] GameObject selectDifficult;
    public void OnSelectButtonLevelDifficult()
    {
        InfomationPlayerManager.Instance.SelectedDifficult();
        selectDifficult.gameObject.SetActive(false);
        objMainSelect.gameObject.SetActive(true);
        AudioManager.instance.PlayListShop();
    }

    #endregion

    #region MainSelect
    [SerializeField] GameObject objMainSelect;
    [SerializeField] Button buttonStartGame;
    public Button ButttonStartGame { get { return ButttonStartGame; } }
    public GameObject ObjMainSelect { get { return objMainSelect; } }
    [Header("PowerUP")]
    [SerializeField] private PowerUP _PowerUP = null;
    public PowerUP PowerUP
    {
        get => this.TryGetMonoComponentInChildren(ref _PowerUP);
    }
    private void OffMainSelect()
    {
        objMainSelect.SetActive(false);
    }
    private void OnBuy()
    {
        AttributePowerUP attri = PowerUP.AttributePowerUPs.Find(e => e.AttributeAdded.Equals(PowerUP.AttributeAdded));
        if (PowerUP.Price > InfomationPlayerManager.Instance.GetValueAttribute(AttributeType.CurrentCoin) && attri.Check.Count - 1 > attri.NumberTick)
        {
            PowerUP.PopupShow.SetActive(false);
            return;
        }
        attri.AddTick(PowerUP.SprActive);
        InfomationPlayerManager.Instance.MinusValueOf(AttributeType.CurrentCoin, PowerUP.Price);
        InfomationPlayerManager.Instance.PowerIncreaseValueOf(PowerUP.AttributeAdded, PowerUP.ValueAdded);
        PowerUP.PopupShow.SetActive(false);
    }
    #endregion
    #region HealthBoss
    [SerializeField] UIHealthBoss _HealthBoss;
    public UIHealthBoss HealthBoss => this.TryGetMonoComponent(ref _HealthBoss);
    private void OnSpawnBoss()
    {
        _HealthBoss.gameObject.SetActive(true);
    }
    private void OnBossDeath()
    {
        _HealthBoss.gameObject.SetActive(false);
    }

    #endregion
}
