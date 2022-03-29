using UnityEngine;

public class Goblin : Enemy
{
    private ProjectilesPool bombsPool; //пул бомб
    [SerializeField] private bool hasDialog; //у этого гоблина есть диалог

    //инициализатор стейт-машины
    protected override void InitializeBehavior()
    {
        behavior = new GoblinBehavior(this);
    }

    //Вызывается после смерти первого гоблина
    public override void DeathEvent()
    {
        if (hasDialog)
        {
            //запускаем диалог
            EventManager.ShowDialog(3, 3);
        }
    }

    //бросает бомбу (вызывается из аниматора)
    public void ThrowBomb()
    {
        if (bombsPool == null)
            bombsPool = GetComponentInChildren<ProjectilesPool>();

        Vector2 direction = new Vector2(transform.localScale.x / 2, 0);

        bombsPool.Shoot(direction);
    }
}
