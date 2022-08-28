using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public bool isInBattle = false;
    public float extrovertCooldownTime = 5f;
    public Extrovert extrovertThatStartedBattle;
    
    GameObject player;
    PlayerMoveCode playerMoveScript;

    public GameObject _healthHandler;
    public HealthManager _healthManager;


    void Start() {
      SceneManager.LoadScene("1.5-GameUIScene", LoadSceneMode.Additive);
      StartCoroutine(WaitForPlayerToSetup());
    }

    IEnumerator WaitForPlayerToSetup() {
      while (true) {
        yield return null;
        GameObject playerObj = player = GameObject.FindGameObjectWithTag("Player");
  
        if (playerObj) {
          Debug.Log("Found Player to setup!");
          player = playerObj;
          playerMoveScript = player.GetComponent<PlayerMoveCode>();
          Extrovert.OnGuardHasSpottedPlayer += StartBattle;
          yield break;
        }
      }
    }

    private void LateUpdate()
    {
        if (!_healthHandler)
        {
            _healthHandler = GameObject.Find("HealthHandler");
        }
        if (!_healthManager)
        {
            _healthManager = _healthHandler ? _healthHandler.GetComponent<HealthManager>() : null;
        }
 
    }

    //IEnumerator DamageOverTime()
    //{
    //    while (_healthManager.socialBatteryManager.socialBatteryHealthAmount >= 0)
    //    {
    //        _healthManager.subtractForOverTime();
    //        yield return new WaitForSeconds(1f);
    //    }

    //}

    void StartBattle(Extrovert extrovert)
    {
        Debug.Log($"StartBattle called with extrovert: {extrovert.name}, current isInBattle: {isInBattle}");
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
        StartCoroutine(DelayThenRunAction(extrovertCooldownTime, SetExtrovertCooldownFalse));
        ResumeLevel();

        if (isRunningAway)
        {
            _healthManager.subtractForRunAway();
        }
        else
        {
            _healthManager.subtractForMisses(emojiMissCount);
        }

        SceneManager.UnloadSceneAsync("2-ConversationBattle");
        extrovertThatStartedBattle.extrovertSoundPlayed = false;

    }

    private IEnumerator DelayThenRunAction(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }

    void SetExtrovertCooldownFalse()
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
