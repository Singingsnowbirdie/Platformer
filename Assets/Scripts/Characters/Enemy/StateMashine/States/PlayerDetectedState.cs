//состояние: обнаружен игрок

public class PlayerDetectedState : State
{
    private readonly EnemyBehavior behavior; //поведение
    private readonly Enemy enemy; //персонаж
    private EnemyCollisionsController collisions;

    public PlayerDetectedState(EnemyBehavior enemyBehavior, Enemy enemy)
    {
        behavior = enemyBehavior;
        this.enemy = enemy;
        collisions = enemy.Collisions;
    }

    //при входе в состояние
    public override void Enter()
    {
        //разворачиваем лицом к игроку (если нужно)
        enemy.Rotation.Turn(enemy.GetPlayerDirection().x * -1);
    }

    public override void Update(float deltaTime)
    {
        //если игрок в радиусе атаки
        if (collisions.IsPlayerInMeleeRange())
            behavior.SwitchState(behavior.MeleeAttackState);
        //если игрок потерян
        else if (!collisions.IsPlayerInAggroRange())
            behavior.SwitchState(behavior.IdleState);
        //если дошли до края
        else if (!collisions.IsGroundDetected() || collisions.IsWallDetected())
        {
            //меняем состояние
            behavior.SwitchState(behavior.IdleState);
        }
        //если есть дальняя атака, используем ее
        else if (behavior.RangeAttackState != null)
            behavior.SwitchState(behavior.RangeAttackState);
        //иначе, сближаемся с игроком
        else
            enemy.Move(true);
    }
}


