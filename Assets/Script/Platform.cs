using UnityEngine;

public class Platform : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Move>().vecMove = Vector3.zero;
                collision.gameObject.GetComponent<Move>().vecMove = Vector3.zero;
        }
    }
}
