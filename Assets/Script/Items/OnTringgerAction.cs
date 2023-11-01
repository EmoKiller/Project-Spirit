using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class OnTringgerAction : MonoBehaviour
{
    [SerializeField]protected UnityEvent events;
    protected virtual void OnTriggerEnter(Collider other)
    {
        
    }
    protected virtual void OnTriggerExit(Collider other)
    {
    }
    protected virtual void TringgerAction()
    {

    }
}
