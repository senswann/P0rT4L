using UnityEngine;

public class Portal : MonoBehaviour
{
    //transform du portail
    [SerializeField] Transform portal;

    // collider du portail
    [SerializeField] BoxCollider portalCollider;

    private void OnTriggerEnter(Collider other)
    {
        //si le joueur ou une boule d energie touche le portail il est teleporter a un autre portail
        if(other.tag == "Player" || other.tag == "Energy")
        {
            other.transform.position = portal.position + portal.up;
        }
    }
}
