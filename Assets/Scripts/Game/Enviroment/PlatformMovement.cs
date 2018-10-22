using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{

    //Posición de inicio
    private Vector3 posA;

    //Posiciòn de inicio
    private Vector3 posB;
    
    //Indica cual será la siguiente posción (elige entre A y B dependiendo en cual fue la ultima)
    private Vector3 nextPos;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform childTransform;

    [SerializeField]
    private Transform transformB;

    private bool startMovement=false;

    [SerializeField]
	private bool hasDetector = false;

    // Use this for initialization
    void Start ()
    {
        posA = childTransform.localPosition;
        posB = transformB.localPosition;
        nextPos = posB;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (startMovement || hasDetector == false)
        {
            Move();
        }
	}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            startMovement = true;
        }
    }

    private void Move()
    {
        //función que hace que se mueva a la siguiente posición
        childTransform.localPosition = Vector3.MoveTowards(childTransform.localPosition, nextPos, speed * Time.deltaTime);

        if (Vector3.Distance(childTransform.localPosition, nextPos) <=0.1)                                                            
        {
            ChangeDestination();
        }
    }

    private void ChangeDestination()
    {
        nextPos = nextPos != posA ? posA : posB;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.layer = 11;
            other.transform.SetParent(childTransform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        other.transform.SetParent(null);
    }
}
