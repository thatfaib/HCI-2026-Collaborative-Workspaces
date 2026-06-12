using UnityEngine;

[CreateAssetMenu(fileName = "New Letter", menuName = "ScriptableObjects/Letter", order = 1)]
public class Letter : ScriptableObject
{
    public string title, heading;
    public string[] segments;

     
}
