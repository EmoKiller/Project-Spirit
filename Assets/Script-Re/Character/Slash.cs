using System;
using Unity.VisualScripting;
using UnityEngine;

public class Slash : MonoBehaviour
{
    BoxCollider BoxCollider => gameObject.GetComponent<BoxCollider>();
    protected Action<CharacterBrain> attack;
    private void OnTriggerEnter(Collider other)
    {
        if (IsObj(other) || IsObjsSlashes(other))
            return;
        attack?.Invoke(other.GetComponent<CharacterBrain>());
    }
    private bool IsObj(Collider other)
    {
        return other.gameObject.layer.Equals(9);
    }
    private bool IsObjsSlashes(Collider other)
    {
        return other.gameObject.layer.Equals(12);
    }
    public void AddActionAttack(Action<CharacterBrain> action) 
    {
        attack = action;
    }
    public void SetActiveSlash(bool set)
    {
        gameObject.SetActive(set);
    }
    public void SetSizeBox(Vector3 vector)
    {
        BoxCollider.size = vector;
    }

}
