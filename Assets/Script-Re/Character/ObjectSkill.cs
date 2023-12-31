using UnityEngine;

public class ObjectSkill : MonoBehaviour , IPool
{
    [SerializeField] Slash slash;
    [SerializeField] DetectedEnemy detectedEnemy;
    [SerializeField] protected BoxCollider boxCollider;
    public string objectName => gameObject.name;
    protected float damage = 2;
    protected float speedSkill = 5;
    private void Awake()
    {
        slash.AddActionAttack(OnHit);
    }
    protected virtual void Start()
    {
        this.DelayCall(speedSkill, () =>
        {
            Hide();
        });
    }
    public void Init(float damage, float speedSkill)
    {
        this.damage = damage;
        this.speedSkill = speedSkill;
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
    }

    public void Hide()
    {
        RewardSystem.Instance.RemoveFromListObjectSkill(this);
        ObjectPooling.Instance.PushToPoolObjectSkill(this);
        gameObject.SetActive(false);
    }

}
