using DG.Tweening;

using UnityEngine;
using UnityEngine.EventSystems;

public class RunAway : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
  public void OnPointerEnter(PointerEventData eventData) {
    transform.DOScale(1.1f, 0.5f);
  }

  public void OnPointerExit(PointerEventData eventData) {
    transform.DOScale(1f, 0.5f);
  }
}