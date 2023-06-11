using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private Rigidbody myBody;

    public void Move(float speed)
    {
        myBody.AddForce(transform.forward.normalized * speed); //here normalized word means it returns the magnitude of 1.
        Invoke("DeactivateGameObject", 5f);
    }

    void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            gameObject.SetActive(false );
        }
    }


    void Start()
    {
        myBody = GetComponent<Rigidbody>(); 
    }



    
}
