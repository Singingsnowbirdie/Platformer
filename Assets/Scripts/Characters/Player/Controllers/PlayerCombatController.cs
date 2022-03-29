//контроллер боя

using UnityEngine;

public class PlayerCombatController
{
    private readonly Player player; //игрок
    private readonly ProjectilesPool projectilesPool; //пул снарядов
    private bool isHoldingSword; //держит ли персонаж меч


    public PlayerCombatController(Player player, ProjectilesPool projectilesPool)
    {
        this.player = player;
        this.projectilesPool = projectilesPool;
    }

    //атака ближнего боя
    public void Attack(bool isAttackButtonPressed, bool isFireButtonPressed)
    {
        //если нажата левая кнопка мыши
        if (isAttackButtonPressed)
        {
            //если не проигрывается анимация другой атаки
            if (!player.PlayerAnimator.IsAttackAnimationPlaying())
            {
                //запускаем анимацию атаки
                player.PlayerAnimator.Attack();
                //запоминаем, что у персонажа меч в руке
                isHoldingSword = true;
                //запускаем таймер выхода из режима боя
                player.RestartUnswordTimer();
            }
        }
        //если нажата правая кнопка мыши 
        else if (isFireButtonPressed)
        {
            //если персонаж на земле и не стреляет
            if (player.Collisions.IsGrounded() && !player.PlayerAnimator.IsAttackAnimationPlaying())
            {
                //атакуем магией
                Cast();
            }
        }
    }

    //нанесение урона
    public void MakeDamage(float damage)
    {
        //каждому врагу в радиусе поражения сообщаем, что он атакован
        foreach (var item in player.Collisions.EnemiesInArea())
        {
            item.gameObject.GetComponent<Enemy>().IsAttacked(damage);
        }
    }

    //дальняя атака (заклинание)
    public void Cast()
    {
        //анимируем каст
        player.PlayerAnimator.Cast();
    }

    public void Fire()
    {
        Vector2 direction = new Vector2(player.Rotation.GazeDirection(), 0);
        projectilesPool.Shoot(direction);
    }

    //держит ли персонаж меч
    public bool IsHoldingSword() => isHoldingSword;

    //убираем меч
    internal void Unsword()
    {
        isHoldingSword = false;
    }
}
