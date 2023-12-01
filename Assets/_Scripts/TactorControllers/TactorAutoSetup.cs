using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEditor;
using UnityEngine;

public class TactorAutoSetup : MonoBehaviour
{
    //public TactorController tactorController;
    public int tactorCount = 8;
    public float searchDelayInSeconds = 3f;
    public int defaultPulseDuration = 150;
    public bool testDeviceOnStartup = false;
    
    private int[] pulseDuration;
    //private List<int> recentlyFired = new List<int>();
    private int[] recentlyFired;

    private TactorController tc;
    
    // Start is called before the first frame update
    void Start()
    {
        tc = GetComponent<TactorController>();
        pulseDuration = new int[tactorCount];
        for (var i = 0; i < tactorCount; i++) pulseDuration[i] = defaultPulseDuration;
        recentlyFired = new int[tactorCount];
        StartCoroutine(AutoSetup());
    }

    public IEnumerator AutoSetup()
    {
        TactorLogger.Debug("TAS: Tactor Auto Setup");
        yield return new WaitForSeconds(10f);
        yield return Discover();
        var deviceName = tc.GetDiscoveredNames()[0];
        var deviceType = tc.GetDiscoveredTypes()[0];
        tc.Connect(deviceName, deviceType);
        if (testDeviceOnStartup) StartupTest();
    }

    private IEnumerator WaitForTactorController()
    {
        while (tc == null)
        {
            TactorLogger.Debug("TAS: Waiting for TC component...");
            yield return new WaitForSeconds(searchDelayInSeconds);
        }
    }

    private IEnumerator Discover()
    {
        while (tc.GetDiscoveredCount() <= 0)
        {
            TactorLogger.Debug("TAS: Searching for devices...");
            tc.Discover();
            yield return new WaitForSeconds(searchDelayInSeconds);
        }
    }

    public void StartupTest()
    {
        TactorLogger.Debug("TAS: Startup Test");
        StartCoroutine(FireInSequence());
    }

    private IEnumerator FireInSequence()
    {
        for (var i = 0; i < tactorCount; i++)
        {
            FireTactor(i);
            yield return new WaitForSeconds(2f*pulseDuration[i]/1000f);
        }
    }

    public void FireTactor(int tacIndex)
    {
        tc.FireTactor(tacIndex+1, pulseDuration[tacIndex]);
        recentlyFired[tacIndex] = 1;
        TactorLogger.Debug($"TAS.FireTactor({tacIndex},{pulseDuration[tacIndex]});");
        TactorLogger.Debug($"TAS Recents: {recentlyFired.ToString()}");
    }

    public void SetDuration(int tacIndex, int durMs)
    {
        pulseDuration[tacIndex] = durMs;
        TactorLogger.Debug($"TAS.SetDuration({tacIndex},{durMs});");
    }

    public void SetGain(int tacIndex, int gain)
    {
        tc.SetGain(tacIndex+1, gain);
        TactorLogger.Debug($"TAS.SetGain({tacIndex},{gain});");
    }

    public void SetFreq(int tacIndex, int freq)
    {
        tc.SetFreq(tacIndex+1, freq);
        TactorLogger.Debug($"TAS.SetFreq({tacIndex},{freq});");
    }

    public int[] popRecentlyFired()
    {
        var temp = recentlyFired;
        recentlyFired = new int[tactorCount];
        //TactorLogger.Debug($"TAS recents :{recentlyFired},{temp});");
        return temp;
    }

}
