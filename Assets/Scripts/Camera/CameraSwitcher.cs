using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    GameObject cam;

    private void Awake()
    {
        cam = transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CameraTrigger"))
        {
            cam.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("CameraTrigger"))
        {
            cam.SetActive(false);
        }
    }

}
