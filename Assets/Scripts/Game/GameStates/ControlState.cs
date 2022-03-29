//состояние игры: игроку предоставлено управление персонажем

using UnityEngine;

public class ControlState : State
{
    private readonly LevelController levelController;
    private readonly UIController ui;

    public ControlState(LevelController levelController, UIController ui)
    {
        this.levelController = levelController;
        this.ui = ui;
    }

    public override void Enter()
    {
        //разрешаем управление
        EventManager.ControlAllowed();
    }
    public override void Update()
    {
        //если нажат Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //переключаемся в состояние паузы
            levelController.SwitchState(levelController.PauseState);
        }
    }

    public override void Exit()
    {
        //запрещаем управление
        EventManager.ControlProhibited();
    }


}
