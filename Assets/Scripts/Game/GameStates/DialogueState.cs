using UnityEngine;

public class DialogueState : State
{
    private readonly LevelController levelController;
    private readonly UIController ui;
    private readonly int firstReplica;
    private readonly int lastReplica;

    public DialogueState(LevelController levelController, UIController ui, int firstReplica, int lastReplica)
    {
        this.levelController = levelController;
        this.ui = ui;
        this.firstReplica = firstReplica;
        this.lastReplica = lastReplica;
    }

    public override void Enter()
    {
        //запускаем вступительный ролик
        ui.ShowDialog(firstReplica, lastReplica);
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
