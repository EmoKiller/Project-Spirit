using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewChest", menuName = "GameUtilities/CreateChest")]
public class ChestObject : ScriptableObject
{
    [Header("Object Reference")]
    public GameObject projectTile = null;
    [Header("Config")]
    public GameObject Coin = null;
    public GameObject SeedFervor = null;
    public GameObject Tarot = null;
    public GameObject Necklace = null;
    public GameObject BluePrint = null;
    public GameObject CommandmentStone  = null;
    public GameObject Bone = null;
    public GameObject HeartRed = null;
    public GameObject HeartBlue = null;
    public Dictionary<GameObject,int> ItemsDrop = null;
    public ChestObject()
    {

    }

}
