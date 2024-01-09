public interface IDamageAble
{
    float CurrentHealth { get; set; }
    float maxHealth { get; set; }
    void TakeDamage(float damage);
    void Dead();

}
