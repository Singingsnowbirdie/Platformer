using UnityEngine;

//движение игрока по горизонтали

public class PlayerMovementController
{
    private readonly Player player;

    public PlayerMovementController(Player player)
    {
        this.player = player;
    }

    //бег
    internal void Move(float direction, AnimationCurve curve, bool isAccelerationButtonPressed, float platformXVelocity, bool isMovementBlocked)
    {

        bool isGrounded = player.Collisions.IsGrounded();

        //если не нажата кнопка бега
        if (Mathf.Abs(direction) < 0.01f)
        {
            //фиксим положение игрока на движущейся платформе (если нужно)
            if (platformXVelocity != 0)
            {
                player.Rigidbody.velocity = new Vector2(platformXVelocity, 0);
            }
        }
        //если нажата кнопка бега и не запрещено двигаться
        else if (!isMovementBlocked)
        {
            //если игрок приседает, он должен двигаться медленнее
            if (player.Crouching.IsCrouching() && isGrounded)
            {
                player.Rigidbody.velocity = new Vector2(curve.Evaluate(direction) / 2 + platformXVelocity, player.Rigidbody.velocity.y);
            }
            //если игрок если игрок ускоряется, он должен двигаться быстреее
            else if (isAccelerationButtonPressed && isGrounded)
            {
                player.Rigidbody.velocity = new Vector2(curve.Evaluate(direction) * 2 + platformXVelocity, player.Rigidbody.velocity.y);
            }
            //если игрок просто бежит
            else
            {
                player.Rigidbody.velocity = new Vector2(curve.Evaluate(direction) + platformXVelocity, player.Rigidbody.velocity.y);
            }
        }
    }
}

