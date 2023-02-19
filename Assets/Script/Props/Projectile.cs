using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    //rigid body du projectile
    Rigidbody rb;

    //transform des 2 portails
    [SerializeField] Transform portalOrange;
    [SerializeField] Transform portalBlue;

    //vitesse de deplacement du projectile
    [SerializeField] float moveSpeed = 10f;

    //esperance de vie du projectile
    [SerializeField] float liveTime = 6f;

    //boolean servant pour differencier les porjectile de portail ainsi que la boule d energie
    public bool isParent = false;
    public bool isOrangeSide = false;
    [SerializeField] bool isPortal = false;

    //UI lies au portails
    [SerializeField] Slider orangeSlider;
    [SerializeField] Slider blueSlider;

    //lors de son activation on recupere son rigid body
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    //fonction permettant le reset du lancer
    public void ResetLaunch()
    {
        rb.velocity = Vector3.zero;
    }

    //fonction permettant le lancer du projectile
    public void Launch(Vector3 direction, float force)
    {
        rb.AddForce(direction * force, ForceMode.VelocityChange);
    }

    void FixedUpdate()
    {
        //si elle va trop loin on la detruit
        if (transform.position.magnitude > 6000.0f)
        {
            Erase();
        }

        //si elle n est pas parent elle a une duree de vie
        if(!isParent)
            Invoke(nameof(Erase), liveTime);

        //on normalise la vitesse
        if (rb.velocity.magnitude != moveSpeed){
            rb.velocity = rb.velocity.normalized * moveSpeed;
        }
    }

    //fonction appeler pour la destruction du projectile
    private void Erase() { Destroy(gameObject); }

    private void OnCollisionEnter(Collision collision)
    {
        if (isPortal)
        {
            if(collision.gameObject.tag == "TpWall")
            {
                //si le projectile est un portail et qu il correspond au portail orange alors on fait apparaitre le portail bleu
                if (isOrangeSide)
                {
                    portalOrange.position = rb.position;
                    portalOrange.LookAt(collision.gameObject.GetComponent<TPWall>().GetTarget());
                    portalOrange.position += portalOrange.transform.forward * 0.25f;
                    orangeSlider.value = 0f;
                }
                //sinon si le projectile est un portail et qu il correspond au portail bleu alors on fait apparaitre le portail bleu
                else
                {
                    portalBlue.position = rb.position;
                    portalBlue.LookAt(collision.gameObject.GetComponent<TPWall>().GetTarget());
                    portalBlue.position += portalBlue.transform.forward * 0.25f;
                    blueSlider.value = 0f;
                }

                Destroy(gameObject);
            }
            else
                Destroy(gameObject);
        }
        else
        {
            //si se ne son pas des projectile de portail et qu il touche le joueur alors il meurt
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<Move>().Death();
                Debug.Log("Touch");
            }
        }
    }
}
