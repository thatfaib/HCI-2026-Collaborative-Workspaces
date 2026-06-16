using UnityEngine;
using TMPro;
using UnityEngine.UI;   
using System.Collections;

public class LetterAdjust : MonoBehaviour
{
    RectTransform rectTransform;
    public float textSize = 3;
    public TextMeshPro[] segments;
    public Toggle[] segmentToggles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        StartCoroutine(CollectSegments());
    }
    public IEnumerator CollectSegments()
    {
        yield return new WaitForSeconds(0.2f);

        segments = GetComponentsInChildren<TextMeshPro>();
        segmentToggles = GetComponentsInChildren<Toggle>();
        resize();
    }

   void changeFontSize(float fontSize){
        foreach (var segment in segments)
        {
            segment.fontSize = fontSize;
        }
       }

    public void resize() {
        float requiredHeight = 0;
        foreach (var segment in segments)
        {
            requiredHeight += segment.preferredHeight + 0.5f;
        }
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,requiredHeight);
        rectTransform.transform.position = new Vector3(rectTransform.transform.position.x,-1.75f,rectTransform.transform.position.z);
   
   }
    
    [ContextMenu("ExportOne")]
    public string ExportSelectedSegment(){
        string message = "Ich verstehe folgenden Text nicht. Kannst du es in einfacher Sprache erklären? : \n";
        for (int i = 0; i < segmentToggles.Length; i++)
        {   
            if (segmentToggles[i].isOn){
                message += segments[i].text;
                break;
            }
        }  
        message += "";
        Debug.Log(message);
        return message;
    }

    [ContextMenu("ExportAll")]
     public string ExportAllSelectedSegments(){
        string message = "Ich verstehe folgende Textabschnitte nicht. Kannst du es in einfacher Sprache erklären ? Gehe auch auf den Zusammenhang zwischen den Abschnitten ein! : \n";
        for (int i = 0; i < segmentToggles.Length; i++)
        {
            if (segmentToggles[i].isOn){
                message += "Abschnitt " + (i+1).ToString() + " : \n";
                message += segments[i].text;
                message += "\n\n";
            }
        }  
        message += "";
        Debug.Log(message);
        return message;
    }
}
