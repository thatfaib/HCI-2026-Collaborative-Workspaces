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

    public void removeText(int id)
    {
        segments = GetComponentsInChildren<TextMeshPro>();   
        TextMeshPro segmentToRemove = segments[id];
        Destroy(segmentToRemove.gameObject);       
    }
}
