using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class SplineFollow : MonoBehaviour
{
    [SerializeField] private SplineContainer spline;
    [SerializeField] private float speed = 1;
    float distancePercentege = 0;
    float splineLength;
    void Start()
    {
        splineLength = spline.CalculateLength();
    }

    
    void Update()
    {
        FollowPath();
    }

    private void FollowPath(){
        distancePercentege += speed * Time.deltaTime / splineLength;

        Vector3 currentPosition = spline.EvaluatePosition(distancePercentege);
        transform.position = currentPosition;

        if(distancePercentege > 1f){
            distancePercentege = 0f;
        }

        Vector3 nextPosition = spline.EvaluatePosition(distancePercentege + 0.05f);
        Vector3 direction = nextPosition - currentPosition;
        transform.rotation = Quaternion.LookRotation(direction, transform.up);
    }
}
