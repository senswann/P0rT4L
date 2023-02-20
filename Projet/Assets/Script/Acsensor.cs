using UnityEngine;

public class Acsensor : MonoBehaviour
{
    [SerializeField] Animator anim;
    public void Action()
    {
        anim.SetTrigger("Up");
    }
    public void ActionStop()
    {
        anim.SetTrigger("Down");
    }
}
