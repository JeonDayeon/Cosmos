using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    SpriteRenderer Effect;
    IEnumerator FadeInFlow()
    {
        Color alpha = Effect.color;
        Debug.Log("페이드인" + alpha);
        while (alpha.a > 0f)
        {
            alpha.a -= 0.1f;
            Effect.color = alpha;
            yield return new WaitForSeconds(0.07f);
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(FadeOutFlow());
    }

    IEnumerator FadeOutFlow() //페이드 인 아웃 코루틴
    {
        Color alpha = Effect.color;
        Debug.Log("페이드아웃" + alpha);
        while (alpha.a < 1f)
        {
            alpha.a += 0.1f;
            Effect.color = alpha;
            yield return new WaitForSeconds(0.05f);
        }
        Debug.Log("페이드아웃" + alpha);
        yield return new WaitForSeconds(1f);
        StartCoroutine(FadeInFlow());
    }
}
