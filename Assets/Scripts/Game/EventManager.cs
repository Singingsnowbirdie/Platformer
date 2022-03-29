using System;
using UnityEngine;

//менеджер событий

public static class EventManager
{
    #region СОСТОЯНИЯ ИГРЫ
    //показ вступительного диалога
    internal static void ShowIntro(int sceneID)
    {
        OnShowIntroEvent?.Invoke(sceneID);
    }
    public static event Action<int> OnShowIntroEvent;

    //показ обычного диалога
    internal static void ShowDialog(int firstReplica, int lastReplica)
    {
        OnShowDialogEvent?.Invoke(firstReplica, lastReplica);
    }
    public static event Action<int, int> OnShowDialogEvent;

    //диалог завершен
    internal static void DialogEnd()
    {
        OnEndDialogEvent?.Invoke();
    }
    public static event Action OnEndDialogEvent;

    //управление запрещено
    internal static void ControlProhibited()
    {
        OnControlProhibitedEvent?.Invoke();
    }
    public static event Action OnControlProhibitedEvent;

    //управление разрешено
    internal static void ControlAllowed()
    {
        OnControlAllowedEvent?.Invoke();
    }
    public static event Action OnControlAllowedEvent;

    //уровень пройден
    internal static void LevelPassed()
    {
        OnLevelPassedEvent?.Invoke();
    }
    public static event Action OnLevelPassedEvent;

    //конец игры (проигрыш)
    public static void GameOver()
    {
        OnGameOverEvent?.Invoke();
    }
    public static event Action OnGameOverEvent;

    #endregion

    #region ЧЕКПОИНТ
    //достигнут чекпоинт
    internal static void CheckpointReached(Vector3 position)
    {
        OnCheckpointReachedEvent?.Invoke(position);
    }
    public static event Action<Vector3> OnCheckpointReachedEvent;

    //загружает чекпоинт
    internal static void LoadCheckpoint(Vector3 checkpoint)
    {
        OnLoadCheckpointEvent?.Invoke(checkpoint);
    }
    public static event Action<Vector3> OnLoadCheckpointEvent;
    #endregion

    #region КЛЮЧ
    //активирует ключ
    internal static void ShowKey()
    {
        OnShowKeyEvent?.Invoke();
    }
    public static event Action OnShowKeyEvent;

    //получен ключ
    internal static void KeyReceived()
    {
        OnKeyReceivedEvent?.Invoke();
    }
    public static event Action OnKeyReceivedEvent;
    #endregion

    #region UI
    //показать HUD
    internal static void ShowHUD()
    {
        OnShowHUDEvent?.Invoke();
    }
    public static event Action OnShowHUDEvent;

    //поменялось количество здоровья
    internal static void HealthAmountChanged(float maxHealth, float currentHealth)
    {
        OnHealthAmountChangedEvent?.Invoke(maxHealth, currentHealth);
    }
    public static event Action<float, float> OnHealthAmountChangedEvent;


    #endregion

    //потрясти камеру
    internal static void ShakeCamera(float cameraShakingIntencity, float cameraShakingTime)
    {
        OnShakeCameraEvent?.Invoke(cameraShakingIntencity, cameraShakingTime);
    }
    public static event Action<float, float> OnShakeCameraEvent;
}
