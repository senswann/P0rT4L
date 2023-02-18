using UnityEngine;

public class DestructorAction : ObjectAction
{
    //element lie a l incinerateur
    [SerializeField] GameObject lightObj;
    [SerializeField] GameObject Couvercle;

    //objet a actionner
    [SerializeField] ObjectAction objAction;

    // position de start de l objet
    [SerializeField] Vector3 positionStartCube;
    
    //boolean permettant l activation de l incinerateur
    bool isActive = false;

    private void Start()
    {
        //on desactive au debut les lumieres
        lightObj.SetActive(false);
    }

    // fonction de la classe ObjectAction appeler lors du besoin d actionnement de l objet
    public override void Action()
    {
        Destroy(Couvercle);
        isActive = true;
    }

    // fonction de la classe ObjectAction appeler lors du besoin de desactivation de l objet
    public override void ActionStop(){}

    private void OnTriggerEnter(Collider collision)
    {
        //si l incinerateur est activer et qu on lui donne le companion cube il active l objet
        if (collision.gameObject.tag == "Companion" && isActive ==true)
        {
            lightObj.SetActive(true);
            objAction.Action();
            GameObject CompanionCube = Instantiate(collision.gameObject, positionStartCube, Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }
}
