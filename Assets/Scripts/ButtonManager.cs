using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonManager : MonoBehaviour
{
    public Toggle upbutton;
    public Toggle downbutton;
    public GameObject letter;

    public float movespeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        upbutton.onValueChanged.AddListener(buta);
        downbutton.onValueChanged.AddListener(butb);
    }

    //Ensure that upon the pressing the up button, the down button turns off
    public void buta(bool isOn){
        if(isOn){
            downbutton.isOn = false;
        }
    }

    //Ensure that upon the pressing the up button, the down button turns off
    public void butb(bool isOn){
        if(isOn){
            upbutton.isOn = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(upbutton.isOn){
            letter.transform.Translate(0, -movespeed * Time.deltaTime,0);
        }
        else if(downbutton.isOn){
            letter.transform.Translate(0, movespeed * Time.deltaTime,0);
        }
    }
}
