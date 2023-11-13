using UnityEngine;

public class ImpactableObjects : MonoBehaviour
{
    [SerializeField] HPObject hpObject;
    [SerializeField] Animator _animator;
    private float hp => hpObject.HP;
    private float weight => hpObject.weight;
    [SerializeField]private float currentHp = 0;

    private void Start()
    {
        currentHp = hp;
    }
    private void OnTriggerEnter(Collider other)
    {
        TakeDamage(1);
    }
    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        if (_animator != null)
            _animator.SetTrigger("HIT");
        if (currentHp <= 0)
            DestroyObject();
    }
    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
