using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeshAgent : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField]private float moveSpeed = 3f;

    private NavMeshAgent _agent = null;
    public NavMeshAgent agentBody => this.TryGetMonoComponent(ref _agent); 

    private NavMeshPath path = null;


    public void Initialized()
    {
        path = GetComponent<NavMeshPath>();
    }
}
