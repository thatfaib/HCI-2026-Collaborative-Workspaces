using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToggleColorDriver : MonoBehaviour
{
    public Toggle toggle;
    public Image targetImage;
    public SwitchPanel switchPanel;
    public ScratchBoard scratchBoard;

    public Color offColor = Color.gray;
    public Color onColor = Color.green;
    public Color hoverColor = Color.yellow;

    private bool isHovered;

    void Awake()
    {
        toggle.onValueChanged.AddListener(UpdateColor);
        toggle.onValueChanged.AddListener(highlightToggle);
        switchPanel = GameObject.FindGameObjectsWithTag("SwitchPanel")[0].GetComponent<SwitchPanel>();
        scratchBoard = switchPanel.scratchBoardPanel.GetComponent<ScratchBoard>();
        UpdateColor(toggle.isOn);
    }

    public void SetHover(bool hover)
    {
        isHovered = hover;
        UpdateColor(toggle.isOn);
    }

    void UpdateColor(bool isOn)
    {
        if (isHovered)
        {
            targetImage.color = hoverColor;
        }
        else
        {
            targetImage.color = isOn ? onColor : offColor;
        }
    }

    void highlightToggle(bool isOn){
        GameObject parent = this.transform.parent.gameObject;
        string text = parent.GetComponent<TextMeshPro>().text;

        if (isOn){
            scratchBoard.addText(text);
        }
        if (!isOn){            
            scratchBoard.removeText(text);
        }


    }
}