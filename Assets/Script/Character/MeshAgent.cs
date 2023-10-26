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
    public NavMeshAgent agentBody => this.TryGetMonoComponent(ref _agent);
    private NavMeshAgent _agent = null;
    private NavMeshPath path = null;
    public void Initialized()
    {
        path = new NavMeshPath();
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
    }
}
