using UnityEngine;

//состояние игры: демонстрируется вступительный ролик

public class IntroState : State
{
    private readonly LevelController levelController;
    private readonly int sceneID; //индекс сцены
    private readonly UIController ui;

    public IntroState(LevelController levelController, int sceneID, UIController ui)
    {
        this.levelController = levelController;
        this.sceneID = sceneID;
        this.ui = ui;
    }

    public override void Enter()
    {
        //запускаем вступительный ролик
        ui.ShowDialog(sceneID);
        //оповещаем подписки (игрока)
        EventManager.ShowIntro(sceneID);
    }
    public override void Update()
    {
        //если нажат Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //выходим в обычное состояние
            levelController.SwitchState(levelController.ControlState);
        }
    }

    public override void Exit()
    {
        //завершаем диалог
        ui.BreakDialog();
    }
}
