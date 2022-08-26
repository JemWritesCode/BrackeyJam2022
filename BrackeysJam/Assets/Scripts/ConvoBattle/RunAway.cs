using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunAway : MonoBehaviour
{
    SceneManagement sceneManagement;

    public void RunAwayEndBattle()
    {
        GameObject sceneManager = GameObject.Find("SceneManager");
        sceneManagement = sceneManager.GetComponent<SceneManagement>();
        sceneManagement.EndBattle();
    }
}
