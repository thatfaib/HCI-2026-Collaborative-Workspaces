using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEngine.UI;

public class LetterGenerator : MonoBehaviour
{
    public Letter letter;
    public TextMeshPro titleSegment;
    public GameObject textSegment;
      
    
    void Start()
    {
        titleSegment.text = letter.title;
        spawnSegments();
    }

    void spawnSegments() {
        
        foreach (var segment in letter.segments)
        {   
            GameObject segmentObject = Instantiate(textSegment,this.transform);
            TextMeshPro segmentText = segmentObject.GetComponent<TextMeshPro>();
            segmentText.text= segment;
        }
        return;
    }
    
}
