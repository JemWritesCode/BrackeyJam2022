using DG.Tweening;

using UnityEditor;

using UnityEngine;

[CustomEditor(typeof(PopupMessageManager))]
public class PopupMessageManagerEditor : Editor {
  private bool MessageGroup = true;
  private string _showMessageText = "Hello, world!";

  public override void OnInspectorGUI() {
    base.OnInspectorGUI();

    if (MessageGroup = EditorGUILayout.BeginFoldoutHeaderGroup(MessageGroup, "Message")) {
      _showMessageText = EditorGUILayout.TextField(text: _showMessageText);

      EditorGUILayout.BeginHorizontal();

      using (new EditorGUI.DisabledScope(!Application.isPlaying)) {
        if (GUILayout.Button("ShowMessage")) {
          PopupMessageManager manager = Selection.activeGameObject.GetComponent<PopupMessageManager>();
          manager.ShowMessage(_showMessageText);
        }

        if (GUILayout.Button("HideMessage")) {
          PopupMessageManager manager = Selection.activeGameObject.GetComponent<PopupMessageManager>();
          manager.HideMessage();
        }
      }

      EditorGUILayout.EndHorizontal();
    }

    EditorGUILayout.EndFoldoutHeaderGroup();
  }
}
