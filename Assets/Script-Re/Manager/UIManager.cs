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
    private void Awake()
    {
        UIButtonAction.OnButtonDown = ButtonDown;
        UIButtonAction.OnButtonUp = ButtonUp;
        UIButtonAction.OnTriggerUpdateFillValue = OnTriggerUpdateFillValue;
        UIButtonAction.gameObject.SetActive(false);
        UIButtonAction.TypeButton[TypeUIButton.ButtonE].gameObject.SetActive(false);
        UIButtonAction.TypeButton[TypeUIButton.Mouse].gameObject.SetActive(false);
    }
    public void Init(int baseHP)
    {
        //UI Infomation
        EventDispatcher.Addlistener<float>(Script.UIManager, Events.UpdateValueAngry, UpdateValueAngry);
        EventDispatcher.Addlistener<Sprite>(Script.UIManager, Events.UpdateIconWeapon, UpdateIconWeapon);
        EventDispatcher.Addlistener<Sprite>(Script.UIManager, Events.UpdateIconCurses, UpdateIconCurses);
        EventDispatcher.Addlistener<string>(Script.UIManager, Events.UpdateUICoin, UpdateUICoin);
        EventDispatcher.Addlistener<float>(Script.UIManager, Events.UpdateValueHunger, UpdateValueHunger);
        //UiControllerHearts
        MaxHp = baseHP;
        foreach (var item in grHeart)
        {
            item.CreateNewHeart = CreateNewHeart;
        }
        
        grHeart[(int)EnemGrPriteHeart.Red].SetStartMaxCurrentHP(MaxHp);
        EventDispatcher.Addlistener<int>(Script.UIManager, Events.PlayerTakeDamage, TakeDamage);
        EventDispatcher.Addlistener<EnemGrPriteHeart>(Script.UIManager, Events.AddHeartAndRestoreFull, AddHeartAndRestoreFull);
        EventDispatcher.Addlistener<EnemGrHeart,int>(Script.UIManager, Events.RestoreHeart, RestoreHeart);
        EventDispatcher.Register<EnemGrHeart, bool>(Script.UIManager, Events.CheckCurrentHP, CheckCurrentHP);
        //UIExp
        SetMaxExpOfLevel(10);
        EventDispatcher.Addlistener<float>(Script.UIManager, Events.UpdateValueExp, UpdateValueExp);
        //UIButtonAction
        EventDispatcher.Addlistener<TypeShowButton, string>(Script.UIManager, Events.UIButtonOpen, UIButtonOpen);
        EventDispatcher.Addlistener(Script.UIManager, Events.UIButtonReset, ResetButton);
        //PopUp
        InfoWeapon.gameObject.SetActive(false);
        EventDispatcher.Addlistener<string, string, string, float, float>(Script.UIManager, Events.UpdateInfoWeapon, UpdateInfoWeapon);
        EventDispatcher.Addlistener<string, string, string>(Script.UIManager, Events.UpdateInfoCurses, UpdateInfoCurses);
        EventDispatcher.Addlistener(Script.UIManager, Events.SetDefault, SetDefault);
    }
    /// <summary>
    /// UI Infomation
    /// </summary>
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
    private void UpdateValueAngry(float value)
    {
        UIInfomation.ImgFillAngry = value;
    }
    private void UpdateIconWeapon(Sprite spr)
    {
        UIInfomation.IconWeapon = spr;
    }
    private void UpdateIconCurses(Sprite spr)
    {
        UIInfomation.IconCurses = spr;
    }
    private void UpdateUICoin(string text)
    {
        UIInfomation.Coin = text;
    }
    private void UpdateValueHunger(float value)
    {
        UIInfomation.ImgFillHunger = value;
    }

    /// <summary>
    /// UiControllerHearts
    /// </summary>
    [SerializeField] private int maxHp;
    
    [SerializeField] private int heart;
    [SerializeField] private int currentHp;
    public int MaxHp
    {
        get { return maxHp; }
        set
        {
            maxHp = value;
        }
    }
    public int Heart => heart;
    public int CurrentHp => currentHp;
    [SerializeField] List<GrHeart> grHeart = new List<GrHeart>();
    private void AddHeartAndRestoreFull(EnemGrPriteHeart grSprite)
    {
        EnemGrHeart grHearts = GameUtilities.ConvertGrSpriteToGrHeart(grSprite);
        grHeart[(int)grHearts].AddHeartAndRestoreFull(GameUtilities.ConvertInt(grSprite));
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
    public void TakeDamage(int valueHit)
    {
        for (int i = grHeart.Count - 1; i > -1; i--)
        {
            grHeart[i].TalkeDamage(ref valueHit);
            if (valueHit == 0)
                break;
        }
    }
    public void RestoreHeart(EnemGrHeart gr, int valueRestore)
    {
        grHeart[(int)gr].RestoreHeart(valueRestore);
    }
    public bool CheckCurrentHP(EnemGrHeart gr)
    {
        return grHeart[(int)gr].CheckCurrentHP();
    }
    /// <summary>
    /// UIExp
    /// </summary>
    [SerializeField] private UIExp _UIExp = null;
    public UIExp UIExp
    {
        get => this.TryGetMonoComponentInChildren(ref _UIExp);
    }
    private void SetMaxExpOfLevel(float value)
    {
        UIExp.MaxValue = value;
    }
    private void UpdateValueExp(float exp)
    {
        UIExp.Value = _UIExp.Value + exp;
    }
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
    /// InfoWeapon
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
}
