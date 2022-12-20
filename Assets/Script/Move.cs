using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    public Vector3 vecMove = Vector3.zero;
    [SerializeField] Vector3 moveDir = Vector3.zero;
    [SerializeField] float moveSpeed = 7f;

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
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (!context.performed && context.canceled)
        {
            return;
        }
        if (context.performed)
        {
            Launch();
        }
    }
    private void Update()
    {
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeigth * 0.5f + 0.2f, WhatIsGround);

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
            rb.drag = 0f;
        LimitSpeed();
    }
    private void FixedUpdate()
    {
        //rb.position = new Vector3(rb.position.x+vecMove.y*0.1f, rb.position.y + vecMove.z, rb.position.z - vecMove.x * 0.1f);
        moveDir = new Vector3(camPlayer.transform.forward.x,0f, camPlayer.transform.forward.z) * vecMove.y + new Vector3(camPlayer.transform.right.x, 0f, camPlayer.transform.right.z) * vecMove.x;
        
        if(grounded)
            rb.AddForce(moveDir.normalized*moveSpeed*10f, ForceMode.Force);
        else if(!grounded)
            rb.AddForce(moveDir.normalized * moveSpeed * 10f * airMulti, ForceMode.Force);
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectileInstance, rb.position + Vector3.one, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(Vector3.zero, 300);
    }

    private void LimitSpeed()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limiteVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limiteVel.x, rb.velocity.y, limiteVel.z);
        }
    }

    private void Jump()
    {
        Debug.Log("Jump");
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump() { isJump = false; }
}
