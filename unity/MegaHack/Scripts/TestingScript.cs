using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour {
    public GameObject AndyPlanePrefab;

    // Use this for initialization
    void Start () {
        // Instantiate three phone models starting at the hit pose.
        //var firstTransform = Vector3.zero + Vector3.up * AndyPlanePrefab.transform.localScale.z / 2;
        //var rot = Quaternion.Euler(-120, 45, 0);

        //var firstPhone = Instantiate(AndyPlanePrefab, firstTransform, rot);
        //var secondPhone = Instantiate(AndyPlanePrefab, firstPhone.transform.right * 0.1f, rot);
        //var thirdPhone = Instantiate(AndyPlanePrefab, firstTransform + Vector3.right * 0.2f, rot);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
