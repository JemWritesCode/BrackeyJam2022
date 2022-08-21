using DG.Tweening;

using UnityEngine;

public class TitleTextAnimator : MonoBehaviour {
  [SerializeField]
  private Vector3 _punchScalePunch;

  public Vector3 PunchScalePunch {
    get => _punchScalePunch;
    set => _punchScalePunch = value;
  }

  [SerializeField]
  [Min(0f)]
  private float _punchScaleDuration;

  public float PunchScaleDuration {
    get => _punchScaleDuration;
    set => _punchScaleDuration = value;
  }

  [SerializeField]

  private int _punchScaleVibrato;

  public int PunchScaleVibrato {
    get => _punchScaleVibrato;
    set => _punchScaleVibrato = value;
  }

  [SerializeField]
  [Range(0f, 1f)]
  private float _punchScaleElasticity;

  public float PunchScaleElasticity {
    get => _punchScaleElasticity;
    set => _punchScaleElasticity = Mathf.Clamp(value, 0f, 1f);
  }

  [SerializeField]
  [Min(0f)]
  private float _loopInterval;

  public float LoopInterval {
    get => _loopInterval;
    set => _loopInterval = value;
  }

  void OnValidate() {
    PunchScalePunch = _punchScalePunch;
    PunchScaleDuration = _punchScaleDuration;
    PunchScaleVibrato = _punchScaleVibrato;
    PunchScaleElasticity = _punchScaleElasticity;
    LoopInterval = _loopInterval;
  }

  void Start() {
    Sequence sequence =
        DOTween.Sequence()
            .Append(
                transform.DOPunchScale(PunchScalePunch, PunchScaleDuration, PunchScaleVibrato, PunchScaleElasticity))
            .AppendInterval(LoopInterval);

    sequence.SetLoops(-1, LoopType.Yoyo);
  }
}
