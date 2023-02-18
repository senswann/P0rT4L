using UnityEngine;

public class Button : MonoBehaviour
{
    //animation du boutton
    [SerializeField] Animator anim;

    //cooldown de desactivation du boutton (si set a 0 il n y a pas de desavtivation)
    [SerializeField] float cooldown = 3f;

    //variable stockant l objet a activer
    [SerializeField] ObjectAction objAction;

    //boolean utilier pour la limitation d action du bouton
    bool isTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        //si le joueur ou le companion cube pas sur le bouton on active son animation ainsi que l objet a activer
        if(other.tag == "Player" || other.tag == "Companion" && !isTrigger)
        {
            isTrigger = true;
            anim.SetTrigger("Use");
            objAction.Action();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //si le joueur ou le companion cube sort du bouton on desactive l objet apres un temps d attente
        if (other.tag == "Player" || other.tag == "Companion")
        {
            if (cooldown > 0f)
                Invoke(nameof(Reset), cooldown);
        }
    }

    //fonction permettant de reset l objet ainsi que le bouton
    private void Reset() { 
        anim.SetTrigger("Up");
        objAction.ActionStop();
        isTrigger = false;
    }
}
