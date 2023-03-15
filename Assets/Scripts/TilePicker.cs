using NUnit.Framework.Internal;
using System.Collections;
using UnityEngine;

public class TilePicker : MonoBehaviour
{
    [HideInInspector] public bool isBeingPicked = false;

    public Wave wave;

    [SerializeField] private float animateInTime = 0.5f;
    [SerializeField] private float pickedHeight = 0.25f;


    float easeOutElastic(float x) => x <= 0 ? 0 : x >= 1 ? 1 : Mathf.Pow(2, -10 * x) * Mathf.Sin((x * 10 - 0.75f) * (2 * Mathf.PI) / 3) + 1;
    float easeOutBounce(float x) {
        float n1 = 7.5625f;
        float d1 = 2.75f;

        if (x < 1 / d1) {
            return n1 * x * x;
        } else if (x < 2 / d1)
        {
            return n1 * (x -= 1.5f / d1) * x + 0.75f;
        }
        else if (x < 2.5 / d1)
        {
            return n1 * (x -= 2.25f / d1) * x + 0.9375f;
        }
        else
        {
            return n1 * (x -= 2.625f / d1) * x + 0.984375f;
        }

    }

    private IEnumerator Up()
    {
        print(wave);
        float elapsedTime = 0f;

        while (elapsedTime < animateInTime)
        {
            elapsedTime += Time.deltaTime;

            transform.position = new Vector3(
                transform.position.x,
                easeOutElastic(elapsedTime / animateInTime) * pickedHeight,
                transform.position.z
            );

            yield return null;
        }
    }
    private IEnumerator Down()
    {
        float elapsedTime = 0f;
        float o = transform.position.y;

        while (elapsedTime < animateInTime)
        {
            elapsedTime += Time.deltaTime;
            float targetEndY = GetComponent<Wave>().CalculateWaveY(transform.position);

            transform.position = new Vector3(
                transform.position.x,
                Mathf.Lerp(o, targetEndY, easeOutBounce(elapsedTime / animateInTime)),
                transform.position.z
            );

            yield return null;
        }
    }

    private void OnMouseOver()
    {
        if (!isBeingPicked)
        {
            isBeingPicked = true;
            StopCoroutine(nameof(Down));
            StartCoroutine(nameof(Up));
        }
    }
    private void OnMouseExit()
    {
        isBeingPicked = false;
        StopCoroutine(nameof(Up));
        StartCoroutine(nameof(Down));
    }
}
