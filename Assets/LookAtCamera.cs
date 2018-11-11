using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    public Transform phoneTransform;
    public Transform firstPersonCamera;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = phoneTransform.position + Vector3.up * 1f;
        transform.LookAt(firstPersonCamera, Vector3.up);
        transform.Rotate(new Vector3(0f, 180f, 0f), Space.Self);
    }
}
