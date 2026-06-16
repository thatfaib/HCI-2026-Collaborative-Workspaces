using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LetterGenerator : MonoBehaviour
{
    public Letter letter;
    public TextMeshPro titleSegment;
    public GameObject textSegment;
      
    public TextMeshPro[] segments;
    public Toggle[] segmentToggles; 

    public RectTransform rectTransform;
    
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        titleSegment.text = letter.title;
        spawnSegments();
    }

    void spawnSegments() {
        foreach (var segment in letter.segments)
        {  
            GameObject segmentObject = Instantiate(textSegment,this.transform);
            TextMeshPro segmentText = segmentObject.GetComponent<TextMeshPro>();
            Toggle _toggle = segmentObject.GetComponentsInChildren<Toggle>()[0];

            segmentText.text = segment;
           }
        resizeRect();
        return;
    }
    
    
    public void resizeRect() {
        segments = GetComponentsInChildren<TextMeshPro>();
        //segmentToggles = GetComponentsInChildren<Toggle>();
        float requiredHeight = 0;
        foreach (var segment in segments)
        {
            requiredHeight += segment.preferredHeight + 0.5f;
        }
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,requiredHeight);
        Debug.Log(requiredHeight);
        rectTransform.transform.position = new Vector3(rectTransform.transform.position.x,0,rectTransform.transform.position.z);
   }
}
