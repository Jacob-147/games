using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    public GameObject bomb;
    public int firePower;
    public int numberOfBombs;
    public float fuse;
	
	// Update is called once per frame
	void Update ()
    {
        //spawnutí bomby
        if(Input.GetButtonDown("Jump") && numberOfBombs >= 1)
        {
            Vector2 spawnPosition = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));

            var newBomb = Instantiate(bomb, spawnPosition, Quaternion.identity) as GameObject;
            newBomb.GetComponent<Bomb>().firePower = firePower;
            newBomb.GetComponent<Bomb>().fuse = fuse;

            numberOfBombs--;

            //přidání bomby po určitém čase
            Invoke("AddBomb", fuse);     
        }
}
    
    public void AddBomb()
    {
        numberOfBombs++;
    }

}
