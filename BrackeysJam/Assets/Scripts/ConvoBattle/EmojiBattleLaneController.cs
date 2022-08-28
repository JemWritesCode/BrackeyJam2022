using DG.Tweening;

using System;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;
using UnityEngine.Events;

public class EmojiBattleLaneController : MonoBehaviour {
  [field: SerializeField, Header("Key")]
  public KeyCode LaneKeyboardKey { get; private set; } = KeyCode.None;

  [field: SerializeField]
  public GameObject KeyIcon { get; private set; }

  [field: SerializeField]
  public TMPro.TMP_Text KeyIconLabel { get; private set; }

  [field: SerializeField, Header("Emoji")]
  public TMPro.TMP_Text EmojiIcon { get; private set; }

  [field: SerializeField, Min(0f)]
  public float EmojiChildMovementDuration { get; private set; } = 10f;

  [field: SerializeField]
  public Ease EmojiChildMovementEase { get; private set; } = Ease.Linear;

  [field: SerializeField, Header("HitArea")]
  public GameObject HitArea { get; private set; }

  [field: SerializeField, Header("Events")]
  public UnityEvent<EmojiBattleLaneController, GameObject, bool> OnEmojiChildHitAttempt { get; private set; }

  private Tweener _laneKeyDownTweener;
  private Sequence _generateEmojiIconSequence;
  private RectTransform _hitAreaRectTransform;

  private readonly List<GameObject> _emojiChildren = new();

  void Awake() {
    _laneKeyDownTweener =
        KeyIcon.transform
            .DOPunchScale(new Vector3(1.05f, 1.05f, 1.05f), 0.25f, 10, 0.5f)
            .SetAutoKill(false)
            .SetLink(KeyIcon.gameObject)
            .Pause();

    _generateEmojiIconSequence =
        DOTween.Sequence()
            .OnPlay(() => GenerateEmojiChild(EmojiChildMovementDuration))
            .Insert(0f, EmojiIcon.transform.DOPunchScale(new Vector3(1.05f, 1.05f, 1.05f), 0.5f, 5, 0.5f))
            .Insert(0f, EmojiIcon.DOFade(1f, 0.25f).SetLoops(2, LoopType.Yoyo))
            .SetAutoKill(false)
            .SetLink(EmojiIcon.gameObject)
            .Pause();

    _hitAreaRectTransform = HitArea.GetComponent<RectTransform>();
  }

  void Update() {
    if (Input.GetKeyDown(LaneKeyboardKey)) {
      _laneKeyDownTweener.Restart();

      CheckForChildHit();
    }
  }

  public void CheckForChildHit(bool performCheck = true) {
    if (_emojiChildren.Count > 0 && _emojiChildren[0]) {
      GameObject child = _emojiChildren[0];
      _emojiChildren.RemoveAt(0);

      if (performCheck
          && Vector3.Distance(HitArea.transform.localPosition, child.transform.localPosition)
                < _hitAreaRectTransform.sizeDelta.y * 1.25f) {
        OnEmojiChildHitAttempt?.Invoke(this, child, true);
      } else {
        OnEmojiChildHitAttempt?.Invoke(this, child, false);
      }
    }
  }

  public void GenerateEmoji() {
    _generateEmojiIconSequence.Restart();
  }

  public void GenerateEmojiChild(float duration) {
    TMPro.TMP_Text child = Instantiate(EmojiIcon, EmojiIcon.transform.parent);
    _emojiChildren.Add(child.gameObject);

    float childScale = UnityEngine.Random.Range(1.5f, 1.9f);

    child.alpha = 1f;
    child.transform.DOScale(childScale, 2f).SetLink(child.gameObject);

    DOTween.Sequence()
        .Append(child.transform.DOShakeRotation(2f, new Vector3(0f, 0f, 35f)))
        .AppendInterval(0.5f)
        .SetLoops(-1, LoopType.Yoyo)
        .SetLink(child.gameObject);

    child.transform
        .DOLocalMove(HitArea.transform.localPosition, duration)
        .SetEase(EmojiChildMovementEase)
        .OnComplete(() => CheckForChildHit(performCheck: false))
        .SetLink(child.gameObject);
  }

  public void DestroyChild(GameObject child) {
    if (child) {
      _emojiChildren.Remove(child);
      Destroy(child);
    }
  }

  public void PauseEmojiChildren() {
    foreach (GameObject child in _emojiChildren) {
      DOTween.Kill(child.transform);
    }

    _emojiChildren.Clear();
  }
}

//[CustomEditor(typeof(EmojiBattleLaneController))]
//public class EmojiBattleLaneControllerEditor : Editor {
//  public override void OnInspectorGUI() {
//    base.OnInspectorGUI();

//    EditorGUILayout.Space(20f);

//    if (GUILayout.Button("Generate")) {
//      Selection.activeGameObject.GetComponent<EmojiBattleLaneController>().GenerateEmoji();
//    }
//  }
//}