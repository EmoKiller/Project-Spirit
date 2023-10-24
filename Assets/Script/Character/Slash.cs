using UnityEngine;

public class Slash : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer.Equals(3))
        {
            Enemy ene = other.gameObject.GetComponent<Enemy>();
            Player.enemy.Invoke(ene);
            Debug.Log("hit Enemy");
        }
        
    }
    
}
