using System.Collections;
using UnityEngine;

//смерть вражеского персонажа

public class EnemyDeathController
{
    private readonly Enemy enemy; //персонаж

    private bool isAlive = true; //персонаж жив
    internal bool IsDead => !isAlive;

    public EnemyDeathController(Enemy enemy)
    {
        this.enemy = enemy;
    }

    internal void Die()
    {
        if (isAlive)
        {
            isAlive = false;
            //проиграть анимацию смерти
            enemy.EnemyAnimator.Death();
            //выключить объект персонажа
            enemy.StartCoroutine(DeathCoroutine());
        }
    }

    IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(2);
        enemy.gameObject.SetActive(false);
    }
}
