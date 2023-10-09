using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeshAgent : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField]private float moveSpeed = 1f;

    [Header("Debuger")]
    public bool showPath = true;

    private NavMeshAgent _agent = null;
    public NavMeshAgent agentBody => this.TryGetMonoComponent(ref _agent); 

    private NavMeshPath path = null;

    public Action OnArried = null; 
    public void Initialized()
    {
        path = new NavMeshPath();
        Debug.Log("Initialized");
    }

    public void MoveToDirection(Vector3 direction)
    {
        agentBody.Move(direction * moveSpeed * Time.deltaTime);
    }

    
    private void OnDrawGizmos()
    {
        if (!showPath || path == null)
        {
            return;
        }

        //Gizmos.color = Color.blue;
        //for (int i = 1; i < path.corners.Length; i++)
        //{
        //    Gizmos.DrawCube(path.corners[i - 1], Vector3.one * 0.2f);
        //    Gizmos.DrawLine(path.corners[i - 1], path.corners[i]);
        //}
    }
}
