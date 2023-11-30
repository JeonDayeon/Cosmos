using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickBlock : MonoBehaviour
{
    Rigidbody2D rbody; //����
    public float length = 1.5f; // �ڵ� ���� Ž�� �Ÿ�
    public bool isDelete = false; // ���� �� �������� ����

    bool isFell = false; //���� �÷���
    float fadeTime = 0.5f; //���̵� �ƿ� �ð�

    float d;

    public AudioSource audios;
    public AudioClip audioc;
    // Start is called before the first frame update
    void Start()
    {
        audios = GetComponent<AudioSource>();
        audios.clip = audioc;

        //RigidBody2D ���� �ùķ��̼� ����
        rbody = GetComponent<Rigidbody2D>();
        rbody.bodyType = RigidbodyType2D.Static;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); //�÷��̾� ã��
        if(player != null)//�÷��̾ �����Ѵٸ�
        {
            //�÷��̾���� �Ÿ� ���
            d = Vector2.Distance(transform.position, player.transform.position);
            Debug.Log("============="+ d);
            if (length >= d) //Ž���Ÿ��� �÷��̾���� �Ÿ����� ���ų� �ִٸ�
            {
                Debug.Log("=============�����ڰ����" + d);
                if (rbody.bodyType == RigidbodyType2D.Static)
                {
                    rbody.bodyType = RigidbodyType2D.Dynamic; // �����ùķ��̼� ����
                    audios.Play();
                    Debug.Log("��������");
                }
            }
            if(isFell)
            {
                //����
                // ���� �������� ���̵�ƿ� ȿ�� �ֱ�
                fadeTime -= Time.deltaTime;// ���� �����Ӱ��� ���̸�ŭ �ð� ����
                Color col = GetComponent<SpriteRenderer>().color; // �� �÷��� ������ ����
                col.a = fadeTime; //���� ����
                GetComponent<SpriteRenderer>().color = col; //�÷��� �缳��
                gameObject.tag = "Untagged"; //�±� �������� ������ 1�� �̻� ���� �ʰ� �ϱ�
                if (fadeTime <= 0.0f)
                {
                    //0���� ������ ����
                    Destroy(gameObject);
                }
            }

        }
    }

    //��ϰ� �����ϸ�
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDelete)
        {
            isFell = true; //���� �÷��� Ʈ���
        }
    }

    void AudioStop()
    {
        audios.Stop();
    }
}
