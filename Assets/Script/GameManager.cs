using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    PlayerController player;
    MapOption mapOption;
    //대화-------------------------------------------
    TalkManager talkmanager;
    public GameObject TalkBox;
    public Text talkText;
    public Text nameText;
    int talkid;
    int talkindex;
    bool isTalk;
    //UI단축키--------------------------------------
    public bool isMain = false;
    public GameObject ExitUI;
    // Start is called before the first frame update
    void Start()
    {
        //Find는 맵에 들어가면 알아서 찾을 수 있게 하기 위함
        player = FindObjectOfType<PlayerController>();
        mapOption = FindObjectOfType<MapOption>();

        talkmanager = FindObjectOfType<TalkManager>();
        TalkBox = GameObject.Find("TalkBox");
        talkText = GameObject.Find("TalkText").GetComponent<Text>();
        nameText = GameObject.Find("NameText").GetComponent<Text>();

        talkindex = 0; //톡 데이터 순서대로 내보내기 위함
        talkid = mapOption.Id; //맵 아이디 가져오기

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
