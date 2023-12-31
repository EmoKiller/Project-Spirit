using UnityEngine;

public class AnimationSkill : ObjectSkill
{
    [SerializeField] Animator _animator;
    protected override void Start()
    {
        
    }
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
