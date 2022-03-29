public class GameOverState : State
{
    private readonly LevelController levelController; //контроллер уровня
    private readonly UIController ui; //интерфейс

    public GameOverState(LevelController levelController, UIController ui)
    {
        this.levelController = levelController;
        this.ui = ui;
    }

    public override void Enter()
    {
        ui.ShowGameOverPanel();
    }

    public override void Exit()
    {
        ui.HideGameOverPanel();
    }


}
