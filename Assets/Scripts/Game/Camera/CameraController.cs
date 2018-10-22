using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    private Vector3 offset;

    // initialization
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Llama a actualizar una vez por frame
    void LateUpdate()
    {

        transform.position = player.transform.position + offset;
        
    }
}
