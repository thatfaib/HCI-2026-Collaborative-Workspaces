using UnityEngine;
using UnityEngine.UI;

public class ToggleColorDriver : MonoBehaviour
{
    public Toggle toggle;
    public Image targetImage;

    public Color offColor = Color.gray;
    public Color onColor = Color.green;
    public Color hoverColor = Color.yellow;

    private bool isHovered;

    void Start()
    {
        toggle.onValueChanged.AddListener(UpdateColor);
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
}