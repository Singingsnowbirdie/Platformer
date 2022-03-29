using UnityEngine;

//контроллер фона

public class ParallaxController : MonoBehaviour
{
    [SerializeField] Transform[] layers;
    [SerializeField] float[] coeff;
    private int layersCount;

    private void Awake()
    {
        layersCount = layers.Length;
    }

    void Update()
    {
        for (int i = 0; i < layersCount; i++)
        {
            layers[i].position = transform.position * coeff[i];
        }
    }
}
