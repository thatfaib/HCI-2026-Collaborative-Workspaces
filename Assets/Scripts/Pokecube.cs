using UnityEngine;

public class Pokecube : MonoBehaviour
{
    public GameObject letter;

    public void up(){
        Vector3 pos = letter.transform.position;
        pos.y += 0.1f;
        letter.transform.position = pos;
    }

}
