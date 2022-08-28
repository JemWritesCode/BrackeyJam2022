using System.Collections;
using System.Collections.Generic;

using DG.Tweening;

using UnityEditor;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConversationBattleManager : MonoBehaviour {
  [field: SerializeField, Header("Background")]
  public Image BattleBackground { get; private set; }

  [field: SerializeField]
  public TMPro.TMP_Text BattleTitle { get; private set; }

  [field: SerializeField, Header("Damage")]
  public TMPro.TMP_Text DamageLabel { get; private set; }

  [field: SerializeField]
  public TMPro.TMP_Text DamageValue { get; private set; }

  [field: SerializeField, Header("Timer")]
  public TMPro.TMP_Text TimerLabel { get; private set; }

  [field: SerializeField]
  public TMPro.TMP_Text TimerValue { get; private set; }

  [field: SerializeField, Header("RunAway")]
  public Button RunAwayButton { get; private set; }

  [field: SerializeField, Header("EmojiBattleLane")]
  public List<EmojiBattleLaneController> EmojiBattleLaneControllers { get; private set; } = new();

  [field: SerializeField]
  public GameObject HitPopup { get; private set; }

  [field: SerializeField]
  public GameObject MissPopup { get; private set; }

  [field: SerializeField, Header("EmojiGenerator")]
  public float GenerateEmojiInterval { get; private set; } = 3f;

  void Awake() {
    ResetBattleUI();
  }

  private Coroutine _emojiGeneratorCoroutine;

  public void BattleStart(int emojiCount, float duration) {
    DamageValue.text = $"OK";
    TimerValue.text = $"{duration:N2}s";

    AnimateBattleStart()
        .OnComplete(() => _emojiGeneratorCoroutine = StartCoroutine(EmojiGenerator(emojiCount, duration)));
  }

  public void BattleEnd(bool isRunningAway) {
    Debug.Log($"Ending Battle: emojiMissCount is: {_emojiMissCount}, ranAway: {isRunningAway}");

    if (_emojiGeneratorCoroutine != null) {
      StopCoroutine(_emojiGeneratorCoroutine);
    }

    foreach (EmojiBattleLaneController controller in EmojiBattleLaneControllers) {
      controller.PauseEmojiChildren();
    }

    GameObject sceneManager = GameObject.FindGameObjectWithTag("SceneManager");

    if (sceneManager) {
      sceneManager.GetComponent<SceneManagement>()
          .EndBattle(emojiMissCount: _emojiMissCount, isRunningAway: isRunningAway);
    }
  }

  public IEnumerator EmojiGenerator(int emojiCount, float duration) {
    yield return null;

    float waitInterval = 0.25f;
    float waitElapsed = 0f;
    float timeElapsed = 0f;
    int emojiGeneratedCount = 0;

    while (timeElapsed < duration) {
      waitElapsed += Time.deltaTime;

      if (waitElapsed >= waitInterval && emojiGeneratedCount < emojiCount) {
        GetRandomController().GenerateEmoji();
        emojiGeneratedCount++;
        waitElapsed = 0f;
        waitInterval = GenerateEmojiInterval + Random.Range(-0.1f, 0.1f);
      }

      timeElapsed += Time.deltaTime;
      TimerValue.text = $"{duration - timeElapsed:N2}s";

      yield return null;
    }

    TimerValue.text = $"0s";
    BattleEnd(false);
  }

  private EmojiBattleLaneController GetRandomController() {
    return EmojiBattleLaneControllers[Random.Range(0, EmojiBattleLaneControllers.Count)];
  }

  public void ResetBattleUI() {
    BattleBackground.color = BattleBackground.color.SetAlpha(0f);
    BattleTitle.alpha = 0f;

    DamageLabel.alpha = 0f;
    DamageValue.alpha = 0f;

    TimerLabel.alpha = 0f;
    TimerValue.alpha = 0f;

    RunAwayButton.GetComponent<CanvasGroup>().alpha = 0f;

    foreach (EmojiBattleLaneController controller in EmojiBattleLaneControllers) {
      controller.GetComponent<CanvasGroup>().alpha = 0f;
    }
  }

  public Sequence AnimateBattleStart() {
    DOTween.Sequence()
        .SetLink(BattleTitle.gameObject)
        .Insert(0f, BattleTitle.transform.DOScale(1.1f, 3f))
        .AppendInterval(1f)
        .SetLoops(-1, LoopType.Yoyo);

    Sequence sequence =
        DOTween.Sequence()
            .SetLink(gameObject)
            .Insert(0f, BattleBackground.DOFade(1f, 3f).From(0f))
            .Insert(0f, BattleTitle.DOFade(1f, 1f).From(0f))
            .Insert(0f, BattleTitle.transform.DOMoveX(25f, 1f).From(isRelative: true))
            .Insert(0.25f, DamageLabel.DOFade(1f, 1f).From(0f))
            .Insert(0.25f, DamageLabel.transform.DOMoveX(-25f, 2f).From(isRelative: true))
            .Insert(0.45f, DamageValue.DOFade(1f, 1f).From(0f))
            .Insert(0.45f, DamageValue.transform.DOMoveX(-25f, 2f).From(isRelative: true))
            .Insert(0.30f, TimerLabel.DOFade(1f, 1f).From(0f))
            .Insert(0.30f, TimerLabel.transform.DOMoveX(25f, 2f).From(isRelative: true))
            .Insert(0.50f, TimerValue.DOFade(1f, 1f).From(0f))
            .Insert(0.50f, TimerValue.transform.DOMoveX(25f, 2f).From(isRelative: true))
            .Insert(0.55f, RunAwayButton.GetComponent<CanvasGroup>().DOFade(1f, 1f).From(0f))
            .Insert(0.55f, RunAwayButton.transform.DOMoveX(35f, 2f).From(isRelative: true));

    float startTime = 1f;

    foreach (EmojiBattleLaneController controller in EmojiBattleLaneControllers) {
      sequence
          .Insert(startTime, controller.GetComponent<CanvasGroup>().DOFade(1f, 1f).From(0f))
          .Insert(startTime, controller.transform.DOMoveX(45f, 1f).From(isRelative: true).SetEase(Ease.OutQuart));

      startTime += 0.25f;
    }

    return sequence;
  }

  private int _emojiMissCount = 0;

  public void ProcessEmojiChildHitAttempt(EmojiBattleLaneController controller, GameObject emojiChild, bool isHit) {
    if (!isHit) {
      _emojiMissCount++;
      DamageValue.text = $"{_emojiMissCount} miss";
    }

    GameObject popup =
        Instantiate(
            isHit ? HitPopup : MissPopup,
            emojiChild.transform.position,
            Quaternion.identity,
            emojiChild.transform.parent);

    DOTween.Kill(emojiChild.transform);

    DOTween.Sequence()
        .SetLink(popup)
        .Insert(0f, popup.transform.DOPunchScale(new Vector3(1.1f, 1.1f, 1.1f), 1f, 5, 0))
        .Insert(0f, popup.transform.DOMoveY(25f, 3f).SetRelative(true))
        .Insert(0f, emojiChild.transform.DOScale(Vector3.zero, 3f))
        .OnComplete(() => {
          Destroy(popup);
          controller.DestroyChild(emojiChild);
        });
  }
}

