using System;
using UnityEngine;

//родительский класс для триггеров, реагирующих на игрока

public abstract class Trigger : MonoBehaviour
{
    [SerializeField] bool isEnemyTrigger; //должен ли этот триггер реагировать на врага


    //касание
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if ((isEnemyTrigger && other.GetComponent<Enemy>() != null)
            || (!isEnemyTrigger && other.GetComponent<Player>() != null))
            WorkOut(other);
    }

    //триггер сработал
    protected abstract void WorkOut(Collider2D other);
}
