using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public static class SlicedFilledImageExtensions {
  /// <summary>Tweens an SlicedFilledImage's fillAmount to the given value.
  /// Also stores the image as the tween's target so it can be used for filtered operations</summary>
  /// <param name="endValue">The end value to reach (0 to 1)</param>
  /// <param name="duration">The duration of the tween</param>
  public static TweenerCore<float, float, FloatOptions> DOFillAmount(
      this SlicedFilledImage target, float endValue, float duration) {
    if (endValue > 1) {
      endValue = 1;
    } else if (endValue < 0) {
      endValue = 0;
    }

    TweenerCore<float, float, FloatOptions> t =
        DOTween.To(() => target.fillAmount, x => target.fillAmount = x, endValue, duration);

    t.SetTarget(target);

    return t;
  }
}
