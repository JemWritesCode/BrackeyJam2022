using DG.Tweening;

using UnityEngine;
using UnityEngine.EventSystems;

public class RunAway : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
  private SceneManagement _sceneManagement;

  public void Awake() {
    GameObject sceneManager = GameObject.FindGameObjectWithTag("SceneManager");

    if (sceneManager) {
      _sceneManagement = sceneManager.GetComponent<SceneManagement>();
    }
  }

  public void OnPointerEnter(PointerEventData eventData) {
    transform.DOScale(1.1f, 0.5f);
  }

  public void OnPointerExit(PointerEventData eventData) {
    transform.DOScale(1f, 0.5f);
  }

  public void RunAwayEndBattle() {
    _sceneManagement.EndBattle();
  }
}