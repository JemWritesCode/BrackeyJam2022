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
        if (socialBatteryManager.socialBatteryHealthAmount <= 0)
        {
            SceneManager.LoadScene("4-Lose");
        }
    }

    public void subtractForRunAway()
    {
        socialBatteryManager.DecreaseBatteryFill(.25f, 1f);
    }

    public void subtractForMisses(int misses)
    {
        socialBatteryManager.DecreaseBatteryFill(.01f * misses, 1f);
    }

    public void subtractForOverTime()
    {
        Debug.Log("Subtracting over time...");
        socialBatteryManager.DecreaseBatteryFill(.002f, 1f);
    }
}
