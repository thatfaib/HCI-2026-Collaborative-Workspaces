using UnityEngine;
using TMPro;
using UnityEngine.UI;   
using System.Collections;

public class LetterManager : MonoBehaviour
{
    public float textSize = 3;
    public TextMeshPro[] segments;
    public Toggle[] segmentToggles;

    public RectTransform rectTransform;
    VerticalLayoutGroup layoutGroup;
    GameObject Panel;

    void Start()
    {
        Panel = gameObject.transform.parent.gameObject;
        layoutGroup = GetComponent<VerticalLayoutGroup>();
        rectTransform = GetComponent<RectTransform>();
    }
    void changeFontSize(float fontSize){
        foreach (var segment in segments)
        {
            segment.fontSize = fontSize;
        }
    }
    
    public void resizeRect() {
        segments = GetComponentsInChildren<TextMeshPro>();
        //segmentToggles = GetComponentsInChildren<Toggle>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        float requiredHeight = LayoutUtility.GetPreferredHeight(rectTransform) * 1.2f;
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,requiredHeight); 
   }

   public void alignRectTop()
    {   
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        RectTransform panelRectTransform =  Panel.GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(panelRectTransform);
        float textheight = rectTransform.rect.height;
        float panelheight= panelRectTransform.rect.height;

        float z = panelheight * 0.5f - textheight * 0.5f;

        rectTransform.localPosition = new Vector3(rectTransform.localPosition.x,rectTransform.localPosition.y,-z+0.5f);  
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

