using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : BaseController
{
    private Rigidbody rb;

    public Transform bullet_StartPoint;
    public GameObject bullet_Prefab;
    public ParticleSystem shootFX;//learn how to use particle system in unity.

    public AudioSource inGameAudio;


    private Animator shootSliderAnim;
    [HideInInspector] public bool canShoot;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inGameAudio = GetComponent<AudioSource>();

        GameObject.Find("Shoot Button").GetComponent<Button>().onClick.AddListener(ShootingControl); //ShootingControl is the funtion that will be executed on clicking this button by using AddListner.
        canShoot = true;
        shootSliderAnim = GameObject.Find("Fire Bar").GetComponent<Animator>();

    }

    private void Update()
    {
        ControlMovementWithKeyboard();
        ChangeRotation();
    }

    private void FixedUpdate() //just like Update just called after some time interval that can be changed in project settings.
    {
        
        MoveTank();
    }
    private void MoveTank()
    {
        rb.MovePosition(rb.position + speed * Time.deltaTime); //clear why Time.deltaTime is used
    }

    private void ControlMovementWithKeyboard()
    {
        if(Input.GetKey(KeyCode.LeftArrow) ||  Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }
        if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            MoveFast();
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            MoveSlow();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            MoveStraight();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            MoveStraight();
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            MoveNormal();
        }
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            MoveNormal();
        }
    }

    void ChangeRotation()
    {
        if(speed.x > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.Euler(0f, maxAngle, 0f), rotationSpeed * Time.deltaTime);
        } else if (speed.x < 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.Euler(0f, -maxAngle, 0f), rotationSpeed * Time.deltaTime);
        } else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.Euler(0f, 0f, 0f), rotationSpeed * Time.deltaTime);

        }
    }

    public void ShootingControl()
    {
        if(Time.timeScale != 0)
        {
            if(canShoot)
            {
                GameObject bullet = Instantiate(bullet_Prefab, bullet_StartPoint.position,
                Quaternion.identity);
                bullet.GetComponent<BulletScript>().Move(2000f);
                shootFX.Play();

                canShoot = false;
                shootSliderAnim.Play("Fill");


            }
        }
    }



}//class end

















