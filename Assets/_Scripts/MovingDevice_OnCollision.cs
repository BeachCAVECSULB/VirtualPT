using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This program is the listener for any collisions on the CharacterModel.
 * Later on, it will be ideal to add the write data function on collision
 * as well.
 * */
public class MovingDevice_OnCollision : MonoBehaviour {
	public AudioSource thudSound;
    int ret;
	void Start(){
        
		thudSound = GetComponent<AudioSource>();
        Tdk.TdkInterface.InitializeTI();
        ret = Tdk.TdkInterface.Connect("COM3",
                                           (int)Tdk.TdkDefines.DeviceTypes.Serial,
                                            System.IntPtr.Zero);
        // hand = GameObject.Find("/Monster/Arm/Hand");Chest

    }

    void OnCollisionEnter(Collision col){
        // special action when colliding with the CharacterModel
		if(col.gameObject.name == "Boz"){
			thudSound.Play();
            Tdk.TdkInterface.Pulse(ret, 1, 250, 0);
            // gameObject.GetComponent<MovingDevice_InfiniteMove>().enabled = false;
        }
	}
}
