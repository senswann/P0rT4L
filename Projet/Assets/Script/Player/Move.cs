using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Move : MonoBehaviour
{
    //variable lie au mouvement du joueur
    public Vector3 vecMove = Vector3.zero;
    [SerializeField] Vector3 moveDir = Vector3.zero;
    [SerializeField] float moveSpeed = 7f;

    //variable lie au gravity gun
    [SerializeField] float launchForce = 3000f;
    [SerializeField] GameObject projectileInstance;
    public bool disableGravityGun = false;

    //variable lie a la detection du ground
    [SerializeField] float groundDrag;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    //variable lie au jump du joueur
    [SerializeField] float jumpForce = 12f;
    [SerializeField] float jumpCooldown = 0.25f;
    [SerializeField] float airMulti = 0.4f;
    bool isJump = false;

    //rigid body du joueur
    [SerializeField] Rigidbody rb;

    //camera du joueur
    [SerializeField] Camera camPlayer;

    //point de respawn du joueur
    [SerializeField] Transform waypoint;

    bool forceGrounded = false;
    
    //setter du waypoint du joueur
    public void SetWaypoint(Transform _waypoint)
    {
        waypoint = _waypoint;
    }

    //action appeler quand le joueur veux bouger
    public void OnMove(InputAction.CallbackContext context)
    {
        if (!context.performed && context.canceled)
        {
            return;
        }
        if (!context.canceled)
        {
            vecMove = context.ReadValue<Vector3>();
        }
    }

    //action appeler quand le joueur veux jump
    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed && context.canceled)
        {
            return;
        }
        if (context.performed)
        {
            // on peux jump seulement si l on est pas entrain de jump et si l on touche le sol
            if (isGrounded() && !isJump)
            {
                isJump = true;
                Jump();
                Invoke(nameof(ResetJump),jumpCooldown);
            }
        }
    }

    //action appeler quand le joueur veux lancer le portail orange
    public void OnShootOrange(InputAction.CallbackContext context)
    {
        if (!context.performed && context.canceled)
        {
            return;
        }
        if (context.performed)
        {
            Launch(true);
        }
    }

    //action appeler quand le joueur veux lancer le portail bleu
    public void OnShootBlue(InputAction.CallbackContext context)
    {
        if (!context.performed && context.canceled)
        {
            return;
        }
        if (context.performed)
        {
            Launch(false);
        }
    }

    //permet de savoir si le joueur touche le sol
    bool isGrounded()
    {
        bool result =true;

        if (!forceGrounded)
        {
            result = Physics.CheckSphere(groundCheck.position, .1f, groundLayer);
        }
        else
        {
            result = true;
        }
        return result;
    }

    private void FixedUpdate()
    {
        //on limite la vitesse du joueur
        LimitSpeed();
        //mouvement du joueur
        moveDir = new Vector3(camPlayer.transform.forward.x,0f, camPlayer.transform.forward.z) * vecMove.y + new Vector3(camPlayer.transform.right.x, 0f, camPlayer.transform.right.z) * vecMove.x;
        //gestion du deplacement du joueur selon si il est en l air ou non
        if (isGrounded())
        {
            rb.velocity = moveSpeed*moveDir.normalized;
        }
        else
            rb.AddForce(moveSpeed*airMulti*moveDir.normalized , ForceMode.Force);
    }

    //corrutine pour forcer le ground quand l'on passe le portail;
    //fonction forcant le déplacement du joueur
    public void MoveForward(){ StartCoroutine(ForcedGroundedCheck()); }
    IEnumerator ForcedGroundedCheck()
    {
        forceGrounded = true;
        yield return new WaitForSeconds(0.1f);
        forceGrounded = false;

    }

    //fonction lancant les projectile servant a créer les portails
    void Launch(bool isOrangeSide)
    {
        if (!disableGravityGun)
        {
            GameObject projectileObject = Instantiate(projectileInstance, camPlayer.transform.position + camPlayer.transform.forward, Quaternion.identity);
            Projectile projectile = projectileObject.GetComponent<Projectile>();
            if (isOrangeSide)
                projectile.isOrangeSide = true;
            projectile.Launch(camPlayer.transform.forward, launchForce, null);
        }
    }

    //fonction permettant la limitation de la vitesse du joueur
    private void LimitSpeed()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude >= moveSpeed)
        {
            Vector3 limiteVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limiteVel.x, rb.velocity.y, limiteVel.z);
        }
    }

    //fonction appeler pour faire jump le joueur
    private void Jump()
    {
        rb.drag = 0f;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    //reset du jump
    private void ResetJump() { isJump = false; }

    //fonction permettant le respawn du joueur a sa mort
    public void Death()
    {
        transform.position = waypoint.position;
    }
    
}
