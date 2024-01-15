using UnityEngine;

public class ObjectSkill : MonoBehaviour , IPool
{
    public TypeEffectEnemy type;
    [SerializeField] Slash slash;
    [SerializeField] DetectedEnemy detectedEnemy;
    [SerializeField] protected BoxCollider boxCollider;
    [SerializeField] Animator _animator;
    public string objectName => type.ToString();
    protected float damage = 2;
    protected float speedSkill = 5;
    [SerializeField] bool isBoomer = false;
    private void Awake()
    {
        slash.AddActionAttack(OnHit);
    }
    public void Init(float damage, float speedSkill)
    {
        this.damage = damage;
        this.speedSkill = speedSkill;
    }
    public void Init(float damage, bool value)
    {
        this.damage = damage;
        isBoomer = value;
    }
    public void Init(float damage, float speedSkill, Vector3 dirStart)
    {
        this.damage = damage;
        this.speedSkill = speedSkill;
        detectedEnemy.DirStarts(dirStart);
    }
    protected virtual void OnHit(CharacterBrain character)
    {
        character.TakeDamage(damage);
        Hide();
    }
    public void Show()
    {
        gameObject.SetActive(true);
        if (isBoomer == true)
            return;
        this.DelayCall(speedSkill, () =>
        {
            Hide();
        });
    }

    public void Hide()
    {
        if (gameObject.tag == "Enemy")
        {
            RewardSystem.Instance.RemoveFromListObjectSkillEnemy(this);
            ObjectPooling.Instance.PushToPoolObjectSkillEnemy(this);
        }
        else
        {
            RewardSystem.Instance.RemoveFromListObjectSkill(this);
            ObjectPooling.Instance.PushToPoolObjectSkill(this);
        }
        gameObject.SetActive(false);
    }
    public void ActiveBoom()
    {
        _animator.SetTrigger("ActiveBoom");
    }
}
