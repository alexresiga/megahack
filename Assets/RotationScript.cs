using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour {

    public bool rotating;
    public Canvas infoCanvas;

    public void ChangeRotation()
    {
        if (rotating)
            infoCanvas.enabled = false;
        else
            infoCanvas.enabled = true;
        rotating = !rotating;
    }

	// Use this for initialization
	void Start () {
        rotating = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (rotating)
        {
            transform.Rotate(Vector3.up, Time.deltaTime * 30f, Space.Self);
            // transform.Rotate(0f, 0f, Time.deltaTime * 30f);
        }
	}
}
