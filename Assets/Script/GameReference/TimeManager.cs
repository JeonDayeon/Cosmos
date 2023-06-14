using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeManager : MonoBehaviour
{
    public Slider Timer;
    float fSliderBarTime;
    PlayerController player;
    void Start()
    {
        Timer = gameObject.GetComponent<Slider>();
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (Timer.value > 0.0f)
        {
            // �ð��� ������ ��ŭ slider Value ����
            Timer.value -= Time.deltaTime;
        }
        else
        {
            player.GameOver();
        }
    }

    public void SetTimer(float time)
    {
        gameObject.SetActive(true);
        Timer = gameObject.GetComponent<Slider>();
        Timer.maxValue = time;
        Timer.value = Timer.maxValue;
    }
}
