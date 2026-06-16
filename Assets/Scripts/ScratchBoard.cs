using UnityEngine;
using TMPro;

public class ScratchBoard : MonoBehaviour
{
    public GameObject textSegment;
    public LetterManager letterManager;
    
    public TextMeshPro[] segments;

    void Start()
    {
        letterManager = GetComponent<LetterManager>();
    
    }


    public void addText(string text){

        GameObject segmentObject = Instantiate(textSegment,this.transform);
        TextMeshPro segmentText = segmentObject.GetComponent<TextMeshPro>();
        segmentText.text = text;

        letterManager.resizeRect();
        letterManager.alignRectTop();
    }

    public void removeText(string text)
    {   
        segments = GetComponentsInChildren<TextMeshPro>();
        foreach (var segment in segments)
        {
            if (segment.text == text)
            {
                Destroy(segment.gameObject);
            }
        }
        
        letterManager.resizeRect();
        letterManager.alignRectTop();
    }
}
