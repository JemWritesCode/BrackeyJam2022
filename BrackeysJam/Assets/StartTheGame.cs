using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTheGame : MonoBehaviour
{
    public void StartTheGameClicked()
    {
        SceneManager.LoadScene("1-MainGame");
    }
}
