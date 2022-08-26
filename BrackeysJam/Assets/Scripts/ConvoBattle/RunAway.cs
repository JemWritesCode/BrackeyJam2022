using UnityEngine;

public class RunAway : MonoBehaviour {
  private SceneManagement _sceneManagement;

  public void Awake() {
    _sceneManagement = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagement>();
  }

  public void RunAwayEndBattle() {
    _sceneManagement.EndBattle();
  }
}