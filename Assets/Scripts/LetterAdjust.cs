using UnityEngine;
using TMPro;
using System.Collections;

public class LetterAdjust : MonoBehaviour
{
    public RectTransform rectTransform;
    public float textSize = 3;
    public TextMeshPro[] segments;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        
        StartCoroutine(CollectSegments());
        segments = GetComponentsInChildren<TextMeshPro>();
        changeFontSize(3);
    }


    IEnumerator CollectSegments()
    {

        yield return new WaitForSeconds(0.2f);

        segments = GetComponentsInChildren<TextMeshPro>();
        changeFontSize(3);
    }
   void changeFontSize(float fontSize){
        float requiredHeight = 0;
        foreach (var segment in segments)
        {
            segment.fontSize = fontSize;
            requiredHeight += segment.preferredHeight;
        }
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,requiredHeight);
        //rectTransform.rect.height = requiredHeight;
        Debug.Log(requiredHeight);
   }

   void Scroll() {

   }
}
