using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    //si le joueur passe le checkpoint alors le waypoint si positionne
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<Move>().SetWaypoint(transform);
        }
    }
}
