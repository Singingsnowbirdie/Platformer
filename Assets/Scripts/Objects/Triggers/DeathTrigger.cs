using UnityEngine;

//предмет или зона, вызывающая смерть игрока (шипы, сосульки)

public class DeathTrigger : Trigger
{
    protected override void WorkOut(Collider2D other)
    {
        other.GetComponentInParent<Player>().Die();
    }
}
