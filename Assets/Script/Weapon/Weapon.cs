using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponObject weaponObject = null;
    public Transform spanPoint = null;
    public virtual void Attack(Vector3 target)
    {

    }
}
