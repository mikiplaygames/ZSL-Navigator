using UnityEngine;
using SpeechLib;

public class TextToSpeech {
    SpVoice voice = new SpVoice();
    public void Speak(string text) {
        voice.Speak(text, SpeechVoiceSpeakFlags.SVSFlagsAsync);
    }
}