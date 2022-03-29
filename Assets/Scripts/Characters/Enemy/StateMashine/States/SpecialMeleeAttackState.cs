using UnityEngine;

public class SpecialMeleeAttackState : MeleeAttackState
{
    public SpecialMeleeAttackState(EnemyBehavior enemyBehavior, Enemy enemy) : base(enemyBehavior, enemy) { }

    //переопределяем ближнюю атаку
    protected override void MeleeAttack()
    {
        int rand = Random.Range(0, 4);
        enemy.EnemyAnimator.Attack(rand);
    }
}
