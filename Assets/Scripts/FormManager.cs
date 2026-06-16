using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class FormManager : MonoBehaviour
{
    public List<Toggle> toggles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i<toggles.Count; i++)
        {
            int index = i;
            toggles[i].onValueChanged.AddListener((isOn) => togglelistener(index, isOn));
        }
    }

    // Update is called once per frame
    void togglelistener(int i, bool isOn)
    {
        if (isOn)
        {
            for(int j = 0; j<toggles.Count; j++)
            {
                if (j != i)
                {
                    toggles[j].isOn = false;
                }
            }   
        }
    }
}
