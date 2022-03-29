using System.Collections.Generic;
using UnityEngine;

//пул любых снарядов 

public class ProjectilesPool : MonoBehaviour
{
    [SerializeField] protected GameObject projectilePref; //префаб снаряда

    [SerializeField] protected bool flipX; //инвертирование по X
    [SerializeField] protected bool flipY; //инвертирование по Y

    protected List<Projectile> projectiles; //пул снарядов

    protected Projectile currentProjectile; //текущий выпускаемый снаряд

    void Awake()
    {
        projectiles = new List<Projectile>();
    }

    public void Shoot(Vector2 direction)
    {
        //перебираем все снаряды
        foreach (var item in projectiles)
        {
            //если находим неактивный, выбираем его
            if (item.gameObject.activeSelf == false)
            {
                currentProjectile = item;
                break;
            }
        }
        //если такого снаряда не нашлось
        if (currentProjectile == null)
        {
            //создаем его
            currentProjectile = Instantiate(projectilePref, transform.position, Quaternion.identity, transform).GetComponent<Projectile>();
            //помещаем его в коллекцию
            projectiles.Add(currentProjectile);
            //поворачиваем снаряд, если нужно
            currentProjectile.Flip(flipX, flipY);
        }
        else
        {
            //помещаем снаряд в исходную позицию
            currentProjectile.transform.position = transform.position;
            //включаем снаряд
            currentProjectile.gameObject.SetActive(true);
        }
        //придаем ему ускорение
        currentProjectile.Shoot(direction);
        //обнуляем снаряд
        currentProjectile = null;
    }
}
