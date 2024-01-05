using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using System.Linq;

#if UNITY_EDITOR
using UnityEditorInternal;
using UnityEditor;
using UnityEngine.UIElements;
#endif

public class TactorManager : MonoBehaviour
{
    
    public static int tactorCount = 8;
    public List<TactorData> tactorDatas = new List<TactorData>();
    
    public float searchDelayInSeconds = 3f;
    public bool testDeviceOnStartup = false;
    private int[] recentlyFired;
    private TactorController tc;

    // class for holding data about each tactor
    [Serializable]
    public class TactorData
    {
        public int tactorIndex = 0;
        [Range(300, 3550)]
        public int frequency = 1300;
        [Range(0, 255)]
        public int gain = 128;
        public int durationInMs = 150;
        public float delayInSeconds = 0;
    }

    void Start()
    {
        tc = GetComponent<TactorController>();
        recentlyFired = new int[tactorCount];
        SetDefaultTactorData();
        // PrintAllTactorData();
        StartCoroutine(AutoSetup());
    }

    // Check for key inputs to initiate coroutines and fire tactors
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TactorLogger.Debug("TAS: Fire First Tactor Once");
            FireTactor(tactorDatas[0].tactorIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TactorLogger.Debug("TAS: Fire all Tactors Once");
            StartCoroutine(FireAllOnce());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TactorLogger.Debug("TAS: Fire Tactors In Sequence");
            StartCoroutine(FireInSequence());
        }
    }

    // Sets default values for TactorData instances and populate the list
    private void SetDefaultTactorData()
    {
        tactorDatas.Clear(); // Clear existing data if any

        for (int i = 0; i < tactorCount; i++)
        {
            TactorData td = new TactorData
            {
                tactorIndex = i, // Assuming indices start from 0
                // Set other default values as needed
            };

            tactorDatas.Add(td);
        }
    }

    public void PrintTactorControllerData()
    {

    }

    //prints tactor data
    public void PrintAllTactorData()
    {
        TactorLogger.Debug($"Printing Tactor Data:");
        for (int i = 0; i < tactorCount; i++)
        {
            TactorData td = tactorDatas[i];
            TactorLogger.Debug($"Tactor {i} Data:");
            TactorLogger.Debug($"  Index: {td.tactorIndex}");
            TactorLogger.Debug($"  Frequency: {td.frequency}");
            TactorLogger.Debug($"  Gain: {td.gain}");
            TactorLogger.Debug($"  Duration: {td.durationInMs} ms");
            TactorLogger.Debug($"  Delay: {td.delayInSeconds} seconds");
        }
    }

    // Tries to discover connected devices, connect to the first discovered device, and test tactors 
    public IEnumerator AutoSetup()
    {
        TactorLogger.Debug("TAS: Tactor Auto Setup");
        // yield return new WaitForSeconds(10f);  // waits 10 seconds before doing discover();
        yield return Discover();
        var deviceName = tc.GetDiscoveredNames()[0];
        var deviceType = tc.GetDiscoveredTypes()[0];
        tc.Connect(deviceName, deviceType);
        if (testDeviceOnStartup) 
        {
            StartCoroutine(FireAllOnce());
        }
    }

    // tries to discover connected devices for 3 seconds(ie. tactor controller connected to pc)
    private IEnumerator Discover()
    {
        while (tc.GetDiscoveredCount() <= 0)
        {
            TactorLogger.Debug("TAS: Searching for devices...");
            tc.Discover();
            yield return new WaitForSeconds(searchDelayInSeconds);
        }
    }

    // sets tactor parameters in TactorController using its TactorData
    private void SetTactorParameters(TactorData td)
    {
        tc.SetGain(td.tactorIndex, td.gain);
        tc.SetFreq(td.tactorIndex, td.frequency);
        // Duration is already set in the TactorData
    }

    // takes in a Tactor index from a TactorData and fires that Tactor
    public void FireTactor(int tacIndex)
    {
        tc.FireTactor(tacIndex, tactorDatas[0].durationInMs);
        recentlyFired[tacIndex] = 1;
        TactorLogger.Debug($"TAS.FireTactor({tacIndex},{tactorDatas[0].durationInMs});");
        TactorLogger.Debug($"TAS Recents: {recentlyFired.ToString()}");
    }

    // coroutine to fire all tactors in a sequence
    private IEnumerator FireInSequence()
    {
        float intervalDuration = 2f*tactorDatas[0].durationInMs/1000f;
        foreach (var td in tactorDatas)
        {
            SetTactorParameters(td);
            FireTactor(td.tactorIndex);
            yield return new WaitForSeconds(intervalDuration);
        }
    }

    // coroutine to fire all tactors at once
    public IEnumerator FireAllOnce()
    {
        foreach (var td in tactorDatas)
        {
            SetTactorParameters(td);
            FireTactor(td.tactorIndex);
            yield return new WaitForSeconds(td.delayInSeconds);
        }
    }

    //test tactors
    public void TestTactors()
    {
        StartCoroutine(FireAllOnce());
    }
}
