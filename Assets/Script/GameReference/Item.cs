using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Heart,
    NoDamage,
    BackTimer,
}

public class Item : MonoBehaviour
{
    //---------------------------------------------------아이템 종류
    public ItemType type;

    //---------------------------------------------------아이템 종류에 따른 이미지
    public Sprite HeartImg;
    public Sprite NoDmgImg;
    public Sprite BackTimeImg;
    //----------------------------------------------------관련 오브젝트 연결
    public PlayerController player;
    public TimeManager time;
    //----------------------------------------------------아이템 종류에 따른 이미지 교체를 위함
    SpriteRenderer spriterenderer;
    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.Find("Player").GetComponent<PlayerController>();
        player = FindObjectOfType<PlayerController>();
        time = GameObject.Find("GameUI").transform.Find("GameTimer").GetComponent<TimeManager>();
        spriterenderer = gameObject.GetComponent<SpriteRenderer>();

        switch (type)
        {
            case ItemType.Heart:
                spriterenderer.sprite = HeartImg;
                break;

            case ItemType.NoDamage:
                spriterenderer.sprite = NoDmgImg;
                break;

            case ItemType.BackTimer:
                spriterenderer.sprite = BackTimeImg;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("굿");
            switch (type)
            {
                case ItemType.Heart:
                    player.Healing();
                    break;

                case ItemType.NoDamage:
                    Debug.Log("들어감");
                    player.Star.SetActive(true);
                    Debug.Log("도는 중" + player.isCorutin);
                    if (player.isCorutin)//코루틴 중복시 생기는 오류 막기
                    {
                        player.StopCoroutine(player.NodamageEffect(15, "Damage"));
                        player.isCorutin = false;
                        Debug.Log("도멈" + player.isCorutin);
                    }
                    StartCoroutine(Nodamage());
                    break;

                case ItemType.BackTimer:
                    time.BackTime(5f);
                    break;
            }
        }
        Destroy(gameObject);
    }
    
    IEnumerator Nodamage()
    {
        Debug.Log("도시");
        yield return player.StartCoroutine(player.NodamageEffect(30, "Item"));
        StopCoroutine(Nodamage());  
    }
}
