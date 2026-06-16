using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class ButtonManager : MonoBehaviour
{
    public Toggle upbutton;
    public Image upimg;

    public Toggle downbutton;
    public Image downimg;

    AudioSource clicknoise;

    public GameObject letter;
    public GameObject simp;
    public GameObject harald;

    public Color off_color;
    public Color on_color;

    public float movespeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        clicknoise = GetComponent<AudioSource>();
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
        clicknoise.Play(0);
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
        clicknoise.Play(0);
    }
    // Update is called once per frame
    void Update()
    {
        if(upbutton.isOn){
            letter.transform.Translate(0, -movespeed * Time.deltaTime,0);
            simp.transform.Translate(0, -movespeed * Time.deltaTime,0);
            harald.transform.Translate(0, -movespeed * Time.deltaTime,0);
        }
        else if(downbutton.isOn){
            letter.transform.Translate(0, movespeed * Time.deltaTime,0);
            simp.transform.Translate(0, movespeed * Time.deltaTime,0);
            harald.transform.Translate(0, movespeed * Time.deltaTime,0);
        }
    }
}
