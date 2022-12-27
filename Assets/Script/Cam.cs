using UnityEngine;

public class Cam : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Rigidbody rb;
    private void FixedUpdate()
    {
        transform.LookAt(target);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
            rb.constraints = RigidbodyConstraints.FreezePosition;
        if(collision.gameObject.tag == "Projectile")
        {
            Debug.Log("cam");
            rb.isKinematic = false;
            rb.useGravity = true;
        }
    }
}
