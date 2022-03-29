using UnityEngine;

//детектор коллизий игрока

public class PlayerCollisionsController : MonoBehaviour
{
    [SerializeField] Transform groundSensor;//сенсор соприкосновения с землей
    [SerializeField] Transform headSensor;//сенсор головы
    [SerializeField] Transform wallSensorUpper;//верхний сенсор стены
    [SerializeField] Transform wallSensorLower;//нижний сенсор стены
    [SerializeField] Transform enemiesSensor; //"нащупывает" врагов

    [SerializeField] LayerMask ground;//маска слоя "земля"
    [SerializeField] LayerMask platform;//маска слоя "платформа"
    [SerializeField] LayerMask damageable;//маска слоя "повреждаемый объект"

    private Player player;//игрок
    private Animator animator; //аниматор
    private PlayerData data; //данные

    #region БАЗОВЫЕ МЕТОДЫ
    private void Awake()
    {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
        data = player.Data;
    }

    private void Update()
    {
        animator.SetBool("isGrounded", IsGrounded());
        animator.SetBool("isOnWall", IsOnWall());
        animator.SetBool("isOnLedge", IsOnLedge());

        //если игрок на стене, то будем считать, что меч он перед этим убрал
        if (IsOnWall())
        {
            player.Combat.Unsword();
        }
    }
    #endregion

    #region СТЕНЫ
    //соприкасается ли игрок со стеной верхним сенсором (пускаем луч)
    public bool IsUpperSensorTouchingWall()
    {
        return Physics2D.Raycast(wallSensorUpper.position, new Vector2(player.Rotation.GazeDirection(), 0), data.WallRayDistance, ground);
    }

    //соприкасается ли игрок со стеной нижним сенсором (рисуем сферу)
    public bool IsLowerSensorTouchingWall()
    {
        return Physics2D.OverlapCircle(wallSensorLower.position, data.SensorRadius, ground);
    }

    //на стене
    public bool IsOnWall()
    {
        return !IsGrounded() && IsUpperSensorTouchingWall();
    }

    //если нижний сенсор "нащупал" землю, а верхний-нет, значит игрок находится на уступе
    public bool IsOnLedge()
    {
        return IsOnWall()
            && !IsTouchingCeiling()
            && !(Physics2D.Raycast(new Vector2(wallSensorUpper.position.x, wallSensorUpper.position.y + data.LedgeRayOffset), new Vector2(player.Rotation.GazeDirection(), 0), data.WallRayDistance, ground));
    }
    
    //как сильно персонаж смещен вверх, относительно края стены (если он на уступе)
    public float GetLedgeOffset()
    {
        //пускаем луч вниз, из места окончания верхнего луча
        return Physics2D.Raycast(new Vector2(wallSensorUpper.position.x + data.WallRayDistance * player.Rotation.GazeDirection(), wallSensorUpper.position.y + data.LedgeRayOffset), Vector2.down, data.LedgeRayOffset, ground).distance;
    }
    #endregion

    //касается ли персонаж земли ногами (или платформы)
    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundSensor.position, data.SensorRadius, ground)
            || Physics2D.OverlapCircle(groundSensor.position, data.SensorRadius, platform);
    }

    //касается ли голова игрока потолка
    internal bool IsTouchingCeiling()
    {
        return Physics2D.OverlapCircle(headSensor.position, data.SensorRadius, ground);
    }

    //враги в зоне поражения
    public Collider2D[] EnemiesInArea()
    {
        //находим все повреждаемые объекты в радиусе атаки
        return Physics2D.OverlapCircleAll(enemiesSensor.position, data.AttackRadius, damageable);
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawLine(wallSensorUpper.position, new Vector2(wallSensorUpper.position.x + data.WallRayDistance * player.Rotation.GazeDirection(), wallSensorUpper.position.y));

    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(new Vector2(wallSensorUpper.position.x, wallSensorUpper.position.y + data.LedgeRayOffset),
    //        new Vector2(wallSensorUpper.position.x + data.WallRayDistance * player.Rotation.GazeDirection(), wallSensorUpper.position.y + data.LedgeRayOffset));

    //    Gizmos.color = Color.green;
    //    Gizmos.DrawLine(new Vector2(wallSensorUpper.position.x + data.WallRayDistance * player.Rotation.GazeDirection(), wallSensorUpper.position.y + data.LedgeRayOffset),
    //        new Vector2(wallSensorUpper.position.x + data.WallRayDistance * player.Rotation.GazeDirection(), wallSensorUpper.position.y));

    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(enemiesSensor.position, player.Data.AttackRadius);
    //}
}
