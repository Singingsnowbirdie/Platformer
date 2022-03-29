//здоровье персонажа

using UnityEngine;

public abstract class HealthController
{
    protected float maxHealth; //максимальное значение здоровья
    protected float currentHealth; //текущее значение здоровья
    protected bool isAlive = true; //персонаж жив

    // получение урона
    public virtual void TakeDamage(float damage)
    {
        //если персонаж жив
        if (isAlive)
        {
            //отнимаем урон
            currentHealth -= damage;

            //здоровье не может быть меньше 0
            if (currentHealth < 0)
                currentHealth = 0;
        }
        //показываем анимацию получения урона
        ShowHitAnimation();
    }
    
    // лечение
    public virtual void Heal(int points)
    {
        //если персонаж жив
        if (isAlive)
        {
            //лечим
            currentHealth += points;

            //здоровье не может быть больше максимального
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;
        }
    }

    public abstract void ShowHitAnimation();

    /// <summary>
    /// Проверка - жив ли персонаж
    /// </summary>
    protected bool IsAlive()
    {
        if (currentHealth == 0)
            isAlive = false;
        return isAlive;
    }
}
