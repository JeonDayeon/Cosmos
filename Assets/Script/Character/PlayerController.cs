using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;
    float axisH = 0.0f;
    public float speed = 0.0f;
    public float Walkspeed = 3.0f;
    public float Runspeed = 9.0f;
    bool isRun;
    //---------------------------------------------------점프
    public float Fjump = 4.0f;//저장용
    public float jumpTem = 0.0f;//실행용
    public LayerMask groundLayer;
    bool goJump = false;
    public bool OnGround = false;
    bool TwoJump = false;
    //---------------------------------------------------애니메이션
    Animator animator;
    public string IdleAnime = "Idle";
    public string WalkAnime = "Walk";
    public string RunAnime = "Run";
    public string JumpAnime = "Jump";
    public string TwoJumpAnime = "TwoJump";
    string nowAnime = " ";
    string oldAnime = " ";
    //---------------------------------------------------데미지(게임오버)
    public int Chance;
    public int ChanceCount;
    public GameObject gameover;
    public TextMeshProUGUI Hp;
    //---------------------------------------------------Goal
    public GameObject goal;

    GameManager game;
    // Start is called before the first frame update
    void Start()
    {
        //동작
        rbody = this.GetComponent<Rigidbody2D>();
        jumpTem = Fjump;

        //애니메이션
        animator = GetComponent<Animator>();
        nowAnime = IdleAnime;
        oldAnime = IdleAnime;

        speed = Walkspeed;

        //게임오버
        ChanceCount = Chance;
        Hp = GameObject.Find("HP").transform.Find("HpTxt").GetComponent<TextMeshProUGUI>();

        //게임성공, 게임오버 시 추출할 UI
        goal = GameObject.Find("GameUI").transform.Find("Goal").gameObject;
        gameover = GameObject.Find("GameUI").transform.Find("GameOver").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        axisH = Input.GetAxisRaw("Horizontal");
        isRun = Input.GetKey(KeyCode.Z);

        if (isRun)
        {
            speed = Runspeed;
        }

        else
            speed = Walkspeed;

        //방향
        if (axisH > 0.0f)
        {
            //오른쪽
            transform.localScale = new Vector2(1, 1);
        }

        else if (axisH < 0.0f)
        {
            //왼쪽
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
            //지면 위 or 속도가 0이 아닐 때
            rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);
        }
        
        if(OnGround && goJump)
        {
            //지면 위 + 점프키
            Jumpmov(jumpTem);
            TwoJump = true;
        }

        else if(!OnGround && goJump && TwoJump)
        {
            //2단 공중 + 점프키
            Jumpmov(jumpTem/20);
            TwoJump = false;
        }

        if (OnGround)
        {
            //지면 위
            if (axisH == 0)
            {
                nowAnime = IdleAnime; //정지
            }
            else
            {
                if (isRun)
                {
                    nowAnime = RunAnime;
                }
                else
                {
                    nowAnime = WalkAnime; //이동
                }
            }
            Debug.Log(nowAnime);
        }

        
        //공중
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
    }

    public void Jumpmov(float speed)
    {
        Vector2 jumpPw = new Vector2(0, jumpTem);
        rbody.AddForce(jumpPw, ForceMode2D.Impulse);
        goJump = false;
    }

    public void Damage()
    {

        ChanceCount--;
        rbody.AddForce(
            new Vector2(rbody.transform.localScale.x * -2f, rbody.transform.localScale.y * 1f) * 6f, ForceMode2D.Impulse);

        Hp.text = ChanceCount.ToString();
        if (ChanceCount == 0)
        {
            GameOver();
        }
    }
    public void GameOver()
    {
        Time.timeScale = 0;
        gameover.SetActive(true);
    }

    public void Goal()
    {
        Time.timeScale = 0;
        goal.SetActive(true);
    }
}
