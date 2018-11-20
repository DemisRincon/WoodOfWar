using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObject : MonoBehaviour {

    //// Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}

    //[SerializeField]
    //protected GameObject dropPosition;

    [SerializeField]
    private GameObject dropEnemy;

    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private Transform enemyPos;

    private bool dropEnabled = true;

    private void OnTriggerEnter2D(Collider2D Player)
    {
        if (Player.name == "Doran" && dropEnabled)
        {
            GameObject tmp = Instantiate(dropEnemy, enemyPos.position, Quaternion.identity);
            dropEnabled = false;
        }        
    }
}
