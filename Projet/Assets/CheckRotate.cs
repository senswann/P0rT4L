using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("local rotation : " + transform.localRotation);
        Debug.Log("global rotation : " + transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
