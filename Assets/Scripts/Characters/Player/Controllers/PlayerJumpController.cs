using UnityEngine;

//управляет поведением игрока в воздухе (прыжок, падение)

public class PlayerJumpController
{
    private readonly Player player; //игрок
    private readonly PlayerData data; //даннные
    private readonly Rigidbody2D rigidbody; //физика
    private readonly PlayerCollisionsController collisions; //коллизии
    private readonly RotationController rotation; //поворот

    public PlayerJumpController(Player player)
    {
        this.player = player;
        data = player.Data;
        rigidbody = player.Rigidbody;
        collisions = player.Collisions;
        rotation = player.Rotation;
    }

    //прыжок
    public void Jump(bool isJumpButtonPressed)
    {
        bool isOnWall = collisions.IsOnWall();
        bool isGrounded = collisions.IsGrounded();

        //если нажата кнопка прыжка
        if (isJumpButtonPressed)
        {
            //если игрок стоит на земле
            if (isGrounded)
            {
                //прыгаем от земли
                GroundJump();
            }
            //если игрок висит на стене
            else if (isOnWall)
            {
                //прыгаем от стены
                WallJump(rotation.GazeDirection());
            }
        }
        //если не нажата кнопка прыжка
        else
        {
            //если игрок движется вниз, не касаясь ногами земли и не соскальзывая по стене, значит он падает
            if (rigidbody.velocity.y < 0 && !isGrounded && !isOnWall)
                //в этом случае, корректируем гравитацию, чтобы он падал быстрее
                rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (data.FallMultiplier - 1) * Time.deltaTime;
            //если игрок движется вверх, не касаясь ногами земли и не карабкаясь по стене, значит, он все еще находится в прыжке
            else if (rigidbody.velocity.y > 0 && !isGrounded && !isOnWall)
            {
                //корректируем гравитацию, чтобы прыжок был ниже, чем при нажатой кнопке
                rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (data.LowJumpMultiplier - 1) * Time.deltaTime;
            }
        }
    }

    //прыжок от земли
    private void GroundJump()
    {
        //придаем ускорение вверх
        rigidbody.velocity = Vector2.up * data.JumpForce;
    }

    //прыжок от стены
    private void WallJump(float gazeDirection)
    {

        //временно блокируем управление
        player.BlockControl(0.5f);

        //отталкиваемся от стены
        Vector2 forceToAdd = new Vector2(data.XWallForce * -gazeDirection, data.YWallForce);
        rigidbody.AddForce(forceToAdd, ForceMode2D.Impulse);

        //поворачиваем игрока от стены
        player.TurnOtherWay();
    }
}
