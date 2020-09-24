using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyController : MonoBehaviour
{
    public Rigidbody rb;

    public int monkeyPlace;
    public bool inMotion;
    public float dragmultiplier;

    void Start()
    {
        dragmultiplier = 1f;
        inMotion = false;
        monkeyPlace = 1;
    }

    public void LeftPad()
    {
        if (monkeyPlace == 2)
        {
            //jump to left side
            
            gameObject.transform.parent = null;
            rb.isKinematic = false;
            rb.AddForce(-200 , 300 , 0);
            rb.AddTorque(0, -90, 0);
            
        }
        else
        {
            gameObject.transform.parent = null;
            rb.isKinematic = false;
            rb.AddForce(0, 300 , 0);
            rb.AddTorque(0, 0, 0);
        }
        monkeyPlace = 1;
        inMotion = true;
    }

    public void RightPad()
    {
        if (monkeyPlace == 1)
        {
            
            gameObject.transform.parent = null;
            rb.isKinematic = false;
            rb.AddForce(200 , 300 , 0);
            rb.AddTorque(0, 90, 0);
            
        }
        else
        {
            gameObject.transform.parent = null;
            rb.isKinematic = false;
            rb.AddForce(0, 300, 0);
            rb.AddTorque(0, 0, 0);
        }
        monkeyPlace = 2;
        inMotion = true;
    }
}
