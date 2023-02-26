using UnityEngine;

public class DisableChamp : MonoBehaviour
{
    //animation de la porte de fin
    [SerializeField] Animator doorAnimation;

    //variable stockant les portails
    [SerializeField] GameObject orangePortal;
    [SerializeField] GameObject bluePortal;
    Vector3 startPositionOrange;
    Vector3 startPositionBlue;

    [SerializeField] bool disableGun = true;

    //on stock les positions de d�part des portails
    private void Start()
    {
        startPositionBlue= bluePortal.transform.position;
        startPositionOrange= orangePortal.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        //quand le joueur rentre dans le champ on start l'animation de la porte et l'on replace les portail a leur position initial
        if (other.tag == "Player")
        {
            doorAnimation.SetTrigger("Open");
            if(disableGun)
                other.GetComponent<Move>().disableGravityGun = true;
            GameManager.instance.ChangeSlide(false, 1f);
            GameManager.instance.ChangeSlide(true, 1f);
            bluePortal.transform.position = startPositionBlue;
            orangePortal.transform.position = startPositionOrange;
        }
    }
}
