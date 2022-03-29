using UnityEngine;

//дверь

public class Door : Trigger
{
    private bool hasKey; //есть ключ
    private Animator animator; //аниматор

    private void OnEnable()
    {
        EventManager.OnKeyReceivedEvent += KeyReceived;
    }
    private void OnDisable()
    {
        EventManager.OnKeyReceivedEvent -= KeyReceived;
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    //получен ключ
    private void KeyReceived()
    {
        hasKey = true;
    }

    //сообщает о том, что уровень пройден (вызывается из аниматора)
    public void LevelPassed()
    {
        EventManager.LevelPassed();
    }

    protected override void WorkOut(Collider2D other)
    {
        if (hasKey)
        {
            //запускаем анимацию
            animator.SetTrigger("open");
        }
    }
}
