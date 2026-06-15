using UnityEngine;
using System.Collections;

public class PlayerLoad : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(resetPosition());
        
    }

    IEnumerator resetPosition(){
        yield return new WaitForSeconds(0.02f);
          this.transform.position = new Vector3(0.5f,0f,-0.3f);
    }

   
}
