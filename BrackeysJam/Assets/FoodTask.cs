using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodTask : MonoBehaviour
{
    public float triggerRadius = 2f;

    public GameObject foodCheckmark;

    void Update()
    {
        if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) <= triggerRadius)
        {
            foodCheckmark.SetActive(true);
            // Make a task complete sound
        }
    }
}
