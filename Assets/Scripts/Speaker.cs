using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speaker
{
    TextToSpeech speech;
    private GameObject[] speakable;
    public void Speak()
    {
        speech = new TextToSpeech();
        speakable = GameObject.FindGameObjectsWithTag("Speakable");
        speech.SpeakItems(speakable);
    }
}
