using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    DialoguePanel dialoguePanel; //панель диалога (скрипт)
    LevelController levelController; //контроллер уровня

    [SerializeField] GameObject HUDPanel; //Информационная панель
    [SerializeField] GameObject dialoguePanelGO; //Панель диалога (объект)
    [SerializeField] GameObject pausePanel; //Панель паузы
    [SerializeField] GameObject gameOverPanel; //Панель завершения игры
    [SerializeField] GameObject levelPassedPanel; //Панель пройденного уровня

    [SerializeField] TextMeshProUGUI countText; //Счет 


    #region БАЗОВЫЕ МЕТОДЫ
    private void Awake()
    {
        dialoguePanel = dialoguePanelGO.GetComponent<DialoguePanel>();
        levelController = GetComponent<LevelController>();
    }
    private void OnEnable()
    {
        //подписываемся
        EventManager.OnShowHUDEvent += ShowHUD;
    }
    private void OnDisable()
    {
        EventManager.OnShowHUDEvent -= ShowHUD;
    }
    #endregion

    #region ДИАЛОГИ
    //показывает вступительный диалог
    public void ShowDialog(int sceneID)
    {
        if (dialoguePanelGO != null)
        {
            dialoguePanelGO.SetActive(true);
            dialoguePanel.ShowIntro(sceneID);
        }
    }

    //показывает обычный диалог
    public void ShowDialog(int firstReplica, int lastReplica)
    {
        dialoguePanelGO.SetActive(true);
        dialoguePanel.ShowDialog(firstReplica, lastReplica);
    }

    //прерывает диалог
    public void BreakDialog()
    {
        dialoguePanel.BreakDialog();
    }
    #endregion

    #region УПРАВЛЕНИЕ ПАНЕЛЯМИ
    //показать панель паузы
    internal void ShowPausePanel()
    {
        pausePanel.SetActive(true);
    }

    //скрыть панель паузы
    internal void HidePausePanel()
    {
        pausePanel.SetActive(false);
    }

    //показывает инфопанель
    public void ShowHUD()
    {
        HUDPanel.SetActive(true);
    }

    //показать панель проигрыша
    internal void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    //скрыть панель проигрыша
    internal void HideGameOverPanel()
    {
        gameOverPanel.SetActive(false);
    }

    //показать панель победы
    internal void ShowlevelPassedPanel(int totalBonuses, int сollectedBonuses)
    {
        levelPassedPanel.SetActive(true);
        countText.text = $"Count: {сollectedBonuses}/{totalBonuses}";
    }
    #endregion

    #region КНОПКИ
    //завершение паузы
    public void EndPause()
    {
        levelController.SwitchState(levelController.ControlState);
    }

    //перезапуск уровня
    public void Restart()
    {
        int sceneID = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneID);
    }

    //выход в меню
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    //загрузка нового уровня
    public void LoadNextLevel()
    {
        int sceneID = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneID + 1);
    }

    //загрузка чекпоинта
    public void LoadCheckpoint()
    {
        if (levelController == null)
            Debug.Log("No controller");
        levelController.LoadCheckpoint();
    }
    #endregion
}
