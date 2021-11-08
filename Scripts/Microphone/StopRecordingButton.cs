using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopRecordingButton : MonoBehaviour
{
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(stopRecording);
    }

    void stopRecording()
    {
        GameManager.manager.stopRecordingMic();
    }
}
