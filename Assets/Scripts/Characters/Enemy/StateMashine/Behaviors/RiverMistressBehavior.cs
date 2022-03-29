using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverMistressBehavior : EnemyBehavior
{
    public RiverMistressBehavior(Enemy enemy) : base(enemy)
    {
        MeleeAttackState = new SpecialMeleeAttackState(this, enemy);
        RangeAttackState = new RangeAttackState(this, enemy);
    }
}
