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
        Blackrope,
        Cosmos,
        HanbokF,
        HanbokM,
        RomanceFantasyF,
        RomanceFantasyM,
        Satto
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
    public GameObject Content;

    public string Name = "Cosmos";
    // Start is called before the first frame update
    void Start()
    {
        data = FindObjectOfType<PlayerData>();
        Name = data.SkinName;
        ChangeSkin();
        GameObject Changeimage;
        for(int i = 0; i < skin.Length; i++)
        {
            Changeimage = Content.transform.GetChild(i).gameObject;
            Changeimage.transform.GetChild(0).GetComponent<Image>().sprite = skin[i].img;
            Changeimage.transform.GetChild(0).GetComponent<Image>().SetNativeSize();
            Changeimage.name = skin[i].name.ToString();
        }
        Button btn;
        for(int i = 0; i < Content.transform.childCount; i++)
        {
            if(Content.transform.GetChild(i).gameObject.name == "SelectSkinButton " + "(" + i + ")")
            {
                btn = Content.transform.GetChild(i).GetComponent<Button>();
                btn.interactable = false;
                Destroy(btn.gameObject.transform.GetChild(0).gameObject);                
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
        //Character.transform.GetComponent<RuntimeAnimatorController>()
    }

    public void ChangeSkin()
    {
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
