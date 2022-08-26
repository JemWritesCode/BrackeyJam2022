using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public bool isInBattle;
    public Extrovert extrovertThatStartedBattle;

    void Start()
    {
            Extrovert.OnGuardHasSpottedPlayer += StartBattle;
    }

    void StartBattle(Extrovert extrovert)
    {
        if (!isInBattle)
        {
            extrovertThatStartedBattle = extrovert;
            string battleSceneName = "2-ConversationBattle";
            isInBattle = true;
            //TODO Pause the Game
            SceneManager.LoadScene(battleSceneName, LoadSceneMode.Additive);
        }
    }

    public void EndBattle()
    {
        isInBattle = false;
        //isOnBattleCooldown = true;
        //TODO Resume the Game
        SceneManager.UnloadSceneAsync("2-ConversationBattle");
    }

}
