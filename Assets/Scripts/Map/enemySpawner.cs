using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour {

    public GameObject prefabCan;
    public GameObject prefabBottle;
    public GameObject prefabBag;
    public GameObject boss;

    private bool spawnBags = true;
    private bool spawnBottles = true;
    private bool spawnCans = true;

    private float time = 0f;
    // Use this for initialization
    void Start () {
        //prefabSpawn();
	}

    private void prefabSpawn(int enemyType)
    {        
        if (enemyType == 1)
        {
            for (int i = 0; i < 1; i++)
            {
                Instantiate(prefabBottle, boss.transform.position, prefabBottle.transform.rotation);
                //time = 0;
            }
        }
        if (enemyType == 2)
        {
            for (int i = 0; i < 1; i++)
            {
                Instantiate(prefabBag, boss.transform.position, prefabBag.transform.rotation);
                //time = 0;
            }
        }
        if (enemyType == 3)
        {
           
            for (int i = 0; i < 1; i++)
            {
                Instantiate(prefabCan, boss.transform.position,prefabCan.transform.rotation);
                time = 0;
            }
        }
    }
 
    float secondsToCount = 30;

    void Update()
    {
        time += Time.deltaTime;
        if (time >= secondsToCount)
        {
            prefabSpawn(3);
            spawnBottles = true;
            spawnBags = true;
            //time = 0;          
        }
        if (time >= 10 && spawnBottles)
        {
            prefabSpawn(1);
            spawnBottles = false;
            //time = 0;          
        }
        if (time >= 20 && spawnBags)
        {
            prefabSpawn(2);
            spawnBags = false;
            //time = 0;          
        }
    }
}
