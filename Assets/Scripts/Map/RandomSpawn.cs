using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class RandomSpawn : MonoBehaviour {

    public GameObject prefab;
    public GameObject[] list;

    public void Update()
    {
        list = GameObject.FindGameObjectsWithTag("area");
        if (list.Length==1)
        {
            prefabSpawn();
        }

    }

    public void prefabSpawn()
    {
        for (int i = 0; i < 5; i++)
        {

            Instantiate(prefab);
        }
       
    }
    
}
