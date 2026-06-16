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

    private void OnEnable()
    {
        microphoneRecord.OnRecordStop += OnRecordStop;
    }

    private void OnDisable()
    {
        microphoneRecord.OnRecordStop -= OnRecordStop;
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
