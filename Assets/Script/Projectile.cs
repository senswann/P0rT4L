using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] Transform portal;
    [SerializeField] float moveSpeed = 10f;
    public bool isParent = false;
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
        if (transform.position.magnitude > 6000.0f)
        {
            Erase();
        }

        if(!isParent)
            Invoke(nameof(Erase), 15f);

        if (rb.velocity.magnitude != moveSpeed){
            rb.velocity = rb.velocity.normalized * moveSpeed;
        }
    }

    private void Erase() { Destroy(gameObject); }

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
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<Move>().Death();
                Debug.Log("Touch");
            }
        }
    }
}
