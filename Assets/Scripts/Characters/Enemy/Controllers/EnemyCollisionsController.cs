using UnityEngine;

public class EnemyCollisionsController : MonoBehaviour
{
    [SerializeField] private Transform sensor; //сенсор 
    [SerializeField] private LayerMask ground; //земля

    private Enemy enemy;
    private RangeTrigger meleeRangeTrigger;
    private RangeTrigger aggroRangeTrigger;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();

        RangeTrigger[] triggers = GetComponentsInChildren<RangeTrigger>();

        meleeRangeTrigger = triggers[0];
        aggroRangeTrigger = triggers[1];
    }

    //обнаружена земля
    public bool IsGroundDetected()
    {
        //пускаем луч вниз и пытаемся нащупать землю
        return Physics2D.Raycast(sensor.position, Vector2.down, enemy.Data.GroundCheckDistance, ground);
    }

    //обнаружена стена
    public bool IsWallDetected()
    {
        return Physics2D.Raycast(sensor.position, new Vector2(enemy.Rotation.GazeDirection(), 0), enemy.Data.WallCheckDistance, ground);
    }

    //игрок в агро-зоне
    public bool IsPlayerInAggroRange()
    {
        return aggroRangeTrigger.IsPlayerIn;
    }

    //игрок на расстоянии атаки
    internal bool IsPlayerInMeleeRange()
    {
        return meleeRangeTrigger.IsPlayerIn;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(sensor.position, new Vector2(sensor.position.x, sensor.position.y - enemy.Data.GroundCheckDistance));

    //    Gizmos.color = Color.green;
    //    Gizmos.DrawLine(sensor.position, new Vector2(sensor.position.x + enemy.Data.WallCheckDistance * enemy.Rotation.GazeDirection(), sensor.position.y));
    //}
}
