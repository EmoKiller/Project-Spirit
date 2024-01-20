using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class IToggleGroup : MonoBehaviour
{
    [SerializeField] List<GameObject> listContents;
    [SerializeField] private List<IToggle> _childs = null;
    public List<IToggle> Childs => this.TryGetMonoComponentsInChildren(ref _childs);
    [SerializeField]protected ToggleGroup group;
    private void Awake()
    {
        foreach (var child in _childs)
        {
            child.OnChangedEvent = OnChildrenChanged;
            child.Toggle.group = group;
        }
    }
    protected void OnChildrenChanged(IToggle Child, bool value)
    {
        listContents[(int)Child.TypeMenu].gameObject.SetActive(value);
    }
    private void Start() => _childs[0].Toggle.isOn = true;
}
