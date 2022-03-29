using UnityEngine;

//трамплин

public class JumpPad : MonoBehaviour
{
    [SerializeField] float tossForce; //подбрасыающая сила

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GroundSensor"))
        {
            other.GetComponentInParent<Player>().GetComponent<Rigidbody2D>().AddForce(Vector2.up * tossForce, ForceMode2D.Impulse);
        }
    }
}
