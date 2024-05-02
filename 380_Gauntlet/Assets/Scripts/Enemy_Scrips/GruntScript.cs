using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntScript : EnemyParent
{

    

    protected override void Awake()
    {
        base.Awake();
        SetStats();
        SetAttackTypes();
    }

    private void SetStats()
    {
        score[0] = 25;
        score[1] = 5;
        score[2] = 10;
        score[3] = 10;
        atkSpd = 1f;
        atkDuration = .25f;
        moveSpd = 2;
        agroDist = 5f;
        atkDist = 2f;
    }

    private void SetAttackTypes()
    {
        possibleAttacks.Add(gameObject.AddComponent<MeleeAttack>());
    }

    public override void Move()
    {
        
    }

    public override void Attack(int i)
    {
        possibleAttacks[i].ExecuteAttackPattern(atkSpd, atkDuration);
    }

    protected override void CheckAttack()
    {    
            for (int i = 0; i < agros.Length; i++)
            {

                if (agros[i] != null && !isAttacking)
                {
                    if (Vector3.Distance(agros[i].transform.position, this.transform.position) < atkDist)
                        Attack(0);
                }
            }

    }

    public override void TakeDamage(int damage, AttackType mytype)
    {
        health -= damage;
        if (health <= 0) OnDeath();
    }
}
