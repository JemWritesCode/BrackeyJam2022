using System.Collections.Generic;

using DG.Tweening;

using UnityEditor;

using UnityEngine;
using UnityEngine.UI;

public class ConversationBattleManager : MonoBehaviour {
  [field: SerializeField, Header("Background")]
  public Image BattleBackground { get; private set; }

  [field: SerializeField]
  public TMPro.TMP_Text BattleTitle { get; private set; }

  [field: SerializeField, Header("RunAway")]
  public Button RunAwayButton { get; private set; }

  [field: SerializeField, Header("EmojiBattleLane")]
  public List<EmojiBattleLaneController> EmojiBattleLaneControllers { get; private set; } = new();

  [field: SerializeField]
  public GameObject HitPopup { get; private set; }

  [field: SerializeField]
  public GameObject MissPopup { get; private set; }

  void Awake() {
    ResetBattleUI();
    AnimateBattleStart();
  }

  public void ResetBattleUI() {
    BattleBackground.color = BattleBackground.color.SetAlpha(0f);
    BattleTitle.alpha = 0f;

    RunAwayButton.GetComponent<CanvasGroup>().alpha = 0f;

    foreach (EmojiBattleLaneController controller in EmojiBattleLaneControllers) {
      controller.GetComponent<CanvasGroup>().alpha = 0f;
    }
  }

  public void AnimateBattleStart() {
    DOTween.Sequence()
        .SetLink(BattleTitle.gameObject)
        .Insert(0f, BattleTitle.transform.DOScale(1.1f, 3f))
        .AppendInterval(1f)
        .SetLoops(-1, LoopType.Yoyo);

    Sequence sequence =
        DOTween.Sequence()
            .Insert(0f, BattleBackground.DOFade(1f, 3f).From(0f))
            .Insert(0f, BattleTitle.DOFade(1f, 1f).From(0f))
            .Insert(0f, BattleTitle.transform.DOMoveX(25f, 1f).From(isRelative: true))
            .Insert(0.5f, RunAwayButton.GetComponent<CanvasGroup>().DOFade(1f, 1f).From(0f))
            .Insert(0.5f, RunAwayButton.transform.DOMoveX(35f, 2f).From(isRelative: true));

    float startTime = 1f;

    foreach (EmojiBattleLaneController controller in EmojiBattleLaneControllers) {
      sequence
          .Insert(startTime, controller.GetComponent<CanvasGroup>().DOFade(1f, 1f).From(0f))
          .Insert(startTime, controller.transform.DOMoveX(45f, 1f).From(isRelative: true).SetEase(Ease.OutQuart));

      startTime += 0.25f;
    }
  }

  public void ProcessEmojiChildHitAttempt
      (EmojiBattleLaneController controller, GameObject emojiChild, bool isHit) {
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

[CustomEditor(typeof(ConversationBattleManager))]
public class ConversationBattleManagerEditor : Editor {
  private readonly List<EmojiBattleLaneController> _controllers = new();

  public void OnEnable() {
    _controllers.Clear();

    if (Selection.activeGameObject.TryGetComponent(out ConversationBattleManager manager)) {
      _controllers.AddRange(manager.EmojiBattleLaneControllers);
    };
  }

  public override void OnInspectorGUI() {
    base.OnInspectorGUI();

    EditorGUILayout.Space(20f);

    if (GUILayout.Button("AnimateBattleStart")) {
      ConversationBattleManager manager = Selection.activeGameObject.GetComponent<ConversationBattleManager>();

      manager.ResetBattleUI();
      manager.AnimateBattleStart();
    }

    EditorGUILayout.Space(20f);

    foreach (EmojiBattleLaneController controller in _controllers) {
      if (!controller) {
        continue;
      }

      if (EditorGUILayout.BeginFoldoutHeaderGroup(true, controller.name)) {
        if (GUILayout.Button("GenerateEmoji")) {
          controller.GenerateEmoji();
        }
      }

      EditorGUILayout.EndFoldoutHeaderGroup();
    }
  }
}