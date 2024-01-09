using System;
using UnityEngine;
using UnityEngine.AI;

public class MeshAgent : MonoBehaviour
{
    public Action onArried = null;
    [Header("Configuration")]
    public float moveSpeed = 1f;

    [Header("Debuger")]
    public bool showPath = true;
    public NavMeshAgent AgentBody => this.TryGetMonoComponent(ref _agent);
    private NavMeshAgent _agent = null;
    private NavMeshPath path = null;
    public void Initialized()
    {
        path = new NavMeshPath();
    }
    public void MoveToDirection(Vector3 direction)
    {
        AgentBody.Move(direction * moveSpeed * Time.deltaTime);
    }
    public void MoveToDirections(Vector3 direction)
    {
        AgentBody.isStopped = false;
        AgentBody.SetDestination(direction);
    }

    private void OnDrawGizmos()
    {
        if (!showPath || path == null)
        {
            return;
        }
    }
}
