using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonManager : MonoBehaviour
{
    public Toggle upbutton;
    public Image upimg;

    public Toggle downbutton;
    public Image downimg;


    public GameObject letter;

    public Color off_color;
    public Color on_color;

    public float movespeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        upbutton.onValueChanged.AddListener(buta);
        upimg.color = off_color;

        downbutton.onValueChanged.AddListener(butb);
        downimg.color = off_color;
    }

    //Ensure that upon the pressing the up button, the down button turns off, also keep track of colors
    public void buta(bool isOn){
        if(isOn){
            downbutton.isOn = false;
            downimg.color = off_color;
            upimg.color = on_color;
        }
        else{
            upimg.color = off_color;
        }
    }

    //Ensure that upon the pressing the up button, the down button turns off, also keep track of colors
    public void butb(bool isOn){
        if(isOn){
            upbutton.isOn = false;
            upimg.color = off_color;
            downimg.color = on_color;
        }
        else{
            downimg.color = off_color;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(upbutton.isOn){
            letter.transform.Translate(0, movespeed * Time.deltaTime,0);
        }
        else if(downbutton.isOn){
            letter.transform.Translate(0, -movespeed * Time.deltaTime,0);
        }
    }
}
