using UnityEngine;

//анимации персонажа

public class CharacterAnimatorController
{
    protected Animator animator; //аниматор

    #region АНИМАЦИИ
    //ближняя атака
    internal void Attack()
    {
        animator.SetTrigger("attack");
    }

    //атака (переопределение)
    internal void Attack(int attackType)
    {
        if (attackType == 0)
            animator.SetTrigger("attack_0");
        else if(attackType == 1)
            animator.SetTrigger("attack_1");
        else if(attackType == 2)
            animator.SetTrigger("attack_2");
        else if(attackType == 3)
            animator.SetTrigger("heal");
    }

    //выстрел/каст
    internal void Cast()
    {
        animator.SetTrigger("cast");
    }

    //получение урона
    internal void Hit()
    {
        animator.SetTrigger("hit");
    }

    //смерть
    internal void Death()
    {
        animator.SetTrigger("death");
    }
    #endregion

    #region ПРОВЕРКИ
    //проигрывается ли анимация атаки
    internal virtual bool IsAttackAnimationPlaying()
    {
        return false;
    }

    //проигрывается ли анимация выстрела/каста
    internal bool IsShooting()
    {
        return !animator.GetCurrentAnimatorStateInfo(0).IsName("Fire");
    }
    #endregion
















}
