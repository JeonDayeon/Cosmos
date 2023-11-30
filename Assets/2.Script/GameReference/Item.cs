using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Heart,
    NoDamage,
    BackTimer,
    Quest,
}

public class Item : MonoBehaviour
{
    //---------------------------------------------------������ ����
    public ItemType type;

    //---------------------------------------------------������ ������ ���� �̹���
    public Sprite HeartImg;
    public Sprite NoDmgImg;
    public Sprite BackTimeImg;
    public Sprite QuestItemImg;
    //----------------------------------------------------���� ������Ʈ ����
    public PlayerController player;
    public TimeManager time;
    //----------------------------------------------------������ ������ ���� �̹��� ��ü�� ����
    SpriteRenderer spriterenderer;
    //----------------------------------------------------�����
    public AudioSource audiosource;
    public AudioClip ItemAudio;
    public AudioClip NoDamageAudio;
    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.Find("Player").GetComponent<PlayerController>();
        player = FindObjectOfType<PlayerController>();
        time = GameObject.Find("GameUI").transform.Find("GameTimer").GetComponent<TimeManager>();
        spriterenderer = gameObject.GetComponent<SpriteRenderer>();
        audiosource = GameObject.Find("Effect").GetComponent<AudioSource>();
        audiosource.clip = ItemAudio;
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

            case ItemType.Quest:
                spriterenderer.sprite = QuestItemImg;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audiosource.clip = ItemAudio;
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("��");
            switch (type)
            {
                case ItemType.Heart:
                    player.Healing();
                    break;

                case ItemType.NoDamage:
                    audiosource.clip = NoDamageAudio;
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

                case ItemType.Quest:
                    audiosource.clip = ItemAudio;
                    player.GetQuestItem();
                    break;
            }
            audiosource.Play();
            Destroy(gameObject);
        }
    }
    
    IEnumerator Nodamage()
    {
        audiosource.Play();
        Debug.Log("����");
        yield return player.StartCoroutine(player.NodamageEffect(30, "Item"));
        StopCoroutine(Nodamage());  
    }
}
