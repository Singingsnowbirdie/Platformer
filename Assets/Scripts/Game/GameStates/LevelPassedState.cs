public class LevelPassedState : State
{
    private readonly UIController ui;
    private readonly int totalBonuses;
    private readonly int сollectedBonuses;

    public LevelPassedState(UIController ui, int totalBonuses, int сollectedBonuses)
    {
        this.ui = ui;
        this.totalBonuses = totalBonuses;
        this.сollectedBonuses = сollectedBonuses;
    }

    public override void Enter()
    {
        ui.ShowlevelPassedPanel(totalBonuses, сollectedBonuses);
    }
}
