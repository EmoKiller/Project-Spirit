using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon" , menuName = "Weapon/CreateWeapon")]
public class WeaponObject : ScriptableObject
{
    [Header("Object Reference")]
    public GameObject projectTile = null;
    public GameObject projectHitBox = null;

    [Header("Comfinguration")]
    public float attackRange = 5f;

    public WeaponObject()
    {

    }
}
