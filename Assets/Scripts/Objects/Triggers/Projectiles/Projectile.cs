using UnityEngine;

//снаряд

public class Projectile : Trigger
{
    [SerializeField] protected float damage; //урон
    [SerializeField] protected float speed; //скорость

    protected SpriteRenderer renderer; //рендерер
    protected Rigidbody2D rigidbody; //физика

    protected bool hit; //было попадание

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    //при контакте
    protected override void WorkOut(Collider2D other)
    {
        Hit(other);
        Disable();
    }

    protected virtual void Hit(Collider2D other)
    {
        //наносим повреждение
        other.gameObject.GetComponent<Player>().Health.TakeDamage(damage);
    }

    private void Disable()
    {
        //выключаем снаряд
        gameObject.SetActive(false);
    }

    //поворачивает снаряд, при необходимости
    internal void Flip(bool flipX, bool flipY)
    {
        renderer.flipX = flipX;
        renderer.flipY = flipY;
    }

    //запускаем снаряд
    internal void Shoot(Vector2 direction)
    {
        Vector2 velocity = new Vector2(direction.x * speed, direction.y);
        rigidbody.velocity = velocity;
    }
}
