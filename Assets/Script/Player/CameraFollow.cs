using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Transform du joueur
    public Transform player;
    public Vector3 offset = Vector3.zero;

    // on set la position de la camera a celle du joueur
    void LateUpdate()
    {
        transform.position = player.position + offset;
        //transform.rotation = player.rotation;
    }
}
