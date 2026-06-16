using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollManager : MonoBehaviour
{
    public Scrollbar Scroller;
    public GameObject letter;
    public GameObject simp;
    public GameObject harald;

    private float letterheight;
    private float simpheight;
    private float haraldheight;
    private Vector3 letterstart;
    private Vector3 simpstart;
    private Vector3 haraldstart;

    private RectTransform letterrecter;
    private RectTransform simprecter;
    private RectTransform haraldrecter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Scroller.onValueChanged.AddListener(scrolled);
        StartCoroutine(starter());
    }

    void scrolled(float value)
    {
        letterrecter.anchoredPosition3D = new Vector3(letterstart.x, letterstart.y, letterstart.z - (letterheight * value));
        simprecter.anchoredPosition3D = new Vector3(simpstart.x, simpstart.y, simpstart.z - (simpheight * value));
        haraldrecter.anchoredPosition3D = new Vector3(haraldstart.x, haraldstart.y, haraldstart.z - (haraldheight * value));
    }

    IEnumerator starter(){
        yield return new WaitForSeconds(0.5f);
        letterrecter = letter.GetComponent<RectTransform>();
        letterstart = letterrecter.anchoredPosition3D;
        letterheight = letterrecter.rect.height;
        simprecter = simp.GetComponent<RectTransform>();
        simpstart = simprecter.anchoredPosition3D;
        simpheight = simprecter.rect.height;
        haraldrecter = harald.GetComponent<RectTransform>();
        haraldstart = haraldrecter.anchoredPosition3D;
        haraldheight = haraldrecter.rect.height;
        
        
    }
}
