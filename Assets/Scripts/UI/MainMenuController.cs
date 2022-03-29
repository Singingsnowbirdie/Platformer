using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//главное меню

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject infoPanel;

    //запуск игры
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    //выход из приложения
    public void Exit()
    {
        Application.Quit();
    }

    //панель информации
    public void ShowHideInfoPanel()
    {
        infoPanel.SetActive(!infoPanel.activeSelf);
    }

}
