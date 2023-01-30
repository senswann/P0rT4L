using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{
    [SerializeField] Transform portal;
    [SerializeField] BoxCollider portalCollider;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" || other.tag == "Energy")
        {
            other.transform.position = portal.position + portal.up;
        }
    }
}