//[CustomEditor(typeof(ConversationBattleManager))]
//public class ConversationBattleManagerEditor : Editor {
//  private readonly List<EmojiBattleLaneController> _controllers = new();

//  public void OnEnable() {
//    _controllers.Clear();

//    if (Selection.activeGameObject.TryGetComponent(out ConversationBattleManager manager)) {
//      _controllers.AddRange(manager.EmojiBattleLaneControllers);
//    };
//  }

//  private float _battleDuration = 20f;
//  private int _battleEmojiCount = 10;

//  public override void OnInspectorGUI() {
//    base.OnInspectorGUI();

//    EditorGUILayout.Space(20f);
//    _battleDuration = EditorGUILayout.Slider("BattleDuration", _battleDuration, 0f, 60f);
//    _battleEmojiCount = EditorGUILayout.IntSlider("BattleEmojiCount", _battleEmojiCount, 0, 60);

//    if (GUILayout.Button("BattleStart")) {
//      ConversationBattleManager manager = Selection.activeGameObject.GetComponent<ConversationBattleManager>();
//      manager.ResetBattleUI();
//      manager.BattleStart(_battleEmojiCount, _battleDuration);
//    }

//    EditorGUILayout.Space(20f);

//    foreach (EmojiBattleLaneController controller in _controllers) {
//      if (!controller) {
//        continue;
//      }

//      if (EditorGUILayout.BeginFoldoutHeaderGroup(true, controller.name)) {
//        if (GUILayout.Button("GenerateEmoji")) {
//          controller.GenerateEmoji();
//        }
//      }

//      EditorGUILayout.EndFoldoutHeaderGroup();
//    }
//  }
//}