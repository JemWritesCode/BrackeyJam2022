using System.Collections;

using UnityEngine;

public class BestFriendTask : MonoBehaviour
{
    public float triggerRadius = 2f;

    public GameObject bffCheckmark;

    public AudioClip soundComplete;
    private bool _hasPlayed = false;

    [field: SerializeField]
    public bool BestieTaskCompleted { get; private set; } = false;

    private void Start()
    {
        GetComponent<AudioSource>().clip = soundComplete;
        StartCoroutine(WaitForPlayerToLoad());
    }

    private Transform _playerTransform;

    private IEnumerator WaitForPlayerToLoad() {
      while (true) {
        yield return null;

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player) {
          Debug.Log($"Found Player!");
          _playerTransform = player.transform;
          yield break;
        }
      }
    }

    void Update()
    {
        if (!_playerTransform)
        {
            return;
        }

        if (!BestieTaskCompleted
            && Vector3.Distance(transform.position, _playerTransform.position) <= triggerRadius)
        {
            Debug.Log($"BFF Task, my position: {transform.position}, player position: {_playerTransform.position}");
            bffCheckmark.SetActive(true);
            PlayBestieSoundOnce();
            BestieTaskCompleted = true;
        }
    }

    void PlayBestieSoundOnce()
    {
        if (!_hasPlayed)
        {
            AudioSource.PlayClipAtPoint(soundComplete, transform.position);
            _hasPlayed = true;
        }
    }
}
