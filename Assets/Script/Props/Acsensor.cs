using UnityEngine;

public class Acsensor : ObjectAction
{
    [SerializeField] Animator anim;
    public override void Action()
    {
        anim.SetTrigger("Up");
    }
    public override void ActionStop()
    {
        anim.SetTrigger("Down");
    }
}
