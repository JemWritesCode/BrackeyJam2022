using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestFriendTask : MonoBehaviour
{
    public float triggerRadius = 2f;

    [SerializeField] GameObject bffCheckmark;

    private void Start()
    {
        //bffCheckmark = GameObject.Find("1.5-GameUIScene/Canvas/TasksToComplete/TasksPanel/TaskRow.BFF/CompleteToggle/Background/BFF.Checkmark");
        bffCheckmark.SetActive(false);
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) <= triggerRadius)
        {
            bffCheckmark.SetActive(true);
        }
    }
}
