public class MushroomBehavior : EnemyBehavior
{
    public MushroomBehavior(Enemy enemy) : base(enemy)
    {
        MeleeAttackState = new DoubleMeleeAttackState(this, enemy);
        RangeAttackState = new RangeAttackState(this, enemy);
    }
}
