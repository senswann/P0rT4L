using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] float cooldown = 3f;
    [SerializeField] Acsensor objAction;
    bool isTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" || other.tag == "Companion" && !isTrigger)
        {
            isTrigger = true;
            anim.SetTrigger("Use");
            objAction.Action();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Companion")
        {
            Invoke(nameof(Reset), cooldown);
        }
    }

    private void Reset() { 
        anim.SetTrigger("Up");
        objAction.ActionStop();
        isTrigger = false;
    }
}
