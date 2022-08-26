using DG.Tweening;

using UnityEngine;

public class EmojiBattleLaneController : MonoBehaviour {
  [field: SerializeField]
  public KeyCode LaneKeyboardKey { get; private set; } = KeyCode.None;

  [field: SerializeField, Header("LaneKey")]
  public GameObject KeyIcon { get; private set; }

  [field: SerializeField]
  public TMPro.TMP_Text KeyIconLabel { get; private set; }

  private Tweener _laneKeyDownTweener;

  void Awake() {
    _laneKeyDownTweener =
        KeyIcon.transform
            .DOPunchScale(new Vector3(1.05f, 1.05f, 1.05f), 0.25f, 10, 0.5f)
            .SetAutoKill(false)
            .Pause();
  }

  void Update() {
    if (Input.GetKeyDown(LaneKeyboardKey)) {
      _laneKeyDownTweener.Restart();
    }
  }
}