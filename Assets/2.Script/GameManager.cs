using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; //나중에 로드씬으로 쓰고 제거

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
    public PlayerController player;
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
    //오디오----------------------------------------
    public AudioSource audioS;
    public AudioClip ClickS;
    //마을------------------------------------------
    public bool stage;
    //마이룸----------------------------------------
    public GameObject myroom;
    public RuntimeAnimatorController myroomAnim;

    // Start is called before the first frame update
    void Start()
    {
        audioS = GameObject.Find("Effect").GetComponent<AudioSource>();
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

        stage = mapOption.isStage;//스테이지인지 아닌지

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
        Time.timeScale = 0.0f;

        audioS.clip = ClickS;
        audioS.Play();
        TalkBox.SetActive(true);

        string talkData = talkmanager.GetTalk(talkid, talkindex, "Content");
        string nameData = talkmanager.GetTalk(talkid, talkindex, "Name");
        string emotionData = talkmanager.GetTalk(talkid, talkindex, "Emotion");
        string ActiveChar = talkmanager.GetTalk(talkid, talkindex, "Active");

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
                        //player.goal.SetActive(true);
                        player.BGM.Stop();
                    }
                }

                else
                {
                    talkid = mapOption.nextId;
                    //player.goal.SetActive(true);
                    player.BGM.Stop();
                    SceneManager.LoadScene(3, LoadSceneMode.Single);//수정하기
                }
            }
            if (player.goal.activeSelf)
                Time.timeScale = 0.0f;
            else
            {
                Time.timeScale = 1.0f;
                if (stage)
                {
                    SceneManager.LoadScene(2, LoadSceneMode.Single);
                    Time.timeScale = 1.0f;
                }
            }
            return;
        }

        else
        {
            talkindex++;
        }
    }

}
