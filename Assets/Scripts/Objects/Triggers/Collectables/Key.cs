//ключ

using UnityEngine;

public class Key : Collectable
{
    protected override void Collect(Collider2D other)
    {
        base.Collect(other);
        //сообщаем что получен ключ
        EventManager.KeyReceived();
        //удаляем
        Destroy(gameObject, 2f);
    }
}
