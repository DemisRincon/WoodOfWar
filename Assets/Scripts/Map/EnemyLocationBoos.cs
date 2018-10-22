using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLocationBoos : MonoBehaviour {
    public GameObject enemy;
    public GameObject boss;
 
    private void Awake()
    {
        //Instantiate(enemy, boss.transform.position, boss.transform.rotation);
        //enemy.transform.position = boss.transform.position;
        //enemy.transform.rotation = boss.transform.rotation;
    }
 
}
