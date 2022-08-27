using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodTask : MonoBehaviour
{
    public float triggerRadius = 2f;

    public GameObject foodCheckmark;

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
            foodCheckmark.SetActive(true);
            // Make a task complete sound
            PlayFoodSoundOnce();
        }
    }

    void PlayFoodSoundOnce()
    {
        if (!hasPlayed)
        {
            AudioSource.PlayClipAtPoint(soundComplete, transform.position);
            hasPlayed = true;
        }
    }
}
