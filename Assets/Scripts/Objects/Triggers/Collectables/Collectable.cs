using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//предмет для сбора

public abstract class Collectable : Trigger
{
    protected Animator animator; //аниматор
    protected bool isCollected; //собран 

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    protected override void WorkOut(Collider2D other)
    {
        if (!isCollected)
        {
            Collect(other);
        }
    }

    protected virtual void Collect(Collider2D other)
    {
        //объект собран
        isCollected = true;
        //запускаем анимацию исчезновения
        animator.SetTrigger("Collected");
    }
}
