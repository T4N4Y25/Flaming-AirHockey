using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckMovement : MonoBehaviour
{
    public GameObject Player;
    public GameObject AI;

    private Rigidbody2D rbPlayer;
    private Rigidbody2D rbAI;

    private Rigidbody2D rb;

    private float PuckSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = Player.GetComponent<Rigidbody2D>();
        rbAI = AI.GetComponent<Rigidbody2D>();

        rb = GetComponent<Rigidbody2D>();

        PuckSpeed = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //Friction(rb.velocity);   Was cauing problems where the Puck would not move when hit hence ignored
    }

    private void Friction(Vector2 Velocity) //Was meant to add friction to the puck for added realism but did not work
    {
        PuckSpeed = Velocity.magnitude;
        
        while (PuckSpeed >0.5f)
        {
            PuckSpeed-= 0.000005f;
        }


    }

    private void OnCollisionEnter2D(Collision2D collision) //Determines direction and speed of Puck when hit by player or AI
    {
        if (Player = collision.gameObject)
        {
            rb.AddForce(-rbPlayer.velocity);
        }

        if (AI = collision.gameObject)
        {
            rb.AddForce(-rbAI.velocity);
        }
    }
}
