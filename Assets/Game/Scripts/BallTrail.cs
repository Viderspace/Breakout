using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTrail : MonoBehaviour
{
    [SerializeField] private GameObject trail1;
    [SerializeField] private GameObject trail2;
    [SerializeField] private GameObject trail3;
    [SerializeField] private GameObject trail4;
    [SerializeField] private GameObject originalBall;
    
    private List<GameObject> trailSegments;
    
    
    public Transform target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;


    private void Start()
    {
        target = originalBall.transform;

        trailSegments = new List<GameObject>
        {
            originalBall,
            trail1,
            trail2,
            trail3,
            trail4
        };
    }

    private void Update()
    {
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 5, -10));
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        // for (int i = 1; i < 5; i++)
        // {
        //     trailSegments[i].transform.localPosition = trailSegments[i - 1].transform.localPosition;
        // }



    }
}
