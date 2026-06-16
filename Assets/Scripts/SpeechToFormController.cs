using UnityEngine;
using TMPro;
using Whisper;
using Whisper.Utils;

public class SpeechToFormController : MonoBehaviour
{
    public WhisperManager whisperManager;
    public MicrophoneRecord microphoneRecord;
    public TMP_Text statusText;

    private TMP_InputField activeField;
    private bool isRecording = false;

    private void OnEnable()
    {
        microphoneRecord.OnRecordStop += OnRecordStop;
    }

    private void OnDisable()
    {
        microphoneRecord.OnRecordStop -= OnRecordStop;
    }

    public void StartRecordingForField(TMP_InputField targetField)
    {
        if (isRecording)
        {
            statusText.text = "Aufnahme läuft bereits.";
            return;
        }

        activeField = targetField;
        isRecording = true;
        statusText.text = "Aufnahme läuft...";
        microphoneRecord.StartRecord();
    }

    public void StopRecording()
    {
        if (!isRecording)
        {
            statusText.text = "Keine Aufnahme aktiv.";
            return;
        }

        statusText.text = "Transkribiere...";
        microphoneRecord.StopRecord();
    }

    private async void OnRecordStop(AudioChunk recordedAudio)
    {
        isRecording = false;

        if (activeField == null)
        {
            statusText.text = "Kein Feld ausgewählt.";
            return;
        }

        var result = await whisperManager.GetTextAsync(
            recordedAudio.Data,
            recordedAudio.Frequency,
            recordedAudio.Channels
        );

        activeField.text = result.Result.Trim();
        statusText.text = "Text eingefügt.";

        activeField = null;
    }
}
