using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerController : MonoBehaviour
{

    public Transform[] RopeSpawnPoints;
    public GameObject ropePrefab;

    private float ropespawntimer = 1f;
    public float spawnInterval = 1f;

    // Start is called before the first frame update
    void Start()
    {
        ropespawntimer = 1f;
        spawnInterval = 1.2f;
    }

    // Update is called once per frame
    void Update()
    {
        ropespawntimer -= Time.deltaTime;
        if (ropespawntimer < 0)
        {
            ropespawntimer = spawnInterval;
            int randomPoint=Random.Range(0,RopeSpawnPoints.Length);
            GameObject go = Instantiate(ropePrefab, RopeSpawnPoints[randomPoint].position, RopeSpawnPoints[randomPoint].rotation);
            if (randomPoint == 0)
                go.GetComponent<RopeScript>().ropeposition = 1;
            else
                go.GetComponent<RopeScript>().ropeposition = 2;
        }
    }
}
