using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveObstacle : MonoBehaviour
{
    public GameObject explosionPrefab;
    public int damage = 20;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            /*Deal Damage Here ( line created after making PlayerHealth script) */
            collision.gameObject.GetComponent<PlayerHealth>().ApplyDamage(damage);
            //After creating this line for dealing damage > go to obstacle prefabs and change damage values randomly for all obstacle prefabs.

            gameObject.SetActive(false);
        }

        if (collision.gameObject.tag == "Bullet")
        {
            Instantiate (explosionPrefab, transform.position, Quaternion.identity);
            gameObject.SetActive(false) ;
        }
    }
}
