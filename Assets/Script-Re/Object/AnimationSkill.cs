using UnityEngine;

public class AnimationSkill : ObjectSkill
{
    protected override void OnHit(CharacterBrain character)
    {
        character.TakeDamage(damage);
    }
    private void OpenBoxCollider()
    {
        boxCollider.enabled = true;
    }
    private void CloseBoxCollider()
    {
        boxCollider.enabled = false;
    }
    
}
