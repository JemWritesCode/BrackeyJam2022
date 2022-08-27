using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KittenTask : MonoBehaviour
{
    public float kittenTriggerRadius = 2f;

    public GameObject kittenCheckmark;

    void Update()
    {
        if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) <= kittenTriggerRadius)
        {
            kittenCheckmark.SetActive(true);
            // Make a task complete sound
        }
    }
}
