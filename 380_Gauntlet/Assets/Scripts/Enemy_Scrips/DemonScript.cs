using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonScript : EnemyParent
{
    

    protected override void Awake()
    {
        base.Awake();
        SetStats();
        SetAttackTypes();
        SetMovementTypes();
    }

    private void SetStats()
    {
        score[0] = 25;
        score[1] = 5;
        score[2] = 10;
        score[3] = 10;
        atkSpd = .5f;
        atkDuration = 1f;
        moveSpd = 2;
        agroDist = 20f;
        atkDist = 2f;
    }

    private void SetAttackTypes()
    {
        possibleAttacks.Add(gameObject.AddComponent<MeleeAttack>());
        possibleAttacks.Add(gameObject.AddComponent<RangedAttack>());
    }

    private void SetMovementTypes()
    {
        possibleMovements.Add(gameObject.AddComponent<RangeMovement>());
        possibleMovements.Add(gameObject.AddComponent<MeleeMovement>());
    }

    public override void Move()
    {
        if (_numPlayersInAgro > 0)
        {
            possibleMovements[0].ExecuteMovementPattern(atkDist * 4, agroDist, agros[0].transform.position);
            possibleMovements[1].ExecuteMovementPattern(.5f, atkDist* 2.1f, agros[0].transform.position);
        }
        
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
                float dist = Vector3.Distance(agros[i].transform.position, this.transform.position);
                if (dist < atkDist)
                    Attack(0);
                else if (dist < agroDist-(atkDist) && dist > (atkDist * 2) ) Attack(1);
            }
        }
    }

    public override void TakeDamage(int damage, AttackType mytype)
    {
        health -= damage;
        if (health <= 0) OnDeath();
    }
}
