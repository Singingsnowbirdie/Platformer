using UnityEngine;

//Движущаяся платформа

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed; //скорость платформы

    private float direction = 1; //направление движения
    private Rigidbody2D rigidbody; //физика
    private Player player; //игрок

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //двигаем платформу
        rigidbody.velocity = new Vector2(direction * speed, 0);
    }

    //проверка касания
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //если платформы касаются ноги игрока
        if (collision.gameObject.CompareTag("GroundSensor"))
        {
            player = collision.gameObject.GetComponentInParent<Player>();
        }

        //если достигли лимита
        if (collision.gameObject.CompareTag("MotionTrigger"))
        {
            direction *= -1;
        }
    }

    //пока игрок стоит на платформе
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GroundSensor"))
        {
            player.FixPlatformBehaviour(rigidbody.velocity.x);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //когда игрок покидает платформу
        if (collision.gameObject.CompareTag("GroundSensor"))
        {
            player.FixPlatformBehaviour(0);
        }
    }
}


