using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestFriendTask : MonoBehaviour
{
    public float triggerRadius = 2f;

    public GameObject bffCheckmark;

    public AudioClip soundComplete;
    private bool hasPlayed = false;

    private void Start()
    {
        GetComponent<AudioSource>().clip = soundComplete;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) <= triggerRadius)
        {
            bffCheckmark.SetActive(true);
            PlayBestieSoundOnce();
        }
    }

    void PlayBestieSoundOnce()
    {
        if (!hasPlayed)
        {
            AudioSource.PlayClipAtPoint(soundComplete, transform.position);
            hasPlayed = true;
        }
    }
}
