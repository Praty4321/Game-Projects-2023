using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target; // yaha player hamara target hai.

    public float distance = 6.3f;
    public float height = 3.5f;

    public float height_Damping = 3.25f;
    public float roatation_Damping = 0.27f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        followPlayer();
    }
    private void followPlayer()
    {
        float wanted_Rotation_angle = target.eulerAngles.y; //player ka rotation angle
        float wanted_Height = target.position.y + height;

        float current_Rotation_angle = transform.eulerAngles.y; // camera ka rotation angle
        float current_Height = transform.position.y;

        current_Rotation_angle = Mathf.LerpAngle(current_Rotation_angle, wanted_Rotation_angle, roatation_Damping * Time.deltaTime); 

        current_Height = Mathf.Lerp(current_Height, wanted_Height, height_Damping * Time.deltaTime);

        Quaternion current_Rotation = Quaternion.Euler(0f, current_Rotation_angle, 0f);
        
        transform.position = target.position;
        transform.position = transform.position - current_Rotation * Vector3.forward * distance;

        transform.position = new Vector3(transform.position.x, current_Height, transform.position.z);


    }   
} // class end
