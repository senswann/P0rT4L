using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] Transform portal;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] bool isPortal = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Launch(Vector3 direction, float force)
    {
        rb.AddForce(direction * force, ForceMode.VelocityChange);
    }

    void FixedUpdate()
    {
        if (transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }

        if (rb.velocity.magnitude != moveSpeed){
            rb.velocity = rb.velocity.normalized * moveSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isPortal)
        {
            if(collision.gameObject.tag == "TpWall")
                portal.position = rb.position;
            Destroy(gameObject);
        }
        else
        {

        }
    }
}
