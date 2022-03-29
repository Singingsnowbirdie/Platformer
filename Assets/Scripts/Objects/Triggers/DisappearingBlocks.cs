using System.Collections;
using UnityEngine;

//исчезающие платформы

public class DisappearingBlocks : Trigger
{
    Block[] blocks;

    bool isDisappearing;

    private void Awake()
    {
        blocks = GetComponentsInChildren<Block>();
    }

    IEnumerator Disappear()
    {
        isDisappearing = true;
        foreach (var item in blocks)
        {
            item.Tremble();
        }
        yield return new WaitForSeconds(1f);

        foreach (var item in blocks)
        {
            item.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.5f);

        foreach (var item in blocks)
        {
            item.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
        isDisappearing = false;
    }

    protected override void WorkOut(Collider2D other)
    {
        if (!isDisappearing)
        {
            StartCoroutine(Disappear());
        }
    }
}
