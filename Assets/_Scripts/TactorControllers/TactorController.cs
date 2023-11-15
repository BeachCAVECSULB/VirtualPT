using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TactorController : MonoBehaviour
{
    private TactorInterfaceWrapper ti;
    [SerializeField] private int controllerTypesToDiscover = (int) 0x01;
    private int deviceID, controllersFound;

    // Start is called before the first frame update
    void Start()
    {
        deviceID = -1;
        controllersFound = 0;
        ti = GetComponent<TactorInterfaceWrapper>();
        Initialize();
    }

    /**
     * <summary>Hacky routine that attempts to find and connect to a Tactor device.</summary>
     */
    public void QuickConnect()
    {
        //DisconnectAll();
        controllersFound = ti.DiscoverControllers(controllerTypesToDiscover);
        TactorLogger.Debug("TC QC # found: " + controllersFound);

        string dname = ti.GetDeviceNameFromDiscover(0);
        TactorLogger.Debug("TC QC name: " + controllersFound);

        int type = ti.GetDeviceTypeFromDiscover(0);
        TactorLogger.Debug("TC QC type: " + type);

        deviceID = ti.ConnectController(dname, type);
        TactorLogger.Debug("TC QC ID: " + deviceID);
    }

    /**
     * <summary>Attempts to discover connected devices.</summary>
     */
    public void Discover()
    {
        //DisconnectAll();
        controllersFound = ti.DiscoverControllers(controllerTypesToDiscover);
        TactorLogger.Debug("TC controllers found: " + controllersFound);
    }

    /// <summary>
    /// Returns the number of currently discovered devices
    /// </summary>
    /// <returns>Integer count of discovered devices</returns>
    public int GetDiscoveredCount()
    {
        return controllersFound;
    }

    /**
     * <summary>Returns the names of discovered devices.</summary>
     * <returns>Array of names of discovered devices. Null if no devices discovered.</returns>
     */
    public string[] GetDiscoveredNames()
    {
        if (controllersFound < 1) return null;
        string[] arr = new string[controllersFound];
        for (int i = 0; i < controllersFound; i++)
        {
            var n = ti.GetDeviceNameFromDiscover(i);
            if (n == null) return null;
            arr[i] = n;
        }

        return arr;
    }

    /**
     * <summary>Returns the type IDs of discovered devices.</summary>
     * <returns>Array of types of discovered devices. Null if no devices discovered.</returns>
     */
    public int[] GetDiscoveredTypes()
    {
        if (controllersFound < 1) return null;
        int[] arr = new int[controllersFound];
        for (int i = 0; i < controllersFound; i++)
        {
            arr[i] = ti.GetDeviceTypeFromDiscover(i);
        }

        return arr;
    }

    /**
     * <summary>Attempts to connect to a given device.</summary>
     * <param name="dname">String name of the device to connect to.</param>
     * <param name="type">Int type ID of the device to connect to.</param>
     */
    public void Connect(string dname, int type)
    {
        deviceID = ti.ConnectController(dname, type);
        TactorLogger.Debug("TC device ID: " + deviceID);
        if (deviceID == -1)
        {
            TactorLogger.Debug("ERROR: " + ti.GetLastErrorCode());
        }
    }

    /**
     * <summary>Disconnects all connected devices.</summary>
     */
    public void DisconnectAll()
    {
        var dc = ti.CloseAllConnections();
        TactorLogger.Debug("DC: " + dc);
    }

    public void DisconnectCurrent()
    {
        var dc = ti.CloseConnection(deviceID);
        TactorLogger.Debug("DC: " + dc);
    }

    /**
     * <summary>Returns the device ID of the connected device.</summary>
     * <returns>The device ID of the connected device.</returns>
     */
    public int GetDeviceID()
    {
        return deviceID;
    }

    /**
     * <summary>Shuts down the tactor interface.</summary>
     */
    public void Shutdown()
    {
        //DisconnectAll();
        var sd = ti.Shutdown();
        TactorLogger.Debug("Shutdown: " + sd);
    }

    public void OnApplicationQuit()
    {
        //DisconnectAll();
        Shutdown();
    }

    /// <summary>
    /// Initializes tactor interface.
    /// </summary>
    public void Initialize()
    {
        int i = ti.Initialize();
        TactorLogger.Debug("TC init: " + i);
    }

    public void FireTactor(int tacNum, int msDuration)
    {
        ti.SendPulse(deviceID, tacNum, msDuration, 0);
    }

    public void SetGain(int tacNum, int gain)
    {
        ti.SetGain(deviceID, tacNum, gain, 0);
    }

    public void SetFreq(int tacNum, int freq)
    {
        ti.SetFreq(deviceID, tacNum, freq, 0);
    }
}

public static class TactorLogger
{
    //[Conditional("ENABLE_TACTOR_LOGS")]
    public static void Debug(string logMsg)
    {
        UnityEngine.Debug.Log(logMsg);
    }
}