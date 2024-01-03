using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoorController : MonoBehaviour
{
    [SerializeField] NavMeshObstacle obstacle;
    public OnScenes onScenes;
    private void OnTriggerEnter(Collider other)
    {
        LoadSceneExtension.LoadScene(onScenes.ToString());
    }

    public void SetDoor(bool value)
    {
        obstacle.enabled = value;
    }
}
