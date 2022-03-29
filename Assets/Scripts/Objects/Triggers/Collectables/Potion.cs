using UnityEngine;

public class Potion : Collectable
{
    [SerializeField] int healingPoints = 30;

    protected override void Collect(Collider2D other)
    {
        base.Collect(other);
        //лечим игрока
        other.gameObject.GetComponent<Player>().Health.Heal(healingPoints);
        //уничтожаем объект через некоторое время
        Destroy(gameObject, 2f);
    }
}
