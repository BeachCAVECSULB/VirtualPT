using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class DataManager : MonoBehaviour 
{
	public Slider swingLevelSlider;
    public Slider hapticIntensityLevelSlider;
    public float swingLevel;
    public float hapticIntensityLevel;
    public InputField filenameInputField;
    public string fileName;

	public PendulumMovingScript pendulumScript;
	public float logInterval = 0.02f; // Set your desired time interval here
    public float logDurationPendulum = 0.5f; // Set the total duration for logging
	public float logDurationUser = 1.0f; // Set the total duration for logging

	private int runCount =0;

	private string dataFilePath; // Path to the data file
    private StreamWriter sw; // StreamWriter for writing data to the file
	
	public Vector3 Lshoulder; 
    public Vector3 Rshoulder; 
    public static Vector3 box;
    public static Vector3 height;
    public GameObject chest; 
    public GameObject lshould;
    public GameObject rshould;
    public GameObject head;

	public void Update()
	{
		if (SceneManager.GetActiveScene().name=="MainMenu")
        {
            swingLevel = swingLevelSlider.value;
            hapticIntensityLevel = hapticIntensityLevelSlider.value;
            fileName = filenameInputField.text;
        }
		else if (SceneManager.GetActiveScene().name=="Classroom")
        {
			InitializeVRBody();
            pendulumScript = GameObject.Find("BottomHandle").GetComponent<PendulumMovingScript>();
            pendulumScript.SetWindupAngle(swingLevel);
			lshould = GameObject.Find("VRManager/LShoulder");
			chest = GameObject.Find("VRManager/Chest");
			head = GameObject.Find("VRManager/Head");
			rshould = GameObject.Find("VRManager/RShoulder");
        }
	}

	public void CreateDataFile()
	{
		//print($"SwingLevel: {swingLevel} HapticLevel: {hapticIntensityLevel} FileName: { fileName}");
		string time = System.DateTime.Now.ToString("h:mm").Replace(':', ' ');
		string date = System.DateTime.UtcNow.Date.ToString("dd-MM-yyyy");

		// Create the data file path using the fileName variable
		dataFilePath = $"{fileName}_{date}_{time}.txt";

		// Open the data file for writing
		sw = File.AppendText(dataFilePath);

		sw.WriteLine("Data Log for Virtual Clinic for {0} -- {1}", date, System.DateTime.Now.ToString("h:mm tt"));
		sw.WriteLine("Swing Level: {0}", swingLevel);
		sw.WriteLine("Haptic Feedback Level: {0}", hapticIntensityLevel);
		sw.WriteLine("Filename: {0}", fileName);
		sw.WriteLine("--------------------------------------------------------");
		sw.Close();
	}

    // logs time, rotation of pendulum at a fixed time interval for a specified duration
    public IEnumerator LogPendulumInfoCoroutine()
	{
		sw = File.AppendText(dataFilePath);

		float elapsedTime = 0f;
		string rotationFormat = "0.000";
		runCount++;
		sw.WriteLine(" Run # {0}\n-------------------------------------------------------- ", runCount);
		sw.WriteLine(" Pendulum Position:");
		while (elapsedTime < logDurationPendulum)
		{
			// Round the elapsed time to the nearest 3rd decimal point
			string formattedTime = (Mathf.Round(elapsedTime * 1000f) / 1000f).ToString("0.000");

			string formattedRotation = pendulumScript.pendulum.transform.rotation.eulerAngles.x.ToString(rotationFormat);

			sw.WriteLine("Time: {0, 8}\tXRotation: {1, 10}", formattedTime, formattedRotation);
			yield return new WaitForSeconds(logInterval);
			elapsedTime += logInterval;
		}
		elapsedTime = 0f;
		sw.Close();
	}

	private List<string> locationLog = new List<string>();
    // Coroutine to log the positions of head, chest, and left/right shoulders

    public IEnumerator LogLocationCoroutine()
    {
        float elapsedTime = 0f;
        string positionFormat = "0.000";

        while (elapsedTime < logDurationUser)
        {
            // Round the elapsed time to the nearest 3rd decimal point
            string formattedTime = (Mathf.Round(elapsedTime * 1000f) / 1000f).ToString("0.000");

            string formattedHeadPosition = head.transform.position.ToString(positionFormat);
            string formattedChestPosition = chest.transform.position.ToString(positionFormat);
            string formattedLShoulderPosition = lshould.transform.position.ToString(positionFormat);
            string formattedRShoulderPosition = rshould.transform.position.ToString(positionFormat);

            string logEntry = string.Format("Time: {0, 8}\tHead: {1, 20}\tChest: {2, 20}\tLShoulder: {3, 20}\tRShoulder: {4, 20}",
                formattedTime, formattedHeadPosition, formattedChestPosition, formattedLShoulderPosition, formattedRShoulderPosition);

            locationLog.Add(logEntry);

            yield return new WaitForSeconds(logInterval);
            elapsedTime += logInterval;
        }
		elapsedTime = 0f; 
		sw = File.AppendText(dataFilePath);
		sw.WriteLine(" User Position:");
		foreach (string logEntry in locationLog)
		{
			sw.WriteLine(logEntry);
		}
		sw.Close();
    }

	public void InitializeVRBody()
	{
		// vrNode3D Headnode = MiddleVR.VRDisplayMgr.GetNode("HeadNode");
        // head.transform.position = MVRTools.ToUnity(Headnode.GetPositionVirtualWorld());
        
        // vrNode3D Chestnode = MiddleVR.VRDisplayMgr.GetNode("ChestNode");
        // chest.transform.position = MVRTools.ToUnity(Chestnode.GetPositionVirtualWorld());

        // vrNode3D LShouldnode = MiddleVR.VRDisplayMgr.GetNode("LShoulderNode");
        // lshould.transform.position = MVRTools.ToUnity(LShouldnode.GetPositionVirtualWorld());

        // vrNode3D RShouldnode = MiddleVR.VRDisplayMgr.GetNode("RShoulderNode");
        // rshould.transform.position = MVRTools.ToUnity(LShouldnode.GetPositionVirtualWorld());
        // height = new Vector3(0, Mathf.Abs(head.transform.position.y), 0);

		// Debug.Log(height);
		// Debug.Log(lshould.transform.position.x);
	}
}

