using DG.Tweening;

using UnityEngine;
using UnityEngine.EventSystems;

public class MenuItemAnimator : MonoBehaviour, IPointerEnterHandler {

  private Sequence _tweenSequence;

  void Awake() {
    _tweenSequence = DOTween.Sequence()
        .Append(transform.DOPunchRotation(new Vector3(35f, 0f, 0f), 2f, 0, 0.1f))
        .AppendInterval(0.5f)
        .SetLoops(-1)
        .Pause();
  }

  public void OnPointerEnter(PointerEventData _) {
    _tweenSequence.Restart();
  }

  public void OnPointerExit(PointerEventData _) {
    _tweenSequence.Pause();
  }
}
