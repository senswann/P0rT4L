using UnityEngine;

public class DisableChamp : MonoBehaviour
{
    [SerializeField] Animator doorAnimation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            doorAnimation.SetTrigger("Open");
            other.GetComponent<Move>().disableGravityGun = false;
        }
    }
}
