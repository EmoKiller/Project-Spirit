using DG.Tweening;
using UnityEngine;

public class ObjectSkill : MonoBehaviour, IPool
{
    public TypeEffectEnemy type;
    [SerializeField] Slash slash;
    [SerializeField] DetectedEnemy detectedEnemy;
    [SerializeField] protected BoxCollider boxCollider;
    [SerializeField] Animator _animator;
    [SerializeField] protected float speedSkill = 5;
    [SerializeField] bool isBoomer = false;
    [SerializeField] MeshRenderer _meshRender;
    public MeshRenderer ShaderMesh {  get { return _meshRender; } }
    public string objectName => type.ToString();
    protected float damage = 2;
    public Tween myTween;
    private void Awake()
    {
        if (type != TypeEffectEnemy.Slashes)
            slash.AddActionAttack(OnHit);
    }
    public void Init(float damage, float speedSkill)
    {
        this.damage = damage;
        this.speedSkill = speedSkill;
        this.DelayCall(speedSkill, () =>
        {
            Hide();
        });
    }
    public void Init(float damage, float speedSkill,Color color)
    {
        this.damage = damage;
        this.speedSkill = speedSkill;
        //_meshRender.material.color = color;
        _meshRender.material.SetColor("_Color", color);
    }
    public void Init(float damage, bool isBoom)
    {
        this.damage = damage;
        isBoomer = isBoom;
    }
    public void Init(float damage, float speedSkill, Vector3 dirStart)
    {
        this.damage = damage;
        this.speedSkill = speedSkill;
        detectedEnemy.DirStarts(dirStart);
        this.DelayCall(speedSkill, () =>
        {
            Hide();
        });
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
        myTween.Kill();
        if (gameObject.tag == "ObjEnemy")
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
    private void OnEnable()
    {
        ObseverConstants.ReloadScene.AddListener(Hide);
    }
    private void OnDisable()
    {
        ObseverConstants.ReloadScene.RemoveListener(Hide);
    }
}
