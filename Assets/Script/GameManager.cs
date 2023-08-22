using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
struct portraitCharacterImg
{
    public enum CharacterImg
    {
        �ڵ�,
        �׺��,
        ���
    }

    [SerializeField]
    public CharacterImg characterImg;

    public Image img;
}

public class GameManager : MonoBehaviour
{
    PlayerController player;
    MapOption mapOption;
    //��ȭ-------------------------------------------
    TalkManager talkmanager;
    public GameObject TalkBox;
    [SerializeField]
    portraitCharacterImg[] imageArr;
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
        if (Input.anyKeyDown && isTalk)
        {
            Talk();
        }
    }

    void Talk()
    {
        int index = 0;
        TalkBox.SetActive(true);

        string talkData = talkmanager.GetTalk(talkid, talkindex, "Content");
        string nameData = talkmanager.GetTalk(talkid, talkindex, "Name");
        string emotionData = talkmanager.GetTalk(talkid, talkindex, "Emotion");
        Debug.Log("�̸�� �̸��" + emotionData);
        talkText.text = talkData;
        nameText.text = nameData;
        Time.timeScale = 0;

        if(emotionData != "n")
        {
            for(int i = 0; i < imageArr.Length; i++)
            {
                if(imageArr[i].characterImg.ToString() == nameData)
                {
                    index = i;
                    if (!imageArr[i].img.gameObject.activeSelf)
                    {
                        imageArr[i].img.gameObject.SetActive(true);
                    }
                    imageArr[i].img.sprite = talkmanager.GetPortrait(nameData, emotionData);
                    imageArr[i].img.color = Color.white;
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
            talkid = mapOption.nextId;
            isTalk = false;
            for (int i = 0; i < imageArr.Length; i++)
            {
                imageArr[i].img.gameObject.SetActive(false);
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
