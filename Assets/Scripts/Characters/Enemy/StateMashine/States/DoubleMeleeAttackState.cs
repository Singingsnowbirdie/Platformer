using UnityEngine;

//для противников, у которых есть две анимации ближних атак

public class DoubleMeleeAttackState : MeleeAttackState
{
    public DoubleMeleeAttackState(EnemyBehavior enemyBehavior, Enemy enemy) : base(enemyBehavior, enemy) { }

    //переопределяем ближнюю атаку
    protected override void MeleeAttack()
    {
        int rand = Random.Range(0, 2);
        enemy.EnemyAnimator.Attack(rand);
    }
}
