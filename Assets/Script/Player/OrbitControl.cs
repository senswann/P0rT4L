using UnityEngine;
using UnityEngine.InputSystem;

public class OrbitControl : MonoBehaviour
{
    [Range(0.1f, 9f)] [SerializeField] float mouseSensibility = 2f;
    [Range(0f, 900f)] [SerializeField] float yRotationLimit = 880f;
    [SerializeField] private GameObject player;
    Vector2 rotation = Vector2.zero;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        var xQuat = Quaternion.AngleAxis(rotation.x * Time.fixedDeltaTime, Vector3.up);
        var yQuat = Quaternion.AngleAxis(rotation.y * Time.fixedDeltaTime, Vector3.left);

        transform.localRotation = xQuat * yQuat;
        player.transform.localRotation = xQuat;
    }
    public void OnMouseY(InputAction.CallbackContext context)
    {
        rotation.x += context.ReadValue<float>() * mouseSensibility;
        //Debug.Log("Horizontal "+context.ReadValue<Vector2>().y);
    }

    public void OnMouseX(InputAction.CallbackContext context)
    {
        rotation.y += context.ReadValue<float>() * mouseSensibility;
        rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit*4, yRotationLimit*4);
        //Debug.Log("Vertical " + context.ReadValue<Vector2>().x);
    }
}
