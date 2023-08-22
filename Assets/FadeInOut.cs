using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    SpriteRenderer Effect;
    IEnumerator FadeInFlow()
    {
        Color alpha = Effect.color;
        Debug.Log("���̵���" + alpha);
        while (alpha.a > 0f)
        {
            alpha.a -= 0.1f;
            Effect.color = alpha;
            yield return new WaitForSeconds(0.07f);
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(FadeOutFlow());
    }

    IEnumerator FadeOutFlow() //���̵� �� �ƿ� �ڷ�ƾ
    {
        Color alpha = Effect.color;
        Debug.Log("���̵�ƿ�" + alpha);
        while (alpha.a < 1f)
        {
            alpha.a += 0.1f;
            Effect.color = alpha;
            yield return new WaitForSeconds(0.05f);
        }
        Debug.Log("���̵�ƿ�" + alpha);
        yield return new WaitForSeconds(1f);
        StartCoroutine(FadeInFlow());
    }
}
