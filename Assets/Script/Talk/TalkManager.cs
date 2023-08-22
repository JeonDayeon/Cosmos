using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct Emotions
{
    public enum Cmotion
    {
        Basic,
        Talk,
        Upset
    }
    public Cmotion motion;
    public Sprite image;
}

[System.Serializable]
struct portraitimage
{
    public enum Character
    {
        �ڵ�,
        �׺��,
        ���
    }

    public Character character;

    [SerializeField]
    public Emotions[] emotions;
}
public class TalkManager : MonoBehaviour
{
    List<Dictionary<string, object>> text;
    public CSVReader CSVReader;

    [SerializeField]
    portraitimage[] portraitimageArr;
    public int id;
    // Start is called before the first frame update
    void Start()
    {
        GenerateData();
    }

    void GenerateData()
    {
        if (id == 100)
        {
            text = CSVReader.Read("Chapter1-1");
        }
        Debug.Log("���׷���Ʈ ���������׷���Ʈ ���������׷���Ʈ ���������׷���Ʈ ������");

    }

    public string GetTalk(int Tid, int talkindex, string typeName)//�Ѱ� �� �����͸� �̱� ���� ���ڿ��� ���ϴ� ���� ����
    {
        id = Tid;
        Debug.Log("����������������������������������������������������������");
        GenerateData();

        if (talkindex == text.Count)
        {
            return null;
        }
        
        else
        {
            return ((string)text[talkindex][typeName]);
        }

    }

    public Sprite GetPortrait(string name, string emotion)
    {
        int index = 0;

        for(int i = 0; i < portraitimageArr.Length; i++)
        {
            if (portraitimageArr[i].character.ToString() == name)
            {
                index = i;
                break;
            }
        }

        for(int j = 0; j < portraitimageArr[index].emotions.Length; j++)
        {
            if (portraitimageArr[index].emotions[j].motion.ToString() == emotion)
            {
                return (portraitimageArr[index].emotions[j].image);
            }
        }

        return(null);
    }
}
