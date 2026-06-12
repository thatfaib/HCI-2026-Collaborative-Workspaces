using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEngine.UI;

public class LetterGenerator : MonoBehaviour
{
    public Letter letter;
    public TextMeshProUGUI titleSegment;
    public GameObject textSegment;
    
    
    void Start()
    {
    
        titleSegment.textInput.text = letter.title
        spawnSegments()
    }

    void Update()
    {
        
    }

    void spawnSegments(){
        foreach (segment in letter.segments){
            t.
            Instantiate()
        }
    }
}
