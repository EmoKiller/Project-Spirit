using UnityEngine;

public class Slash : MonoBehaviour
{
    [SerializeField] private GameObject SlashObj;
    BoxCollider BoxCollider => SlashObj.GetComponent<BoxCollider>();
    private void OnTriggerEnter(Collider other)
    {
        
        if (IsEnemy(other))
        {
            
            //Enemy ene = other.GetComponent<Enemy>();
            //player.enemy.Invoke(ene);
            Debug.Log("hit Enemy");
        }
        if (IsPlayer(other))
        {
            //Player player = other.GetComponent<Player>();
            //player.enemy.Invoke(ene);
            Debug.Log("hit Player");
        }
    }
    
    public void SetActiveSlash(bool set)
    {
        SlashObj.SetActive(set);
    }
    public void SetSizeBox(float x, float y, float z)
    {
        BoxCollider.size = new Vector3(x,y,z);
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
