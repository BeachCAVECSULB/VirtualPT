﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class TactorInterfaceWrapper : MonoBehaviour
{
    /****************************************************************************
    *FUNCTION: InitializeTI
    *DESCRIPTION		Sets up TDK
    *
    *
    *RETURNS:
    *			on success:		0
    *			on failure:		value(-1) check GetLastEAIError() for Error Code
    *****************************************************************************/
    [DllImport("TactorInterface", EntryPoint = "InitializeTI")]
    public static extern int InitializeTI();

    /**
     * <summary>Sets up TDK</summary>
     * <returns>0 : -1 check GetLastEAIError() for Error Code</returns>
     */
    public int Initialize()
    {
        return InitializeTI();
    }

    /****************************************************************************
    *FUNCTION: ShutdownTI
    *DESCRIPTION		Shuts down and cleans up the TDK
    *
    *
    *RETURNS:
    *			on success:		0
    *			on failure:		value(-1) check GetLastEAIError() for Error Code
    *****************************************************************************/
    [DllImport("TactorInterface", EntryPoint = "ShutdownTI")]
    public static extern int ShutdownTI();

    /**
     * <summary>Shuts down and cleans up the TDK</summary>
     * <returns>0 : -1 check GetLastEAIError() for Error Code</returns>
     */
    public int Shutdown()
    {
        return ShutdownTI();
    }

    /**
     * Original returns a char* idk how to handle that in C#
     * null terminated strings just become strings?
     */
    /****************************************************************************
    *FUNCTION: GetVersionNumber
    *DESCRIPTION		Version Identification of TDK
    *
    *
    *RETURNS:
    *			on success:		value Version Number
    *****************************************************************************/
    [DllImport("TactorInterface", EntryPoint = "GetVersionNumber")]
    public static extern string GetVersionNumber();

    /**
     * <summary>Version Identification of TDK</summary>
     * <returns>value Version Number</returns>
     */
    public string GetVersion()
    {
        return GetVersionNumber();
    }

    /****************************************************************************
    *FUNCTION: Connect
    *DESCRIPTION		Connect to a Tactor Controller
    *PARAMETERS
    *IN: const char*	_name - Tactor Controller Name (proper name or COM Port)
    *IN: int			_type - Tactor Controller Type (see list of types in 	
    *IN: void*			_callback - reponse packet return function (can be null) 
    *								(see function declaration in 
    *
    *RETURNS:
    *			on success:		Board Identification Number
    *			on failure:		value(-1) check GetLastEAIError() for Error Code
    *****************************************************************************/
    [DllImport("TactorInterface", EntryPoint = "Connect")]
    public static extern int Connect(string dname, int Type, System.IntPtr _Callback);

    /**
     * <summary>Connect to a Tactor Controller</summary>
     * <param name="deviceName">Tactor Controller Name (proper name or COM Port)</param>
     * <param name="type">Tactor Controller Type (see list of types in</param>
     * <returns>Board Identification Number : -1 check GetLastEAIError() for Error Code</returns>
     */
    public int ConnectController(string deviceName, int type)
    {
        return Connect(deviceName, type, System.IntPtr.Zero);
    }

    /****************************************************************************
    *FUNCTION: Discover
    *DESCRIPTION		Scan Available Ports on computer for Tactor Controller
    *PARAMETERS
    *IN: int			_type -	Type of Controllers to Scan For (bitfield,
    *							multiple types can be ORd together.)
    *
    *RETURNS:
    *			on success:		Number of Devices Found
    *			on failure:		value(-1) check GetLastEAIError() for Error Code
    *****************************************************************************/
    [DllImport("TactorInterface", EntryPoint = "Discover")]
    public static extern int Discover(int Type);

    /**
     * <summary>Scan Available Ports on computer for Tactor Controller</summary>
     * <param name="type">Type of Controllers to Scan For (bitfield,
    *							multiple types can be ORd together.)</param>
     * <returns>Number of Devices Found : -1 check GetLastEAIError() for Error Code</returns>
     */
    public int DiscoverControllers(int type)
    {
        return Discover(type);
    }

    /****************************************************************************
    *FUNCTION: DiscoverLimited
    *DESCRIPTION		Scan Available Ports on computer for Tactor Controller with alotted amount
    *PARAMETERS
    *IN: int			_type -	Type of Controllers to Scan For (bitfield,
    *									multiple types can be ORd together.)
    *					_amount - the alotted amount.
    *						
    *RETURNS:
    *			on success:		Number of Devices Found
    *			on failure:		value(-1) check GetLastEAIError() for Error Code
    *****************************************************************************/
    [DllImport("TactorInterface", EntryPoint = "DiscoverLimited")]
    public static extern int DiscoverLimited(int Type, int Amount);

    /**
     * <summary>Scan Available Ports on computer for Tactor Controller with alotted amount</summary>
     * <param name="type">Type of Controllers to Scan For (bitfield,
    *									multiple types can be ORd together.)</param>
     * <param name="amount">the alotted amount.</param>
     * <returns>Number of Devices Found : -1 check GetLastEAIError() for Error Code </returns>
     */
    public int DiscoverControllersLimited(int type, int amount)
    {
        return DiscoverLimited(type, amount);
    }

    /****************************************************************************
    *FUNCTION: GetDiscoveredDeviceName
    *DESCRIPTION		Scan Available Ports on computer for Tactor Controller
    *PARAMETERS
    *IN: int			index -	Device To Get Name From (Index from Discover) NOT BOARD ID
    *
    *RETURNS:
    *			on success:		const char* Name
    *			on failure:		NULL check GetLastEAIError() for Error Code 
    *****************************************************************************/
    [DllImport("TactorInterface", EntryPoint = "GetDiscoveredDeviceName")]
    public static extern IntPtr GetDiscoveredDeviceName(int Index);

    /**
     * <summary>Scan Available Ports on computer for Tactor Controller</summary>
     * <param name="index">Device To Get Name From (Index from Discover) NOT BOARD ID</param>
     * <returns>string Name : NULL check GetLastEAIError() for Error Code</returns>
     */
    public string GetDeviceNameFromDiscover(int index)
    {
        var targetIndex = index;
        IntPtr discoveredNamePTR = GetDiscoveredDeviceName(targetIndex);
        if (discoveredNamePTR == null) return "-1";
        string deviceName = Marshal.PtrToStringAnsi(discoveredNamePTR);
        return deviceName;
    }

    /****************************************************************************
    *FUNCTION: GetDiscoveredDeviceType
    *DESCRIPTION		Scan Available Ports on computer for Tactor Controller
    *PARAMETERS
    *IN: int			index -	Device To Get Name From (Index from Discover) NOT BOARD ID
    *
    *RETURNS:
    *			on success:		integer representing the device discovered's type
    *			on failure:		0 check GetLastEAIError() for Error Code 
    *****************************************************************************/
    [DllImport("TactorInterface", EntryPoint = "GetDiscoveredDeviceType")]
    public static extern int GetDiscoveredDeviceType(int Index);

    /**
     * <summary>Scan Available Ports on computer for Tactor Controller</summary>
     * <param name="index">Device To Get Name From (Index from Discover) NOT BOARD ID</param>
     * <returns>integer representing the device discovered's type : 0 check GetLastEAIError() for Error Code</returns>
     */
    public int GetDeviceTypeFromDiscover(int index)
    {
        return GetDiscoveredDeviceType(index);
    }

    /****************************************************************************
    *FUNCTION: Close
    *DESCRIPTION		Closes Connection with Selected Device
    *PARAMETERS
    *IN: int			_deviceID - Tactor Controller Device ID (returned from Connect)
    *
    *RETURNS:
    *			on success:		0
    *			on failure:		value(-1) check GetLastEAIError() for Error Code
    *****************************************************************************/
    [DllImport("TactorInterface", EntryPoint = "Close")]
    public static extern int Close(int _DeviceID);

    /**
     * <summary>Closes Connection with Selected Device</summary>
     * <param name="deviceID">Tactor Controller Device ID (returned from Connect)</param>
     * <returns>0 : -1 check GetLastEAIError() for Error Code</returns>
     */
    public int CloseConnection(int deviceID)
    {
        return Close(deviceID);
    }

    /****************************************************************************
    *FUNCTION: CloseAll
    *DESCRIPTION		Closes All Active Connections
    *
    *RETURNS:
    *			on success:		0
    *			on failure:		value(-1) check GetLastEAIError() for Error Code
    *****************************************************************************/
    [DllImport("TactorInterface", EntryPoint = "CloseAll")]
    public static extern int CloseAll();

    /**
     * <summary>Closes All Active Connections</summary>
     * <returns>0 : -1 check GetLastEAIError() for Error Code</returns>
     */
    public int CloseAllConnections()
    {
        return CloseAll();
    }

    /****************************************************************************
    *FUNCTION: Pulse
    *DESCRIPTION		Command Sent to the Tactor Controller
    *					Pulses Specified Tactor
    *PARAMETERS
    *IN: int			_deviceID		- Device To apply Command
    *IN: int			_tacNum			- Tactor Number For Command
    *IN: int			_msDuration		- Duration of Pulse
    *IN: int			_delay			- Delay before running Command (ms)
    *
    *RETURNS:
    *			on success:		value(0)
    *			on failure:		value(-1) check GetLastEAIError() for Error Code
    *****************************************************************************/
    [DllImport("TactorInterface", EntryPoint = "Pulse")]
    public static extern int Pulse(int DeviceID, int _TacNum, int _MsDuration, int _Delay);

    /**
     * <summary>Command Sent to the Tactor Controller Pulses Specified Tactor</summary>
     * <param name="deviceID">Device To apply Command</param>
     * <param name="tacNum">Tactor Number For Command</param>
     * <param name="msDuration">Duration of Pulse</param>
     * <param name="delay">Delay before running Command (ms)</param>
     * <returns>0 : -1 check GetLastEAIError() for Error Code</returns>
     */
    public int SendPulse(int deviceID, int tacNum, int msDuration, int delay)
    {
        return Pulse(deviceID, tacNum, msDuration, delay);
    }

    /****************************************************************************
    *FUNCTION: SendActionWait
    *DESCRIPTION		Command Sent to the Tactor Controller
    *					Waits all actions for given time.
    *PARAMETERS
    *IN: int			_deviceID		- Device To apply Command
    *IN: int			_msDuration		- Duration of Wait
    *IN: int			_delay			- Delay before running Command (ms)
    *
    *RETURNS:
    *			on success:		value(0)
    *			on failure:		value(-1) check GetLastEAIError() for Error Code
    *****************************************************************************/
    [DllImport("TactorInterface", EntryPoint = "SendActionWait")]
    public static extern int SendActionWait(int DeviceID, int _MsDuration, int _Delay);

    /**
     * <summary>Command Sent to the Tactor Controller Waits all actions for given time.</summary>
     * <param name="deviceID">Device To apply Command</param>
     * <param name="msDuration">Duration of Wait</param>
     * <param name="delay">Delay before running Command (ms)</param>
     * <returns>0 : -1 check GetLastEAIError() for Error Code</returns>
     */
    public int SendWait(int deviceID, int msDuration, int delay)
    {
        return SendActionWait(deviceID, msDuration, delay);
    }

    /****************************************************************************
    *FUNCTION: ChangeGain
    *DESCRIPTION		Command Sent to the Tactor Controller
    *					Changes the Gain For Specified Tactor
    *PARAMETERS
    *IN: int			_deviceID	- Device To apply Command
    *IN: int			_tacNum		- Tactor Number For Command
    *IN: int			_gainVal	- Gain Value (0-255)
    *IN: int			_delay		- Delay before running Command (ms)
    *
    *RETURNS:
    *			on success:		value(0)
    *			on failure:		value(-1) check GetLastEAIError() for Error Code
    *****************************************************************************/
    [DllImport("TactorInterface", EntryPoint = "ChangeGain")]
    public static extern int ChangeGain(int DeviceID, int _TacNum, int _GainVal, int _Delay);

    /**
     * <summary>Command Sent to the Tactor Controller Changes the Gain For Specified Tactor</summary>
     * <param name="deviceID">Device To apply Command</param>
     * <param name="tacNum">Tactor Number For Command</param>
     * <param name="gainVal">Gain Value (0-255)</param>
     * <param name="delay">Delay before running Command (ms)</param>
     * <returns>0 : -1 check GetLastEAIError() for Error Code</returns>
     */
    public int SetGain(int deviceID, int tacNum, int gainVal, int delay)
    {
        return ChangeGain(deviceID, tacNum, gainVal, delay);
    }

    /****************************************************************************
    *FUNCTION: RampGain
    *DESCRIPTION		Command Sent to the Tactor Controller
    *					Changes the Gain From start to end within the duration specified for the tactor number specified
    *PARAMETERS
    *IN: int			_deviceID	- Device To apply Command
    *IN: int			_tacNum		- Tactor Number For Command
    *IN: int			_gainStart	- Start Gain Value (0-255)
    *IN: int			_gainEnd	- End Gain Value (0-255)	
    *IN: int			_duration	- Duration of Ramp (ms)
    *IN: int			_func		- Function Type
    *IN: int			_delay		- Delay before running Command (ms)
    *
    *RETURNS:
    *			on success:		value(0)
    *			on failure:		value(-1) check GetLastEAIError() for Error Code
    *****************************************************************************/
    [DllImport("TactorInterface", EntryPoint = "RampGain")]
    public static extern int RampGain(int DeviceID, int _TacNum, int _GainStart, int _GainEnd,
        int _Duration, int _Func, int _Delay);
    
    /**
     * <summary>
     * Command Sent to the Tactor Controller
     * Changes the Gain From start to end within the duration specified for the tactor number specified
     * </summary>
     * <param name="deviceId">Device To apply Command</param>
     * <param name="tacNum">Tactor Number For Command</param>
     * <param name="gainStart">Start Gain Value (0-255)</param>
     * <param name="gainEnd">End Gain Value (0-255)	</param>
     * <param name="duration">Duration of Ramp (ms)</param>
     * <param name="funcType">Function Type</param>
     * <param name="delay">Delay before running Command (ms)</param>
     * <returns>0 : -1 check GetLastEAIError() for Error Code</returns>
     */
    public int SetRampingGain(int deviceId, int tacNum, int gainStart, int gainEnd, int duration, int funcType, int delay)
    {
        return RampGain(deviceId, tacNum, gainStart, gainEnd, duration, funcType, delay);
    }

    /****************************************************************************
    *FUNCTION: ChangeFreq
    *DESCRIPTION		Command Sent to the Tactor Controller
    *					Changes the Frequency For Specified Tactor
    *PARAMETERS
    *IN: int			_deviceID	- Device To apply Command
    *IN: int			_tacNum		- Tactor Number For Command
    *IN: int			_freqVal	- Freq Value (300-3550)
    *IN: int			_delay		- Delay before running Command (ms)
    *
    *RETURNS:
    *			on success:		value(0)
    *			on failure:		value(-1) check GetLastEAIError() for Error Code
    *****************************************************************************/
    [DllImport("TactorInterface", EntryPoint = "ChangeFreq")]
    public static extern int ChangeFreq(int deviceID, int _tacNum, int freqVal, int _delay);

    /**
     * <summary>
     * Command Sent to the Tactor Controller
     * Changes the Frequency For Specified Tactor
     * </summary>
     * <param name="deviceId">Device To apply Command</param>
     * <param name="tacNum">Tactor Number For Command</param>
     * <param name="freqVal">Freq Value (300-3550)</param>
     * <param name="delay">Delay before running Command (ms)</param>
     * <returns>0 : -1 check GetLastEAIError() for Error Code</returns>
     */
    public int SetFreq(int deviceId, int tacNum, int freqVal, int delay)
    {
        return ChangeFreq(deviceId, tacNum, freqVal, delay);
    }

    /****************************************************************************
    *FUNCTION: RampFreq
    *DESCRIPTION		Command Sent to the Tactor Controller
    *					Changes the Freq From start to end within the duration specified for the tactor number specified
    *PARAMETERS
    *IN: int			_deviceID	- Device To apply Command
    *IN: int			_tacNum		- Tactor Number For Command
    *IN: int			_freqStart	- Start Freq Value (300-3550)
    *IN: int			_freqEnd	- End Freq Value (300-3550)
    *IN: int			_duration	- Duration of Ramp (ms)
    *IN: int			_func		- Function Type
    *IN: int			_delay		- Delay before running Command (ms)
    *
    *RETURNS:
    *			on success:		value(0)
    *			on failure:		value(-1) check GetLastEAIError() for Error Code
    *****************************************************************************/
    [DllImport("TactorInterface", EntryPoint = "RampFreq")]
    public static extern int RampFreq(int deviceID, int _tacNum, int _freqStart, int _freqEnd,
        int _duration, int _func, int _delay);

    /**
     * <summary>
     * Command Sent to the Tactor Controller
     * Changes the Freq From start to end within the duration specified for the tactor number specified
     * </summary>
     * <param name="deviceID">Device To apply Command</param>
     * <param name="tacNum">Tactor Number For Command</param>
     * <param name="freqStart">Start Freq Value (300-3550)</param>
     * <param name="freqEnd">End Freq Value (300-3550)	</param>
     * <param name="duration">Duration of Ramp (ms)</param>
     * <param name="funcType">Function Type</param>
     * <param name="delay">Delay before running Command (ms)</param>
     * <returns>0 : -1 check GetLastEAIError() for Error Code</returns>
     */
    public int SetRampingFreq(int deviceID, int tacNum, int freqStart, int freqEnd, int duration, int funcType, int delay)
    {
        return RampFreq(deviceID, tacNum, freqStart, freqEnd, duration, funcType, delay);
    }

    /****************************************************************************
    *FUNCTION: ChangeSigSource
    *DESCRIPTION		Command Sent to the Tactor Controller
    *					Changes the Sig Source For Specified Tactor
    *PARAMETERS
    *IN: int			_deviceID	- Device To apply Command
    *IN: int			_tacNum		- Tactor Number For Command
    *IN: int			_type		- New Sig Source Type
    *IN: int			_delay		- Delay before running Command (ms)
    *
    *RETURNS:
    *			on success:		value(0)
    *			on failure:		value(-1) check GetLastEAIError() for Error Code
    *****************************************************************************/
    [DllImport("TactorInterface", EntryPoint = "ChangeSigSource")]
    public static extern int ChangeSigSource(int _device, int _tacNum, int _type, int _delay);

    /**
     * <summary>
     * Command Sent to the Tactor Controller
    *	Changes the Sig Source For Specified Tactor
     * </summary>
     * <param name="deviceID">Device To apply Command</param>
     * <param name="tacNum">Tactor Number For Command</param>
     * <param name="sigType">New Sig Source Type</param>
     * <param name="delay">Delay before running Command (ms)</param>
     * <returns>0 : -1 check GetLastEAIError() for Error Code</returns>
     */
    public int SetSigSource(int deviceID, int tacNum, int sigType, int delay)
    {
        return ChangeSigSource(deviceID, tacNum, sigType, delay);
    }

    /****************************************************************************
    *FUNCTION: ReadFW
    *DESCRIPTION		Command Sent to the Tactor Controller
    *					Requests Firmware Version From Tactor Controller
    *PARAMETERS
    *IN: int			_deviceID		- Device To apply Command
    *
    *RETURNS:
    *			on success:		value(0)
    *			on failure:		value(-1) check GetLastEAIError() for Error Code
    *****************************************************************************/
    [DllImport("TactorInterface", EntryPoint = "ReadFW")]
    public static extern int ReadFW(int deviceID);

    /**
     * <summary>
     * Command Sent to the Tactor Controller
     * Requests Firmware Version From Tactor Controller
     * </summary>
     * <param name="deviceID">Device To apply Command</param>
     * <returns>0 : -1 check GetLastEAIError() for Error Code</returns>
     */
    public int GetFirmwareVersion(int deviceID)
    {
        return ReadFW(deviceID);
    }

    /****************************************************************************
    *FUNCTION: TactorSelfTest
    *DESCRIPTION		Command Sent to the Tactor Controller
    *					Self Test Tactor Controller
    *PARAMETERS
    *IN: int			_deviceID		- Device To apply Command
    *IN: int			_delay			- Delay before running Command (ms)
    *
    *RETURNS:
    *			on success:		value(0)
    *			on failure:		value(-1) check GetLastEAIError() for Error Code
    *****************************************************************************/
    [DllImport("TactorInterface", EntryPoint = "TactorSelfTest")]
    public static extern int TactorSelfTest(int deviceID, int _delay);

    /**
     * <summary>
     * Command Sent to the Tactor Controller
     * Self Test Tactor Controller
     * </summary>
     * <param name="deviceID">Device To apply Command</param>
     * <param name="delay">Delay before running Command (ms)</param>
     * <returns>0 : -1 check GetLastEAIError() for Error Code</returns>
     */
    public int PerformSelfTest(int deviceID, int delay)
    {
        return TactorSelfTest(deviceID, delay);
    }

    /****************************************************************************
    *FUNCTION: ReadSegmentList
    *DESCRIPTION		Command Sent to the Tactor Controller
    *					Request for Segment List From Tactor Controller
    *					Represents Number of Tactors connected to Tactor Controller
    *PARAMETERS
    *IN: int			_deviceID		- Device To apply Command
    *IN: int			_delay			- Delay before running Command (ms)
    *
    *RETURNS:
    *			on success:		value(0)
    *			on failure:		value(-1) check GetLastEAIError() for Error Code
    *****************************************************************************/
    [DllImport("TactorInterface", EntryPoint = "ReadSegmentList")]
    public static extern int ReadSegmentList(int deviceID, int _delay);

    /**
     * <summary>
     * Command Sent to the Tactor Controller
     * Request for Segment List From Tactor Controller
     * Represents Number of Tactors connected to Tactor Controller
     * </summary>
     * <param name="deviceID">Device To apply Command</param>
     * <param name="delay">Delay before running Command (ms)</param>
     * <returns>0 : -1 check GetLastEAIError() for Error Code</returns>
     */
    public int GetConnectedTactors(int deviceID, int delay)
    {
        return ReadSegmentList(deviceID, delay);
    }

    /****************************************************************************
    *FUNCTION: ReadBatteryLevel
    *DESCRIPTION		Command Sent to the Tactor Controller
    *					Request for Battery Level From Tactor Controller
    *
    *PARAMETERS
    *IN: int			_deviceID		- Device To apply Command
    *IN: int			_delay			- Delay before running Command (ms)
    *
    *RETURNS:
    *			on success:		value(0)
    *			on failure:		value(-1) check GetLastEAIError() for Error Code
    *****************************************************************************/
    [DllImport("TactorInterface", EntryPoint = "ReadBatteryLevel")]
    public static extern int ReadBatteryLevel(int deviceID, int _delay);

    /**
     * <summary>
     * Command Sent to the Tactor Controller
     * Request for Battery Level From Tactor Controller
     * </summary>
     * <param name="deviceID">Device To apply Command</param>
     * <param name="delay">Delay before running Command (ms)</param>
     * <returns>0 : -1 check GetLastEAIError() for Error Code</returns>
     */
    public int GetBatteryLevel(int deviceID, int delay)
    {
        return ReadBatteryLevel(deviceID, delay);
    }

    /****************************************************************************
    *FUNCTION: Stop
    *DESCRIPTION		Command Sent to the Tactor Controller
    *					Requests Tactor Controller To Stop All Commands
    *PARAMETERS
    *IN: int			_deviceID	- Device To apply Command
    *IN: int			_delay		- Delay before running Command (ms)
    *
    *RETURNS:
    *			on success:		value(0)
    *			on failure:		value(-1) check GetLastEAIError() for Error Code
    *****************************************************************************/
    [DllImport("TactorInterface", EntryPoint = "Stop")]
    public static extern int Stop(int deviceID, int _delay);

    /**
     * <summary>
     * Command Sent to the Tactor Controller
     * Requests Tactor Controller To Stop All Commands
     * </summary>
     * <param name="deviceID">Device To apply Command</param>
     * <param name="delay">Delay before running Command (ms)</param>
     * <returns>0 : -1 check GetLastEAIError() for Error Code</returns>
     */
    public int SendStop(int deviceID, int delay)
    {
        return Stop(deviceID, delay);
    }

    /**
     * What is an unsigned char*??????
     */
    /****************************************************************************
    *FUNCTION: SetTactors
    *DESCRIPTION		Command Sent to the Tactor Controller
    *					Sets the tactors on or off based on the byte array given.
    *PARAMETERS
    *IN: int			_deviceID	- Device To apply Command
    *IN: int			_delay		- Delay before running Command (ms)
    *IN: unsigned char*	states		- An 8-byte array of boolean values representing
    *								  the desired tactor states. Tactor 1 is at
    *								  the LSB of byte 1, tactor 64 is the MSB
    *								  of byte 8.
    *
    *RETURNS:
    *			on success:		value(0)
    *			on failure:		value(-1) check GetLastEAIError() for Error Code
    *****************************************************************************/
    [DllImport("TactorInterface", EntryPoint = "SetTactors")]
    public static extern int SetTactors(int device_id, int delay, bool[] states);

    /**
     * <summary>
     * Command Sent to the Tactor Controller
     * Sets the tactors on or off based on the byte array given.
     * </summary>
     * <param name="deviceID">Device To apply Command</param>
     * <param name="delay">Delay before running Command (ms)</param>
     * <param name="states">
     * An 8-byte array of boolean values representing
    *		the desired tactor states. Tactor 1 is at
    *		the LSB of byte 1, tactor 64 is the MSB
    *		of byte 8.</param>
     * <returns>0 : -1 check GetLastEAIError() for Error Code</returns>
     */
    public int SetTactorStates(int deviceID, int delay, bool[] states)
    {
        return SetTactors(deviceID, delay, states);
    }

    /****************************************************************************
    *FUNCTION: SetTactorType
    *DESCRIPTION		Command Sent to the Tactor Controller
    *					Sets the given tactor to the type described.
    *PARAMETERS
    *IN: int			device_id	- Device To apply Command
    *IN: int			delay		- Delay before running Command (ms)
    *IN: int			tactor		- The tactor to modify
    *IN: type			type		- The type to set the tactor to
    *
    *RETURNS:
    *			on success:		value(0)
    *			on failure:		value(-1) check GetLastEAIError() for Error Code
    *****************************************************************************/
    [DllImport("TactorInterface", EntryPoint = "SetTactorType")]
    public static extern int SetTactorType_(int device_id, int delay, int tactor, int type);

    /**
     * <summary>
     * Command Sent to the Tactor Controller
     * Requests Tactor Controller To Stop All Commands
     * </summary>
     * <param name="deviceID">Device To apply Command</param>
     * <param name="delay">Delay before running Command (ms)</param>
     * <param name="tactor">The tactor to modify</param>
     * <param name="type">The type to set the tactor to</param>
     * <returns>0 : -1 check GetLastEAIError() for Error Code</returns>
     */
    public int SetTactorType(int deviceID, int delay, int tactor, int type)
    {
        return SetTactorType_(deviceID, delay, tactor, type);
    }

    /****************************************************************************
    *FUNCTION: UpdateTI
    *DESCRIPTION		Update The TactorInterface for house maintence
    *					- will check for errors with internal threads
    *
    *PARAMETERS
    *
    *RETURNS:
    *			on success:		value(0)
    *			on failure:		Error code - See EAI_Defines.h for reason
    *****************************************************************************/
    [DllImport("TactorInterface", EntryPoint = "UpdateTI")]
    public static extern int UpdateTI();
    
    /**
     * <summary>
     * Update The TactorInterface for house maintence
     * - will check for errors with internal threads
     * </summary>
     * <returns>0 : -1 check GetLastEAIError() for Error Code</returns>
     */
    public int Maintenance()
    {
        return UpdateTI();
    }

    /****************************************************************************
    *FUNCTION: GetLastEAIError
    *DESCRIPTION		Returns EAI Error Code (See full list of error codes in EAI_ErrorCodes.h)
    *
    *PARAMETERS
    *
    *
    *RETURNS:			Last ErrorCode
    *****************************************************************************/
    [DllImport("TactorInterface", EntryPoint = "GetLastEAIError")]
    public static extern int GetLastEAIError();

    /**
     * <summary>
     * Returns EAI Error Code (See full list of error codes in EAI_ErrorCodes.h)
     * </summary>
     * <returns>Last ErrorCode</returns>
     */
    public int GetLastErrorCode()
    {
        return GetLastEAIError();
    }

    /****************************************************************************
    *FUNCTION: SetLastEAIError
    *DESCRIPTION		Sets EAI Error Code (See full list of error codes in EAI_ErrorCodes.h)
    *
    *PARAMETERS
    *IN: int			e the last error code.. ***internal use.
    *
    *RETURNS:			Last ErrorCode
    *****************************************************************************/
    [DllImport("TactorInterface", EntryPoint = "SetLastEAIError")]
    public static extern int SetLastEAIError(int e);

    public int SetLastErrorCode(int e)
    {
        return SetLastEAIError(e);
    }

    /****************************************************************************
    *FUNCTION: SetTimeFactor
    *DESCRIPTION		Set DLL Time Factor to be passed with each Action List
    *					10 is the default
    *
    *PARAMETERS
    *IN: int			_timeFactor (1-255) multiple delay value * timefactor for actual delay
    *
    *RETURNS:
    *			on success: 0
    *			on failure: -1, and sets last EAI error as ERROR_BADPARAMETER
    *
    *****************************************************************************/
    [DllImport("TactorInterface", EntryPoint = "SetTimeFactor")]
    public static extern int SetTimeFactor_(int value);

    /**
     * <summary>Set DLL Time Factor to be passed with each Action List
    *					10 is the default</summary>
     * <param name="value">(1-255) multiple delay value * timefactor for actual delay</param>
     * <returns>0 : -1, and sets last EAI error as ERROR_BADPARAMETER</returns>
     */
    public int SetTimeFactor(int value)
    {
        return SetTimeFactor_(value);
    }

    /****************************************************************************
    *FUNCTION: BeginStoreTAction
    *DESCRIPTION		Sets the tactor controller in the 'recording TAction' mode.
    *
    *PARAMETERS
    *IN: int			_deviceID	- Device To apply Command
    *IN: int			tacID		- The the address of the TAction to store (1-10)
    *
    *RETURNS:
    *			on success: 0
    *			on failure: -1, and sets last EAI error as ERROR_BADPARAMETER
    *
    *****************************************************************************/
    [DllImport("TactorInterface", EntryPoint = "BeginStoreTAction")]
    public static extern int BeginStoreTAction(int _deviceID, int tacID);

    /**
     * <summary>Sets the tactor controller in the 'recording TAction' mode.</summary>
     * <param name="deviceID">Device To apply Command</param>
     * <param name="recordingID">The the address of the TAction to store (1-10)</param>
     * <returns>0 : -1, and sets last EAI error as ERROR_BADPARAMETER</returns>
     */
    public int StartRecording(int deviceID, int recordingID)
    {
        return BeginStoreTAction(deviceID, recordingID);
    }

    /****************************************************************************
    *FUNCTION: FinishStoreTAction
    *DESCRIPTION		Finishes the 'recording TAction' mode.
    *
    *PARAMETERS
    *IN: int			_deviceID	- Device To apply Command
    *
    *RETURNS:
    *			on success: 0
    *			on failure: -1, and sets last EAI error as ERROR_BADPARAMETER
    *
    *****************************************************************************/
    [DllImport("TactorInterface", EntryPoint = "FinishStoreTAction")]
    public static extern int FinishStoreTAction(int _deviceID);

    /**
     * <summary>Finishes the 'recording TAction' mode.</summary>
     * <param name="deviceID">Device To apply Command</param>
     *  <returns>0 : -1, and sets last EAI error as ERROR_BADPARAMETER</returns>
     */
    public int EndRecording(int deviceID)
    {
        return FinishStoreTAction(deviceID);
    }

    /****************************************************************************
    *FUNCTION: PlayStoredTAction
    *DESCRIPTION		Plays a TAction stored on the tactor device.
    *
    *PARAMETERS
    *IN: int			_deviceID	- Device To apply Command
    *IN: int			_delay		- Delay before running Command (ms)
    *IN: int			tacId		- The the address of the TAction to play (1-10)
    *
    *RETURNS:
    *			on success: 0
    *			on failure: -1, and sets last EAI error as ERROR_BADPARAMETER
    *
    *****************************************************************************/
    [DllImport("TactorInterface", EntryPoint = "PlayStoredTAction")]
    public static extern int PlayStoredTAction(int _deviceID, int _delay, int tacId);

    /**
     * <summary>Plays a TAction stored on the tactor device.</summary>
     * <param name="deviceID">Device To apply Command</param>
     * <param name="delay">Delay before running Command (ms)</param>
     * <param name="recordingID">The the address of the TAction to play (1-10)</param>
     * <returns>0 : -1, and sets last EAI error as ERROR_BADPARAMETER</returns>
     */
    public int PlayRecording(int deviceID, int delay, int recordingID)
    {
        return PlayStoredTAction(deviceID, delay, recordingID);
    }

    /****************************************************************************
    *FUNCTION: SetFreqTimeDelay
    *DESCRIPTION		Disables or enables the pulse to end on the sin wave reaching
    *					zero or the duration of the pulse
    *
    *PARAMETERS
    *IN: int			_deviceID		- Device To apply Command
    *IN: int			_delayOn		- False to end with duration, True to end when
    *									  sin reaches zero after duration.
    *
    *RETURNS:
    *			on success:		value(0)
    *			on failure:		value(-1) check GetLastEAIError() for Error Code
    *****************************************************************************/
    [DllImport("TactorInterface", EntryPoint = "SetFreqTimeDelay")]
    public static extern int SetFreqTimeDelay(int _deviceID, bool _delayOn);

    /**
     * <summary>
     * Disables or enables the pulse to end on the sin wave reaching
    *					zero or the duration of the pulse
     * </summary>
     * <param name="deviceID">Device To apply Command</param>
     * <param name="endOnZero">False to end with duration, True to end when sin reaches zero after duration.</param>
     * <returns>0 : -1 check GetLastEAIError() for Error Code</returns>
     */
    public int SetPulseEndOnZero(int deviceID, bool endOnZero)
    {
        return SetFreqTimeDelay(deviceID, endOnZero);
    }
}