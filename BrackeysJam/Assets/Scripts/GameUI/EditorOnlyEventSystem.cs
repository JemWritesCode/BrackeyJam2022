using UnityEngine;

public class EditorOnlyEventSystem : MonoBehaviour {
  public void Awake() {
    if (Application.isPlaying
        && Application.isEditor
        && FindObjectsOfType<UnityEngine.EventSystems.EventSystem>().Length <= 0) {
      Debug.Log("Adding EventSystem and StandaloneInputModule.");

      gameObject.AddComponent<UnityEngine.EventSystems.EventSystem>();
      gameObject.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
    }
  }
}