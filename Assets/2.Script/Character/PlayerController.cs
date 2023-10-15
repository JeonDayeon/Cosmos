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
    public LayerMask DamageLayer;
    bool goJump = false;
    public bool OnGround = false;
    public bool OnDamage = false;
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
    //---------------------------------------------------스킨
    [SerializeField]
    Skins[] skin;
    public string SkinName = "Cosmos";
    public PlayerData data;
    //---------------------------------------------------데미지(게임오버)
    public int Chance;
    public int ChanceCount;
    public GameObject gameover;
    public TextMeshProUGUI Hp;
    public SpriteRenderer spriterender;
    //---------------------------------------------------Goal
    public GameObject goal;
    public GameManager gameManager;//골인 후 스토리 보여주기 위함
    //---------------------------------------------------노데미지 효과
    public GameObject Star;
    public bool isCorutin;//코루틴 동작 확인
    //---------------------------------------------------퀘스트
    public GameObject QuestPanel;
    public TextMeshProUGUI QuestText;
    public string QuestItemName;
    public int QuestItemNumber;
    public int HaveItemNum;
    //--------------------------------------------------세이브포인트
    public GameObject[] SavePoint;


    // Start is called before the first frame update
    void Start()
    {
        //스킨
        data = FindObjectOfType<PlayerData>();
        
        if (data != null && SkinName != data.SkinName)
        {
            SkinName = data.SkinName;
            ChangeSkins();
        }
        
        //동작
        rbody = this.GetComponent<Rigidbody2D>();
        jumpTem = Fjump;

        //애니메이션
        animator = GetComponent<Animator>();
        nowAnime = IdleAnime;
        oldAnime = IdleAnime;

        //게임매니저
        gameManager = FindObjectOfType<GameManager>();

        speed = Walkspeed;

        //게임 데미지
        spriterender = gameObject.GetComponent<SpriteRenderer>();

        //게임오버
        ChanceCount = Chance;
        Hp = GameObject.Find("HP").transform.Find("HpTxt").GetComponent<TextMeshProUGUI>();

        //게임성공, 게임오버 시 추출할 UI
        goal = GameObject.Find("GameUI").transform.Find("Goal").gameObject;
        gameover = GameObject.Find("GameUI").transform.Find("GameOver").gameObject;

        //스타 소환
        Star = gameObject.transform.GetChild(1).gameObject;
        Hp.text = ChanceCount.ToString();

        //퀘스트 판넬 켜져있을 때만 퀘스트 보이도록
        QuestPanel = GameObject.Find("QuestPanel");
        if (QuestPanel != null)
        {
            QuestText = QuestPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            QuestText.text = QuestItemName + " 수집 (" + HaveItemNum + "/" + QuestItemNumber + ")";
        }

        //떨어졌을 시 가장 가까운 세이브 포이트로 이동하게
        GameObject saves;
        saves = GameObject.Find("Save");

        SavePoint = new GameObject[saves.transform.childCount];
        for(int i = 0; i < saves.transform.childCount; i++)
        {
            SavePoint[i] = saves.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        axisH = Input.GetAxisRaw("Horizontal");
        isRun = Input.GetKey(KeyCode.Z);

        if (Input.GetKeyDown(KeyCode.T))
        {
            Healing();
        }

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
    }

    private void FixedUpdate()
    {
        OnGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer);
        OnDamage = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), DamageLayer);
        if (OnGround || OnDamage || axisH != 0)
        {
            //지면 위 or 속도가 0이 아닐 때
            rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);
        }

        if ((OnGround||OnDamage) && goJump)
        {
            //지면 위 + 점프키
            Jumpmov(jumpTem);
            TwoJump = true;
        }

        else if ((!OnGround||!OnDamage) && goJump && TwoJump)
        {
            //2단 공중 + 점프키
            Jumpmov(jumpTem / 20);
            TwoJump = false;
        }

        if (OnGround || OnDamage)
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
            int ClosePoint;
            ClosePoint = 0;

            for (int i = 0; i < SavePoint.Length; i++)
            {
                float CloseVec = Vector2.Distance(gameObject.transform.position, SavePoint[ClosePoint].transform.position);
                float iArrVec = Vector2.Distance(gameObject.transform.position, SavePoint[i].transform.position);

                ClosePoint = CloseVec >= iArrVec ? i : ClosePoint;
            }

            if (ChanceCount != 0)
            {
                gameObject.transform.position = SavePoint[ClosePoint].transform.position;
                Damage(collision.transform.position, true);
            }
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
            Damage(collision.transform.position, false);
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

    public void Damage(Vector2 targetPos, bool isOut)
    {

        ChanceCount--;

        if (!isOut)
        {
            int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
            rbody.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);
        }
        StartCoroutine(NodamageEffect(15, "damage"));
        Hp.text = ChanceCount.ToString();

        if (ChanceCount == 0)
        {
            GameOver();
        }
    }

    public void Healing()
    {
        ChanceCount++;
        Hp.text = ChanceCount.ToString();
    }

    public IEnumerator NodamageEffect(int time, string type)
    {
        isCorutin = true;
        gameObject.layer = 11;

        int count = 0;
        while(count < time)
        {
            if(gameObject.layer != 11)
            {
                gameObject.layer = 11;
            }

            float fadeCnt = 0;
            while (fadeCnt < 1.0f)
            {
                fadeCnt += 0.1f;
                yield return new WaitForSeconds(0.01f);
                switch (type)
                {
                    case "damage":
                        spriterender.color = new Color(1, 1, 1, fadeCnt);
                        break;
                    case "Item":
                        Star.SetActive(true);
                        spriterender.color = new Color(0.9787856f, 1, 0.5603774f, 1);
                        break;
                }
            }

            while(fadeCnt < 0.6f)
            {
                fadeCnt -= 0.01f;
                yield return new WaitForSeconds(0.01f);
                switch (type)
                {
                    case "damage":
                        spriterender.color = new Color(1, 1, 1, fadeCnt);
                        break;
                    case "Item":
                        break;
                }
            }
            count++;
        }
        count = 0;
        spriterender.color = new Color(1, 1, 1, 1);
        if (Star.activeSelf&&type == "Item")
        {
            Star.SetActive(false);
        }
        gameObject.layer = 0;
        isCorutin = true;
        StopCoroutine(NodamageEffect(time, type));
    }

    
    public void GameOver()
    {
        Time.timeScale = 0;
        gameover.SetActive(true);
    }

    public void Goal()
    {
        if (QuestPanel != null)
        {
            if(HaveItemNum >= QuestItemNumber)
            {
                Time.timeScale = 0;
                gameManager.talkid++;
                gameManager.isTalk = true;
                gameManager.Talk();
            }

            else if(HaveItemNum < QuestItemNumber)
            {
                Time.timeScale = 0;
                gameManager.isTalk = true;
                gameManager.Talk();
            }
        }

        else
        {
            Time.timeScale = 0;
            gameManager.isTalk = true;
            gameManager.Talk();
        }
    }

    public void GetQuestItem()
    {
        HaveItemNum++;
        QuestText.text = QuestItemName + " 수집 (" + HaveItemNum + "/" + QuestItemNumber + ")";
    }

    public void ChangeSkins()
    {
        Animator PAnimator = gameObject.GetComponent<Animator>();
        for (int i = 0; i < skin.Length; i++)
        {
            if (skin[i].name.ToString() == SkinName)
            {
                PAnimator.runtimeAnimatorController = skin[i].animator;
                name = skin[i].name.ToString();
                break;
            }
        }
    }
}
