using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private bool isRed;
    private bool isEightBall = false;
    private bool isCueBall = false;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (rb.velocity.y > 0) 
        {
            Vector3 newVelocity = rb.velocity;
            newVelocity.y = 0f;
            rb.velocity = newVelocity;
        }
    }

    public bool IsBallRed() 
    {
        return isRed;
    }

    public bool IsEightBall() 
    {
        return isEightBall;
    }

    public bool IsCueBall() 
    {
        return isCueBall;
    }

    public void BallSetUp(bool red) 
    {
        isRed = red;
        if (isRed)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else 
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
    }

    public void MakeCueBall() 
    {
        isCueBall = true;   
    }

    public void MakeEightBall() 
    {
        isEightBall = true;
        GetComponent<Renderer>().material.color = Color.black;

    }

}
