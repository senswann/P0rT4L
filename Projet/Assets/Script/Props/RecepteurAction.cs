using UnityEngine;

public class RecepteurAction : MonoBehaviour
{
    // lumiere lier a l objet
    [SerializeField] GameObject lightObj;

    // objet a actionner
    [SerializeField] ObjectAction objAction;

    // objet a actionner
    [SerializeField] ActivationLight[] activeLight;

    //boolean pour le faire qu'une fois
    bool once =true;

    //desactivation des lumieres au start
    private void Start()
    {
        lightObj.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        //si une boule d energie rentre en contact avec le recepteur il active l objet
        if(collision.gameObject.tag == "Energy" && once)
        {
            once = false;
            collision.gameObject.GetComponent<Projectile>().GetShooter().Stop();
            lightObj.SetActive(true);
            objAction.Action();
            Destroy(collision.gameObject);
            foreach (var element in activeLight){
                element.Action();
            }
        }
    }
}
