using UnityEngine;

public class Portal : MonoBehaviour
{
    //transform du portail
    [SerializeField] Transform portal;
    [SerializeField] Transform portalContainer;

    //camera du joueur
    [SerializeField] OrbitControl orbitalControl;
    private void OnTriggerEnter(Collider other)
    {
        //si le joueur ou une boule d energie touche le portail il est teleporter a un autre portail
        if(other.tag == "Player" || other.tag == "Energy")
        {
            
           other.transform.position = portal.position + portal.up;

            if (other.tag == "Energy")
            {
                other.GetComponent<Projectile>().ResetLaunch();
                other.GetComponent<Projectile>().Launch(portal.up * 2f, 5.5f,other.GetComponent<Projectile>().GetShooter());
            }
            else
            {
                orbitalControl.SetRotation(portalContainer.rotation);
            }
        }
    }
}
