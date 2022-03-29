using UnityEngine;

//светлячок (бонус)

public class Spark : Collectable
{
    [SerializeField] private int bonus; //стоимость бонуса

    internal int BonusCost => bonus;
    internal bool IsCollected => isCollected;
}
