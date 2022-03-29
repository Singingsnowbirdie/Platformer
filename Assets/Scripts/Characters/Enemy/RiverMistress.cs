//хозяйка реки

public class RiverMistress : Enemy
{

    //инициализатор стейт-машины
    protected override void InitializeBehavior()
    {
        behavior = new RiverMistressBehavior(this);
    }

    //событие после смерти
    public override void DeathEvent()
    {
        EventManager.LevelPassed();
    }

    //лечение (вызывается из аниматора)
    public void Heal() 
    {
        Health.Heal(10);
    }

    //наносит урон (дальний) (вызывается из аниматора)
    public void MakeRangeDamage()
    {
        //если в момент атаки, игрок находится в зоне поражения, сообщаем ему, что он атакован
        if (Collisions.IsPlayerInAggroRange())
        {
            player.IsAttacked(Data.Damage);
        }
    }
}
