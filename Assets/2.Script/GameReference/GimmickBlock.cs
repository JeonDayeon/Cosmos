using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickBlock : MonoBehaviour
{
    Rigidbody2D rbody; //물리
    public float length = 1.5f; // 자동 낙하 탐지 거리
    public bool isDelete = false; // 낙하 후 제거할지 여부

    bool isFell = false; //낙하 플래그
    float fadeTime = 0.5f; //페이드 아웃 시간

    float d;

    public AudioSource audios;
    public AudioClip audioc;
    // Start is called before the first frame update
    void Start()
    {
        audios = GetComponent<AudioSource>();
        audios.clip = audioc;

        //RigidBody2D 물리 시뮬레이션 정지
        rbody = GetComponent<Rigidbody2D>();
        rbody.bodyType = RigidbodyType2D.Static;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); //플레이어 찾기
        if(player != null)//플레이어가 존재한다면
        {
            //플레이어와의 거리 계산
            d = Vector2.Distance(transform.position, player.transform.position);
            Debug.Log("============="+ d);
            if (length >= d) //탐지거리가 플레이어와의 거리보다 같거나 멀다면
            {
                Debug.Log("=============가보자고고고고" + d);
                if (rbody.bodyType == RigidbodyType2D.Static)
                {
                    rbody.bodyType = RigidbodyType2D.Dynamic; // 물리시뮬레이션 시작
                    audios.Play();
                    Debug.Log("떨어져라");
                }
            }
            if(isFell)
            {
                //낙하
                // 투명도 변경으로 페이드아웃 효과 주기
                fadeTime -= Time.deltaTime;// 이전 프레임과의 차이만큼 시간 차감
                Color col = GetComponent<SpriteRenderer>().color; // 원 컬러값 가져와 저장
                col.a = fadeTime; //투명도 변경
                GetComponent<SpriteRenderer>().color = col; //컬러값 재설정
                gameObject.tag = "Untagged"; //태그 변경으로 데미지 1번 이상 받지 않게 하기
                if (fadeTime <= 0.0f)
                {
                    //0보다 작으면 제거
                    Destroy(gameObject);
                }
            }

        }
    }

    //블록과 접촉하면
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDelete)
        {
            isFell = true; //낙하 플래그 트루로
        }
    }

    void AudioStop()
    {
        audios.Stop();
    }
}
