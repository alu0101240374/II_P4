using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public delegate void startRecordingMicDelegate();
    public static event startRecordingMicDelegate eventRecordMic;

    public delegate void playMicDelegate();
    public static event startRecordingMicDelegate eventPlayMic;

    public delegate void stopRecordingMicDelegate();
    public static event startRecordingMicDelegate eventStopRecordingMic;

    public delegate void playCameraDelegate();
    public static event playCameraDelegate eventPlayCamera;

    public delegate void pauseCameraDelegate();
    public static event pauseCameraDelegate eventPauseCamera;

    public delegate void stopCameraDelegate();
    public static event stopCameraDelegate eventStopCamera;

    public delegate void loudAudioDelegate();
    public static event stopCameraDelegate eventLoudAudio;


    public static GameManager manager;
    void Awake() 
      {
          if (manager == null)
          {
            manager = this;
            DontDestroyOnLoad(this);
          } else if (manager != this)
          {
              Destroy(gameObject);
          }
      }

    void Update()
    {
        
    }

    public void recordMic()
    {
        eventRecordMic();
    }

    public void playMic()
    {
        eventPlayMic();
    }

    public void stopRecordingMic()
    {
        eventStopRecordingMic();
    }

    public void playCamera()
    {
        eventPlayCamera();
    }

    public void pauseCamera()
    {
        eventPauseCamera();
    }

    public void stopCamera()
    {
        eventStopCamera();
    }

    public void loudClip()
    {
        eventLoudAudio();
    }
}
