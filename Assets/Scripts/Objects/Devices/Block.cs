using UnityEngine;

public class Block : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        animator.SetBool("tremble", false);
    }

    public void Tremble()
    {
        animator.SetBool("tremble", true);
    }
}
