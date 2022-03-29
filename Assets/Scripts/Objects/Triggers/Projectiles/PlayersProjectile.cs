using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//снаряд игрока (магия)

public class PlayersProjectile : Projectile
{
    protected override void Hit(Collider2D other)
    {
        //наносим повреждение
        other.gameObject.GetComponent<Enemy>().Health.TakeDamage(damage);
    }
}
