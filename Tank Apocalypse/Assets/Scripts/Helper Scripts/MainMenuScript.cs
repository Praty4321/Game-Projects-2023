using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public Animator cameraAnim;

    public void PlayGame()
    {
        cameraAnim.Play("SlidingOfCamera");
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
     public void ShowControls()
    {
        SceneManager.LoadScene("Controls");
    }
}
