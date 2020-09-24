using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMonkey : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject monkey;
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Rope")
        {
            rb.isKinematic = true;
            monkey.transform.SetParent(col.transform);
            print(col.gameObject.GetComponent<RopeScript>().ropeposition);
            monkey.GetComponent<MonkeyController>().monkeyPlace = col.gameObject.GetComponent<RopeScript>().ropeposition;
            col.gameObject.GetComponent<CapsuleCollider>().enabled = false;
           //col.transform.SetParent(monkey.transform);
        }
    }
}
