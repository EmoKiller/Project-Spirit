using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static System.Net.Mime.MediaTypeNames;
using static UnityEngine.ProBuilder.AutoUnwrapSettings;

public class UIManager : MonoBehaviour
{
    public enum Script
    {
        UIManager
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
    UIHideBar _UIHideBar = null;
    public UIHideBar UIShowBar
    {
        get => this.TryGetMonoComponentInChildren(ref _UIHideBar);
    }
    /// <summary>
    ///  ButtonAction
    /// </summary>
    UIButtonAction _UIButtonAction;
    public UIButtonAction UIButtonAction
    {
        get => this.TryGetMonoComponentInChildren(ref _UIButtonAction);
    }

    private void Awake()
    {
        EventDispatcher.Addlistener<TypeShowButton, string>(Script.UIManager, Events.UIButtonOpen, UIButtonOpen);
        EventDispatcher.Addlistener(Script.UIManager, Events.UIButtonReset, ResetButton);
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //UIButtonAction.TypeButton[ty]
        }
    }
    private void UIButtonOpen(TypeShowButton type, string str)
    {
        UIButtonAction.SwitchTypeButton(type);
        UIButtonAction.gameObject.SetActive(true);
        UIButtonAction.rectButton.sizeDelta += new Vector2(100, 0);
        UIButtonAction.rectButton.sizeDelta += new Vector2((str.Length * 25), 0);
        UIButtonAction.UpdateText(str);
    }
    private void ResetButton()
    {
        //gameObject.SetActive(false);
        //UIButtonAction.rectButton.sizeDelta = new Vector2(0, 110);
        //rectShowText.sizeDelta = rectButton.sizeDelta;
        //fill.fillAmount = 0f;
        //buttonE.gameObject.SetActive(false);
        //mouseClick.gameObject.SetActive(false);
    }

}
