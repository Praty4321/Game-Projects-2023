using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    public Vector3 speed; //overall speed
    public float x_Speed = 8f, y_Speed = 0, z_Speed = 15f;
    public float accelerated = 18f, deccelerated = 12f;//when we start to move faster and then to slow down.
    
    protected float rotationSpeed = 10f; //protected bcoz we will use it in child class PlayerController
    protected float maxAngle = 10f;//maximum angle by which we can roatate the tank.

    public float low_Sound_Pitch, normal_Sound_Pitch, high_Sound_Pitch; //these will be used in combination to stimulate our engine sound of tank.

    public AudioClip engine_ON_Sound, engine_OFF_Sound;
    private bool isSlow; //to determine whether we are slow or not.

    private AudioSource soundManager;

    private void Awake()
    {
        speed = new Vector3 (0f, 0f, z_Speed);
        soundManager = GetComponent<AudioSource>();
    }
    
    protected void MoveLeft()
    {
        speed = new Vector3(-x_Speed / 2f, 0f, speed.z); //devide by 2 coz we dont want it to be very fast movement
    }
    protected void MoveRight()
    {
        speed = new Vector3(x_Speed / 2f, 0f, speed.z);
    }
    protected void MoveStraight()
    {
        speed = new Vector3(0f, 0f, speed.z);
    }

    protected void MoveNormal()
    {
        if (isSlow)
        {
            isSlow = false;

            //soundManager.Stop();
            //soundManager.clip = engine_ON_Sound;
            //soundManager.volume = 0.3f;
            //soundManager.Play();
        }
        speed = new Vector3(speed.x, 0f, z_Speed); ;
    }
    protected void MoveSlow()
    {
        if (!isSlow)
        {
            isSlow = true;

            //soundManager.Stop();
            //soundManager.clip = engine_OFF_Sound;
            //soundManager.volume = 0.5f;
            //soundManager.Play();
        }
        speed = new Vector3(speed.x, 0f, deccelerated);

    }
    protected void MoveFast()
    {
        speed = new Vector3(speed.x, 0f, accelerated);
    }


}//class end






















