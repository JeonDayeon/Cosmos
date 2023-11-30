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
        Upset,
        Smile
    }
    public Cmotion motion;
    public Sprite image;
}

[System.Serializable]
struct portraitimage
{
    public enum Character
    {
        ƒ⁄µ,
        ≥◊∫Ê∂Û,
        ≥ÎπŸ,
        ¡÷∏,
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
        switch(id)
        {
            case 100:
                text =  CSVReader.Read("Chapter1-1");
                break;
            case 101:
                text = CSVReader.Read("Chapter1-2");
                break;
            case 200:
                text = CSVReader.Read("Chapter2-1");
                break;
            case 201:
                text = CSVReader.Read("Chapter2-1Not");
                break;
            case 202:
                text = CSVReader.Read("Chapter2-1Ok");
                break;
            case 220:
                text = CSVReader.Read("Chapter2-2");
                break;
            case 221:
                text = CSVReader.Read("Chapter2-2_Suc");
                break;
        }
        Debug.Log("¡¶≥◊∑π¿Ã∆Æ µ•¿Ã≈Õ¡¶≥◊∑π¿Ã∆Æ µ•¿Ã≈Õ¡¶≥◊∑π¿Ã∆Æ µ•¿Ã≈Õ¡¶≥◊∑π¿Ã∆Æ µ•¿Ã≈Õ");

    }

    public string GetTalk(int Tid, int talkindex, string typeName)//≥—∞‹ ¡Ÿ µ•¿Ã≈Õ∏¶ ªÃ±‚ ¿ß«ÿ πÆ¿⁄ø≠∑Œ ø¯«œ¥¬ ∞Õ¿ª πﬁ¿Ω
    {
        id = Tid;
        Debug.Log("∞Ÿ≈Â∞Ÿ≈Â∞Ÿ≈Â∞Ÿ≈Â∞Ÿ≈Â∞Ÿ≈Â∞Ÿ≈Â∞Ÿ≈Â∞Ÿ≈Â∞Ÿ≈Â∞Ÿ≈Â∞Ÿ≈Â∞Ÿ≈Â∞Ÿ≈Â∞Ÿ≈Â∞Ÿ≈Â∞Ÿ≈Â∞Ÿ≈Â∞Ÿ≈Â");
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
