using UnityEngine;

public class SnowTrigger : MonoBehaviour
{
    GameObject node;

    private void Awake()
    {
        node = transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CameraTrigger"))
        {
            node.SetActive(true);
        }
    } 
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("CameraTrigger"))
        {
            node.SetActive(false);
        }
    }

}
