using DG.Tweening;

using UnityEditor;

using UnityEngine;

public class EmojiBattleLaneController : MonoBehaviour {
  [field: SerializeField, Header("Key")]
  public KeyCode LaneKeyboardKey { get; private set; } = KeyCode.None;

  [field: SerializeField]
  public GameObject KeyIcon { get; private set; }

  [field: SerializeField]
  public TMPro.TMP_Text KeyIconLabel { get; private set; }

  [field: SerializeField, Header("Emoji")]
  public TMPro.TMP_Text EmojiIcon { get; private set; }

  [field: SerializeField]
  public Ease EmojiChildMovementEase { get; private set; } = Ease.Linear;

  [field: SerializeField, Header("HitArea")]
  public GameObject HitArea { get; private set; }

  private Tweener _laneKeyDownTweener;
  private Sequence _generateEmojiIconSequence;

  void Awake() {
    _laneKeyDownTweener =
        KeyIcon.transform
            .DOPunchScale(new Vector3(1.05f, 1.05f, 1.05f), 0.25f, 10, 0.5f)
            .SetAutoKill(false)
            .Pause();

    _generateEmojiIconSequence =
        DOTween.Sequence()
            .Insert(0f, EmojiIcon.transform.DOPunchScale(new Vector3(1.05f, 1.05f, 1.05f), 0.5f, 5, 0.5f))
            .Insert(0f, EmojiIcon.DOFade(1f, 0.25f).SetLoops(2, LoopType.Yoyo))
            .OnComplete(() => GenerateEmojiChild())
            .SetAutoKill(false)
            .Pause();
  }

  void Update() {
    if (Input.GetKeyDown(LaneKeyboardKey)) {
      _laneKeyDownTweener.Restart();
    }
  }

  public void GenerateEmoji() {
    _generateEmojiIconSequence.Restart();
  }

  public void GenerateEmojiChild() {
    TMPro.TMP_Text child = Instantiate(EmojiIcon, EmojiIcon.transform.parent);
    child.alpha = 1f;
    child.transform.DOScale(2f, 2f);

    DOTween.Sequence()
        .Append(child.transform.DOShakeRotation(2f, new Vector3(0f, 0f, 15f)))
        .AppendInterval(1.5f)
        .SetLoops(-1, LoopType.Yoyo);

    child.transform
        .DOLocalMove(HitArea.transform.localPosition, 15f)
        .SetEase(EmojiChildMovementEase)
        .OnComplete(() => Destroy(child));
  }
}

[CustomEditor(typeof(EmojiBattleLaneController))]
public class EmojiBattleLaneControllerEditor : Editor {
  public override void OnInspectorGUI() {
    base.OnInspectorGUI();

    EditorGUILayout.Space(20f);

    if (GUILayout.Button("Generate")) {
      Selection.activeGameObject.GetComponent<EmojiBattleLaneController>().GenerateEmoji();
    }
  }
}