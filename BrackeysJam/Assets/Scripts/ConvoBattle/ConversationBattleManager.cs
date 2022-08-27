using System.Collections.Generic;
using System.Linq;

using UnityEditor;

using UnityEngine;

public class ConversationBattleManager : MonoBehaviour {
  [field: SerializeField]
  public List<EmojiBattleLaneController> EmojiBattleLaneControllers { get; private set; } = new();

}

[CustomEditor(typeof(ConversationBattleManager))]
[CanEditMultipleObjects]
public class ConversationBattleManagerEditor : Editor {
  private List<EmojiBattleLaneController> _controllers;

  void Awake() {
    _controllers = Selection.activeGameObject.GetComponent<ConversationBattleManager>().EmojiBattleLaneControllers;
  }

  public override void OnInspectorGUI() {
    base.OnInspectorGUI();

    foreach (EmojiBattleLaneController controller in _controllers) {
      if (EditorGUILayout.BeginFoldoutHeaderGroup(true, controller.name)) {
        if (GUILayout.Button("GenerateEmoji")) {
          controller.GenerateEmoji();
        }
      }

      EditorGUILayout.EndFoldoutHeaderGroup();
    }
  }
}