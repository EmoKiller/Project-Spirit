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
        foreach (var Move in directionMove)
            Move.OnSelected = SetActiveDirectionMove;
    }
    public void SetActiveDirectionMove(DirectionMove type)
    {
        foreach (var move in directionMove)
        {
            move.gameObject.SetActive(move.direction == type);
        }
    }
}
