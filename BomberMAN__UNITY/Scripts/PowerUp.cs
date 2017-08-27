using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    public int bombs;
    public int firePower;
    GameController gameController;

	// Use this for initialization
	void Start () {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        gameController.level[(int)transform.position.x, (int)transform.position.y] = gameObject;
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //přidání hráči bomb, síly ohně z perků
        if (collision.gameObject.tag == "Player")
        {
            BombSpawner bombSpawner = collision.gameObject.GetComponent<BombSpawner>();
            bombSpawner.numberOfBombs += bombs;
            bombSpawner.firePower += firePower;
            Destroy(gameObject);

        }
        else if(collision.gameObject.tag == "Player2")
        {
            Bomb2Spawner bombSpawner = collision.gameObject.GetComponent<Bomb2Spawner>();
            bombSpawner.numberOfBombs += bombs;
            bombSpawner.firePower += firePower;
            Destroy(gameObject);
        }
    }
}
