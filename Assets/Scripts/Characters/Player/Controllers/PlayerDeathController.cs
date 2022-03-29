//смерть игрока
public class PlayerDeathController
{
    private readonly Player player; //игрок
    private readonly PlayerAnimatorController animator; //аниматор
    private readonly float gravityDef; //множитель гравитации, по умолчанию
    private bool isAlive = true; //персонаж жив

    public PlayerDeathController(Player player)
    {
        this.player = player;
        animator = player.PlayerAnimator;
        gravityDef = this.player.Rigidbody.gravityScale;
    }

    //смерть персонажа
    internal void Die()
    {
        if (isAlive)
        {
            isAlive = false;
            //включает гравитацию (фикс зависания на стене при смерти)
            player.Rigidbody.gravityScale = gravityDef;
            //проигрывает анимацию смерти
            animator.Death();
        }
    }

    //оживляет персонажа
    internal void Rise()
    {
        isAlive = true;
        animator.Rise();
    }
}
