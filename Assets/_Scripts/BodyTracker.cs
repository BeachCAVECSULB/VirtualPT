using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyTracker : MonoBehaviour
{
    /*     
    tracks patients body using ArtDTrack2 system and updates body nodes accordingly

    https://github.com/ar-tracking/UnityDTrackPlugin?tab=readme-ov-file#dtrackidentifyingbodyids
    Instructions:

    make sure to set the listen port number on dtracksource
    each mocap point has a body id
    update the body ids for all the body nodes according to the DTrack Standard Bodies
    on the DTrack software 
    */
}

// public Vector3 Lshoulder; 
// public Vector3 Rshoulder; 
// public static Vector3 box;
// public static Vector3 height;
// public GameObject chest; 
// public GameObject lshould;
// public GameObject rshould;
// public GameObject head;

// public void InitializeVRBody()
// {
//     vrNode3D Headnode = MiddleVR.VRDisplayMgr.GetNode("HeadNode");
//     head.transform.position = MVRTools.ToUnity(Headnode.GetPositionVirtualWorld());
    
//     vrNode3D Chestnode = MiddleVR.VRDisplayMgr.GetNode("ChestNode");
//     chest.transform.position = MVRTools.ToUnity(Chestnode.GetPositionVirtualWorld());

//     vrNode3D LShouldnode = MiddleVR.VRDisplayMgr.GetNode("LShoulderNode");
//     lshould.transform.position = MVRTools.ToUnity(LShouldnode.GetPositionVirtualWorld());

//     vrNode3D RShouldnode = MiddleVR.VRDisplayMgr.GetNode("RShoulderNode");
//     rshould.transform.position = MVRTools.ToUnity(LShouldnode.GetPositionVirtualWorld());
//     height = new Vector3(0, Mathf.Abs(head.transform.position.y), 0);

//     Debug.Log(height);
//     Debug.Log(lshould.transform.position.x);
// }
