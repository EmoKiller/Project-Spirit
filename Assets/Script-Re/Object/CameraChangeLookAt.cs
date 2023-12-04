using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChangeLookAt : MonoBehaviour
{
    [SerializeField] Transform _transform;
    protected void OnTriggerEnter(Collider other)
    {
        EventDispatcher.Publish(CameraFollow.Script.CameraFollow, Events.CameraChangeTarget, _transform);
    }
    protected void OnTriggerExit(Collider other)
    {
        EventDispatcher.Publish(CameraFollow.Script.CameraFollow, Events.CameraTargetPlayer);
    }
    public void Removed()
    {
        gameObject.SetActive(false);
    }
}
