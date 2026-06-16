using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Whisper;
using Whisper.Utils;

public class Form_Listener : MonoBehaviour
{
    [Header("Whisper")]
    public WhisperManager whisper;
    public MicrophoneRecord microphoneRecord;

    [Header("UI")]
    public TMP_Text outputText;
    public Toggle mic;

    public Color offColor;
    public Color onColor;
    public Image micsprite;

    private void Start()
    {
        mic.onValueChanged.AddListener(Toggler);

        // Event einmal registrieren
        microphoneRecord.OnRecordStop += OnRecordStop;
    }

    private void OnDestroy()
    {
        // sauber entfernen
        microphoneRecord.OnRecordStop -= OnRecordStop;
    }

    private void Toggler(bool isOn)
    {
        if (isOn)
        {
            micsprite.color = onColor;
            StartRecording();
        }
        else
        {
            micsprite.color = offColor;
            StopRecording();
        }
    }

    public void StartRecording()
    {
        outputText.text = "Aufnahme läuft...";
        microphoneRecord.StartRecord();
    }

    public void StopRecording()
    {
        outputText.text = "Transkribiere...";
        microphoneRecord.StopRecord();
    }

    private async void OnRecordStop(AudioChunk recordedAudio)
    {
        var result = await whisper.GetTextAsync(
            recordedAudio.Data,
            recordedAudio.Frequency,
            recordedAudio.Channels
        );

        outputText.text = result.Result;
        Debug.Log(result.Result);
    }
}