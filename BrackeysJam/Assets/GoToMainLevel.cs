using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMainLevel : MonoBehaviour
{
    public void GoToMainLevelClicked()
    {
        SceneManager.LoadScene("1-MainGame");
    }
}
