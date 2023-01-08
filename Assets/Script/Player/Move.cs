using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    public Vector3 vecMove = Vector3.zero;
    [SerializeField] Vector3 moveDir = Vector3.zero;
    [SerializeField] float moveSpeed = 7f;
    [SerializeField] float launchForce = 3000f;

    [SerializeField] float groundDrag;
    [SerializeField] float playerHeigth;
    [SerializeField] LayerMask WhatIsGround;
    bool grounded = false;

    [SerializeField] float jumpForce = 12f;
    [SerializeField] float jumpCooldown = 0.25f;
    [SerializeField] float airMulti = 0.4f;
    bool isJump = false;

    [SerializeField] Rigidbody rb;
    [SerializeField] Camera camPlayer;
    [SerializeField] GameObject projectileInstance;

    [SerializeField] Transform waypoint;

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

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed && context.canceled)
        {
            return;
        }
        if (context.performed)
        {
            if (grounded && !isJump)
            {
                isJump = true;
                Jump();
                Invoke(nameof(ResetJump),jumpCooldown);
            }
        }
    }
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
    private void Update()
    {
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeigth * 0.5f + 0.2f, WhatIsGround);

        if (grounded&!isJump)
            rb.drag = groundDrag;
        else
            rb.drag = 0f;
        LimitSpeed();
    }
    private void FixedUpdate()
    {
        moveDir = new Vector3(camPlayer.transform.forward.x,0f, camPlayer.transform.forward.z) * vecMove.y + new Vector3(camPlayer.transform.right.x, 0f, camPlayer.transform.right.z) * vecMove.x;

        if (grounded)
            rb.velocity = moveSpeed*moveDir.normalized;
        else if (!grounded)
            rb.AddForce(moveSpeed*airMulti*moveDir.normalized , ForceMode.Force);
    }

    void Launch(bool isOrangeSide)
    {
        GameObject projectileObject = Instantiate(projectileInstance, camPlayer.transform.position + camPlayer.transform.forward, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        if (isOrangeSide)
            projectile.isOrangeSide = true;
        projectile.Launch(camPlayer.transform.forward, launchForce);
    }

    private void LimitSpeed()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude >= moveSpeed)
        {
            Vector3 limiteVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limiteVel.x, rb.velocity.y, limiteVel.z);
        }
    }

    private void Jump()
    {
        rb.drag = 0f;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    public void Death()
    {
        transform.position = waypoint.position;
    }
    private void ResetJump() { isJump = false; }
}
