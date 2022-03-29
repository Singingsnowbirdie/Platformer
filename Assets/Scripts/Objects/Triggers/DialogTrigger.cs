using UnityEngine;

//триггер диалога

public class DialogTrigger : Trigger
{
    [SerializeField] int firstReplica; //первая реплика диалога
    [SerializeField] int lastReplica; //последняя реплика диалога

    bool isTriggered;

    protected override void WorkOut(Collider2D other)
    {
        if (!isTriggered)
        {
            isTriggered = true;
            //запускаем диалог
            EventManager.ShowDialog(firstReplica, lastReplica);
        }
    }
}
