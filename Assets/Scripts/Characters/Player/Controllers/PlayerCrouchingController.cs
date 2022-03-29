using UnityEngine;

//логика приседания игрока

public class PlayerCrouchingController
{
    private readonly Collider2D fullHeightCollider; //коллайдер в полный рост
    private readonly PlayerCollisionsController collisions; //коллизии
    private bool isCrouching; //персонаж приседает

    public PlayerCrouchingController(Player player)
    {
        Collider2D[] colliders = player.GetComponents<Collider2D>();
        fullHeightCollider = colliders[0];
        collisions = player.Collisions;
    }

    //приседание
    internal void Crouch(bool isCrouchButtonPressed)
    {
        //если игрок стоит на земле
        if (collisions.IsGrounded())
        {
            //если нажата кнопка приседания
            if (isCrouchButtonPressed)
            {
                isCrouching = true;
            }
            //если не нажата кнопка приседания, игрок находится в состоянии приседания и ему ничего не мешает выпрямиться
            else if (isCrouching && !collisions.IsTouchingCeiling())
            {
                isCrouching = false;
            }
        }
        //если игрок не стоит на земле
        else
        {
            isCrouching = false;
        }

        if (isCrouching)
            fullHeightCollider.enabled = false;
        else
            fullHeightCollider.enabled = true;
    }

    //находится ли персонаж в приседании
    internal bool IsCrouching()
    {
        return isCrouching;
    }
}




