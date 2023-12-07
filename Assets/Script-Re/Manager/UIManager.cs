using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    UIHideBar _UIHideBar = null;
    public UIHideBar UIShowBar
    {
        get
        {
            if (_UIHideBar == null)
                _UIHideBar = GetComponentInChildren<UIHideBar>();
            return _UIHideBar;
        }
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
    private void Awake()
    {
        
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            UIShowBar.IsOnTab = !UIShowBar.IsOnTab;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //GruopMenuEsc
        }
    }
}
