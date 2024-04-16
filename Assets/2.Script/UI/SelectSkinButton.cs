using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
struct Skins_
{ 
    public enum Name
    {
        Cosmos,
        Cosmos_2,
        HanbokF,
        HanbokM,
        Satto,
        RomanceFantasyF,
        RomanceFantasyM,
        Blackrope,
        Nevula,
    }

    public Name name;

    public RuntimeAnimatorController animator;
    public Sprite img;
}

public class SelectSkinButton : MonoBehaviour
{
    [SerializeField]
    Skins[] skin;

    public GameObject Character;
    public PlayerData data;
    public GameObject Content; //��ũ�Ѻ� �ȿ� �ִ� content ����ֱ�

    public string Name = "Cosmos";
    // Start is called before the first frame update
    void Start()
    {
        data = FindObjectOfType<PlayerData>();
        Name = data.SkinName;
        ChangeSkin();
        GameObject Changeimage;
        for (int i = 0; i < skin.Length; i++) //��Ų ��ư ĭ �̹��� ��ü
        {
            Changeimage = Content.transform.GetChild(i).gameObject;
            Changeimage.transform.GetChild(0).GetComponent<Image>().sprite = skin[i].img;
            Changeimage.transform.GetChild(0).GetComponent<Image>().SetNativeSize();//ũ�� ��û Ŀ���� ���� �߻�
            RectTransform rect_ = Changeimage.transform.GetChild(0).GetComponent<RectTransform>();//��Ų �̹��� ��Ʈ ��������
            rect_.sizeDelta = new Vector2(rect_.rect.width / 3, rect_.rect.height / 3); //�� ������ ũ�� �۰� �����
            Changeimage.name = skin[i].name.ToString(); //��ư �̸� ��Ų �̸����� ����
        }
        Button btn;
        for (int i = 0; i < Content.transform.childCount; i++)//��Ų�� ���� ���� ��ư
        {
            if (Content.transform.GetChild(i).gameObject.name == "SelectSkinButton " + "(" + i + ")")
            {
                btn = Content.transform.GetChild(i).GetComponent<Button>();
                btn.interactable = false; //��Ȱ��ȭ
                Destroy(btn.gameObject.transform.GetChild(0).gameObject); //��ư �ȿ� �̹��� ����
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void isSkinBtn()
    {
        GameObject clickBtn = EventSystem.current.currentSelectedGameObject;
        Name = clickBtn.name;
        ChangeSkin();
    }

    public void ChangeSkin()
    {
        Debug.Log("����");
        Animator CAnimator = Character.GetComponent<Animator>();
        for (int i = 0; i < skin.Length; i++)
        {
            if (skin[i].name.ToString() == Name)
            {
                CAnimator.runtimeAnimatorController = skin[i].animator;
                data.SkinName = Name;
                break;
            }
        }
    }
    public void NameSend()
    {
        data.SkinName = Name;
    }
}