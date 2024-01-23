using UnityEngine;
using SpeechLib;
using TMPro;
using System.Linq;
using Unity.VisualScripting;

public class TextToSpeech {
    SpVoice voice = new SpVoice();
    public void Speak(string text) {
        voice.Speak(text, SpeechVoiceSpeakFlags.SVSFlagsAsync);
    }
    public void SpeakItems(GameObject[] items) {
        var list = items.ToList();
        list.Sort((y,x) => x.transform.position.y.CompareTo(y.transform.position.y));

        foreach (GameObject obj in list) {
            Speak(obj.GetComponent<TMP_Text>().text);
        }
    }
}