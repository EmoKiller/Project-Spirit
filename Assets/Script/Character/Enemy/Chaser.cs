using UnityEngine;

public class Chaser : Enemy
{
    [Header("Config Chaser")]
    [SerializeField] Transform body2;
    [SerializeField] Transform body3;
    [SerializeField] Transform body4;
    float distance = 0.7f;
    protected override void Start()
    {
        base.Start();
        Init();
    }
    private void Init()
    {
        maxHealth = characterAttack.HP;
        health = maxHealth;
        healthBar.SetHealh(maxHealth);
        slash.SetSizeBox(4, 1, 4);
        SetoffSlash();
        characterAnimator.AddStepAniAtk(StartAniAtk, SetOnSlash, SetoffSlash, FinishAniAtk);
        slash.AddActionAttack(OnAttackHit);
        //deadAction = Dead;
        deadBody.SetActive(false);

        body2.transform.SetParent(null);
        body3.transform.SetParent(null);
        body4.transform.SetParent(null);
    }
    protected override void Update()
    {
        base.Update();
        MoveBodys();



    }
    private void MoveBodys()
    {
        MoveToWards(body2, transform);
        MoveToWards(body3, body2);
        MoveToWards(body4, body3);
    }
    private void MoveToWards(Transform transA,Transform transB)
    {
        //Vector3 dirs = transB.position - dir.position;
        //transA.position = dirs.normalized + transB.position;
        //transA.position = new Vector3(direction);
        if (Vector3.Distance(transA.position, transB.position) > distance)
        {
            transA.position = Vector3.MoveTowards(transA.position, transB.position, Time.deltaTime * agent.moveSpeed);
        }
    }
}
