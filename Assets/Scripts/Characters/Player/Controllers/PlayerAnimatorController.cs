using System;
using UnityEngine;

public class PlayerAnimatorController : CharacterAnimatorController
{
    private readonly Player player; //игрок

    public PlayerAnimatorController(Player player, Animator animator)
    {
        base.animator = animator;
        this.player = player;
    }

    //пробуждение
    public void Rise()
    {
        animator.SetBool("isDead", false);
        animator.Play("Rise");
    }

    //взбирание на уступ
    internal void PlayLedgeClimbAnimation()
    {
        animator.Play("LedgeClimb");
    }

    //пытаемся убрать меч
    internal bool Unsword(float xInput)
    {
        //если проигрывается анимация покоя с мечом, то меч можно убрать
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Sworded") && xInput == 0)
        {
            animator.SetTrigger("unsword");
            return true;
        }
        return false;
    }

    //обновляет состояния в аниматоре
    internal void UpdateValues(float xInput, bool isAccelerated, bool isMovementBlocked)
    {
        //приседание
        animator.SetBool("isCrouching", player.Crouching.IsCrouching());
        //ускорение
        animator.SetBool("isAccelerated", isAccelerated);
        //держит меч
        animator.SetBool("isHoldingSword", player.Combat.IsHoldingSword());
        //скорость по y
        animator.SetFloat("yVelocity", player.Rigidbody.velocity.y);
        //скорость по x
        if (!isMovementBlocked)
            animator.SetFloat("xInput", xInput);
        else
            animator.SetFloat("xInput", 0);
    }

    //проигрывается ли анимация атаки
    internal override bool IsAttackAnimationPlaying()
    {
        foreach (var item in player.Data.AttackAnimations)
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
}
