using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMicButton : MonoBehaviour
{
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(startRecording);
    }

    public void startRecording() 
    {
       GameManager.manager.recordMic();
    }
}
