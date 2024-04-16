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
    public GameObject Content; //스크롤뷰 안에 있는 content 집어넣기

    public string Name = "Cosmos";
    // Start is called before the first frame update
    void Start()
    {
        data = FindObjectOfType<PlayerData>();
        Name = data.SkinName;
        ChangeSkin();
        GameObject Changeimage;
        for (int i = 0; i < skin.Length; i++) //스킨 버튼 칸 이미지 교체
        {
            Changeimage = Content.transform.GetChild(i).gameObject;
            Changeimage.transform.GetChild(0).GetComponent<Image>().sprite = skin[i].img;
            Changeimage.transform.GetChild(0).GetComponent<Image>().SetNativeSize();//크기 엄청 커지는 문제 발생
            RectTransform rect_ = Changeimage.transform.GetChild(0).GetComponent<RectTransform>();//스킨 이미지 렉트 가져오기
            rect_.sizeDelta = new Vector2(rect_.rect.width / 3, rect_.rect.height / 3); //값 나눠서 크기 작게 만들기
            Changeimage.name = skin[i].name.ToString(); //버튼 이름 스킨 이름으로 변경
        }
        Button btn;
        for (int i = 0; i < Content.transform.childCount; i++)//스킨이 없어 남는 버튼
        {
            if (Content.transform.GetChild(i).gameObject.name == "SelectSkinButton " + "(" + i + ")")
            {
                btn = Content.transform.GetChild(i).GetComponent<Button>();
                btn.interactable = false; //비활성화
                Destroy(btn.gameObject.transform.GetChild(0).gameObject); //버튼 안에 이미지 제거
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
        Debug.Log("변경");
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