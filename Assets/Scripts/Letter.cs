using UnityEngine;

[CreateAssetMenu(fileName = "New Letter", menuName = "ScriptableObjects/Letter", order = 1)]
public class Letter : ScriptableObject
{
    public string title, heading;
    public Segment[] segments;

    publ ic class Segment {
        public sting textbody;
    }   
}
