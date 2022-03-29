using System;

public class RangeAttackState : State
{
    protected readonly EnemyBehavior behavior; //поведение
    protected readonly Enemy enemy; //персонаж

    public RangeAttackState(EnemyBehavior enemyBehavior, Enemy enemy)
    {
        behavior = enemyBehavior;
        this.enemy = enemy;
    }

    public override void Update(float deltaTime)
    {
        //если проигрывается анимация атаки
        if (enemy.EnemyAnimator.IsAttackAnimationPlaying())
        {
            return;
        }

        //если игрок в зоне атаки
        if (enemy.Collisions.IsPlayerInMeleeRange())
        {
            behavior.SwitchState(behavior.MeleeAttackState);
        }
        //иначе, если игрок в зоне агра 
        else if (enemy.Collisions.IsPlayerInAggroRange())
        {
            //используем дальнюю атаку
            RangeAttack();
        }
        //иначе 
        else
        {
            //оборачиваемся и переходим в состояние покоя
            enemy.Rotation.Flip();
            behavior.SwitchState(behavior.IdleState);
        }
    }

    //дальняя атака
    private void RangeAttack()
    {
        enemy.EnemyAnimator.RangeAttack();
    }
}

