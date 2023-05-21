using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeManager : MonoBehaviour
{
    Slider Timer;
    float fSliderBarTime;
    PlayerController player;
    void Start()
    {
        Timer = GetComponent<Slider>();
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (Timer.value > 0.0f)
        {
            // 시간이 변경한 만큼 slider Value 변경
            Timer.value -= Time.deltaTime;
        }
        else
        {
            player.GameOver();
        }
    }
}
