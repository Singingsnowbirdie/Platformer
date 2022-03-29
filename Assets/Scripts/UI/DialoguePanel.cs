using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : MonoBehaviour
{
    private Animator animator; //аниматор
    private DialogueData[] dialogues; //диалоги
    private string[,] currentDialog; //диалог
    private readonly float delay = 0.05f; //задержка между появлением символов

    [SerializeField] TextMeshProUGUI replicaText; //поле вывода текста реплики
    [SerializeField] Image playerImage; //портрет игрока (справа)
    [SerializeField] Image otherImage; //портрет собеседника (слева)
    [SerializeField] List<Sprite> portraits; //портреты

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    //инициализатор диалогов
    private void InitializeDialogue(int sceneID)
    {
        //создаем массив и заполняем диалогами
        dialogues = new DialogueData[4];
        dialogues[0] = new DialogueData_1();
        dialogues[1] = new DialogueData_2();
        dialogues[2] = new DialogueData_3();
        dialogues[3] = new DialogueData_4();
        //текущий диалог = номер сцены минус 1
        currentDialog = dialogues[sceneID - 1].GetDialog();
    }

    //показывает вступительный диалог
    internal void ShowIntro(int sceneID)
    {
        InitializeDialogue(sceneID);
        StartCoroutine(ShowDialogСoroutine(0, dialogues[sceneID - 1].GetIntroFinalLine()));
    }

    //показывает обычный диалог
    public void ShowDialog(int firstReplica, int lastReplica)
    {
        StartCoroutine(ShowDialogСoroutine(firstReplica, lastReplica));
    }

    //прерывает диалог
    public void BreakDialog()
    {
        StopAllCoroutines();

        //прячем текст
        replicaText.text = "";

        //прячем аватарки
        playerImage.sprite = portraits[2];
        otherImage.sprite = portraits[2];

        //прячем полосы
        animator.Play("HideLines");
    }

    //показывает аватарку персонажа, подающего реплику
    private void ShowDialogImage(string image)
    {
        //показывает аватарку игрока (справа)
        if (image == "0")
        {
            playerImage.sprite = portraits[0];
            otherImage.sprite = portraits[2];
        }
        //показывает аватарку собеседника (слева)
        else if (image == "1")
        {
            playerImage.sprite = portraits[2];
            otherImage.sprite = portraits[1];
        }
        //показывает аватарку собеседника (слева)
        else if (image == "3")
        {
            playerImage.sprite = portraits[2];
            otherImage.sprite = portraits[3];
        }
        //показывает аватарку собеседника (слева)
        else if (image == "4")
        {
            playerImage.sprite = portraits[2];
            otherImage.sprite = portraits[4];
        }
    }

    //закрывает панель (вызывается из аниматора)
    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    #region КОРУТИНЫ
    //показ диалога
    public IEnumerator ShowDialogСoroutine(int firstReplica, int lastReplica)
    {
        //ждем 2 секунды (пока появятся полосы под вывод текста)
        yield return new WaitForSeconds(2f);
        //показываем текст (в диапазоне указанных реплик)
        for (int i = firstReplica; i < lastReplica + 1; i++)
        {
            yield return StartCoroutine(ShowReplica(i));
        }
        //сообщаем о завершении диалога
        EventManager.DialogEnd();
    }

    //показ реплики
    IEnumerator ShowReplica(int id)
    {
        //показываем аватарку
        ShowDialogImage(currentDialog[id, 0]);

        string text = currentDialog[id, 1];

        for (int i = 0; i < text.Length + 1; i++)
        {
            //видимая часть текста
            string visibleText = "";
            //невидимая часть текста
            string invisibleText = "";

            visibleText = text.Substring(0, i);
            invisibleText = text.Substring(i);

            string currentText = visibleText + "<color=#00000000>" + invisibleText + "</color>";

            replicaText.text = currentText;
            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForSeconds(2);
    }
    #endregion
}
