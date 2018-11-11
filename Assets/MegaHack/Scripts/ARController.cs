using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GoogleARCore;

public class ARController : MonoBehaviour
{
    private List<DetectedPlane> newTrackedPlanes = new List<DetectedPlane>();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Session.Status != SessionStatus.Tracking)
            return;

        Session.GetTrackables<DetectedPlane>(newTrackedPlanes, TrackableQueryFilter.New);
    }
}
