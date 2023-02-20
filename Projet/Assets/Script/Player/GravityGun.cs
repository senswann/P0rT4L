using UnityEngine;
using UnityEngine.InputSystem;

public class GravityGun : MonoBehaviour
{
    //camera du joueur
    public Camera cam;

    // float servant de parametre pour le lancer de projectile
    public float maxGrabDistance = 10f, throwForce = 1f, lerpSpeed = 5f;

    //variable du joueur
    [SerializeField] Move player;

    //distance du grab
    [SerializeField] float dist = 1;

    //Transform ou va aller l'objet grab
    public Transform objectHolder;

    //boolean pour savoir si il est entrain de grab un objet
    private bool isGrabbed=false;

    //rigid body de l'objet grab
    Rigidbody grabbedRB;
    Collider grabbedCollider;
    Transform grabbedTransform;

    private void FixedUpdate()
    {
        //si l'on a bien le rigid body de l'objet ainis que ca target position, ou lui dit d'aller vers cette position
        if (grabbedRB && objectHolder)
        {
            grabbedRB.MovePosition(Vector3.Lerp(grabbedRB.position, objectHolder.transform.position + cam.transform.forward*dist, Time.fixedDeltaTime * lerpSpeed));
            grabbedTransform.LookAt(cam.transform);
        }
    }

    //Action de grab un objet quand la touche d'action est utilisé
    public void OnGravityGun(InputAction.CallbackContext context)
    {
        if (!context.performed && context.canceled)
        {   
            return;
        }
        if (context.performed)
        {
            //si on a déja un objet grab on le lache
            if (grabbedRB && isGrabbed)
            {
                isGrabbed = false;
                Physics.IgnoreCollision(grabbedCollider, GetComponent<Collider>(), false);
                grabbedRB.useGravity = true;
                grabbedRB.velocity = -objectHolder.transform.up;
                grabbedRB = null;
                grabbedCollider = null;
            }
            //sinon on lance un raycast parcourant une distance defini pour savoir si l'on touche quelque chose
            else
            {
                RaycastHit hit;
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
                if(Physics.Raycast(ray, out hit, maxGrabDistance))
                {
                    //si il touche le compannion cube on le grab
                    if(hit.collider.gameObject.tag == "Companion")
                    {
                        grabbedRB = hit.collider.gameObject.GetComponent<Rigidbody>();
                        grabbedCollider = hit.collider;
                        grabbedTransform = hit.transform;
                    }
                    //sinon si l'on touche un boutton on l'actionne
                    else if (hit.collider.gameObject.tag == "Button")
                        hit.collider.gameObject.GetComponent<ButtonAction>().Action();
                    if (grabbedRB)
                    {
                        isGrabbed = true;
                        grabbedRB.useGravity = false;
                        Physics.IgnoreCollision(grabbedCollider, GetComponent<Collider>(), true);
                        
                    }
                }
            }
            player.disableGravityGun = isGrabbed;
        }
    }
}
