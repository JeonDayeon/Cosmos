using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    PlayerController player;
    MapOption mapOption;
    //��ȭ-------------------------------------------
    TalkManager talkmanager;
    public GameObject TalkBox;
    public Text talkText;

    int talkid;
    int talkindex;
    bool isTalk;
    // Start is called before the first frame update
    void Start()
    {
        //Find�� �ʿ� ���� �˾Ƽ� ã�� �� �ְ� �ϱ� ����
        player = FindObjectOfType<PlayerController>();
        mapOption = FindObjectOfType<MapOption>();

        talkmanager = FindObjectOfType<TalkManager>();
        TalkBox = GameObject.Find("TalkBox");
        talkText = GameObject.Find("TalkText").GetComponent<Text>();

        talkindex = 0; //�� ������ ������� �������� ����
        talkid = mapOption.Id; //�� ���̵� ��������

        Talk();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            Talk();
        }
    }

    void Talk()
    {
        TalkBox.SetActive(true);
        string talkData = talkmanager.GetTalk(talkid, talkindex);
        talkText.text = talkData;
        Time.timeScale = 0;
        if (talkData == null)
        {
            talkindex = 0;
            TalkBox.SetActive(false);
            talkid = mapOption.nextId;
            Time.timeScale = 1.0f;
            return;
        }

        else
        {
            talkindex++;
        }
    }

}
