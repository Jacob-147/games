using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public int speed;
    public Rigidbody2D rb2d;   

    // Use this for initialization
    void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        // získání hodnot z klávesnice
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

                
        //nastavení pohybu
        Vector2 movement = new Vector2(x, y) * speed ;
        rb2d.velocity = movement;

    }

}



