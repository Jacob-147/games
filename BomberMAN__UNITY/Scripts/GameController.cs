using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //score - proměné
    public Text score;
    public static int scoreCount = 0;
    public Text score2;
    public static int scoreCount2 = 0;
    public GameObject playerWin;
    public GameObject player2Win;
    public GameObject playerPat;


    public GameObject levelHolder;
    public const int X = 26;
    public const int Y = 16;
    public GameObject[,] level = new GameObject[X, Y];


    void Update()
    {
        if (GameObject.Find("Player1") == null && GameObject.Find("Player2") != null)
        {
            player2Win.SetActive(true);

            if (Input.GetButtonDown("Fire1"))
            {
                scoreCount += 1;
                score.text = "";
                score.text += scoreCount;
                Invoke("ReloadLevel", 0.5f);
            }
        }
        else if (GameObject.Find("Player2") == null && GameObject.Find("Player1") != null)
        {
            playerWin.SetActive(true);

            if (Input.GetButtonDown("Fire1"))
            {

                scoreCount2 += 1;
                score2.text = "";
                score2.text += scoreCount2;
                Invoke("ReloadLevel", 0.5f);
            }
        }
        else if (GameObject.Find("Player2") == null && GameObject.Find("Player1") == null)
        {
            playerPat.SetActive(true);
            player2Win.SetActive(false);
            playerWin.SetActive(false);
            if (Input.GetButtonDown("Fire1"))
            {
                Invoke("ReloadLevel", 0.5f);
            }
        }


    }
    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Use this for initialization
    void Start()
    {
        //score - získání textového řetezce
        score = GameObject.Find("Player1Score").GetComponent<Text>();
        score.text = "";
        score2 = GameObject.Find("Player2Score").GetComponent<Text>();
        score2.text = "";
        //obnovení skore po smrti
        score.text += scoreCount;
        score2.text += scoreCount2;


        LevelScan();
    }

    public void LevelScan()
    {
        var objects = levelHolder.GetComponentsInChildren<Transform>();

        foreach (var child in objects)
        {
            level[(int)child.position.x, (int)child.position.y] = child.gameObject;
        }

        
    }
}
