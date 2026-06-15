using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollManager : MonoBehaviour
{
    public Scrollbar Scroller;
    public GameObject letter;

    public float scrollval;

    private float height;
    private Vector3 startpos;
    private RectTransform recter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Scroller.onValueChanged.AddListener(scrolled);
        StartCoroutine(starter());
    }

    void scrolled(float value)
    {
        recter.anchoredPosition3D = new Vector3(startpos.x, startpos.y, startpos.z - (height * value));
        scrollval = value;
    }

    IEnumerator starter(){
        yield return new WaitForSeconds(0.5f);
        recter = letter.GetComponent<RectTransform>();
        height = recter.rect.height;
        Debug.Log(height);
        startpos = recter.anchoredPosition3D;
    }
}
