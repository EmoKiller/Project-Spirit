using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class IToggleGroup : MonoBehaviour
{
    [SerializeField] List<GameObject> listContents;
    private List<IToggle> _childs = null;
    public List<IToggle> Childs => this.TryGetMonoComponentsInChildren(ref _childs);
    [SerializeField]protected ToggleGroup group;
    private void Awake()
    {
        foreach (var child in Childs)
        {
            child.OnChangedEvent = OnChildrenChanged;
            child.Toggle.group = group;
        }
    }
    protected void OnChildrenChanged(IToggle Child, bool value)
    {
        listContents[(int)Child.TypeMenu].gameObject.SetActive(value);
    }
    private void Start()
    {
        Childs[0].Toggle.isOn = true;
    }
}
