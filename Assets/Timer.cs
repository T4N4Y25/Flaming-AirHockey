using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameObject Player;
    public GameObject AI;
    public GameObject Puck;
    private bool startgame;
    private float Countdown;

    // Start is called before the first frame update
    void Start()
    {
        startgame = false;
        Countdown = 6f;
    }



    // Update is called once per frame
    void Update()
    {
        if (Countdown > 0)
        {
            Countdown -= Time.deltaTime;
        }                                                                 //Game objects for the game (Player, Ai and the Puck) are activated when the timer ends and hence the game begins when the countdown ends

        if(Countdown <= 0)
        {
            startgame = true;
        }

        Player.SetActive(startgame);
        Puck.SetActive(startgame);
        AI.SetActive(startgame);
        this.gameObject.SetActive(!startgame);

    }

    private void OnGUI()
    {
        GUIStyle font = new GUIStyle();
        font.fontStyle = FontStyle.Bold;
        font.normal.textColor = Color.white;                                            //Timer display
        font.fontSize = 25;
        GUI.TextArea(new Rect(0, 0, 250, 50), $"Game begins in {Mathf.Floor(Countdown)}", font);

        GUIStyle font2 = new GUIStyle();
        font2.fontStyle = FontStyle.Bold;                                           //Text explaining simple rules of the game
        font2.normal.textColor = Color.white;
        font2.fontSize = 15;
        GUI.TextArea(new Rect(100, 100, 250, 50), "Use W,A,S,D to move.\n The shift button will accelerate you but only orthogonally." +
            "\n Hitting the puck more than once before the other player hits it will result in a loss of points.\n " +
            "Portal will randomly spawn in the area which can teleport the puck. \n The first player to 5 points wins! \n " +
            "Press ESC at any time to quit the game."
            , font2);
    }
}
