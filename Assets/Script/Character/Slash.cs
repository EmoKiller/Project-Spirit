using UnityEngine;

public class Slash : MonoBehaviour
{

    [SerializeField] private Player player;

    public void Init(Player attacker)
    {
        player = attacker;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsEnemy(other))
        {
            Enemy ene = other.GetComponent<Enemy>();
            player.enemy.Invoke(ene);
            Debug.Log("hit Enemy");
        }

    }

    private bool IsEnemy(Collider other)
    {
        return other.gameObject.layer.Equals(3);
    }
}
