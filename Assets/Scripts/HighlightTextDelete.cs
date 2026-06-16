using UnityEngine;

public class HighlightTextDelete : MonoBehaviour
{
    LetterManager letterManager;

    void Start()
    {
        letterManager = this.gameObject.transform.parent.parent.parent.gameObject.GetComponent<LetterManager>();
    }

    [ContextMenu("Delete")]
    public void remove()
    {
        Destroy(this.gameObject);
        letterManager.resizeRect();
        letterManager.alignRectTop();
    }
}
