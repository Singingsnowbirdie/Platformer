public abstract class DialogueData
{
    protected int introFinalLine; //последняя реплика вступительного диалога 
    protected string[,] dialog; //диалог

    public DialogueData() { }

    //возвращает диалог
    public string[,] GetDialog()
    {
        return dialog;
    }

    //возвращает индекс последней реплики
    public int GetIntroFinalLine()
    {
        return introFinalLine;
    }
}
