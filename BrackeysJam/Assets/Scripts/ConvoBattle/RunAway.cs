using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunAway : MonoBehaviour
{
    public void RunAwayEndBattle()
    {
        SceneManager.LoadScene("1-MainGame");
        Debug.Log("Loading 1-MainGame...");
        // isInBattle = false;
    }
}
