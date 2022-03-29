public class MovingState : State
{
    private readonly EnemyBehavior behavior;
    private readonly Enemy enemy;
    private EnemyCollisionsController collisions;

    public MovingState(EnemyBehavior enemyBehavior, Enemy enemy)
    {
        behavior = enemyBehavior;
        this.enemy = enemy;
    }

    public override void Enter()
    {
        if (collisions == null)
            collisions = enemy.Collisions;

        //если дошли до края
        if (!collisions.IsGroundDetected() || collisions.IsWallDetected())
        {
            //разворачиваем
            enemy.Rotation.Flip();
        }
    }

    public override void Update(float deltaTime)
    {
        //если дошли до края
        if (!collisions.IsGroundDetected() || collisions.IsWallDetected())
        {
            //меняем состояние
            behavior.SwitchState(behavior.IdleState);
        }
        //если обнаружен игрок,меняем состояние
        else if (collisions.IsPlayerInAggroRange())
            behavior.SwitchState(behavior.PlayerDetectedState);
        //иначе, движемся
        else
            enemy.Move(false);
    }
}
