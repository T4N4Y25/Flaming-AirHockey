using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
public class PlayerPadel : MonoBehaviour
{
    // Start is called before the first frame update
    private Keyboard key;
    private Rigidbody2D rb;
    public float speed = 1000f;
    public float Enhancer = 5;

    private DirectionCheck direction;

    private struct DirectionCheck   //Struct used to determine the direction the player moves in (used for the speeding up function of the shift button)
    {
        public bool Up ;
        public bool Down ;
        public bool Left ;
        public bool Right ;

        public DirectionCheck(bool up, bool down, bool left, bool right)
        {
            Up = up;
            Down = down;
            Left = left;
            Right = right;
        }
            


    }
    void Start()
    {
        key = Keyboard.current;
        rb = GetComponent<Rigidbody2D>();

       direction = new DirectionCheck(false, false, false, false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (key.shiftKey.isPressed)   //Player moves double the speed when shift is pressed else the player moves normally
        {
            Enhance();

            

        }
        else
        {
            Movement();
        }

        // direction = new DirectionCheck(false, false, false, false);

        QuitGame(); 

    }

    private void QuitGame()//Allows player to quit the game
    {
        if ( key.escapeKey.isPressed)

        {
            Application.Quit(); 
        }
    }

    private void Movement()    //Method assigning movement controls on keyboard and determining the direction player is moving is for the Enhance() method
    {
        Vector2 input = new Vector2(); 

        if (key.wKey.isPressed)
        {
            input += Vector2.up;
            direction.Up = true;
        }
        else
        {
            direction.Up = false;
        }

        if (key.sKey.isPressed)
        {
            input += Vector2.down;
            direction.Down = true;
        }
        else
        {
            direction.Down = false;
        }

        if (key.aKey.isPressed)
        {
            input += Vector2.left;
            direction.Left = true;
            
        }
        else
        {
            direction.Left = false;
        }

        if (key.dKey.isPressed)
        {
            input += Vector2.right;
            direction.Right = true;
        }
        else
        {
            direction.Right = false;
        }

        input = input.normalized;
        //direction = new DirectionCheck(false, false, false, false);
        rb.velocity = input * Time.deltaTime*speed;
    }

    private void Enhance()  //Based on the boolean struct DirectionCheck which determines direction the player can move; double the speed when the shift key is pressed.
    {

         Vector2 Pos = new Vector2();
         

         if (direction.Up)
         {
             Pos = Vector2.up * Enhancer;
         }

         if (direction.Down)
          {
            Pos = Vector2.down * Enhancer;
        }

          if (direction.Left)
          {
            Pos = Vector2.left * Enhancer;
        }

        if (direction.Right)
        {
            Pos = Vector2.right * Enhancer;
        }

        

        rb.velocity=Pos * speed *Time.deltaTime;
        Pos = Pos.normalized;

        //direction = new DirectionCheck(false, false, false, false);




    }
}
