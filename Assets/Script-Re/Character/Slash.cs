using System;
using UnityEngine;

public class Slash : MonoBehaviour
{
    BoxCollider BoxCollider => gameObject.GetComponent<BoxCollider>();
    protected Action<CharacterBrain> attack;
    private void OnTriggerEnter(Collider other)
    {
        if (IsItems(other))
            return;
        attack?.Invoke(other.GetComponent<CharacterBrain>());
    }
    private bool IsItems(Collider other)
    {
        return other.gameObject.layer.Equals(9);
    }
    public void AddActionAttack(Action<CharacterBrain> action) 
    {
        attack += action;
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
