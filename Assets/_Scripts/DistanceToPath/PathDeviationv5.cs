//Vinicius- I commented this script out because it looks to be a drone script that was brought over to do the haptics of this project and there are missing dependencies
// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Events;

// public class PathDeviationv5 : MonoBehaviour
// {
//     public GameObject waypointCircuit;
//     public GameObject droneBody;
//     public float maxAllowedDeviation = 500f;
//     public float warningFrequencyInSeconds = 10f;
//     public UnityEvent onPathDeviation;
//     public UnityEvent onTooFarRight;
//     public UnityEvent onTooFarLeft;

//     // could rewrite everything to use Vector2 instead of Vector3, would cut down 33% of memory usage if paths are large
//     private Vector3[] points;
//     private Vector3[] forwardSteps;
//     private Vector3[] backwardSteps;
//     private Quaternion[] rotations;
//     private int nearestPointIndex;
//     private Transform droneTransform;
//     private bool warningOnCooldown;

//     // Start is called before the first frame update
//     void Start()
//     {
//         droneTransform = droneBody.GetComponent<Transform>();
//         var wc = waypointCircuit.GetComponent<WaypointCircuit>();
//         CachePoints(wc.Waypoints);
//         nearestPointIndex = 0;
//         warningOnCooldown = false;
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         var fixedDronePos = droneTransform.position;
//         fixedDronePos.y = 0;
//         nearestPointIndex = GetNearestPointIndex(nearestPointIndex, fixedDronePos);

//         var aheadOfNearestPoint = DistSq2D(forwardSteps[nearestPointIndex], fixedDronePos)
//                                   < DistSq2D(backwardSteps[nearestPointIndex], fixedDronePos);

//         var nearestSegmentPointIndex = aheadOfNearestPoint
//             ? (nearestPointIndex + 1 + points.Length) % points.Length
//             : (nearestPointIndex - 1 + points.Length) % points.Length;

//         var dist = DistanceToLine(points[nearestPointIndex],
//             points[nearestSegmentPointIndex],
//             fixedDronePos);

//         if (!warningOnCooldown && dist > maxAllowedDeviation)
//         {
//             DeviatedFromPath();

//             // find vector from nearest point the drone is ahead of, to the drone. orient relative to the point to get drone left/right of path information
//             var nearestPointAheadOf =
//                 aheadOfNearestPoint ? nearestPointIndex : nearestSegmentPointIndex;
//             var lookVector = points[nearestPointAheadOf+1] - points[nearestPointAheadOf];
//             var pointRotation = Quaternion.LookRotation(lookVector).eulerAngles.y;
//             //var pointRotNeg = 0 - rotations[nearestPointAheadOf].eulerAngles.y;
//             var pointToDrone = fixedDronePos - points[nearestPointAheadOf];
//             pointToDrone = Quaternion.Euler(0, 0 - pointRotation, 0) * pointToDrone;
//             var isPositiveDegrees = pointToDrone.x > 0;
//             var angleDiff = Mathf.Abs(droneTransform.eulerAngles.y - pointRotation);
//             var facingForward = angleDiff < 90 || angleDiff > 270;
            
//             if (isPositiveDegrees && facingForward || !isPositiveDegrees && !facingForward)
//             {
//                 PathOnLeft();
//             }
//             else
//             {
//                 PathOnRight();
//             }

//             StartCoroutine(WarningCooldown(warningFrequencyInSeconds));
//         }
//     }

//     /// <summary>
//     /// Caches a given array of 3D transforms
//     /// </summary>
//     /// <param name="waypoints">Array of transforms to cache</param>
//     private void CachePoints(Transform[] waypoints)
//     {
//         points = new Vector3[waypoints.Length];
//         rotations = new Quaternion[waypoints.Length];
//         for (var i = 0; i < waypoints.Length; i++)
//         {
//             points[i] = waypoints[i].position;
//             points[i].y = 0;
//             rotations[i] = waypoints[i].rotation;
//         }

//         forwardSteps = new Vector3[points.Length];
//         backwardSteps = new Vector3[points.Length];
//         for (var i = 0; i < points.Length; i++)
//         {
//             // step = point + unit vector in direction of next/prev point
//             forwardSteps[i] =
//                 points[i] + Vector3.Normalize(points[(i + 1 + points.Length) % points.Length] - points[i]);
//             backwardSteps[i] =
//                 points[i] + Vector3.Normalize(points[(i - 1 + points.Length) % points.Length] - points[i]);
//         }
//     }

//     /// <summary>
//     /// Returns the squared 2D distance between 2 vectors
//     /// </summary>
//     /// <param name="a">Vector3</param>
//     /// <param name="b">Vector3</param>
//     /// <returns>Squared distance in 2D space</returns>
//     private float DistSq2D(Vector3 a, Vector3 b)
//     {
//         return (a.x - b.x) * (a.x - b.x) + (a.z - b.z) * (a.z - b.z);
//     }

//     /// <summary>
//     /// Returns the index of the nearest point to the body.\
//     /// </summary>
//     /// <param name="lastNearestPointIndex">int index of the most recent nearest point</param>
//     /// <param name="body">Vector3 to calculate the nearest point to</param>
//     /// <returns>int index of the nearest point</returns>
//     private int GetNearestPointIndex(int lastNearestPointIndex, Vector3 body)
//     {
//         // not sure if assigning these distances to vars or calculating them at comparison is faster
//         var next = DistSq2D(points[(lastNearestPointIndex + 1 + points.Length) % points.Length], body);
//         var prev = DistSq2D(points[(lastNearestPointIndex - 1 + points.Length) % points.Length], body);
//         if (next < prev)
//         {
//             if (next < DistSq2D(points[lastNearestPointIndex], body))
//             {
//                 return (lastNearestPointIndex + 1 + points.Length) % points.Length;
//             }
//         }
//         else
//         {
//             if (prev < DistSq2D(points[lastNearestPointIndex], body))
//             {
//                 return (lastNearestPointIndex - 1 + points.Length) % points.Length;
//             }
//         }

//         return lastNearestPointIndex;
//     }

//     /// <summary>
//     /// Returns the distance between a body (c) and a line (ab)
//     /// </summary>
//     /// <param name="a">Vector3 line point 1</param>
//     /// <param name="b">Vector3 line point 2</param>
//     /// <param name="c">Vector3 body</param>
//     /// <returns>float distance from line</returns>
//     private float DistanceToLine(Vector3 a, Vector3 b, Vector3 c)
//     {
//         // lhs = vector AC
//         // rhs = e = normalized vector AB
//         return Vector3.Cross(c - a, Vector3.Normalize((b - a))).magnitude;
//     }

//     /// <summary>
//     /// Initiates a cool down period for the warning message. Duration in seconds.
//     /// </summary>
//     /// <param name="duration">CD in seconds</param>
//     /// <returns></returns>
//     private IEnumerator WarningCooldown(float duration)
//     {
//         warningOnCooldown = true;
//         yield return new WaitForSeconds(duration);
//         warningOnCooldown = false;
//     }

//     private void DeviatedFromPath()
//     {
//         TactorLogger.Debug("TOO FAR");
//         onPathDeviation.Invoke();
//     }

//     private void PathOnLeft()
//     {
//         TactorLogger.Debug("PATH TO YOUR LEFT");
//         onTooFarRight.Invoke();
//     }

//     private void PathOnRight()
//     {
//         TactorLogger.Debug("PATH TO YOUR RIGHT");
//         onTooFarLeft.Invoke();
//     }
// }