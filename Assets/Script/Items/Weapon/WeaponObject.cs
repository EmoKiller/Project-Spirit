using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon" , menuName = "Weapon/CreateWeapon")]
public class WeaponObject : ScriptableObject
{
    [Header("Object Reference")]
    public GameObject projectTile = null;

    [Header("Comfinguration")]
    public float attackRange = 2f;
    public float damage = 5f;

    public WeaponObject()
    {

    }
}
