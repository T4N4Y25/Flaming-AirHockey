using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject prefabPortal;
    private GameObject Portals;
    private GameObject Puck;

    public Transform position;
    private Position PosStruct;
    private Transform CurrentPos;

    private bool Create;
    private float timer;

    Rigidbody2D rb;
    Rigidbody2D rbPuck;
    Rigidbody2D rbVelocity;
    BoxCollider2D col;

    // Start is called before the first frame update
    void Start()
    {
        PosStruct = new Position(position.GetChild(0),
            position.GetChild(1),
            position.GetChild(2),
            position.GetChild(3),                                   //6 empty children objects are used to indicate the 6 positions that portals are randomly spawned and where the puck exits when they hit a portal
            position.GetChild(4),
            position.GetChild(5));


        Create = true;                                                  //Create = true results in a new portal to be created and the timer to begin counting down, if false a new portal will not be created again until the timer has ended (Create is set back to true when timer ends)
        timer = Random.Range(15,20);                                      //Time for timer in the range of 15 and 20 seconds

        Puck = this.gameObject;
        rbPuck = Puck.GetComponent<Rigidbody2D>();

    }

    private struct Position     //Struct storing the various postions for the portals
    {
        public Transform Pos1, Pos2, Pos3, Pos4, Pos5, Pos6;

        public Position(Transform pos1, Transform pos2, Transform pos3, Transform pos4, Transform pos5, Transform pos6)
        {
            Pos1 = pos1;
            Pos2 = pos2;
            Pos3 = pos3;
            Pos4 = pos4;
            Pos5 = pos5;
            Pos6 = pos6;

        }

    }

    // Update is called once per frame
    void Update()
    {
       if (Create)                    //If the create variable is true a portal will be generated in a random position
        {
            PositionGenerator();
            PortalGenerator();
            
        }

        Timer();

    }

  private void PositionGenerator()             //Determines a random position for the portal by assigning numbers to one of the positions from the struct then randomly generating a number
    {
        int Rand = Random.Range(0, 5);

        switch (Rand)
        {
            case 0:
                CurrentPos = PosStruct.Pos1;
                break;

            case 1:
                CurrentPos = PosStruct.Pos2;
                break;

            case 2:
                CurrentPos = PosStruct.Pos3;
                break;

            case 3:
                CurrentPos = PosStruct.Pos4;
                break;

            case 4:
                CurrentPos = PosStruct.Pos5;
                break;

            case 5:
                CurrentPos = PosStruct.Pos6;
                break;

            default:
                CurrentPos = PosStruct.Pos1;
                break;



        }
    }

    private void PortalGenerator()    //Method creating the portals from a prefab
    {
        
        
            Portals = Instantiate(prefabPortal, CurrentPos.position, Quaternion.identity);
        Portals.layer = LayerMask.NameToLayer("Portals");        //The portals layer only allows the puck to collide with it hence the paddles can ignore the portals


        rb = Portals.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation; //Prevents portals from moving
        
        
         

        col = Portals.AddComponent<BoxCollider2D>();

        

        
            
        

    }

    private void Timer()
    {
        //float timer = Random.Range(15, 21);    Ignore line (caused errors)
        Create = false;
        
        

        if (timer > 0)     //Timer of total time 15-20 seconds counts down
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0) //When timer finishes counting down the current portal is destroyed and create is set to true allowing a new portal to be generated
        {
            Destroy(Portals);
            Create = true;
            timer = Random.Range(15,20);
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject == Portals)   //Puck moves positions to one of the 6 random positions from the struct when colliding with a portal
        {
            Debug.Log("The objects have collided");
            PositionGenerator();
            rbPuck.MovePosition(CurrentPos.position);
            
        }
    }

}
