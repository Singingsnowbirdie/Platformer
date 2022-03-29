using System;

public class PlayerHealthController : HealthController
{
    private Player player; //игрок

    public PlayerHealthController(Player player)
    {
        this.player = player;
        this.maxHealth = player.Data.MaxHealth;
        currentHealth = maxHealth;
    }

    //показать анимацию получения урона
    public override void ShowHitAnimation()
    {
        player.PlayerAnimator.Hit();
    }

    //получение урона
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        //оповещаем интерфейс
        EventManager.HealthAmountChanged(maxHealth, currentHealth);

        //проверяем, не умер ли игрок
        if (!IsAlive())
            player.Death.Die();
    }

    //лечение
    public override void Heal(int healingPoints)
    {
        base.Heal(healingPoints);

        //оповещаем интерфейс
        EventManager.HealthAmountChanged(maxHealth, currentHealth);
    }

    //после воскрешения
    internal void Restore()
    {
        //если игрок умер от того, что у него закончилось здоровье, восстанавливаем его
        //если игрок умер мгновенно (от столкновения с шипами, сосульками), здоровье не трогаем
        if (currentHealth <= 0)
            currentHealth = maxHealth;
        //оповещаем интерфейс
        EventManager.HealthAmountChanged(maxHealth, currentHealth);
    }
}
