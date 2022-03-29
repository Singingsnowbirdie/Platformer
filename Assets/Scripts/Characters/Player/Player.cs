using System.Collections;
using UnityEngine;

//игрок
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(PlayerCollisionsController))]

public class Player : MonoBehaviour
{
    [SerializeField] PlayerData data; //данные
    [SerializeField] Transform newPos; //место появления персонажа после анимации вскарабкивания на стену
    [SerializeField] Transform firePoint; //место появления снаряда
    [SerializeField] AnimationCurve curve; //кривая скорости

    private bool isControlBlocked; //заблокировано управление

    private float xInput; //вод по x
    private float yInput; //ввод по y

    private float platformXVelocity; //скорость (и направление движения) платформы, на которой стоит (опционально) игрок

    #region СВОЙСТВА
    public PlayerData Data { get => data; set => data = value; } //данные
    public RotationController Rotation { get; private set; } //поворот
    public PlayerCollisionsController Collisions { get; private set; }  //коллизии
    public PlayerMovementController Movement { get; private set; } //логика движения по горизонтали
    public PlayerCrouchingController Crouching { get; private set; } //логика приседаний
    public PlayerJumpController Jumps { get; private set; } //логика прыжков и поведения в воздухе
    public PlayerWallController Wall { get; private set; } //логика поведения на стене
    public PlayerDeathController Death { get; private set; } //смерть
    public PlayerCombatController Combat { get; private set; } //атака
    public PlayerHealthController Health { get; private set; } //здоровье
    public PlayerAnimatorController PlayerAnimator { get; private set; } //аниматор
    public Rigidbody2D Rigidbody { get; private set; } //физика
    #endregion

    #region БАЗОВЫЕ МЕТОДЫ
    private void OnEnable()
    {
        //подписываемся
        EventManager.OnShowIntroEvent += ShowIntro;
        EventManager.OnControlProhibitedEvent += BlockControl;
        EventManager.OnControlAllowedEvent += UnblockControl;
        EventManager.OnLoadCheckpointEvent += LoadCheckpoint;
    }
    public void Awake()
    {
        Collisions = GetComponent<PlayerCollisionsController>();
        Rigidbody = GetComponent<Rigidbody2D>();

        PlayerAnimator = new PlayerAnimatorController(this, GetComponent<Animator>());
        Rotation = new RotationController(transform);
        Movement = new PlayerMovementController(this);
        Crouching = new PlayerCrouchingController(this);
        Jumps = new PlayerJumpController(this);
        Wall = new PlayerWallController(this, newPos);
        Death = new PlayerDeathController(this);
        Combat = new PlayerCombatController(this, GetComponentInChildren<ProjectilesPool>());
        Health = new PlayerHealthController(this);
    }
    private void Update()
    {
        //считываем ввод по x
        xInput = Input.GetAxis("Horizontal");
        //считываем ввод по y
        yInput = Input.GetAxis("Vertical");

        //управляем анимациями
        PlayerAnimator.UpdateValues(Mathf.Abs(xInput), Input.GetKey(KeyCode.LeftShift), isControlBlocked);

        //если запрещено управление, не продолжаем
        if (isControlBlocked)
            return;

        //управляем прыжком
        Jumps.Jump(Input.GetButtonDown("Jump"));

        //управляем атакой
        Combat.Attack(Input.GetButtonDown("Fire1"), Input.GetButtonDown("Fire2"));
    }
    private void FixedUpdate()
    {
        if (xInput != 0)
        {
            //управляем движением
            Movement.Move(xInput, curve, Input.GetKey(KeyCode.LeftShift), platformXVelocity, isControlBlocked);
            //управляем поворотом
            Rotation.Turn(xInput);
        }

        //если запрещено управление, не продолжаем
        if (isControlBlocked)
            return;

        //управляем приседанием
        Crouching.Crouch(Input.GetButton("Crouch"));

        //управляем поведением на стене
        Wall.WallBehaviour(yInput);
    }
    private void OnDisable()
    {
        EventManager.OnShowIntroEvent -= ShowIntro;
        EventManager.OnControlAllowedEvent -= UnblockControl;
        EventManager.OnControlProhibitedEvent -= BlockControl;
        EventManager.OnLoadCheckpointEvent -= LoadCheckpoint;
    }
    #endregion

    #region ДИАЛОГИ
    //При демонстрации обычного диалога
    private void ShowDialogBehavior(int arg1, int arg2)
    {
        //блокируем управление
        BlockControl(0);
    }

    //управляет поведением игрока во время показа вступительного ролика
    private void ShowIntro(int sceneID)
    {
        //блокируем управление
        BlockControl(0);

        //если 1й уровень
        if (sceneID == 1)
            StartCoroutine(RiseСoroutine());
    }

    //запускает анимацию "пробуждения" через 1 секунду
    IEnumerator RiseСoroutine()
    {
        yield return new WaitForSeconds(1);
        GetComponent<Animator>().enabled = true;
        PlayerAnimator.Rise();
    }
    #endregion

    #region ВЫЗЫВАЮТСЯ ИЗ АНИМАТОРА
    //наносит урон (вызывается из аниматора)
    public void MakeDamage()
    {
        Combat.MakeDamage(Data.Damage);
    }

    //попытка убрать меч
    void Unsword()
    {
        //если игрок не убрал меч, перезапускаем таймер
        if (!PlayerAnimator.Unsword(xInput))
            RestartUnswordTimer();
        //иначе запоминаем, что он убрал меч
        else
            Combat.Unsword();
    }

    //помечает мертвым
    public void IsDead()
    {
        PlayerAnimator.IsDead();
    }

    //завершает игру 
    public void EndGame()
    {
        EventManager.GameOver();
    }

    //завершает карабканье на уступ
    public void FinishLedgeClimb()
    {
        Wall.FinishLedgeClimb();
    }

    //выпускает снаряд
    public void Fire()
    {
        Combat.Fire();
    }
    #endregion

    #region ПРОЧИЕ
    //фикс скорости игрока, при нахождении на движущейся горизонтально платформе
    internal void FixPlatformBehaviour(float platformXVelocity)
    {
        this.platformXVelocity = platformXVelocity;
    }

    //временно блокирует управление и возвращает его по таймеру
    internal void BlockControl(float returnIn)
    {
        //временно блокируем управление
        isControlBlocked = true;
        //если задано время разблокировки
        if (returnIn != 0)
        {
            //включаем обратно по таймеру 
            Invoke("UnblockControl", returnIn);
        }
    }

    //блокирует управление
    private void BlockControl()
    {
        //временно блокируем управление
        isControlBlocked = true;
    }

    //разблокирует управление 
    public void UnblockControl()
    {
        isControlBlocked = false;
    }

    //поворачивает игрока
    internal void TurnOtherWay()
    {
        Rotation.Flip();
    }

    //убивает игрока
    internal void Die()
    {
        Death.Die();
    }

    //запускает таймер выхода из режима боя
    internal void RestartUnswordTimer()
    {
        //отменяем предыдущий вызов
        CancelInvoke("Unsword");
        //устанавливаем таймер для нового вызова
        Invoke("Unsword", 2);
    }

    //атакован кем-то
    internal void IsAttacked(float damage)
    {
        //применяем урон
        Health.TakeDamage(damage);
    }

    //загружает чекпоинт
    private void LoadCheckpoint(Vector3 point)
    {
        transform.position = point;
        Death.Rise();
        Health.Restore();
    }
    #endregion
}
