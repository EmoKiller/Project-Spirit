using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashesSkill : ObjectSkill
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<CharacterBrain>().TakeDamage(damage);
        }
        if (other.gameObject.CompareTag("ObjEnemy"))
        {
            other.GetComponent<ObjectSkill>().Hide();
        }
    }
}
