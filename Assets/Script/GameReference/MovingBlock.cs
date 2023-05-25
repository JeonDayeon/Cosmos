using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public float moveX = 0.0f; //X�̵��Ÿ�
    public float moveY = 0.0f; //Y�̵��Ÿ�
    public float times = 0.0f; //�ð�
    public float weight = 0.0f; //���� �ð�
    public bool isMoveWhenOn = false; //�ö��� �� �����̱�

    public bool isCanMove = true; //������
    float perDX;    //1�����Ӵ� x�̵� ��
    float perDY;    //1�����Ӵ� y�̵� ��
    Vector3 defPos; //�ʱ� ��ġ
    bool isReverse = false; //���� ����
    // Start is called before the first frame update
    void Start()
    {
        defPos = transform.position; //�ʱ� ��ġ
        float timestep = Time.fixedDeltaTime; // 1�����ӿ� �̵��ϴ� �ð�
        perDX = moveX / (1.0f / timestep * times); //1������ X�� �̵���
        perDY = moveY / (1.0f / timestep * times); //1������ Y�� �̵���

        if (isMoveWhenOn)  //ó������ �������� �ʰ� �ö󰡸� �����̱� ����
        {
            isCanMove = false;
        }
    }
    private void FixedUpdate()
    {
        if (isCanMove)
        {
            //�̵� ��
            float x = transform.position.x;
            float y = transform.position.y;
            bool endX = false;
            bool endY = false;

            if (isReverse)
            {
                //�ݴ� �������� �̵�
                //�̵��� = ��� && �̵� ��ġ�� �ʱ� ��ġ���� ���� ���
                //�̵��� = ���� && �̵� ��ġ�� �ʱ� ��ġ���� ū ���
                if ((perDX >= 0.0f && x <= defPos.x) || (perDX < 0.0f && x >= defPos.x))
                {
                    //�̵��� +
                    endX = true; //X���� �̵� ����
                }

                if ((perDY >= 0.0f && y <= defPos.y) || (perDY < 0.0f && y >= defPos.y))
                {
                    endY = true; //y���� �̵� ����
                }
                //��� �̵�
                transform.Translate(new Vector3(-perDX, -perDY, defPos.z));
            }

            else
            {
                //���������� �̵�
                //�̵��� = ��� && �̵� ��ġ�� �ʱ� ��ġ���� ū ���
                //�̵��� = ���� && �̵� ��ġ�� �ʱ� + �̵��Ÿ� ���� ���� ���
                if ((perDX >= 0.0f && x >= defPos.x + moveX) || (perDX < 0.0f && x <= defPos.x + moveX))
                {
                    endX = true; //X���� �̵� ����
                }
                if ((perDY >= 0.0f && y >= defPos.y + moveY) || (perDY < 0.0f && y <= defPos.y + moveY))
                {
                    endY = true; //Y���� �̵� ����
                }
                //��� �̵�
                Vector3 v = new Vector3(perDX, perDY, defPos.z);
                transform.Translate(v);
            }

            if (endX && endY)
            {
                //�̵� ����

                if (isReverse) //��ġ�� ��߳��� ���� �����ϰ��� ���ư��� ���� �ʱ� ��ġ�� ������
                {
                    transform.position = defPos;
                }
                isReverse = !isReverse; //�� ����
                isCanMove = false; //�̵� ���� ���� false
                if (isMoveWhenOn == false)
                {
                    //�ö��� �� ���� ���� ���
                    Invoke("Move", weight); //weight��ŭ ���� �� �ٽ� �̵�
                }
            }
        }
    }
    //�̵��ϰ� �����
    public void Move()
    {
        isCanMove = true;
    }

    //�̵����� ���ϰ� �����
    public void Stop()
    {
        isCanMove = false;
    }

    //���� ����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //������ ���� �÷��̾�� �ڼ����� �����
            collision.transform.SetParent(transform);
            if(isMoveWhenOn)
            {
                //�ö��� �� �����̴� ����
                isCanMove = true; //�̵��ϰ� �����
            }
        }
    }

    //���� ����
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.transform.SetParent(null);
    }
}

