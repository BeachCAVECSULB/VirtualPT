// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class DrawCircle : MonoBehaviour
// {
//     public Transform centerPoint; // Center of the circle
//     public float radius = 5f; // Radius of the circle
//     public float angleInDegrees = 45f; // Angle in degrees

//     void Start()
//     {
//         float distanceFromJoint = Vector3.Distance(transform.position, jointPosition);

//         // Calculate the position of a point on the circumference based on the angle
//         Vector3 pointOnCircle = CalculatePointOnCircle(centerPoint.position, radius, angleInDegrees);
        
//         // Draw the circle and the point in the Scene view (for visualization purposes)
//         DrawCircle(centerPoint.position, radius);
//         DrawPoint(pointOnCircle);
//     }

//     Vector3 CalculatePointOnCircle(Vector3 center, float radius, float angleInDegrees)
//     {
//         // Convert the angle from degrees to radians
//         float angleInRadians = Mathf.Deg2Rad * angleInDegrees;

//         // Calculate the coordinates of the point on the circumference
//         float x = center.x + radius * Mathf.Cos(angleInRadians);
//         float y = center.y + radius * Mathf.Sin(angleInRadians);
//         float z = center.z; // Assuming the circle is in the xy-plane

//         return new Vector3(x, y, z);
//     }

//     void DrawCircle(Vector3 center, float radius)
//     {
//         // Draw a circle in the Scene view using Debug.DrawLine
//         int segments = 360;
//         float angleIncrement = 360f / segments;

//         for (int i = 0; i < segments; i++)
//         {
//             float startAngle = i * angleIncrement;
//             float endAngle = (i + 1) * angleIncrement;

//             Vector3 startPoint = CalculatePointOnCircle(center, radius, startAngle);
//             Vector3 endPoint = CalculatePointOnCircle(center, radius, endAngle);

//             Debug.DrawLine(startPoint, endPoint, Color.blue, 0);
//         }
//     }

//     void DrawPoint(Vector3 point)
//     {
//         // Draw a point in the Scene view using Debug.DrawRay
//         Debug.DrawRay(point, Vector3.up * 0.1f, Color.red, 0);
//     }
// }
