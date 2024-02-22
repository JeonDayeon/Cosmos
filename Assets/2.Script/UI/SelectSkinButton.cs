using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
struct Skins
{ 
    public enum Name
    {
        Cosmos,
        HanbokF,
        HanbokM,
        Satto,
        RomanceFantasyF,
        RomanceFantasyM,
        Blackrope,
        Nevula,
        Cosmos_2,
    }

    public Name name;

    public RuntimeAnimatorController animator;
    public Sprite img;
}

public class SelectSkinButton : MonoBehaviour
{
    [SerializeField]
    Skins[] skin;
    [SerializeField]
    Skins Cosmos_2;

    public GameObject Character;
    public PlayerData data;
    public Image Content;

    public string Name = "Cosmos";
    public int SkinNum = 0;
    public int page;

    public UiManager uim;
    public bool isSpecial; //특별 스킨일 때 판별

    // Start is called before the first frame update
    void Start()
    {
        uim = FindObjectOfType<UiManager>();
        data = FindObjectOfType<PlayerData>();
        Name = data.SkinName;
        ChangeSkin();

        Content.sprite = skin[SkinNum].img;
        page = SkinNum;
        //for(int i = 0; i < skin.Length; i++)
        //{
        //    Changeimage = Content.transform.GetChild(i).gameObject;
        //    Changeimage.transform.GetChild(0).GetComponent<Image>().sprite = skin[i].img;
        //    Changeimage.transform.GetChild(0).GetComponent<Image>().SetNativeSize();
        //    Changeimage.name = skin[i].name.ToString();
        //}
        //Button btn;
        //for(int i = 0; i < Content.transform.childCount; i++)
        //{
        //    if(Content.transform.GetChild(i).gameObject.name == "SelectSkinButton " + "(" + i + ")")
        //    {
        //        btn = Content.transform.GetChild(i).GetComponent<Button>();
        //        btn.interactable = false;
        //        Destroy(btn.gameObject.transform.GetChild(0).gameObject);                
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void newView()
    {
        Name = data.SkinName;
        ChangeSkin();
        Content.sprite = skin[SkinNum].img;
        page = SkinNum;

        Animator CAnimator = Character.GetComponent<Animator>();
        CAnimator.runtimeAnimatorController = skin[SkinNum].animator;
    }

    public void isSkinBtn()
    {
        GameObject clickBtn = EventSystem.current.currentSelectedGameObject;
        Name = clickBtn.name;
        ChangeSkin();
        //Character.transform.GetComponent<RuntimeAnimatorController>()
    }

    public void isNextSkinBtn()
    {
        if (isSpecial)
        {
            isSpecial = false;
        }

        if (page < skin.Length - 1)
        {
            page++;
            Content.sprite = skin[page].img;
            Debug.Log(page + " " + skin[page].name);

            Animator CAnimator = Character.GetComponent<Animator>();
            CAnimator.runtimeAnimatorController = skin[page].animator;
        }

        else
        {
            page = 0;
            Content.sprite = skin[page].img;
            Debug.Log(page + " " + skin[page].name);

            Animator CAnimator = Character.GetComponent<Animator>();
            CAnimator.runtimeAnimatorController = skin[page].animator;
        }
    }
    public void isPreviousSkinBtn()
    {
        if (isSpecial)
        {
            isSpecial = false;
        }

        if (page > 0)
        {
            page--;
            Content.sprite = skin[page].img;
            Debug.Log(page + " " + skin[page].name);

            Animator CAnimator = Character.GetComponent<Animator>();
            CAnimator.runtimeAnimatorController = skin[page].animator;
        }

        else
        {
            page = skin.Length - 1;
            Content.sprite = skin[page].img;
            Debug.Log(page + " " + skin[page].name);

            Animator CAnimator = Character.GetComponent<Animator>();
            CAnimator.runtimeAnimatorController = skin[page].animator;
        }
    }

    public void ChangeSkin()
    {
        Animator CAnimator = Character.GetComponent<Animator>();
        if (!isSpecial)
        {
            for (int i = 0; i < skin.Length; i++)
            {
                if (skin[i].name.ToString() == Name)
                {
                    SkinNum = i;
                    CAnimator.runtimeAnimatorController = skin[i].animator;
                    data.SkinName = Name;
                    break;
                }
            }
        }
    }

    public void SelectSkin()
    {
        uim.SelectEffect();
        if (isSpecial)
        {
            SkinNum = 8;
            Name = Cosmos_2.name.ToString();
            data.SkinName = Name;
            isSpecial = false;
        }

        else
        {
            SkinNum = page;
            Name = skin[SkinNum].name.ToString();
            data.SkinName = Name;
        }
    }

    public void NameSend()
    {
        data.SkinName = Name;
    }

    public void OtherSkin(int num)
    {
        if (page == 0)
        {
            isSpecial = true;
            Content.sprite = Cosmos_2.img;
            Debug.Log(page + " " + Cosmos_2.name);

            Animator CAnimator = Character.GetComponent<Animator>();
            CAnimator.runtimeAnimatorController = Cosmos_2.animator;
        }
    }
}
