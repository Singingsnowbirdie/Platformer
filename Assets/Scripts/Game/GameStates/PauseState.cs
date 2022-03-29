using UnityEngine;

public class PauseState : State
{
    private readonly LevelController levelController;
    private readonly UIController ui;

    public PauseState(LevelController levelController, UIController ui)
    {
        this.levelController = levelController;
        this.ui = ui;
    }

    public PauseState()
    {
    }

    public override void Enter()
    {
        ui.ShowPausePanel();
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
        ui.HidePausePanel();
    }
}

