using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LetterGenerator : MonoBehaviour
{
    public Letter letter;
    public LetterManager letterManager;
    public TextMeshPro titleSegment;
    public GameObject textSegment;      
    void Start()
    {
        letterManager = GetComponent<LetterManager>();
        titleSegment.text = letter.title;
        
        StartCoroutine(spawnSegments());
    }

    IEnumerator spawnSegments() {
        foreach (var segment in letter.segments)
        {  
            GameObject segmentObject = Instantiate(textSegment,this.transform);
            TextMeshPro segmentText = segmentObject.GetComponent<TextMeshPro>();
            segmentText.text = segment;
        }
        yield return new WaitForSeconds(0.2f);
        letterManager.resizeRect();
        letterManager.alignRectTop();
    
    }
    
    
}   
