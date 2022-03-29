using UnityEngine;

public class Pendulum : MonoBehaviour
{
    Rigidbody2D rigidbody; //тело

    Player player; //игрок

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    //проверка касания
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //если платформы касаются ноги игрока
        if (collision.gameObject.CompareTag("GroundSensor"))
        {
            player = collision.gameObject.GetComponentInParent<Player>();
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
