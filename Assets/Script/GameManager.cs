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
    public Text nameText;
    int talkid;
    int talkindex;
    bool isTalk;
    //UI����Ű--------------------------------------
    public bool isMain = false;
    public GameObject ExitUI;
    // Start is called before the first frame update
    void Start()
    {
        //Find�� �ʿ� ���� �˾Ƽ� ã�� �� �ְ� �ϱ� ����
        player = FindObjectOfType<PlayerController>();
        mapOption = FindObjectOfType<MapOption>();

        talkmanager = FindObjectOfType<TalkManager>();
        TalkBox = GameObject.Find("TalkBox");
        talkText = GameObject.Find("TalkText").GetComponent<Text>();
        nameText = GameObject.Find("NameText").GetComponent<Text>();

        talkindex = 0; //�� ������ ������� �������� ����
        talkid = mapOption.Id; //�� ���̵� ��������

        isTalk = true;
        Talk();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && isTalk)
        {
            Talk();
        }
    }

    void Talk()
    {
        TalkBox.SetActive(true);
        string talkData = talkmanager.GetTalk(talkid, talkindex, "Content");
        string nameData = talkmanager.GetTalk(talkid, talkindex, "Name");
        talkText.text = talkData;
        nameText.text = nameData;
        Time.timeScale = 0;
        if (talkData == null)
        {
            talkindex = 0;
            TalkBox.SetActive(false);
            talkid = mapOption.nextId;
            isTalk = false;
            Time.timeScale = 1.0f;
            return;
        }

        else
        {
            talkindex++;
        }
    }

}
