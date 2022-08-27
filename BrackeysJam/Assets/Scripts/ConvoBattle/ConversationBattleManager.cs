using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

public class ConversationBattleManager : MonoBehaviour {
  [field: SerializeField]
  public List<EmojiBattleLaneController> EmojiBattleLaneControllers { get; private set; } = new();

  void Awake() {
    foreach (EmojiBattleLaneController controller in EmojiBattleLaneControllers) {
      controller.OnEmojiChildHitAttempt += ProcessEmojiChildHitAttempt;
    }

    StartCoroutine(GenerateRandomEmojis(10));
  }

  IEnumerator GenerateRandomEmojis(int count) {
    yield return null;

    for (int i = 0; i < count; i++) {
      yield return new WaitForSeconds(Random.Range(2f, 5f));

      int index = Random.Range(0, EmojiBattleLaneControllers.Count);
      EmojiBattleLaneControllers[index].GenerateEmoji();
    }
  }

  public void ProcessEmojiChildHitAttempt(EmojiBattleLaneController controller, Rect hitRect) {
    Debug.Log($"Get rect son: {hitRect} from controller ({controller.name}");
  }
}

[CustomEditor(typeof(ConversationBattleManager))]
public class ConversationBattleManagerEditor : Editor {
  private readonly List<EmojiBattleLaneController> _controllers = new();

  public void OnEnable() {
    _controllers.Clear();

    if (Selection.activeGameObject.TryGetComponent(out ConversationBattleManager manager)) {
      _controllers.AddRange(manager.EmojiBattleLaneControllers);
    };
  }

  public override void OnInspectorGUI() {
    base.OnInspectorGUI();

    foreach (EmojiBattleLaneController controller in _controllers) {
      if (!controller) {
        continue;
      }

      if (EditorGUILayout.BeginFoldoutHeaderGroup(true, controller.name)) {
        if (GUILayout.Button("GenerateEmoji")) {
          controller.GenerateEmoji();
        }
      }

      EditorGUILayout.EndFoldoutHeaderGroup();
    }
  }
}