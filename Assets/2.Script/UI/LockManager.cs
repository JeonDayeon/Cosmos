using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct ChapterLock
{
    public GameObject ChapterBtn;

    public int MaxStage;
    public int Unlock;
}


public class LockManager : MonoBehaviour
{
    [SerializeField]
    ChapterLock[] ChapterLocks;

    public GameObject UnlockPrefab;
    public GameObject[] Buttons;

    public int Chapter;
    public int Unlock;
    public bool Manager;

    public LockManager ChapBtn;
    // Start is called before the first frame update
    void Start()
    {
        UnlockPrefab = Resources.Load("Prefab/LockStageBox") as GameObject;
        if(Manager == true)
        {
            for(int i = 0; i < 2; i++)
            {
                ChapterLocks[i].ChapterBtn = GameObject.Find("Chapter"+ (i + 1));
            }
            UnLockSetting();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Manager == true && ChapterLocks[0].ChapterBtn == null)
        {
            for (int i = 0; i < 2; i++)
            {
                ChapterLocks[i].ChapterBtn = GameObject.Find("Chapter" + (i + 1));
            }
        }

        if(Manager == true && ChapterLocks[0].ChapterBtn != null)
        {
            UnLockSetting();
        }
    }

    public void Setting()
    {
        GameObject Button = GameObject.Find("Buttons");
        Buttons = new GameObject[Button.transform.childCount];

        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i] = Button.transform.GetChild(i).gameObject;
            if(i + 1 > Unlock)
            {
                GameObject obj = Instantiate(UnlockPrefab, Buttons[i].transform.position, Quaternion.identity, Buttons[i].transform);
            }
        }
    }
    public void UnLock(int Chapter, int unlockN)
    {
        if(ChapterLocks[Chapter].Unlock < unlockN)
            ChapterLocks[Chapter].Unlock = unlockN;
    }

    public void UnLockSetting()
    {
        for (int i = 0; i < ChapterLocks.Length; i++)
        {
            ChapBtn = ChapterLocks[i].ChapterBtn.GetComponent<LockManager>();
            ChapBtn.Unlock = ChapterLocks[i].Unlock;
        }
    }
}
