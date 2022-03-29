public class GoblinBehavior : EnemyBehavior
{
    public GoblinBehavior(Enemy enemy) : base(enemy)
    {
        MeleeAttackState = new DoubleMeleeAttackState(this, enemy);
        RangeAttackState = new RangeAttackState(this, enemy);
    }
}
