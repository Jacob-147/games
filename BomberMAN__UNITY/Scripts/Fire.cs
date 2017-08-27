using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

	// Use this for initialization
	void Start () {

        //zničení ohně
        Destroy(gameObject, 0.5F);
	}

    void Update()
    {
        //efekt ohně
        transform.Rotate(5, 1, -20);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<PowerUpSpawner>() != null)
        {
            GetComponent<CircleCollider2D>().enabled = false;
            collision.gameObject.GetComponent<PowerUpSpawner>().SpawnPowerUps();
        }
        else if (collision.gameObject.GetComponent<Fire>() != null)
        {
            return;
        }
        else if (collision.gameObject.GetComponent<Bomb>() != null)
        {
            collision.gameObject.GetComponent<Bomb>().Explode();
        }

        Destroy(collision.gameObject);

    }
}
