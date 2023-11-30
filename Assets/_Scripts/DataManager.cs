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
    public float logDuration = 0.5f; // Set the total duration for logging

	private string dataFilePath; // Path to the data file
    private StreamWriter sw; // StreamWriter for writing data to the file

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
            pendulumScript = GameObject.Find("BottomHandle").GetComponent<PendulumMovingScript>();
            pendulumScript.SetWindupAngle(swingLevel);
        }
	}

	public void CreateDataFile()
	{
		//print($"SwingLevel: {swingLevel} HapticLevel: {hapticIntensityLevel} FileName: { fileName}");

		// Log current date and time
		string time = System.DateTime.Now.ToString("h:mm").Replace(':', ' ');
		string date = System.DateTime.UtcNow.Date.ToString("dd-MM-yyyy");

		// Create the data file path using the fileName variable
		dataFilePath = $"{fileName}_{date}_{time}.txt";

		// Open the data file for writing
		sw = File.AppendText(dataFilePath);

		// Write header information
		sw.WriteLine("Data Log for Virtual Clinic for {0} -- {1}", date, System.DateTime.Now.ToString("h:mm tt"));
		sw.WriteLine("Swing Level: {0}", swingLevel);
		sw.WriteLine("Haptic Feedback Level: {0}", hapticIntensityLevel);
		sw.WriteLine("Filename: {0}", fileName);
		sw.WriteLine("--------------------------------------------------------");
		sw.Close();
	}
// add other information to be collected to txt file
// (gather user position, pendulum rotation, and pendulum speed) - during pendulum moving state and 2 seconds after 
// change the collect data button to just create the file with the specific information

    // logs time, rotation of pendulum at a fixed time interval for a specified duration
    public IEnumerator LogPendulumInfoCoroutine()
	{
		sw = File.AppendText(dataFilePath);

		float elapsedTime = 0f;
		string rotationFormat = "0.000"; // Format specifier for rotation

		while (elapsedTime < logDuration)
		{
			// Round the elapsed time to the nearest 3rd decimal point
			string formattedTime = (Mathf.Round(elapsedTime * 1000f) / 1000f).ToString("0.000");

			string formattedRotation = pendulumScript.pendulum.transform.rotation.eulerAngles.x.ToString(rotationFormat);

			sw.WriteLine("Time: {0, 8}\tXRotation: {1, 10}", formattedTime, formattedRotation);
			yield return new WaitForSeconds(logInterval);
			elapsedTime += logInterval;
		}
		// Close the StreamWriter after logging for the specified duration
		sw.Close();
	}

	//logs location of user
	public void LogLocation()
	{

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