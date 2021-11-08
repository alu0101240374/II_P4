using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicrophoneScript : MonoBehaviour
{
    private int soundQuality = 44100;
    public int recordDuration = 10;
    public float loudnessDetection;
    
    string deviceName;
    AudioSource audioSource;
    void Start()
    {
        GameManager.eventRecordMic += recordMic;
        GameManager.eventPlayMic += playMic;
        GameManager.eventStopRecordingMic += stopRecording;
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
}
