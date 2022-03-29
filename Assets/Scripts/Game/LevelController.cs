using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] GameObject bonusSources; //родительский объект всех источников бонусов

    private int totalBonuses; //всего бонусов на этом уровне
    private int сollectedBonuses;//собрано бонусов
    private int penalties; //штрафы за смерть
    private int sceneID; //индекс сцены
    private UIController ui; //интерфейс
    private State currentState; //текущее состояние
    private Vector3 lastCheckpoint; //последний запомненный чекпоинт

    #region СОСТОЯНИЯ
    public State IntroState { get; set; }
    public State DialogueState { get; set; }
    public State ControlState { get; set; }
    public State PauseState { get; set; }
    public State GameOverState { get; set; }
    public State LevelPassedState { get; set; }
    #endregion

    #region БАЗОВЫЕ МЕТОДЫ
    private void OnEnable()
    {
        //события смены состояния
        EventManager.OnShowDialogEvent += ShowDialog;
        EventManager.OnEndDialogEvent += EndDialog;
        EventManager.OnGameOverEvent += GameOver;
        EventManager.OnLevelPassedEvent += EndLevel;
        //прочие события
        EventManager.OnCheckpointReachedEvent += NewCheckpoint;
    }
    private void Awake()
    {
        sceneID = SceneManager.GetActiveScene().buildIndex;
        ui = GetComponent<UIController>();

        InitializeStates();
    }
    private void Start()
    {
        SwitchState(IntroState);
    }
    private void Update()
    {
        currentState.Update();
    }
    private void OnDisable()
    {
        //отписываемся
        EventManager.OnShowDialogEvent -= ShowDialog;
        EventManager.OnGameOverEvent -= GameOver;
        EventManager.OnLevelPassedEvent -= EndLevel;
        EventManager.OnCheckpointReachedEvent += NewCheckpoint;
    }
    #endregion

    #region МЕТОДЫ, УПРАВЛЯЮЩИЕ СОСТОЯНИЯМИ
    //инициализирует состояния (кроме тех, которые инициализируются непосредственно перед запуском)
    private void InitializeStates()
    {
        IntroState = new IntroState(this, sceneID, ui);
        ControlState = new ControlState(this, ui);
        PauseState = new PauseState(this, ui);
        GameOverState = new GameOverState(this, ui);
    }

    //переключает состояние
    public void SwitchState(State state)
    {
        if (currentState != null)
            currentState.Exit();
        currentState = state;
        currentState.Enter();
    }

    //показывает обычный диалог
    public void ShowDialog(int firstReplica, int lastReplica)
    {
        //инициализируем состояние
        DialogueState = new DialogueState(this, ui, firstReplica, lastReplica);
        //запускаем его
        SwitchState(DialogueState);
    }

    //выходит из диалога
    private void EndDialog()
    {
        SwitchState(ControlState);
    }

    //завершает игру (проигрыш)
    private void GameOver()
    {
        penalties -= 10;
        SwitchState(GameOverState);
    }

    //возвращает игрока на чекпоинт
    internal void LoadCheckpoint()
    {
        EventManager.LoadCheckpoint(lastCheckpoint);
        SwitchState(ControlState);
    }

    //завершает уровень (победа)
    private void EndLevel()
    {
        //подсчитываем бонусы
        CountBonuses();
        //инициализируем состояние
        LevelPassedState = new LevelPassedState(ui, totalBonuses, сollectedBonuses);
        //запускаем его
        SwitchState(LevelPassedState);
    }

    #endregion

    //подсчитывает бонусы за уровень
    private void CountBonuses()
    {
        Spark[] collectables = bonusSources.GetComponentsInChildren<Spark>();
        Enemy[] enemies = bonusSources.GetComponentsInChildren<Enemy>();

        //подсчитываем очки за фонарики
        foreach (var item in collectables)
        {
            totalBonuses += item.BonusCost;
            if (item.IsCollected)
                сollectedBonuses += item.BonusCost;
        }
        //подсчитываем очки за врагов
        foreach (var item in enemies)
        {
            totalBonuses += item.BonusCost;
            if (item.IsCollected)
                сollectedBonuses += item.BonusCost;
        }

        //отнимаем очки за штрафы
        сollectedBonuses += penalties;

        //фиксим отрицательное значение
        if (сollectedBonuses < 0)
            сollectedBonuses = 0;
    }

    //запоминает новый чекпоинт
    private void NewCheckpoint(Vector3 checkpoint)
    {
        lastCheckpoint = checkpoint;
    }
}
