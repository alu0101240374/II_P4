using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeObjects : MonoBehaviour
{
    public Transform objectReactingToBasses, objectReactingToNB, objectReactingToMiddles, objectReactingToHighs; 

    [SerializeField] float t = 0.1f;

    MicrophoneScript microphone;

    void FixedUpdate()
    {
        makeObjectShakeScale();
    }

    void makeObjectShakeScale()
    {
        objectReactingToBasses.localScale = Vector3.Lerp(objectReactingToBasses.localScale, new Vector3(1, 1, MicrophoneScript.instance.getFrecuenciesDiapason(0, 7, 10)), t);

        objectReactingToNB.localScale = Vector3.Lerp(objectReactingToNB.localScale, new Vector3(1, 1, MicrophoneScript.instance.getFrecuenciesDiapason(7, 15, 100)), t);

        objectReactingToMiddles.localScale = Vector3.Lerp(objectReactingToMiddles.localScale, new Vector3(1, 1, MicrophoneScript.instance.getFrecuenciesDiapason(15, 30, 200)), t);

        objectReactingToHighs.localScale = Vector3.Lerp(objectReactingToHighs.localScale, new Vector3(1, 1, MicrophoneScript.instance.getFrecuenciesDiapason(30, 32, 1000)), t);
    }
}
