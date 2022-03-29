using UnityEngine;

public class EnemyAnimatorController : CharacterAnimatorController
{
    private readonly Enemy enemy; //персонаж

    public EnemyAnimatorController(Enemy enemy, Animator animator)
    {
        base.animator = animator;
        this.enemy = enemy;
    }

    //движение
    internal void SetVelocity(float xVelocity)
    {
        animator.SetFloat("xVelocity", xVelocity);
    }

    //проигрывается ли анимация атаки
    internal override bool IsAttackAnimationPlaying()
    {
        foreach (var item in enemy.Data.AttackAnimations)
        {
            //если проигрывается хотя бы одна анимация атаки
            if (animator.GetCurrentAnimatorStateInfo(0).IsName(item))
            {
                return true;
            }
        }
        return false;
    }

    //помечает мертвым
    internal void IsDead()
    {
        animator.SetBool("isDead", true);
    }

    //проигрывает анимацию дальней атаки
    internal void RangeAttack()
    {
        animator.SetTrigger("rangeAttack");
    }
}
