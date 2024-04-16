using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public float MaxSpeed;
    public float Debugtimer = 3f;
    private Rigidbody2D rb;

    private Vector2 Initial;
    private Vector2 End; 

    public GameObject Puck;
    private Rigidbody2D rbPuck;

    public Transform Pucktrans;

    private AIBoundaries.Boundary puckBoundary;

    

    
     
  
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Initial = rb.position;
        rbPuck = Puck.GetComponent<Rigidbody2D>();


        puckBoundary = new AIBoundaries.Boundary(Pucktrans.GetChild(0).position.y, Pucktrans.GetChild(1).position.y, Pucktrans.GetChild(3).position.x, Pucktrans.GetChild(4).position.x);
    }

    private void FixedUpdate()
    {
        float speed;

        if (rbPuck.position.y < puckBoundary.Bottom)
        {
            speed = MaxSpeed * Random.Range(0.1f, 2f);
            End = new Vector2(Mathf.Clamp(rbPuck.position.x, puckBoundary.Left, puckBoundary.Right), rbPuck.position.y);
        }
        else
        {
            speed = MaxSpeed * Random.Range(0.1f, 2f);
            End = new Vector2(Mathf.Clamp(rbPuck.position.x, puckBoundary.Left, puckBoundary.Right),Mathf.Clamp( rbPuck.position.x, puckBoundary.Left, puckBoundary.Right)); //Code sourced from Retro Coder (Youtube) (Reference to source code in Reflection)
            
        }

        if (rbPuck.position.x < puckBoundary.Left || rbPuck.position.x > puckBoundary.Right) //If puck is on players side of the screen the AI will return to its initial position
        {
            speed = MaxSpeed*Random.Range(0.1f, 0.3f);                           
            End = Initial;
        }

        rb.MovePosition(Vector2.MoveTowards(rb.position, End, speed * Time.fixedDeltaTime));

        PuckDebug();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PuckDebug() //Method to fix a few glitches and bugs with the AI and puck (determined from playtesting)
    {
        
        if (rbPuck.position.x > puckBoundary.Right)       //Prevents Puck from being stuck at the top of the screen or to the right of the AI where the AI can not hit it properly
        {
            rbPuck.AddForce(1.5f * -rbPuck.velocity);
        }

        if (rbPuck.position.y > puckBoundary.Top)
        {
            rbPuck.AddForce(1.5f * (-rbPuck.velocity ));
        }

        if (rbPuck.velocity.magnitude ==0 && (Debugtimer != 0)) //If Puck is idle for too long due to the AI potentially not hitting it at all the Puck will return to the intial position
        {
            Debugtimer -= Time.fixedDeltaTime;
        }

        if (Debugtimer <= 0)
        {
            rbPuck.MovePosition(new Vector2(0, 0));
            Debugtimer = 3f;
        }
    }
}
