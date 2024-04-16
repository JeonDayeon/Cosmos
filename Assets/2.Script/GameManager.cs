using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; //���߿� �ε������ ���� ����

[System.Serializable]
struct portraitCharacterImg
{
    public enum CharacterImg
    {
        �ڵ�,
        �׺��,
        ���,
        �ָ�,
    }

    [SerializeField]
    public CharacterImg characterImg;

    public Image img; //ĳ���� �̹��� ������Ʈ
}

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    MapOption mapOption;
    //��ȭ-------------------------------------------
    TalkManager talkmanager;
    public GameObject TalkBox;
    [SerializeField]
    portraitCharacterImg[] imageArr;
    public TextMeshProUGUI talkText;
    public TextMeshProUGUI nameText;
    public int talkid;
    int talkindex;
    public bool isTalk;
    //UI����Ű--------------------------------------
    public bool isMain = false;
    public GameObject ExitUI;
    //UIȰ�� boolean--------------------------------
    public GameObject GameSetting;
    //�����----------------------------------------
    public AudioSource audioS;
    public AudioClip ClickS;
    //����------------------------------------------
    public bool stage;
    //���̷�----------------------------------------
    public GameObject myroom;
    public RuntimeAnimatorController myroomAnim;

    // Start is called before the first frame update
    void Start()
    {
        audioS = GameObject.Find("Effect").GetComponent<AudioSource>();
        //Find�� �ʿ� ���� �˾Ƽ� ã�� �� �ְ� �ϱ� ����
        player = FindObjectOfType<PlayerController>();
        mapOption = FindObjectOfType<MapOption>();

        talkmanager = FindObjectOfType<TalkManager>();
        TalkBox = GameObject.Find("TalkBox");
        talkText = GameObject.Find("TalkText").GetComponent<TextMeshProUGUI>();
        nameText = GameObject.Find("NameText").GetComponent<TextMeshProUGUI>();
        talkindex = 0; //�� ������ ������� �������� ����
        
        talkid = mapOption.Id; //�� ���̵� ��������
        isTalk = mapOption.isStory;

        stage = mapOption.isStage;//������������ �ƴ���

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
            Debug.Log("�Լ�");
      
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
                    SceneManager.LoadScene(3, LoadSceneMode.Single);//�����ϱ�
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
