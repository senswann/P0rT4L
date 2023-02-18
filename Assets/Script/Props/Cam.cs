using UnityEngine;

public class Cam : MonoBehaviour
{
    // target de la camera
    [SerializeField] Transform target;

    // rigid body de la camera
    [SerializeField] Rigidbody rb;
    private void FixedUpdate()
    {
        //regarder dans la direction de la target
        transform.LookAt(target);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // si la camera rentre en contact avec le sol on freeze ca position
        if (collision.gameObject.tag == "ground")
            rb.constraints = RigidbodyConstraints.FreezePosition;
        //si un projectile touche la camera elle tombe
        if(collision.gameObject.tag == "Projectile" || collision.gameObject.tag == "Energy")
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }
    }
}
