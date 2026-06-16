using UnityEngine;

public class SwitchPanel : MonoBehaviour
{
    public GameObject[] panels;
    
    GameObject activePanel;
    ScratchBoard scratchBoard;
    
    public GameObject scratchBoardPanel; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        activePanel = panels[0];
        foreach (var panel in panels)
        {
            panel.SetActive(false);            
        }
        activePanel.SetActive(true);
        scratchBoardPanel = panels[2];
        scratchBoard = scratchBoardPanel.GetComponent<ScratchBoard>();
    }    
    
    [ContextMenu("Switch to 0")]
    void switch0(){
        switchPanel(0);
    }
    [ContextMenu("Switch to 1")]
     void switch1(){
        switchPanel(1);
    }
    [ContextMenu("Switch to 2")]
     void switch2(){
        switchPanel(2);
    }


    void switchPanel(int id){
        activePanel.SetActive(false);
        activePanel = panels[id];
        activePanel.SetActive(true);
        LetterManager letterManager = activePanel.GetComponent<LetterManager>();
        letterManager.resizeRect();
        letterManager.alignRectTop();
    }
}
