using UnityEngine;

//вешается на стену, для уничтожения (отключения) снарядов

public class WallTrigger : MonoBehaviour
{
    //касание
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Projectile>() != null)
        {
            other.gameObject.SetActive(false);
        }
    }
}
