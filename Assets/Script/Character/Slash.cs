using UnityEngine;

public class Slash : MonoBehaviour
{
    private GameObject slashObj;
    //public void Init(Player attacker)
    //{
    //    player = attacker;
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (IsEnemy(other))
        {
            Enemy ene = other.GetComponent<Enemy>();
            //player.enemy.Invoke(ene);
            Debug.Log("hit Enemy");
        }
        if (IsPlayer(other))
        {
            Player player = other.GetComponent<Player>();
            //player.enemy.Invoke(ene);
            Debug.Log("hit Player");
        }
    }
    public void CreatdSlash()
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

}
