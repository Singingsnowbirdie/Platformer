using UnityEngine;

//пул спор (для гриба)

public class SporesPool : ProjectilesPool
{
   private Vector2 direction;

    internal void Throw()
    {
        //бросаем спору вправо
        direction = new Vector2(-1, 0);
        Shoot(direction);

        //бросаем спору влево
        direction = new Vector2(1, 0);
        Shoot(direction);
    }
}
