using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    bool IsAnyKeyStart = false;
    void Start()
    {
        Invoke("AnyStart", 2f);
    }

    void Update()
    {
        if(Input.anyKeyDown&&IsAnyKeyStart)
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
    }

    public void AnyStart()
    {
        IsAnyKeyStart = true;
    }
}
