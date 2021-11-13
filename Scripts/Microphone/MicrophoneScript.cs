using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MicrophoneScript : MonoBehaviour
{
    public static MicrophoneScript instance;
    private int soundQuality = 44100;
    public int recordDuration = 10;
    public float loudnessDetection;
    
    string deviceName;
    AudioSource audioSource;

    public float[] spectrumWitdh;

    void Start()
    {
        instance = this;
        GameManager.eventRecordMic += recordMic;
        GameManager.eventPlayMic += playMic;
        GameManager.eventStopRecordingMic += stopRecording;
        spectrumWitdh = new float[64];
    }

    void FixedUpdate()
    {   
        if (audioSource != null)
            audioSource.GetSpectrumData(spectrumWitdh, 0, FFTWindow.Blackman);
    }

    void recordMic()
    {
        audioSource = GetComponent<AudioSource>();
        string[] devices = Microphone.devices;
        deviceName = devices[0];
        audioSource.clip = Microphone.Start(deviceName, false, recordDuration, soundQuality);
    }

    void playMic()
    { 
        audioSource.Play();
    }

    void stopRecording()
    {
        Microphone.End(deviceName);
    }

    public float getFrecuenciesDiapason(int start, int end, int mult)
    {
        return spectrumWitdh.ToList().GetRange(start, end).Average() * mult;
    }
}
