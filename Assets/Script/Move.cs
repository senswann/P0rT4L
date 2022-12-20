using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    public Vector3 vecMove = Vector3.zero;
    public float verticalMove = 0f;

    [SerializeField] Rigidbody rb;
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
            verticalMove=0f;
            return;
        }
        if (!context.canceled)
        {
            verticalMove = 0.2f;
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

    private void FixedUpdate()
    {
        rb.position = new Vector3(rb.position.x+vecMove.y*0.1f, rb.position.y + verticalMove, rb.position.z - vecMove.x * 0.1f);
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectileInstance, rb.position + Vector3.one, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(Vector3.zero, 300);
    }
}
