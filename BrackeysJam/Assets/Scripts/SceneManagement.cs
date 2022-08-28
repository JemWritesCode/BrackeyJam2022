using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public bool isInBattle;
    public float extrovertCooldownTime = 5f;
    public Extrovert extrovertThatStartedBattle;
    
    GameObject player;
    PlayerMoveCode playerMoveScript;

    void Start()
    {
        SceneManager.LoadScene("1.5-GameUIScene", LoadSceneMode.Additive);
        player = GameObject.FindGameObjectWithTag("Player");
        playerMoveScript =  player.GetComponent<PlayerMoveCode>();
        Extrovert.OnGuardHasSpottedPlayer += StartBattle;
    }

    void StartBattle(Extrovert extrovert)
    {
        if (!isInBattle)
        {
            extrovertThatStartedBattle = extrovert;
            isInBattle = true;
            PauseLevel();
            SceneManager.LoadScene("2-ConversationBattle", LoadSceneMode.Additive);
            extrovertThatStartedBattle.extrovertSoundPlayed = true;
        }
    }

    public void EndBattle()
    {
        isInBattle = false;
        extrovertThatStartedBattle.isOnBattleCooldown = true;
        Invoke("setExtrovertCooldownFalse", extrovertCooldownTime);
        ResumeLevel();
        SceneManager.UnloadSceneAsync("2-ConversationBattle");
        extrovertThatStartedBattle.extrovertSoundPlayed = false;
    }

    void setExtrovertCooldownFalse()
    {
        extrovertThatStartedBattle.isOnBattleCooldown = false;
    }

    void PauseLevel()
    {
        playerMoveScript.isPaused = true;

    }

    void ResumeLevel()
    {
        playerMoveScript.isPaused = false;
    }
}
