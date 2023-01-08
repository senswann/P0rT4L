using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{
    [SerializeField] Transform portal;
    [SerializeField] CapsuleCollider portalCollider;
    static bool isTeleport = false;
    bool disable = true;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !isTeleport)
        {
            other.transform.position = portal.position + portal.forward;
            StartCoroutine(DisablePortal());
        }
    }

    IEnumerator DisablePortal()
    {
        isTeleport = true;
        yield return new WaitForSeconds(0.5f);
        disable = false;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && isTeleport && !disable)
        {
            isTeleport = false;
            disable = true;
        }
    }
}
