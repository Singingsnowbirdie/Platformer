using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Общее")]
    [SerializeField] private float damage = 10;
    [SerializeField] private float maxHealth = 100f;

    [Header("Коллизии")]
    [SerializeField] private float sensorRadius = 0.2f; //радиус сенсора
    [SerializeField] private float wallRayDistance = 0.1f; //расстояние до стены
    [SerializeField] private float ledgeRayOffset = 0.3f; //отступ для верхнего луча
    [SerializeField] private float attackRadius = 0.8f; //радиус атаки

    [Header("Прыжки")]
    [SerializeField] private float jumpForce = 10; //сила прыжка
    [SerializeField] private float lowJumpMultiplier = 2f; //множитель низкого прыжка
    [SerializeField] private float fallMultiplier = 2.5f; //множитель скорости падения
    [SerializeField] private float xWallForce = 10; //сила толчка от стены по x
    [SerializeField] private float yWallForce = 10; //сила толчка от стены по y

    [Header("Взаимодействие со стенами")]
    [SerializeField] private float climbSpeed = 3f; //скорость карабканья по стенам
    [SerializeField] private float slideSpeed = 2f; //скорость скольжения по стене

    [Header("Анимации атаки")]
    [SerializeField] private string[] attackAnimations; //анимации атаки

    #region СВОЙСТВА
    public float SensorRadius { get => sensorRadius; set => sensorRadius = value; }
    public float WallRayDistance { get => wallRayDistance; set => wallRayDistance = value; }
    public float LedgeRayOffset { get => ledgeRayOffset; set => ledgeRayOffset = value; }
    public float AttackRadius { get => attackRadius; set => attackRadius = value; }
    public float JumpForce { get => jumpForce; set => jumpForce = value; }
    public float LowJumpMultiplier { get => lowJumpMultiplier; set => lowJumpMultiplier = value; }
    public float FallMultiplier { get => fallMultiplier; set => fallMultiplier = value; }
    public float XWallForce { get => xWallForce; set => xWallForce = value; }
    public float YWallForce { get => yWallForce; set => yWallForce = value; }
    public float ClimbSpeed { get => ClimbSpeed1; set => ClimbSpeed1 = value; }
    public float ClimbSpeed1 { get => climbSpeed; set => ClimbSpeed1 = value; }
    public float SlideSpeed { get => slideSpeed; set => slideSpeed = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float Damage { get => damage; set => damage = value; }
    public string[] AttackAnimations { get => attackAnimations; set => attackAnimations = value; }
    #endregion
}