// public class Classroom_WriteData : MonoBehaviour {
//     public float logRate = 5f; // how many logs do you want per second
//     public string outputFilePath;
//     private StreamWriter sw;

//     public void OnEnable()
//     {
//         // log current time
//         string time = DateTime.Now.ToString("h:mm").Replace(':', ' ');
//         // log current date
//         DateTime dateTime = DateTime.UtcNow.Date;
//         string date = dateTime.ToString("dd-MM-yyyy");
//         // create output path
//         outputFilePath = "VirtualClinic_Log_" + date + "_" + time + ".txt";
//         // start writing to file
//         sw = System.IO.File.AppendText(outputFilePath);
//         sw.WriteLine("Data Log for Virtual Clinic for {0} -- {1}", date, DateTime.Now.ToString("h:mm tt"));
//         sw.WriteLine("T = time \tx = xRotation \ty = yRotation \tz = zRotation");
//         sw.WriteLine("--------------------------------------------------------");
//         // continuously track position based on 1 / samplingRate value
//         // parameters: what method to call, time, repeatRate
//         InvokeRepeating("logLocation", 0, 1 / logRate);
//     }

//     public void OnDisable()
//     {
//         // close the stream writer
//         sw.Close();
//         CancelInvoke();
//     }

//     public void logLocation()
//     {
//         // write to text file
//         sw.WriteLine("T: {0, 8}\tx: {1, 10}\ty: {2, 10}\tz: {3}", Time.time, transform.rotation.x, transform.rotation.y, transform.rotation.z);
//     }
// }