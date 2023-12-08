using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;
public class UIManager : SerializedMonoBehaviour
{
    public enum Script
    {
        UIManager
    }
    [SerializeField] UiControllerHearts _UIControllerHeart = null;
    public UiControllerHearts UiControllerHearts
    {
        get => this.TryGetMonoComponentInChildren(ref _UIControllerHeart);
    }
    //Dictionary<TypeFIll, IFill> _listFill = new Dictionary<TypeFIll, IFill>();
    //Dictionary<TypeAmount, IMountValue> _listMount = new Dictionary<TypeAmount, IMountValue>();
    //public Dictionary<TypeFIll, IFill> ListFill
    //{
    //    get
    //    {
    //        if (_listFill.Count == 0)
    //        {
    //            List<IFill> listfills = GetComponentsInChildren<IFill>().ToList();
    //            foreach (IFill fill in listfills)
    //            {
    //                _listFill.Add(fill.Type, fill);
    //            }
    //        }
    //        return _listFill;
    //    }
    //}
    //public Dictionary<TypeAmount, IMountValue> ListMount
    //{
    //    get
    //    {
    //        if (_listMount.Count == 0)
    //        {
    //            List<IMountValue> listMounts = GetComponentsInChildren<IMountValue>().ToList();
    //            foreach (IMountValue mount in listMounts)
    //            {
    //                _listMount.Add(mount.Type, mount);
    //            }
    //        }
    //        return _listMount;
    //    }
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
    public void Init(int baseHP)
    {
        EventDispatcher.Addlistener<TypeShowButton, string>(Script.UIManager, Events.UIButtonOpen, UIButtonOpen);
        EventDispatcher.Addlistener(Script.UIManager, Events.UIButtonReset, ResetButton);
        UIButtonAction.OnButtonDown = ButtonDown;
        UIButtonAction.OnButtonUp = ButtonUp;
        UIButtonAction.OnTriggerUpdateFillValue = OnTriggerUpdateFillValue;
        UIButtonAction.gameObject.SetActive(false);
        UIButtonAction.TypeButton[TypeUIButton.ButtonE].gameObject.SetActive(false);
        UIButtonAction.TypeButton[TypeUIButton.Mouse].gameObject.SetActive(false);
        UiControllerHearts.Init(baseHP);
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
        Debug.Log(UIButtonAction.TypeButton[TypeUIButton.TextShow].Text);
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
}
