using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mic : MonoBehaviour
{
// Pseudocode-ish C#
    // Put this in a class
    private AudioSource src;
    void Start()
    {
        // FIXME:Get the list of input sources and check that they work here
        // Attach an AudioSource to this object to make the code below work
        src = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!Microphone.IsRecording(null))
        {
            src.clip = Microphone.Start(null, true, 110, 44100);
        }
        else
        {
            // Pause, do some magic to copy the clip to another buffer for processing, start recording again
        }
    }
}
