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

    public GameObject spaceship;
    public float uptime;
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
    
    
}
