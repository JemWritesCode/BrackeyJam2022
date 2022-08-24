using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public bool isInBattle = false;
    // Start is called before the first frame update
    void Start()
    {
            Extrovert.OnGuardHasSpottedPlayer += StartBattle;
    }

    void StartBattle()
    {
        if (!isInBattle)
        {
            string battleSceneName = "2-ConversationBattle";
            Debug.Log("sceneName to load:" + battleSceneName);
            SceneManager.LoadScene(battleSceneName);
        }
    }

    void EndBattle()
    {
        isInBattle = false;
    }

}
