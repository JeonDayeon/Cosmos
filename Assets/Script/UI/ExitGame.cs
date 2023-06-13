using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public bool isMain = false;
    public GameObject ExitUI;

    private void Start()
    {
        ExitUI = transform.Find("ExitMenu").gameObject;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ExitUI.activeSelf)
            {
                ExitUI.SetActive(false);
            }

            else if(!ExitUI.activeSelf)
            {
                Debug.Log("TTTTTTTTTTRRRRRRRRRRRRRRRRRRRUUUUUUUUUUUUUUUEEEEEEEEEEEEEEE");
                ExitUI.SetActive(true);
            }
        }
    }
    public void Exit()
    {
        Application.Quit();
    }
}
