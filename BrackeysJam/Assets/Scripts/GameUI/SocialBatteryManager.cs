using System;
using DG.Tweening;
using System.Collections;

using UnityEngine;

public class SocialBatteryManager : MonoBehaviour {
  [field: SerializeField]
  public SlicedFilledImage BatteryFill { get; private set; }

  [field: SerializeField, Min(0f)]
  public float SetFillDuration { get; private set; } = 2f;

  [field: SerializeField]
  public Ease SetFillEase { get; private set; } = Ease.Linear;

  [field: SerializeField, Min(0)]
  public int SetFillFlashCount { get; private set; } = 20;

  [field: SerializeField]
  public Color IncreaseFillColor { get; private set; } = Color.green;

  [field: SerializeField]
  public Color DecreaseFillColor { get; private set; } = Color.red;

  private Color _batteryFillStartColor;


    public float socialBatteryHealthAmount;


  public void Awake() {
    _batteryFillStartColor = BatteryFill.color;
    BatteryFill.fillAmount = 1f;
    socialBatteryHealthAmount = 1f;
  }

    private void Start()
    {
        StartCoroutine(DamageOverTime());
    }

    IEnumerator DamageOverTime()
    {
        while(socialBatteryHealthAmount > 0)
        {
            DecreaseBatteryFill(.1f, 1f);
            yield return new WaitForSeconds(1f);
        }
    }

    public void IncreaseBatteryFill(float offset, float duration = float.NegativeInfinity) {
    SetBatteryFill(BatteryFill.fillAmount + offset, duration, IncreaseFillColor);
        socialBatteryHealthAmount += offset;
  }

  public void DecreaseBatteryFill(float offset, float duration = float.NegativeInfinity) {
    SetBatteryFill(BatteryFill.fillAmount - offset, duration, DecreaseFillColor);
        socialBatteryHealthAmount -= offset;
    }

  public void SetBatteryFill(float fill, float duration = float.NegativeInfinity) {
    SetBatteryFill(fill, duration, BatteryFill.color);
        socialBatteryHealthAmount = fill;
    }

  public void SetBatteryFill(float fill, float duration, Color flashColor) {
    if (duration < 0) {
      duration = SetFillDuration;
    }

    BatteryFill
        .DOFillAmount(fill, duration)
        .SetEase(SetFillEase);

    DOTween.Sequence()
      .Append(
          BatteryFill
              .DOColor(flashColor, duration)
              .SetEase(Ease.InOutFlash, SetFillFlashCount, 1))
      .Append(
          BatteryFill
              .DOColor(_batteryFillStartColor, 0.1f));
  }
}
