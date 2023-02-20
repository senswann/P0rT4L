using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructorAction : MonoBehaviour
{
    [SerializeField] GameObject lightObj;

    private void Start()
    {
        lightObj.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Companion")
        {
            lightObj.SetActive(true);
            GameObject CompanionCube = Instantiate(collision.gameObject, new Vector3(0,3.286f, 0), Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }
}
