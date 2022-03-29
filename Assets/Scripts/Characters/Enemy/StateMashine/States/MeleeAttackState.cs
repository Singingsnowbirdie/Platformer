//состояние: ближняя атака

public class MeleeAttackState : State
{
    protected readonly EnemyBehavior behavior; //поведение
    protected readonly Enemy enemy; //персонаж

    public MeleeAttackState(EnemyBehavior enemyBehavior, Enemy enemy)
    {
        behavior = enemyBehavior;
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        //если игрок вне зоны атаки
        if (!enemy.Collisions.IsPlayerInMeleeRange())
        {
            //оборачиваемся (ищем игрока)
            enemy.Rotation.Flip();
        }
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
            MeleeAttack();
        }
        //иначе, если игрок в зоне агра 
        else if (enemy.Collisions.IsPlayerInAggroRange())
        {
            //переключаемся в состояние "игрок обнаружен"
            behavior.SwitchState(behavior.PlayerDetectedState);
        }
        //иначе 
        else
        {
            //оборачиваемся и переходим в состояние покоя
            enemy.Rotation.Flip();
            behavior.SwitchState(behavior.IdleState);
        }
    }

    //ближняя атака (переопределяем для врагов, у которых больше одной ближней атаки)
    protected virtual void MeleeAttack()
    {
        //запускаем анимацию атаки 
        enemy.EnemyAnimator.Attack();
    }
}
