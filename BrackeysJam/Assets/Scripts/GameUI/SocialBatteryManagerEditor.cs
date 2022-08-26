using UnityEditor;

using UnityEngine;

[CustomEditor(typeof(SocialBatteryManager))]
public class SocialBatteryManagerEditor : Editor {
  private bool _showBatteryFillGroup = true;
  private float _batteryFillAmount = 0f;
  private float _batteryFillOffset = 0f;

  public override void OnInspectorGUI() {
    base.OnInspectorGUI();

    if (_showBatteryFillGroup = EditorGUILayout.BeginFoldoutHeaderGroup(_showBatteryFillGroup, "BatteryFill")) {
      _batteryFillAmount = EditorGUILayout.Slider("fillAmount", _batteryFillAmount, 0f, 1f);

      using (new EditorGUI.DisabledScope(!Application.isPlaying)) {
        if (GUILayout.Button("Set")) {
          SocialBatteryManager manager = Selection.activeGameObject.GetComponent<SocialBatteryManager>();
          manager.SetBatteryFill(_batteryFillAmount);
        }
      }

      _batteryFillOffset = EditorGUILayout.Slider("fillOffset", _batteryFillOffset, 0f, 1f);

      EditorGUILayout.BeginHorizontal();

      using (new EditorGUI.DisabledScope(!Application.isPlaying)) {
        if (GUILayout.Button("Increase")) {
          SocialBatteryManager manager = Selection.activeGameObject.GetComponent<SocialBatteryManager>();
          manager.SetBatteryFill(
              manager.BatteryFill.fillAmount + _batteryFillOffset,
              manager.SetFillDuration,
              manager.IncreaseFillColor);
        }

        if (GUILayout.Button("Decrease")) {
          SocialBatteryManager manager = Selection.activeGameObject.GetComponent<SocialBatteryManager>();
          manager.SetBatteryFill(
              manager.BatteryFill.fillAmount - _batteryFillOffset,
              manager.SetFillDuration,
              manager.DecreaseFillColor);
        }
      }

      EditorGUILayout.EndHorizontal();
    }

    EditorGUILayout.EndFoldoutHeaderGroup();
  }
}
