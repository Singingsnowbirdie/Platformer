using UnityEngine;

public class RangeTrigger : MonoBehaviour
{
    public bool IsPlayerIn { get; private set; }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsPlayerIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsPlayerIn = false;
        }
    }
}
