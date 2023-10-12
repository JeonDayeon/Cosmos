using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSetting : MonoBehaviour
{
    public Transform timer;
    public float Time;

    private void Start()
    {
        timer = GameObject.Find("GameUI").transform.Find("GameTimer");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(Time != 0)
            {
                timer.transform.GetComponent<TimeManager>().SetTimer(Time);
            }
        }
    }
}
