using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon" , menuName = "Weapon/CreateWeapon")]
public class WeaponObject : ScriptableObject
{
    [Header("Object Reference")]
    public GameObject projectTile = null;

    [Header("Comfinguration")]
    public float AttackRange = 2f;
    public float Firsthit = 1f;
    public float SecondHit = 1f;
    public float ThirdHit = 1f;
    public float FourHit = 1f;

    public WeaponObject()
    {

    }
}
