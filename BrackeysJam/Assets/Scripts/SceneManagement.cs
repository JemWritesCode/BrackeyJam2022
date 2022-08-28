using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public bool isInBattle;
    public float extrovertCooldownTime = 5f;
    public Extrovert extrovertThatStartedBattle;
    
    GameObject player;
    PlayerMoveCode playerMoveScript;

    public GameObject socialBattery;
    public SocialBatteryManager socialBatteryManager;

    void Start()
    {
        SceneManager.LoadScene("1.5-GameUIScene", LoadSceneMode.Additive);
        player = GameObject.FindGameObjectWithTag("Player");
        playerMoveScript =  player.GetComponent<PlayerMoveCode>();
        Extrovert.OnGuardHasSpottedPlayer += StartBattle;

        socialBattery = GameObject.Find("SocialBattery");
        socialBatteryManager = socialBattery.GetComponent<SocialBatteryManager>();
    }



    void Update()
    {
        HandleLoss();


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

            // TODO(Jemmeh): here's where you can trigger the battle with custom values
            StartCoroutine(StartBattleWhenManagerFound(emojiCount: 10, battleDuration: 20f));
        }
    }

    IEnumerator StartBattleWhenManagerFound(int emojiCount, float battleDuration) {
      while (true) {
        yield return null;
        GameObject battleManagerObj = GameObject.FindGameObjectWithTag("ConversationBattleManager");

        if (battleManagerObj) {
          Debug.Log($"Found ConversationBattleManager!");
          battleManagerObj.GetComponent<ConversationBattleManager>().BattleStart(emojiCount, battleDuration);
          yield break;
        }
      }
    }

    public void EndBattle(int emojiMissCount, bool isRunningAway)
    {
        // TODO(Jemmeh): isRunningAway=true if they hit the runAwayButton
        // TODO(Jemmeh): emojiMissCount wikll start at 0 and more if they miss emojis
        Debug.Log($"Ending Battle: emojiMissCount is: {emojiMissCount}, ranAway: {isRunningAway}");
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


    void HandleLoss()
    {
        // if social battery reaches 0
        if(socialBatteryManager.socialBatteryHealthAmount <= 0)
        {
            SceneManager.LoadScene("4-Lose");
        }
    }

    void GoToWinScreen()
    {
        // if all the tasks are completed and they go to the exit door
    }
}
