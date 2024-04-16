using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBoundaries : MonoBehaviour  //All this code is the same as the "Boundaries" script except the transforms stored are altered to suit the AI's boundaries
{
    public Transform transform;
    Boundary playerBoundary;
    Rigidbody2D rb;
    public struct Boundary
    {
        public float Top, Bottom, Left, Right;


        public Boundary(float top, float bottom, float left, float right)
        {
            Top = top;
            Bottom = bottom;
            Left = left;
            Right = right;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        playerBoundary = new Boundary(transform.GetChild(0).position.y, transform.GetChild(1).position.y, transform.GetChild(3).position.x, transform.GetChild(4).position.x);
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentpos = rb.position;
        float clampedY = Mathf.Clamp(currentpos.y, playerBoundary.Top, playerBoundary.Bottom);
        float clampedX = Mathf.Clamp(currentpos.x, playerBoundary.Left, playerBoundary.Right);

        bool OutsideXL = currentpos.x < playerBoundary.Left;
        bool OutsideXR = currentpos.x > playerBoundary.Right;
        bool OutsideYT = currentpos.y > playerBoundary.Top;
        bool OutsideYB = currentpos.y < playerBoundary.Bottom;

        if (OutsideXL )
        {
            Vector2 stop = new Vector2((clampedX + 0.00001f), currentpos.y);
            rb.MovePosition(stop);
        }

        if (OutsideXR )
        {
            Vector2 stopr = new Vector2((clampedX - 0.00001f), currentpos.y);
            rb.MovePosition(stopr);
        }

       // if (OutsideYT)
       // {
          //  Vector2 stopt = new Vector2(currentpos.x, (clampedY - 0.000001f));           Was causing errors
           // rb.MovePosition(stopt);
      //  }

        if (OutsideYB)
        {
            Vector2 stopb = new Vector2(currentpos.x, clampedX + 0.00000001f);
            rb.MovePosition(stopb);
        }

    }
}
