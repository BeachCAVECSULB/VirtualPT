//using MiddleVR_Unity3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Collect_data : MonoBehaviour
{

    public static float shoulderDist;
    public Vector3 Lshoulder; //this.gameObject.transform.GetChild(0);
    public Vector3 Rshoulder; //grandChild = this.gameObject.transform.GetChild(0).GetChild(0).gameObject;
    public static Vector3 box;
    public static Vector3 height;
    public GameObject chest; 
    public GameObject lshould;
    public GameObject rshould;
    public GameObject head;
    // Update is called once per frame
    public void Distance()
    {
      /*  lshould = GameObject.Find("VRManager/LShoulder");
        chest = GameObject.Find("VRManager/Chest");
        head = GameObject.Find("VRManager/Head");
        rshould = GameObject.Find("VRManager/RShoulder");

        vrNode3D Headnode = MiddleVR.VRDisplayMgr.GetNode("HeadNode");
        head.transform.position = MVRTools.ToUnity(Headnode.GetPositionVirtualWorld());
        
        vrNode3D Chestnode = MiddleVR.VRDisplayMgr.GetNode("ChestNode");
        chest.transform.position = MVRTools.ToUnity(Chestnode.GetPositionVirtualWorld());

        vrNode3D LShouldnode = MiddleVR.VRDisplayMgr.GetNode("LShoulderNode");
        lshould.transform.position = MVRTools.ToUnity(LShouldnode.GetPositionVirtualWorld());

        vrNode3D RShouldnode = MiddleVR.VRDisplayMgr.GetNode("RShoulderNode");
        rshould.transform.position = MVRTools.ToUnity(LShouldnode.GetPositionVirtualWorld());


        box = new Vector3(Mathf.Abs(rshould.transform.position.x - lshould.transform.position.x), Mathf.Abs(rshould.transform.position.y - chest.transform.position.y), 0);
        height= new Vector3(0, Mathf.Abs(head.transform.position.y), 0);

        Debug.Log(height);
        Debug.Log(lshould.transform.position.x);*/

    }
}
