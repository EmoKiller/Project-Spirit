using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewScriptTalk", menuName = "GameUtilities/ScriptTalk")]

public class PopUpTalkObject : ScriptableObject
{
    [Header("Object Reference")]
    public GameObject projectTile = null;
    [Header("Comfinguration")]
    public List<string> ListText = null;
    public PopUpTalkObject()
    {
        
    }
}
