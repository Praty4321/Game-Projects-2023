using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    public GameObject bloodFXPrefabs;
    private float speed = 1f;
    public AudioSource zombieCrushSound;
    private Rigidbody myBody;

    private bool isAlive;
    void Start()
    {
        speed = Random.Range(1f, 5f);
        myBody = GetComponent<Rigidbody>();
        zombieCrushSound = GetComponent<AudioSource>();
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            myBody.velocity = new Vector3(0f, 0f, -speed);
        }
        

        if (transform.position.y < -10f) // when ground will shift to forward the zombies who didn't die will fall down and stay in the system, so to remove them we are setting this up.
        {
            gameObject.SetActive (false);
        }
    }

    void Die() //what is happenin here
    {
        isAlive = false;
        myBody.velocity = Vector3.zero;//object will not move anymore
        GetComponent<Collider>().enabled = false; // means disable collider and will not detect collision any more.
        GetComponentInChildren<Animator>().Play("Idle");//coz walk animation is playing by default.

        transform.rotation = Quaternion.Euler(90f, 0f, 0f); //rotation will have a falling on Ground effect on zombie 
        transform.localScale = new Vector3(1f, 1f, 0.2f); //kuchle jane ka effect i.e, we will decrease the size of zombie.
        transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);//setting them on little bit above the ground.
    }

    void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }
    private void OnCollisionEnter(Collision target)
    {
        if(target.gameObject.tag == "Player" || target.gameObject.tag == "Bullet")
        {
            Instantiate(bloodFXPrefabs, transform.position, Quaternion.identity); //instantiate blood at the position of zombie
            zombieCrushSound.Play();
            Invoke("DeactivateGameObject", 3f);

            //Increase Score
            GameplayController.instance.IncreaseScore();

            /* above line written after increseScore function is added to gameplayController.   */ 

            Die();
        }
    }




}//class end
















































