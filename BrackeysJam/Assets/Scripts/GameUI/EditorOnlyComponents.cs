using UnityEngine;

public class EditorOnlyComponents : MonoBehaviour {
  public void Awake() {
    if (Application.isPlaying && Application.isEditor) {
      if (FindObjectsOfType<Camera>().Length <= 0) {
        Debug.Log("Adding Camera.");
        gameObject.AddComponent<Camera>();
      }

      if (FindObjectsOfType<UnityEngine.EventSystems.EventSystem>().Length <= 0) {
        Debug.Log("Adding EventSystem and StandaloneInputModule.");
        gameObject.AddComponent<UnityEngine.EventSystems.EventSystem>();
        gameObject.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
      }
    }
  }
}