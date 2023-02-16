using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructorAction : ObjectAction
{
    [SerializeField] GameObject lightObj;
    [SerializeField] GameObject Couvercle;
    [SerializeField] ObjectAction objAction;
    [SerializeField] Vector3 positionStartCube;
    bool isActive = false;
    private void Start()
    {
        lightObj.SetActive(false);
    }

    public override void Action()
    {
        Destroy(Couvercle);
        isActive = true;
    }
    public override void ActionStop(){}

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Companion" && isActive ==true)
        {
            lightObj.SetActive(true);
            objAction.Action();
            GameObject CompanionCube = Instantiate(collision.gameObject, positionStartCube, Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }
}
