using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KittenTask : MonoBehaviour
{
    public float kittenTriggerRadius = 2f;

    public GameObject kittenCheckmark;

    public AudioClip soundComplete;
    private bool hasPlayed = false;

    private void Start()
    {
        GetComponent<AudioSource>().clip = soundComplete;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) <= kittenTriggerRadius)
        {
            kittenCheckmark.SetActive(true);
            PlayKittenSoundOnce();
            
        }
    }

    void PlayKittenSoundOnce()
    {
        if (!hasPlayed)
        {
            AudioSource.PlayClipAtPoint(soundComplete, transform.position);
            hasPlayed = true;
        }
    }
}
