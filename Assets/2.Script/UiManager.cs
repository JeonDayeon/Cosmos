using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UiManager : MonoBehaviour
{
    public Image Panel;
    float time = 0f;
    float F_time = 1f;

    public AudioSource audioSource;
    public AudioClip FadeoutS;
    public AudioClip FadeinS;
    public AudioClip GoTravelS;

    public AudioClip successS;
    public AudioClip DingdongS;

    public GameObject spaceship;
    public float uptime;

    public Image SkinBg;
    public Color SelectCol;
    public int num;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {

    }

    public void CancleAudio()
    {

    }
    public void StartFade()
    {
        StartCoroutine(FadeOutFlow());
    }
    public void GoTravelEffect()
    {
        audioSource.PlayOneShot(GoTravelS);
        StartCoroutine(PlaySound());
    }
    IEnumerator PlaySound()
    {
        audioSource.PlayOneShot(GoTravelS);
        yield return new WaitForSeconds(10f);
    }

    IEnumerator FadeOutFlow()
    {
        audioSource.clip = FadeoutS;
        audioSource.Play();
        //Panel.gameObject.SetActive(true);
        time = 0.8f;
        Color alpha = Panel.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Panel.color = alpha;
            yield return null;
        }
        yield return null;
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(FadeInFlow());
    }

    IEnumerator FadeInFlow()
    {
        audioSource.clip = FadeinS;
        audioSource.Play();
        time = 0f;
        Color alpha = Panel.color;
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            Panel.color = alpha;
            yield return null;
        }
        Panel.gameObject.SetActive(false);
        yield return null;
    }

    //»õÈ¿°ú
    public void SelectEffect()
    {
        StartCoroutine(SelectBrite());
    }

    IEnumerator SelectBrite()
    {
        audioSource.PlayOneShot(successS);
        Color alpha = SkinBg.color;
        while (alpha != SelectCol)
        {
            alpha = Color.Lerp(alpha, SelectCol, 0.3f);
            SkinBg.color = alpha;
            yield return null;
        }
        yield return null;
        StartCoroutine(SelectDark());
    }

    IEnumerator SelectDark()
    {;
        Debug.Log("Dark");
        time = 0.8f;
        Color alpha = SkinBg.color;
        while (alpha != Color.black)
        {
            alpha = Color.Lerp(alpha, Color.black, 0.35f);
            SkinBg.color = alpha;
            yield return null;
        }

        audioSource.PlayOneShot(DingdongS);

        yield return new WaitForSeconds(0.5f);
        SkinBg.gameObject.SetActive(false);
        Panel.gameObject.SetActive(true);

        time = 0.8f;
        alpha = Panel.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            Panel.color = alpha;
        }

        time = 0f;
        while (alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(1, 0, time);
            Panel.color = alpha;
            yield return null;
        }
        Panel.gameObject.SetActive(false);
        yield return null;
    }
}
