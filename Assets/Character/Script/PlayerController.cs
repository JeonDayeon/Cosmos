using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;
    float axisH = 0.0f;
    public float speed = 0.0f;
    public float Walkspeed = 3.0f;
    public float Runspeed = 9.0f;
    bool isRun;
    //---------------------------------------------------����
    public float Fjump = 4.0f;//�����
    public float jumpTem = 0.0f;//�����
    public LayerMask groundLayer;
    bool goJump = false;
    public bool OnGround = false;
    bool TwoJump = false;
    //---------------------------------------------------�ִϸ��̼�
    Animator animator;
    public string IdleAnime = "Idle";
    public string WalkAnime = "Walk";
    public string RunAnime = "Run";
    public string JumpAnime = "Jump";
    public string TwoJumpAnime = "TwoJump";
    string nowAnime = " ";
    string oldAnime = " ";
    //---------------------------------------------------������(���ӿ���)
    public int Chance;
    public int ChanceCount;
    public GameObject test;
    private Text Hp;
    //---------------------------------------------------Goal
    public GameObject goal;

    // Start is called before the first frame update
    void Start()
    {
        //����
        rbody = this.GetComponent<Rigidbody2D>();
        jumpTem = Fjump;

        //�ִϸ��̼�
        animator = GetComponent<Animator>();
        nowAnime = IdleAnime;
        oldAnime = IdleAnime;

        speed = Walkspeed;

        //���ӿ���
        ChanceCount = Chance;
        Hp = GameObject.Find("HpTxt").GetComponent<Text>();


    }

    // Update is called once per frame
    void Update()
    {
        axisH = Input.GetAxisRaw("Horizontal");
        isRun = Input.GetKey(KeyCode.Z);

        if (isRun)
        {
            speed = Runspeed;
            Debug.Log("====================================== ��");
        }

        else
            speed = Walkspeed;

        //����
        if (axisH > 0.0f)
        {
            //������
            Debug.Log("������");
            transform.localScale = new Vector2(1, 1);
        }

        else if (axisH < 0.0f)
        {
            //����
            Debug.Log("����");
            transform.localScale = new Vector2(-1, 1);
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        Hp.text = ChanceCount.ToString();
    }

    private void FixedUpdate()
    {
        OnGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer);

        if(OnGround || axisH != 0)
        {
            //���� �� or �ӵ��� 0�� �ƴ� ��
            rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);
        }
        
        if(OnGround && goJump)
        {
            //���� �� + ����Ű
            Jumpmov(jumpTem);
            TwoJump = true;
        }

        else if(!OnGround && goJump && TwoJump)
        {
            //2�� ���� + ����Ű
            Jumpmov(jumpTem/20);
            TwoJump = false;
        }

        if (OnGround)
        {
            //���� ��
            if (axisH == 0)
            {
                nowAnime = IdleAnime; //����
            }
            else
            {
                if (isRun)
                {
                    nowAnime = RunAnime;
                }
                else
                {
                    nowAnime = WalkAnime; //�̵�
                }
            }
            Debug.Log(nowAnime);
        }

        
        //����
        else
        {
            if (TwoJump)
            {
                nowAnime = TwoJumpAnime;
            }
            else
            {
                nowAnime = JumpAnime;
            }
        }

        if (nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            animator.Play(nowAnime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Out"))
        {
            GameOver();
        }
        if (collision.gameObject.CompareTag("Goal"))
        {
            Goal();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Demage"))
        {
            Damage();
        }
    }

    public void Jump()
    {
        goJump = true;
        Debug.Log("����");
    }

    public void Jumpmov(float speed)
    {
        Debug.Log("�����ϱ�");
        Vector2 jumpPw = new Vector2(0, jumpTem);
        rbody.AddForce(jumpPw, ForceMode2D.Impulse);
        goJump = false;
    }

    public void Damage()
    {
        Debug.Log("=======================������");
        ChanceCount--;
        Hp.text = ChanceCount.ToString();
        if (ChanceCount == 0)
        {
            GameOver();
        }
    }
    public void GameOver()
    {
        Time.timeScale = 0;
        Debug.Log("=======================�ƿ�");
        test.SetActive(true);
    }

    public void Goal()
    {
        Time.timeScale = 0;
        goal.SetActive(true);
    }
}
