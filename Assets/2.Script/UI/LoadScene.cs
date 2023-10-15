using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{
    public string stageID;

    public void LoadStage()
    {
        SceneManager.LoadScene(stageID, LoadSceneMode.Single);
    }

    public void ReLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}
