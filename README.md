# Práctica 4 - Micrófono y cámara

> Gabriel García Jaubert
>
> Universidad de La Laguna
>
> 7 de noviembre de 2021


La aplicación creada genera una sencilla interfaz donde el usuario puede pulsar botones haciendo uso del click del ratón para utilizar la cámara y el micrófono.

![Demostración](./Gifs/Camara.gif)

## Cámara

Para la cámara se utilizó un plano que funciona a modo de pantalla. Para reflejar en el plano las imágenes captadas en la cámara se le asigna un material en el que la textura representa esas imágenes. Para ello se crea un script ```WebCamera.cs``` que es el que gestiona la creación de la cámara, junto a las acciones que esta puede hacer. El código en cuestión es el siguiente:  

```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebCamera : MonoBehaviour
{
    WebCamTexture webCamTexture;
    void Start()
    {   
        WebCamDevice[] devices = WebCamTexture.devices;
        webCamTexture = new WebCamTexture();
        webCamTexture.deviceName = devices[1].name;

        GameManager.eventPlayCamera += playCamera;
        GameManager.eventPauseCamera += pauseCamera;
        GameManager.eventStopCamera += stopCamera;
    }

    void playCamera()
    {
        GetComponent<Renderer>().material.mainTexture = webCamTexture;
        webCamTexture.Play();
    }

    void pauseCamera()
    {
        webCamTexture.Pause();
    }

    void stopCamera()
    {
        webCamTexture.Stop();
        GetComponent<Renderer>().material.mainTexture = default;
    }
}

```

Observamos que el primer paso es crear el objeto de la cámara. Es importante especificar qué cámara queremos capturar con 'devices[n].name', ya que por ejemplo en mi caso, tenía varias cámaras instaladas por lo que si no se especifica surgen errores. El siguiente paso es suscribirse a los eventos que requieren del uso de la cámara. Estos eventos están asociados a las siguientes funciones:  
  - playCamera(): Comienza la captura de la cámara.
  - pauseCamera(): Pausa la captura de la cámara.
  - stopCamera(): Termina la captura y devuelve el material original a la pantalla.

Para activar estos eventos se utiliza un GameManager que aparte de gestionar la cámara también gestiona micrófono. Ese script es el siguiente:  

```c#
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
```

En este script utilizamos el patrón singleton para que esta clase solo pueda tener una instancia existente al mismo tiempo. Más tarde, crea los métodos delegados y los eventos, y define las funciones públicas con las que se pueden activar estos eventos. Este script está asignado a un objeto vacío que está en la parte superior de la jerarquía de objetos en la escena. 

Para que las funciones del GameManager sean llamadas hace falta la interacción del usuario con el ratón. Para ello se creó un canvas en la escena que contiene botones de la clase Button. Estos botones son los encargados de notificar al GameManager cuando el usuario ha hecho click sobre ellos. Esto es sencillo ya que los botones tiene la opción de añadir un `onclick.AddEventListener(función)`, que permite crear un evento que esté a la escucha y llame a la función indicada como parámetro cuando concretamente se realice un 'click' sobre él. Un ejemplo de estos botones es el de *Comenzar a grabar*. Los otros botones son casi idénticos:  

```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCameraButton : MonoBehaviour
{
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(playCamera);
    }

    public void playCamera() 
    {
       GameManager.manager.playCamera();
    }
}
```

## Micrófono

Para interactuar con el micrófono se ofrece la misma interfaz que con la cámara, pero con ligeras variaciones en la implementación. También se crearon botones que veremos más adelante, y utiliza el GameManager como gestor de eventos, pero el objeto micrófono, es un objeto con el componente AudioSource, en el que se modifica y asigna el atributo clip, para que sea el clip capturado por el micrófono. El script de este objeto es el siguiente:  

```c#
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
        int sampleDataLength = 1024;
        float clipLoudness;
        float[] clipSampleData = new float[sampleDataLength];
        
        audioSource.clip.GetData(clipSampleData, audioSource.timeSamples);
        clipLoudness = 0f;

        foreach (var sample in clipSampleData)
        {
            clipLoudness += Mathf.Abs(sample);
            Debug.Log(Mathf.Abs(sample));
        }
        
        clipLoudness /= sampleDataLength;

        Debug.Log(clipLoudness);
        if (clipLoudness > loudnessDetection)
          GameManager.manager.loudClip();
    }

    void stopRecording()
    {
        Microphone.End(deviceName);
    }
}
```

Como en el script de la cámara, declaramos los eventos a los que nos queremos suscribir. Las funciones en cuestión son:  

  - recordMic: Comienza la grabación del micrófono.
  - playMic: Reproduce el audio grabado por el micrófono
  - stopRecording: Termina la grabación del micrófono si esta no ha llegado a su límite de tiempo.

En la función `recordMic` obtenemos el componente de tipo AudioSource. Después guardamos en un array de tipo string todos los dispositivos. Para comenzar a grabar simplemente escribimos `audioSource.clip = Microphone.Start(deviceName, false, recordDuration, soundQuality)`, donde deviceName es el nombre del micrófono que queremos usar, *false* es que no queremos hacer un bucle en al grabación y recordDuration es el tiempo que le asigna el usuario de grabación, cuando termine el tiempo se cortará la grabación. Por último está soundQuality, que es un valor int de 44100. Para `stopRecording` simplemente llamamos al método estático de la clase *Microphone*: **End(deviceName)**.
Por último se encuentra la función `playMic`, que reproduce el clip grabado previamente.

Por último veremos un ejemplo de botón para activar el micrófono, en este caso, para comenzar a grabar:  

```c#
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
```