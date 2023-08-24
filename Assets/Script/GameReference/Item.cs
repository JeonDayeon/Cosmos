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
    //---------------------------------------------------������ ����
    public ItemType type;

    //---------------------------------------------------������ ������ ���� �̹���
    public Sprite HeartImg;
    public Sprite NoDmgImg;
    public Sprite BackTimeImg;
    //----------------------------------------------------���� ������Ʈ ����
    public PlayerController player;
    public TimeManager time;
    //----------------------------------------------------������ ������ ���� �̹��� ��ü�� ����
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
            Debug.Log("��");
            switch (type)
            {
                case ItemType.Heart:
                    player.Healing();
                    break;

                case ItemType.NoDamage:
                    Debug.Log("��");
                    player.Star.SetActive(true);
                    Debug.Log("���� ��" + player.isCorutin);
                    if (player.isCorutin)//�ڷ�ƾ �ߺ��� ����� ���� ����
                    {
                        player.StopCoroutine(player.NodamageEffect(15, "Damage"));
                        player.isCorutin = false;
                        Debug.Log("����" + player.isCorutin);
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
        Debug.Log("����");
        yield return player.StartCoroutine(player.NodamageEffect(30, "Item"));
        StopCoroutine(Nodamage());  
    }
}
