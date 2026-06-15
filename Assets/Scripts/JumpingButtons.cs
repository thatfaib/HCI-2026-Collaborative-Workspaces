using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JumpingButtons : MonoBehaviour
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

    //Ensure that upon the pressing the up button, the down button turns off, also keep track of colors
    public void buta(bool isOn){
        letter.transform.Translate(0, -movespeed * Time.deltaTime,0);
    }

    //Ensure that upon the pressing the up button, the down button turns off, also keep track of colors
    public void butb(bool isOn){
        letter.transform.Translate(0, movespeed * Time.deltaTime,0);
    }

}