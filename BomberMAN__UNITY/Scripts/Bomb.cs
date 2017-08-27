using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    internal int firePower;
    internal float fuse;
    public GameObject fire;
    GameController gc;

    Vector3[] directions = new Vector3[]
 {
        Vector3.up,
        Vector3.down,
        Vector3.left,
        Vector3.right
 };


    // Use this for initialization
    void Start () {
        Invoke("Explode", fuse);
        gc = GameObject.Find("GameController").GetComponent<GameController>();

    }

    private void Update()
    {
        //efekty bomby
        transform.localScale += new Vector3(0.0002F, 0.0002F, 0);
        transform.Rotate(0, 0, 0.17F);
        
    }

    // Update is called once per frame
    public void Explode () {


        //spawnutí ohně
        Instantiate(fire, transform.position, Quaternion.identity);
        foreach (var direction in directions)
        {
            SpawnFire(direction);
        }

        //zničení ohně
        Destroy(gameObject);
	}

    private void SpawnFire(Vector3 offset, int fire=1)
    {
        int x = (int)transform.position.x + (int)offset.x * fire;
        int y = (int)transform.position.y + (int)offset.y * fire;


        if (gc.level[x, y] == null && fire < firePower)
        {
            Instantiate(this.fire, transform.position + (offset * fire), Quaternion.identity);
            SpawnFire(offset,++fire);
        }
        else if(fire < firePower)
        {
            if (gc.level[x, y] != null && gc.level[x, y].tag == "Destroyable")
            {          
                Instantiate(this.fire, transform.position + (offset * fire), Quaternion.identity);
            }
        }

    }
    

    //zapnutí kolize na bombě
    public void OnTriggerExit2D(Collider2D collision)
    {
        GetComponent<CircleCollider2D>().isTrigger = false;

    }

}