using UnityEngine;

//Лифт

public class Elevator : MonoBehaviour
{
    [SerializeField] float speed; //скорость
    [SerializeField] private Transform basePoint; //исходное положение платформы
    [SerializeField] private Transform targetPoint; //целевое положение платформы

    private Rigidbody2D platformRB; //физика
    private bool playerIsOn; //игрок стоит на платформе

    private void Awake()
    {
        platformRB = GetComponent<Rigidbody2D>();
    }

    //проверка касания
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //если триггера касаются ноги игрока
        if (collision.gameObject.CompareTag("GroundSensor"))
            playerIsOn = true;
    }

    //проверка касания
    private void OnTriggerExit2D(Collider2D collision)
    {
        //если триггера не касаются ноги игрока
        if (collision.gameObject.CompareTag("GroundSensor"))
            playerIsOn = false;
    }

    void Update()
    {
        //движение к цели
        if (playerIsOn)
        {
            if (transform.position.y < targetPoint.position.y)
                platformRB.velocity = new Vector2(0, speed);
            else
                platformRB.velocity = Vector2.zero;
        }
        //возвращение
        else if (!playerIsOn)
        {
            if (transform.position.y > basePoint.position.y)
                platformRB.velocity = new Vector2(0, -speed);
            else
                platformRB.velocity = Vector2.zero;
        }
    }
}
