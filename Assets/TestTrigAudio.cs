using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class TestTrigAudio : MonoBehaviour
{
    /* 
    Attch this script to the gameobject that has a FMOD Event Emitter. 

    Assing the desired Event Emitter in the inspector field. 
    In the case for "SoundTriggerTest" Prefabs the gameobject has a event emitter with Harpoon Shoot assigned to it. 
    The Event Emitter has been dragged down and assigned to the Harpoon SFX field. 


    You can add multiple Event Emitter to the same gameobject, and manully assign them in the inspector window. 
    Make sure to decleare the SFX varuables in the script like I have done with the 2HarpoonSFX" Variable
    */

    [SerializeField] StudioEventEmitter HarpoonSFX;
    [SerializeField] StudioEventEmitter MySoundEffect; // Example of how to declear a FMOD Event SFX as a variable
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // When the Key "Y" is pressed down the HarpoonSFX will be played. 
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("Sound Triggered");
            HarpoonSFX.Play();
            /*
            Ideally we should also have another coniditon check to make sure that we cannot spam the sound effect while it's already playing. 
            Sudo code:
            If (PlayerIsShooting && !PlayingSFX) 
                SFX.Play()
            && = AND
            ! refereas to a Bool that is not true. So !PlayingSFX means that if this bool is false. 
            */  
        }
    }
}
