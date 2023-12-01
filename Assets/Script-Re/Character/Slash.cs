using System;
using UnityEngine;

public class Slash : MonoBehaviour
{
    private string typeSlash = "";
    public string ThisType => typeSlash;
    BoxCollider BoxCollider => gameObject.GetComponent<BoxCollider>();
    protected Action<CharacterBrain> attack;
    private void OnTriggerEnter(Collider other)
    {

        if (typeSlash != "Enemy" & IsEnemy(other))
        {
            Enemy ene = other.GetComponent<Enemy>();
            attack?.Invoke(ene);
            Debug.Log("hit Enemy");
        }
        if (typeSlash != "Player" && IsPlayer(other))
        {
            Player player = other.GetComponent<Player>();
            attack?.Invoke(player);
            Debug.Log("hit Player");
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
    }
    
    private bool IsEnemy(Collider other)
    {
        return other.gameObject.layer.Equals(3);
    }
    private bool IsPlayer(Collider other)
    {
        return other.gameObject.layer.Equals(6);
    }
    public void AddActionAttack(Action<CharacterBrain> action) 
    {
        attack += action;
    }
    public void SetType(string type)
    {
        typeSlash = type;
    }
    public void SetActiveSlash(bool set)
    {
        gameObject.SetActive(set);
    }
    public void SetSizeBox(float x, float y, float z)
    {
        BoxCollider.size = new Vector3(x, y, z);
    }

}
