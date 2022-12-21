using UnityEngine;
using UnityEngine.InputSystem;

public class GravityGun : MonoBehaviour
{
    public Camera cam;
    public float maxGrabDistance = 10f, throwForce = 1f, lerpSpeed = 5f;
    [SerializeField] float dist = 1;
    public Transform objectHolder;

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
            if (grabbedRB)
            {
                grabbedRB.isKinematic = false;
                grabbedRB.velocity = -objectHolder.transform.up;
                grabbedRB = null;
            }
            return;
        }
        if (context.performed)
        {
            RaycastHit hit;
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            if(Physics.Raycast(ray, out hit, maxGrabDistance))
            {
                if(hit.collider.gameObject.tag == "Companion")
                    grabbedRB = hit.collider.gameObject.GetComponent<Rigidbody>();
                if (grabbedRB)
                {
                    grabbedRB.isKinematic = true;
                }
            }

        }
    }
}
