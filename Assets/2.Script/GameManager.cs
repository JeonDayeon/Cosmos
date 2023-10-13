using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
struct portraitCharacterImg
{
    public enum CharacterImg
    {
        코디,
        네뷸라,
        노바,
        주모,
    }

    [SerializeField]
    public CharacterImg characterImg;

    public Image img; //캐릭터 이미지 오브젝트
}

public class GameManager : MonoBehaviour
{
    PlayerController player;
    MapOption mapOption;
    //대화-------------------------------------------
    TalkManager talkmanager;
    public GameObject TalkBox;
    [SerializeField]
    portraitCharacterImg[] imageArr;
    public TextMeshProUGUI talkText;
    public TextMeshProUGUI nameText;
    public int talkid;
    int talkindex;
    public bool isTalk;
    //UI단축키--------------------------------------
    public bool isMain = false;
    public GameObject ExitUI;
    //UI활성 boolean--------------------------------
    public GameObject GameSetting;

    // Start is called before the first frame update
    void Start()
    {
        //Find는 맵에 들어가면 알아서 찾을 수 있게 하기 위함
        player = FindObjectOfType<PlayerController>();
        mapOption = FindObjectOfType<MapOption>();

        talkmanager = FindObjectOfType<TalkManager>();
        TalkBox = GameObject.Find("TalkBox");
        talkText = GameObject.Find("TalkText").GetComponent<TextMeshProUGUI>();
        nameText = GameObject.Find("NameText").GetComponent<TextMeshProUGUI>();
        talkindex = 0; //톡 데이터 순서대로 내보내기 위함
        talkid = mapOption.Id; //맵 아이디 가져오기

        isTalk = mapOption.isStory;

        if (isTalk)
        {
            Time.timeScale = 0.0f;
            Talk();
        }

        else
        {
            TalkBox.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && isTalk && !GameSetting.activeSelf)
        {
            Talk();
        }
    }

    public void Talk()
    {
        TalkBox.SetActive(true);

        string talkData = talkmanager.GetTalk(talkid, talkindex, "Content");
        string nameData = talkmanager.GetTalk(talkid, talkindex, "Name");
        string emotionData = talkmanager.GetTalk(talkid, talkindex, "Emotion");
        string ActiveChar = talkmanager.GetTalk(talkid, talkindex, "Active");

        Debug.Log("이모션 이모션" + emotionData);
        talkText.text = talkData;
        nameText.text = nameData;

        if (emotionData != "n")
        {
            Debug.Log("입성");
      
            for (int i = 0; i < imageArr.Length; i++)
            {
                if(imageArr[i].characterImg.ToString() == nameData)
                {
                    if (!imageArr[i].img.gameObject.activeSelf)
                    {
                        imageArr[i].img.gameObject.SetActive(true);
                    }

                    if (ActiveChar == "f")
                    {
                        imageArr[i].img.gameObject.SetActive(false);

                    }

                    imageArr[i].img.sprite = talkmanager.GetPortrait(nameData, emotionData);
                    imageArr[i].img.color = Color.white;
                    imageArr[i].img.SetNativeSize();
                }

                else if (imageArr[i].characterImg.ToString() != nameData)
                {   
                    imageArr[i].img.color = Color.gray;
                }
            }

        }

        if (talkData == null)
        {
            talkindex = 0;
            TalkBox.SetActive(false);
            isTalk = false;
            mapOption.isStory = false;
            for (int i = 0; i < imageArr.Length; i++)
            {
                imageArr[i].img.gameObject.SetActive(false);
            }
            if (talkid % 10 == 0)
            {
                talkid += 1;
            }
            else
            {
                if (player.QuestPanel != null)
                {
                    if (player.HaveItemNum >= player.QuestItemNumber)
                    {
                        talkid = mapOption.nextId;
                        player.goal.SetActive(true);
                    }
                }

                else
                {
                    talkid = mapOption.nextId;
                    player.goal.SetActive(true);
                }
            }
            Time.timeScale = 1.0f;
            return;
        }

        else
        {
            talkindex++;
        }
    }

}
