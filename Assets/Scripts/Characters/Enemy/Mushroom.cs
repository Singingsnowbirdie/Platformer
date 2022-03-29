public class Mushroom : Enemy
{
    SporesPool spores;

    //инициализатор стейт-машины
    protected override void InitializeBehavior()
    {
        behavior = new MushroomBehavior(this);
    }

    //выбрасывает споры (вызывается из аниматора)
    public void ThrowSpores()
    {
        if (spores == null)
            spores = GetComponentInChildren<SporesPool>();

        spores.Throw();
    }
}
