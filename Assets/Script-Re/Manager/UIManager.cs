using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    
    [Header("HideBar")]
    [SerializeField] RectTransform WayBlack;
    [SerializeField] GameObject GruopMenuEsc;
    [SerializeField] GameObject InventoryTab;
    Dictionary<TypeFIll, IFill> listFIll = new Dictionary<TypeFIll, IFill>();
    Dictionary<TypeAmount, IMountValue> listMount = new Dictionary<TypeAmount, IMountValue>();
    bool ToggleValue = false;
    private void OnEnable()
    {
        
    }
    private void Awake()
    {
        //HideBar
        List<IFill> listfills = GetComponentsInChildren<IFill>().ToList();
        foreach (IFill fill in listfills)
        {
            listFIll.Add(fill.Type,fill);
        }
        List<IMountValue> listMounts = GetComponentsInChildren<IMountValue>().ToList();
        foreach (IMountValue mount in listMounts)
        {
            listMount.Add(mount.Type, mount);
        }
        //HideBar
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleTabHideBar();
            //InventoryTab
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //GruopMenuEsc
        }
    }
    private void ToggleTabHideBar()
    {
        ToggleValue = !ToggleValue;
        
        if (ToggleValue)
        {
            WayBlack.gameObject.SetActive(ToggleValue);
            WayBlack.DOAnchorPos(new Vector2(0,0),1);
            return;
        }
        WayBlack.DOAnchorPos(new Vector2(-1200, 0), 1).OnComplete(() =>
        {
            WayBlack.gameObject.SetActive(ToggleValue);
        });
    }
}
