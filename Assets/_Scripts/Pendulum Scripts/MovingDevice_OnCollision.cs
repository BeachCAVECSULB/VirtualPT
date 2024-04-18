using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
Connects with Tactor Controller
If Pendulum collides with user, play sound and fire tactor
 * */
public class MovingDevice_OnCollision : MonoBehaviour 
{
  public AudioSource thudSound;
    int ret;
    
  void Start()
    {
        
    thudSound = GetComponent<AudioSource>();
      Tdk.TdkInterface.InitializeTI();

      //(TactorController name, TactorControllerType, responsepacket?)  
      //returns:Tactor Controller Id
      ret = Tdk.TdkInterface.Connect("COM3", (int)Tdk.TdkDefines.DeviceTypes.Serial, 
      System.IntPtr.Zero);

    }

  void OnCollisionEnter(Collision col)
  {
    // special action when colliding with the CharacterModel
    if(col.gameObject.CompareTag("User"))//col.gameObject.name == "Boz"
    {
      thudSound.Play();
      /* 
      * (device_id, tactor_number, pulse_duration, delay_before_pulse)
      * device_id is the id of the entire tactor controller while tactor_number is the 
        individual tactors of the controller 
      * TdkInterface calls functions from the TactorInterface.dll library in the Tactor 
        plugin. Thats why TdkInterface is kind of empty.
      */
      Tdk.TdkInterface.Pulse(ret, 1, 250, 0);
    }
  }
}
