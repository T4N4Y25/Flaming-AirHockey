using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.InputSystem;


public class Score : MonoBehaviour
{
    public GameObject Goal1;
    public GameObject Goal2;
    public GameObject Padel;
    public GameObject AI;
    public GameObject Timer;

    private int PlayerScore = 0;
    private int OpponentScore = 0;
    private int Hits = 1;
    private int OppHits = 1;
    private bool EndOfGame;
    private bool Winner;
    private bool PlayAgain;
    private bool Exit;

    private string ScoreDisplay;
    

    Rigidbody2D rb;
    Rigidbody2D rbPadel;
    Rigidbody2D rbAI;

    Vector2 Restart;
    Vector2 RestartP;
    Vector2 RestartAI;

    Keyboard Retry;



   

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Restart = rb.transform.position; //Inital position of Puck 

        rbPadel = Padel.GetComponent<Rigidbody2D>();
        RestartP = rbPadel.transform.position; //Initial position of Padel

        rbAI = AI.GetComponent<Rigidbody2D>();
        RestartAI = rbAI.transform.position; //Initial position of AI

        EndOfGame = false; //Determines when Game ends
        Winner = false; //Determines winner
        PlayAgain = false; //Determines whether to reset game
        Exit = false; //Determines whether to quit game
        

        Retry = Keyboard.current; //Inputs to choose to replay or quit
    }

    // Update is called once per frame
    void Update()
    {
        ScoreDisplay = $"Player Score : Opponent Score \n"  + $"{PlayerScore}           :         {OpponentScore}"; //Tracks display for current score 

        EndGame(); //Activates when EndOfGame is true
        ScorePenalty(); //Method to recalculate score depending on whether the player/AI hit the puck more than once before the other player hit it

        

    }

    private void FixedUpdate()
    {

        if (Hits < 0 ) //Hits and OppHits tracks the number of hits the player and AI are allowed before they receive a penalty (loss of points), this code prevents negative numbers so only one point is lost at each concsecutive hit
        {
            Hits++; 
        }

        if (OppHits < 0)
        {
            OppHits++;
        }
    }

    private void OnGUI() //Display method used for the score, displaying the winner and displaying whether the player wants to retry or quit
    {

         GUIStyle font = new GUIStyle();
        font.fontStyle = FontStyle.Bold;
        font.normal.textColor = Color.white;
        font.fontSize = 25;
         GUI.TextArea(new Rect(0, 0, 250, 50), ScoreDisplay, font ); //Score Display dsiplays the score during the game; and when the game ends Score Display changes to show the winner and whether the player wants to retry or quit


        if (EndOfGame)
        {
            ScoreDisplay += " Press Enter to play again. \n Press the spacebar to exit"; 
        }
    }

    private void ScorePenalty() //Method for score penalty (also prevents negative scores)
    {
        if (OppHits < 0 && OpponentScore > 0)
        {
            OpponentScore--;
        }

        if (Hits < 0 && PlayerScore > 0)
        {
            PlayerScore--;
        }
            
    }

    private void EndGame() //Activates when EndOfGame is true
    {
        if (PlayerScore == 5 || OpponentScore == 5)
        {
            EndOfGame = true;
        }
        if (EndOfGame)
        {
            if (PlayerScore == 5) //Outputted in GUI() method using Score Display
            {
                Debug.Log("You won the game!");
                Winner = true;
            }

            if (OpponentScore == 5)
            {
                Debug.Log("You lost the game!");
            }
        }

        Padel.SetActive(!EndOfGame); //AI and padel are deactivated when the game ends, the puck is still active as the current script with the End of Game options is on the puck
        AI.SetActive(!EndOfGame);
        

        if (EndOfGame)
        {
          ScoreDisplay = Winner ? $"You won the game!" : "You lost the game!";
            rb.velocity *= 0;
        }

        if (EndOfGame)
        {
            if (Retry.enterKey.isPressed) //Enter is used to replay the game
            {
                PlayAgain = true;

            }

            if (Retry.spaceKey.isPressed) //Spacebar quits the game
            {
                Exit = true;
            }
        }

        if (PlayAgain)
        {
            Replay();
            
        }
        
        
            if (Exit)
            {
                Application.Quit();
            }
        
    }

    private void Replay() //This method resets every important variable and object for the new game
    {
         float Countdown = 6f;
        PlayerScore = 0;
        OpponentScore = 0;
        EndOfGame = false;
        Winner = false;
        PlayAgain = false;
   

        rbPadel.MovePosition(RestartP);
        rbAI.MovePosition(RestartAI);
        rb.MovePosition(Restart);
        rb.velocity *= 0;

        Hits = 1;
        OppHits = 1;

        if (Countdown > 0)
        {
            Countdown -= Time.deltaTime;
            
        }
        ScoreDisplay = $"The Game begins in: {Mathf.Floor(Countdown)}";

        if (Countdown <= 0)
        {
            Padel.SetActive(true);
            AI.SetActive(true);
            ScoreDisplay = $"Player Score : Opponent Score \n" + $"{PlayerScore}           :         {OpponentScore}";
        }

    }


  

    private void OnCollisionEnter2D(Collision2D collision)//Collisions with goals for scoring
    {

        if(Goal2 == collision.gameObject)
        {
            PlayerScore++;
            Hits ++; //Scoring a goal resets the number of hits the player has with the puck
            Debug.Log($"Player score is: {PlayerScore}");

            rb.MovePosition(Restart); //Puck returns to initial position upon a score
         
            rb.velocity *= 0;

            rbPadel.MovePosition(RestartP); //Player and AI also return to initial positions
            rbAI.MovePosition(RestartAI);

        }

        if(Goal1 == collision.gameObject)
        {
            OpponentScore++;
            OppHits ++;
            Debug.Log($"Opponent score is: {OpponentScore}");

            rb.MovePosition(Restart);
            
            rb.velocity *= 0;

            rbPadel.MovePosition(RestartP);
            rbAI.MovePosition(RestartAI);
        }

        if (Padel == collision.gameObject) //This statement decreases the counter for the number of hits the player has taken with the puck
        {
            Hits--;
            if (OppHits < 1) //If the oppoenent hits the puck the player is allowed to hit the puck again without a penalty 
            {
                Hits++;

            }
        }

        if (AI == collision.gameObject)
        {
            OppHits--;
            if (Hits < 1)
            {
                OppHits++;
            }

        }

         

        
    }
}
