using UnityEngine;

public class EnemyHealthController : HealthController
{
    private Enemy enemy;

    public EnemyHealthController(Enemy enemy)
    {
        this.enemy = enemy;
        this.maxHealth = enemy.Data.MaxHealth;
        currentHealth = maxHealth;
    }

    //показывает анимацию получения урона
    public override void ShowHitAnimation()
    {
        enemy.EnemyAnimator.Hit();
    }

    //получение урона
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        //проверяем, не умер ли персонаж
        if (!IsAlive())
            enemy.Death.Die();
    }
}
