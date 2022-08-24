using DG.Tweening;

using UnityEngine;

public class PopupMessageManager : MonoBehaviour {
  [field: SerializeField]
  public Transform MessagePanel { get; private set; }

  [field: SerializeField]
  public TMPro.TMP_Text MessageText { get; private set; }

  [field: SerializeField]
  public Ease ShowMessageEase { get; private set; } = Ease.Linear;

  [field: SerializeField]
  public Ease HideMessageEase { get; private set; } = Ease.Linear;

  private Vector3 _panelStartPosition;

  public void Awake() {
    _panelStartPosition = MessagePanel.position;

    ResetMessage();
  }

  public void ResetMessage() {
    MessagePanel.position = _panelStartPosition;
    MessageText.alpha = 0f;
  }

  public void ShowMessage(string text) {
    ResetMessage();

    MessageText.text = text;
    MessagePanel.DOMoveY(50f, 2f).From(isRelative: true).SetEase(ShowMessageEase);
    MessageText.DOFade(1f, 1.5f);
  }

  public void HideMessage() {
    MessageText.DOFade(0f, 1.5f);
    MessagePanel.DOMoveY(-50f, 2f).SetRelative(true).SetEase(HideMessageEase);
  }
}