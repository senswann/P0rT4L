using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = Vector3.zero;
    // on set la position de la caméra a celle du joueur
    void LateUpdate()
    {
        transform.position = player.position + offset;
        //transform.rotation = player.rotation;
    }
}
