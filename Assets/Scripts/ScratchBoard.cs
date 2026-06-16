using UnityEngine;
using TMPro;

public class ScratchBoard : MonoBehaviour
{
    public GameObject textSegment;
    LetterAdjust letterAdjust;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        letterAdjust = GetComponent<LetterAdjust>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addText(string text){

        GameObject segmentObject = Instantiate(textSegment,this.transform);
        TextMeshPro segmentText = segmentObject.GetComponent<TextMeshPro>();
        segmentText.text = text;
        StartCoroutine(letterAdjust.CollectSegments());
    }
}
