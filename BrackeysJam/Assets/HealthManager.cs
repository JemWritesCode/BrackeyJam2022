using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public GameObject socialBattery;
    public SocialBatteryManager socialBatteryManager;

    void Start()
    {
        socialBattery = GameObject.Find("SocialBattery");
        socialBatteryManager = socialBattery.GetComponent<SocialBatteryManager>();
    }

    private void Update()
    {
        HandleLoss();
    }

    void HandleLoss()
    {
        // if social battery reaches 0
        if (socialBatteryManager.socialBatteryHealthAmount <= 0)
        {
            SceneManager.LoadScene("4-Lose");
        }
    }

    void GoToWinScreen()
    {
        // if all the tasks are completed and they go to the exit door
    }

}
