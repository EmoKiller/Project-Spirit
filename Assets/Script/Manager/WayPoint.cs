using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class WayPoint : MonoBehaviour
{
    public string targetEnemy = "";
    public List<WayPoint> points = null;

    public WayPoint()
    {

    }
}
