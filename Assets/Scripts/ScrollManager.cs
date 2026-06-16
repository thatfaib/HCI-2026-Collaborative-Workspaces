using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Numerics;

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
        letterrecter.anchoredPosition3D = new Vector3(startpos.x, startpos.y, startpos.z - (letterheight * value));
    }

    IEnumerator starter(){
        yield return new WaitForSeconds(0.5f);
        letterrecter = letter.GetComponent<RectTransform>();
        letterheight = letterrecter.rect.height;
        letterstart = letterrecter.anchoredPosition3D;
    }
}
