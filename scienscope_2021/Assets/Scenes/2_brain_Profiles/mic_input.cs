using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class mic_input : MonoBehaviour
{
    AudioSource audio;
    string input;
    float sensitivity = 100f;
    float mTimer, mRefTime;
    int minFreq, maxFreq;
    int amountSamples = 5;

    float loudness;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        mRefTime = 1.0f;
        audio.loop = true; // Set the AudioClip to loop
        audio.mute = false; // Mute the sound, we don't want the player to hear it
        input = Microphone.devices[0].ToString();
        GetMicCaps();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Microphone.devices[0].ToString());
        loudness = GetAveragedVolume() * sensitivity;

        if (!Microphone.IsRecording(input))
        {
            StartMicrophone();
        }

        mTimer += Time.deltaTime;

        if (mTimer >= mRefTime)
        {
            StopMicrophone();
            StartMicrophone();
            mTimer = 0;
        }

        //Debug.Log(loudness);
    }

    void GetMicCaps()
    {
        Microphone.GetDeviceCaps(input, out minFreq, out maxFreq);//Gets the frequency of the device

        if (maxFreq - minFreq < 1)
        {
            minFreq = 0;
            maxFreq = 44100;
        }
    }

    public void StartMicrophone()
    {
        audio.clip = Microphone.Start(input, true, 10, maxFreq);//Starts recording
        while (!(Microphone.GetPosition(input) > 0)) { } // Wait until the recording has started
        audio.Play(); // Play the audio source!
    }

    public void StopMicrophone()
    {
        audio.Stop();//Stops the audio
        Microphone.End(input);//Stops the recording of the device
    }

    float GetAveragedVolume()
    {
        float[] data = new float[amountSamples];
        float a = 0;
        audio.GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        return a / amountSamples;
    }
    
    public float getLoudness()
    {
        return loudness;
    }
}
