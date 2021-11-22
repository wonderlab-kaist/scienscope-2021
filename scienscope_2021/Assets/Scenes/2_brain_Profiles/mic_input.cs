using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]

public class mic_input : MonoBehaviour
{
    AudioSource audio_this;
    string input;
    float sensitivity = 100f;
    float mTimer, mRefTime;
    int minFreq, maxFreq;
    int amountSamples = 5;

    float loudness;

    public Text debug;

    void Start()
    {
        audio_this = this.GetComponent<AudioSource>();
        mRefTime = 1.0f;
        audio_this.loop = true; // Set the AudioClip to loop
        //audio_this.mute = false; // Mute the sound, we don't want the player to hear it
        input = Microphone.devices[0].ToString();
        GetMicCaps();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Microphone.devices[0].ToString());
        loudness = GetAveragedVolume() * sensitivity;
        debug.text = loudness.ToString() + "  " +input;
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
        audio_this.clip = Microphone.Start(input, true, 10, maxFreq);//Starts recording
        while (!(Microphone.GetPosition(input) > 0)) { } // Wait until the recording has started
        audio_this.Play(); // Play the audio source!
    }

    public void StopMicrophone()
    {
        audio_this.Stop();//Stops the audio
        Microphone.End(input);//Stops the recording of the device
    }

    float GetAveragedVolume()
    {
        float[] data = new float[amountSamples];
        float a = 0;
        audio_this.GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        return a / (float)amountSamples;
    }
    
    public float getLoudness()
    {
        return loudness;
    }
}
