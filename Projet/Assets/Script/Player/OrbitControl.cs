using UnityEngine;
using UnityEngine.InputSystem;

public class OrbitControl : MonoBehaviour
{
    //variable de sensibilite de la souris 
    [Range(0.1f, 9f)] [SerializeField] float mouseSensibility = 2f;

    //variable utilise pour le blockage de la camera
    [Range(0f, 900f)] [SerializeField] float yRotationLimit = 880f;

    //variable stockant le joueur
    [SerializeField] private Move player;
    Vector2 rotation = Vector2.zero;

    //on lock le curseur de la souris pour il ne s affiche plus et corresponde a la camera
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.lockState = CursorLockMode.Locked;
    }

    //calul de la rotation a appliquer a la camera et au joueur 
    private void FixedUpdate()
    {
        var xQuat = Quaternion.AngleAxis(rotation.x * Time.fixedDeltaTime, Vector3.up);
        var yQuat = Quaternion.AngleAxis(rotation.y * Time.fixedDeltaTime, Vector3.left);

        transform.localRotation = xQuat * yQuat; 
        player.transform.localRotation = xQuat;
    }

    //action appeler quand la souris bouge sur l axe X
    public void OnMouseY(InputAction.CallbackContext context)
    {
        rotation.x += context.ReadValue<float>() * mouseSensibility;
    }

    //action appeler quand la souris bouge sur l axe Y
    public void OnMouseX(InputAction.CallbackContext context)
    {
        rotation.y += context.ReadValue<float>() * mouseSensibility;
        rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit*4, yRotationLimit*4);
    }

    //setter du rotation
    public void SetRotation(Quaternion _rotation){
        rotation.x= (_rotation.eulerAngles.y) * 50;
        rotation.y= _rotation.eulerAngles.x * 50;
        player.MoveForward(); 
    }
}
