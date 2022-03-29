using UnityEngine;

public class EnemyBehavior
{
    State currentState; //текущее состояние

    #region СОСТОЯНИЯ
    public State IdleState { get; set; }
    public State MovingState { get; set; }
    public State PlayerDetectedState { get; set; }
    public State MeleeAttackState { get; set; }
    public State RangeAttackState { get; set; }
    #endregion

    public EnemyBehavior(Enemy enemy)
    {
        IdleState = new IdleState(this, enemy);
        MovingState = new MovingState(this, enemy);
        PlayerDetectedState = new PlayerDetectedState(this, enemy);
        MeleeAttackState = new MeleeAttackState(this, enemy);
    }

    //переключает состояние
    public void SwitchState(State state)
    {
        currentState = state;
        if (currentState != null)
            currentState.Enter();
    }

    //в состоянии
    public void UpdateState(float deltaTime)
    {
        if (currentState != null)
            currentState.Update(deltaTime);
    }
}
