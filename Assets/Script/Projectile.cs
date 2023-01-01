using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] Transform portalOrange;
    [SerializeField] Transform portalBlue;
    [SerializeField] float moveSpeed = 10f;
    public bool isParent = false;
    public bool isOrangeSide = false;
    [SerializeField] bool isPortal = false;

    [SerializeField] Slider orangeSlider;
    [SerializeField] Slider blueSlider;

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
            {
                Debug.Log("wall");
                Debug.Log(transform.forward);
                if (isOrangeSide)
                {
                    portalOrange.position = rb.position-(transform.forward*0.025f);
                    orangeSlider.value = 0f;
                }
                else
                {
                    portalBlue.position = rb.position - (transform.forward * 0.025f);
                    blueSlider.value = 0f;
                }

                Destroy(gameObject);
            }
            else
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
