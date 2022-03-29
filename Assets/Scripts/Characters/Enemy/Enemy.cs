using UnityEngine;

//враг

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(EnemyCollisionsController))]
public class Enemy : MonoBehaviour
{
    [SerializeField] protected EnemyData data; //данные
    protected EnemyBehavior behavior; //поведение
    protected Player player;

    #region СВОЙСТВА
    public EnemyData Data { get => data; set => data = value; }
    public EnemyCollisionsController Collisions { get; private set; }
    public EnemyAnimatorController EnemyAnimator { get; private set; }
    public EnemyDeathController Death { get; private set; }
    public EnemyHealthController Health { get; private set; }
    public RotationController Rotation { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    internal int BonusCost => data.Bonus;
    public bool IsCollected => Death.IsDead;

    #endregion

    #region БАЗОВЫЕ МЕТОДЫ
    protected virtual void OnEnable()
    {
        EventManager.OnControlProhibitedEvent += PauseBehavior;
        EventManager.OnControlAllowedEvent += UnpauseBehavior;
    }

    protected virtual void OnDisable()
    {
        EventManager.OnControlProhibitedEvent -= PauseBehavior;
        EventManager.OnControlAllowedEvent -= UnpauseBehavior;
    }

    public void Awake()
    {
        player = FindObjectOfType<Player>();

        Collisions = GetComponent<EnemyCollisionsController>();
        Rigidbody = GetComponent<Rigidbody2D>();

        EnemyAnimator = new EnemyAnimatorController(this, GetComponent<Animator>());
        Death = new EnemyDeathController(this);
        Health = new EnemyHealthController(this);
        Rotation = new RotationController(transform);

        InitializeBehavior();
    }

    //инициализатор стейт-машины
    protected virtual void InitializeBehavior()
    {
        behavior = new EnemyBehavior(this);
    }

    private void Update()
    {
        //обновляет текущее состояние
        behavior.UpdateState(Time.deltaTime);
        //обновляет значения аниматора
        EnemyAnimator.SetVelocity(Mathf.Abs(Rigidbody.velocity.x));
    }
    #endregion

    #region СОСТОЯНИЯ
    //включает поведение
    private void UnpauseBehavior()
    {
        behavior.SwitchState(behavior.IdleState);
    }

    //отключает поведение
    private void PauseBehavior()
    {
        behavior.SwitchState(null);
    }
    #endregion

    #region ВЫЗЫВАЮТСЯ ИЗ АНИМАТОРА
    //наносит урон (ближний)
    public void MakeDamage()
    {
        //если в момент атаки, игрок находится в зоне поражения, сообщаем ему, что он атакован
        if (Collisions.IsPlayerInMeleeRange())
        {
            player.IsAttacked(Data.Damage);
        }
    }

    //помечает мертвым
    public void IsDead()
    {
        Rigidbody.bodyType = RigidbodyType2D.Static;
        EnemyAnimator.IsDead();
    }
    #endregion

    //перемещение
    public void Move(bool isRush)
    {
        //двигаем с обычной скоростью
        if (!isRush)
            Rigidbody.velocity = new Vector2(Rotation.GazeDirection() * data.MovingSpeed, Rigidbody.velocity.y);
        //двигаем со скоростью рывка
        else
            Rigidbody.velocity = new Vector2(Rotation.GazeDirection() * data.RushSpeed, Rigidbody.velocity.y);
    }

    //атакован игроком
    internal void IsAttacked(float damage)
    {
        //отталкиваем персонажа от игрока
        Rigidbody.AddForce(GetPlayerDirection(), ForceMode2D.Impulse);
        //применяем урон
        Health.TakeDamage(damage);
        //разворачиваем лицом к игроку (если нужно)
        Rotation.Turn(GetPlayerDirection().x * -1);
        //переходим в состояние атаки
        behavior.SwitchState(behavior.MeleeAttackState);
    }

    //определяем положение персонажа, относительно игрока
    public Vector2 GetPlayerDirection()
    {
        if ((transform.position.x - player.transform.position.x > 0))
            return new Vector2(1, 0);
        else
            return new Vector2(-1, 0);
    }

    //Вызывается после смерти (опционально)
    public virtual void DeathEvent() { }
}
