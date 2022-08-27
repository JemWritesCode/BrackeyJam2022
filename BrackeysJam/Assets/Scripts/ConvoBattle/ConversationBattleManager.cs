using System.Collections.Generic;
using System.Linq;

using UnityEditor;

using UnityEngine;

public class ConversationBattleManager : MonoBehaviour {
  [field: SerializeField]
  public List<EmojiBattleLaneController> EmojiBattleLaneControllers { get; private set; } = new();

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