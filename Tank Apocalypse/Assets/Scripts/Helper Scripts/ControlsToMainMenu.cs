using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsToMainMenu : MonoBehaviour
{
    public void ControlsToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
