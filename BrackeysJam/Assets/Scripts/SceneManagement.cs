using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public bool isInBattle;
    void Start()
    {
            Extrovert.OnGuardHasSpottedPlayer += StartBattle;
    }

    void StartBattle()
    {
        if (!isInBattle) //if you're not already in battle, then load the battle scene
        {
            string battleSceneName = "2-ConversationBattle";
            isInBattle = true;
            SceneManager.LoadScene(battleSceneName, LoadSceneMode.Additive);
        }
    }


}
