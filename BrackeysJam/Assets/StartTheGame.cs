using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTheGame : MonoBehaviour
{
    public void StartTheGameClicked()
    {
        SceneManager.LoadScene("0.5-Tutorial");
    }
}
