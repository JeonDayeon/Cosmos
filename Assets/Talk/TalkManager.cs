using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    List<Dictionary<string, object>> text;
    public CSVReader CSVReader;

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

    public string GetTalk(int Tid, int talkindex)
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
            return ((string)text[talkindex]["Content"]);
        }

    }
}
