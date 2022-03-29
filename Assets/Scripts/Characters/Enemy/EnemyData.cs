using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyData")]

public class EnemyData : ScriptableObject
{
    [Header("Урон и здоровье")]
    [SerializeField] float damage = 10; //урон
    [SerializeField] float maxHealth = 100f; //запас здоровья

    [Header("Передвижение")]
    [SerializeField] float movingSpeed = 1.5f; //скорость передвижения
    [SerializeField] float rushSpeed = 3; //скорость рывка

    [Header("Смерть")]
    [SerializeField] int bonus = 10; //бонус за убийство этого врага

    [Header("Коллизии")]
    [SerializeField] float groundCheckDistance = 1; //расстояние до земли
    [SerializeField] float wallCheckDistance = 1; //расстояние до стены

    [Header("Анимации")]
    [SerializeField] private string[] attackAnimations; //анимации атаки

    #region СВОЙСТВА
    public float Damage { get => damage; set => damage = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public int Bonus { get => bonus; set => bonus = value; }
    public float MovingSpeed { get => movingSpeed; set => movingSpeed = value; }
    public float RushSpeed { get => rushSpeed; set => rushSpeed = value; }
    public float GroundCheckDistance { get => groundCheckDistance; set => groundCheckDistance = value; }
    public float WallCheckDistance { get => wallCheckDistance; set => wallCheckDistance = value; }
    public string[] AttackAnimations { get => attackAnimations; set => attackAnimations = value; }
    #endregion
}
