using System;
using System.IO;

using UnityEditor;

using UnityEngine;

public class UIBuilder : EditorWindow {
  [MenuItem("ComfyLib/UIBuilder")]
  public static void ShowWindow() {
    GetWindow(typeof(UIBuilder));
  }

  private int _spriteWidth;
  private int _spriteHeight;
  private int _spriteRadius;

  private string _targetPath;

  private void OnGUI() {
    GUILayout.Label("Sprite Settings", EditorStyles.boldLabel);

    _spriteWidth = EditorGUILayout.IntSlider("Width", _spriteWidth, 0, 512);
    _spriteHeight = EditorGUILayout.IntSlider("Height", _spriteHeight, 0, 512);
    _spriteRadius = EditorGUILayout.IntSlider("Radius", _spriteRadius, 0, 90);

    GUILayout.Space(10f);

    GUILayout.Label("Saving", EditorStyles.boldLabel);

    _targetPath = EditorGUILayout.DelayedTextField("Asset Path", _targetPath, EditorStyles.textField);

    if (GUILayout.Button("Select Asset Path")) {
      _targetPath = EditorUtility.OpenFolderPanel("Asset Path", Application.dataPath, string.Empty);
    }

    GUILayout.Space(20f);

    if (GUILayout.Button("Generate")) {
      GenerateThenSaveSprite();
    }
  }

  void GenerateThenSaveSprite() {
    Sprite sprite = CreateRoundedCornerSprite(_spriteWidth, _spriteHeight, _spriteRadius, FilterMode.Bilinear);

    string filename = Path.Combine(_targetPath, $"{sprite.name}.png");
    Debug.Log($"Saving texture to: {filename}");

    byte[] bytes = sprite.texture.EncodeToPNG();
    File.WriteAllBytes(filename, bytes);

    AssetDatabase.Refresh();

    filename = $"Assets{filename[Application.dataPath.Length..]}";
    Debug.Log($"Reloading saved texture at relative path: {filename}");

    TextureImporter importer = (TextureImporter) AssetImporter.GetAtPath(filename);
    importer.spritePixelsPerUnit = sprite.pixelsPerUnit;
    importer.mipmapEnabled = false;
    importer.textureType = TextureImporterType.Sprite;
    importer.spriteBorder = sprite.border;

    EditorUtility.SetDirty(importer);
    importer.SaveAndReimport();

    Debug.Log($"Done!");
  }

  static readonly Color32 ColorWhite = Color.white;
  static readonly Color32 ColorClear = Color.clear;

  public static Sprite CreateRoundedCornerSprite(
      int width, int height, int radius, FilterMode filterMode = FilterMode.Bilinear) {
    string name = $"RoundedCorner-{width}w-{height}h-{radius}r";

    Texture2D texture = new(width, height);
    texture.name = name;
    texture.wrapMode = TextureWrapMode.Clamp;
    texture.filterMode = filterMode;

    Color32[] pixels = new Color32[width * height];

    for (int y = 0; y < height; y++) {
      for (int x = 0; x < width; x++) {
        pixels[(y * width) + x] = IsCornerPixel(x, y, width, height, radius) ? ColorClear : ColorWhite;
      }
    }

    texture.SetPixels32(pixels);
    texture.Apply();

    int borderWidth;
    for (borderWidth = 0; borderWidth < width; borderWidth++) {
      if (pixels[borderWidth] == Color.white) {
        break;
      }
    }

    int borderHeight;
    for (borderHeight = 0; borderHeight < height; borderHeight++) {
      if (pixels[borderHeight * width] == Color.white) {
        break;
      }
    }

    Sprite sprite =
        Sprite.Create(
            texture,
            new(0, 0, width, height),
            new(0.5f, 0.5f),
            pixelsPerUnit: 100f,
            extrude: 0,
            SpriteMeshType.FullRect,
            new(borderWidth, borderHeight, borderWidth, borderHeight));

    sprite.name = $"{name}-{borderWidth}bw-{borderHeight}bh";

    return sprite;
  }

  static bool IsCornerPixel(int x, int y, int w, int h, int rad) {
    if (rad == 0) {
      return false;
    }

    int dx = Math.Min(x, w - x);
    int dy = Math.Min(y, h - y);

    if (dx == 0 && dy == 0) {
      return true;
    }

    if (dx > rad || dy > rad) {
      return false;
    }

    dx = rad - dx;
    dy = rad - dy;

    return Math.Round(Math.Sqrt(dx * dx + dy * dy)) > rad;
  }
}
