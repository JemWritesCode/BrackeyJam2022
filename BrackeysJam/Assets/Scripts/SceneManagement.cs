using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public bool isInBattle;
    public float extrovertCooldownTime = 5f;
    public Extrovert extrovertThatStartedBattle;

    void Start()
    {
        SceneManager.LoadScene("1.5-GameUIScene", LoadSceneMode.Additive);
        Extrovert.OnGuardHasSpottedPlayer += StartBattle;
    }

    void StartBattle(Extrovert extrovert)
    {
        if (!isInBattle)
        {
            extrovertThatStartedBattle = extrovert;
            isInBattle = true;
            //TODO Pause the Game
            SceneManager.LoadScene("2-ConversationBattle", LoadSceneMode.Additive);
            extrovertThatStartedBattle.extrovertSoundPlayed = true;
        }
    }

    public void EndBattle()
    {
        isInBattle = false;
        extrovertThatStartedBattle.isOnBattleCooldown = true;
        Invoke("setExtrovertCooldownFalse", extrovertCooldownTime);
        //TODO Resume the Game
        SceneManager.UnloadSceneAsync("2-ConversationBattle");
        extrovertThatStartedBattle.extrovertSoundPlayed = false;
    }

    void setExtrovertCooldownFalse()
    {
        extrovertThatStartedBattle.isOnBattleCooldown = false;
    }

    void PauseLevel()
    {
        
    }
}
