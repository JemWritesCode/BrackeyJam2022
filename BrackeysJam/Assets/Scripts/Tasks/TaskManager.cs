using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public GameObject bestFriend;
    public BestFriendTask bestFriendTask;

    public GameObject foodPlatter;
    public FoodTask foodTask;

    public GameObject bunchOfKittens;
    public KittenTask kittenTask;

    void Start()
    {
        SetupFoodTask();
        SetupBestFriendTask();
        SetupFindKittensTask();
    }

    public void SetupBestFriendTask()
    {
        bestFriend = GameObject.Find("BestFriend");
        bestFriendTask = bestFriend.GetComponent<BestFriendTask>();
        bestFriendTask.bffCheckmark = GameObject.Find("BFF.Checkmark");
        bestFriendTask.bffCheckmark.SetActive(false);
    }

    public void SetupFoodTask()
    {
        foodPlatter = GameObject.Find("FoodPlatter");
        foodTask = foodPlatter.GetComponent<FoodTask>();
        foodTask.foodCheckmark = GameObject.Find("Food.Checkmark");
        foodTask.foodCheckmark.SetActive(false);
    }

    public void SetupFindKittensTask()
    {
        bunchOfKittens = GameObject.Find("MainKitten");
        kittenTask = bunchOfKittens.GetComponent<KittenTask>();
        kittenTask.kittenCheckmark = GameObject.Find("kit.check");
        kittenTask.kittenCheckmark.SetActive(false);
    }

}
