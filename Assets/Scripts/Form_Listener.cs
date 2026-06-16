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
        micsprite.color = offColor;
        mic.onValueChanged.AddListener(Toggler);

        // Event einmal registrieren
        microphoneRecord.OnRecordStop += OnRecordStop;
        Debug.Log("WhisperManager: " + whisper);
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
        outputText.text = "STOP erkannt";

        try
        {
            outputText.text = "Verarbeitung Beginnt";

            var result = await whisper.GetTextAsync(
                recordedAudio.Data,
                recordedAudio.Frequency,
                recordedAudio.Channels
            );

            outputText.text = "Verarbeitung ist fertig";

            if (result == null)
            {
                outputText.text = "Result NULL";
                return;
            }

            if (string.IsNullOrEmpty(result.Result))
            {
                outputText.text = "Kein Text erkannt";
                return;
            }

            outputText.text = result.Result;
        }
        catch (System.Exception e)
        {
            outputText.text = "FEHLER:\n" + e.Message;
        }
    }
}