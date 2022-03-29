using UnityEngine;

public class IdleState : State
{
    private readonly EnemyBehavior behavior; //поведение
    private float timer; //таймер до конца состояния
    private EnemyCollisionsController collisions;

    public IdleState(EnemyBehavior enemyBehavior, Enemy enemy)
    {
        behavior = enemyBehavior;
        collisions = enemy.gameObject.GetComponent<EnemyCollisionsController>();
    }

    //при входе в состояние
    public override void Enter()
    {
        timer = 2;
    }

    //обновляем
    public override void Update(float deltaTime)
    {
        //ищем игрока
        if (collisions.IsPlayerInAggroRange())
        {
            behavior.SwitchState(behavior.PlayerDetectedState);
        }

        //ведем отсчет до конца состояния
        timer -= deltaTime;

        //если время вышло, меняем состояние
        if (timer <= 0)
            behavior.SwitchState(behavior.MovingState);
    }
}
