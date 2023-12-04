using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewScriptTalk", menuName = "GameUtilities/ScriptTalk")]

public class PopUpTalkObject : ScriptableObject
{
    [Header("Object Reference")]
    public GameObject projectTile = null;
    [Header("Comfinguration")]
    public List<string> TextList = null;
    public List<Transform> pointTalk = null;
    public PopUpTalkObject()
    {
        
    }

}
