using UnityEngine;

//поведение игрока на стене

public class PlayerWallController
{

    private readonly Player player; //игрок
    private readonly PlayerData data; //данные
    private readonly PlayerAnimatorController animator; //аниматор
    private readonly Rigidbody2D rigidbody; //физика
    private readonly Transform newPos; //место появления персонажа после вскарабкивания на стену
    private readonly PlayerCollisionsController collisions; //коллизии
    private readonly float gravityDef; //множитель гравитации, по умолчанию

    public PlayerWallController(Player player, Transform newPos)
    {
        this.newPos = newPos;
        this.player = player;

        animator = player.PlayerAnimator;
        rigidbody = player.Rigidbody;
        collisions = player.Collisions;
        data = player.Data;

        gravityDef = rigidbody.gravityScale;
    }


    //поведение игрока на стене
    internal void WallBehaviour(float yInput)
    {
        //если игрок касается ногами земли
        if (collisions.IsGrounded())
        {
            //включаем гравитацию (на тот случай, если она вдруг была выключена)
            rigidbody.gravityScale = gravityDef;
            //и больше ничего не делаем
            return;
        }

        //если верхний сенсор касается земли
        if (collisions.IsUpperSensorTouchingWall())
        {
            //отключаем гравитацию
            rigidbody.gravityScale = 0;

            //если нижний сенсор касается земли (игроку есть во что упереться ногами)
            if (collisions.IsLowerSensorTouchingWall())
            {
                //если игрок добрался до уступа - взбираемся на него
                if (collisions.IsOnLedge())
                    LedgeClimb(collisions.GetLedgeOffset());
                //иначе,если нажата кнопка "вверх", то игрок ползет вверх
                else if (yInput > 0)
                    WallClimb();
                //если игрок зажимает кнопку "вниз", то персонаж съезжает по стене
                else if (yInput < 0)
                    WallSlide();
                //иначе, висим на руках
                else
                    WallGrab();
            }
        }
        else
        {
            //включаем гравитацию
            rigidbody.gravityScale = gravityDef;
        }
    }

    //зависаем на стене
    internal void WallGrab()
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
    }

    //соскальзываем по стене
    internal void WallSlide()
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, -data.SlideSpeed);
    }

    //ползем по стене вверх
    internal void WallClimb()
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, data.ClimbSpeed);
    }

    //взбираемся на уступ
    private void LedgeClimb(float ledgeOffset)
    {
        //временно блокируем управление
        player.BlockControl(0);

        //обнуляем скорость, чтобы избежать багов
        rigidbody.velocity = new Vector2(0, 0);

        //если смещение большое - корректируем положение персонажа
        if (ledgeOffset > 0.01f * 1.5f)
            //принудительно смещаем персонажа вниз, чтобы его руки в анимации "висит на стене" находились четко на краю стены
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - ledgeOffset + 0.01f, player.transform.position.z);

        //запускаем анимацию карабканья на уступ
        animator.PlayLedgeClimbAnimation();
    }

    //завершаем карабканье на уступ (запускается из анимации)
    public void FinishLedgeClimb()
    {
        //перемещаем персонажа на новое место
        player.transform.position = newPos.position;
        //возвращаем игроку управление
        player.UnblockControl();
    }
}

