using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Classroom_WriteData : MonoBehaviour {
    public float logRate = 5f; // how many logs do you want per second
    public string outputFilePath;
    private StreamWriter sw;

    public void OnEnable()
    {
        // log current time
        string time = DateTime.Now.ToString("h:mm").Replace(':', ' ');
        // log current date
        DateTime dateTime = DateTime.UtcNow.Date;
        string date = dateTime.ToString("dd-MM-yyyy");
        // create output path
        outputFilePath = "VirtualClinic_Log_" + date + "_" + time + ".txt";
        // start writing to file
        sw = System.IO.File.AppendText(outputFilePath);
        sw.WriteLine("Data Log for Virtual Clinic for {0} -- {1}", date, DateTime.Now.ToString("h:mm tt"));
        sw.WriteLine("T = time \tx = xRotation \ty = yRotation \tz = zRotation");
        sw.WriteLine("--------------------------------------------------------");
        // continuously track position based on 1 / samplingRate value
        // parameters: what method to call, time, repeatRate
        InvokeRepeating("logLocation", 0, 1 / logRate);
    }

    public void OnDisable()
    {
        // close the stream writer
        sw.Close();
        CancelInvoke();
    }

    public void logLocation()
    {
        // write to text file
        sw.WriteLine("T: {0, 8}\tx: {1, 10}\ty: {2, 10}\tz: {3}", Time.time, transform.rotation.x, transform.rotation.y, transform.rotation.z);
    }
}
