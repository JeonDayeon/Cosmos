using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerData : MonoBehaviour
{
    public static PlayerData instance = null;//�̱���

    public string SkinName;//ĳ���� ��Ų �̸�

    //public GameObject Player;
    private void Awake()
    {
        if (instance == null) //instance�� null. ��, �ý��ۻ� �����ϰ� ���� ������
        {
            instance = this; //���ڽ��� instance�� �־��ݴϴ�.
            DontDestroyOnLoad(gameObject); //OnLoad(���� �ε� �Ǿ�����) �ڽ��� �ı����� �ʰ� ����
        }
        else
        {
            if (instance != this) //instance�� ���� �ƴ϶�� �̹� instance�� �ϳ� �����ϰ� �ִٴ� �ǹ�
                Destroy(this.gameObject); //�� �̻� �����ϸ� �ȵǴ� ��ü�̴� ��� AWake�� �ڽ��� ����
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        if (SkinName == null)
        {
            SkinName = "Cosmos";
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
