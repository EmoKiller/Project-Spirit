using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ManagerDirectionMove : MonoBehaviour
{
    [SerializeField] private List<ChildrenDirectionMove> directionMove = new List<ChildrenDirectionMove>();
   
    public void Initialized()
    {
        directionMove = GetComponentsInChildren<ChildrenDirectionMove>().ToList();
    }
    public void SetActiveDirectionMove(DirectionMove type)
    {
        foreach (var move in directionMove)
        {
            move.gameObject.SetActive(move.direction == type);
        }
    }
    public void DirectionMove(Vector3 positon, Vector3 dirTarget,int dirNum)
    {

        if (Mathf.Abs(dirTarget.x - positon.x) >= Mathf.Abs(dirTarget.z - positon.z))
        {
            dirNum = positon.x < dirTarget.x ? 3 : 2;
        }
        else if (Mathf.Abs(dirTarget.x - positon.x) <= Mathf.Abs(dirTarget.z - positon.z))
        {
            dirNum = positon.z < dirTarget.z ? 1 : 0;
        }
            
        SetActiveDirectionMove((DirectionMove)dirNum);

    }
}
