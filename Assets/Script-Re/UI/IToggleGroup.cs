using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IToggleGroup : MonoBehaviour
{
    [SerializeField] List<GameObject> listContents;
    private List<IToggle> _childs = null;
    public List<IToggle> Childs => this.TryGetMonoComponentsInChildren(ref _childs);

    [SerializeField]protected ToggleGroup group = null;
    private void Awake()
    {
        if (group == null )
            group = GetComponent<ToggleGroup>();
        foreach ( var child in Childs)
        {
            child.OnChangedEvent = OnChildrenChanged;
            child.Toggle.group = group;
        }
    }
    private void Start()
    {
        Childs[0].Toggle.isOn = true;
    }
    protected void OnChildrenChanged(IToggle Child, bool value)
    {
        listContents[(int)Child.TypeMenu].gameObject.SetActive(value);

    }
}
