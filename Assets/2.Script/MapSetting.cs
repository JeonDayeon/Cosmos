using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSetting : MonoBehaviour
{
    private GameManager game;
    
    public Transform timer;
    public float Time;

    public bool isTalk;
    public int TalkNumber;

    private void Start()
    {
        timer = GameObject.Find("GameUI").transform.Find("GameTimer");
        game = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(Time != 0)
            {
                timer.transform.GetComponent<TimeManager>().SetTimer(Time);
            }

            if(isTalk)
            {
                game.talkid = TalkNumber;
                game.isTalk = isTalk;
                Invoke("game.Talk", 50f);
            }
        }
    }
}
