using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AutoStudyLogger : MonoBehaviour
{
    public static AutoStudyLogger Instance;

    [Header("Study Settings")]
    public string participantId = "P01";
    public string condition = "NoAI";

    [Header("Privacy")]
    public bool logTextContent = true;
    public bool redactLongText = true;
    public int maxTextLength = 80;

    [Header("Auto Logging")]
    public bool logButtons = true;
    public bool logInputFields = true;
    public bool logDropdowns = true;
    public bool logToggles = true;
    public bool logSliders = true;
    public bool logTextChanges = true;
    public bool logObjectActivation = true;
    public bool logConsoleMessages = true;

    private double sessionStartTime;
    private bool sessionRunning = false;
    private string filePath;
    private readonly List<string> rows = new List<string>();

    private readonly Dictionary<GameObject, bool> knownActiveStates = new Dictionary<GameObject, bool>();
    private readonly Dictionary<TMP_Text, string> knownTextValues = new Dictionary<TMP_Text, string>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (logConsoleMessages)
        {
            Application.logMessageReceived += HandleConsoleLog;
        }
    }

    private void Start()
    {
        AutoRegisterExistingObjects();
    }

    public void StartSession()
    {
        sessionStartTime = Time.realtimeSinceStartupAsDouble;
        sessionRunning = true;

        string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string fileName = $"studylog_{participantId}_{condition}_{timestamp}.csv";
        filePath = Path.Combine(Application.persistentDataPath, fileName);

        rows.Clear();
        rows.Add("time,participant,condition,event_type,object_name,value,scene,notes");

        LogEvent("SESSION_START", "study", "", "Session started");
        AutoRegisterExistingObjects();
    }

    public void EndSession()
    {
        if (!sessionRunning) return;

        LogEvent("SESSION_END", "study", GetElapsedTime().ToString("F2"), "Session ended");

        File.WriteAllLines(filePath, rows);
        Debug.Log("CSV gespeichert unter: " + filePath);

        sessionRunning = false;
    }

    private void Update()
    {
        if (!sessionRunning) return;

        if (logObjectActivation)
        {
            CheckObjectActivationChanges();
        }

        if (logTextChanges)
        {
            CheckTextChanges();
        }
    }

    private void AutoRegisterExistingObjects()
    {
        RegisterButtons();
        RegisterInputFields();
        RegisterDropdowns();
        RegisterToggles();
        RegisterSliders();
        RegisterTextObjects();
        RegisterActiveStates();

        SceneManager.activeSceneChanged -= OnSceneChanged;
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void RegisterButtons()
    {
        if (!logButtons) return;

        Button[] buttons = FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (Button button in buttons)
        {
            string buttonName = GetPath(button.gameObject);

            button.onClick.RemoveListener(() => LogEvent("BUTTON_CLICK", buttonName));

            button.onClick.AddListener(() =>
            {
                LogEvent("BUTTON_CLICK", buttonName);
            });
        }
    }

    private void RegisterInputFields()
    {
        if (!logInputFields) return;

        TMP_InputField[] inputFields = FindObjectsByType<TMP_InputField>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (TMP_InputField inputField in inputFields)
        {
            string fieldName = GetPath(inputField.gameObject);

            inputField.onSelect.AddListener((value) =>
            {
                LogEvent("INPUT_FIELD_SELECTED", fieldName);
            });

            inputField.onValueChanged.AddListener((value) =>
            {
                LogEvent("INPUT_FIELD_CHANGED", fieldName, PrepareText(value));
            });

            inputField.onEndEdit.AddListener((value) =>
            {
                LogEvent("INPUT_FIELD_END_EDIT", fieldName, PrepareText(value));
            });
        }
    }

    private void RegisterDropdowns()
    {
        if (!logDropdowns) return;

        TMP_Dropdown[] dropdowns = FindObjectsByType<TMP_Dropdown>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (TMP_Dropdown dropdown in dropdowns)
        {
            string dropdownName = GetPath(dropdown.gameObject);

            dropdown.onValueChanged.AddListener((value) =>
            {
                string selectedText = "";

                if (dropdown.options != null && value >= 0 && value < dropdown.options.Count)
                {
                    selectedText = dropdown.options[value].text;
                }

                LogEvent("DROPDOWN_CHANGED", dropdownName, value + ":" + PrepareText(selectedText));
            });
        }
    }

    private void RegisterToggles()
    {
        if (!logToggles) return;

        Toggle[] toggles = FindObjectsByType<Toggle>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (Toggle toggle in toggles)
        {
            string toggleName = GetPath(toggle.gameObject);

            toggle.onValueChanged.AddListener((value) =>
            {
                LogEvent("TOGGLE_CHANGED", toggleName, value.ToString());
            });
        }
    }

    private void RegisterSliders()
    {
        if (!logSliders) return;

        Slider[] sliders = FindObjectsByType<Slider>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (Slider slider in sliders)
        {
            string sliderName = GetPath(slider.gameObject);

            slider.onValueChanged.AddListener((value) =>
            {
                LogEvent("SLIDER_CHANGED", sliderName, value.ToString("F2"));
            });
        }
    }

    private void RegisterTextObjects()
    {
        if (!logTextChanges) return;

        TMP_Text[] texts = FindObjectsByType<TMP_Text>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (TMP_Text text in texts)
        {
            if (!knownTextValues.ContainsKey(text))
            {
                knownTextValues.Add(text, text.text);
            }
        }
    }

    private void RegisterActiveStates()
    {
        if (!logObjectActivation) return;

        Transform[] allTransforms = FindObjectsByType<Transform>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (Transform t in allTransforms)
        {
            if (!knownActiveStates.ContainsKey(t.gameObject))
            {
                knownActiveStates.Add(t.gameObject, t.gameObject.activeInHierarchy);
            }
        }
    }

    private void CheckObjectActivationChanges()
    {
        List<GameObject> objects = new List<GameObject>(knownActiveStates.Keys);

        foreach (GameObject obj in objects)
        {
            if (obj == null) continue;

            bool currentState = obj.activeInHierarchy;

            if (knownActiveStates[obj] != currentState)
            {
                knownActiveStates[obj] = currentState;
                LogEvent("OBJECT_ACTIVE_CHANGED", GetPath(obj), currentState.ToString());
            }
        }
    }

    private void CheckTextChanges()
    {
        List<TMP_Text> texts = new List<TMP_Text>(knownTextValues.Keys);

        foreach (TMP_Text text in texts)
        {
            if (text == null) continue;

            string currentText = text.text;

            if (knownTextValues[text] != currentText)
            {
                knownTextValues[text] = currentText;
                LogEvent("TEXT_CHANGED", GetPath(text.gameObject), PrepareText(currentText));
            }
        }
    }

    private void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        LogEvent("SCENE_CHANGED", newScene.name);
        AutoRegisterExistingObjects();
    }

    private void HandleConsoleLog(string logString, string stackTrace, LogType type)
    {
        if (!sessionRunning) return;

        LogEvent("UNITY_CONSOLE_" + type.ToString().ToUpper(), "console", PrepareText(logString));
    }

    public void LogEvent(string eventType, string objectName = "", string value = "", string notes = "")
    {
        if (!sessionRunning && eventType != "SESSION_START") return;

        string row =
            Escape(GetElapsedTime().ToString("F2")) + "," +
            Escape(participantId) + "," +
            Escape(condition) + "," +
            Escape(eventType) + "," +
            Escape(objectName) + "," +
            Escape(value) + "," +
            Escape(SceneManager.GetActiveScene().name) + "," +
            Escape(notes);

        rows.Add(row);
    }

    private double GetElapsedTime()
    {
        return Time.realtimeSinceStartupAsDouble - sessionStartTime;
    }

    private string PrepareText(string text)
    {
        if (!logTextContent)
        {
            return "[TEXT_REDACTED]";
        }

        if (string.IsNullOrEmpty(text))
        {
            return "";
        }

        text = text.Replace("\n", " ").Replace("\r", " ");

        if (redactLongText && text.Length > maxTextLength)
        {
            return text.Substring(0, maxTextLength) + "...";
        }

        return text;
    }

    private string Escape(string value)
    {
        if (value == null) return "";
        value = value.Replace("\"", "\"\"");
        return "\"" + value + "\"";
    }

    private string GetPath(GameObject obj)
    {
        if (obj == null) return "null";

        string path = obj.name;
        Transform current = obj.transform.parent;

        while (current != null)
        {
            path = current.name + "/" + path;
            current = current.parent;
        }

        return path;
    }

    private void OnApplicationPause(bool pause)
    {
        if (!sessionRunning) return;

        LogEvent(pause ? "APPLICATION_PAUSE" : "APPLICATION_RESUME", "application");
    }

    private void OnApplicationQuit()
    {
        if (logConsoleMessages)
        {
            Application.logMessageReceived -= HandleConsoleLog;
        }

        if (sessionRunning)
        {
            EndSession();
        }
    }
}
