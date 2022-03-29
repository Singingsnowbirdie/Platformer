//чекпоинт

using UnityEngine;

public class CheckPointTrigger : Trigger
{
    protected override void WorkOut(Collider2D other)
    {
        EventManager.CheckpointReached(transform.position);
    }
}
