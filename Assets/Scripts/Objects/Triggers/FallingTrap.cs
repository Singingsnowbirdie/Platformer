using UnityEngine;

//падающая ловушка

public class FallingTrap : Trigger
{
    //соединение
    [SerializeField] Joint2D joint;
    //задержка перед отключением соединения
    [SerializeField] float delay;
    //интенсивность тряски камеры
    [SerializeField] float cameraShakingIntencity = 5f;
    //продолжительность тряски камеры
    [SerializeField] float cameraShakingTime = 0.1f;

    void BreakJoint()
    {
        if (joint == null)
            joint = GetComponentInParent<Joint2D>();

        joint.enabled = false;
    }

    protected override void WorkOut(Collider2D other)
    {
        //потрясываем камеру
        EventManager.ShakeCamera(cameraShakingIntencity, cameraShakingTime);
        //отключаем соединение по таймеру
        Invoke("BreakJoint", delay);
    }
}
