using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChangeLookAt : MonoBehaviour
{
    [SerializeField] Transform _transform;
    [SerializeField] Transform savetrans;
    protected void OnTriggerEnter(Collider other)
    {
        Player p = other.GetComponent<Player>();
        savetrans = p.ReturnTrans();
        EventDispatcher.Publish(ListScript.CameraFollow, Events.SetSmooth,0.8f);
        EventDispatcher.Publish(ListScript.CameraFollow, Events.UpdateTransform, _transform);
    }
    protected void OnTriggerExit(Collider other)
    {
        ReturnTransPlayer();
    }
    public void ReturnTransPlayer()
    {
        EventDispatcher.Publish(ListScript.CameraFollow, Events.UpdateTransform, savetrans);
        EventDispatcher.Publish(ListScript.CameraFollow, Events.SetSmooth, 0.3f);
    }
}
