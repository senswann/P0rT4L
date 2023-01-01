using UnityEngine;
using UnityEngine.InputSystem;

public class GravityGun : MonoBehaviour
{
    public Camera cam;
    public float maxGrabDistance = 10f, throwForce = 1f, lerpSpeed = 5f;
    [SerializeField] float dist = 1;
    public Transform objectHolder;
    private bool isGrabbed=false;

    Rigidbody grabbedRB;
    private void FixedUpdate()
    {
        if (grabbedRB && objectHolder)
        {
            grabbedRB.MovePosition(Vector3.Lerp(grabbedRB.position, objectHolder.transform.position + cam.transform.forward*dist, Time.fixedDeltaTime * lerpSpeed));
        }
    }
    public void OnGravityGun(InputAction.CallbackContext context)
    {
        if (!context.performed && context.canceled)
        {   
            return;
        }
        if (context.performed)
        {
            Debug.Log(grabbedRB + " " + isGrabbed);
            if (grabbedRB && isGrabbed)
            {
                isGrabbed = false;
                grabbedRB.isKinematic = false;
                grabbedRB.velocity = -objectHolder.transform.up;
                grabbedRB = null;
            }
            else
            {
                RaycastHit hit;
                Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
                if(Physics.Raycast(ray, out hit, maxGrabDistance))
                {
                    if(hit.collider.gameObject.tag == "Companion")
                        grabbedRB = hit.collider.gameObject.GetComponent<Rigidbody>();
                    if (grabbedRB)
                    {
                        isGrabbed = true;
                        grabbedRB.isKinematic = true;
                    }
                }
            }
        }
    }
}
