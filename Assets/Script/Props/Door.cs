using UnityEngine;

public class Door : ObjectAction
{
    //animation de la porte
    [SerializeField] Animator anim;

    // fonction de la classe ObjectAction appeler lors du besoin d actionnement de l objet
    public override void Action()
    {
        anim.SetTrigger("Down");
    }

    // fonction de la classe ObjectAction appeler lors du besoin de desactivation de l objet
    public override void ActionStop()
    {
        anim.SetTrigger("Up");
    }
}
