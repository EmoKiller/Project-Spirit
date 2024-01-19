using DG.Tweening;
using UnityEngine;

public class ImpactableObjects : MonoBehaviour , IPool
{

    public ListTypeEffects TypeMaterial;
    [SerializeField] HPObject hpObject;
    [SerializeField] Animator _animator;
    private float hp => hpObject.HP;
    private float weight => hpObject.weight;

    public string objectName => TypeMaterial.ToString();

    [SerializeField]private float currentHp = 0;
    private void Start()
    {
        currentHp = hp;
    }
    private void OnTriggerEnter(Collider other)
    {
        TakeDamage(1, other.transform);
    }
    public void TakeDamage(float damage, Transform trans)
    {
        RewardSystem.Instance.SpawnObjEffectAnimation(TypeEffectAnimation.HitObject2, transform.position + new Vector3(0,2,0));
        currentHp -= damage;
        if (_animator != null)
            _animator.SetTrigger("HIT");
        if (currentHp <= 0)
        {
            if (TypeMaterial != ListTypeEffects.None)
            {
                RewardSystem.Instance.SpawnEffectDestroyObj(TypeMaterial,transform.position, out EffectDestroyObject effectDestroy);
                if (trans.position.x > transform.position.x)
                    effectDestroy.transform.DORotate(new Vector3(0, -180, 0), 0);
                effectDestroy.transform.position = transform.position + new Vector3(0, 2, 0);
                effectDestroy.Show();
            }
            for (int i = 0; i < Random.Range(2,5); i++)
            {
                RewardSystem.Instance.DropObject(TypeItemsCanDrop.ObjDropAngry, transform.position);
            }
            Hide();
        }   
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        if (gameObject.CompareTag("EnemyDead")) 
        {
            RewardSystem.Instance.RemoveFromListImpactableObj(this);
            ObjectPooling.Instance.PushToPoolImpactableObjects(this);
        }
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
