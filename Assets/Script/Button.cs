using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] float cooldown = 3f;
    [SerializeField] Acsensor objAction;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" || other.tag == "Companion")
        {
            anim.SetTrigger("Use");
            objAction.Action();
            Invoke(nameof(Reset), cooldown);
        }
    }
    private void Reset() { 
        anim.SetTrigger("Up");
        objAction.ActionStop();
    }
}
