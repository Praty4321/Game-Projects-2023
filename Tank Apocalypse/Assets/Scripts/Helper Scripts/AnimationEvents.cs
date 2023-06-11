using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationEvents : MonoBehaviour
{
    // Through this script we will control the shooting
    private PlayerController playerController;
    private Animator anim;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }

    void ResetShooting()
    {
        playerController.canShoot = true;
        anim.Play("Idle"); 
    }

    public void CameraStartGame()
    {
        SceneManager.LoadScene("Gameplay");
    }


}
